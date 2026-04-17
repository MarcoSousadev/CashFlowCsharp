using CashFlow.Communication.Requests;
using CashFlow.Communication.Responses;
using CashFlow.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CashFlow.Application.UseCases.Expenses.Update
{
    public interface IUpdateUseCase
    {
        Task<bool> Execute(long id, RequestExpenseJson request);
               
    }
}
