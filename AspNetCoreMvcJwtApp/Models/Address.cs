namespace AspNetCoreMvcJwtApp.Models
{
    public class Address
    {
        public int Id { get; set; }
        public string Location { get; set; }

        public int PersonId { get; set; }
        public Person Person { get; set; }
    }
}
