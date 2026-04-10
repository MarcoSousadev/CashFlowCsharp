using CashFlow.Domain.Entities;
using CashFlow.Domain.Repositories.Expenses;
using Microsoft.EntityFrameworkCore;

namespace CashFlow.Infrastructure.DataAcess.Repositories
{
    internal class ExpensesRepository : IReadOnlyRepository, IWriteOnlyRepository , IDeleteOnlyRepository, IUpdateOnlyRepository
    {
        private readonly CashFlowDbContext _dbContext;
        public ExpensesRepository(CashFlowDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task Add(Expense expense)
        {
           await _dbContext.Expenses.AddAsync(expense);
        }

        public async Task<bool> Delete(long id)
        {
            var result =  await _dbContext.Expenses.AsNoTracking().FirstOrDefaultAsync(expenses => expenses.Id == id);

            if(result == null)
            {
                return false;
            }

             _dbContext.Expenses.Remove(result);

            return true;

        }

        public async Task<List<Expense>> GetAll()
        {
            return await _dbContext.Expenses.AsNoTracking().ToListAsync();
          
        }

        async Task<Expense?> IReadOnlyRepository.GetById(long id)
        {
            return await _dbContext.Expenses.AsNoTracking().FirstOrDefaultAsync(expenses => expenses.Id == id);
        }

        async Task<Expense?> IUpdateOnlyRepository.GetById(long id)
        {
            return await _dbContext.Expenses.FirstOrDefaultAsync(expenses => expenses.Id == id);
        }

        public void Update(Expense expense)
        {
            _dbContext.Expenses.Update(expense);
        }
    }
}
