namespace EFDataAccessLayer.EntityClasses
{
   public partial class Product
    {
        //Primitive Properties
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public string Description { get; set; } = null!;
        public float Price { get; set; }
        public bool IsAvailable { get; set; }

        //Foreign Keys
        public int CreatedByUserId { get; set; }    
        public int CategoryId { get; set; }
        public int RestaurantId { get; set; }

        //Navigation Property
        public virtual Restaurant Restaurant { get; set; } = null!;
        public virtual User CreatedByUser { get; set; } = null!;
        public virtual Category Category { get; set; } = null!;
        public virtual ICollection<OrderItem> OrdersItems { get; set; } = new List<OrderItem>();

    }
}
