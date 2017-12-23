using Data_Layer.Abstract;
using Data_Layer.Abstract.Repositories;
using Domain_Layer.Entities;

namespace Data_Layer.Implementations.Repositories
{
    public class AnticipoRepository : DomainContextRepository<Anticipo>, IAnticipoRepository
    {
        public AnticipoRepository(ClubNegociosNetworkingContext context) : base(context)
        {
        }
    }
}
