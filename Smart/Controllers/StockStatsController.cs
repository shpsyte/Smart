using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Core.Domain.Production;
using Data.Context;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Services.Interfaces;
using Smart.Models.Production;
using Smart.Services;

namespace Smart.Controllers
{
    public class StockStatsController : BaseController
    {
        private readonly IServices<Product> _productServices;
        private readonly IServices<CategoryProduct> _categoryProductServices;
        private readonly IServices<ClassProduct> _classProductServices;
        private readonly IServices<ProductImage> _productImageServices;
        private readonly IServices<Location> _locationServices;
        private readonly IServices<Image> _imageServices;

        public StockStatsController(

                                IServices<CategoryProduct> categoryProductServices,
                                IServices<ClassProduct> classProductServices,
                                IServices<ProductImage> productImageServices,
                                IServices<Product> productServices,
                           IServices<Image> imageServices,
        IServices<Location> locationServices, IUser currentUser, IEmailSender emailSender, IHttpContextAccessor accessor, SmartContext context) : base(currentUser, emailSender, accessor, context)
        {
            this._productServices = productServices;
            this._categoryProductServices = categoryProductServices;
            this._classProductServices = classProductServices;
            this._productServices = productServices;
            this._productImageServices = productImageServices;
            this._locationServices = locationServices;
            this._imageServices = imageServices;
        }

        private void LoadViewData()
        {
            ViewData["CategoryId"] = new SelectList(_categoryProductServices.GetAll(), "CategoryId", "Name");
            ViewData["ClassId"] = new SelectList(_classProductServices.GetAll(), "ClassId", "Name");
            ViewData["LocationId"] = new SelectList(_locationServices.GetAll(), "WarehouseId", "Name");
        }




        public async Task<IQueryable<Product>> GetProducts(StockReportsModel filter)
        {
            var data = await _productServices.QueryAsync();
            data = data.Include(a => a.VProduct);
            

            if (filter.ProductId.HasValue)
                data = data.Where(a => a.ProductId == filter.ProductId.Value);

            if (!string.IsNullOrEmpty(filter.Name))
                data = data.Where(a => a.Name.Contains(filter.Name));

            if (!string.IsNullOrEmpty(filter.ProductNumber))
                data = data.Where(a => a.Name.Contains(filter.ProductNumber));

            if (!string.IsNullOrEmpty(filter.Manufacturer))
                data = data.Where(a => a.Name.Contains(filter.Manufacturer));

            if (!string.IsNullOrEmpty(filter.Ean))
                data = data.Where(a => a.Name.Contains(filter.Ean));

            if (!string.IsNullOrEmpty(filter.HsCode))
                data = data.Where(a => a.Name.Contains(filter.HsCode));

            if (!string.IsNullOrEmpty(filter.Location))
                data = data.Where(a => a.Name.Contains(filter.Location));



            if (!string.IsNullOrEmpty(filter.SizeUnitMeasureCode))
                data = data.Where(a => a.Name.Contains(filter.SizeUnitMeasureCode));

            if (!string.IsNullOrEmpty(filter.ProductAttribute))
                data = data.Where(a => a.Name.Contains(filter.ProductAttribute));

            if (filter.ProductSourceId.HasValue)
                data = data.Where(a => a.ProductSourceId == filter.ProductSourceId);

            if (filter.LocationId.HasValue)
                data = data.Where(a => a.ProductInventory.Any(i => i.LocationId == filter.LocationId));

            if (filter.ClassId.HasValue)
                data = data.Where(a => a.ClassId == filter.ClassId);

            if (filter.CategoryId.HasValue)
                data = data.Where(a => a.ClassId == filter.CategoryId);

            if (filter.CategoryId.HasValue)
                data = data.Where(a => a.ClassId == filter.CategoryId);

            if (filter.SellStartDate.HasValue)
                data = data.Where(a => a.SellStartDate >= filter.SellStartDate);

            if (filter.SellEndDate.HasValue)
                data = data.Where(a => a.SellEndDate <= filter.SellEndDate);

            if (filter.SellEndDate.HasValue)
                data = data.Where(a => a.SellEndDate <= filter.SellEndDate);

            data = data.Where(a => a.MakeFlag == filter.MakeFlag);
            data = data.Where(a => a.VariableFlag == filter.VariableFlag);
            data = data.Where(a => a.FinishedGoodsFlag == filter.FinishedGoodsFlag);

            data = data.Where(a => a.Active == filter.Active);



            if (filter.WithStock)
            {
                data = data.Where(a => a.VProduct.Stock.Value > 0);
            }

            
           if (filter.TypeView.Equals("_ProductWithutSaldo", StringComparison.CurrentCultureIgnoreCase))
            {
                data = data.Where(a => a.VProduct.Stock.Value == 0);

            }

            if (filter.TypeView.Equals("_ProductMinimumStocklevel", StringComparison.CurrentCultureIgnoreCase))
            {
                data = data.Where(a =>
                   a.SafetyStockLevel.Value > 0
                && ((a.SafetyStockLevel.Value / a.VProduct.Stock.Value) * 100 >= 80));

            }



            return data;
        }

        [Route("stock-reports/stats-management")]
        public IActionResult Reports() => View();



        [Route("stock-reports/products")]
        [HttpGet]
        public IActionResult Product(StockReportsModel filter)
        {
            LoadViewData();
            return View(filter);
        }

        [Route("stock-reports/products-stock")]
        [HttpGet]
        public IActionResult ProductStock(StockReportsModel filter)
        {
            LoadViewData();
            filter.TypeView = "_ProductStock";
            return View(filter);
        }


        [Route("stock-reports/products-less-stock")]
        [HttpGet]
        public IActionResult ProductWithutSaldo(StockReportsModel filter)
        {
            LoadViewData();
            filter.TypeView = "_ProductWithutSaldo";
            return View(filter);
        }

        [Route("stock-reports/products-value-stock")]
        [HttpGet]
        public IActionResult ValueStockLocation(StockReportsModel filter)
        {
            LoadViewData();
            filter.TypeView = "_ValueStockLocation";
            filter.WithStock = true;
            return View(filter);
        }

        [Route("stock-reports/products-min-vs-stock")]
        [HttpGet]
        public IActionResult ProductMinimumStocklevel(StockReportsModel filter)
        {
            LoadViewData();
            filter.TypeView = "_ProductMinimumStocklevel";
            return View(filter);
        }






        [Route("stock-reports/products")]
        [HttpPost]
        public async Task<IActionResult> Product(StockReportsModel filter, string trash)
        {
            LoadViewData();
            IQueryable<Product> data = await GetProducts(filter);
            filter.Product = data;
            return View(filter);
        }







    }
}