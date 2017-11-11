using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Data_Layer.Abstract;
using Domain_Layer.Entities;
using Faker.Generator;
using Service_Layer.Abstract;
using Faker;

namespace Service_Layer.Implementation
{
    public class ClienteService : BaseService<Cliente>, IClienteService
    {
        public ClienteService(IUnitOfWork unitOfWork): base(unitOfWork, unitOfWork.ClienteRepository)
        {

        }

        #region Base Method

        
        public override Cliente Add(Cliente entity)
        {
            try
            {
                UnitOfWork.ClienteRepository.Add(entity);
                UnitOfWork.Complete();
            }
            catch (Exception e)
            {
                UnitOfWork.ClienteRepository.Remove(entity);
                throw e;
            }

            return entity;
        }

        

      

        public Cliente GetClienteByDocumento(string documento)
        {
            return UnitOfWork.ClienteRepository.GetClienteByDocumento(documento);
        }

      
        #endregion

        public void GenerateFakeData()
        {
            var clienteFactory = new Faker<Cliente>();
            var clientes = clienteFactory.CreateMany(100, v =>
            {
                v.ClienteId = new IntegerGenerator().Get(100, 200);
                v.Nombre = new NameGenerator().Get();  // Name generator will generate real names like Jhon Doe 1, Bruno Matarazo 2.
                v.NumeroDocumento = new IntegerGenerator().Get(800000000, 1000000000).ToString();
                v.Apellidos = new NameGenerator().Get(10);
                v.Telefono = new IntegerGenerator().Get(100000000, 1000000000).ToString();
                v.FechaNacimiento = DateTime.Now;
            });

            foreach (var cliente in clientes)
            {
                UnitOfWork.ClienteRepository.Add(cliente);
            }

            UnitOfWork.Complete();
        }

    }
}
