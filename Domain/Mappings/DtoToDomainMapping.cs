using AutoMapper;
using Domain.DTOs;
using Domain.Entities;

namespace Domain.Mappings
{
    public class DtoToDomainMapping : Profile
    {
        public DtoToDomainMapping()
        {
            CreateMap<AccountDTO, Account>();
            CreateMap<TransactionDTO, Transaction>();
        }
    }
}