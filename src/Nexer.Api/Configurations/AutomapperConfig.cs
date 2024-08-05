using AutoMapper;
using Nexer.Api.ViewModels;
using Nexer.Business.Models;

namespace Nexer.Api.Configurations
{
    public class AutomapperConfig : Profile
    {
        public AutomapperConfig()
        {
            CreateMap<Customer, CustomerViewModel>().ReverseMap();
            CreateMap<ProductViewModel, Product>();

            CreateMap<Product, ProductViewModel>()
                .ForMember(dest => dest.CustomerName, opt => opt.MapFrom(src => src.Customer.Name));

            CreateMap<Billing, BillingViewModel>()
               .ForMember(dest => dest.Customer, opt => opt.MapFrom(src => src.Customer))
               .ForMember(dest => dest.BillingLines, opt => opt.MapFrom(src => src.BillingLines));

            CreateMap<BillingLine, BillingLineViewModel>()
                .ForMember(dest => dest.Product, opt => opt.MapFrom(src => src.Product))
                .ReverseMap();
        }

    }
}
