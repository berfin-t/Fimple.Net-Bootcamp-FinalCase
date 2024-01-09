using AutoMapper;
using BankSystem.Application.Dto;
using BankSystem.Domain.Entities;

namespace BankSystem.Application.Mapping
{
    public class GeneralProfile : Profile
    {
        public GeneralProfile()
        {
            CreateMap<AccountModel, AccountDto>().ReverseMap();

            CreateMap<LoginModel, LoginDto>().ReverseMap();

            CreateMap<UserModel, UserDto>().ReverseMap();
            
            CreateMap<TransactionModel, TransactionDto>().ReverseMap();            
        }
    }
}
