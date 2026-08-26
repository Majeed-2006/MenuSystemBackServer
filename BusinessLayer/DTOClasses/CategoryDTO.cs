namespace App.API.DTOClasses
{
    public class CategoryDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Discription { get; set; }
        public bool IsAvilable{ get; set; }
        public int PrepDuration { get; set; }

        public CategoryDTO(int id, string name,string discription, bool isAvilable,int prepDuration)    
        {
            Id = id;
            Name = name;
            Discription = discription;
            IsAvilable = isAvilable;   
            PrepDuration = prepDuration;   
        }
    }
}
