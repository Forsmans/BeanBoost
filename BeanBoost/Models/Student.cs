using MongoDB.Bson;

namespace BeanBoost.Models
{
    public class Student
    {
        public ObjectId Id { get; set; }
        public string Name { get; set; }
        public int Position { get; set; }
        public int TimesBought { get; set; }


    }
}
