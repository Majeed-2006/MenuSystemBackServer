namespace EFDataAccessLayer.EntityClasses
{
    public partial class User
    {
        //Primitive Properties //Primitive Properties
        public int Id { get; set; } 
        public string FirstName { get; set; } = null!;
        public string LastName { get; set; } = null!;
        public string UserName { get; set; } = null!;
        public string Password { get; set; } = null!;
        public string Email { get; set; } = null!;
        public string Phone { get; set; } = null!;
        public string Address { get; set; } = null!;

        //Foreign Keys
        public int RestaurantId { get; set; }
        public int? ManagerId { get; set; }
        public int RoleId { get; set; }
        //Navigation Property 
        public virtual Restaurant Restaurant { get; set; } = null!;
        public virtual User? Manager { get; set; }
        public virtual Role Role { get; set; } = null!;
        public virtual ICollection<Product> Products { get; set; } = new List<Product>();
        public virtual ICollection<Category> Categories { get; set; } = new List<Category>();
        public virtual ICollection<User> Users { get; set; } = new List<User>();
        public virtual ICollection<Order> CashierOrders { get; set; } = new List<Order>();
        public virtual ICollection<Order> WaiterOrders { get; set; } = new List<Order>();
        public virtual ICollection<Order> DriverOrders { get; set; } = new List<Order>();
    }
}
