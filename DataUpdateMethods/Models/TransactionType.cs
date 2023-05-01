﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
#nullable disable
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace DataUpdateMethods.Models;

[Table("TransactionTypes", Schema = "Application")]
[Index("TransactionTypeName", Name = "UQ_Application_TransactionTypes_TransactionTypeName", IsUnique = true)]
public partial class TransactionType
{
    [Key]
    [Column("TransactionTypeID")]
    public int TransactionTypeId { get; set; }

    [Required]
    [StringLength(50)]
    public string TransactionTypeName { get; set; }

    public int LastEditedBy { get; set; }

    [InverseProperty("TransactionType")]
    public virtual ICollection<CustomerTransaction> CustomerTransactions { get; set; } = new List<CustomerTransaction>();

    [ForeignKey("LastEditedBy")]
    [InverseProperty("TransactionTypes")]
    public virtual Person LastEditedByNavigation { get; set; }

    [InverseProperty("TransactionType")]
    public virtual ICollection<StockItemTransaction> StockItemTransactions { get; set; } = new List<StockItemTransaction>();

    [InverseProperty("TransactionType")]
    public virtual ICollection<SupplierTransaction> SupplierTransactions { get; set; } = new List<SupplierTransaction>();
}