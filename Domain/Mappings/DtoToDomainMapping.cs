using AutoMapper;
using Domain.DTOs;
using Domain.Entities;

namespace Domain.Mappings
{
    public class DtoToDomainMapping : Profile
    {
        //TODO: montar o mapping
        DtoToDomainMapping()
        {
            CreateMap<AccountDTO, Account>();
        }
    }
}