using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Data_Layer.Abstract;
using Data_Layer.Abstract.Repositories;
using Domain_Layer.Entities;

namespace Service_Layer.Implementation
{
    public class TrabajadorService : BaseService<Trabajador>
    {
        public TrabajadorService(IUnitOfWork unitOfWork) : base(unitOfWork, unitOfWork.TrabajadorRepository)
        {
        }

        public Trabajador Auhtenticate(string username, string password)
        {
            return UnitOfWork.TrabajadorRepository.Find(t => t.Usuario == username && t.Password == password).FirstOrDefault();

        }
    }
}
