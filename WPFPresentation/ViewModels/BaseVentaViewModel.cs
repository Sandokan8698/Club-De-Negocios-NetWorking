using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Input;
using FirstFloor.ModernUI.Windows.Controls;
using WPFPresentation.Models;
using WPFPresentation.Models.Provider;
using WPFPresentation.Utils;

namespace WPFPresentation.ViewModels
{
    public abstract class BaseVentaViewModel : BaseViewModel
    {
        #region Property

        private VentaModel _venta;
        protected DeudaModel _deudaCliente;

        private ProveedorModel _proveedor;
        public ProveedorModel Proveedor
        {
            get { return _proveedor; }
            set { if (_proveedor != value) _proveedor = value; OnPropertyChanged("Proveedor"); }
        }

        private decimal _precioProveedor;
        public decimal PrecioProveedor
        {
            get { return _precioProveedor; }
            set { if (_precioProveedor != value) { _precioProveedor = value; OnPropertyChanged("PrecioProveedor"); } }
        }

        private decimal _abono;
        public decimal Abono
        {
            get { return _abono; }
            set { if (_abono != value) _abono = value; OnPropertyChanged("Abono"); }
        }

        private string _identificador;
        public string Identificador
        {
            get { return _identificador; }
            set { if (_identificador != value) { _identificador = value; OnPropertyChanged("Identificador"); } }
        }

        protected PedidoModel PedidoToAdd { get; set; }
        protected SubPedidoModel SubPedidoToAdd { get; set; }
        protected SubPedidoEntryModel SubPedidoEntryToAdd { get; set; }


        public CommandModel AddPedidoComand { get; private set; }
        public CommandModel DeletePedidoComman { get; private set; }

        /// <summary>
        /// THE VENTA MODEL
        /// </summary>
        public VentaModel Venta
        {
            get { return _venta; }
            set { if (_venta != value) { _venta = value; OnPropertyChanged("Venta"); } }
        }

        private ObservableCollection<ProveedorModel> _proveedores;

        /// <summary>
        /// Lista de Proveedores sobre los que se ejecuta la busqueda en para agregar
        /// un nuevo pedido
        /// </summary>
        public ObservableCollection<ProveedorModel> Proveedores
        {
            get { return _proveedores; }
            set
            {
                if (_proveedores != value) _proveedores = value; OnPropertyChanged("Proveedores");
            }
        }

        /// <summary>
        /// Esta clase la pongo aqui pq se faja con la ui por debajo y es mejor asi
        /// solo sirve para determinar el numero de la venta y la deuda del cliente que
        /// se pone en el texbox a la hora de seleccionar un cliente
        /// </summary>
        public DeudaModel DeudaCliente
        {
            get { return _deudaCliente; }
            set
            {
                if (_deudaCliente != value)
                {
                    _deudaCliente = value;
                    OnPropertyChanged("DeudaCliente");
                }
            }
        }

        #endregion


        #region Ctor

        public BaseVentaViewModel(FacadeProvider facadeProvider) : base(facadeProvider)
        {
            Venta = new VentaModel();
            AddPedidoComand = new AddPedidoCommand(this);
            DeletePedidoComman = new DeletePedidoCommand(this);

            //Atamos al cambio de propiedad de venta FinClientId un helper par
            //Cambiar el cliente por la id que se pase
            Venta.Attach("FindClientId", s => SetClientByDocument(s));


        }

        public async override void InitializeViewContent()
        {
            IsLoading = true;

            Proveedores = await Task.Run(() =>
            {
                return FacadeProvider.ProveedorProvider().GetAll();
            });

            IsLoading = false;


        }

        #endregion

        #region Implementetion Command

        // implementation of the AddPedidoCommand

        protected class AddPedidoCommand : CommandModel
        {
            private BaseVentaViewModel viewModel;

            public AddPedidoCommand(BaseVentaViewModel viewModel)
            {
                this.viewModel = viewModel;
            }

            public override void OnCanExecute(object sender, CanExecuteRoutedEventArgs e)
            {
                decimal deuda = 0;
                if (viewModel.DeudaCliente != null)
                    deuda = viewModel.DeudaCliente.Deuda;


                e.CanExecute = viewModel.Venta.Cliente != null
                               & viewModel.Proveedor != null
                               & !string.IsNullOrEmpty(viewModel.Identificador)
                               & viewModel.PrecioProveedor > 0
                               & deuda == 0;

                e.Handled = true;
            }

            public override void OnExecute(object sender, ExecutedRoutedEventArgs e)
            {
                viewModel.AddPedido();
            }
        }

        protected class DeletePedidoCommand : CommandModel
        {
            private BaseVentaViewModel viewModel;

            public DeletePedidoCommand(BaseVentaViewModel viewModel)
            {
                this.viewModel = viewModel;
            }

            public override void OnCanExecute(object sender, CanExecuteRoutedEventArgs e)
            {
                e.CanExecute = true;
                e.Handled = true;
            }

            public override void OnExecute(object sender, ExecutedRoutedEventArgs e)
            {

            }
        }

        #endregion


        #region Helpers

        public virtual void AddPedido()
        {


            SubPedidoEntryToAdd = new SubPedidoEntryModel { Abono = Abono, };
            SubPedidoToAdd = new SubPedidoModel { Identificador = Identificador, PrecioProveedor = PrecioProveedor };
            PedidoToAdd = new PedidoModel { VentaId = Venta.VentaId, ProveedorId = Proveedor.ProveedorId, Proveedor = Proveedor, ItemNumero = Venta.Pedidos.Count + 1 };


        }

        protected bool ProveedorExist(int proveedorId)
        {
            foreach (var pedido in Venta.Pedidos)
            {
                if (pedido.Proveedor.ProveedorId == proveedorId)
                    return true;
            }

            return false;
        }

        protected void InitPedidoToAddObjects()
        {
            PrecioProveedor = 0;
            Abono = 0;
            Identificador = "";
            Proveedor = new ProveedorModel();
        }

        public abstract void RemovePedido(object element);

        /// <summary>
        /// Metodo que actualiza el cliente de esta venta lo hago asi
        /// para que los modelos permanescan libre del acceso a datos no se si esta bien
        /// </summary>
        /// <param name="document"></param>
        /// <returns></returns>
        public async void SetClientByDocument(object document)
        {
            IsLoading = true;

            await Task.Run(() => Venta.Cliente = FacadeProvider.ClienteProvider().GetClienteByDocumento(document.ToString()));


            if (Venta.Cliente != null)
            {
                Venta.ClienteId = Venta.Cliente.ClienteId;
                SetClientDeuda(Venta.ClienteId);
            }
            else
            {
                DeudaCliente = null;
            }

            IsLoading = false;

        }

        public void SetClientDeuda(int id)
        {
            var cliente = FacadeProvider.ClienteProvider().Get(id);

            if (cliente != null)
            {
                var venta = FacadeProvider.VentaProvider().GetVentaWithDeuda(id);

                if (venta != null)
                {
                    DeudaCliente = new DeudaModel { Deuda = venta.Deuda, VentaId = venta.VentaId };
                }
                else
                {
                    DeudaCliente = null;
                }
            }
            else
            {
                //para que se borre la deuda del cliente que ya se habia buscado
                DeudaCliente = null;
            }


        }

        /// <summary>
        /// Actualiza los numero de la propiedad ItemNumero
        /// </summary>
        protected void ActualizeItemNumero(ObservableCollection<PedidoModel> pedidos)
        {
            for (int i = 0; i < pedidos.Count; i++)
            {
                pedidos[i].ItemNumero = i + 1;
            }
        }

        protected int GetItemNumero(ObservableCollection<PedidoModel> pedidos)
        {
            return pedidos.Count + 1;
        }


        #endregion

    }
}
