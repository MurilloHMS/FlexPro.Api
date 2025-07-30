using AutoMapper;
using FlexPro.Api.Application.DTOs.Cliente;
using FlexPro.Domain.Entities;

namespace FlexPro.Api.Application.Mappings;

public class ClienteProfile : Profile
{
    public ClienteProfile()
    {
        CreateMap<ClienteRequestDTO, Cliente>();
        CreateMap<Cliente, ClienteResponseDTO>();
    }
}