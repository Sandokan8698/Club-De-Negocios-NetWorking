using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Data_Layer;
using Data_Layer.Abstract;
using Data_Layer.Abstract.Repositories;
using Data_Layer.Implementations.Repositories;
using Domain_Layer.Entities;
using Service_Layer.Abstract;

namespace Service_Layer.Implementation
{
    public class BaseService<TEntity>:IService<TEntity> where TEntity: class 
    {
        protected IUnitOfWork UnitOfWork;
        private  IRepository<TEntity> _baseRepository;

        public BaseService(IUnitOfWork unitOfWork, IRepository<TEntity> baseRepository)
        {
            UnitOfWork = unitOfWork;
            _baseRepository = baseRepository;
        }

        public virtual TEntity  Add(TEntity entity)
        {
            using (UnitOfWork)
            {
                _baseRepository.Add(entity);
                UnitOfWork.Complete();
                return entity;
            }
            
        }

        public virtual void AddRange(IEnumerable<TEntity> entities)
        {
            throw new NotImplementedException();
        }

        public virtual IEnumerable<TEntity> Find(Expression<Func<TEntity, bool>> predicate)
        {
            using (UnitOfWork)
            {
                return _baseRepository.Find(predicate);
            }
            
        }

        public virtual TEntity Get(int id)
        {
            using (UnitOfWork)
            {
                return _baseRepository.Get(id);
            }
          
        }

        public virtual IEnumerable<TEntity> GetAll()
        {
            using (UnitOfWork)
            {
                return _baseRepository.GetAll();
            }
          
        }

        public virtual void Remove(TEntity entity)
        {
            using (UnitOfWork)
            {
                _baseRepository.Remove(entity);
                UnitOfWork.Complete();
            }
          
        }

        public virtual void RemoveRange(IEnumerable<TEntity> entities)
        {
            throw new NotImplementedException();
        }

        public virtual TEntity SingleOrDefault(Expression<Func<TEntity, bool>> predicate)
        {
            throw new NotImplementedException();
        }

        public virtual TEntity Update(TEntity entity)
        {
            using (UnitOfWork)
            {
                _baseRepository.Update(entity);
                UnitOfWork.Complete();
                return entity;
            }
           
        }
    }
}
