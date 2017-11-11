
using System;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using Domain_Layer.Entities;
using FirstFloor.ModernUI.Windows.Controls;
using WPFPresentation.Models;
using WPFPresentation.Models.Provider;
using WPFPresentation.Pages;
using WPFPresentation.Utils;

namespace WPFPresentation.ViewModels
{
    public class VentaNewViewModel: BaseVentaViewModel
    {

        #region Property

        public CommandModel AddNewVentaComman { get; private set; }
        public CommandModel SaveVentaComand { get; private set; }

        #endregion

        public VentaNewViewModel(FacadeProvider facadeProvider):base(facadeProvider)
        {
            AddNewVentaComman = new AddNewVentaCommand(this);
            SaveVentaComand = new SaveVentaCommand(this);

            Messenger.Instance.Register(o => ActualizeProveedorList(o), ViewModelMessages.AddNewProveedor);

        }
     
        #region Implementetion Command

        // implementation of the AddPedidoCommand
        private class AddNewVentaCommand : CommandModel
        {
            private VentaNewViewModel _newViewModel;

            public AddNewVentaCommand(VentaNewViewModel newViewModel)
            {
                this._newViewModel = newViewModel;
            }

            public override void OnCanExecute(object sender, CanExecuteRoutedEventArgs e)
            {
                e.CanExecute = true;

                e.Handled = true;
            }

            public override void OnExecute(object sender, ExecutedRoutedEventArgs e)
            {
                _newViewModel.AddNewVenta();
            }
        }

        private class SaveVentaCommand : CommandModel
        {
            private VentaNewViewModel _newViewModel;

            public SaveVentaCommand(VentaNewViewModel newViewModel)
            {
                this._newViewModel = newViewModel;
            }

            public override void OnCanExecute(object sender, CanExecuteRoutedEventArgs e)
            {
                e.CanExecute = _newViewModel.Venta.Pedidos.Count > 0;
                e.Handled = true;
            }

            public override void OnExecute(object sender, ExecutedRoutedEventArgs e)
            {
               _newViewModel.SaveVenta();
            }
        }
        #endregion

        #region Helpers

        private async void SaveVenta()
        {
            var venta =  FacadeProvider.VentaProvider().Add(Venta);

            InitPedidoToAddObjects();

            Venta = new VentaModel();
            Venta.Cliente = new ClienteModel();
            DeudaCliente = null;
            Venta.Attach("FindClientId", s => SetClientByDocument(s));

            var ventaPrintPage = await Task.Factory.StartNew(() =>
            {
              return new VentaPrintPage();

            }, CancellationToken.None
                , TaskCreationOptions.None
                , TaskScheduler.FromCurrentSynchronizationContext());
             
            ventaPrintPage.VentaModels = venta;
            ventaPrintPage.ShowDialog();    

        }

        public override void AddPedido()
        {
            try
            {
                if (!ProveedorExist(Proveedor.ProveedorId))      
                {
                    base.AddPedido();

                    SubPedidoToAdd.Add(SubPedidoEntryToAdd);

                    PedidoToAdd.Add(SubPedidoToAdd);

                    Venta.Add(PedidoToAdd);

                    ActualizeItemNumero(Venta.Pedidos);

                    InitPedidoToAddObjects();
                }
                else
                {
                    var dlg = new ModernDialog
                    {
                        Title = "Aviso",
                        Content = "El proveedor ya fue agreagado"
                    };
                    dlg.Buttons = new Button[] { dlg.OkButton };
                    dlg.ShowDialog();
                }
            }
                      
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
            }
        }

        public override void RemovePedido(object elemento)
        {
            Venta.Remove((PedidoModel) elemento);
            ActualizeItemNumero(Venta.Pedidos);
        }

        public void AddNewVenta()
        {
            Venta = new VentaModel();
            DeudaCliente = null;
            Venta.Attach("FindClientId", s => SetClientByDocument(s));
        }

        public void ShowClienteDeudaDialog()
        {
            if (DeudaCliente != null)
            {
                var DeudaVenta = FacadeProvider.VentaProvider().Get(DeudaCliente.VentaId);
                VentaDetailDialogPage ventaDetailViewModel = new VentaDetailDialogPage(DeudaVenta);
                ventaDetailViewModel.ShowDialog();
            }
           
        }

        /// <summary>
        /// Manejador del evento AddNewProveedor para cuando se agrege un nuevo proveedor se actualize la lista 
        /// de proveedores sobres los que se realiza la busqueda en este viewmodel
        /// </summary>
        /// <param name="o"></param>
        private void ActualizeProveedorList(object o)
        {
            Proveedores.Add((ProveedorModel)o);
        }

        private void ActualizePedidoSupdedidoSupdedidoEntryeValueAbono(object o)
        {
            
        }
        #endregion

    }
}
