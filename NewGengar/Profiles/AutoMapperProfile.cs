
using AutoMapper;
using NewGengar.Data.Dtos;
using NewGengar.Models;
namespace NewGengar.Profiles;

public class AutoMapperProfile : Profile
{
    public AutoMapperProfile()
    {
        CreateMap<Collaborator, CreateCollaboratorDto>().ReverseMap();
        CreateMap<Collaborator, UpdateCollaboratorDto>().ReverseMap();
        CreateMap<Collaborator, ReadCollaboratorDto>();

        CreateMap<Project, CreateProjectDto>().ReverseMap();
        CreateMap<Project, UpdateProjectDto>().ReverseMap();
        CreateMap<Project, ReadProjectDto>();

        CreateMap<PendingHour, CreatePendingHourDto>().ReverseMap();
        CreateMap<PendingHour, UpdatePendingHourDto>().ReverseMap();
        CreateMap<PendingHour, ReadPendingHourDto>();

        CreateMap<PendingHour, CreateApprovedHourDto>().ReverseMap();
        CreateMap<PendingHour, UpdateApprovedHourDto>().ReverseMap();
        CreateMap<PendingHour, ReadApprovedHourDto>();
    }
}
