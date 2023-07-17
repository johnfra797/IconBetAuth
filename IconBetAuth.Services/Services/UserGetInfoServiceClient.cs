

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
     public static class UserGetInfoServiceClient
    {
        public record Query(UserDTO userDTO) : IRequest<UserDTO>;

        public class Handler : IRequestHandler<Query, UserDTO>
        {
            private readonly IClientRepository _clientRepository;

            public Handler(IClientRepository clientRepository)
            {
                this._clientRepository = clientRepository;
            }

            public Task<UserDTO> Handle(Query request, CancellationToken cancellationToken)
            {
                return _clientRepository.GetInfo(request.userDTO);
            }
        }
    }
}
