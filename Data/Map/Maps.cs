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

    
    public class BusinessEntityMap : IMapConfiguration<BusinessEntity>
    {
        public void Map(EntityTypeBuilder<BusinessEntity> entity)
        {
            entity.ToTable("BusinessEntity", Shema.shemaBusiness);
            entity.HasKey(e => e.BusinessEntityId);
            entity.OwnsOne(p => p.Email).Property(p => p.Email).HasColumnName("Email");
            entity.OwnsOne(p => p.Name).Property(p => p.Name).HasColumnName("Name");
            
        }
    }
    public class CategoryPersonMap : IMapConfiguration<CategoryPerson>
    {
        public void Map(EntityTypeBuilder<CategoryPerson> entity)
        {
            entity.ToTable("CategoryPerson", Shema.shemaPerson);
            entity.HasKey(e => e.CategoryId);
        }
    }
    public class TaxGroupMap : IMapConfiguration<TaxGroup>
    {
        public void Map(EntityTypeBuilder<TaxGroup> entity)
        {
            entity.ToTable("TaxGroup", Shema.shemaAccounting);
            entity.HasKey(e => e.TaxGroupId);
        }
    }
    public class TaxOperationMap : IMapConfiguration<TaxOperation>
    {
        public void Map(EntityTypeBuilder<TaxOperation> entity)
        {
            entity.ToTable("TaxOperation", Shema.shemaAccounting);
            entity.HasKey(P => P.TaxOperationId);
        }
    }
    public class CategoryFinancialMap : IMapConfiguration<CategoryFinancial>
    {
        public void Map(EntityTypeBuilder<CategoryFinancial> entity)
        {
            entity.ToTable("CategoryFinancial", Shema.shemaFinancial);
            entity.HasKey(e => e.ChartAccountId);
        }
    }
    public class CostCenterMap : IMapConfiguration<CostCenter>
    {
        public void Map(EntityTypeBuilder<CostCenter> entity)
        {
            entity.ToTable("CostCenter", Shema.shemaFinancial);
            entity.HasKey(e => e.CostCenterId);
        }
    }
    public class BankMap : IMapConfiguration<Bank>
    {
        public void Map(EntityTypeBuilder<Bank> entity)
        {
            entity.ToTable("AccountBank", Shema.shemaFinancial);
            entity.HasKey(e => e.AccountBankId);
            

        }
    }
    public class CountryMap : IMapConfiguration<Country>
    {
        public void Map(EntityTypeBuilder<Country> entity)
        {
            entity.ToTable("Country", Shema.shemaPerson);
            entity.HasKey(e => e.CountryId);
        }
    }
    public class StateProvinceMap : IMapConfiguration<StateProvince>
    {
        public void Map(EntityTypeBuilder<StateProvince> entity)
        {
            entity.ToTable("StateProvince", Shema.shemaPerson);
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
            entity.ToTable("City", Shema.shemaPerson);
            entity.HasKey(e => e.CityId);

            entity.HasOne(d => d.StateProvince)
                .WithMany(p => p.City)
                .HasForeignKey(d => d.StateProvinceId);
        }
    }
    public class CategoryProductMap : IMapConfiguration<CategoryProduct>
    {
        public void Map(EntityTypeBuilder<CategoryProduct> entity)
        {
            entity.ToTable("CategoryProduct", Shema.shemaProduction);
            entity.HasKey(e => e.CategoryId);
        }
    }
    public class ClassProductMap : IMapConfiguration<ClassProduct>
    {
        public void Map(EntityTypeBuilder<ClassProduct> entity)
        {
            entity.ToTable("ProductClass", Shema.shemaProduction);
            entity.HasKey(e => e.ClassId);
        }
    }
    public class Warehousemap : IMapConfiguration<Location>
    {
        public void Map(EntityTypeBuilder<Location> entity)
        {
            entity.HasKey(a => a.WarehouseId);
            entity.ToTable("Location", Shema.shemaProduction);
        }
    }
    public class ConditionMap : IMapConfiguration<Condition>
    {
        public void Map(EntityTypeBuilder<Condition> entity)
        {
            entity.ToTable("PaymentCondition", Shema.shemaSales);
            entity.HasKey(e => e.ConditionId);
        }
    }
    public class PersonMap : IMapConfiguration<Person>
    {
        public void Map(EntityTypeBuilder<Person> entity)
        {
            entity.ToTable("Person", Shema.shemaPerson);
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
            entity.ToTable("Address", Shema.shemaPerson);


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
            entity.ToTable("PersonAddress", Shema.shemaPerson);
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











   
        public class VRevenueMap : IMapConfiguration<VRevenue>
        {
            public void Map(EntityTypeBuilder<VRevenue> entity)
            {

                entity.ToTable("VRevenue", "Financial");
                entity.HasKey(e => new { e.RevenueId });
                // entity.Property(e => e.Id).ValueGeneratedNever();
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
                entity.ToTable("VRevenueTrans", "Financial");
                entity.HasKey(e => new { e.Id });
                // entity.Property(e => e.Id).ValueGeneratedNever();
                entity.HasOne(d => d.VRevenue)
                   .WithMany(p => p.VRevenueTrans)
                   .HasForeignKey(d => d.RevenueId);

                entity.HasOne(d => d.Person)
                  .WithMany(p => p.VRevenueTrans)
                  .HasForeignKey(d => d.PersonId);

            }
        }
        public class VExpenseTransMap : IMapConfiguration<VExpenseTrans>
        {
            public void Map(EntityTypeBuilder<VExpenseTrans> entity)
            {
                entity.ToTable("VExpenseTrans", "Financial");
                entity.HasKey(e => new { e.Id });
                // entity.Property(e => e.Id).ValueGeneratedNever();
                entity.HasOne(d => d.VExpense)
                   .WithMany(p => p.VExpenseTrans)
                   .HasForeignKey(d => d.ExpenseId);

                entity.HasOne(d => d.Person)
                  .WithMany(p => p.VExpenseTrans)
                  .HasForeignKey(d => d.PersonId);

            }
        }

        public class VExpenseMap : IMapConfiguration<VExpense>
        {
            public void Map(EntityTypeBuilder<VExpense> entity)
            {
                entity.HasKey(e => new { e.ExpenseId });
                entity.ToTable("VExpense", "Financial");
                // entity.Property(e => e.Id).ValueGeneratedNever();
                entity.HasOne(d => d.Person)
                   .WithMany(p => p.VExpense)
                   .HasForeignKey(d => d.PersonId);

                entity.HasOne(d => d.Expense)
                    .WithOne(p => p.VExpense)
                    .HasForeignKey<VExpense>(b => b.ExpenseId);

            }
        }


        public class VCashFlowMap : IMapConfiguration<VCashFlow>
        {
            public void Map(EntityTypeBuilder<VCashFlow> entity)
            {
                entity.HasKey(e => new { e.Id });
                entity.ToTable("VCashFlow", "Financial");
                // entity.Property(e => e.Id).ValueGeneratedNever();
                entity.HasOne(d => d.Person)
                   .WithMany(p => p.VCashFlow)
                   .HasForeignKey(d => d.PersonId);

            }
        }

        public class RevenueTransMap : IMapConfiguration<RevenueTrans>
        {
            public void Map(EntityTypeBuilder<RevenueTrans> entity)
            {
                entity.ToTable("RevenueTrans", "Financial");
                entity.HasKey(e => new { e.Id });

                entity.Property(e => e.Description).HasColumnType("varchar(150)").IsUnicode(false);
                entity.Property(e => e.Total).HasColumnType("numeric(12, 4)");
                entity.Property(e => e.Signal).IsRequired();
                entity.HasOne(d => d.Revenue)
                      .WithMany(p => p.RevenueTrans)
                      .HasForeignKey(d => d.RevenueId);



                entity.Property(e => e.CreateDate)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.HasOne(d => d.Bank)
                      .WithMany(p => p.RevenueTrans)
                      .HasForeignKey(d => d.BankId);


                entity.HasOne(d => d.Condition)
                     .WithMany(p => p.RevenueTrans)
                     .HasForeignKey(d => d.BankId);
            }
        }

        public class ExpenseTransMap : IMapConfiguration<ExpenseTrans>
        {
            public void Map(EntityTypeBuilder<ExpenseTrans> entity)
            {
                entity.ToTable("ExpenseTrans", "Financial");
                entity.HasKey(e => new { e.Id });
                entity.Property(e => e.Description).HasColumnType("varchar(150)").IsUnicode(false);
                entity.Property(e => e.Total).HasColumnType("numeric(12, 4)");
                entity.Property(e => e.Signal).IsRequired();

                entity.Property(e => e.CreateDate)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");


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



        public class BankTransMap : IMapConfiguration<BankTrans>
        {
            public void Map(EntityTypeBuilder<BankTrans> entity)
            {
                entity.ToTable("BankTrans", "Financial");
                entity.HasKey(e => e.Id);

                entity.Property(e => e.Description)
                                .HasMaxLength(150)
                                .IsUnicode(false);

                entity.Property(e => e.Total).HasColumnType("numeric(12, 4)");

                entity.HasOne(d => d.Bank)
                                .WithMany(p => p.BankTrans)
                                .HasForeignKey(d => d.BankId)
                                .OnDelete(DeleteBehavior.ClientSetNull)
                                .HasConstraintName("FK_BankTrans_Bank");

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



        public class RevenueMap : IMapConfiguration<Revenue>
        {
            public void Map(EntityTypeBuilder<Revenue> entity)
            {
                entity.ToTable("Revenue", "Financial");
                entity.HasKey(e => new { e.RevenueId });
                entity.Property(e => e.RevenueNumber)
                    .HasMaxLength(30)
                    .IsUnicode(false);
                entity.Property(e => e.RevenueSeq).IsRequired();
                entity.Property(e => e.RevenueTotalSeq).IsRequired();
                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(80)
                    .IsUnicode(false);
                entity.Property(e => e.CreateDate)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");
                entity.Property(e => e.DueDate).HasColumnType("date");
                entity.Property(e => e.DuePayment).HasColumnType("date");
                entity.Property(e => e.ModifiedDate)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");
                entity.Property(e => e.Comment)
                    .HasMaxLength(250)
                    .IsUnicode(false);
                entity.Property(e => e.Total).HasColumnType("numeric(12, 4)");
                entity.HasOne(d => d.CategoryFinancial)
                    .WithMany(p => p.Revenue)
                    .HasForeignKey(d => d.CategoryId)
                    .HasConstraintName("FK_Revenue_FinancialCategory");
                entity.HasOne(d => d.CostCenter)
                    .WithMany(p => p.Revenue)
                    .HasForeignKey(d => d.CostCenterId)
                    .HasConstraintName("FK_Revenue_FinancialCostCenter");
                entity.HasOne(d => d.PaymentCondition)
                    .WithMany(p => p.Revenue)
                    .HasForeignKey(d => d.PaymentConditionId)
                    .HasConstraintName("FK_Revenue_PaymentCondition");
                entity.HasOne(d => d.Person)
                    .WithMany(p => p.Revenue)
                    .HasForeignKey(d => d.PersonId)
                    .HasConstraintName("FK_Revenue_Person");
            }
        }








        public class TempFinancialSplitMap : IMapConfiguration<TempFinancialSplit>
        {
            public void Map(EntityTypeBuilder<TempFinancialSplit> entity)
            {
                entity.ToTable("TempFinancialSplit", "Financial");
                entity.HasKey(e => new { e.Id })
                  .HasName("PK_TempFinancialSplit");
                //entity.Property(e => e.Id). .ValueGeneratedNever();

            }
        }






    public class EmailMap : IMapConfiguration<Email>
    {
        public void Map(EntityTypeBuilder<Email> entity)
        {
            entity.ToTable("Email", "Person");
            entity.HasIndex(e => e.Email1)
                .HasName("IX_Email");
            entity.Property(e => e.EmailId).HasColumnName("EmailID");
            entity.Property(e => e.BusinessEntityId).HasColumnName("BusinessEntityID");
            entity.Property(e => e.Email1)
                .IsRequired()
                .HasColumnName("Email")
                .HasMaxLength(250)
                .IsUnicode(false);
            entity.Property(e => e.Type)
                .IsRequired()
                .HasDefaultValueSql("(3)");

        }
    }
    public class ExpenseMap : IMapConfiguration<Expense>
    {
        public void Map(EntityTypeBuilder<Expense> entity)
        {
            entity.ToTable("Expense", "Financial");
            entity.Property(e => e.BusinessEntityId).HasColumnName("BusinessEntityID");
            entity.Property(e => e.Comment)
                .HasMaxLength(250)
                .IsUnicode(false);
            entity.Property(e => e.CreateDate)
                .HasColumnType("datetime")
                .HasDefaultValueSql("(getdate())");
            entity.Property(e => e.DueDate).HasColumnType("date");
            entity.Property(e => e.DuePayment).HasColumnType("date");
            entity.Property(e => e.ExpenseNumber)
                .HasMaxLength(30)
                .IsUnicode(false);
            entity.Property(e => e.ModifiedDate)
                .HasColumnType("datetime")
                .HasDefaultValueSql("(getdate())");
            entity.Property(e => e.Name)
                .IsRequired()
                .HasMaxLength(80)
                .IsUnicode(false);
            entity.Property(e => e.Total).HasColumnType("numeric(12, 4)");

            entity.HasOne(d => d.CategoryFinancial)
                .WithMany(p => p.Expense)
                .HasForeignKey(d => d.CategoryId)
                .HasConstraintName("FK_Expense_ChartAccount");
            entity.HasOne(d => d.CostCenter)
                .WithMany(p => p.Expense)
                .HasForeignKey(d => d.CostCenterId)
                .HasConstraintName("FK_Expense_CostCenter");
            entity.HasOne(d => d.Person)
                .WithMany(p => p.Expense)
                .HasForeignKey(d => d.PersonId)
                .HasConstraintName("FK_Expense_Person");
        }
    }
    public class HsCodeMap : IMapConfiguration<HsCode>
    {
        public void Map(EntityTypeBuilder<HsCode> entity)
        {
            entity.ToTable("HsCode", "Production");
            entity.HasIndex(e => e.HsCode1)
                .HasName("IX_Ncm");
            entity.Property(e => e.CityTaxes)
                .HasColumnType("numeric(5, 2)")
                .HasDefaultValueSql("((0))");
            entity.Property(e => e.HsCode1)
                .IsRequired()
                .HasColumnName("HsCode")
                .HasColumnType("char(20)");
            entity.Property(e => e.ImportFederalTaxes)
                .HasColumnType("numeric(5, 2)")
                .HasDefaultValueSql("((0))");
            entity.Property(e => e.Name)
                .IsRequired()
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.NationalFederalTaxes)
                .HasColumnType("numeric(5, 2)")
                .HasDefaultValueSql("((0))");
            entity.Property(e => e.StateTaxes)
                .HasColumnType("numeric(5, 2)");

        }
    }
    public class ImageMap : IMapConfiguration<Image>
    {
        public void Map(EntityTypeBuilder<Image> entity)
        {
            entity.ToTable("Image", "Production");
            entity.Property(e => e.Active).HasDefaultValueSql("((1))");
            entity.Property(e => e.BusinessEntityId).HasColumnName("BusinessEntityID");
            entity.Property(e => e.Comments).IsUnicode(false);
            entity.Property(e => e.CreateDate)
                .HasColumnType("datetime")
                .HasDefaultValueSql("(getdate())");
            entity.Property(e => e.LargeImageFileName).HasMaxLength(50).IsUnicode(false);
            entity.Property(e => e.ModifiedDate)
                .HasColumnType("datetime")
                .HasDefaultValueSql("(getdate())");

        }
    }
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
   
    public class PersonEmailMap : IMapConfiguration<PersonEmail>
    {
        public void Map(EntityTypeBuilder<PersonEmail> entity)
        {
            entity.ToTable("PersonEmail", "Person");
            entity.HasIndex(e => new { e.PersonId, e.EmailId })
                .HasName("IX_PersonEmail");
            entity.HasOne(d => d.Email)
                .WithMany(p => p.PersonEmail)
                .HasForeignKey(d => d.EmailId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_PersonEmail_Email");
            entity.HasOne(d => d.Person)
                .WithMany(p => p.PersonEmail)
                .HasForeignKey(d => d.PersonId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_PersonEmail_Person");

        }
    }
    public class PersonPhoneMap : IMapConfiguration<PersonPhone>
    {
        public void Map(EntityTypeBuilder<PersonPhone> entity)
        {
            entity.ToTable("PersonPhone", "Person");
            entity.HasIndex(e => new { e.PersonId, e.PhoneId })
                .HasName("IX_PersonPhone");
            entity.HasOne(d => d.Person)
                .WithMany(p => p.PersonPhone)
                .HasForeignKey(d => d.PersonId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_PersonPhone_Person");
            entity.HasOne(d => d.Phone)
                .WithMany(p => p.PersonPhone)
                .HasForeignKey(d => d.PhoneId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_PersonPhone_Phone");

        }
    }
    public class PhoneMap : IMapConfiguration<Phone>
    {
        public void Map(EntityTypeBuilder<Phone> entity)
        {
            entity.ToTable("Phone", "Person");
            entity.Property(e => e.PhoneId).ValueGeneratedNever();
            entity.Property(e => e.BusinessEntityId).HasColumnName("BusinessEntityID");
            entity.Property(e => e.Phone1)
                .IsRequired()
                .HasColumnName("Phone")
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Type)
                .IsRequired()
                .HasColumnType("char(2)")
                .HasDefaultValueSql("('CN')");

        }
    }

    public class VProductMap : IMapConfiguration<VProduct>
    {
        public void Map(EntityTypeBuilder<VProduct> entity)
        {
            entity.ToTable("VProduct", "Production");
            entity.HasKey(e => new { e.ProductId });
            entity.HasOne(d => d.Product)
                 .WithOne(p => p.VProduct)
                 .HasForeignKey<VProduct>(b => b.ProductId);

        }
    }


    public class ProductInventoryMap : IMapConfiguration<ProductInventory>
    {
        public void Map(EntityTypeBuilder<ProductInventory> entity)
        {
            entity.ToTable("ProductInventory", "Production");
            entity.HasKey(e => e.Id);

            entity.Property(e => e.CreateDate)
                .HasColumnType("date")
                .HasDefaultValueSql("(getdate())");

            entity.Property(e => e.Description)
                .HasColumnType("varchar(150)")
                .HasMaxLength(150)
                .IsUnicode(false);

            entity.Property(e => e.NumberDoc)
             .HasColumnType("varchar(150)")
             .HasMaxLength(150)
             .IsUnicode(false);

            entity.Property(e => e.Shelf)
         .HasColumnType("varchar(50)")
         .HasMaxLength(50)
         .IsUnicode(false);

            entity.Property(e => e.Quantity)
                .HasColumnType("numeric(12, 4)")
                .HasDefaultValueSql("((0))");

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



    public class ProductMap : IMapConfiguration<Product>
    {
        public void Map(EntityTypeBuilder<Product> entity)
        {
            entity.ToTable("Product", "Production");
            entity.Property(e => e.Active).HasDefaultValueSql("((1))");
            entity.Property(e => e.BusinessEntityId).HasColumnName("BusinessEntityID");
            entity.Property(e => e.CategoryId).HasColumnName("CategoryID");
            entity.Property(e => e.ClassId).HasColumnName("ClassID");
            entity.Property(e => e.Ean)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.FinishedGoodsFlag).HasDefaultValueSql("((1))");
            entity.Property(e => e.Height).HasColumnType("numeric(10, 3)");
            entity.Property(e => e.HsCode)
                .HasMaxLength(30)
                .IsUnicode(false);
            entity.Property(e => e.HsCodeTax)
                .HasMaxLength(30)
                .IsUnicode(false);
            entity.Property(e => e.Length).HasColumnType("numeric(10, 3)");
            entity.Property(e => e.ListPrice)
                .HasColumnType("numeric(12, 4)")
                .HasDefaultValueSql("((0))");
            entity.Property(e => e.Location)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Manufacturer)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.MaximumStocklevel).HasColumnType("numeric(12, 4)");
            entity.Property(e => e.ModifiedDate)
                .HasColumnType("datetime")
                .HasDefaultValueSql("(getdate())");
            entity.Property(e => e.Name)
                .IsRequired()
                .HasMaxLength(120)
                .IsUnicode(false);
            entity.Property(e => e.ProductAttribute).HasColumnType("xml");
            entity.Property(e => e.ProductNumber)
                .IsRequired()
                .HasMaxLength(30)
                .IsUnicode(false);
            entity.Property(e => e.ProductSourceId).HasColumnName("ProductSourceID");
            entity.Property(e => e.ReorderPoint)
                .HasColumnType("numeric(12, 4)")
                .HasDefaultValueSql("((0))");
            entity.Property(e => e.SafetyStockLevel).HasColumnType("numeric(12, 4)");
            entity.Property(e => e.SellEndDate).HasColumnType("datetime");
            entity.Property(e => e.SellStartDate)
                .HasColumnType("datetime")
                .HasDefaultValueSql("(getdate())");
            entity.Property(e => e.CreateDate)
                .HasColumnType("datetime")
                .HasDefaultValueSql("(getdate())");
            entity.Property(e => e.SizeUnitMeasureCode).HasColumnType("char(3)");
            entity.Property(e => e.StandardCost)
                .HasColumnType("numeric(12, 4)")
                .HasDefaultValueSql("((0))");
            entity.Property(e => e.TaxImport).HasColumnType("numeric(5, 2)");
            entity.Property(e => e.TaxIva).HasColumnType("numeric(5, 2)");
            entity.Property(e => e.TaxProduction).HasColumnType("numeric(5, 2)");
            entity.Property(e => e.TaxSale).HasColumnType("numeric(5, 2)");
            entity.Property(e => e.Weight).HasColumnType("numeric(12, 4)");
            entity.Property(e => e.WeightTotal).HasColumnType("numeric(12, 4)");
            entity.Property(e => e.Width).HasColumnType("numeric(10, 3)");

            entity.HasOne(d => d.Category)
                .WithMany(p => p.Product)
                .HasForeignKey(d => d.CategoryId)
                .HasConstraintName("FK_Product_ProductCategory");
            entity.HasOne(d => d.Class)
                .WithMany(p => p.Product)
                .HasForeignKey(d => d.ClassId)
                .HasConstraintName("FK_Product_ProductClass");
            entity.HasOne(d => d.TaxGroup)
                .WithMany(p => p.Product)
                .HasForeignKey(d => d.TaxGroupId)
                .HasConstraintName("FK_Product_ProductTax");
        }
    }
    public class ProductImageMap : IMapConfiguration<ProductImage>
    {
        public void Map(EntityTypeBuilder<ProductImage> entity)
        {
            entity.ToTable("ProductImage", "Production");
            entity.HasIndex(e => new { e.ImageId, e.ProductId, e.IsPrimary })
                .HasName("IX_ProductImage");
            entity.Property(e => e.Id).HasColumnName("Id");
            entity.Property(e => e.CreateDate)
                .HasColumnType("datetime")
                .HasDefaultValueSql("(getdate())");
            entity.HasOne(d => d.Image)
                .WithMany(p => p.ProductImage)
                .HasForeignKey(d => d.ImageId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_ProductImage_Image");
            entity.HasOne(d => d.Product)
                .WithMany(p => p.ProductImage)
                .HasForeignKey(d => d.ProductId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_ProductImage_Product");

        }
    }

    public class TaxMap : IMapConfiguration<Tax>
    {
        public void Map(EntityTypeBuilder<Tax> entity)
        {
            entity.ToTable("Tax", "Accounting");
            entity.Property(e => e.TaxId).ValueGeneratedNever();
            entity.Property(e => e.BusinessEntityId).HasColumnName("BusinessEntityID");
            entity.Property(e => e.TaxGroupId)
                .IsRequired()
                .HasColumnType("char(2)")
                .HasDefaultValueSql("('SL')");

        }
    }
    
   
    public class UserSettingMap : IMapConfiguration<UserSetting>
    {
        public void Map(EntityTypeBuilder<UserSetting> entity)
        {
            entity.ToTable("UserSetting", "Security");
            entity.Property(e => e.UserSettingId).ValueGeneratedNever();

            entity.Property(e => e.BusinessEntityId).HasColumnName("BusinessEntityID");


        }
    }
}