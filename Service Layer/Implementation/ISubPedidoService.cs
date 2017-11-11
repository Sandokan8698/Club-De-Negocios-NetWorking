using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Data_Layer.Utils.ServiceFilter;
using Domain_Layer.Entities;
using Service_Layer.Abstract;

namespace Service_Layer.Implementation
{
    public interface ISubPedidoService: IService<SubPedido>
    {
        IEnumerable<SubPedido> ApplayFilter(FilterEntitie filterEntitie);
    }
}
