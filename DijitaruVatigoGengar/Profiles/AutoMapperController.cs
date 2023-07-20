using AutoMapper;
using DijitaruVatigoGengar.Data.Dtos;
using DijitaruVatigoGengar.Models;

namespace DijitaruVatigoGengar.Profiles;

public class AutoMapperProfile : Profile
{
    public AutoMapperProfile()
    {
        CreateMap<Collaborator, CreateCollaboratorDto>().ReverseMap();
        CreateMap<Collaborator, UpdateCollaboratorDto>().ReverseMap();
        CreateMap<Collaborator, ReadCollaboratorDto>();
        CreateMap<CreateCollaboratorDto, Collaborator>();

        CreateMap<Project, CreateProjectDto>().ReverseMap();
        CreateMap<Project, UpdateProjectDto>().ReverseMap();
        CreateMap<Project, ReadProjectDto>();
        CreateMap<CreateProjectDto, Project>();

        CreateMap<PendingHour, CreatePendingHourDto>().ReverseMap();
        CreateMap<PendingHour, UpdatePendingHourDto>().ReverseMap();
        CreateMap<PendingHour, ReadPendingHourDto>();
        CreateMap<CreatePendingHourDto, PendingHour>();
    }
}
