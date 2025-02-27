namespace DynamicFormApi.Domain.Entities
{
    public class FormResponse
    {
        public int Id { get; set; }
        public int FormId { get; set; }
        public string ResponseData { get; set; } = string.Empty;
        public Form Form { get; set; } = null!;
    }
}
