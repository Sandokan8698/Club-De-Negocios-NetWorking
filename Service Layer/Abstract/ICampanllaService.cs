
using System.Collections.Generic;
using Domain_Layer.Entities;

namespace Service_Layer.Abstract
{
    public interface ICampanllaService: IService<Campanlla>
    {
        IEnumerable<Campanlla> GetCampanllasByProveedorId(int proveedorId);
    }
}
