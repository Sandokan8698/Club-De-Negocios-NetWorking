
using System.Collections.Generic;
using System.Linq;
using Data_Layer.Abstract;
using Data_Layer.Utils.FilterEngine;
using Data_Layer.Utils.ServiceFilter;
using Domain_Layer.Entities;
using Faker;
using Faker.Generator;
using Service_Layer.Abstract;

namespace Service_Layer.Implementation
{
    public class VentaService : BaseService<Venta>, IVentaService
    {


        public VentaService(IUnitOfWork unitOfWork) : base(unitOfWork,unitOfWork.VentaRepository)
        {

        }

        public override Venta Add(Venta entity)
        {
            var venta = base.Add(entity);
            venta = Get(venta.VentaId);
            return venta;
        }

        public Venta GetVentaWithDeuda(int clientId)
        {
            var ventas = UnitOfWork.VentaRepository.GetAll().Where(v => v.ClienteId == clientId);
            return ventas.FirstOrDefault(v => v.Deuda > 0);
        }

        public IEnumerable<Venta> GetClientVentas(Cliente cliente)
        {
            return UnitOfWork.VentaRepository.GetClientVentas(cliente);
        }

        public IEnumerable<Venta> GetClienteVentasFiltered(Cliente cliente, FilterEntitie filterEntitie)
        {
            var clienteVentas = GetClientVentas(cliente);
            return ApplyFilter(filterEntitie, clienteVentas.AsQueryable());
        }

        public IEnumerable<Venta> GetVentasFilters(FilterEntitie filterEntitie)
        {
            return ApplyFilter(filterEntitie, GetAll().AsQueryable());
        }

        public IEnumerable<Venta> ApplyFilter(FilterEntitie filterEntitie, IQueryable<Venta> ventasQueryable)
        {
            var filterCreator = FilterCreatorFactory.Instance.Create(FilterBuilderTypes.VentasFilterCreator, filterEntitie);
            var filterQuerys = filterCreator.CreateFilterQuerys();
            return ventasQueryable.ApplySearchCriteria(filterQuerys);
        }

        public void GenerateFakeData()
        {
            var ventaFactory = new Faker<Venta>();
            var ventas = ventaFactory.CreateMany(100, v =>
            {
                v.ClienteId = new IntegerGenerator().Get(87, 186);
                v.Fecha = new DateTimeGenerator().Get().Date;
                v.Pedidos = GetFackePedidos();
                v.Cliente = null;
                v.Trabajador = null;
                v.TrabajadorId = 3;  // Name generator will generate real names like Jhon Doe 1, Bruno Matarazo 2.
            });

            foreach (var venta in ventas)
            {
                UnitOfWork.VentaRepository.Add(venta);
            }

            UnitOfWork.Complete();
        }

        private ICollection<Pedido> GetFackePedidos()
        {
            int randomNumber = new IntegerGenerator().Get(10,40);
            ICollection<Pedido> pedidos = new List<Pedido>();

            var pedidoFactory = new  Faker<Pedido>();

            for (int i = 0; i < randomNumber; i++)
            {
                var precioProveedor = new IntegerGenerator().Get(100, 1000);
                var abono = new IntegerGenerator().Get(10, 100);
                var deuda = precioProveedor - abono;
                pedidos.Add(new Pedido
                {
                   
                });
            }

            return pedidos;
        }
    }
}
