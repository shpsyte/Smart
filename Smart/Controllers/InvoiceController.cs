using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Authorization;
using Services.Interfaces;
using Smart.Services;
using Core.Domain.Sale;
using Smart.Data;
using Core.Domain.PersonAndData;
using Core.Domain.Production;
using Microsoft.AspNetCore.Mvc.Rendering;
using Core.Domain.Accounting;
using System.Collections.Generic;
using Smart.Extensions.Invoices;
using Smart.Models.InvoiceModel;
using Data.Context;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Smart.Helpers;

namespace Smart.Controllers
{
    [Authorize]
    public class InvoiceController : BaseController
    {
        #region vars
        private readonly IServices<Invoice> _invoiceServices;
        private readonly IServices<InvoiceDetail> _invoinceDetailsServices;
        private readonly IServices<Product> _productServices;
        private readonly IServices<Address> _addressServices;
        private readonly IServices<Person> _personServices;
        private readonly IServices<Location> _locationServices;
        private readonly IServices<TaxOperation> _taxOperationServices;
        private readonly InvoiceExtension _invoiceExtension;

        #endregion
        #region ctor
        public InvoiceController(
                                IServices<Address> addressServices,
                                IServices<Person> personServices,
                                IServices<InvoiceDetail> invoinceDetailsServices,
                                IServices<Location> locationServices,
                                IServices<Invoice> invoiceServices,
                                IServices<Product> productServices,
                                InvoiceExtension invoiceExtension,
                                IServices<TaxOperation> taxOperationServices,
                                IUser currentUser,
                                IEmailSender emailSender,
                                IHttpContextAccessor accessor,
                                SmartContext context
                                ) : base(currentUser, emailSender, accessor, context)
        {
            this._invoiceServices = invoiceServices;
            this._addressServices = addressServices;
            this._personServices = personServices;
            this._locationServices = locationServices;
            this._taxOperationServices = taxOperationServices;
            this._invoinceDetailsServices = invoinceDetailsServices;
            this._productServices = productServices;
            this._invoiceExtension = invoiceExtension;
        }
        #endregion


        private void LoadViewData(int id = 0)
        {

            ViewData["CarrierId"] = new SelectList(_personServices.GetAll(a => a.PersonType == 5 || a.PersonType == 6), "PersonId", "FirstName");
            ViewData["WarehouseId"] = new SelectList(_locationServices.GetAll(), "WarehouseId", "Name");
            ViewData["SalesPersonId"] = new SelectList(_personServices.GetAll(a => a.PersonType == 3 || a.PersonType == 6), "PersonId", "FirstName");
            ViewData["TaxOperationId"] = new SelectList(_taxOperationServices.GetAll(), "TaxOperationId", "Name");


            var produts = _invoinceDetailsServices.Query(a => a.InvoiceId == id).Include(a => a.Product).ToList();
            ViewData["products"] = produts;

            ViewData["ErrorMessage"] = ErrorMessage;
        }


        [TempData]
        public string ErrorMessage { get; set; }
        #region methods

        // GET: Invoice
        [Route("invoice-management/invoice-list")]
        public async Task<IActionResult> List(string search)
        {
            ViewData["search"] = search;
            ViewData["ErrorMessage"] = ErrorMessage;
            var data = await _invoiceServices.QueryAsync(a => a.Status != 6);
            if (!string.IsNullOrEmpty(search))
            {
                data = data.Where(p =>
                            p.InvoiceNumber.Contains(search)
                    || p.PurchaseOrderNumber.Contains(search)
                    || p.AccountNumber.Contains(search)
                    || p.CarrierTrackingNumber.Contains(search)
                    || p.Package.Contains(search)
                    || p.CarrierTruckId.Contains(search)
                    || p.Comment.Contains(search)
                 );
            }
            return View(data.Include(a => a.Customer));
        }

        [HttpPost, ValidateAntiForgeryToken]
        public JsonResult GetProduct(string name, int TaxOperationID, int id, int locationId, IList<InvoiceProductModel> product)
        {
            var products = _productServices.QueryAsync(a => a.Name.Contains(name) || a.ProductNumber.Contains(name)).Result;
            var tax = _taxOperationServices.SingleOrDefaultAsync(a => a.TaxOperationId == TaxOperationID).Result;

            var ret = (from a in products
                       select new
                       InvoiceProductModel()
                       {
                           id = id,
                           Seq = (product.Max(p => (int?)p.Seq) ?? 0) + 1,
                           ProductId = a.ProductId,
                           Name = a.Name.ToLower().Titleize(),
                           ProductNumber = a.ProductNumber,
                           Stock = a.VProduct.Stock,
                           CodOper = tax.DefaultCode,
                           TaxOperationId = tax.TaxOperationId,
                           Product = a,
                           Discont = 0,
                           Qty = 1,
                           TaxProduction = a.TaxProduction.HasValue ? a.TaxProduction.Value : 0,
                           WarehouseId = locationId,
                           TaxSales = a.TaxSale.HasValue ? a.TaxSale.Value : 0,
                           UnitPrice = a.ListPrice.HasValue ? a.ListPrice.Value : 0,
                           Total = (a.ListPrice.HasValue ? a.ListPrice.Value : 0) * 1,
                           StandartCost = a.StandardCost.HasValue ? a.StandardCost.Value : 0
                       });


            return Json(ret);
        }
        [HttpPost, ValidateAntiForgeryToken]
        public JsonResult GetCustomer(string name)
        {
            var customer = _personServices.QueryAsync(a => a.FirstName.Contains(name) || a.LastName.Contains(name) || a.RegistrationCode.Contains(name)).Result;
            var ret = (from a in customer
                       select new
                       {
                           CustomerId = a.PersonId,
                           FirstName = a.FirstName,
                           LastName = a.LastName,
                           RegCod = a.RegistrationCode,
                           OpenValue = a.VRevenue.Sum(e => e.Total)
                       });


            return Json(ret);
        }

        // GET: Invoice/Add
        [Route("invoice-management/invoice-add")]
        public IActionResult Add()
        {
            LoadViewData(0);
            var data = new Invoice() { Sequence = 1, Number = 0, InvoiceDate = System.DateTime.Now, RevisionNumber = 1 };
            return View(data);
        }

        // POST: Invoice/Add
        [HttpPost, ValidateAntiForgeryToken]
        [Route("invoice-management/invoice-add")]
        public async Task<IActionResult> Add([Bind("InvoiceId,TaxOperationId,RevisionNumber,InvoiceDate,InvoiceCode,DueDate,ShipDate,Status,OnlineInvoiceFlag,InvoiceNumber,PurchaseOrderNumber,AccountNumber,CustomerId,SalesPersonId,CarrierId,WarehouseId,BillToAddressId,ShipToAddressId,CarrierTrackingNumber,FreightType,Volumes,Package,CarrierTruckId,Weight,TotalWeight,SubTotal,TaxAmt,Freight,TotalDue,Comment,ModifiedDate,BusinessEntityId,OrderQty,ProductId,Finality,InvoiceTp,Number,Sequence,ProductId,Name,Qty,UnitPrice,TaxProduction,TaxSales,CodOper,Discount,Total,CustomerName")] Invoice invoice, List<InvoiceProductModel> product, bool continueAdd)
        {
            if (invoice.Number == 0)
                invoice.Number = (_invoiceServices.Query(a => a.Sequence == invoice.Sequence).Max(p => (int?)p.Number) ?? 0) + 1;


            product.RemoveAll(e => e.ProductId == 0);

            ModelState.Clear();
            var validInvoice = TryValidateModel(invoice);
            var validProduct = TryValidateModel(product);
           
            if (validInvoice)
            {
                if (product.Any())
                {
                    invoice.BusinessEntityId = _BusinessId;
                    List<InvoiceDetail> invoiceDetailList = _invoiceExtension.CreateInvoiceDetail(product, invoice).ToList();
                    invoice.SubTotal = invoiceDetailList.Sum(a => a.LineTotal);
                    invoiceDetailList.ForEach(a => _invoinceDetailsServices.AddAsyncNoSave(a));
                    ViewData["products"] = invoiceDetailList;
                }
                else
                {
                    await _invoiceServices.AddAsyncNoSave(invoice);
                }


                try
                {
                    await _context.SaveChangesAsync();
                    ErrorMessage = $"Nota incluída com sucesso { invoice.Number}";
                    return continueAdd ? RedirectToAction(nameof(Add)) : RedirectToAction(nameof(List));
                }
                catch (System.Exception e)
                {
                    ErrorMessage = $"Erro na inclusão da Nota: { e }";
                    return View(invoice);
                }


            }
            string messages = string.Join("; ", ModelState.Values
                                        .SelectMany(x => x.Errors)
                                        .Select(x => x.ErrorMessage));

            ErrorMessage = $"Erro, Nota inválida par alnaçamento: { messages }";
            
            LoadViewData();
            return View(invoice);
        }

        // GET: Invoice/Edit/5
        [Route("invoice-management/invoice-edit/{id?}")]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return RedirectToAction("AccessDenied", "Account");
            }

            var invoice = await _invoiceServices.QueryAsync(m => m.InvoiceId == id);


            if (invoice == null)
            {
                return RedirectToAction("AccessDenied", "Account");
            }

            LoadViewData(id.Value);
            
            return View(invoice.Include(a => a.Customer).First());
        }

        // POST: Invoice/Edit/5
        [HttpPost, ValidateAntiForgeryToken]
        [Route("invoice-management/invoice-edit/{id?}")]
        public async Task<IActionResult> Edit(int id, [Bind("InvoiceId,TaxOperationId,RevisionNumber,InvoiceDate,InvoiceCode,DueDate,ShipDate,Status,OnlineInvoiceFlag,InvoiceNumber,PurchaseOrderNumber,AccountNumber,CustomerId,SalesPersonId,CarrierId,WarehouseId,BillToAddressId,ShipToAddressId,CarrierTrackingNumber,FreightType,Volumes,Package,CarrierTruckId,Weight,TotalWeight,SubTotal,TaxAmt,Freight,TotalDue,Comment,ModifiedDate,BusinessEntityId,OrderQty,ProductId,Finality,InvoiceTp,Number,Sequence,ProductId,Name,Qty,UnitPrice,TaxProduction,TaxSales,CodOper,Discount,Total,CustomerName")] Invoice invoice, List<InvoiceProductModel> product, bool continueAdd, bool addTrash,string CustomerName)
        {
            if (id != invoice.InvoiceId)
            {
                return NotFound();
            }

            product.RemoveAll(e => e.ProductId == 0);

            ModelState.Clear();
            var validInvoice = TryValidateModel(invoice);
            var validProduct = TryValidateModel(product);

             

            if (validInvoice)
            {
                    //delete any product
                await _invoinceDetailsServices.Query(a => a.InvoiceId == id).ForEachAsync(
                      item => _invoinceDetailsServices.DeleteNoSave(item));

                if (product.Any())
                {
                    invoice.BusinessEntityId = _BusinessId;
                    List<InvoiceDetail> invoiceDetailList = _invoiceExtension.CreateInvoiceDetail(product, null).ToList();
                    invoice.SubTotal = invoiceDetailList.Sum(a => a.LineTotal);
                    invoiceDetailList.ForEach(a => a.InvoiceId = id);
                    invoiceDetailList.ForEach(a => _invoinceDetailsServices.AddAsyncNoSave(a));
                }

                try
                {
                    await _invoiceServices.UpdateAsync(invoice);
                    ErrorMessage = $"Nota Alterada com sucesso: { invoice.Number }";
                    return continueAdd ? RedirectToAction(nameof(Add)) : RedirectToAction(nameof(List));
                }
                catch (System.Exception e)
                {
                    ErrorMessage = $"Erro na inclusão da Nota: { e }";
                    return View(invoice);
                }


            }

            string messages = string.Join("; ", ModelState.Values
                                       .SelectMany(x => x.Errors)
                                       .Select(x => x.ErrorMessage));

            ErrorMessage = $"Nota inválida par alnaçamento: { messages }";
            LoadViewData(id);
            invoice.CustomerName = CustomerName;
            return View(invoice);
        }
 
        #endregion methods
    }
}
