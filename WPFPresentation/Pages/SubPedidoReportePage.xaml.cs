using System.Windows;
using System.Windows.Controls;
using FirstFloor.ModernUI.Windows;
using WPFPresentation.Models;
using WPFPresentation.Pages.Content;
using WPFPresentation.Utils;
using WPFPresentation.ViewModels;


namespace WPFPresentation.Pages
{
    /// <summary>
    /// Interaction logic for SubPedidoReportePage.xaml
    /// </summary>
    public partial class SubPedidoReportePage : UserControl
    {
        private SubPedidoReporteViewModel _subPedidoReporteViewModel;
        public SubPedidoReportePage()
        {
            InitializeComponent();
            _subPedidoReporteViewModel = new SubPedidoReporteViewModel(DependencyResolver.Instance.FacadeProvider);
            DataContext = _subPedidoReporteViewModel;

            PaginatorControlSubPedido.Paginator.ItemsPerPage = 50;
            PaginatorControlSubPedido.Paginator.SetPaginable(_subPedidoReporteViewModel);

            _subPedidoReporteViewModel.InitializeViewContent();

            FilterControlSubPedido.FilterControlViewModel.MensageToSendWhenApplay = ViewModelMessages.ApplayFilterInSubPedidoReport;
            FilterControlSubPedido.FilterControlViewModel.MensageToSendWhenRemove = ViewModelMessages.RemoveFilterInSubPedidoReport;

           
        }


        private void ButtonBase_OnClick(object sender, RoutedEventArgs e)
        {
           var item = (SubPedidoModel)DgSubPedidos.SelectedCells[0].Item;
           VentaDetailDialogPage ventaDetailViewModel = new VentaDetailDialogPage(item.Pedido.Venta);
            ventaDetailViewModel.ShowDialog();
        }
    }
}
