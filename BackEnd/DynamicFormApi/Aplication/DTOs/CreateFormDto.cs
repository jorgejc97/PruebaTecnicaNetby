namespace DynamicFormApi.Aplication.DTOs
{
    public class CreateFormDto
    {
        public string Name { get; set; } = string.Empty;
        public List<CreateFormFieldDto> Fields { get; set; } = new();
    }

    public class CreateFormFieldDto
    {
        public string Label { get; set; } = string.Empty;
        public string FieldType { get; set; } = string.Empty;
    }
}
