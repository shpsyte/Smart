using Microsoft.EntityFrameworkCore;
using Core.Domain.PersonAndData;
using Core.Domain.Finance;
using Core.Domain.Business;
using Core.Domain.Production;
using Core.Domain.Region;
using Core.Domain.Sale;
using Core.Domain.Accounting;
using Core.Domain.Identity;
using Core.Domain.Finance.Views;

namespace Smart.Data
{
    public partial class ContextOnlyGClasse : DbContext
    {
        public ContextOnlyGClasse()
        {

        }
        public ContextOnlyGClasse(DbContextOptions<ContextOnlyGClasse> options) : base(options)
        {
        }


        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer(@"Data Source=IDELLSERVER\\\\eXPRESS,1433;Database=Smart;User=sa;Password=Jymkatana_6985;MultipleActiveResultSets=true");
            }
        }



        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<ProductInventory>(entity =>
            {
                entity.ToTable("ProductInventory", "Production");
                entity.HasKey(e => e.Id);


            });

            modelBuilder.Entity<RevenueTrans>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.ToTable("RevenueTrans", "Financial");

            });



            
                 modelBuilder.Entity<VProduct>(entity =>
                 {
                     entity.HasKey(e => e.ProductId);
                     entity.ToTable("VProduct", "Production");

                 });
            modelBuilder.Entity<VExpense>(entity =>
                {
                    entity.HasKey(e => e.ExpenseId);
                    entity.ToTable("VExpense", "Financial");

                });



            modelBuilder.Entity<VRevenue>(entity =>
            {
                entity.HasKey(e => e.RevenueId);
                entity.ToTable("VRevenue", "Financial");

            });


            modelBuilder.Entity<ExpenseTrans>(entity =>
                {
                    entity.HasKey(e => e.Id);
                    entity.ToTable("ExpenseTrans", "Financial");

                });

            modelBuilder.Entity<CategoryProduct>(entity =>
            {
                entity.HasKey(e => e.CategoryId);
                entity.ToTable("Category", "Production");

            });





            modelBuilder.Entity<Address>(entity =>
            {
                entity.HasKey(e => e.AddressId);

                entity.ToTable("Address", "Person");

                entity.HasIndex(e => e.BusinessEntityId);

                entity.HasIndex(e => e.PostalCode)
                    .HasName("IX_Address");

                entity.Property(e => e.BusinessEntityId).HasColumnName("BusinessEntityID");

                entity.Property(e => e.Deleted).HasColumnName("deleted");

                entity.Property(e => e.District)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Number).HasColumnType("char(20)");

                entity.Property(e => e.PostalCode)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.SpatialLocation)
                    .HasMaxLength(150)
                    .IsUnicode(false);

                entity.Property(e => e.StreetAddress)
                    .IsRequired()
                    .HasMaxLength(150)
                    .IsUnicode(false);

                entity.Property(e => e.StreetAddressLine2)
                    .HasMaxLength(150)
                    .IsUnicode(false);

                entity.Property(e => e.StreetAddressLine3)
                    .HasMaxLength(150)
                    .IsUnicode(false);


            });

            modelBuilder.Entity<BusinessEntity>(entity =>
            {
                entity.HasKey(e => e.BusinessEntityId);
                entity.ToTable("BusinessEntity", "Business");

                entity.Property(e => e.BusinessEntityId).HasColumnName("BusinessEntityID");

                entity.Property(e => e.Active).HasDefaultValueSql("((1))");

                entity.Property(e => e.CreateDate)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.EmailCreate)
                    .IsRequired()
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.ExternalCode)
                    .IsRequired()
                    .HasMaxLength(40)
                    .IsUnicode(false)
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Validate)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");
            });



            modelBuilder.Entity<CategoryFinancial>(entity =>
            {
                entity.HasKey(e => e.ChartAccountId);

                entity.ToTable("Category", "Financial");




            });

            modelBuilder.Entity<CategoryPerson>(entity =>
            {
                entity.HasKey(e => e.CategoryId);

                entity.ToTable("Category", "Person");

                entity.HasIndex(e => e.BusinessEntityId);

                entity.HasIndex(e => e.Name)
                    .HasName("IX_PersonType");

                entity.Property(e => e.BusinessEntityId).HasColumnName("BusinessEntityID");

                entity.Property(e => e.CreateDate)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.ModifiedDate)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(80)
                    .IsUnicode(false);


            });

            modelBuilder.Entity<City>(entity =>
            {
                entity.HasKey(e => e.CityId);
                entity.ToTable("City", "Person");

                entity.HasIndex(e => e.Name)
                    .HasName("IX_City");

                entity.HasIndex(e => e.StateProvinceId);



                entity.Property(e => e.MiddleName)
                    .HasMaxLength(50)
                    .IsUnicode(false);



                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(150)
                    .IsUnicode(false);

                entity.Property(e => e.SpecialCodeRegion)
                    .HasMaxLength(80)
                    .IsUnicode(false);

                entity.HasOne(d => d.StateProvince)
                    .WithMany(p => p.City)
                    .HasForeignKey(d => d.StateProvinceId)
                    .HasConstraintName("FK_City_StateProvince");
            });

            modelBuilder.Entity<ClassProduct>(entity =>
            {
                entity.HasKey(e => e.ClassId);

                entity.ToTable("Class", "Production");

                entity.HasIndex(e => e.BusinessEntityId);

                entity.HasIndex(e => e.Name)
                    .HasName("IX_ProductClass");

                entity.Property(e => e.BusinessEntityId).HasColumnName("BusinessEntityID");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(120)
                    .IsUnicode(false);


            });

            modelBuilder.Entity<Condition>(entity =>
            {
                entity.HasKey(e => e.ConditionId);
                entity.ToTable("Condition", "Sales");

                entity.HasIndex(e => e.BusinessEntityId);

                entity.Property(e => e.Active).HasDefaultValueSql("((1))");

                entity.Property(e => e.BusinessEntityId).HasColumnName("BusinessEntityID");



                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Tax).HasColumnType("numeric(5, 2)");


            });

            modelBuilder.Entity<CostCenter>(entity =>
            {
                entity.HasKey(e => e.CostCenterId);
                entity.ToTable("CostCenter", "Financial");




            });

            modelBuilder.Entity<Country>(entity =>
            {
                entity.HasKey(e => e.CountryId);
                entity.ToTable("Country", "Person");


            });

            modelBuilder.Entity<Email>(entity =>
            {
                entity.HasKey(e => e.EmailId);
                entity.ToTable("Email", "Person");

                entity.HasIndex(e => e.BusinessEntityId);

                entity.HasIndex(e => e.Email1)
                    .HasName("IX_Email");

                entity.Property(e => e.EmailId).HasColumnName("EmailID");

                entity.Property(e => e.BusinessEntityId).HasColumnName("BusinessEntityID");

                entity.Property(e => e.Email1)
                    .IsRequired()
                    .HasColumnName("Email")
                    .HasMaxLength(250)
                    .IsUnicode(false);



            });

            modelBuilder.Entity<Expense>(entity =>
            {
                entity.HasKey(e => e.ExpenseId);
                entity.ToTable("Expense", "Financial");

                entity.HasIndex(e => e.BusinessEntityId);

                entity.HasIndex(e => e.CategoryId)
                    .HasName("IX_Expense_ChartAccountId");

                entity.HasIndex(e => e.CostCenterId);

                entity.HasIndex(e => e.PersonId);

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
            });

            modelBuilder.Entity<HsCode>(entity =>
            {
                entity.HasKey(e => e.HsCodeId);
                entity.ToTable("HsCode", "Production");

                entity.HasIndex(e => e.HsCode1)
                    .HasName("IX_Ncm");

                entity.Property(e => e.BusinessEntityId).HasColumnName("BusinessEntityID");

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

                entity.Property(e => e.StateTaxes).HasColumnType("numeric(5, 2)");


            });

            modelBuilder.Entity<Image>(entity =>
            {
                entity.HasKey(e => e.ImageId);
                entity.ToTable("Image", "Production");

                entity.HasIndex(e => e.BusinessEntityId);

                entity.Property(e => e.Active).HasDefaultValueSql("((1))");

                entity.Property(e => e.BusinessEntityId).HasColumnName("BusinessEntityID");

                entity.Property(e => e.Comments).IsUnicode(false);

                entity.Property(e => e.CreateDate)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.LargeImageFileName)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.ModifiedDate)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");


            });

            modelBuilder.Entity<Invoice>(entity =>
            {
                entity.HasKey(e => e.InvoiceId);
                entity.ToTable("Invoice", "Sales");

                entity.HasIndex(e => e.BillToAddressId);

                entity.HasIndex(e => e.BusinessEntityId);

                entity.HasIndex(e => e.CarrierId);

                entity.HasIndex(e => e.CustomerId);

                entity.HasIndex(e => e.SalesPersonId);

                entity.HasIndex(e => e.ShipToAddressId);

                entity.HasIndex(e => e.WarehouseId);

                entity.Property(e => e.InvoiceId).ValueGeneratedNever();

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
            });

            modelBuilder.Entity<InvoiceDetail>(entity =>
            {
                entity.HasKey(e => new { e.InvoiceId, e.InvoiceDetailId });

                entity.ToTable("InvoiceDetail", "Sales");

                entity.HasIndex(e => e.BusinessEntityId);

                entity.HasIndex(e => e.ProductId);

                entity.HasIndex(e => e.WarehouseId);

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
            });

            modelBuilder.Entity<Person>(entity =>
            {
                entity.HasKey(e => e.PersonId);
                entity.ToTable("Person", "Person");

                entity.HasIndex(e => e.BusinessEntityId);

                entity.HasIndex(e => e.CategoryId);

                entity.HasIndex(e => e.RegistrationCode)
                    .HasName("IX_Person_1");

                entity.HasIndex(e => new { e.FirstName, e.LastName })
                    .HasName("IX_Person");

                entity.Property(e => e.Active).HasDefaultValueSql("((1))");

                entity.Property(e => e.BusinessEntityId).HasColumnName("BusinessEntityID");

                entity.Property(e => e.Comments).IsUnicode(false);

                entity.Property(e => e.CreateDate)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.Email).IsUnicode(false);

                entity.Property(e => e.FirstName)
                    .IsRequired()
                    .HasMaxLength(120)
                    .IsUnicode(false);

                entity.Property(e => e.LastName)
                    .HasMaxLength(80)
                    .IsUnicode(false);

                entity.Property(e => e.ModifiedDate)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.PersonCode).HasColumnType("char(50)");



                entity.Property(e => e.RegistrationCode)
                    .HasMaxLength(150)
                    .IsUnicode(false);

                entity.Property(e => e.RegistrationState)
                    .HasMaxLength(150)
                    .IsUnicode(false);



                entity.HasOne(d => d.Category)
                    .WithMany(p => p.Person)
                    .HasForeignKey(d => d.CategoryId)
                    .HasConstraintName("FK_Person_PersonClass");
            });

            modelBuilder.Entity<PersonAddress>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.ToTable("PersonAddress", "Person");

                entity.HasIndex(e => e.AddressId);

                entity.HasIndex(e => e.PersonId);

                entity.Property(e => e.BusinessEntityId).HasColumnName("BusinessEntityID");

                entity.HasOne(d => d.Address)
                    .WithMany(p => p.PersonAddress)
                    .HasForeignKey(d => d.AddressId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_PersonAddress_Address");



                entity.HasOne(d => d.Person)
                    .WithMany(p => p.PersonAddress)
                    .HasForeignKey(d => d.PersonId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_PersonAddress_Person");
            });

            modelBuilder.Entity<PersonEmail>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.ToTable("PersonEmail", "Person");

                entity.HasIndex(e => e.EmailId);

                entity.HasIndex(e => new { e.PersonId, e.EmailId })
                    .HasName("IX_PersonEmail");

                entity.Property(e => e.BusinessEntityId).HasColumnName("BusinessEntityID");


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
            });

            modelBuilder.Entity<PersonPhone>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.ToTable("PersonPhone", "Person");

                entity.HasIndex(e => e.PhoneId);

                entity.HasIndex(e => new { e.PersonId, e.PhoneId })
                    .HasName("IX_PersonPhone");

                entity.Property(e => e.BusinessEntityId).HasColumnName("BusinessEntityID");



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
            });

            modelBuilder.Entity<Phone>(entity =>
            {
                entity.HasKey(e => e.PhoneId);
                entity.ToTable("Phone", "Person");

                entity.HasIndex(e => e.BusinessEntityId);

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


            });

            modelBuilder.Entity<Product>(entity =>
            {
                entity.HasKey(e => e.ProductId);
                entity.ToTable("Product", "Production");

                entity.HasIndex(e => e.BusinessEntityId);

                entity.HasIndex(e => e.CategoryId);

                entity.HasIndex(e => e.ClassId);

                entity.HasIndex(e => e.TaxGroupId);

                entity.Property(e => e.Active).HasDefaultValueSql("((1))");

                entity.Property(e => e.BusinessEntityId).HasColumnName("BusinessEntityID");

                entity.Property(e => e.CategoryId).HasColumnName("CategoryID");

                entity.Property(e => e.ClassId).HasColumnName("ClassID");

                entity.Property(e => e.CreateDate)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

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
            });

            modelBuilder.Entity<ProductImage>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.ToTable("ProductImage", "Production");

                entity.HasIndex(e => e.ProductId);

                entity.HasIndex(e => new { e.ImageId, e.ProductId, e.IsPrimary })
                    .HasName("IX_ProductImage");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.BusinessEntityId).HasColumnName("BusinessEntityID");

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
            });

            modelBuilder.Entity<Revenue>(entity =>
            {
                entity.HasKey(e => e.RevenueId);
                entity.ToTable("Revenue", "Financial");

                entity.HasIndex(e => e.BusinessEntityId);

                entity.HasIndex(e => e.CategoryId)
                    .HasName("IX_Revenue_ChartAccountId");

                entity.HasIndex(e => e.CostCenterId);

                entity.HasIndex(e => e.PaymentConditionId);

                entity.HasIndex(e => e.PersonId);

                entity.Property(e => e.BusinessEntityId).HasColumnName("BusinessEntityID");

                entity.Property(e => e.Comment)
                    .HasMaxLength(250)
                    .IsUnicode(false);

                entity.Property(e => e.CreateDate)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.DueDate).HasColumnType("date");

                entity.Property(e => e.DuePayment).HasColumnType("date");

                entity.Property(e => e.ModifiedDate)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(80)
                    .IsUnicode(false);

                entity.Property(e => e.RevenueNumber)
                    .HasMaxLength(30)
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
            });



            modelBuilder.Entity<StateProvince>(entity =>
            {
                entity.HasKey(e => e.StateProvinceId);
                entity.ToTable("StateProvince", "Person");

                entity.HasIndex(e => e.CountryRegionId);

                entity.HasIndex(e => new { e.Name, e.StateProvinceCode })
                    .HasName("IX_StateProvince");



                entity.Property(e => e.IsOnlyStateProvinceFlag).HasDefaultValueSql("((1))");


                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(80)
                    .IsUnicode(false);

                entity.Property(e => e.StateProvinceCode)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.HasOne(d => d.CountryRegion)
                    .WithMany(p => p.StateProvince)
                    .HasForeignKey(d => d.CountryRegionId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_StateProvince_Territory");
            });

            modelBuilder.Entity<Tax>(entity =>
            {
                entity.HasKey(e => e.TaxId);
                entity.ToTable("Tax", "Accounting");

                entity.HasIndex(e => e.BusinessEntityId);

                entity.Property(e => e.TaxId).ValueGeneratedNever();

                entity.Property(e => e.BusinessEntityId).HasColumnName("BusinessEntityID");

                entity.Property(e => e.TaxGroupId)
                    .IsRequired()
                    .HasColumnType("char(2)")
                    .HasDefaultValueSql("('SL')");


            });

            modelBuilder.Entity<TaxGroup>(entity =>
            {
                entity.HasKey(e => e.TaxGroupId);
                entity.ToTable("TaxGroup", "Accounting");

                entity.HasIndex(e => e.BusinessEntityId);


                entity.Property(e => e.BusinessEntityId).HasColumnName("BusinessEntityID");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(120)
                    .IsUnicode(false);


            });

            modelBuilder.Entity<TaxOperation>(entity =>
            {
                entity.HasKey(e => e.TaxOperationId);
                entity.ToTable("TaxOperation", "Accounting");

            });


            modelBuilder.Entity<UserSetting>(entity =>
            {
                entity.HasKey(e => e.UserSettingId);
                entity.ToTable("UserSetting", "Security");

                entity.HasIndex(e => e.BusinessEntityId);

                entity.Property(e => e.UserSettingId).ValueGeneratedNever();

                entity.Property(e => e.BusinessEntityId).HasColumnName("BusinessEntityID");


            });



            modelBuilder.Entity<Location>(entity =>
            {
                entity.HasKey(e => e.WarehouseId);
                entity.ToTable("Warehouse", "Production");

                entity.HasIndex(e => e.Name)
                    .HasName("IX_Warehouse");

                entity.Property(e => e.BusinessEntityId).HasColumnName("BusinessEntityID");


                entity.Property(e => e.Name)
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });
        }



        public DbSet<Core.Domain.Accounting.TaxGroup> TaxGroup { get; set; }



        public DbSet<Core.Domain.Accounting.TaxOperation> TaxOperation { get; set; }



        public DbSet<Core.Domain.Finance.CategoryFinancial> CategoryFinancial { get; set; }



        public DbSet<Core.Domain.Finance.CostCenter> CostCenter { get; set; }



        public DbSet<Core.Domain.Region.Country> Country { get; set; }



        public DbSet<Core.Domain.Region.StateProvince> StateProvince { get; set; }



        public DbSet<Core.Domain.Region.City> City { get; set; }



        public DbSet<Core.Domain.Production.CategoryProduct> CategoryProduct { get; set; }



        public DbSet<Core.Domain.Production.ClassProduct> ClassProduct { get; set; }



        public DbSet<Core.Domain.Production.Location> Warehouse { get; set; }



        public DbSet<Core.Domain.Sale.Condition> Condition { get; set; }



        public DbSet<Core.Domain.PersonAndData.Person> Person { get; set; }



        public DbSet<Core.Domain.Finance.Revenue> Revenue { get; set; }



        public DbSet<Core.Domain.Finance.Bank> Bank { get; set; }



        public DbSet<Core.Domain.Finance.Expense> Expense { get; set; }



        public DbSet<Core.Domain.Finance.BankTrans> BankTrans { get; set; }



        public DbSet<Core.Domain.Production.Product> Product { get; set; }



        public DbSet<Core.Domain.Sale.Invoice> Invoice { get; set; }


    }
}
