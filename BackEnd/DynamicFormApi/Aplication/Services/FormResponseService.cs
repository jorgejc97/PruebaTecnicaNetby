using DynamicFormApi.Aplication.DTOs;
using DynamicFormApi.Domain.Entities;
using DynamicFormApi.Domain.Interfaces;

namespace DynamicFormApi.Application.Services;

public class FormResponseService
{
    private readonly IFormResponseRepository _responseRepository;
    private readonly IFormRepository _formRepository;

    public FormResponseService(IFormResponseRepository responseRepository, IFormRepository formRepository)
    {
        _responseRepository = responseRepository;
        _formRepository = formRepository;
    }

    public async Task<FormResponseDto> AddResponseAsync(int formId, FormResponseDto responseDto)
    {
        var form = await _formRepository.GetByIdAsync(formId);
        if (form == null) throw new ArgumentException($"Form with ID {formId} does not exist");

        var response = new FormResponse
        {
            FormId = formId,
            ResponseData = responseDto.ResponseData
        };
        var addedResponse = await _responseRepository.AddAsync(response);
        return new FormResponseDto { Id = addedResponse.Id, ResponseData = addedResponse.ResponseData };
    }

    public async Task<List<FormResponseDto>> GetResponsesAsync(int formId)
    {
        var form = await _formRepository.GetByIdAsync(formId);
        if (form == null) throw new ArgumentException($"Form with ID {formId} does not exist");

        var responses = await _responseRepository.GetByFormIdAsync(formId);
        return responses.Select(r => new FormResponseDto { Id = r.Id, ResponseData = r.ResponseData }).ToList();
    }
}