﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
#nullable disable
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace DataUpdateMethods.Models;

[Table("CustomerCategories", Schema = "Sales")]
[Index("CustomerCategoryName", Name = "UQ_Sales_CustomerCategories_CustomerCategoryName", IsUnique = true)]
public partial class CustomerCategory
{
    [Key]
    [Column("CustomerCategoryID")]
    public int CustomerCategoryId { get; set; }

    [Required]
    [StringLength(50)]
    public string CustomerCategoryName { get; set; }

    public int LastEditedBy { get; set; }

    [InverseProperty("CustomerCategory")]
    public virtual ICollection<Customer> Customers { get; set; } = new List<Customer>();

    [ForeignKey("LastEditedBy")]
    [InverseProperty("CustomerCategories")]
    public virtual Person LastEditedByNavigation { get; set; }

    [InverseProperty("CustomerCategory")]
    public virtual ICollection<SpecialDeal> SpecialDeals { get; set; } = new List<SpecialDeal>();
}