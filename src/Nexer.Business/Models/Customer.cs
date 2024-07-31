
namespace Nexer.Business.Models
{
    public class Customer : Entity
    {
        public string? Name { get; set; }
        public string? Email { get; set; }
        public string? Address { get; set; }

        public IEnumerable<Product> Products { get; set; }
    }
}
