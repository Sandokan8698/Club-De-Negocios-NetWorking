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
    public class PedidoService : BaseService<Pedido>, IPedidoService
    {

        public PedidoService(IUnitOfWork unitOfWork): base(unitOfWork, unitOfWork.PedidoRepositoy)
        {

        }

       
        public IEnumerable<Pedido> GetPedidosByVentaId(int ventaId)
        {
            return UnitOfWork.PedidoRepositoy.Find(p => p.VentaId == ventaId);
        }
    }
}
