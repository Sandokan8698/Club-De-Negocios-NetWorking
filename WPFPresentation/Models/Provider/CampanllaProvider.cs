
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq.Expressions;
using AutoMapper;
using Data_Layer.Implementations;
using Domain_Layer.Entities;
using Service_Layer.Abstract;
using Service_Layer.Implementation;

namespace WPFPresentation.Models.Provider
{
    public class CampanllaProvider : BaseProvider<CampanllaModel, Campanlla>
    {
        public CampanllaProvider(UnitOfWork unitOfWork) : base(unitOfWork, unitOfWork.CampanllaRepository)
        {
        }
    }
}
