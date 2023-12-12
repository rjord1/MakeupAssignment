namespace api.Models
{
    public class Cars
    {
        public int CarID {get; set;}
        public string Make {get; set;}
        public string Model {get; set;}
        public int Mileage {get; set;}
        public string Date {get; set;}
        public bool Hold {get; set;}
        public bool Deleted {get; set;}
    }
}