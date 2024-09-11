namespace Introduction.Common
{
    public class DogOwnerFilter
    {
        public string SearchQuery { get; set; }
        public Guid Id { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? PhoneNumber { get; set; }
        public string? Email { get; set; }
    }
}