namespace Introduction.WebAPI
{
    public class Dog
    {

        public Guid Id { get; set; }
        public Guid DogOwnerId { get; set; }
        public string? Name { get; set; }
        public DateTime BirthDate { get; set; }
        public int Age { get; set; }
        public string? FurColor { get; set; }
        public string? Breed { get; set; }
        public bool? IsTrained { get; set; }
        public DogOwner? DogOwner { get; set; }
    }
}
