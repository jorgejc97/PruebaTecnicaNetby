namespace DynamicFormApi.Aplication.DTOs
{
    public class FormDto
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public List<FormFieldDto> Fields { get; set; } = [];
    }
}
