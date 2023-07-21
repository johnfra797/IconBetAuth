using IconBetAuth.Data.Definitions;
using IconBetAuth.Domain.DTO;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IconBetAuth.Services.Services
{
    public static class RegisterServiceClient
    {
        public record Query(LoginDTO loginDTO) : IRequest<bool>;

        public class Handler : IRequestHandler<Query, bool>
        {
            private readonly IClientRepository _clientRepository;

            public Handler(IClientRepository clientRepository)
            {
                this._clientRepository = clientRepository;
            }

            public Task<bool> Handle(Query request, CancellationToken cancellationToken)
            {
                return _clientRepository.Register(request.loginDTO);
            }
        }
    }
}
