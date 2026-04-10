using CashFlow.Domain.Entities;

namespace CashFlow.Domain.Repositories.Expenses
{
    public interface IReadOnlyRepository
    {
        Task<List<Expense>> GetAll();
        Task<Expense?> GetById(long Id);
    }
}
