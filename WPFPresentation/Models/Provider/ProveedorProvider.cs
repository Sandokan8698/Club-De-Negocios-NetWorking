﻿using System;
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
    public class ProveedorProvider : BaseProvider<ProveedorModel, Proveedor>

    {
        public ProveedorProvider(UnitOfWork unitOfWork) : base(unitOfWork, unitOfWork.ProveedorRepository)
        {
        }
    }
}
