using AutoMapper;
using Microsoft.Extensions.Configuration;
using System.Linq.Expressions;
using TaskOfAlifTech.Domain.Configurations;
using TaskOfAlifTech.Domain.Entities.Transactions;
using TaskOfAlifTech.Service.DTOs.Transactions;
using TaskOfAlifTech.Service.Exceptions;
using TaskOfAlifTech.Service.Extensions;
using TaskOfAlifTech.Service.Interfaces;
using TasOfAlifTech.Data.IRepositories;

namespace TaskOfAlifTech.Service.Services
{
    public class TransactionService : ITransactionService
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IConfiguration configuration;
        private readonly IMapper mapper;
        public TransactionService(IUnitOfWork unitOfWork, IMapper mapper, IConfiguration configuration)
        {
            this.unitOfWork = unitOfWork;
            this.mapper = mapper;
            this.configuration = configuration;
        }

        /// <summary>
        /// Create transaction p2p
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        /// <exception cref="AppException"></exception>

        public async Task<TransactionViewDto> AddP2pAsync(TransactionForCreationDto dto)
        {
            var fromWallet = await unitOfWork.Wallets.GetAsync(wallet => wallet.Id == dto.FromWalletId);
            if (fromWallet is null)
                throw new AppException(400, "Sender wallet not found!");

            var toWallet = await unitOfWork.Wallets.GetAsync(wallet => wallet.Id == dto.ToWalletId);
            if (toWallet is null)
                throw new AppException(400, "Reciever wallet not found!");

            if (fromWallet.Balance - dto.Amount < 0)
                throw new AppException(400, "Not enough money on balance");

            var user = await unitOfWork.Users.GetAsync(user => user.Id == toWallet.UserId);
            var transactionSettings = configuration[$"TransactionSettings:{(user.IsIdentified ? "Identified" : "Unidentified")}"];
            decimal amountLimit = decimal.Parse(transactionSettings);

            if (toWallet.Balance + dto.Amount > amountLimit)
                throw new AppException(400, $"Balance must not exceed: {amountLimit}");

            fromWallet.Balance -= dto.Amount;
            toWallet.Balance += dto.Amount;

            unitOfWork.Wallets.Update(fromWallet);
            unitOfWork.Wallets.Update(toWallet);
            await unitOfWork.SaveChangesAsync();

            var transaction = await unitOfWork.Transactions.CreateAsync(mapper.Map<Transaction>(dto));
            await unitOfWork.SaveChangesAsync();

            var fromUser = await unitOfWork.Users.GetAsync(user => user.Id == fromWallet.UserId);
            var toUser = await unitOfWork.Users.GetAsync(user => user.Id == toWallet.UserId);

            var viewDto = new TransactionViewDto
            {
                To = toUser.FirstName + " " + toUser.LastName,
                Amount = transaction.Amount,
                CreatedAt = transaction.CreatedAt,
                From = fromUser.FirstName + " " + fromUser.LastName
            };

            return viewDto;
        }

        /// <summary>
        /// Create replenishment
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        /// <exception cref="AppException"></exception>
        public async Task<TransactionViewDto> AddMoneyAsync(TransactionToAddMoneyDto dto)
        {
            var toWallet = await unitOfWork.Wallets.GetAsync(wallet => wallet.Id == dto.ToWalletId);
            if (toWallet is null)
                throw new AppException(400, "Reciever wallet not found!");

            var user = await unitOfWork.Users.GetAsync(user => user.Id == toWallet.UserId);

            var transactionSettings = configuration[$"TransactionSettings:{(user.IsIdentified ? "Identified" : "Unidentified")}"];
            decimal amountLimit = decimal.Parse(transactionSettings);

            if (toWallet.Balance + dto.Amount > amountLimit)
                throw new AppException(400, $"Balance must not exceed: {amountLimit}");

            toWallet.Balance += dto.Amount;
            unitOfWork.Wallets.Update(toWallet);
            await unitOfWork.SaveChangesAsync();

            var transaction = await unitOfWork.Transactions.CreateAsync(mapper.Map<Transaction>(dto));
            await unitOfWork.SaveChangesAsync();

            var toUser = await unitOfWork.Users.GetAsync(user => user.Id == toWallet.UserId);

            var viewDto = new TransactionViewDto
            {
                To = toUser.FirstName + " " + toUser.LastName,
                Amount = transaction.Amount,
                CreatedAt = transaction.CreatedAt,
                From = dto.From
            };

            return viewDto;
        }
        /// <summary>
        /// Get all transactions by filter
        /// </summary>
        /// <param name="params"></param>
        /// <param name="expression"></param>
        /// <returns></returns>
        public async Task<IEnumerable<TransactionViewDto>> GetAllAsync(PaginationParams? @params = null, Expression<Func<Transaction, bool>>? expression = null)
        {
            var transactions = unitOfWork.Transactions.Where(expression,
                isTracking: true, include: new[] { "ToWallet.User", "FromWallet.User" }).ToPagedList(@params);

            var result = new List<TransactionViewDto>();
            foreach (var item in transactions)
            {
                var viewDto = new TransactionViewDto
                {
                    To = item.ToWallet!.User!.FirstName + " " + item.ToWallet.User.LastName,
                    Amount = item.Amount,
                    CreatedAt = item.CreatedAt,
                    From = item.FromWalletId is null
                        ? item.From
                        : item.FromWallet.User.FirstName + " " + item.FromWallet.User.LastName
                };

                result.Add(viewDto);
            }

            return result;
        }
    }
}
