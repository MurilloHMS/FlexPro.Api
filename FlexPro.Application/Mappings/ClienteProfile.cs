using AutoMapper;
using FlexPro.Application.DTOs.Cliente;
using FlexPro.Domain.Entities;

namespace FlexPro.Api.Application.Mappings;

public class ClienteProfile : Profile
{
    public ClienteProfile()
    {
        CreateMap<ClienteRequestDto, Cliente>();
        CreateMap<Cliente, ClienteResponseDto>();
    }
}