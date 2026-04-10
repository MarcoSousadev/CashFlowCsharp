using CashFlow.Domain.Repositories;
using CashFlow.Domain.Repositories.Expenses;
using CashFlow.Excpetion;
using CashFlow.Excpetion.ExceptionsBase;
using System.Reflection;

namespace CashFlow.Application.UseCases.Expenses.Delete
{
    public class DeleteExpenseUseCase : IDeleteExpenseUseCase
    {
        private readonly IDeleteOnlyRepository _repository;
        private readonly IUnitOfWork _unitOfWork;

        public DeleteExpenseUseCase(IDeleteOnlyRepository repository, IUnitOfWork unitOfWork)
        {
            _repository = repository;

            _unitOfWork = unitOfWork;

        }

        public async Task<bool> Execute(long id)
        {
           var result =  await _repository.Delete(id);

            if(result == false)
            {
                throw new NotFoundException(ResourceErrorMessages.RESOURCE_NOT_FOUND);
            }

            return result;

            await _unitOfWork.Commit();
        }
    }
}
