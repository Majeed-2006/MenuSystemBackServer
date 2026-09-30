namespace EFDataAccessLayer.EntityClasses
{
    public partial class OrderStatus
    {
        //Primitive Properties
        public int Id { get; set; }
        public string EngName { get; set; } = null!;
        public string? ArName { get; set; }

        //Navigation Property
        public virtual ICollection<Order> Orders { get; set; } = new List<Order>();
    }
}

