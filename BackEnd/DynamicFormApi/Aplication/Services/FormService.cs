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

        public async Task<List<FormSummaryDto>> GetAllFormsAsync()
        {
            var forms = await _formRepository.GetAllAsync();
            return forms.Select(f => new FormSummaryDto
            {
                Id = f.Id,
                Name = f.Name
            }).ToList();
        }

        public async Task<FormDto?> GetFormByIdAsync(int id)
        {
            var form = await _formRepository.GetByIdAsync(id);
            if (form == null)
                throw new ArgumentException($"El formulario con ID {id} no existe");
            return MapToDto(form);
        }

        public async Task<FormDto> CreateFormAsync(CreateFormDto createFormDto)
        {
            if (string.IsNullOrWhiteSpace(createFormDto.Name))
                throw new ArgumentException("El nombre del formulario no puede estar vacío");
            if (createFormDto.Fields == null || createFormDto.Fields.Count < 1)
                throw new ArgumentException("El formulario debe tener al menos un campo");
            foreach (var field in createFormDto.Fields)
            {
                if (string.IsNullOrWhiteSpace(field.Label))
                    throw new ArgumentException("El nombre de la pregunta no puede estar vacía");
                if (string.IsNullOrWhiteSpace(field.FieldType))
                    throw new ArgumentException("El tipo de Pregunta no puede estar vacío");
            }

            var form = new Form(createFormDto.Name);
            var createdForm = await _formRepository.AddAsync(form);

            foreach (var fieldDto in createFormDto.Fields)
            {
                var field = new FormField(fieldDto.Label, fieldDto.FieldType);
                await _formRepository.AddFieldAsync(createdForm.Id, field);
            }

            var updatedForm = await _formRepository.GetByIdAsync(createdForm.Id);
            return MapToDto(updatedForm!);
        }

        public async Task UpdateFormAsync(int id, UpdateFormDto updateFormDto)
        {
            if (string.IsNullOrWhiteSpace(updateFormDto.Name))
                throw new ArgumentException("El nombre del formulario no puede estar vacío");
            if (updateFormDto.Fields == null || updateFormDto.Fields.Count < 1)
                throw new ArgumentException("El formulario debe tener al menos una pregunta");

            var form = await _formRepository.GetByIdAsync(id);
            if (form == null)
                throw new ArgumentException($"El formulario con ID {id} no existe");

            form.UpdateName(updateFormDto.Name);
            await _formRepository.UpdateAsync(form);

            var existingFields = form.Fields.ToList();
            var updatedFieldIds = updateFormDto.Fields.Where(f => f.Id.HasValue).Select(f => f.Id.Value).ToList();

            foreach (var existingField in existingFields)
            {
                if (!updatedFieldIds.Contains(existingField.Id))
                {
                    await _formRepository.DeleteFieldAsync(id, existingField.Id);
                }
            }

            foreach (var fieldDto in updateFormDto.Fields)
            {
                if (string.IsNullOrWhiteSpace(fieldDto.Label))
                    throw new ArgumentException("El nombre de la pregunta no puede estar vacía");
                if (string.IsNullOrWhiteSpace(fieldDto.FieldType))
                    throw new ArgumentException("El tipo de pregunta no puede estar vacío");

                if (fieldDto.Id.HasValue)
                {
                    var field = existingFields.FirstOrDefault(f => f.Id == fieldDto.Id.Value);
                    if (field != null)
                    {
                        field.Update(fieldDto.Label, fieldDto.FieldType);
                        await _formRepository.UpdateFieldAsync(id, field);
                    }
                    else
                    {
                        throw new ArgumentException($"\r\nLa pregunta con ID {fieldDto.Id} no existe en el formulario {id}");
                    }
                }
                else
                {
                    var field = new FormField(fieldDto.Label, fieldDto.FieldType);
                    await _formRepository.AddFieldAsync(id, field);
                }
            }
        }

        public async Task DeleteFormAsync(int id)
        {
            var form = await _formRepository.GetByIdAsync(id);
            if (form == null)
                throw new ArgumentException($"El formulario con ID {id} no existe");

            await _formRepository.DeleteAsync(id);
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
