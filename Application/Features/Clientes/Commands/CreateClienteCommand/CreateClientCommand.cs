using Application.Wrappers;
using MediatR;
using System;

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
}