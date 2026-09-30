namespace EFDataAccessLayer.EntityClasses
{
    public partial class Category
    {
        //Primitive Properties
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public string Description { get; set; } = null!;
        public bool IsAvailable{ get; set; }
        public int PrepDuration { get; set; }

        //Foreign Keys
        public int RestaurantId { get; set; }
        public int CreatedByUserId { get; set; }

        //Navigation Property
        public virtual Restaurant Restaurant { get; set; } = null!;
        public virtual ICollection<Product> Products { get; set; } = new List<Product>();
        public virtual User CreatedByUser { get; set; } = null!;
    }
}
