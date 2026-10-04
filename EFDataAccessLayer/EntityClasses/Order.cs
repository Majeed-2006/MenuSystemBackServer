namespace EFDataAccessLayer.EntityClasses
{
    public partial class Order
    {
        //Primitive Properties
        public int Id { get; set; }
        public double TotalPrice { get; set; }
        public DateTime OrderDateTime { get; set; }
        public int OrderType { get; set; }
        public int PaymentStatus { get; set; }

        //Foreign Keys
        public int CashierId { get; set; }
        public int? WaiterId { get; set; }   
        public int? DriverId { get; set; }
        public int OrderStatusId { get; set; }
        public int CustomerId { get; set; }
        public int RestaurantId { get; set; }


        //Navigation Property
        public virtual Restaurant Restaurant { get; set; } = null!;
        public virtual Customer Customer { get; set; } = null!;
        public virtual ICollection<OrderItem> OrderItems { get; set; } = new List<OrderItem>();
        public virtual OrderStatus OrderStatus { get; set; } = null!;
        public virtual User Cashier { get; set; } = null!;
        public virtual User? Waiter { get; set; }
        public virtual User? Driver { get; set; }
    }
}
