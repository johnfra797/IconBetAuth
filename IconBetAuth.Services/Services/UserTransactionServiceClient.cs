
using IconBetAuth.Data.Definitions;
using IconBetAuth.Domain.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MediatR;

namespace IconBetAuth.Services
{
     public static class UserTransactionServiceClient
    {
        public record Query(TransactionDTO transactionDTO) : IRequest<TransactionResponseDTO>;

        public class Handler : IRequestHandler<Query, TransactionResponseDTO>
        {
            private readonly IClientRepository _clientRepository;

            public Handler(IClientRepository clientRepository)
            {
                this._clientRepository = clientRepository;
            }

            public Task<TransactionResponseDTO> Handle(Query request, CancellationToken cancellationToken)
            {
                return _clientRepository.WriteBet(request.transactionDTO);
            }
        }
    }
}
