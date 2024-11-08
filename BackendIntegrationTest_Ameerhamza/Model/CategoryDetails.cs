namespace BackendIntegrationTest_Ameerhamza.Model
{
    public class CategoryDetails
    {
        /// <summary>
        /// Gets or Sets Id
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Gets or Sets Title
        /// </summary>
        public string? Title { get; set; }

        /// <summary>
        /// Gets or Sets ParentCategoryId
        /// </summary>

        public int? ParentCategoryId { get; set; }
    }
}
