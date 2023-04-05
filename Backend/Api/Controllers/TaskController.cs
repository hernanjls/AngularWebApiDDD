using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TEST.Application.Dtos.Task.Request;
using TEST.Application.Interfaces;
using TEST.Infrastructure.Commons.Bases.Request;

namespace TEST.Api.Controllers
{
    //[Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class TaskController : ControllerBase
    {
        private readonly ITaskApplication _taskApplication;

        public TaskController(ITaskApplication taskApplication)
        {
            _taskApplication = taskApplication;
        }

        [HttpPost]
        public async Task<IActionResult> ListTasks([FromBody] BaseFiltersRequest filters)
        {
            var response = await _taskApplication.ListTasks(filters);
            return Ok(response);
        }

        [HttpGet("Select")]
        public async Task<IActionResult> ListSelectTasks()
        {
            var response = await _taskApplication.ListSelectTasks();
            return Ok(response);
        }

        [HttpGet("{taskId:int}")]
        public async Task<IActionResult> TaskById(int taskId)
        {
            var response = await _taskApplication.TaskById(taskId);
            return Ok(response);
        }

        [HttpPost("Register")]
        public async Task<IActionResult> RegisterCategory([FromBody] TaskRequestDto requestDto)
        {
            var response = await _taskApplication.RegisterTask(requestDto);
            return Ok(response);
        }

        [HttpPut("Edit/{taskId:int}")]
        public async Task<IActionResult> EditTask(int taskId, [FromBody] TaskRequestDto requestDto)
        {
            var response = await _taskApplication.EditTask(taskId, requestDto);
            return Ok(response);
        }

        [HttpPut("Remove/{taskId:int}")]
        public async Task<IActionResult> RemoveCategory(int taskId)
        {
            var response = await _taskApplication.RemoveTask(taskId);
            return Ok(response);
        }
    }
}