
using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using AutoMapper;
using IconBetAuth.Domain;
using IconBetAuth.Domain.DTO;
using IconBetAuth.Domain.Enum;
using IconBetAuth.Domain.ExtensionMethods;
using IconBetAuth.Domain.Models;
using IconBetAuth.Domino.Domain;

namespace IconBetAuth.Data.DB
{
    public class DataBase
    {
        private readonly IconBetAuthContext _iconBetAuthContext;
        private readonly IMapper _mapper;
        public DataBase(string stringConnDB, IMapper mapper)
        {
            _iconBetAuthContext = new IconBetAuthContext(stringConnDB);
            _mapper = mapper;
        }
        public TransactionResponseDTO SaveTransaction(Transaction transaction)
        {
            TransactionResponseDTO transactionResponseDTO = new TransactionResponseDTO();
            ResponseDTO responseDTO = ValidateTransaction(transaction);
            try
            {
                transactionResponseDTO.hasError = responseDTO.hasError;
                transactionResponseDTO.Messages = responseDTO.Messages;
                if (!responseDTO.hasError)
                {
                    transaction.CreationDate = DateTime.Now;
                    _iconBetAuthContext.Add(transaction);
                    int result = _iconBetAuthContext.SaveChanges();
                    if (result == 1)
                    {
                        transactionResponseDTO.UUID = transaction.UUID;
                    }
                    else
                    {
                        transactionResponseDTO.hasError = true;
                        transactionResponseDTO.Messages.Add(Error.GeneralError.GetDescription());
                    }

                }
            }
            catch (Exception ex)
            {
                transactionResponseDTO.hasError = true;
                transactionResponseDTO.Messages.Add(Error.GeneralError.GetDescription());
            }
            return transactionResponseDTO;
        }

        public User? Login(LoginDTO loginDTO)
        {
            return _iconBetAuthContext.User.FirstOrDefault(x => x.UserName == loginDTO.UserName && x.Password == loginDTO.Password && x.Active == true);
        }
        public UserDTO Register(RegisterDTO registerDTO)
        {
            UserDTO userDTO = new UserDTO();
            ResponseDTO response = ValidateUser(registerDTO);
            try
            {
                userDTO.hasError = response.hasError;
                userDTO.Messages = response.Messages;
                if (!response.hasError)
                {
                    User user = _mapper.Map<User>(registerDTO);
                    user.Active = true;
                    user.CreationDate = DateTime.Now;
                    _iconBetAuthContext.Add(user);
                    int result = _iconBetAuthContext.SaveChanges();
                    if (result == 1)
                    {
                        userDTO = _mapper.Map<UserDTO>(user);
                    }
                    else
                    {
                        userDTO.hasError = true;
                        userDTO.Messages.Add(Error.GeneralError.GetDescription());
                    }
                }
            }
            catch (Exception ex)
            {
                userDTO.hasError = true;
                userDTO.Messages.Add(Error.GeneralError.GetDescription());
            }
            return userDTO;
        }
        public bool DeActivateUser(LoginDTO loginDTO)
        {
            try
            {
                User? user = Login(loginDTO);
                if (user != null)
                {
                    user.Active = false;
                    _iconBetAuthContext.User.Update(user);
                    int result = _iconBetAuthContext.SaveChanges();
                    return result == 1;
                }
                return false;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public bool UpdateUser(User user)
        {
            try
            {
                _iconBetAuthContext.User.Update(user);
                int result = _iconBetAuthContext.SaveChanges();
                return result == 1;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
        public User? GetUser(string userName)
        {
            return _iconBetAuthContext.User.FirstOrDefault(x => x.UserName == userName);
        }
        private ResponseDTO ValidateUser(RegisterDTO registerDTO)
        {
            ResponseDTO responseDTO = new ResponseDTO();
            var existsEmail = _iconBetAuthContext.User.Any(x => x.Email == registerDTO.Email);
            if (existsEmail)
            {
                responseDTO.Messages.Add(Error.ExistsEmail.GetDescription());
            }

            var existsPhone = _iconBetAuthContext.User.Any(x => x.Phone == registerDTO.Phone);
            if (existsPhone)
            {
                responseDTO.Messages.Add(Error.ExistsPhone.GetDescription());
            }

            var existsUsername = _iconBetAuthContext.User.Any(x => x.UserName == registerDTO.UserName);
            if (existsUsername)
            {
                responseDTO.Messages.Add(Error.ExistsUserName.GetDescription());
            }
            responseDTO.hasError = responseDTO.Messages.Count > 0;
            return responseDTO;
        }
        private ResponseDTO ValidateTransaction(Transaction transaction)
        {
            ResponseDTO responseDTO = new ResponseDTO();
            var existsTicketUUID = _iconBetAuthContext.Transaction.Any(x => x.TicketUUID == transaction.TicketUUID);
            if (existsTicketUUID)
            {
                responseDTO.Messages.Add(Error.TicketUUIDError.GetDescription());
            }

            responseDTO.hasError = responseDTO.Messages.Count > 0;
            return responseDTO;
        }
    }
}