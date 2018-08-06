using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Authorization;
using Services.Interfaces;
using Smart.Services;
using Core.Domain.Production;
using Smart.Data;
using Core.Domain.Accounting;
using Microsoft.AspNetCore.Mvc.Rendering;
using Smart.Helpers;
using System.Collections.Generic;
using System.IO;
using Data.Context;
using Smart.Models.Production;

namespace Smart.Controllers
{
    [Authorize]
    public class ProductController : BaseController
    {
        #region vars
        private readonly IServices<Product> _productServices;
        private readonly IServices<ProductImage> _productImageServices;
        private readonly IServices<Image> _imageServices;
        private readonly IServices<CategoryProduct> _categoryProductServices;
        private readonly IServices<ClassProduct> _classProductServices;
        private readonly IServices<TaxGroup> _taxGroupServices;
        private readonly IServices<ProductInventory> _productInventoryServices;
        private readonly IServices<Location> _locationServices;

        #endregion
        #region ctor
        public ProductController(
                                IServices<ProductImage> productImageServices,
                                IServices<Image> imageServices,
                                IServices<CategoryProduct> categoryProductServices,
                                IServices<ClassProduct> classProductServices,
                                IServices<TaxGroup> taxGroupServices,
                                IServices<Product> productServices,
                                IServices<ProductInventory> productInventoryServices,
                                IServices<Location> locationServices,
                                IUser currentUser,
                                IEmailSender emailSender,
                                IHttpContextAccessor accessor,
                                SmartContext context
                                ) : base(currentUser, emailSender, accessor, context)
        {
            this._productServices = productServices;
            this._categoryProductServices = categoryProductServices;
            this._classProductServices = classProductServices;
            this._taxGroupServices = taxGroupServices;
            this._productServices = productServices;
            this._imageServices = imageServices;
            this._productImageServices = productImageServices;
            this._productInventoryServices = productInventoryServices;
            this._locationServices = locationServices;
        }
        #endregion
        #region privates
          [TempData]
        public string ErrorMessage { get; set; }
        private void LoadViewData()
        {
            ViewData["CategoryId"] = new SelectList(_categoryProductServices.GetAll(), "CategoryId", "Name");
            ViewData["ClassId"] = new SelectList(_classProductServices.GetAll(), "ClassId", "Name");
            ViewData["TaxGroupId"] = new SelectList(_taxGroupServices.GetAll(), "TaxGroupId", "Name");
            ViewData["LocationId"] = new SelectList(_locationServices.GetAll(), "WarehouseId", "Name");
        }
        #endregion
        #region methods


        //public JsonResult GetProduct(string name)
        //{
        //    var products = _productServices.QueryAsync(a => a.Name.Contains(name)).Result;
        //    var ret = (from a in products select new { id = a.ProductId, name = a.Name, cod = a.ProductNumber });
        //    return Json(ret);
        //}


        [HttpPost, ValidateAntiForgeryToken]
        public JsonResult GetNextCode(string partialName)
        {
            string next = ((_productServices.Query().Max(p => (int?)p.ProductId) ?? 0) + 1).ToString("0000");
            if (!string.IsNullOrEmpty(partialName))
            {
                next = partialName.Truncate(3, false).ToUpper() + next.ToString();
            }
            return Json(next);
        }

        [HttpPost, ValidateAntiForgeryToken]
        public async Task<JsonResult> RemoveImageAsync(int id)
        {
            var image = _imageServices.SingleOrDefaultAsync(a => a.ImageId == id).Result;
            var productimage = _productImageServices.SingleOrDefaultAsync(a => a.ImageId == id).Result;

            if (image != null)
                _imageServices.Delete (image, false);

            if (productimage != null)
                _productImageServices.Delete (productimage, false);

            try
            {
                await _context.SaveChangesAsync();
                return Json(new { ok = "ok" });
            }
            catch (System.Exception)
            {
                return Json(new { ok = "nok" });
            }

        }

        [HttpPost, ValidateAntiForgeryToken]
        [Route("product-management/product-inventory")]
        public async Task<IActionResult> Inventory([Bind("Id,BusinessEntityID,LocationId,ProductId,VarId,Shelf,Bin,CreateDate,Description,NumberDoc,Quantity,Signal")] ProductInventory data)
        {
            if (ModelState.IsValid)
            {
                try
                {
                await _productInventoryServices.AddAsync(data);
                ErrorMessage = $"Movimentação efetuada com sucesso";

                }
                catch (System.Exception e)
                {
                    ErrorMessage = $"Erro na Movimentação {(string.IsNullOrEmpty(e.Message) ? "" : e.Message)}";

                }
            }else
            {
                ErrorMessage = $"Erro na movimentação";
            }
             
            return RedirectToAction(nameof(List));
        }


        // GET: Product
        [Route("product-management/product-list")]
        public async Task<IActionResult> List(string search)
        {
            LoadViewData();
            ViewData["search"] = search;
            ViewData["ErrorMessage"] = ErrorMessage;
            var data = await _productServices.QueryAsync();
            data = data.Where(a => a.Deleted == false);
            if (!string.IsNullOrEmpty(search))
            {
                data = data.Where(p =>
                            p.Name.Contains(search)
                    || p.ProductNumber.Contains(search)
                    || p.Manufacturer.Contains(search)
                    || p.Ean.Contains(search)
                    || p.HsCode.Contains(search)
                    || p.HsCodeTax.Contains(search)
                    || p.Location.Contains(search)
                    || p.SizeUnitMeasureCode.Contains(search)
                    || p.ProductAttribute.Contains(search)
                 );
            }


            return View(data.Include(a => a.VProduct));
        }

        // GET: Product/Add
        [Route("product-management/product-add")]
        public IActionResult Add()
        {

            LoadViewData();
            var data = new Product();
            return View(data);
        }


        // POST: Product/Add
        [HttpPost, ValidateAntiForgeryToken]
        [Route("product-management/product-add")]
        public async Task<IActionResult> Add([Bind("ProductId,Name,ProductNumber,Manufacturer,Ean,HsCode,HsCodeTax,Location,MakeFlag,VariableFlag,FinishedGoodsFlag,SafetyStockLevel,MaximumStocklevel,ReorderPoint,StandardCost,ListPrice,SizeUnitMeasureCode,Weight,WeightTotal,Height,Width,Length,DaysToManufacture,ProductAttribute,ProductSourceId,ClassId,CategoryId,TaxGroupId,TaxIva,TaxImport,TaxProduction,TaxSale,SellStartDate,SellEndDate,ModifiedDate,CreateDate,Active,BusinessEntityId,Deleted,Image,Product,InitialStock")] Product product, List<IFormFile> Image, decimal? InitialStock, bool continueAdd)
        {
            if (ModelState.IsValid)
            {
                if (Image.Any())
                {
                    foreach (var image in Image)
                    {
                        if (image.Length > 0)
                        {
                            using (var memoryStream = new MemoryStream())
                            {
                                image.CopyTo(memoryStream);
                                var newimage = new Image()
                                {
                                    BusinessEntityId = _BusinessId,
                                    Active = true,
                                    Comments = "",
                                    CreateDate = System.DateTime.Now.Date,
                                    Deleted = false,
                                    LargeImage = memoryStream.ToArray(),
                                    LargeImageFileName = image.FileName,
                                    ModifiedDate = System.DateTime.Now.Date
                                };
                                product.BusinessEntityId = _BusinessId;
                                var productimage = new ProductImage()
                                {
                                    BusinessEntityId = _BusinessId,
                                    CreateDate = System.DateTime.Now.Date,
                                    Image = newimage,
                                    IsPrimary = true,
                                    Product = product
                                };

                                await _productImageServices.AddAsync (productimage, false);
                            }
                        }
                    }
                    await _context.SaveChangesAsync();
                }
                else
                {
                    await _productServices.AddAsync(product);
                }

                if (InitialStock.HasValue && InitialStock.Value > 0)
                {
                    var location = _locationServices.SingleOrDefaultAsync(a => a.DefaultLocation == true).Result;
                    if (location != null)
                    {
                        var stockInicial = new ProductInventory()
                        {
                            Bin = 0,
                            BusinessEntityId = _BusinessId,
                            Description = "Inicial",
                            LocationId = location.WarehouseId,
                            ProductId = product.ProductId,
                            NumberDoc = product.ProductId.ToString(),
                            Quantity = InitialStock.Value,
                            Signal = 1
                        };
                       await _productInventoryServices.AddAsync(stockInicial);

                    }
                }
                ErrorMessage = $"Cadastro efetuado com sucesso";
                return continueAdd ? RedirectToAction(nameof(Add)) : RedirectToAction(nameof(List));
            }
            LoadViewData();
            return View(product);
        }

        // GET: Product/Edit/5
        [Route("product-management/product-edit/{id?}")]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return RedirectToAction("AccessDenied", "Account");
            }

            var product = await _productServices.SingleOrDefaultAsync(m => m.ProductId == id);

            if (product == null)
            {
                return RedirectToAction("AccessDenied", "Account");
            }
            LoadViewData();

            ViewData["Images"] = _productImageServices.Query(a => a.ProductId == id).Select(a => a.Image).ToList();



            return View(product);
        }

        // POST: Product/Edit/5
        [HttpPost, ValidateAntiForgeryToken]
        [Route("product-management/product-edit/{id?}")]
        public async Task<IActionResult> Edit(int id, [Bind("ProductId,Name,ProductNumber,Manufacturer,Ean,HsCode,HsCodeTax,Location,MakeFlag,VariableFlag,FinishedGoodsFlag,SafetyStockLevel,MaximumStocklevel,ReorderPoint,StandardCost,ListPrice,SizeUnitMeasureCode,Weight,WeightTotal,Height,Width,Length,DaysToManufacture,ProductAttribute,ProductSourceId,ClassId,CategoryId,TaxGroupId,TaxIva,TaxImport,TaxProduction,TaxSale,SellStartDate,SellEndDate,ModifiedDate,CreateDate,Active,BusinessEntityId,Deleted,Image,Product")] Product product, List<IFormFile> Image, bool continueAdd, bool addTrash)
        {
            if (id != product.ProductId)
            {
                return NotFound();
            }
            typeof(Product).GetProperty("Deleted").SetValue(product, addTrash);

            LoadViewData();
            if (ModelState.IsValid)
            {
                if (Image.Any())
                {
                    foreach (var image in Image)
                    {
                        if (image.Length > 0)
                        {
                            using (var memoryStream = new MemoryStream())
                            {
                                image.CopyTo(memoryStream);
                                var newimage = new Image()
                                {
                                    BusinessEntityId = _BusinessId,
                                    Active = true,
                                    Comments = "",
                                    CreateDate = System.DateTime.Now.Date,
                                    Deleted = false,
                                    LargeImage = memoryStream.ToArray(),
                                    LargeImageFileName = image.FileName,
                                    ModifiedDate = System.DateTime.Now.Date
                                };
                                //product.BusinessEntityId = _BusinessId;
                                var productimage = new ProductImage()
                                {
                                    BusinessEntityId = _BusinessId,
                                    CreateDate = System.DateTime.Now.Date,
                                    Image = newimage,
                                    IsPrimary = true,
                                    ProductId = id
                                };

                                await _productImageServices.AddAsync (productimage, false);
                            }
                        }
                    }

                }
                else
                {
                    await _productServices.UpdateAsync (product, false);

                }


                try
                {
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    throw;
                }
                return continueAdd ? RedirectToAction(nameof(Edit), new { id = product.ProductId }) : RedirectToAction(nameof(List));
            }
            return View(product);
        }


        #endregion methods
    }
}
