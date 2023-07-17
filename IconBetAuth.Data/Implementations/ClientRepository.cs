
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
using IconBetAuth.Domain.Models;
using Microsoft.Extensions.Configuration;

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
            string db = _configuration.GetSection("ConnectionStrings").GetSection("DB").Value;

            UrlGetInfo = _configuration.GetSection("Client").GetSection("UrlGetInfo").Value;
            Url = _configuration.GetSection("Client").GetSection("Url").Value;
            Clave = _configuration.GetSection("Client").GetSection("Clave").Value;
            ClaveCliente = _configuration.GetSection("Client").GetSection("ClaveCliente").Value;

            _dataBase = new DataBase(db, mapper);
        }
        public async Task<BalanceDTO> GetBalance(UserDTO userDTO, string apiUrl)
        {
            BalanceDTO balanceDTO = new BalanceDTO();
            try
            {

                var userDto = await GetInfoByUser(userDTO);
                balanceDTO.UserName = userDto.UserName;
                balanceDTO.Balance = userDto.Balance;
                balanceDTO.Currency = userDto.Currency;
            }
            catch (Exception ex)
            {

            }

            return await Task.Run(() => balanceDTO);
        }

        public async Task<BalanceDTO> GetBalance(UserDTO userDTO)
        {
            Customer customer = _dataBase.GetCustomer(userDTO.Company);
            return await GetBalance(userDTO, customer.APIUrl);
        }

        public async Task<UserDTO> GetInfoByUser(UserDTO user)
        {
            try
            {
                string resultado = string.Empty;
                HttpWebRequest request = (HttpWebRequest)WebRequest.Create($"{UrlGetInfo}&account={user.UserName}");
                request.AutomaticDecompression = DecompressionMethods.GZip | DecompressionMethods.Deflate;
                request.Accept = "application/json";
                request.Method = WebRequestMethods.Http.Get;
                using (HttpWebResponse response = (HttpWebResponse)request.GetResponse())
                using (Stream stream = response.GetResponseStream())
                using (StreamReader reader = new StreamReader(stream))
                {
                    resultado = reader.ReadToEnd();
                }

                XmlDocument xmlDoc = new XmlDocument();
                xmlDoc.LoadXml(resultado);

                XmlNodeList xnList = xmlDoc.SelectNodes("/Data");

                UserDTO userDTO = new UserDTO();

                foreach (XmlNode xn in xnList)
                {
                    //user.Company = xn["Compania"].InnerText;
                    user.Agent = xn["Agente"].InnerText;
                    user.Currency = xn["Moneda"].InnerText;
                    user.BalanceStr = xn["BalanceDeCliente"].InnerText.Replace(",", ".");
                    decimal balance;
                    decimal.TryParse(xn["BalanceDeCliente"].InnerText.Replace(",", "."), out balance);
                    user.Balance = balance;
                }

                return user;
            }
            catch (Exception ex)
            {
                return new UserDTO();
            }
        }

        public async Task<UserDTO> GetInfo(UserDTO userDTO)
        {
            Customer customer = _dataBase.GetCustomer(userDTO.Company);
            if (customer != null)
            {
                UserDTO userDTOResponse = await GetInfoByUser(userDTO);
                return userDTOResponse;
            }
            else
                return null;
        }

        public async Task<string> WriteBetTransaction(TransactionDTO transactionDTO)
        {
            string code = string.Empty;
            try
            {
                string amount = transactionDTO.Amount.ToString().Replace(".", ",");
                int transactionsType = (int)transactionDTO.TransactionsType;
                string resultado = string.Empty;
                HttpWebRequest request = (HttpWebRequest)WebRequest.Create($"{Url}/RegistrarMontoDescrip?clave={Clave}&Clavecliente={ClaveCliente}&tipoTransaccion={transactionsType}&usuario={transactionDTO.UserName}&monto={amount}&descripcion={transactionDTO.Description}");
                request.AutomaticDecompression = DecompressionMethods.GZip | DecompressionMethods.Deflate;
                request.Accept = "application/json";
                request.Method = WebRequestMethods.Http.Get;
                using (HttpWebResponse response = (HttpWebResponse)request.GetResponse())
                using (Stream stream = response.GetResponseStream())
                using (StreamReader reader = new StreamReader(stream))
                {
                    resultado = reader.ReadToEnd();
                }

                XmlDocument xmlDoc = new XmlDocument();
                xmlDoc.LoadXml(resultado);
                int codeResult;
                int.TryParse(xmlDoc.InnerText, out codeResult);
                if (codeResult > 0)
                {
                    code = xmlDoc.InnerText;

                    Transaction Transaction = _mapper.Map<Transaction>(transactionDTO);
                    _dataBase.SaveTransaction(Transaction);
                }

            }
            catch (Exception ex)
            {

            }
            return code;
        }
        public async Task<string> WriteBet(TransactionDTO transactionDTO)
        {
            Customer customer = _dataBase.GetCustomer(transactionDTO.Company);
            return await WriteBetTransaction(transactionDTO);
        }

        public Task<UserDTO> Login(UserDTO userDTO)
        {

        }
    }
}
