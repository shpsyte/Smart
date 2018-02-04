using Core.Domain.Finance;
using Core.Domain.Finance.Views;
using Core.Domain.Production;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Smart.Models.Production
{
    public class StockReportsModel
    {
        public StockReportsModel()
        {
            this.Active = true;
            this.FinishedGoodsFlag = true;
            this.WithStock = false;
            this.TypeView = "_Product";
        }
        public IEnumerable<Product> Product { get; set; }
        public IEnumerable<VProduct> VProduct { get; set; }


        public int? ProductId { get; set; }
       
        public string Name { get; set; }
       
        public string ProductNumber { get; set; }


        public string Manufacturer { get; set; }



        public string Ean { get; set; }
        public string HsCode { get; set; }
        
        public string Location { get; set; }

        public string SizeUnitMeasureCode { get; set; }
        public string ProductAttribute { get; set; }


        public bool MakeFlag { get; set; }
        public bool VariableFlag { get; set; }

        public bool FinishedGoodsFlag { get; set; }


        public bool Active { get; set; }

        public bool WithStock { get; set; }

        public int? ProductSourceId { get; set; }
        public int? ClassId { get; set; }
        public int? CategoryId { get; set; }
        public int? LocationId { get; set; }

        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime? SellStartDate { get; set; }



        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime? SellEndDate { get; set; }


        public string TypeView { get; set; }
        

    }
}
