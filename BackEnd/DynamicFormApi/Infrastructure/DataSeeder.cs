using DynamicFormApi.Aplication.DTOs;
using DynamicFormApi.Aplication.Services;

namespace DynamicFormApi.Infrastructure
{
    public static class DataSeeder
    {
        public static async Task SeedInitialDataAsync(FormService service)
        {

            var existingForms = await service.GetAllFormsAsync();
            if (existingForms.Any()) return;

            await service.CreateFormAsync(new CreateFormDto
            {
                Name = "Personas",
                Fields = new List<CreateFormFieldDto>
            {
                new CreateFormFieldDto { Label = "Nombres", FieldType = "text" },
                new CreateFormFieldDto { Label = "Apellido", FieldType = "text" },
                new CreateFormFieldDto { Label = "Fecha de Nacimiento", FieldType = "date" },
                new CreateFormFieldDto { Label = "Estatura (cm)", FieldType = "number" },
                new CreateFormFieldDto { Label = "Género", FieldType = "text" },
                new CreateFormFieldDto { Label = "Email", FieldType = "text" },
                new CreateFormFieldDto { Label = "Teléfono", FieldType = "number" }
            }
            });


            await service.CreateFormAsync(new CreateFormDto
            {
                Name = "Mascotas",
                Fields = new List<CreateFormFieldDto>
            {
                new CreateFormFieldDto { Label = "Nombre", FieldType = "text" },
                new CreateFormFieldDto { Label = "Especie", FieldType = "text" },
                new CreateFormFieldDto { Label = "Raza", FieldType = "text" },
                new CreateFormFieldDto { Label = "Color", FieldType = "text" },
                new CreateFormFieldDto { Label = "Edad", FieldType = "number" },
                new CreateFormFieldDto { Label = "Fecha de Adopción", FieldType = "date" },
                new CreateFormFieldDto { Label = "Vacunado", FieldType = "checkbox" }
            }
            });

        }
    }
}
