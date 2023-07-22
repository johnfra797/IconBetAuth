
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using AutoMapper;
using IconBetAuth.Data.DB;
using IconBetAuth.Data.Definitions;
using IconBetAuth.Domain.DTO;
using IconBetAuth.Domain.Enum;
using IconBetAuth.Domain.ExtensionMethods;
using IconBetAuth.Domain.Models;
using Microsoft.Extensions.Configuration;
using static Azure.Core.HttpHeader;

namespace IconBetAuth.Data.Implementations
{
    public class ClientRepository : IClientRepository
    {
        private readonly IConfiguration _configuration;
        private DataBase _dataBase { get; set; }
        private readonly IMapper _mapper;
        public string UrlGetInfo { get; set; }
        public string Url { get; set; }
        public string Clave { get; set; }
        public string ClaveCliente { get; set; }
        public ClientRepository(IConfiguration configuration, IMapper mapper)
        {
            _configuration = configuration;
            _mapper = mapper;
        }
        private void initDB(string db)
        {
            _dataBase = new DataBase(db, _mapper);
        }

        public async Task<BalanceDTO> GetBalance(UserDTO userDTO)
        {
            initDB(userDTO.DatabaseConnectionString);
            BalanceDTO balanceDTO = new BalanceDTO();
            var userDto = await GetInfoByUser(userDTO);
            balanceDTO.UserName = userDto.UserName;
            balanceDTO.Balance = userDto.Balance.Value;
            balanceDTO.Currency = userDto.Currency;
            return balanceDTO;
        }

        public async Task<UserDTO?> GetInfoByUser(UserDTO user)
        {
            User? userResponse = _dataBase.GetUser(user.UserName);
            if (userResponse != null)
            {
                return _mapper.Map<UserDTO>(userResponse);
            }
            return null;

        }

        public async Task<UserDTO?> GetInfo(UserDTO userDTO)
        {
            initDB(userDTO.DatabaseConnectionString);
            UserDTO? userDTOResponse = await GetInfoByUser(userDTO);
            return userDTOResponse;
        }

        public async Task<TransactionResponseDTO> WriteBetTransaction(TransactionDTO transactionDTO)
        {
            transactionDTO.UUID = Guid.NewGuid().ToString();
            Transaction Transaction = _mapper.Map<Transaction>(transactionDTO);
            var result =_dataBase.SaveTransaction(Transaction);
            if (!result.hasError)
            {
                User user = _dataBase.GetUser(transactionDTO.UserName);
                if (transactionDTO.TransactionsType == TransactionsType.Debit)
                {
                    user.Balance = user.Balance - transactionDTO.Amount;
                }
                if (transactionDTO.TransactionsType == TransactionsType.Credit)
                {
                    user.Balance = user.Balance + transactionDTO.Amount;
                }
                _dataBase.UpdateUser(user);
            }
            return result;
        }
        public async Task<TransactionResponseDTO> WriteBet(TransactionDTO transactionDTO)
        {
            initDB(transactionDTO.DatabaseConnectionString);
            return await WriteBetTransaction(transactionDTO);
        }

        public async Task<UserDTO?> Login(LoginDTO loginDTO)
        {
            initDB(loginDTO.DatabaseConnectionString);
            User? user = _dataBase.Login(loginDTO);
            UserDTO userDTO = new UserDTO();
            if (user != null)
            {
                userDTO = _mapper.Map<UserDTO>(user);
            }
            else
            {
                userDTO.hasError = true;
                userDTO.Messages.Add(Error.LoginError.GetDescription());
            }
            return userDTO;
        }

        public async Task<UserDTO?> Register(RegisterDTO registerDTO)
        {
            initDB(registerDTO.DatabaseConnectionString);
            UserDTO userDTO = _dataBase.Register(registerDTO);
            return userDTO;
        }

        public async Task<bool> DeActivateUser(LoginDTO loginDTO)
        {
            initDB(loginDTO.DatabaseConnectionString);
            return _dataBase.DeActivateUser(loginDTO);
        }
    }
}
