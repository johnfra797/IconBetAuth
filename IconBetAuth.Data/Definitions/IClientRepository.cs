﻿
using IconBetAuth.Domain.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IconBetAuth.Data.Definitions
{
    public interface IClientRepository
    {
        Task<UserDTO> GetInfo(UserDTO userDTO);
        Task<BalanceDTO> GetBalance(UserDTO userDTO);
        Task<TransactionResponseDTO> WriteBet(TransactionDTO transactionDTO);
        Task<UserDTO> Login(LoginDTO loginDTO);
        Task<UserDTO> Register(RegisterDTO registerDTO);
        Task<bool> DeActivateUser(LoginDTO loginDTO);

    }
}
