using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Data_Layer;
using Data_Layer.Implementations;
using Service_Layer.Implementation;

namespace WPFPresentation.Models.Provider
{
    public class FacadeProvider
    {
        private ClienteProvider _clienteProvider;
        private PedidoProvider _pedidoProvider;
        private ProveedorProvider _proveedorProvider;
        private VentaProvider _ventaProvider;
        private CampanllaProvider _campanllaProvider;
        private SubPedidoProvider _subPedidoProvider;
        private SubPedidoEntryProvider _subPedidoEntryProvider;
        private TrabajadorProvider _trabajadorProvider;

        private FacadeService _facadeService;

        public FacadeProvider(FacadeService facadeService)
        {
            //_clienteProvider = new ClienteProvider(facadeService);
            //_pedidoProvider = new PedidoProvider(facadeService);
            //_proveedorProvider = new ProveedorProvider(facadeService);
            //_ventaProvider = new VentaProvider(facadeService);
            //_campanllaProvider = new CampanllaProvider(facadeService);
            //_subPedidoProvider = new SubPedidoProvider(facadeService);
            //_subPedidoEntryProvider = new SubPedidoEntryProvider(facadeService);
            //_trabajadorProvider = new TrabajadorProvider(facadeService);

            //_facadeService = facadeService;

        }


        public ClienteProvider ClienteProvider()
        {
            return new ClienteProvider(new UnitOfWork(new ClubNegociosNetworkingContext()));
        }

        public PedidoProvider PedidoProvider()
        {
            return new PedidoProvider(new UnitOfWork(new ClubNegociosNetworkingContext()));
        }

        public ProveedorProvider ProveedorProvider()
        {
            return new ProveedorProvider(new UnitOfWork(new ClubNegociosNetworkingContext()));
        }

        public VentaProvider VentaProvider()
        {
            return new VentaProvider(new UnitOfWork(new ClubNegociosNetworkingContext()));
        }

        public CampanllaProvider CampanllaProvider()
        {
            return new CampanllaProvider(new UnitOfWork(new ClubNegociosNetworkingContext()));
        }

        public SubPedidoProvider SubPedidoProvider()
        {
            return new SubPedidoProvider(new UnitOfWork(new ClubNegociosNetworkingContext()));
        }

        public SubPedidoEntryProvider SubPedidoEntryProvider()
        {
            return new SubPedidoEntryProvider(new UnitOfWork(new ClubNegociosNetworkingContext()));
        }

        public TrabajadorProvider TrabajadorProvider()
        {
            return new TrabajadorProvider(new UnitOfWork(new ClubNegociosNetworkingContext()));
        }
    }
}
