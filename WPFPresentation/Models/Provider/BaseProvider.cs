using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Data_Layer;
using Data_Layer.Abstract;
using Data_Layer.Abstract.Repositories;
using Data_Layer.Implementations;
using Domain_Layer.Entities;
using Service_Layer.Abstract;
using Service_Layer.Implementation;

namespace WPFPresentation.Models.Provider
{
    public abstract class BaseProvider<TEntity, TSource > : IProvider<TEntity> where TEntity: class where TSource: class 
    {

        /// <summary>
        /// Instancia del Proveeder de Servicio de este provider
        /// </summary>
        protected IUnitOfWork UnitOfWork;
        private IRepository<TSource> _baseRepository;

        public BaseProvider(UnitOfWork unitOfWork, IRepository<TSource> baseRepository)
        {
            UnitOfWork = unitOfWork;
            _baseRepository = baseRepository;
        }

        public virtual TEntity Add(TEntity entity)
        {
            using (UnitOfWork)
            {
                var entitySource = Mapper.Map<TEntity,TSource>(entity);
                _baseRepository.Add(entitySource);
                UnitOfWork.Complete();
                return Mapper.Map<TSource, TEntity>(entitySource);        
            }
        }

        public void AddRange(ObservableCollection<TEntity> entities)
        {
            throw new NotImplementedException();
        }

        public ObservableCollection<TEntity> Find(Expression<Func<TEntity, bool>> predicate)
        {
            throw new NotImplementedException();
        }

        public TEntity Get(int id)
        {
            using (UnitOfWork)
            {
            
                var entiyEntity = _baseRepository.Get(id);
                return Mapper.Map<TSource, TEntity>(entiyEntity);
            }
        }

        public virtual ObservableCollection<TEntity> GetAll()
        {
            using (UnitOfWork)
            {
                var entitysSources = _baseRepository.GetAll();
                return Mapper.Map<IEnumerable<TSource>, ObservableCollection<TEntity>>(entitysSources);
            }
           
        }

        public void Remove(TEntity entity)
        {
            using (UnitOfWork)
            {
                var entitySource = Mapper.Map<TEntity, TSource>(entity);
                _baseRepository.Remove(entitySource);
                UnitOfWork.Complete();

            }
        }

        public void RemoveRange(ObservableCollection<TEntity> entities)
        {
            throw new NotImplementedException();
        }

        public TEntity SingleOrDefault(Expression<Func<TEntity, bool>> predicate)
        {
            throw new NotImplementedException();
        }

        public TEntity Update(TEntity entity)
        {
            using (UnitOfWork)
            {
                var entitySource = Mapper.Map<TEntity, TSource>(entity);
                var entiyEntity = _baseRepository.Update(entitySource);
                UnitOfWork.Complete();

                return Mapper.Map<TSource, TEntity>(entiyEntity);
            }
        }
    }
}
