namespace DynamicFormApi.Domain.Entities
{
    public class FormField
    {
        public int Id { get; set; }
        public string Label { get; private set; }
        public string FieldType { get; private set; }
        public int FormId { get; set; }
        public Form Form { get; set; } = null!;

        public FormField(string label, string fieldType)
        {
            if (string.IsNullOrWhiteSpace(label)) throw new ArgumentException("Label cannot be empty", nameof(label));
            if (string.IsNullOrWhiteSpace(fieldType)) throw new ArgumentException("FieldType cannot be empty", nameof(fieldType));
            Label = label;
            FieldType = fieldType;
        }

        public void Update(string label, string fieldType)
        {
            if (!string.IsNullOrWhiteSpace(label)) Label = label;
            if (!string.IsNullOrWhiteSpace(fieldType)) FieldType = fieldType;
        }
    }
}
