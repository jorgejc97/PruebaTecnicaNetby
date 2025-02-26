using DynamicFormApi.Domain.Entities;

namespace DynamicFormApi.Domain.Interfaces
{
    public interface IFormRepository
    {
        Task<List<Form>> GetAllAsync();
        Task<Form?> GetByIdAsync(int id);
        Task<Form> AddAsync(Form form);
        Task UpdateAsync(Form form);
        Task DeleteAsync(int id);
        Task<FormField> AddFieldAsync(int formId, FormField field);
        Task UpdateFieldAsync(int formId, FormField field);
        Task DeleteFieldAsync(int formId, int fieldId);
    }
}
