using DynamicFormApi.Domain.Entities;
using DynamicFormApi.Domain.Interfaces;
using DynamicFormApi.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace DynamicFormApi.Infrastructure.Repositories
{
    public class FormResponseRepository : IFormResponseRepository
    {
        private readonly AppDbContext _context;

        public FormResponseRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<FormResponse> AddAsync(FormResponse response)
        {
            var addedResponse = await _context.FormResponses.AddAsync(response);
            await _context.SaveChangesAsync();
            return addedResponse.Entity;
        }

        public async Task<List<FormResponse>> GetByFormIdAsync(int formId)
        {
            return await _context.FormResponses
                .Where(fr => fr.FormId == formId)
                .ToListAsync();
        }
    }
}
