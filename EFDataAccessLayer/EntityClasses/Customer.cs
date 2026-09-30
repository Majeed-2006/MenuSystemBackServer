namespace EFDataAccessLayer.EntityClasses
{
    public partial class Customer
    {
        //Primitive Properties
        public int Id { get; set; }
        public string FirstName { get; set; } = null!;
        public string LastName { get; set; } = null!;
        public string Phone { get; set; } = null!;
        public int NumberOfOrders { get; set; }
        public string? Email { get; set; } = null;
        public DateTime LastOrderDate { get; set; }

        //Foreign Keys
        public int RestaurantId { get; set; }

        //Navigation Property
        public virtual Restaurant Restaurant { get; set; } = null!;
        public virtual ICollection<Order> Orders { get; set; } = new List<Order>();
    }
}
