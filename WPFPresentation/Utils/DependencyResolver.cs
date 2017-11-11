using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Data_Layer;
using Data_Layer.Abstract;
using Data_Layer.Implementations;
using Service_Layer.Implementation;
using WPFPresentation.Models.Provider;

namespace WPFPresentation.Utils
{
    public sealed class DependencyResolver
    {
        #region Data
        static readonly DependencyResolver instance = new DependencyResolver();
        private IUnitOfWork unitOfWork;
        private FacadeService facadeService;
       

        #endregion

        #region Ctor

        //CTORs
        static DependencyResolver()
        {

        }

        private DependencyResolver()
        {
            unitOfWork = new UnitOfWork(new ClubNegociosNetworkingContext());
            facadeService = new FacadeService(unitOfWork);
            FacadeProvider = new FacadeProvider(facadeService);
        }
        #endregion

        #region Public Properties
        /// <summary>
        /// La instancia del singleton
        /// </summary>
        public static DependencyResolver Instance
        {
            get { return instance; }
        }

        public FacadeProvider FacadeProvider { get;}

        #endregion
    }
}
