﻿using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Data_Layer.Abstract.Repositories;
using Data_Layer.Implementations;
using Domain_Layer.Entities;
using Service_Layer.Abstract;
using Service_Layer.Implementation;

namespace WPFPresentation.Models.Provider
{
    public class PedidoProvider : BaseProvider<PedidoModel,Pedido>
    {
        public PedidoProvider(UnitOfWork unitOfWork):base(unitOfWork, unitOfWork.PedidoRepositoy)
        {

        }

        public override PedidoModel Add(PedidoModel entity)
        {

            using (UnitOfWork)
            {
                var entitySource = Mapper.Map<PedidoModel, Pedido>(entity);
                entitySource =  UnitOfWork.PedidoRepositoy.Add(entitySource);
                UnitOfWork.Complete();
                entitySource = UnitOfWork.PedidoRepositoy.Get(entitySource.PedidoId);
                return Mapper.Map<Pedido, PedidoModel>(entitySource);
            }
        }
    }
}
