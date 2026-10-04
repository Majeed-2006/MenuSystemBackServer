namespace EFDataAccessLayer.EntityClasses
{
    public partial class OrderItem
    {
        //Primitive Properties
        public int Id { get; set; }
        public int Quantity { get; set; }
        public double UnitPrice { get; set; }

        //Foreign Keys
        public int OrderId { get; set; }
        public int ProductId { get; set; }

        //Navigation Property
        public virtual Order Order { get; set; } = null!;
        public virtual Product Product{ get; set; } = null!;

    }
}
