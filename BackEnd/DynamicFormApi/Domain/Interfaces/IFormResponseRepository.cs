using DynamicFormApi.Domain.Entities;

namespace DynamicFormApi.Domain.Interfaces
{
    public interface IFormResponseRepository
    {
        Task<FormResponse> AddAsync(FormResponse response);
        Task<List<FormResponse>> GetByFormIdAsync(int formId);
    }
}
