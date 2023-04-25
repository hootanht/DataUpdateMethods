﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
#nullable disable
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace DataUpdateMethods.Models;

[Table("Orders", Schema = "Sales")]
[Index("ContactPersonId", Name = "FK_Sales_Orders_ContactPersonID")]
[Index("CustomerId", Name = "FK_Sales_Orders_CustomerID")]
[Index("PickedByPersonId", Name = "FK_Sales_Orders_PickedByPersonID")]
[Index("SalespersonPersonId", Name = "FK_Sales_Orders_SalespersonPersonID")]
public partial class Order
{
    [Key]
    [Column("OrderID")]
    public int OrderId { get; set; }

    [Column("CustomerID")]
    public int CustomerId { get; set; }

    [Column("SalespersonPersonID")]
    public int SalespersonPersonId { get; set; }

    [Column("PickedByPersonID")]
    public int? PickedByPersonId { get; set; }

    [Column("ContactPersonID")]
    public int ContactPersonId { get; set; }

    [Column("BackorderOrderID")]
    public int? BackorderOrderId { get; set; }

    [Column(TypeName = "date")]
    public DateTime OrderDate { get; set; }

    [Column(TypeName = "date")]
    public DateTime ExpectedDeliveryDate { get; set; }

    [StringLength(20)]
    public string CustomerPurchaseOrderNumber { get; set; }

    public bool IsUndersupplyBackordered { get; set; }

    public string Comments { get; set; }

    public string DeliveryInstructions { get; set; }

    public string InternalComments { get; set; }

    public DateTime? PickingCompletedWhen { get; set; }

    public int LastEditedBy { get; set; }

    public DateTime LastEditedWhen { get; set; }

    [ForeignKey("BackorderOrderId")]
    [InverseProperty("InverseBackorderOrder")]
    public virtual Order BackorderOrder { get; set; }

    [ForeignKey("ContactPersonId")]
    [InverseProperty("OrderContactPeople")]
    public virtual Person ContactPerson { get; set; }

    [ForeignKey("CustomerId")]
    [InverseProperty("Orders")]
    public virtual Customer Customer { get; set; }

    [InverseProperty("BackorderOrder")]
    public virtual ICollection<Order> InverseBackorderOrder { get; set; } = new List<Order>();

    [InverseProperty("Order")]
    public virtual ICollection<Invoice> Invoices { get; set; } = new List<Invoice>();

    [ForeignKey("LastEditedBy")]
    [InverseProperty("OrderLastEditedByNavigations")]
    public virtual Person LastEditedByNavigation { get; set; }

    [InverseProperty("Order")]
    public virtual ICollection<OrderLine> OrderLines { get; set; } = new List<OrderLine>();

    [ForeignKey("PickedByPersonId")]
    [InverseProperty("OrderPickedByPeople")]
    public virtual Person PickedByPerson { get; set; }

    [ForeignKey("SalespersonPersonId")]
    [InverseProperty("OrderSalespersonPeople")]
    public virtual Person SalespersonPerson { get; set; }
}