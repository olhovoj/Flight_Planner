using AutoMapper;
using FlightPlanner.Core.Models;
using FlightPlanner.UseCases.Models;

namespace FlightPlanner.UseCases.Mappings;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<Airport, AirportViewModel>()
            .ForMember(viewModel => viewModel.Airport, 
                options => options
                .MapFrom(source => source.AirportCode));
        CreateMap<AirportViewModel, Airport>()
            .ForMember(airport => airport.AirportCode, 
                options => options
                    .MapFrom(source => source.Airport));
        CreateMap<AddFlightRequest, Flight>();
        CreateMap<Flight, FlightViewModel>();
    }
}