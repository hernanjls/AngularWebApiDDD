using AutoMapper;
using TEST.Application.Dtos.Task.Request;
using TEST.Application.Dtos.Task.Response;
using TEST.Domain.Entities;
using TEST.Infrastructure.Commons.Bases.Response;
using TEST.Utilities.Static;

namespace TEST.Application.Mappers
{
    public class TaskMappingsProfile : Profile
    {
        public TaskMappingsProfile()
        {
            CreateMap<Domain.Entities.TaskEntity, TaskResponseDto>()
                .ForMember(x => x.TaskId, x => x.MapFrom(y => y.Id))
                .ForMember<string>(x => x.StateTask, x => x.MapFrom(y => y.State.Equals((int)StateTypes.Active) ? "Activo" : "Inactivo"))
                .ReverseMap();

            CreateMap<BaseEntityResponse<Domain.Entities.TaskEntity>, BaseEntityResponse<TaskResponseDto>>()
                .ReverseMap();

            CreateMap<TaskRequestDto, Domain.Entities.TaskEntity>();

            CreateMap<Domain.Entities.TaskEntity, TaskSelectResponseDto>()
                .ForMember(x => x.TaskId, x => x.MapFrom(y => y.Id))
                .ReverseMap();
        }
    }
}