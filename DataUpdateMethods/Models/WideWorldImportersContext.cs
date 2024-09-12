using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace DataUpdateMethods.Models;

public partial class WideWorldImportersContext : DbContext
{
    private readonly IConfiguration _configuration;

    public WideWorldImportersContext()
    {
    }

    public WideWorldImportersContext(DbContextOptions<WideWorldImportersContext> options, IConfiguration configuration)
        : base(options)
    {
        _configuration = configuration;
    }

    public virtual DbSet<BuyingGroup> BuyingGroups { get; set; }

    public virtual DbSet<City> Cities { get; set; }

    public virtual DbSet<ColdRoomTemperature> ColdRoomTemperatures { get; set; }

    public virtual DbSet<Color> Colors { get; set; }

    public virtual DbSet<Country> Countries { get; set; }

    public virtual DbSet<Customer> Customers { get; set; }

    public virtual DbSet<CustomerCategory> CustomerCategories { get; set; }

    public virtual DbSet<CustomerTransaction> CustomerTransactions { get; set; }

    public virtual DbSet<DeliveryMethod> DeliveryMethods { get; set; }

    public virtual DbSet<Invoice> Invoices { get; set; }

    public virtual DbSet<InvoiceLine> InvoiceLines { get; set; }

    public virtual DbSet<Order> Orders { get; set; }

    public virtual DbSet<OrderLine> OrderLines { get; set; }

    public virtual DbSet<PackageType> PackageTypes { get; set; }

    public virtual DbSet<PaymentMethod> PaymentMethods { get; set; }

    public virtual DbSet<Person> People { get; set; }

    public virtual DbSet<PurchaseOrder> PurchaseOrders { get; set; }

    public virtual DbSet<PurchaseOrderLine> PurchaseOrderLines { get; set; }

    public virtual DbSet<SpecialDeal> SpecialDeals { get; set; }

    public virtual DbSet<StateProvince> StateProvinces { get; set; }

    public virtual DbSet<StockGroup> StockGroups { get; set; }

    public virtual DbSet<StockItem> StockItems { get; set; }

    public virtual DbSet<StockItemHolding> StockItemHoldings { get; set; }

    public virtual DbSet<StockItemStockGroup> StockItemStockGroups { get; set; }

    public virtual DbSet<StockItemTransaction> StockItemTransactions { get; set; }

    public virtual DbSet<Supplier> Suppliers { get; set; }

    public virtual DbSet<SupplierCategory> SupplierCategories { get; set; }

    public virtual DbSet<SupplierTransaction> SupplierTransactions { get; set; }

    public virtual DbSet<SystemParameter> SystemParameters { get; set; }

    public virtual DbSet<TransactionType> TransactionTypes { get; set; }

    public virtual DbSet<VehicleTemperature> VehicleTemperatures { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        if (!optionsBuilder.IsConfigured)
        {
            var connectionString = _configuration.GetConnectionString("WideWorldImportersDatabase");
            optionsBuilder.UseSqlServer(connectionString);
        }
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.UseCollation("Latin1_General_100_CI_AS");

        modelBuilder.Entity<BuyingGroup>(entity =>
        {
            entity.HasKey(e => e.BuyingGroupId).HasName("PK_Sales_BuyingGroups");

            entity.ToTable(tb => tb.IsTemporal(ttb =>
                    {
                        ttb.UseHistoryTable("BuyingGroups_Archive", "Sales");
                        ttb
                            .HasPeriodStart("ValidFrom")
                            .HasColumnName("ValidFrom");
                        ttb
                            .HasPeriodEnd("ValidTo")
                            .HasColumnName("ValidTo");
                    }));

            entity.Property(e => e.BuyingGroupId).HasDefaultValueSql("(NEXT VALUE FOR [Sequences].[BuyingGroupID])");

            entity.HasOne(d => d.LastEditedByNavigation).WithMany(p => p.BuyingGroups)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Sales_BuyingGroups_Application_People");
        });

        modelBuilder.Entity<City>(entity =>
        {
            entity.HasKey(e => e.CityId).HasName("PK_Application_Cities");

            entity.ToTable(tb => tb.IsTemporal(ttb =>
                    {
                        ttb.UseHistoryTable("Cities_Archive", "Application");
                        ttb
                            .HasPeriodStart("ValidFrom")
                            .HasColumnName("ValidFrom");
                        ttb
                            .HasPeriodEnd("ValidTo")
                            .HasColumnName("ValidTo");
                    }));

            entity.Property(e => e.CityId).HasDefaultValueSql("(NEXT VALUE FOR [Sequences].[CityID])");

            entity.HasOne(d => d.LastEditedByNavigation).WithMany(p => p.Cities)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Application_Cities_Application_People");

            entity.HasOne(d => d.StateProvince).WithMany(p => p.Cities)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Application_Cities_StateProvinceID_Application_StateProvinces");
        });

        modelBuilder.Entity<ColdRoomTemperature>(entity =>
        {
            entity.HasKey(e => e.ColdRoomTemperatureId)
                .HasName("PK_Warehouse_ColdRoomTemperatures")
                .IsClustered(false);

            entity
                .IsMemoryOptimized()
                .ToTable(tb => tb.IsTemporal(ttb =>
                    {
                        ttb.UseHistoryTable("ColdRoomTemperatures_Archive", "Warehouse");
                        ttb
                            .HasPeriodStart("ValidFrom")
                            .HasColumnName("ValidFrom");
                        ttb
                            .HasPeriodEnd("ValidTo")
                            .HasColumnName("ValidTo");
                    }));
        });

        modelBuilder.Entity<Color>(entity =>
        {
            entity.HasKey(e => e.ColorId).HasName("PK_Warehouse_Colors");

            entity.ToTable(tb => tb.IsTemporal(ttb =>
                    {
                        ttb.UseHistoryTable("Colors_Archive", "Warehouse");
                        ttb
                            .HasPeriodStart("ValidFrom")
                            .HasColumnName("ValidFrom");
                        ttb
                            .HasPeriodEnd("ValidTo")
                            .HasColumnName("ValidTo");
                    }));

            entity.Property(e => e.ColorId).HasDefaultValueSql("(NEXT VALUE FOR [Sequences].[ColorID])");

            entity.HasOne(d => d.LastEditedByNavigation).WithMany(p => p.Colors)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Warehouse_Colors_Application_People");
        });

        modelBuilder.Entity<Country>(entity =>
        {
            entity.HasKey(e => e.CountryId).HasName("PK_Application_Countries");

            entity.ToTable(tb => tb.IsTemporal(ttb =>
                    {
                        ttb.UseHistoryTable("Countries_Archive", "Application");
                        ttb
                            .HasPeriodStart("ValidFrom")
                            .HasColumnName("ValidFrom");
                        ttb
                            .HasPeriodEnd("ValidTo")
                            .HasColumnName("ValidTo");
                    }));

            entity.Property(e => e.CountryId).HasDefaultValueSql("(NEXT VALUE FOR [Sequences].[CountryID])");

            entity.HasOne(d => d.LastEditedByNavigation).WithMany(p => p.Countries)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Application_Countries_Application_People");
        });

        modelBuilder.Entity<Customer>(entity =>
        {
            entity.HasKey(e => e.CustomerId).HasName("PK_Sales_Customers");

            entity.ToTable(tb => tb.IsTemporal(ttb =>
                    {
                        ttb.UseHistoryTable("Customers_Archive", "Sales");
                        ttb
                            .HasPeriodStart("ValidFrom")
                            .HasColumnName("ValidFrom");
                        ttb
                            .HasPeriodEnd("ValidTo")
                            .HasColumnName("ValidTo");
                    }));

            entity.Property(e => e.CustomerId).HasDefaultValueSql("(NEXT VALUE FOR [Sequences].[CustomerID])");

            entity.HasOne(d => d.AlternateContactPerson).WithMany(p => p.CustomerAlternateContactPeople).HasConstraintName("FK_Sales_Customers_AlternateContactPersonID_Application_People");

            entity.HasOne(d => d.BillToCustomer).WithMany(p => p.InverseBillToCustomer)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Sales_Customers_BillToCustomerID_Sales_Customers");

            entity.HasOne(d => d.BuyingGroup).WithMany(p => p.Customers).HasConstraintName("FK_Sales_Customers_BuyingGroupID_Sales_BuyingGroups");

            entity.HasOne(d => d.CustomerCategory).WithMany(p => p.Customers)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Sales_Customers_CustomerCategoryID_Sales_CustomerCategories");

            entity.HasOne(d => d.DeliveryCity).WithMany(p => p.CustomerDeliveryCities)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Sales_Customers_DeliveryCityID_Application_Cities");

            entity.HasOne(d => d.DeliveryMethod).WithMany(p => p.Customers)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Sales_Customers_DeliveryMethodID_Application_DeliveryMethods");

            entity.HasOne(d => d.LastEditedByNavigation).WithMany(p => p.CustomerLastEditedByNavigations)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Sales_Customers_Application_People");

            entity.HasOne(d => d.PostalCity).WithMany(p => p.CustomerPostalCities)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Sales_Customers_PostalCityID_Application_Cities");

            entity.HasOne(d => d.PrimaryContactPerson).WithMany(p => p.CustomerPrimaryContactPeople)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Sales_Customers_PrimaryContactPersonID_Application_People");
        });

        modelBuilder.Entity<CustomerCategory>(entity =>
        {
            entity.HasKey(e => e.CustomerCategoryId).HasName("PK_Sales_CustomerCategories");

            entity.ToTable(tb => tb.IsTemporal(ttb =>
                    {
                        ttb.UseHistoryTable("CustomerCategories_Archive", "Sales");
                        ttb
                            .HasPeriodStart("ValidFrom")
                            .HasColumnName("ValidFrom");
                        ttb
                            .HasPeriodEnd("ValidTo")
                            .HasColumnName("ValidTo");
                    }));

            entity.Property(e => e.CustomerCategoryId).HasDefaultValueSql("(NEXT VALUE FOR [Sequences].[CustomerCategoryID])");

            entity.HasOne(d => d.LastEditedByNavigation).WithMany(p => p.CustomerCategories)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Sales_CustomerCategories_Application_People");
        });

        modelBuilder.Entity<CustomerTransaction>(entity =>
        {
            entity.HasKey(e => e.CustomerTransactionId)
                .HasName("PK_Sales_CustomerTransactions")
                .IsClustered(false);

            entity.HasIndex(e => e.TransactionDate, "CX_Sales_CustomerTransactions").IsClustered();

            entity.Property(e => e.CustomerTransactionId).HasDefaultValueSql("(NEXT VALUE FOR [Sequences].[TransactionID])");
            entity.Property(e => e.IsFinalized).HasComputedColumnSql("(case when [FinalizationDate] IS NULL then CONVERT([bit],(0)) else CONVERT([bit],(1)) end)", true);
            entity.Property(e => e.LastEditedWhen).HasDefaultValueSql("(sysdatetime())");

            entity.HasOne(d => d.Customer).WithMany(p => p.CustomerTransactions)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Sales_CustomerTransactions_CustomerID_Sales_Customers");

            entity.HasOne(d => d.Invoice).WithMany(p => p.CustomerTransactions).HasConstraintName("FK_Sales_CustomerTransactions_InvoiceID_Sales_Invoices");

            entity.HasOne(d => d.LastEditedByNavigation).WithMany(p => p.CustomerTransactions)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Sales_CustomerTransactions_Application_People");

            entity.HasOne(d => d.PaymentMethod).WithMany(p => p.CustomerTransactions).HasConstraintName("FK_Sales_CustomerTransactions_PaymentMethodID_Application_PaymentMethods");

            entity.HasOne(d => d.TransactionType).WithMany(p => p.CustomerTransactions)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Sales_CustomerTransactions_TransactionTypeID_Application_TransactionTypes");
        });

        modelBuilder.Entity<DeliveryMethod>(entity =>
        {
            entity.HasKey(e => e.DeliveryMethodId).HasName("PK_Application_DeliveryMethods");

            entity.ToTable(tb => tb.IsTemporal(ttb =>
                    {
                        ttb.UseHistoryTable("DeliveryMethods_Archive", "Application");
                        ttb
                            .HasPeriodStart("ValidFrom")
                            .HasColumnName("ValidFrom");
                        ttb
                            .HasPeriodEnd("ValidTo")
                            .HasColumnName("ValidTo");
                    }));

            entity.Property(e => e.DeliveryMethodId).HasDefaultValueSql("(NEXT VALUE FOR [Sequences].[DeliveryMethodID])");

            entity.HasOne(d => d.LastEditedByNavigation).WithMany(p => p.DeliveryMethods)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Application_DeliveryMethods_Application_People");
        });

        modelBuilder.Entity<Invoice>(entity =>
        {
            entity.HasKey(e => e.InvoiceId).HasName("PK_Sales_Invoices");

            entity.Property(e => e.InvoiceId).HasDefaultValueSql("(NEXT VALUE FOR [Sequences].[InvoiceID])");
            entity.Property(e => e.ConfirmedDeliveryTime).HasComputedColumnSql("(TRY_CONVERT([datetime2](7),json_value([ReturnedDeliveryData],N'$.DeliveredWhen'),(126)))", false);
            entity.Property(e => e.ConfirmedReceivedBy).HasComputedColumnSql("(json_value([ReturnedDeliveryData],N'$.ReceivedBy'))", false);
            entity.Property(e => e.LastEditedWhen).HasDefaultValueSql("(sysdatetime())");

            entity.HasOne(d => d.AccountsPerson).WithMany(p => p.InvoiceAccountsPeople)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Sales_Invoices_AccountsPersonID_Application_People");

            entity.HasOne(d => d.BillToCustomer).WithMany(p => p.InvoiceBillToCustomers)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Sales_Invoices_BillToCustomerID_Sales_Customers");

            entity.HasOne(d => d.ContactPerson).WithMany(p => p.InvoiceContactPeople)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Sales_Invoices_ContactPersonID_Application_People");

            entity.HasOne(d => d.Customer).WithMany(p => p.InvoiceCustomers)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Sales_Invoices_CustomerID_Sales_Customers");

            entity.HasOne(d => d.DeliveryMethod).WithMany(p => p.Invoices)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Sales_Invoices_DeliveryMethodID_Application_DeliveryMethods");

            entity.HasOne(d => d.LastEditedByNavigation).WithMany(p => p.InvoiceLastEditedByNavigations)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Sales_Invoices_Application_People");

            entity.HasOne(d => d.Order).WithMany(p => p.Invoices).HasConstraintName("FK_Sales_Invoices_OrderID_Sales_Orders");

            entity.HasOne(d => d.PackedByPerson).WithMany(p => p.InvoicePackedByPeople)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Sales_Invoices_PackedByPersonID_Application_People");

            entity.HasOne(d => d.SalespersonPerson).WithMany(p => p.InvoiceSalespersonPeople)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Sales_Invoices_SalespersonPersonID_Application_People");
        });

        modelBuilder.Entity<InvoiceLine>(entity =>
        {
            entity.HasKey(e => e.InvoiceLineId).HasName("PK_Sales_InvoiceLines");

            entity.Property(e => e.InvoiceLineId).HasDefaultValueSql("(NEXT VALUE FOR [Sequences].[InvoiceLineID])");
            entity.Property(e => e.LastEditedWhen).HasDefaultValueSql("(sysdatetime())");

            entity.HasOne(d => d.Invoice).WithMany(p => p.InvoiceLines)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Sales_InvoiceLines_InvoiceID_Sales_Invoices");

            entity.HasOne(d => d.LastEditedByNavigation).WithMany(p => p.InvoiceLines)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Sales_InvoiceLines_Application_People");

            entity.HasOne(d => d.PackageType).WithMany(p => p.InvoiceLines)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Sales_InvoiceLines_PackageTypeID_Warehouse_PackageTypes");

            entity.HasOne(d => d.StockItem).WithMany(p => p.InvoiceLines)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Sales_InvoiceLines_StockItemID_Warehouse_StockItems");
        });

        modelBuilder.Entity<Order>(entity =>
        {
            entity.HasKey(e => e.OrderId).HasName("PK_Sales_Orders");

            entity.Property(e => e.OrderId).HasDefaultValueSql("(NEXT VALUE FOR [Sequences].[OrderID])");
            entity.Property(e => e.LastEditedWhen).HasDefaultValueSql("(sysdatetime())");

            entity.HasOne(d => d.BackorderOrder).WithMany(p => p.InverseBackorderOrder).HasConstraintName("FK_Sales_Orders_BackorderOrderID_Sales_Orders");

            entity.HasOne(d => d.ContactPerson).WithMany(p => p.OrderContactPeople)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Sales_Orders_ContactPersonID_Application_People");

            entity.HasOne(d => d.Customer).WithMany(p => p.Orders)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Sales_Orders_CustomerID_Sales_Customers");

            entity.HasOne(d => d.LastEditedByNavigation).WithMany(p => p.OrderLastEditedByNavigations)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Sales_Orders_Application_People");

            entity.HasOne(d => d.PickedByPerson).WithMany(p => p.OrderPickedByPeople).HasConstraintName("FK_Sales_Orders_PickedByPersonID_Application_People");

            entity.HasOne(d => d.SalespersonPerson).WithMany(p => p.OrderSalespersonPeople)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Sales_Orders_SalespersonPersonID_Application_People");
        });

        modelBuilder.Entity<OrderLine>(entity =>
        {
            entity.HasKey(e => e.OrderLineId).HasName("PK_Sales_OrderLines");

            entity.Property(e => e.OrderLineId).HasDefaultValueSql("(NEXT VALUE FOR [Sequences].[OrderLineID])");
            entity.Property(e => e.LastEditedWhen).HasDefaultValueSql("(sysdatetime())");

            entity.HasOne(d => d.LastEditedByNavigation).WithMany(p => p.OrderLines)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Sales_OrderLines_Application_People");

            entity.HasOne(d => d.Order).WithMany(p => p.OrderLines)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Sales_OrderLines_OrderID_Sales_Orders");

            entity.HasOne(d => d.PackageType).WithMany(p => p.OrderLines)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Sales_OrderLines_PackageTypeID_Warehouse_PackageTypes");

            entity.HasOne(d => d.StockItem).WithMany(p => p.OrderLines)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Sales_OrderLines_StockItemID_Warehouse_StockItems");
        });

        modelBuilder.Entity<PackageType>(entity =>
        {
            entity.HasKey(e => e.PackageTypeId).HasName("PK_Warehouse_PackageTypes");

            entity.ToTable(tb => tb.IsTemporal(ttb =>
                    {
                        ttb.UseHistoryTable("PackageTypes_Archive", "Warehouse");
                        ttb
                            .HasPeriodStart("ValidFrom")
                            .HasColumnName("ValidFrom");
                        ttb
                            .HasPeriodEnd("ValidTo")
                            .HasColumnName("ValidTo");
                    }));

            entity.Property(e => e.PackageTypeId).HasDefaultValueSql("(NEXT VALUE FOR [Sequences].[PackageTypeID])");

            entity.HasOne(d => d.LastEditedByNavigation).WithMany(p => p.PackageTypes)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Warehouse_PackageTypes_Application_People");
        });

        modelBuilder.Entity<PaymentMethod>(entity =>
        {
            entity.HasKey(e => e.PaymentMethodId).HasName("PK_Application_PaymentMethods");

            entity.ToTable(tb => tb.IsTemporal(ttb =>
                    {
                        ttb.UseHistoryTable("PaymentMethods_Archive", "Application");
                        ttb
                            .HasPeriodStart("ValidFrom")
                            .HasColumnName("ValidFrom");
                        ttb
                            .HasPeriodEnd("ValidTo")
                            .HasColumnName("ValidTo");
                    }));

            entity.Property(e => e.PaymentMethodId).HasDefaultValueSql("(NEXT VALUE FOR [Sequences].[PaymentMethodID])");

            entity.HasOne(d => d.LastEditedByNavigation).WithMany(p => p.PaymentMethods)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Application_PaymentMethods_Application_People");
        });

        modelBuilder.Entity<Person>(entity =>
        {
            entity.HasKey(e => e.PersonId).HasName("PK_Application_People");

            //entity.ToTable(tb => tb.IsTemporal(ttb =>
            //        {
            //            ttb.UseHistoryTable("People_Archive", "Application");
            //            ttb
            //                .HasPeriodStart("ValidFrom")
            //                .HasColumnName("ValidFrom");
            //            ttb
            //                .HasPeriodEnd("ValidTo")
            //                .HasColumnName("ValidTo");
            //        }));

            entity.Property(e => e.PersonId).HasDefaultValueSql("(NEXT VALUE FOR [Sequences].[PersonID])");
            entity.Property(e => e.OtherLanguages).HasComputedColumnSql("(json_query([CustomFields],N'$.OtherLanguages'))", false);
            entity.Property(e => e.SearchName).HasComputedColumnSql("(concat([PreferredName],N' ',[FullName]))", true);

            entity.HasOne(d => d.LastEditedByNavigation).WithMany(p => p.InverseLastEditedByNavigation)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Application_People_Application_People");
        });

        modelBuilder.Entity<PurchaseOrder>(entity =>
        {
            entity.HasKey(e => e.PurchaseOrderId).HasName("PK_Purchasing_PurchaseOrders");

            entity.Property(e => e.PurchaseOrderId).HasDefaultValueSql("(NEXT VALUE FOR [Sequences].[PurchaseOrderID])");
            entity.Property(e => e.LastEditedWhen).HasDefaultValueSql("(sysdatetime())");

            entity.HasOne(d => d.ContactPerson).WithMany(p => p.PurchaseOrderContactPeople)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Purchasing_PurchaseOrders_ContactPersonID_Application_People");

            entity.HasOne(d => d.DeliveryMethod).WithMany(p => p.PurchaseOrders)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Purchasing_PurchaseOrders_DeliveryMethodID_Application_DeliveryMethods");

            entity.HasOne(d => d.LastEditedByNavigation).WithMany(p => p.PurchaseOrderLastEditedByNavigations)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Purchasing_PurchaseOrders_Application_People");

            entity.HasOne(d => d.Supplier).WithMany(p => p.PurchaseOrders)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Purchasing_PurchaseOrders_SupplierID_Purchasing_Suppliers");
        });

        modelBuilder.Entity<PurchaseOrderLine>(entity =>
        {
            entity.HasKey(e => e.PurchaseOrderLineId).HasName("PK_Purchasing_PurchaseOrderLines");

            entity.Property(e => e.PurchaseOrderLineId).HasDefaultValueSql("(NEXT VALUE FOR [Sequences].[PurchaseOrderLineID])");
            entity.Property(e => e.LastEditedWhen).HasDefaultValueSql("(sysdatetime())");

            entity.HasOne(d => d.LastEditedByNavigation).WithMany(p => p.PurchaseOrderLines)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Purchasing_PurchaseOrderLines_Application_People");

            entity.HasOne(d => d.PackageType).WithMany(p => p.PurchaseOrderLines)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Purchasing_PurchaseOrderLines_PackageTypeID_Warehouse_PackageTypes");

            entity.HasOne(d => d.PurchaseOrder).WithMany(p => p.PurchaseOrderLines)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Purchasing_PurchaseOrderLines_PurchaseOrderID_Purchasing_PurchaseOrders");

            entity.HasOne(d => d.StockItem).WithMany(p => p.PurchaseOrderLines)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Purchasing_PurchaseOrderLines_StockItemID_Warehouse_StockItems");
        });

        modelBuilder.Entity<SpecialDeal>(entity =>
        {
            entity.HasKey(e => e.SpecialDealId).HasName("PK_Sales_SpecialDeals");

            entity.Property(e => e.SpecialDealId).HasDefaultValueSql("(NEXT VALUE FOR [Sequences].[SpecialDealID])");
            entity.Property(e => e.LastEditedWhen).HasDefaultValueSql("(sysdatetime())");

            entity.HasOne(d => d.BuyingGroup).WithMany(p => p.SpecialDeals).HasConstraintName("FK_Sales_SpecialDeals_BuyingGroupID_Sales_BuyingGroups");

            entity.HasOne(d => d.CustomerCategory).WithMany(p => p.SpecialDeals).HasConstraintName("FK_Sales_SpecialDeals_CustomerCategoryID_Sales_CustomerCategories");

            entity.HasOne(d => d.Customer).WithMany(p => p.SpecialDeals).HasConstraintName("FK_Sales_SpecialDeals_CustomerID_Sales_Customers");

            entity.HasOne(d => d.LastEditedByNavigation).WithMany(p => p.SpecialDeals)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Sales_SpecialDeals_Application_People");

            entity.HasOne(d => d.StockGroup).WithMany(p => p.SpecialDeals).HasConstraintName("FK_Sales_SpecialDeals_StockGroupID_Warehouse_StockGroups");

            entity.HasOne(d => d.StockItem).WithMany(p => p.SpecialDeals).HasConstraintName("FK_Sales_SpecialDeals_StockItemID_Warehouse_StockItems");
        });

        modelBuilder.Entity<StateProvince>(entity =>
        {
            entity.HasKey(e => e.StateProvinceId).HasName("PK_Application_StateProvinces");

            entity.ToTable(tb => tb.IsTemporal(ttb =>
                    {
                        ttb.UseHistoryTable("StateProvinces_Archive", "Application");
                        ttb
                            .HasPeriodStart("ValidFrom")
                            .HasColumnName("ValidFrom");
                        ttb
                            .HasPeriodEnd("ValidTo")
                            .HasColumnName("ValidTo");
                    }));

            entity.Property(e => e.StateProvinceId).HasDefaultValueSql("(NEXT VALUE FOR [Sequences].[StateProvinceID])");

            entity.HasOne(d => d.Country).WithMany(p => p.StateProvinces)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Application_StateProvinces_CountryID_Application_Countries");

            entity.HasOne(d => d.LastEditedByNavigation).WithMany(p => p.StateProvinces)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Application_StateProvinces_Application_People");
        });

        modelBuilder.Entity<StockGroup>(entity =>
        {
            entity.HasKey(e => e.StockGroupId).HasName("PK_Warehouse_StockGroups");

            entity.ToTable(tb => tb.IsTemporal(ttb =>
                    {
                        ttb.UseHistoryTable("StockGroups_Archive", "Warehouse");
                        ttb
                            .HasPeriodStart("ValidFrom")
                            .HasColumnName("ValidFrom");
                        ttb
                            .HasPeriodEnd("ValidTo")
                            .HasColumnName("ValidTo");
                    }));

            entity.Property(e => e.StockGroupId).HasDefaultValueSql("(NEXT VALUE FOR [Sequences].[StockGroupID])");

            entity.HasOne(d => d.LastEditedByNavigation).WithMany(p => p.StockGroups)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Warehouse_StockGroups_Application_People");
        });

        modelBuilder.Entity<StockItem>(entity =>
        {
            entity.HasKey(e => e.StockItemId).HasName("PK_Warehouse_StockItems");

            entity.ToTable(tb => tb.IsTemporal(ttb =>
                    {
                        ttb.UseHistoryTable("StockItems_Archive", "Warehouse");
                        ttb
                            .HasPeriodStart("ValidFrom")
                            .HasColumnName("ValidFrom");
                        ttb
                            .HasPeriodEnd("ValidTo")
                            .HasColumnName("ValidTo");
                    }));

            entity.Property(e => e.StockItemId).HasDefaultValueSql("(NEXT VALUE FOR [Sequences].[StockItemID])");
            entity.Property(e => e.SearchDetails).HasComputedColumnSql("(concat([StockItemName],N' ',[MarketingComments]))", false);
            entity.Property(e => e.Tags).HasComputedColumnSql("(json_query([CustomFields],N'$.Tags'))", false);

            entity.HasOne(d => d.Color).WithMany(p => p.StockItems).HasConstraintName("FK_Warehouse_StockItems_ColorID_Warehouse_Colors");

            entity.HasOne(d => d.LastEditedByNavigation).WithMany(p => p.StockItems)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Warehouse_StockItems_Application_People");

            entity.HasOne(d => d.OuterPackage).WithMany(p => p.StockItemOuterPackages)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Warehouse_StockItems_OuterPackageID_Warehouse_PackageTypes");

            entity.HasOne(d => d.Supplier).WithMany(p => p.StockItems)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Warehouse_StockItems_SupplierID_Purchasing_Suppliers");

            entity.HasOne(d => d.UnitPackage).WithMany(p => p.StockItemUnitPackages)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Warehouse_StockItems_UnitPackageID_Warehouse_PackageTypes");
        });

        modelBuilder.Entity<StockItemHolding>(entity =>
        {
            entity.HasKey(e => e.StockItemId).HasName("PK_Warehouse_StockItemHoldings");

            entity.Property(e => e.StockItemId).ValueGeneratedNever();
            entity.Property(e => e.LastEditedWhen).HasDefaultValueSql("(sysdatetime())");

            entity.HasOne(d => d.LastEditedByNavigation).WithMany(p => p.StockItemHoldings)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Warehouse_StockItemHoldings_Application_People");

            entity.HasOne(d => d.StockItem).WithOne(p => p.StockItemHolding)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("PKFK_Warehouse_StockItemHoldings_StockItemID_Warehouse_StockItems");
        });

        modelBuilder.Entity<StockItemStockGroup>(entity =>
        {
            entity.HasKey(e => e.StockItemStockGroupId).HasName("PK_Warehouse_StockItemStockGroups");

            entity.Property(e => e.StockItemStockGroupId).HasDefaultValueSql("(NEXT VALUE FOR [Sequences].[StockItemStockGroupID])");
            entity.Property(e => e.LastEditedWhen).HasDefaultValueSql("(sysdatetime())");

            entity.HasOne(d => d.LastEditedByNavigation).WithMany(p => p.StockItemStockGroups)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Warehouse_StockItemStockGroups_Application_People");

            entity.HasOne(d => d.StockGroup).WithMany(p => p.StockItemStockGroups)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Warehouse_StockItemStockGroups_StockGroupID_Warehouse_StockGroups");

            entity.HasOne(d => d.StockItem).WithMany(p => p.StockItemStockGroups)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Warehouse_StockItemStockGroups_StockItemID_Warehouse_StockItems");
        });

        modelBuilder.Entity<StockItemTransaction>(entity =>
        {
            entity.HasKey(e => e.StockItemTransactionId)
                .HasName("PK_Warehouse_StockItemTransactions")
                .IsClustered(false);

            entity.Property(e => e.StockItemTransactionId).HasDefaultValueSql("(NEXT VALUE FOR [Sequences].[TransactionID])");
            entity.Property(e => e.LastEditedWhen).HasDefaultValueSql("(sysdatetime())");

            entity.HasOne(d => d.Customer).WithMany(p => p.StockItemTransactions).HasConstraintName("FK_Warehouse_StockItemTransactions_CustomerID_Sales_Customers");

            entity.HasOne(d => d.Invoice).WithMany(p => p.StockItemTransactions).HasConstraintName("FK_Warehouse_StockItemTransactions_InvoiceID_Sales_Invoices");

            entity.HasOne(d => d.LastEditedByNavigation).WithMany(p => p.StockItemTransactions)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Warehouse_StockItemTransactions_Application_People");

            entity.HasOne(d => d.PurchaseOrder).WithMany(p => p.StockItemTransactions).HasConstraintName("FK_Warehouse_StockItemTransactions_PurchaseOrderID_Purchasing_PurchaseOrders");

            entity.HasOne(d => d.StockItem).WithMany(p => p.StockItemTransactions)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Warehouse_StockItemTransactions_StockItemID_Warehouse_StockItems");

            entity.HasOne(d => d.Supplier).WithMany(p => p.StockItemTransactions).HasConstraintName("FK_Warehouse_StockItemTransactions_SupplierID_Purchasing_Suppliers");

            entity.HasOne(d => d.TransactionType).WithMany(p => p.StockItemTransactions)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Warehouse_StockItemTransactions_TransactionTypeID_Application_TransactionTypes");
        });

        modelBuilder.Entity<Supplier>(entity =>
        {
            entity.HasKey(e => e.SupplierId).HasName("PK_Purchasing_Suppliers");

            entity.ToTable(tb => tb.IsTemporal(ttb =>
                    {
                        ttb.UseHistoryTable("Suppliers_Archive", "Purchasing");
                        ttb
                            .HasPeriodStart("ValidFrom")
                            .HasColumnName("ValidFrom");
                        ttb
                            .HasPeriodEnd("ValidTo")
                            .HasColumnName("ValidTo");
                    }));

            entity.Property(e => e.SupplierId).HasDefaultValueSql("(NEXT VALUE FOR [Sequences].[SupplierID])");

            entity.HasOne(d => d.AlternateContactPerson).WithMany(p => p.SupplierAlternateContactPeople)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Purchasing_Suppliers_AlternateContactPersonID_Application_People");

            entity.HasOne(d => d.DeliveryCity).WithMany(p => p.SupplierDeliveryCities)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Purchasing_Suppliers_DeliveryCityID_Application_Cities");

            entity.HasOne(d => d.DeliveryMethod).WithMany(p => p.Suppliers).HasConstraintName("FK_Purchasing_Suppliers_DeliveryMethodID_Application_DeliveryMethods");

            entity.HasOne(d => d.LastEditedByNavigation).WithMany(p => p.SupplierLastEditedByNavigations)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Purchasing_Suppliers_Application_People");

            entity.HasOne(d => d.PostalCity).WithMany(p => p.SupplierPostalCities)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Purchasing_Suppliers_PostalCityID_Application_Cities");

            entity.HasOne(d => d.PrimaryContactPerson).WithMany(p => p.SupplierPrimaryContactPeople)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Purchasing_Suppliers_PrimaryContactPersonID_Application_People");

            entity.HasOne(d => d.SupplierCategory).WithMany(p => p.Suppliers)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Purchasing_Suppliers_SupplierCategoryID_Purchasing_SupplierCategories");
        });

        modelBuilder.Entity<SupplierCategory>(entity =>
        {
            entity.HasKey(e => e.SupplierCategoryId).HasName("PK_Purchasing_SupplierCategories");

            entity.ToTable(tb => tb.IsTemporal(ttb =>
                    {
                        ttb.UseHistoryTable("SupplierCategories_Archive", "Purchasing");
                        ttb
                            .HasPeriodStart("ValidFrom")
                            .HasColumnName("ValidFrom");
                        ttb
                            .HasPeriodEnd("ValidTo")
                            .HasColumnName("ValidTo");
                    }));

            entity.Property(e => e.SupplierCategoryId).HasDefaultValueSql("(NEXT VALUE FOR [Sequences].[SupplierCategoryID])");

            entity.HasOne(d => d.LastEditedByNavigation).WithMany(p => p.SupplierCategories)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Purchasing_SupplierCategories_Application_People");
        });

        modelBuilder.Entity<SupplierTransaction>(entity =>
        {
            entity.HasKey(e => e.SupplierTransactionId)
                .HasName("PK_Purchasing_SupplierTransactions")
                .IsClustered(false);

            entity.HasIndex(e => e.TransactionDate, "CX_Purchasing_SupplierTransactions").IsClustered();

            entity.Property(e => e.SupplierTransactionId).HasDefaultValueSql("(NEXT VALUE FOR [Sequences].[TransactionID])");
            entity.Property(e => e.IsFinalized).HasComputedColumnSql("(case when [FinalizationDate] IS NULL then CONVERT([bit],(0)) else CONVERT([bit],(1)) end)", true);
            entity.Property(e => e.LastEditedWhen).HasDefaultValueSql("(sysdatetime())");

            entity.HasOne(d => d.LastEditedByNavigation).WithMany(p => p.SupplierTransactions)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Purchasing_SupplierTransactions_Application_People");

            entity.HasOne(d => d.PaymentMethod).WithMany(p => p.SupplierTransactions).HasConstraintName("FK_Purchasing_SupplierTransactions_PaymentMethodID_Application_PaymentMethods");

            entity.HasOne(d => d.PurchaseOrder).WithMany(p => p.SupplierTransactions).HasConstraintName("FK_Purchasing_SupplierTransactions_PurchaseOrderID_Purchasing_PurchaseOrders");

            entity.HasOne(d => d.Supplier).WithMany(p => p.SupplierTransactions)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Purchasing_SupplierTransactions_SupplierID_Purchasing_Suppliers");

            entity.HasOne(d => d.TransactionType).WithMany(p => p.SupplierTransactions)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Purchasing_SupplierTransactions_TransactionTypeID_Application_TransactionTypes");
        });

        modelBuilder.Entity<SystemParameter>(entity =>
        {
            entity.HasKey(e => e.SystemParameterId).HasName("PK_Application_SystemParameters");

            entity.Property(e => e.SystemParameterId).HasDefaultValueSql("(NEXT VALUE FOR [Sequences].[SystemParameterID])");
            entity.Property(e => e.LastEditedWhen).HasDefaultValueSql("(sysdatetime())");

            entity.HasOne(d => d.DeliveryCity).WithMany(p => p.SystemParameterDeliveryCities)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Application_SystemParameters_DeliveryCityID_Application_Cities");

            entity.HasOne(d => d.LastEditedByNavigation).WithMany(p => p.SystemParameters)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Application_SystemParameters_Application_People");

            entity.HasOne(d => d.PostalCity).WithMany(p => p.SystemParameterPostalCities)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Application_SystemParameters_PostalCityID_Application_Cities");
        });

        modelBuilder.Entity<TransactionType>(entity =>
        {
            entity.HasKey(e => e.TransactionTypeId).HasName("PK_Application_TransactionTypes");

            entity.ToTable(tb => tb.IsTemporal(ttb =>
                    {
                        ttb.UseHistoryTable("TransactionTypes_Archive", "Application");
                        ttb
                            .HasPeriodStart("ValidFrom")
                            .HasColumnName("ValidFrom");
                        ttb
                            .HasPeriodEnd("ValidTo")
                            .HasColumnName("ValidTo");
                    }));

            entity.Property(e => e.TransactionTypeId).HasDefaultValueSql("(NEXT VALUE FOR [Sequences].[TransactionTypeID])");

            entity.HasOne(d => d.LastEditedByNavigation).WithMany(p => p.TransactionTypes)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Application_TransactionTypes_Application_People");
        });

        modelBuilder.Entity<VehicleTemperature>(entity =>
        {
            entity.HasKey(e => e.VehicleTemperatureId)
                .HasName("PK_Warehouse_VehicleTemperatures")
                .IsClustered(false);

            entity.IsMemoryOptimized();

            entity.Property(e => e.FullSensorData).UseCollation("Latin1_General_CI_AS");
            entity.Property(e => e.VehicleRegistration).UseCollation("Latin1_General_CI_AS");
        });
        modelBuilder.HasSequence<int>("BuyingGroupID", "Sequences").StartsAt(3L);
        modelBuilder.HasSequence<int>("CityID", "Sequences").StartsAt(38187L);
        modelBuilder.HasSequence<int>("ColorID", "Sequences").StartsAt(37L);
        modelBuilder.HasSequence<int>("CountryID", "Sequences").StartsAt(242L);
        modelBuilder.HasSequence<int>("CustomerCategoryID", "Sequences").StartsAt(9L);
        modelBuilder.HasSequence<int>("CustomerID", "Sequences").StartsAt(1062L);
        modelBuilder.HasSequence<int>("DeliveryMethodID", "Sequences").StartsAt(11L);
        modelBuilder.HasSequence<int>("InvoiceID", "Sequences").StartsAt(70511L);
        modelBuilder.HasSequence<int>("InvoiceLineID", "Sequences").StartsAt(228266L);
        modelBuilder.HasSequence<int>("OrderID", "Sequences").StartsAt(73596L);
        modelBuilder.HasSequence<int>("OrderLineID", "Sequences").StartsAt(231413L);
        modelBuilder.HasSequence<int>("PackageTypeID", "Sequences").StartsAt(15L);
        modelBuilder.HasSequence<int>("PaymentMethodID", "Sequences").StartsAt(5L);
        modelBuilder.HasSequence<int>("PersonID", "Sequences").StartsAt(3262L);
        modelBuilder.HasSequence<int>("PurchaseOrderID", "Sequences").StartsAt(2075L);
        modelBuilder.HasSequence<int>("PurchaseOrderLineID", "Sequences").StartsAt(8368L);
        modelBuilder.HasSequence<int>("SpecialDealID", "Sequences").StartsAt(3L);
        modelBuilder.HasSequence<int>("StateProvinceID", "Sequences").StartsAt(54L);
        modelBuilder.HasSequence<int>("StockGroupID", "Sequences").StartsAt(11L);
        modelBuilder.HasSequence<int>("StockItemID", "Sequences").StartsAt(228L);
        modelBuilder.HasSequence<int>("StockItemStockGroupID", "Sequences").StartsAt(443L);
        modelBuilder.HasSequence<int>("SupplierCategoryID", "Sequences").StartsAt(10L);
        modelBuilder.HasSequence<int>("SupplierID", "Sequences").StartsAt(14L);
        modelBuilder.HasSequence<int>("SystemParameterID", "Sequences").StartsAt(2L);
        modelBuilder.HasSequence<int>("TransactionID", "Sequences").StartsAt(336253L);
        modelBuilder.HasSequence<int>("TransactionTypeID", "Sequences").StartsAt(14L);

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
