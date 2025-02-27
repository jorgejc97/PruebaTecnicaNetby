namespace DynamicFormApi.Aplication.DTOs
{
    public class UpdateFormDto
    {
        public string Name { get; set; } = string.Empty;
        public List<UpdateFormFieldDto> Fields { get; set; } = new();
    }

    public class UpdateFormFieldDto
    {
        public int? Id { get; set; }
        public string Label { get; set; } = string.Empty;
        public string FieldType { get; set; } = string.Empty;
    }
}
