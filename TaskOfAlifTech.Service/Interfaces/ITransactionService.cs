using System.Linq.Expressions;
using TaskOfAlifTech.Domain.Configurations;
using TaskOfAlifTech.Domain.Entities.Transactions;
using TaskOfAlifTech.Service.DTOs.Transactions;

namespace TaskOfAlifTech.Service.Interfaces
{
    public interface ITransactionService
    {
        Task<TransactionViewDto> AddMoneyAsync(TransactionToAddMoneyDto dto);
        Task<TransactionViewDto> AddP2pAsync(TransactionForCreationDto dto);

        Task<IEnumerable<TransactionViewDto>> GetAllAsync(PaginationParams? @params = null,
            Expression<Func<Transaction, bool>>? expression = null);


    }
}
