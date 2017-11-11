using AutoMapper;
using Data_Layer.Implementations;
using Domain_Layer.Entities;


namespace WPFPresentation.Models.Provider
{
    public class TrabajadorProvider : BaseProvider<TrabajadorModel, Trabajador>
    {
        public TrabajadorProvider(UnitOfWork unitOfWork) : base(unitOfWork,unitOfWork.TrabajadorRepository)
        {

        }

        public TrabajadorModel Auhtenticate(string username, string password)
        {
            using (UnitOfWork)
            {
                var trabajador = UnitOfWork.TrabajadorRepository.Auhtenticate(username, password);
                return Mapper.Map<Trabajador, TrabajadorModel>(trabajador);
            }
          
        }
    }

}
