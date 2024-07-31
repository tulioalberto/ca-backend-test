
namespace Nexer.Business.Models
{
    public abstract class Entity
    {
        //id novo será sempre gerado
        protected Entity()
        {
            Id = Guid.NewGuid();
        }

        public Guid Id { get; set; }
    }
}
