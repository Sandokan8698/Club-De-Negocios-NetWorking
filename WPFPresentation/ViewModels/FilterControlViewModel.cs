using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Input;
using WPFPresentation.Models;
using WPFPresentation.Models.Provider;
using WPFPresentation.Utils;

namespace WPFPresentation.ViewModels
{
    public class FilterControlViewModel : BaseViewModel
    {
        #region Propertys 

        public CommandModel ApplyFilter { get; private set; }
        public CommandModel RemoveFilter { get; private set; }

        public ViewModelMessages MensageToSendWhenApplay { get; set; }
        public ViewModelMessages MensageToSendWhenRemove { get; set; }

        public bool IsFiltered { get; set; }

        private ObservableCollection<ClienteModel> _clientes;
        /// <summary>
        /// Clientes sobre los que se realiza la busqueda
        /// </summary>
        public ObservableCollection<ClienteModel> Clientes
        {
            get { return _clientes; }
            set
            {
                if (_clientes != value) _clientes = value; OnPropertyChanged("Clientes");
            }
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
        /// El Model de Filtro 
        /// </summary>
        private FilterModel _filter;
        public FilterModel Filter
        {
            get { return _filter; }
            set
            {
                if (_filter != value)
                {
                    _filter = value;
                    OnPropertyChanged("Filter");
                }
            }
        }

        #endregion


        #region Ctor

        public FilterControlViewModel(FacadeProvider facadeProvider) : base(facadeProvider)
        {
            ApplyFilter = new ApplyFilters(this);
            RemoveFilter = new RemoveFilters(this);
            Filter = new FilterModel();

            //Escuchamos los mensages que mandan las viewModels con el valor 
            //del FilterModel para inicializar el filterModel de esta newControlViewModel
            //Nada se inicializa el Filter de esta pagina segun le convenga al ControlViewModel que lo esta enviando
            Messenger.Instance.Register(InitializeFilter, ViewModelMessages.InitializerFilterValue);

            Messenger.Instance.Register(o => ActualizeProveedorList(o), ViewModelMessages.AddNewProveedor);

            Messenger.Instance.Register(o => ActualizeClienteList(o), ViewModelMessages.AddNewCliente);

        }

        #endregion

        #region Helpers

        public  override void InitializeViewContent()
        {
          
            Clientes = FacadeProvider.ClienteProvider().GetAll();
            Proveedores = FacadeProvider.ProveedorProvider().GetAll();
        }

        public override void UnloadViewContent()
        {
            Messenger.Instance.Unregister(InitializeFilter, ViewModelMessages.InitializerFilterValue);
        }

        public void InitializeFilter(object param)
        {
            Filter = (FilterModel)param;
        }

        /// <summary>
        /// Reinicia los valores del FilterControl
        /// </summary>
        public void ReloadFilter()
        {
            IsFiltered = false;
            Filter.Cliente = null;
            Filter.Identificador = "";
            Filter.VentaId = 0;
            Filter.Proveedor = null;
            Filter.Desde = DateTime.Now.AddMonths(-1);
            Filter.Hasta = DateTime.Now;
        }
        #endregion


        #region Implementetion Command

        // implementation of the AddPedidoCommand
        private class ApplyFilters : CommandModel
        {
            private FilterControlViewModel viewModel;

            public ApplyFilters(FilterControlViewModel viewModel)
            {
                this.viewModel = viewModel;
            }

            public override void OnCanExecute(object sender, CanExecuteRoutedEventArgs e)
            {
                e.CanExecute =  true;
                e.Handled = true;
            }

            public override void OnExecute(object sender, ExecutedRoutedEventArgs e)
            {
                Messenger.Instance.NotifyColleagues(viewModel.MensageToSendWhenApplay, viewModel.Filter);
                viewModel.IsFiltered = true;
            }
        }

        private class RemoveFilters : CommandModel
        {
            private FilterControlViewModel viewModel;

            public RemoveFilters(FilterControlViewModel viewModel)
            {
                this.viewModel = viewModel;
            }

            public override void OnCanExecute(object sender, CanExecuteRoutedEventArgs e)
            {

                e.CanExecute = viewModel.IsFiltered;
                e.Handled = true;
            }

            public override void OnExecute(object sender, ExecutedRoutedEventArgs e)
            {
                Messenger.Instance.NotifyColleagues(viewModel.MensageToSendWhenRemove, viewModel.Filter);
                viewModel.ReloadFilter();
            }
        }

        /// <summary>
        /// Manejador del mensage AddNewProveedor para cuando se agrege un nuevo proveedor se actualize la lista 
        /// de proveedores sobres los que se realiza la busqueda en este viewmodel
        /// </summary>
        /// <param name="o"></param>
        private void ActualizeProveedorList(object o)
        {
            
            Proveedores.Add((ProveedorModel)o);
        }

        /// <summary>
        /// Manejador del mensage AddNewCliente para cuando se agrege un nuevo cliente se actualize la lista 
        /// de clientes sobres los que se realiza la busqueda en este viewmodel
        /// </summary>
        /// <param name="o"></param>
        private void ActualizeClienteList(object o)
        {
            Clientes.Add((ClienteModel)o);
        }
        #endregion
    }
}
