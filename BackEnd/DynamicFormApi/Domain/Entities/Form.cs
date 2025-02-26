namespace DynamicFormApi.Domain.Entities
{
    public class Form
    {
        public int Id { get; set; }
        public string Name { get; private set; }
        public List<FormField> Fields { get; private set; } = [];

        public Form(string name)
        {
            if (string.IsNullOrWhiteSpace(name)) throw new ArgumentException("Name cannot be empty", nameof(name));
            Name = name;
        }

        public void UpdateName(string newName)
        {
            if (string.IsNullOrWhiteSpace(newName)) throw new ArgumentException("New name cannot be empty", nameof(newName));
            Name = newName;
        }

        public void AddField(FormField field)
        {
            Fields.Add(field);
        }
    }
}
