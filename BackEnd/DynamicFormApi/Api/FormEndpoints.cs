using DynamicFormApi.Aplication.DTOs;
using DynamicFormApi.Aplication.Services;
using DynamicFormApi.Application.Services;
using Microsoft.OpenApi.Any;
using Microsoft.OpenApi.Models;

namespace DynamicFormApi.Api;

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

        app.MapPost("/forms", async (CreateFormDto createFormDto, FormService service) =>
        {
            var form = await service.CreateFormAsync(createFormDto);
            return Results.Created($"/forms/{form.Id}", form);
        })
        .WithName("CreateForm")
        .WithOpenApi();

        app.MapPut("/forms/{id}", async (int id, UpdateFormDto updateFormDto, FormService service) =>
        {
            await service.UpdateFormAsync(id, updateFormDto);
            return Results.NoContent();
        })
        .WithName("UpdateForm")
        .WithOpenApi(operation =>
        {
            operation.Description = "Actualiza un formulario completo (nombre y campos). Los campos existentes se actualizan, nuevos se agregan, y los no enviados se eliminan.";
            operation.Parameters[0].Description = "ID del formulario.";
            operation.RequestBody = new OpenApiRequestBody
            {
                Content = new Dictionary<string, OpenApiMediaType>
                {
                    ["application/json"] = new OpenApiMediaType
                    {
                        Schema = new OpenApiSchema { Type = "object" },
                        Example = new OpenApiObject
                        {
                            ["name"] = new OpenApiString("Formulario Modificado"),
                            ["fields"] = new OpenApiArray
                            {
                                new OpenApiObject
                                {
                                    ["id"] = new OpenApiInteger(1),
                                    ["label"] = new OpenApiString("Pregunta 1 Modificada"),
                                    ["fieldType"] = new OpenApiString("text")
                                },
                                new OpenApiObject
                                {
                                    ["label"] = new OpenApiString("Pregunta Nueva"),
                                    ["fieldType"] = new OpenApiString("number")
                                }
                            }
                        }
                    }
                }
            };
            return operation;
        });

        app.MapDelete("/forms/{id}", async (int id, FormService service) =>
        {
            await service.DeleteFormAsync(id);
            return Results.NoContent();
        })
        .WithName("DeleteForm")
        .WithOpenApi();

        app.MapPost("/forms/{formId}/responses", async (int formId, FormResponseDto responseDto, FormResponseService service) =>
        {
            var response = await service.AddResponseAsync(formId, responseDto);
            return Results.Created($"/forms/{formId}/responses/{response.Id}", response);
        })
        .WithName("AddResponse")
        .WithOpenApi();

        app.MapGet("/forms/{formId}/responses", async (int formId, FormResponseService service) =>
        {
            var responses = await service.GetResponsesAsync(formId);
            return Results.Ok(responses);
        })
        .WithName("GetResponses")
        .WithOpenApi();
    }
}