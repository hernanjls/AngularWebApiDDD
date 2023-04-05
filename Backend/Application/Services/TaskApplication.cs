using AutoMapper;
using TEST.Application.Commons.Bases;
using TEST.Application.Dtos.Task.Request;
using TEST.Application.Dtos.Task.Response;
using TEST.Application.Interfaces;
using TEST.Application.Validators.Category;
using TEST.Domain.Entities;
using TEST.Infrastructure.Commons.Bases.Request;
using TEST.Infrastructure.Commons.Bases.Response;
using TEST.Infrastructure.Persistences.Interfaces;
using TEST.Utilities.Static;
using WatchDog;

namespace TEST.Application.Services
{
    public class TaskApplication : ITaskApplication
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly TaskValidator _validationRules;

        public TaskApplication(IUnitOfWork unitOfWork, IMapper mapper, TaskValidator validationRules)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _validationRules = validationRules;
        }

        public async Task<BaseResponse<BaseEntityResponse<TaskResponseDto>>> ListTasks(BaseFiltersRequest filters)
        {
            var response = new BaseResponse<BaseEntityResponse<TaskResponseDto>>();

            try
            {
                var categories = await _unitOfWork.Task.ListTasks(filters);

                if (categories is not null)
                {
                    response.IsSuccess = true;
                    response.Data = _mapper.Map<BaseEntityResponse<TaskResponseDto>>(categories);
                    response.Message = ReplyMessage.MESSAGE_QUERY;
                }
                else
                {
                    response.IsSuccess = false;
                    response.Message = ReplyMessage.MESSAGE_QUERY_EMPTY;
                }
            }
            catch (Exception ex)
            {
                response.IsSuccess = false;
                response.Message = ReplyMessage.MESSAGE_EXCEPTION;
                WatchLogger.Log(ex.Message);
            }

            return response;
        }

        public async Task<BaseResponse<IEnumerable<TaskSelectResponseDto>>> ListSelectTasks()
        {
            var response = new BaseResponse<IEnumerable<TaskSelectResponseDto>>();

            try
            {
                var tasks = await _unitOfWork.Task.GetAllAsync();

                if (tasks is not null)
                {
                    response.IsSuccess = true;
                    response.Data = _mapper.Map<IEnumerable<TaskSelectResponseDto>>(tasks);
                    response.Message = ReplyMessage.MESSAGE_QUERY;
                }
                else
                {
                    response.IsSuccess = false;
                    response.Message = ReplyMessage.MESSAGE_QUERY_EMPTY;
                }
            }
            catch (Exception ex)
            {
                response.IsSuccess = false;
                response.Message = ReplyMessage.MESSAGE_EXCEPTION;
                WatchLogger.Log(ex.Message);
            }

            return response;
        }

        public async Task<BaseResponse<TaskResponseDto>> TaskById(int taskId)
        {
            var response = new BaseResponse<TaskResponseDto>();

            try
            {
                var task = await _unitOfWork.Task.GetByIdAsync(taskId);

                if (task is not null)
                {
                    response.IsSuccess = true;
                    response.Data = _mapper.Map<TaskResponseDto>(task);
                    response.Message = ReplyMessage.MESSAGE_QUERY;
                }
                else
                {
                    response.IsSuccess = false;
                    response.Message = ReplyMessage.MESSAGE_QUERY_EMPTY;
                }
            }
            catch (Exception ex)
            {
                response.IsSuccess = false;
                response.Message = ReplyMessage.MESSAGE_EXCEPTION;
                WatchLogger.Log(ex.Message);
            }

            return response;
        }

        public async Task<BaseResponse<bool>> RegisterTask(TaskRequestDto requestDto)
        {
            var response = new BaseResponse<bool>();

            try
            {
                var validationResult = await _validationRules.ValidateAsync(requestDto);

                if (!validationResult.IsValid)
                {
                    response.IsSuccess = false;
                    response.Message = ReplyMessage.MESSAGE_VALIDATE;
                    response.Errors = validationResult.Errors;
                    return response;
                }

                var category = _mapper.Map<Domain.Entities.TaskEntity>(requestDto);
                response.Data = await _unitOfWork.Task.RegisterAsync(category);

                if (response.Data)
                {
                    response.IsSuccess = true;
                    response.Message = ReplyMessage.MESSAGE_SAVE;
                }
                else
                {
                    response.IsSuccess = false;
                    response.Message = ReplyMessage.MESSAGE_FAILED;
                }
            }
            catch (Exception ex)
            {
                response.IsSuccess = false;
                response.Message = ReplyMessage.MESSAGE_EXCEPTION;
                WatchLogger.Log(ex.Message);
            }

            return response;
        }

        public async Task<BaseResponse<bool>> EditTask(int taskId, TaskRequestDto requestDto)
        {
            var response = new BaseResponse<bool>();

            try
            {
                var categoryEdit = await TaskById(taskId);

                if (categoryEdit.Data is null)
                {
                    response.IsSuccess = false;
                    response.Message = ReplyMessage.MESSAGE_QUERY_EMPTY;
                    return response;
                }

                var category = _mapper.Map<Domain.Entities.TaskEntity>(requestDto);
                category.Id = taskId;
                response.Data = await _unitOfWork.Task.EditAsync(category);

                if (response.Data)
                {
                    response.IsSuccess = true;
                    response.Message = ReplyMessage.MESSAGE_UPDATE;
                }
                else
                {
                    response.IsSuccess = false;
                    response.Message = ReplyMessage.MESSAGE_FAILED;
                }
            }
            catch (Exception ex)
            {
                response.IsSuccess = false;
                response.Message = ReplyMessage.MESSAGE_EXCEPTION;
                WatchLogger.Log(ex.Message);
            }

            return response;
        }

        public async Task<BaseResponse<bool>> RemoveTask(int taskId)
        {
            var response = new BaseResponse<bool>();

            try
            {
                var category = await TaskById(taskId);

                if (category.Data is null)
                {
                    response.IsSuccess = false;
                    response.Message = ReplyMessage.MESSAGE_QUERY_EMPTY;
                    return response;
                }

                response.Data = await _unitOfWork.Task.RemoveAsync(taskId);

                if (response.Data)
                {
                    response.IsSuccess = true;
                    response.Message = ReplyMessage.MESSAGE_DELETE;
                }
                else
                {
                    response.IsSuccess = false;
                    response.Message = ReplyMessage.MESSAGE_FAILED;
                }
            }
            catch (Exception ex)
            {
                response.IsSuccess = false;
                response.Message = ReplyMessage.MESSAGE_EXCEPTION;
                WatchLogger.Log(ex.Message);
            }

            return response;
        }
    }
}