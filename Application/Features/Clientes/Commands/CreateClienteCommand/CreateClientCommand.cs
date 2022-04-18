using Application.Interfaces;
using Application.Wrappers;
using AutoMapper;
using Domain.Entities;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Features.Clientes.Commands.CreateClienteCommand
{
    public class CreateClientCommand : IRequest<Response<int>>
    {
        public string Nombre { get; set; }
        public string Apellido { get; set; }
        public DateTime FechaNacimiento { get; set; }
        public string Telefono { get; set; }
        public string Email { get; set; }
        public string Direccion { get; set; }  
    }

    public class CreateClienteCommandHandler : IRequestHandler<CreateClientCommand, Response<int>>
    {
        private readonly IRepositoryAsync<Cliente> _repositoryAsync;

        private readonly IMapper _mapper;
        public CreateClienteCommandHandler(IRepositoryAsync<Cliente> repositoryAsync, IMapper mapper)
        {
            _repositoryAsync = repositoryAsync;
            _mapper = mapper;
        }

        public async Task<Response<int>> Handle(CreateClientCommand request, CancellationToken cancellationToken)
        {
            var nuevoRegistro = _mapper.Map<Cliente>(request);
            try
            {
                var data = await _repositoryAsync.AddAsync(nuevoRegistro);

                return new Response<int>(data.Id);
            }
            catch (Exception)
            {

                throw;
            }
            
        }
    }
}