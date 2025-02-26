using DynamicFormApi.Aplication.DTOs;
using DynamicFormApi.Domain.Entities;
using DynamicFormApi.Domain.Interfaces;

namespace DynamicFormApi.Aplication.Services
{
    public class FormService
    {
        private readonly IFormRepository _formRepository;

        public FormService(IFormRepository formRepository)
        {
            _formRepository = formRepository ?? throw new ArgumentNullException(nameof(formRepository));
        }

        public async Task<List<FormDto>> GetAllFormsAsync()
        {
            var forms = await _formRepository.GetAllAsync();
            return forms.Select(MapToDto).ToList();
        }

        public async Task<FormDto?> GetFormByIdAsync(int id)
        {
            var form = await _formRepository.GetByIdAsync(id);
            return form != null ? MapToDto(form) : null;
        }

        public async Task<FormDto> CreateFormAsync(string name)
        {
            var form = new Form(name);
            var createdForm = await _formRepository.AddAsync(form);
            return MapToDto(createdForm);
        }

        public async Task UpdateFormAsync(int id, string name)
        {
            var form = await _formRepository.GetByIdAsync(id);
            if (form != null)
            {
                form.UpdateName(name);
                await _formRepository.UpdateAsync(form);
            }
        }

        public async Task DeleteFormAsync(int id)
        {
            await _formRepository.DeleteAsync(id);
        }

        public async Task<FormFieldDto> AddFieldAsync(int formId, string label, string fieldType)
        {
            var field = new FormField(label, fieldType);
            var createdField = await _formRepository.AddFieldAsync(formId, field);
            return MapToFieldDto(createdField);
        }

        public async Task UpdateFieldAsync(int formId, int fieldId, string label, string fieldType)
        {
            var form = await _formRepository.GetByIdAsync(formId);
            if (form != null)
            {
                var field = form.Fields.FirstOrDefault(f => f.Id == fieldId);
                if (field != null)
                {
                    field.Update(label, fieldType);
                    await _formRepository.UpdateFieldAsync(formId, field);
                }
            }
        }

        public async Task DeleteFieldAsync(int formId, int fieldId)
        {
            await _formRepository.DeleteFieldAsync(formId, fieldId);
        }

        private static FormDto MapToDto(Form form) => new()
        {
            Id = form.Id,
            Name = form.Name,
            Fields = form.Fields.Select(MapToFieldDto).ToList()
        };

        private static FormFieldDto MapToFieldDto(FormField field) => new()
        {
            Id = field.Id,
            Label = field.Label,
            FieldType = field.FieldType
        };
    }
}
