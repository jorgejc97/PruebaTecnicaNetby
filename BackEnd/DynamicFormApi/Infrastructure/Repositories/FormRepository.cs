using DynamicFormApi.Domain.Entities;
using DynamicFormApi.Domain.Interfaces;
using DynamicFormApi.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace DynamicFormApi.Infrastructure.Repositories
{
    public class FormRepository : IFormRepository
    {
        private readonly AppDbContext _context;

        public FormRepository(AppDbContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public async Task<List<Form>> GetAllAsync()
        {
            return await _context.Forms.Include(f => f.Fields).ToListAsync();
        }

        public async Task<Form?> GetByIdAsync(int id)
        {
            return await _context.Forms.Include(f => f.Fields).FirstOrDefaultAsync(f => f.Id == id);
        }

        public async Task<Form> AddAsync(Form form)
        {
            _context.Forms.Add(form);
            await _context.SaveChangesAsync();
            return form;
        }

        public async Task UpdateAsync(Form form)
        {
            _context.Forms.Update(form);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var form = await _context.Forms.FindAsync(id);
            if (form != null)
            {
                _context.Forms.Remove(form);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<FormField> AddFieldAsync(int formId, FormField field)
        {
            field.FormId = formId;
            _context.FormFields.Add(field);
            await _context.SaveChangesAsync();
            return field;
        }

        public async Task UpdateFieldAsync(int formId, FormField field)
        {
            _context.FormFields.Update(field);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteFieldAsync(int formId, int fieldId)
        {
            var field = await _context.FormFields.FindAsync(fieldId);
            if (field != null && field.FormId == formId)
            {
                _context.FormFields.Remove(field);
                await _context.SaveChangesAsync();
            }
        }
    }
}
