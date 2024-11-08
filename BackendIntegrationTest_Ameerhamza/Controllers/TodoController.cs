using BackendIntegrationTest_Ameerhamza.Configuration;
using BackendIntegrationTest_Ameerhamza.Model;
using BackendIntegrationTest_Ameerhamza.Services.TodoList;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Runtime.CompilerServices;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace BackendIntegrationTest_Ameerhamza.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TodoController : ControllerBase
    {
        public ITodoService _todoService;
        private readonly DbContextOptions<TodoDbContext> _dbContextOptions;
        public TodoController(ITodoService todoService, DbContextOptions<TodoDbContext> todoDbContext)
        {
            this._todoService = todoService;
            this._dbContextOptions = todoDbContext;

        }


        /// <summary>
        /// Gets FetchTodosFromApi
        /// </summary>
        /// <param name="projectId">ProjectId</param>
        /// <returns>EquipmentTag</returns>
        [HttpPost]
        [Route("fetch")]
        public async Task<IActionResult> FetchTodosFromApi()
        {
            string apiUrl = "https://dummyjson.com/todos"; // URL to fetch the JSON data

            // Fetch the JSON data from the API
            var todoList = await _todoService.GetTodosFromApi(apiUrl);

            if (todoList == null)
            {
                return NotFound("Could not fetch data from the API.");
            }

            // Insert data into the database using Entity Framework
            await _todoService.InsertTodosIntoDatabase(todoList);

            return Ok("Todos have been successfully inserted into the database.");

        }

        /// <summary>
        /// Create 
        /// </summary>
        /// <param name="todoModel"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("Create")]
        public async Task<TodoModel> CreateTodoItems(TodoModel todoModel)
        {
            var result = await this._todoService.Create(todoModel);
            return result;

        }

        /// <summary>
        /// Update 
        /// </summary>
        /// <param name="todoModel"></param>
        /// <param name="todoId"></param>
        /// <returns></returns>
        [HttpPut]
        [Route("Update")]
        public async Task<TodoModel> UpdateTodoItems(TodoModel todoModel, int todoId)
        {

            var result = await this._todoService.Update(todoModel, todoId);
            return result;


        }


        /// <summary>
        /// Get By ID
        /// </summary>
        /// <param name="todoId"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("Get")]
        public async Task<TodoModel> Get(int todoId)
        {
            var result = await this._todoService.GetById( todoId);
            return result;
        }

        /// <summary>
        /// Delete By ID
        /// </summary>
        /// <param name="todoId"></param>
        /// <returns></returns>
        [HttpDelete("{id}")]
        public async Task Delete(int todoId)
        {
             await this._todoService.Delete(todoId);
            return;
        }
    }
}
