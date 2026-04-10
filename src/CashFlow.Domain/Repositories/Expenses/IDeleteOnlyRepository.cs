using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CashFlow.Domain.Repositories.Expenses
{
    public interface IDeleteOnlyRepository
    {
    
        Task<bool> Delete(long id);
        
        
    }
}
