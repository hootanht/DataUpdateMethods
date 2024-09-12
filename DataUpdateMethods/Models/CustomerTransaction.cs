﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
#nullable disable
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace DataUpdateMethods.Models;

/// <summary>
/// Represents a customer transaction in the sales schema.
/// </summary>
[Table("CustomerTransactions", Schema = "Sales")]
[Index("TransactionDate", "CustomerId", Name = "FK_Sales_CustomerTransactions_CustomerID")]
[Index("TransactionDate", "InvoiceId", Name = "FK_Sales_CustomerTransactions_InvoiceID")]
[Index("TransactionDate", "PaymentMethodId", Name = "FK_Sales_CustomerTransactions_PaymentMethodID")]
[Index("TransactionDate", "TransactionTypeId", Name = "FK_Sales_CustomerTransactions_TransactionTypeID")]
[Index("TransactionDate", "IsFinalized", Name = "IX_Sales_CustomerTransactions_IsFinalized")]
public partial class CustomerTransaction
{
    [Key]
    [Column("CustomerTransactionID")]
    public int CustomerTransactionId { get; set; }

    [Column("CustomerID")]
    public int CustomerId { get; set; }

    [Column("TransactionTypeID")]
    public int TransactionTypeId { get; set; }

    [Column("InvoiceID")]
    public int? InvoiceId { get; set; }

    [Column("PaymentMethodID")]
    public int? PaymentMethodId { get; set; }

    [Column(TypeName = "date")]
    public DateTime TransactionDate { get; set; }

    [Column(TypeName = "decimal(18, 2)")]
    public decimal AmountExcludingTax { get; set; }

    [Column(TypeName = "decimal(18, 2)")]
    public decimal TaxAmount { get; set; }

    [Column(TypeName = "decimal(18, 2)")]
    public decimal TransactionAmount { get; set; }

    [Column(TypeName = "decimal(18, 2)")]
    public decimal OutstandingBalance { get; set; }

    [Column(TypeName = "date")]
    public DateTime? FinalizationDate { get; set; }

    public bool? IsFinalized { get; set; }

    public int LastEditedBy { get; set; }

    public DateTime LastEditedWhen { get; set; }

    [ForeignKey("CustomerId")]
    [InverseProperty("CustomerTransactions")]
    public virtual Customer Customer { get; set; }

    [ForeignKey("InvoiceId")]
    [InverseProperty("CustomerTransactions")]
    public virtual Invoice Invoice { get; set; }

    [ForeignKey("LastEditedBy")]
    [InverseProperty("CustomerTransactions")]
    public virtual Person LastEditedByNavigation { get; set; }

    [ForeignKey("PaymentMethodId")]
    [InverseProperty("CustomerTransactions")]
    public virtual PaymentMethod PaymentMethod { get; set; }

    [ForeignKey("TransactionTypeId")]
    [InverseProperty("CustomerTransactions")]
    public virtual TransactionType TransactionType { get; set; }
}
