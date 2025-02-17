namespace Application.Mappings;

using AutoMapper;
using Domain.Abstractions;
using Features.Customers.Commands.RegisterCustomer;
using Features.Customers.Vms;
using Features.Entrances.Commands.RegisterEntrance;
using Features.Exits.Commands.RegisterExit;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<RegisterCustomerCommand, Customer>();
        CreateMap<RegisterEntranceCommand, Entrance>();
        CreateMap<RegisterExitCommand, Exit>();
        CreateMap<Customer, CustomerVm>()
            .ForMember(dest => dest.Entrances, opt => opt.MapFrom(src => src.Entrances))
            .ForMember(dest => dest.Exits, opt => opt.MapFrom(src => src.Exits))
            .ReverseMap();
        
        CreateMap<Customer, CustomerActivityVm>()
            .ForMember(dest => dest.Entrances, opt => opt.MapFrom(src => src.Entrances))
            .ForMember(dest => dest.Exits, opt => opt.MapFrom(src => src.Exits))
            .ReverseMap();
    }
}
