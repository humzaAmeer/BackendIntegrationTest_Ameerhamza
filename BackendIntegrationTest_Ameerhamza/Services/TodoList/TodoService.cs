
using BackendIntegrationTest_Ameerhamza.Configuration;
using BackendIntegrationTest_Ameerhamza.Model;
using BackendIntegrationTest_Ameerhamza.Services.WeatherService;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Newtonsoft.Json.Linq;
using System;

namespace BackendIntegrationTest_Ameerhamza.Services.TodoList
{
    public class TodoService : ITodoService
    {
        public DbContextOptions<TodoDbContext> _dbContextOptions;
        public TodoDbContext _dbContext;
        public IWeatherService weatherService;

        public TodoService(DbContextOptions<TodoDbContext> todoDbContext, TodoDbContext todoDb, IWeatherService weatherService)
        {

            this._dbContextOptions = todoDbContext;
            this._dbContext = todoDb;
            this.weatherService = weatherService;
        }




        public async Task<TodoModel> Create(TodoModel todoItems)
        {
            if (todoItems == null)
            {
                throw new ArgumentNullException(nameof(todoItems), "TodoItem cannot be null");
            }

            var val = new TodoModel
            {
                Id = todoItems.Id,
                Todo = todoItems.Todo,
                Location = todoItems.Location,
                Completed = todoItems.Completed,
                DueDate = todoItems.DueDate,
                Priority = todoItems.Priority,
                UserId = todoItems.UserId,
                Category = new List<CategoryDetails>() // Initialize the Category list
            };

            // Map the categories to val.Category
            todoItems.Category.ForEach(x =>
            {
                var categoryDetail = new CategoryDetails
                {
                    Id = x.Id,
                    Title = x.Title,
                    ParentCategoryId = val.Id // Link category to Todo by ParentCategoryId
                };
                val.Category.Add(categoryDetail); // Directly add to val.Category
            });

            // Add the CategoryDetails to the database
            await this._dbContext.categoryDetails.AddRangeAsync(val.Category);
            await this._dbContext.SaveChangesAsync();

            // Add the TodoModel to the database
            await this._dbContext.todoTable.AddAsync(val);
            await this._dbContext.SaveChangesAsync();

            return val;
        }

        public async Task<TodoModel> Update(TodoModel todoItems, int todoId)
        {
            var val = this._dbContext.todoTable.FirstOrDefault(x => x.Id == todoId);


            val.Id = todoItems.Id;
            val.Todo = todoItems.Todo;
            val.Location = todoItems.Location;
            val.Completed = todoItems.Completed;
            val.Category = todoItems.Category;
            val.DueDate = todoItems.DueDate;
            val.Priority = todoItems.Priority;
            val.UserId = todoItems.UserId;

            var context = new TodoDbContext(this._dbContextOptions);
            // Add the single TodoItem to the DbSet
            context.todoTable.Update(val);

            // Save changes to the database
            await context.SaveChangesAsync();
            return val;
        }


        public async Task<TodoModel> GetById(int todoId)
        {
            var Item = this._dbContext.todoTable.FirstOrDefault(x => x.Id == todoId);

            var currentWeather = await weatherService.GetCurrentWeather(Item.Location);

            // Create a To-Do item with weather info
            var todoItem = new TodoModel
            {
                Id = Item.Id,
                Location = Item.Location,
                Priority = Item.Priority,
                Todo = Item.Todo,
                UserId = Item.UserId,
                DueDate = Item.DueDate,
                Category = Item.Category,
                WeatherInfo = currentWeather.Text
            };
            return todoItem;
        }

        public async Task Delete(int todoId)
        {
            var Item = await this._dbContext.todoTable.FindAsync(todoId);
            if (Item != null)
            {
                this._dbContext.todoTable.Remove(Item);
                await _dbContext.SaveChangesAsync();
                Console.WriteLine("Successfully Deleted");
            }

            return;
        }
        // Fetch JSON data from the API and deserialize it into a list of Todo objects
        public async Task<TodoModel[]> GetTodosFromApi(string apiUrl)
        {
            using (var client = new HttpClient())
            {
                var response = await client.GetStringAsync(apiUrl);
                var json = JObject.Parse(response);
                var todos = json["todos"].ToObject<TodoModel[]>();
                foreach (var todo in todos)
                {
                    // You can assign static/default values or derive values based on existing properties.

                    var categories = await GetCategoriesForTodo(todo);

                    // Assign the categories list to the todo
                    todo.Category = categories;
                    todo.Location = "No location";     // Static value
                    todo.Priority = "3";          // Default priority
                    todo.DueDate = DateTime.UtcNow.AddDays(7); // Default due date, 7 days from today
                }
                return todos;
            }
        }

        private async Task<List<CategoryDetails>> GetCategoriesForTodo(TodoModel todo)
        {
            // Example of fetching categories based on some logic (e.g., UserId or default categories)
            List<CategoryDetails> categories = new List<CategoryDetails>();

            // Example logic: Assign categories based on some conditions
            if (todo.UserId == "1")  // If the UserId is 1, assign "Work" category
            {
                var contexts = new TodoDbContext(this._dbContextOptions);

                var workCategory = await contexts.categoryDetails
                    .FirstOrDefaultAsync(c => c.Title == "Work");

                if (workCategory != null)
                {
                    categories.Add(workCategory);
                }
            }

            // Add other categories based on different logic or default categories
            var context = new TodoDbContext(this._dbContextOptions);

            var personalCategory = await context.categoryDetails.FirstOrDefaultAsync(c => c.Title == "Personal");

            if (personalCategory != null)
            {
                categories.Add(personalCategory);
            }

            return categories;
        }


        // Insert the fetched todos into the SQL Server database using EF
        public async Task InsertTodosIntoDatabase(TodoModel[] todos)
        {
            var context = new TodoDbContext(this._dbContextOptions);
            // Check if the database is empty, then insert the data
            if (!context.todoTable.Any())
            {
                await context.todoTable.AddRangeAsync(todos);
                var value = await context.SaveChangesAsync();
            }
            else
            {
                Console.WriteLine("Todos data already exists in the database.");
            }

        }
    }

}

