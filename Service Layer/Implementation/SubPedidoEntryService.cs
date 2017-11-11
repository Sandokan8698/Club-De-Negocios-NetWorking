using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Data_Layer.Abstract;
using Domain_Layer.Entities;
using Service_Layer.Abstract;

namespace Service_Layer.Implementation
{
    public class SubPedidoEntryService : BaseService<SubPedidoEntry>, ISubpedidoEntryService
    {
        public SubPedidoEntryService(IUnitOfWork unitOfWork) : base(unitOfWork, unitOfWork.SubPedidoEntryRepository)
        {
        }

    }
}
