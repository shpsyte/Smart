using Core.Domain.Accounting;
using Core.Domain.Business;
using Core.Domain.Finance;
using Core.Domain.Finance.Views;
using Core.Domain.Identity;
using Core.Domain.PersonAndData;
using Core.Domain.Production;
using Core.Domain.Region;
using Core.Domain.Sale;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
namespace Data.Map
{



    #region Person
    public class BusinessEntityMap : IMapConfiguration<BusinessEntity>
    {
        public void Map(EntityTypeBuilder<BusinessEntity> entity)
        {
            entity.ToTable("BusinessEntity");
            entity.HasKey(e => e.BusinessEntityId);
            entity.OwnsOne(p => p.Email).Property(p => p.Email).HasColumnName("Email");
            entity.OwnsOne(p => p.Name).Property(p => p.Name).HasColumnName("Name");

        }
    }
    public class CategoryPersonMap : IMapConfiguration<CategoryPerson>
    {
        public void Map(EntityTypeBuilder<CategoryPerson> entity)
        {
            entity.ToTable("CategoryPerson");
            entity.HasKey(e => e.CategoryId);
        }
    }
    public class PersonMap : IMapConfiguration<Person>
    {
        public void Map(EntityTypeBuilder<Person> entity)
        {
            entity.ToTable("Person");
            entity.HasKey(p => p.PersonId);


            entity.HasOne(d => d.Category)
                .WithMany(p => p.Person)
                .HasForeignKey(d => d.CategoryId);
        }
    }
    public class AddressMap : IMapConfiguration<Address>
    {

        public void Map(EntityTypeBuilder<Address> entity)
        {
            entity.HasKey(e => e.AddressId);
            entity.ToTable("Address");


            entity.HasOne(d => d.City)
               .WithMany(p => p.Address)
               .HasForeignKey(d => d.CityId)
               .OnDelete(DeleteBehavior.Cascade);

            entity.HasOne(d => d.StateProvince)
              .WithMany(p => p.Address)
              .HasForeignKey(d => d.StateProvinceId)
              .OnDelete(DeleteBehavior.Cascade);

        }
    }
    public class PersonAddressMap : IMapConfiguration<PersonAddress>
    {
        public void Map(EntityTypeBuilder<PersonAddress> entity)
        {
            entity.ToTable("PersonAddress");
            entity.HasOne(d => d.Address)
                .WithMany(p => p.PersonAddress)
                .HasForeignKey(d => d.AddressId)
                .OnDelete(DeleteBehavior.ClientSetNull);

            entity.HasOne(d => d.Person)
                .WithMany(p => p.PersonAddress)
                .HasForeignKey(d => d.PersonId)
                .OnDelete(DeleteBehavior.ClientSetNull);
        }
    }
    public class EmailMap : IMapConfiguration<Email>
    {
        public void Map(EntityTypeBuilder<Email> entity)
        {
            entity.ToTable("Email");
            entity.HasKey(e => e.EmailId);

        }
    }

    public class PhoneMap : IMapConfiguration<Phone>
    {
        public void Map(EntityTypeBuilder<Phone> entity)
        {
            entity.ToTable("Phone");
            entity.HasKey(e => e.PhoneId);

        }
    }


    public class PersonEmailMap : IMapConfiguration<PersonEmail>
    {
        public void Map(EntityTypeBuilder<PersonEmail> entity)
        {
            entity.ToTable("PersonEmail");
            entity.HasKey(e => e.PersonEmailId);

            entity.HasOne(d => d.Email)
                .WithMany(p => p.PersonEmail)
                .HasForeignKey(d => d.EmailId)
                .OnDelete(DeleteBehavior.Cascade);

            entity.HasOne(d => d.Person)
                .WithMany(p => p.PersonEmail)
                .HasForeignKey(d => d.PersonId)
                .OnDelete(DeleteBehavior.Cascade);

        }
    }
    public class PersonPhoneMap : IMapConfiguration<PersonPhone>
    {
        public void Map(EntityTypeBuilder<PersonPhone> entity)
        {
            entity.ToTable("PersonPhone");
            entity.HasKey(e => e.PersonPhoneId);

            entity.HasOne(d => d.Person)
                .WithMany(p => p.PersonPhone)
                .HasForeignKey(d => d.PersonId)
                .OnDelete(DeleteBehavior.Cascade);

            entity.HasOne(d => d.Phone)
                .WithMany(p => p.PersonPhone)
                .HasForeignKey(d => d.PhoneId)
                .OnDelete(DeleteBehavior.Cascade);

        }
    }




    #endregion
    #region Tax
    public class TaxGroupMap : IMapConfiguration<TaxGroup>
    {
        public void Map(EntityTypeBuilder<TaxGroup> entity)
        {
            entity.ToTable("TaxGroup");
            entity.HasKey(e => e.TaxGroupId);
        }
    }
    public class TaxOperationMap : IMapConfiguration<TaxOperation>
    {
        public void Map(EntityTypeBuilder<TaxOperation> entity)
        {
            entity.ToTable("TaxOperation");
            entity.HasKey(P => P.TaxOperationId);
        }
    }

    public class TaxMap : IMapConfiguration<Tax>
    {
        public void Map(EntityTypeBuilder<Tax> entity)
        {
            entity.ToTable("Tax");
            entity.HasKey(p => p.TaxId);

        }
    }

    public class UserSettingMap : IMapConfiguration<UserSetting>
    {
        public void Map(EntityTypeBuilder<UserSetting> entity)
        {
            entity.ToTable("UserSetting");
            entity.HasKey(e => e.UserSettingId);

        }
    }

    #endregion
    #region Location
    public class CountryMap : IMapConfiguration<Country>
    {
        public void Map(EntityTypeBuilder<Country> entity)
        {
            entity.ToTable("Country");
            entity.HasKey(e => e.CountryId);
        }
    }
    public class StateProvinceMap : IMapConfiguration<StateProvince>
    {
        public void Map(EntityTypeBuilder<StateProvince> entity)
        {
            entity.ToTable("StateProvince");
            entity.HasKey(e => e.StateProvinceId);

            entity.HasOne(d => d.Country)
                .WithMany(p => p.StateProvince)
                .HasForeignKey(d => d.CountryID)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
    public class CityMap : IMapConfiguration<City>
    {
        public void Map(EntityTypeBuilder<City> entity)
        {
            entity.ToTable("City");
            entity.HasKey(e => e.CityId);

            entity.HasOne(d => d.StateProvince)
                .WithMany(p => p.City)
                .HasForeignKey(d => d.StateProvinceId);
        }
    }

    #endregion
    #region production

    public class ProductInventoryMap : IMapConfiguration<ProductInventory>
    {
        public void Map(EntityTypeBuilder<ProductInventory> entity)
        {
            entity.ToTable("ProductInventory");
            entity.HasKey(e => e.Id);

            entity.HasOne(d => d.Location)
                .WithMany(p => p.ProductInventory)
                .HasForeignKey(d => d.LocationId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_ProductInventory_Location");

            entity.HasOne(d => d.Product)
                .WithMany(p => p.ProductInventory)
                .HasForeignKey(d => d.ProductId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_ProductInventory_Product");

        }
    }
    public class CategoryProductMap : IMapConfiguration<CategoryProduct>
    {
        public void Map(EntityTypeBuilder<CategoryProduct> entity)
        {
            entity.ToTable("CategoryProduct");
            entity.HasKey(e => e.CategoryId);
        }
    }
    public class ClassProductMap : IMapConfiguration<ClassProduct>
    {
        public void Map(EntityTypeBuilder<ClassProduct> entity)
        {
            entity.ToTable("ProductClass");
            entity.HasKey(e => e.ClassId);
        }
    }
    public class Warehousemap : IMapConfiguration<Location>
    {
        public void Map(EntityTypeBuilder<Location> entity)
        {
            entity.HasKey(a => a.WarehouseId);
            entity.ToTable("Location");
        }
    }

    public class ProductMap : IMapConfiguration<Product>
    {
        public void Map(EntityTypeBuilder<Product> entity)
        {
            entity.ToTable("Product");
            entity.HasKey(p => p.ProductId);

            entity.HasOne(d => d.Category)
                .WithMany(p => p.Product)
                .HasForeignKey(d => d.CategoryId)
                .OnDelete(DeleteBehavior.SetNull);

            entity.HasOne(d => d.Class)
                .WithMany(p => p.Product)
                .HasForeignKey(d => d.ClassId)
                .OnDelete(DeleteBehavior.SetNull);


            entity.HasOne(d => d.TaxGroup)
                .WithMany(p => p.Product)
                .HasForeignKey(d => d.TaxGroupId)
                .OnDelete(DeleteBehavior.SetNull);
        }
    }

    public class VProductMap : IMapConfiguration<VProduct>
    {
        public void Map(EntityTypeBuilder<VProduct> entity)
        {
            entity.ToTable("WiewInventoryProduct");
            entity.HasKey(e => new { e.ProductId });
            entity.HasOne(d => d.Product)
                 .WithOne(p => p.VProduct)
                 .HasForeignKey<VProduct>(b => b.ProductId);

        }
    }
    public class ImageMap : IMapConfiguration<Image>
    {
        public void Map(EntityTypeBuilder<Image> entity)
        {
            entity.ToTable("Image");
            entity.HasKey(p => p.ImageId);
        }
    }
    public class ProductImageMap : IMapConfiguration<ProductImage>
    {
        public void Map(EntityTypeBuilder<ProductImage> entity)
        {
            entity.ToTable("ProductImage");
            entity.HasKey(p => p.Id);

            entity.HasOne(d => d.Image)
                .WithMany(p => p.ProductImage)
                .HasForeignKey(d => d.ImageId)
                .OnDelete(DeleteBehavior.Cascade);

            entity.HasOne(d => d.Product)
                .WithMany(p => p.ProductImage)
                .HasForeignKey(d => d.ProductId)
                .OnDelete(DeleteBehavior.Cascade);

        }
    }


    public class HsCodeMap : IMapConfiguration<HsCode>
    {
        public void Map(EntityTypeBuilder<HsCode> entity)
        {
            entity.ToTable("HsCode");
            entity.HasKey(p => p.Code);

        }
    }

    #endregion
    #region Sales

    public class ConditionMap : IMapConfiguration<Condition>
    {
        public void Map(EntityTypeBuilder<Condition> entity)
        {
            entity.ToTable("PaymentCondition");
            entity.HasKey(e => e.ConditionId);
        }
    }

    #endregion
    #region Financial

    public class CategoryFinancialMap : IMapConfiguration<CategoryFinancial>
    {
        public void Map(EntityTypeBuilder<CategoryFinancial> entity)
        {
            entity.ToTable("CategoryFinancial");
            entity.HasKey(e => e.ChartAccountId);
        }
    }
    public class CostCenterMap : IMapConfiguration<CostCenter>
    {
        public void Map(EntityTypeBuilder<CostCenter> entity)
        {
            entity.ToTable("CostCenter");
            entity.HasKey(e => e.CostCenterId);
        }
    }
    public class AccountBankMap : IMapConfiguration<AccountBank>
    {
        public void Map(EntityTypeBuilder<AccountBank> entity)
        {
            entity.ToTable("AccountBank");
            entity.HasKey(e => e.AccountBankId);


        }
    }

    public class ExpenseMap : IMapConfiguration<Expense>
    {
        public void Map(EntityTypeBuilder<Expense> entity)
        {
            entity.ToTable("Expense");
            entity.HasKey(p => p.ExpenseId);

            entity.HasOne(d => d.CategoryFinancial)
                .WithMany(p => p.Expense)
                .HasForeignKey(d => d.CategoryId)
                .OnDelete(DeleteBehavior.SetNull);

            entity.HasOne(d => d.CostCenter)
                .WithMany(p => p.Expense)
                .HasForeignKey(d => d.CostCenterId)
                .OnDelete(DeleteBehavior.SetNull);

            entity.HasOne(d => d.Person)
                .WithMany(p => p.Expense)
                .HasForeignKey(d => d.PersonId)
                .OnDelete(DeleteBehavior.SetNull);
        }
    }
    public class ExpenseTransMap : IMapConfiguration<ExpenseTrans>
    {
        public void Map(EntityTypeBuilder<ExpenseTrans> entity)
        {
            entity.ToTable("ExpenseTrans");
            entity.HasKey(e => new { e.ExpenseTransId });

            entity.HasOne(d => d.Expense)
                  .WithMany(p => p.ExpenseTrans)
                  .HasForeignKey(d => d.ExpenseId);

            entity.HasOne(d => d.Bank)
                  .WithMany(p => p.ExpenseTrans)
                  .HasForeignKey(d => d.BankId);


            entity.HasOne(d => d.Condition)
                 .WithMany(p => p.ExpenseTrans)
                 .HasForeignKey(d => d.BankId);
        }
    }
    public class RevenueMap : IMapConfiguration<Revenue>
    {
        public void Map(EntityTypeBuilder<Revenue> entity)
        {
            entity.ToTable("Revenue");
            entity.HasKey(e => new { e.RevenueId }); 

            entity.HasOne(d => d.CategoryFinancial)
                .WithMany(p => p.Revenue)
                .HasForeignKey(d => d.CategoryId)
                .OnDelete(DeleteBehavior.SetNull);
            entity.HasOne(d => d.CostCenter)
                .WithMany(p => p.Revenue)
                .HasForeignKey(d => d.CostCenterId)
                .OnDelete(DeleteBehavior.SetNull);

            entity.HasOne(d => d.PaymentCondition)
                .WithMany(p => p.Revenue)
                .HasForeignKey(d => d.ConditionId)
                .OnDelete(DeleteBehavior.SetNull);

            entity.HasOne(d => d.Person)
                .WithMany(p => p.Revenue)
                .HasForeignKey(d => d.PersonId)
                .OnDelete(DeleteBehavior.SetNull);
        }
    }

    public class RevenueTransMap : IMapConfiguration<RevenueTrans>
    {
        public void Map(EntityTypeBuilder<RevenueTrans> entity)
        {
            entity.ToTable("RevenueTrans");
            entity.HasKey(e => new { e.RevenueTransId });

             

            entity.HasOne(d => d.Bank)
                  .WithMany(p => p.RevenueTrans)
                  .HasForeignKey(d => d.BankId)
                  .OnDelete(DeleteBehavior.SetNull);


            entity.HasOne(d => d.Condition)
                 .WithMany(p => p.RevenueTrans)
                  .OnDelete(DeleteBehavior.SetNull);



        }
    }
    public class BankTransMap : IMapConfiguration<BankTrans>
    {
        public void Map(EntityTypeBuilder<BankTrans> entity)
        {
            entity.ToTable("BankTrans");
            entity.HasKey(e => e.BankTransId);

            entity.HasOne(d => d.Bank)
                            .WithMany(p => p.BankTrans)
                            .HasForeignKey(d => d.BankId)
                            .OnDelete(DeleteBehavior.Cascade);

            entity.HasOne(d => d.ExpenseTrans)
                          .WithMany(p => p.BankTrans)
                          .HasForeignKey(d => d.ExpenseTransId)
                          .OnDelete(DeleteBehavior.ClientSetNull);

            entity.HasOne(d => d.RevenueTrans)
                          .WithMany(p => p.BankTrans)
                          .HasForeignKey(d => d.RevenueTransId)
                          .OnDelete(DeleteBehavior.ClientSetNull);

            entity.HasOne(d => d.CategoryFinancial)
                         .WithMany(p => p.BankTrans)
                         .HasForeignKey(d => d.CategoryId)
                         .OnDelete(DeleteBehavior.ClientSetNull);
        }
    }
    public class TempFinancialSplitMap : IMapConfiguration<TempFinancialSplit>
    {
        public void Map(EntityTypeBuilder<TempFinancialSplit> entity)
        {
            entity.ToTable("TempFinancialSplit");
            entity.HasKey(e => new { e.Id });
        }
    }

    public class VCashFlowMap : IMapConfiguration<VCashFlow>
    {
        public void Map(EntityTypeBuilder<VCashFlow> entity)
        {
            entity.HasKey(e => new { e.Id });
            entity.ToTable("WiewCashFlow");

            entity.HasOne(d => d.Person)
               .WithMany(p => p.VCashFlow)
               .HasForeignKey(d => d.PersonId);

        }
    }

    public class VExpenseMap : IMapConfiguration<VExpense>
    {
        public void Map(EntityTypeBuilder<VExpense> entity)
        {
            entity.ToTable("WiewExpense");
            entity.HasKey(e => new { e.ExpenseId });

            entity.HasOne(d => d.Person)
               .WithMany(p => p.VExpense)
               .HasForeignKey(d => d.PersonId);

            entity.HasOne(d => d.Expense)
                .WithOne(p => p.VExpense)
                .HasForeignKey<VExpense>(b => b.ExpenseId);

        }
    }

    public class VExpenseTransMap : IMapConfiguration<VExpenseTrans>
    {
        public void Map(EntityTypeBuilder<VExpenseTrans> entity)
        {
            entity.ToTable("WiewExpenseTrans");
            entity.HasKey(e => new { e.ExpenseTransId });

            entity.HasOne(d => d.VExpense)
               .WithMany(p => p.VExpenseTrans)
               .HasForeignKey(d => d.ExpenseId);

            entity.HasOne(d => d.Person)
              .WithMany(p => p.VExpenseTrans)
              .HasForeignKey(d => d.PersonId);

        }
    }

     
    public class VRevenueMap : IMapConfiguration<VRevenue>
    {
        public void Map(EntityTypeBuilder<VRevenue> entity)
        {

            entity.ToTable("WiewRevenue");
            entity.HasKey(e => new { e.RevenueId });

            entity.HasOne(d => d.Person)
                   .WithMany(p => p.VRevenue)
                   .HasForeignKey(d => d.PersonId);
            entity.HasOne(d => d.Revenue)
            .WithOne(p => p.VRevenue)
            .HasForeignKey<VRevenue>(b => b.RevenueId);


        }
    }


    public class VRevenueTransMap : IMapConfiguration<VRevenueTrans>
    {
        public void Map(EntityTypeBuilder<VRevenueTrans> entity)
        {
            entity.ToTable("ViewRevenueTrans");
            entity.HasKey(e => new { e.RevenueTransId });
            

            entity.HasOne(d => d.VRevenue)
               .WithMany(p => p.VRevenueTrans)
               .HasForeignKey(d => d.RevenueId);

            entity.HasOne(d => d.Person)
              .WithMany(p => p.VRevenueTrans)
              .HasForeignKey(d => d.PersonId);

        }
    }



    #endregion



    public class InvoiceMap : IMapConfiguration<Invoice>
    {
        public void Map(EntityTypeBuilder<Invoice> entity)
        {
            entity.ToTable("Invoice", "Sales");
            //entity.Property(e => e.InvoiceId).ValueGeneratedNever();
            entity.Property(e => e.AccountNumber)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.BillToAddressId).HasColumnName("BillToAddressID");
            entity.Property(e => e.BusinessEntityId).HasColumnName("BusinessEntityID");
            entity.Property(e => e.CarrierTrackingNumber).HasMaxLength(25);
            entity.Property(e => e.CarrierTruckId)
                .HasMaxLength(10)
                .IsUnicode(false);
            entity.Property(e => e.Comment).IsUnicode(false);
            entity.Property(e => e.CustomerId).HasColumnName("CustomerID");
            entity.Property(e => e.DueDate).HasColumnType("date");
            entity.Property(e => e.Freight)
                .HasColumnType("numeric(12, 4)")
                .HasDefaultValueSql("((0.00))");
            entity.Property(e => e.InvoiceDate)
                .HasColumnType("datetime")
                .HasDefaultValueSql("(getdate())");
          
            entity.Property(e => e.InvoiceNumber)
                .IsRequired()
                .HasMaxLength(25)
                .HasComputedColumnSql("(isnull(N'SO'+CONVERT([nvarchar](23),[InvoiceID]),N'*** ERROR ***'))");
            entity.Property(e => e.ModifiedDate)
                .HasColumnType("datetime")
                .HasDefaultValueSql("(getdate())");
            entity.Property(e => e.OnlineInvoiceFlag).HasDefaultValueSql("((1))");
            entity.Property(e => e.Package)
                .HasMaxLength(20)
                .IsUnicode(false);
            entity.Property(e => e.PurchaseOrderNumber)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.RevisionNumber).HasDefaultValueSql("((0))");
            entity.Property(e => e.SalesPersonId).HasColumnName("SalesPersonID");
            entity.Property(e => e.ShipDate).HasColumnType("date");
            entity.Property(e => e.ShipToAddressId).HasColumnName("ShipToAddressID");
            entity.Property(e => e.Status).HasDefaultValueSql("((1))");
            entity.Property(e => e.SubTotal)
                .HasColumnType("numeric(12, 4)")
                .HasDefaultValueSql("((0.00))");
            entity.Property(e => e.TaxAmt)
                .HasColumnType("numeric(12, 4)")
                .HasDefaultValueSql("((0.00))");

            entity.Property(e => e.TotalDue)
                .HasColumnType("numeric(14, 4)")
                .HasComputedColumnSql("(isnull(([SubTotal]+[TaxAmt])+[Freight],(0)))");

            entity.Property(e => e.TotalWeight).HasColumnType("numeric(9, 2)");
            entity.Property(e => e.Weight).HasColumnType("numeric(9, 2)");
            entity.HasOne(d => d.BillToAddress)
                .WithMany(p => p.InvoiceBillToAddress)
                .HasForeignKey(d => d.BillToAddressId)
                .HasConstraintName("FK_Order_Address");

            entity.HasOne(d => d.Carrier)
                .WithMany(p => p.InvoiceCarrier)
                .HasForeignKey(d => d.CarrierId)
                .HasConstraintName("FK_Order_Person2");
            entity.HasOne(d => d.Customer)
                .WithMany(p => p.InvoiceCustomer)
                .HasForeignKey(d => d.CustomerId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Order_Person");
            entity.HasOne(d => d.SalesPerson)
                .WithMany(p => p.InvoiceSalesPerson)
                .HasForeignKey(d => d.SalesPersonId)
                .HasConstraintName("FK_Order_Person1");
            entity.HasOne(d => d.ShipToAddress)
                .WithMany(p => p.InvoiceShipToAddress)
                .HasForeignKey(d => d.ShipToAddressId)
                .HasConstraintName("FK_Order_Address1");
            entity.HasOne(d => d.Location)
                .WithMany(p => p.Invoice)
                .HasForeignKey(d => d.WarehouseId)
                .HasConstraintName("FK_Order_Warehouse");
            entity.HasOne(d => d.TaxOperation)
             .WithMany(p => p.Invoice)
             .HasForeignKey(d => d.TaxOperationId);
        }
    }
    public class InvoiceDetailMap : IMapConfiguration<InvoiceDetail>
    {
        public void Map(EntityTypeBuilder<InvoiceDetail> entity)
        {
            entity.HasKey(e => new { e.InvoiceId, e.InvoiceDetailId });
            entity.ToTable("InvoiceDetail", "Sales");
            entity.Property(e => e.InvoiceDetailId).ValueGeneratedOnAdd();
            entity.Property(e => e.BusinessEntityId).HasColumnName("BusinessEntityID");
            entity.Property(e => e.CarrierTrackingNumber).HasMaxLength(25);
            entity.Property(e => e.LineTotal)
                .HasColumnType("numeric(26, 10)")
                .HasComputedColumnSql("(isnull(([UnitPrice]*((1.0)-[UnitPriceDiscount]))*[OrderQty],(0.0)))");
            entity.Property(e => e.ModifiedDate)
                .HasColumnType("datetime")
                .HasDefaultValueSql("(getdate())");
            entity.Property(e => e.OrderQty).HasColumnType("numeric(5, 3)");
            entity.Property(e => e.ProductId).HasColumnName("ProductID");

            entity.Property(e => e.ProductNumber).HasColumnName("ProductNumber").HasColumnType("varchar(100)").IsUnicode(false);
            entity.Property(e => e.CodOper).HasColumnName("CodOper").HasColumnType("varchar(50)").IsUnicode(false);
            entity.Property(e => e.TaxOperationId).HasColumnName("TaxOperationId");
            entity.Property(e => e.TaxProduction).HasColumnName("TaxProduction");
            entity.Property(e => e.TaxSales).HasColumnName("TaxSales");
            entity.Property(e => e.StandartCost).HasColumnName("StandartCost");



            entity.Property(e => e.UnitPrice).HasColumnType("numeric(12, 4)");
            entity.Property(e => e.UnitPriceDiscount)
                .HasColumnType("numeric(6, 3)")
                .HasDefaultValueSql("((0.0))");

            entity.HasOne(d => d.Invoice)
                .WithMany(p => p.InvoiceDetail)
                .HasForeignKey(d => d.InvoiceId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_OrderDetail_Order");
            entity.HasOne(d => d.Product)
                .WithMany(p => p.InvoiceDetail)
                .HasForeignKey(d => d.ProductId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_OrderDetail_Product");
            entity.HasOne(d => d.Warehouse)
                .WithMany(p => p.InvoiceDetail)
                .HasForeignKey(d => d.WarehouseId)
                .HasConstraintName("FK_OrderDetail_Warehouse");
        }
    }
  

}