using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Data_Layer;
using Data_Layer.Abstract;
using Data_Layer.Implementations;

namespace Service_Layer.Implementation
{
    public class FacadeService
    {
        private ClienteService _clienteService;
        private PedidoService _pedidoService;
        private ProveedorService _proveedorService;
        private VentaService _ventaService;
        private CampanllaService _campanllaService;
        private SubPedidoEntryService _subPedidoEntryService;
        private SubPedidoService _subPedidoService;
        private TrabajadorService _trabajadorService;

        public FacadeService(IUnitOfWork unitOfWork)
        {
            _clienteService = new ClienteService(unitOfWork);
            _pedidoService = new PedidoService(unitOfWork);
            _proveedorService = new ProveedorService(unitOfWork);
            _ventaService = new VentaService(unitOfWork);
            _campanllaService = new CampanllaService(unitOfWork);
            _subPedidoEntryService = new SubPedidoEntryService(unitOfWork);
            _subPedidoService = new SubPedidoService(unitOfWork);
            _trabajadorService = new TrabajadorService(unitOfWork);
        }

        public FacadeService()
        {
            
        }

        public ClienteService GetClienteService()
        {
            return new ClienteService(Resolve());
        }

        public PedidoService GetPedidoService()
        {
            return new PedidoService(Resolve());
        }

        public ProveedorService GEtProveedorService()
        {
            return new ProveedorService(Resolve());
        }

        public VentaService GeVentaService()
        {
            return new VentaService(Resolve());
        }

        public CampanllaService GetCampanllaService()
        {
            return new CampanllaService(Resolve());
        }

        public SubPedidoEntryService GetSubPedidoEntryService()
        {
            return new SubPedidoEntryService(Resolve());
        }

        public SubPedidoService GetSubPedidoService()
        {
            return new SubPedidoService(Resolve());
        }

        public TrabajadorService GeTrabajadorService()
        {
            return new TrabajadorService(Resolve());
        }

        private UnitOfWork Resolve()
        {
            return new UnitOfWork(new ClubNegociosNetworkingContext());
        }
    }
}
