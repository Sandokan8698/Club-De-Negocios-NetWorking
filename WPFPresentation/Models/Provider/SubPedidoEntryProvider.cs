using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Data_Layer.Implementations;
using Domain_Layer.Entities;
using Service_Layer.Abstract;
using Service_Layer.Implementation;

namespace WPFPresentation.Models.Provider
{
    public class SubPedidoEntryProvider : BaseProvider<SubPedidoEntryModel, SubPedidoEntry>
    {
        public SubPedidoEntryProvider(UnitOfWork unitOfWork) : base(unitOfWork,unitOfWork.SubPedidoEntryRepository)
        {
        }

        public ObservableCollection<SubPedidoEntryModel> GetDailyReportPayment(VentaModel venta)
        {
            using (UnitOfWork)
            {
                var ventaSource = Mapper.Map<VentaModel, Venta>(venta);
                var supedidosEntrys = UnitOfWork.SubPedidoEntryRepository.GetDailyReportPayment(ventaSource);
                return Mapper.Map<IEnumerable<SubPedidoEntry>, ObservableCollection<SubPedidoEntryModel>>(
                    supedidosEntrys);
            }
        }
    }
}
