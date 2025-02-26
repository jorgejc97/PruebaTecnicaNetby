using DynamicFormApi.Aplication.DTOs;
using DynamicFormApi.Aplication.Services;

namespace DynamicFormApi.Api
{
    public static class FormEndpoints
    {
        public static void MapFormEndpoints(this IEndpointRouteBuilder app)
        {
            app.MapGet("/forms", async (FormService service) =>
                Results.Ok(await service.GetAllFormsAsync()))
                .WithName("GetAllForms")
                .WithOpenApi();

            app.MapGet("/forms/{id}", async (int id, FormService service) =>
            {
                var form = await service.GetFormByIdAsync(id);
                return form != null ? Results.Ok(form) : Results.NotFound();
            })
            .WithName("GetFormById")
            .WithOpenApi();

            app.MapPost("/forms", async (string name, FormService service) =>
            {
                var form = await service.CreateFormAsync(name);
                return Results.Created($"/forms/{form.Id}", form);
            })
            .WithName("CreateForm")
            .WithOpenApi();

            app.MapPut("/forms/{id}", async (int id, string name, FormService service) =>
            {
                await service.UpdateFormAsync(id, name);
                return Results.NoContent();
            })
            .WithName("UpdateForm")
            .WithOpenApi();

            app.MapDelete("/forms/{id}", async (int id, FormService service) =>
            {
                await service.DeleteFormAsync(id);
                return Results.NoContent();
            })
            .WithName("DeleteForm")
            .WithOpenApi();

            app.MapPost("/forms/{formId}/fields", async (int formId, FormFieldDto fieldDto, FormService service) =>
            {
                var field = await service.AddFieldAsync(formId, fieldDto.Label, fieldDto.FieldType);
                return Results.Created($"/forms/{formId}/fields/{field.Id}", field);
            })
            .WithName("AddField")
            .WithOpenApi();

            app.MapPut("/forms/{formId}/fields/{fieldId}", async (int formId, int fieldId, FormFieldDto fieldDto, FormService service) =>
            {
                await service.UpdateFieldAsync(formId, fieldId, fieldDto.Label, fieldDto.FieldType);
                return Results.NoContent();
            })
            .WithName("UpdateField")
            .WithOpenApi();

            app.MapDelete("/forms/{formId}/fields/{fieldId}", async (int formId, int fieldId, FormService service) =>
            {
                await service.DeleteFieldAsync(formId, fieldId);
                return Results.NoContent();
            })
            .WithName("DeleteField")
            .WithOpenApi();
        }
    }
}
