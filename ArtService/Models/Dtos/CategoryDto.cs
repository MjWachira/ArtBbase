namespace ArtService.Models.Dtos
{
    public class CategoryDto
    {
        public Guid Id { get; set; }
        public string CategoryName { get; set; } = string.Empty;
        public string CategoryImage { get; set; } = string.Empty;
    }
}
