

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using IconBetAuth.Data.Definitions;
using IconBetAuth.Domain.DTO;
using MediatR;

namespace IconBetAuth.Services
{
     public static class UserGetBalanceServiceClient
    {
        public record Query(UserDTO userDTO) : IRequest<BalanceDTO>;

        public class Handler : IRequestHandler<Query, BalanceDTO>
        {
            private readonly IClientRepository _clientRepository;

            public Handler(IClientRepository clientRepository)
            {
                this._clientRepository = clientRepository;
            }

            public Task<BalanceDTO> Handle(Query request, CancellationToken cancellationToken)
            {
                return _clientRepository.GetBalance(request.userDTO);
            }
        }
    }
}
