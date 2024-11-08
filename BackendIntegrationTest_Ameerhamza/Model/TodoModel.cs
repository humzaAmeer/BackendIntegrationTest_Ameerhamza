using System.ComponentModel.DataAnnotations.Schema;

namespace BackendIntegrationTest_Ameerhamza.Model
{
    public class TodoModel
    {
        public int Id { get; set; }

        /// <summary>
        /// Gets or Sets Todo
        /// </summary>
        public string? Todo { get; set; }

        /// <summary>
        /// Gets or Sets Completed
        /// </summary>
        public string? Completed { get; set; }

        /// <summary>
        /// Gets or Sets UserId
        /// </summary>
        public string? UserId { get; set; }

        /// <summary>
        /// Gets or Sets LIST CATEGORY
        /// </summary>
        public List<CategoryDetails> Category { get; set; } = new List<CategoryDetails>();

        /// <summary>
        /// Gets or Sets Priority
        /// </summary>
        public string? Priority { get; set; }

        /// <summary>
        /// Gets or Sets Location
        /// </summary>
        public string? Location { get; set; }


        /// <summary>
        /// Gets or Sets WeatherInfo
        /// </summary>
        [NotMapped]
        public string? WeatherInfo { get; set; }

        /// <summary>
        /// Gets or Sets DueDate
        /// </summary>
        public DateTime? DueDate { get; set; }

    }
}
