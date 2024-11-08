using BackendIntegrationTest_Ameerhamza.Model;

namespace BackendIntegrationTest_Ameerhamza.Services.TodoList
{
    public interface ITodoService
    {

        /// <summary>
        /// Create's jobs for a specified project
        /// </summary>
        /// <param name="jobs">job detail</param>
        /// <returns>bool</returns>
        public Task<TodoModel> Create(TodoModel todoItems);

        /// <summary>
        /// Update's jobs for a specified project
        /// </summary>
        /// <param name="jobs">job detail</param>
        /// <returns>bool</returns>
        public Task<TodoModel> Update(TodoModel todoItems, int todoId);


        public  Task InsertTodosIntoDatabase(TodoModel[] todos);


        public  Task<TodoModel[]> GetTodosFromApi(string apiUrl);

        public Task<TodoModel> GetById(int todoId);

        public Task Delete(int todoId);
    }
}
