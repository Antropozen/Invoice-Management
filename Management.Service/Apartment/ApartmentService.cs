﻿using AutoMapper;
using Management.DB.Entities.DataContext;
using Management.Model;
using Management.Model.Apartment;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Management.Service.Apartment
{
    public class ApartmentService : IApartmentService
    {
        private readonly IMapper mapper;
        public ApartmentService(IMapper _mapper)
        {
            mapper = _mapper;
        }
        public General<ApartmentViewModel> Delete(int id)
        {
            var result = new General<ApartmentViewModel>();
            using (var srv = new ManagementContext())
            {
                var data = srv.Apartment.SingleOrDefault(i => i.Id == id);
                if (data is not null)
                {
                    srv.Apartment.Remove(data);
                    srv.SaveChanges();
                    result.Entity = mapper.Map<ApartmentViewModel>(data);
                    result.IsSucces = true;
                }
                else
                {
                    result.BadMessage = "Daire bilgileri silinemedi.";
                }
            }
            return result;
        }

        public General<ApartmentViewModel> ListApartment()
        {
            var result = new General<ApartmentViewModel>();
            using (var srv = new ManagementContext())
            {
                var data = srv.Apartment.OrderBy(i => i.Id);
                if (data.Any())
                {
                    result.List = mapper.Map<List<ApartmentViewModel>>(data);
                    result.IsSucces = true;
                }
                else
                {
                    result.BadMessage = "Daire bilgisi bulunamadı.";
                }
            }
            return result;
        }

        public General<ApartmentInsertModel> Insert(ApartmentInsertModel newApartment)
        {
            var result = new General<ApartmentInsertModel>() { IsSucces = false };
            var model = mapper.Map<Management.DB.Entities.Apartment>(newApartment);
            using (var srv = new ManagementContext())
            {
                model.Idatetime = DateTime.Now;
                srv.Apartment.Add(model);
                srv.SaveChanges();
                result.Entity = mapper.Map<ApartmentInsertModel>(model);
                result.IsSucces = true;
            }
            return result;
        }

        public General<ApartmentUpdateModel> Update(int id, ApartmentUpdateModel apartmentUpdate)
        {
            var result = new General<ApartmentUpdateModel>();
            using (var srv = new ManagementContext())
            {
                var data = srv.Apartment.SingleOrDefault(i => i.Id == id);
                if (data is not null)
                {
                    data.AelectricityBill = apartmentUpdate.AelectricityBill;
                    data.AgasBill = apartmentUpdate.AgasBill;
                    data.AwaterBill = apartmentUpdate.AwaterBill;
                    data.Adues = apartmentUpdate.Adues;
                    srv.SaveChanges();
                    result.Entity = mapper.Map<ApartmentUpdateModel>(apartmentUpdate);
                    result.IsSucces = true;
                }
                else
                {
                    result.BadMessage = "Faturalar güncellenemedi.";
                }
            }
            return result;
        }

        public General<ApartmentViewModel> GetById(int id)
        {
            var result = new General<ApartmentViewModel>();
            using (var service = new ManagementContext())
            {
                var data = service.Apartment.SingleOrDefault(a => a.Id == id);
                if (data is null)
                {
                    result.BadMessage = "No apartment found!";
                    return result;
                }
                result.Entity = mapper.Map<ApartmentViewModel>(data);
                result.IsSucces = true;
            }
            return result;
        }
    }
}
