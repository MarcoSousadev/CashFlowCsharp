using CashFlow.Domain.Entities;
using CashFlow.Domain.Repositories.Expenses;

namespace CashFlow.Application.UseCases.Expenses.ListExpenses
{
    internal class GetByIdUseCase : IGetByIdUseCase
    {
        private readonly IExpensesRepository _repository;
        public GetByIdUseCase(IExpensesRepository repository)
        {
            _repository = repository;
        }
        public async Task<Expense> Execute(long id)
        {
            var result = await _repository.GetById(id); 

            return new Expense
            {
                Amount = result.Amount,
                Date = result.Date,
                Description = result.Description,
                Id = result.Id,
                PaymentType = result.PaymentType,
                Title = result.Title
            };
        }
    }
}
