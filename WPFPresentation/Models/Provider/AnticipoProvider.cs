using Domain_Layer.Entities;
using Data_Layer.Implementations;

namespace WPFPresentation.Models.Provider
{
    public class AnticipoProvider : BaseProvider<AnticipoModel, Anticipo>
    {

        public AnticipoProvider(UnitOfWork unitOfWork) : base(unitOfWork)
        {

        }
        public override void CreateBaseRepository()
        {
            _baseRepository = UnitOfWork.AnticipoRepository;
        }
    }
}
