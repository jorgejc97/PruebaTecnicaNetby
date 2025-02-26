using DynamicFormApi.Aplication.Services;

namespace DynamicFormApi.Data
{
    public static class DataSeeder
    {
        public static async Task SeedInitialDataAsync(FormService service)
        {
            // Verificar si ya hay datos
            var existingForms = await service.GetAllFormsAsync();
            if (existingForms.Any()) return;

            // Formulario 1: Personas
            await service.CreateFormAsync("Personas");
            await service.AddFieldAsync(1, "Nombres", "text");
            await service.AddFieldAsync(1, "Fecha de Nacimiento", "date");
            await service.AddFieldAsync(1, "Estatura", "number");

            // Formulario 2: Mascotas
            await service.CreateFormAsync("Mascotas");
            await service.AddFieldAsync(2, "Especie", "text");
            await service.AddFieldAsync(2, "Raza", "text");
            await service.AddFieldAsync(2, "Color", "text");
            await service.AddFieldAsync(2, "Nombre", "text");

        }
    }
}
