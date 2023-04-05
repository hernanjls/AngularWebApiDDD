using TEST.Application.Commons.Bases;
using TEST.Application.Dtos.Task.Request;
using TEST.Application.Dtos.Task.Response;
using TEST.Infrastructure.Commons.Bases.Request;
using TEST.Infrastructure.Commons.Bases.Response;

namespace TEST.Application.Interfaces
{
    public interface ITaskApplication
    {
        Task<BaseResponse<BaseEntityResponse<TaskResponseDto>>> ListTasks(BaseFiltersRequest filters);
        Task<BaseResponse<IEnumerable<TaskSelectResponseDto>>> ListSelectTasks();
        Task<BaseResponse<TaskResponseDto>> TaskById(int taskId);
        Task<BaseResponse<bool>> RegisterTask(TaskRequestDto requestDto);
        Task<BaseResponse<bool>> EditTask(int taskId, TaskRequestDto requestDto);
        Task<BaseResponse<bool>> RemoveTask(int taskId);
    }
}