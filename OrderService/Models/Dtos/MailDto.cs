namespace OrderService.Models.Dtos
{
    public class MailDto
    {
        public Guid OrderId { get; set; }
        public Double OrderAmount { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
    }
}
