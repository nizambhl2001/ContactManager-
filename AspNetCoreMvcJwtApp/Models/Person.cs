using System.Net;
using System.Numerics;

namespace AspNetCoreMvcJwtApp.Models
{
    public class Person
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public List<Phone> Phones { get; set; }
        public List<Address> Addresses { get; set; }
    }
}
