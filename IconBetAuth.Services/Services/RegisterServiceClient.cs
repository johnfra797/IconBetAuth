﻿using IconBetAuth.Data.Definitions;
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
        public record Query(RegisterDTO registerDTO) : IRequest<UserDTO>;

        public class Handler : IRequestHandler<Query, UserDTO>
        {
            private readonly IClientRepository _clientRepository;

            public Handler(IClientRepository clientRepository)
            {
                this._clientRepository = clientRepository;
            }

            public Task<UserDTO> Handle(Query request, CancellationToken cancellationToken)
            {
                return _clientRepository.Register(request.registerDTO);
            }
        }
    }
}
