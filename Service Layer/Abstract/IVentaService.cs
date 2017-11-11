using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain_Layer.Entities;

namespace Service_Layer.Abstract
{
    public interface IVentaService: IService<Venta>
    {
        IEnumerable<Venta> GetClientVentas(Cliente cliente);
    }
}
