using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Data_Layer.Abstract;
using Data_Layer.Abstract.Repositories;
using Data_Layer.Utils.ServiceFilter;
using Domain_Layer.Entities;

namespace Service_Layer.Implementation
{
    public class SubPedidoService : BaseService<SubPedido>, ISubPedidoService
    {
        public SubPedidoService(IUnitOfWork unitOfWork) : base(unitOfWork, unitOfWork.SubPedidoRepository)
        {
        }

        public IEnumerable<SubPedido> ApplayFilter(FilterEntitie filterEntitie)
        {
           
                return UnitOfWork.SubPedidoRepository.ApplayFilter(filterEntitie);
            
           
        }

        public override IEnumerable<SubPedido> GetAll()
        {
            return UnitOfWork.SubPedidoRepository.GetAll();
        }

        public IEnumerable<SubPedido> PaginateFiltered(int page, int pageSize, FilterEntitie filterEntitie)
        {
            
                return UnitOfWork.SubPedidoRepository.PaginateFiltered(page, pageSize, filterEntitie);
           
            
        }
    }
}
