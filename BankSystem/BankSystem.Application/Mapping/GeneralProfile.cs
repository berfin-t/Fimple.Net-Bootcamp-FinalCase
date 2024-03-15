using AutoMapper;
using BankSystem.Application.Dto;
using BankSystem.Business.Dto;
using BankSystem.Data.Entities;
using BankSystem.Domain.Entities;

namespace BankSystem.Application.Mapping
{
    public class GeneralProfile : Profile
    {
        public GeneralProfile()
        {
            CreateMap<AccountModel, AccountDto>().ReverseMap();

            CreateMap<LoanApplicationModel, LoanApplicationDto>().ReverseMap();

            CreateMap<LoanModel, LoanDto>().ReverseMap();

            CreateMap<PaymentModel, PaymentDto>().ReverseMap();

            CreateMap<SupportTicketModel, SupportTicketDto>().ReverseMap();

            CreateMap<LoginModel, LoginDto>().ReverseMap();

            CreateMap<UserModel, UserDto>().ReverseMap();
            
            CreateMap<TransactionModel, TransactionDto>().ReverseMap();
            
        }
    }
}
