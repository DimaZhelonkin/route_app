using AutoMapper;
using ApplicationInitCallResponse = Ark.IdentityServer.Application.UCaller.Features.Commands.InitCall.InitCallResponse;
using ApplicationInitRepeatResponse =
    Ark.IdentityServer.Application.UCaller.Features.Commands.InitRepeat.InitRepeatResponse;
using ApplicationCheckPhoneResponse =
    Ark.IdentityServer.Application.UCaller.Features.Commands.CheckPhone.CheckPhoneResponse;
using ApplicationGetInfoResponse = Ark.IdentityServer.Application.UCaller.Features.Commands.GetInfo.GetInfoResponse;
using ApplicationCheckHealthResponse =
    Ark.IdentityServer.Application.UCaller.Features.Commands.CheckHealth.CheckHealthResponse;
using InfrastructureInitCallResponse = Ark.IdentityServer.Infrastructure.UCaller.Models.Responses.InitCallResponse;
using InfrastructureInitRepeatResponse = Ark.IdentityServer.Infrastructure.UCaller.Models.Responses.InitRepeatResponse;
using InfrastructureCheckPhoneResponse = Ark.IdentityServer.Infrastructure.UCaller.Models.Responses.CheckPhoneResponse;
using InfrastructureGetInfoResponse = Ark.IdentityServer.Infrastructure.UCaller.Models.Responses.GetInfoResponse;
using InfrastructureCheckHealthResponse =
    Ark.IdentityServer.Infrastructure.UCaller.Models.Responses.CheckHealthResponse;

namespace Ark.IdentityServer.Infrastructure.UCaller.Mappings;

public class UCallerProfile : Profile
{
    public UCallerProfile()
    {
        CreateMap<InfrastructureInitCallResponse, ApplicationInitCallResponse>();
        CreateMap<InfrastructureInitRepeatResponse, ApplicationInitRepeatResponse>();
        CreateMap<InfrastructureCheckPhoneResponse, ApplicationCheckPhoneResponse>();
        CreateMap<InfrastructureGetInfoResponse, ApplicationGetInfoResponse>();
        CreateMap<ApplicationCheckHealthResponse, InfrastructureCheckHealthResponse>();
    }
}