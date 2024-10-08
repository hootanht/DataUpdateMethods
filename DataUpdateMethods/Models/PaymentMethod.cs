﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
#nullable disable
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace DataUpdateMethods.Models;

/// <summary>
/// Represents a payment method in the application schema.
/// </summary>
[Table("PaymentMethods", Schema = "Application")]
[Index("PaymentMethodName", Name = "UQ_Application_PaymentMethods_PaymentMethodName", IsUnique = true)]
public partial class PaymentMethod
{
    [Key]
    [Column("PaymentMethodID")]
    public int PaymentMethodId { get; set; }

    [Required]
    [StringLength(50)]
    public string PaymentMethodName { get; set; }

    public int LastEditedBy { get; set; }

    [InverseProperty("PaymentMethod")]
    public virtual ICollection<CustomerTransaction> CustomerTransactions { get; set; } = new List<CustomerTransaction>();

    [ForeignKey("LastEditedBy")]
    [InverseProperty("PaymentMethods")]
    public virtual Person LastEditedByNavigation { get; set; }

    [InverseProperty("PaymentMethod")]
    public virtual ICollection<SupplierTransaction> SupplierTransactions { get; set; } = new List<SupplierTransaction>();
}
