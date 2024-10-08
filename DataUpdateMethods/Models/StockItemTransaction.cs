﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
#nullable disable
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace DataUpdateMethods.Models;

/// <summary>
/// Represents a stock item transaction in the warehouse schema.
/// </summary>
[Table("StockItemTransactions", Schema = "Warehouse")]
[Index("CustomerId", Name = "FK_Warehouse_StockItemTransactions_CustomerID")]
[Index("InvoiceId", Name = "FK_Warehouse_StockItemTransactions_InvoiceID")]
[Index("PurchaseOrderId", Name = "FK_Warehouse_StockItemTransactions_PurchaseOrderID")]
[Index("StockItemId", Name = "FK_Warehouse_StockItemTransactions_StockItemID")]
[Index("SupplierId", Name = "FK_Warehouse_StockItemTransactions_SupplierID")]
[Index("TransactionTypeId", Name = "FK_Warehouse_StockItemTransactions_TransactionTypeID")]
public partial class StockItemTransaction
{
    [Key]
    [Column("StockItemTransactionID")]
    public int StockItemTransactionId { get; set; }

    [Column("StockItemID")]
    public int StockItemId { get; set; }

    [Column("TransactionTypeID")]
    public int TransactionTypeId { get; set; }

    [Column("CustomerID")]
    public int? CustomerId { get; set; }

    [Column("InvoiceID")]
    public int? InvoiceId { get; set; }

    [Column("SupplierID")]
    public int? SupplierId { get; set; }

    [Column("PurchaseOrderID")]
    public int? PurchaseOrderId { get; set; }

    public DateTime TransactionOccurredWhen { get; set; }

    [Column(TypeName = "decimal(18, 3)")]
    public decimal Quantity { get; set; }

    public int LastEditedBy { get; set; }

    public DateTime LastEditedWhen { get; set; }

    [ForeignKey("CustomerId")]
    [InverseProperty("StockItemTransactions")]
    public virtual Customer Customer { get; set; }

    [ForeignKey("InvoiceId")]
    [InverseProperty("StockItemTransactions")]
    public virtual Invoice Invoice { get; set; }

    [ForeignKey("LastEditedBy")]
    [InverseProperty("StockItemTransactions")]
    public virtual Person LastEditedByNavigation { get; set; }

    [ForeignKey("PurchaseOrderId")]
    [InverseProperty("StockItemTransactions")]
    public virtual PurchaseOrder PurchaseOrder { get; set; }

    [ForeignKey("StockItemId")]
    [InverseProperty("StockItemTransactions")]
    public virtual StockItem StockItem { get; set; }

    [ForeignKey("SupplierId")]
    [InverseProperty("StockItemTransactions")]
    public virtual Supplier Supplier { get; set; }

    [ForeignKey("TransactionTypeId")]
    [InverseProperty("StockItemTransactions")]
    public virtual TransactionType TransactionType { get; set; }
}
