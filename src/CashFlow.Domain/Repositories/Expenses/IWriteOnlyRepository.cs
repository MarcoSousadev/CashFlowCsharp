using CashFlow.Domain.Entities;

namespace CashFlow.Domain.Repositories.Expenses
{
    public interface IWriteOnlyRepository
    {
        Task Add(Expense expense);
    }
}
