
namespace Nexer.Business.Models
{
    public class Product : Entity
    {
        public Guid CustomerId { get; set; }
        public string? Name { get; set; }
        public string? Description { get; set; }
        //public int Quantity { get; set; }
        //public double UnitPrice { get; set; }

        /* Ef Core relacao */
        public Customer Customer { get; set; }
    }
}
