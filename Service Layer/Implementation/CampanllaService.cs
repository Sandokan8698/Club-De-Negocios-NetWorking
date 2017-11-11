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
    public class CampanllaService: BaseService<Campanlla>, ICampanllaService
    {
        public CampanllaService(IUnitOfWork unitOfWork): base(unitOfWork, unitOfWork.CampanllaRepository)
        {

        }

        public IEnumerable<Campanlla> GetCampanllasByProveedorId(int proveedorId)
        {
          return  UnitOfWork.CampanllaRepository.Find(c => c.ProveedorId == proveedorId);
        }

    }
}
