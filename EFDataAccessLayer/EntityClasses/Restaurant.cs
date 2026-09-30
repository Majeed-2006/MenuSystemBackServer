namespace EFDataAccessLayer.EntityClasses
{
    public partial class Restaurant
    {
        //Primitive Properties 
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public string subDomain { get; set; } = null!;
        public DateTime CreatedAt { get; set; }
        public bool IsDeleted { get; set; } = false;


        //Navigation Property 
        public virtual ICollection<Product> Products { get; set; } = new List<Product>();
        public virtual ICollection<Customer> Customers { get; set; } = new List<Customer>();
        public virtual ICollection<Category> Categories { get; set; } = new List<Category>();
        public virtual ICollection<User> Users { get; set; } = new List<User>();
        public virtual ICollection<Order> Orders { get; set; } = new List<Order>();
    }

}
