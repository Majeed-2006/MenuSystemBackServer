namespace BusinessLayer.DTOClasses.Categories
{
    public class CategorySaveDTO
    {
        public string Name { get; set; }
        public string Discription { get; set; }
        public bool IsAvilable { get; set; }
        public int PrepDuration { get; set; }
        public int RestaurantId { get; set; }
        public int CreatedByUserId { get; set; }
        public CategorySaveDTO(string name, string discription, bool isAvilable, int prepDuration,
                               int restaurantId,int createdByUserId)
        {
            Name = name;
            Discription = discription;
            IsAvilable = isAvilable;
            PrepDuration = prepDuration;
            RestaurantId = restaurantId; ;
            CreatedByUserId = createdByUserId;  
        }
    }
}
