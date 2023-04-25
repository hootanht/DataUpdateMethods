﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
#nullable disable
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace DataUpdateMethods.Models;

[Table("StockItemHoldings", Schema = "Warehouse")]
public partial class StockItemHolding
{
    [Key]
    [Column("StockItemID")]
    public int StockItemId { get; set; }

    public int QuantityOnHand { get; set; }

    [Required]
    [StringLength(20)]
    public string BinLocation { get; set; }

    public int LastStocktakeQuantity { get; set; }

    [Column(TypeName = "decimal(18, 2)")]
    public decimal LastCostPrice { get; set; }

    public int ReorderLevel { get; set; }

    public int TargetStockLevel { get; set; }

    public int LastEditedBy { get; set; }

    public DateTime LastEditedWhen { get; set; }

    [ForeignKey("LastEditedBy")]
    [InverseProperty("StockItemHoldings")]
    public virtual Person LastEditedByNavigation { get; set; }

    [ForeignKey("StockItemId")]
    [InverseProperty("StockItemHolding")]
    public virtual StockItem StockItem { get; set; }
}