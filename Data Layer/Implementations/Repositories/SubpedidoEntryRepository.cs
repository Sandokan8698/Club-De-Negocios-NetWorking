using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Core.Objects;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Data_Layer.Abstract.Repositories;
using Domain_Layer.Entities;

namespace Data_Layer.Implementations.Repositories
{
    public class SubpedidoEntryRepository : DomainContextRepository<SubPedidoEntry>, ISubPedidoEntryRepository
    {
        public SubpedidoEntryRepository(ClubNegociosNetworkingContext context) : base(context)
        {
            
        }

        public IEnumerable<SubPedidoEntry> GetDailyReportPayment(Venta venta)
        {
            return CnnContext.SubPedidoEntriesDbSet.Where(sbe => sbe.FechaCreacion == DateTime.Today && sbe.SubPedido.Pedido.VentaId == venta.VentaId && sbe.Abono > 0)
                .Include(sbe => sbe.SubPedido).AsNoTracking().ToList();
        }

        public override void Remove(SubPedidoEntry entity)
        {
            var removeEntity = CnnContext.SubPedidoEntriesDbSet.First(sbe => sbe.SubPedidoEntryId == entity.SubPedidoEntryId);
            CnnContext.SubPedidoEntriesDbSet.Remove(removeEntity);
        }
    }
}
