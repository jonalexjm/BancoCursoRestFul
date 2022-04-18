using Application.Features.Clientes.Commands.CreateClienteCommand;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace WebApi.Controllers.v1
{
    [ApiVersion("1.0")]
    public class ClientesController : BaseApiController
    {

        //post api/<controller>
        [HttpPost]
        public async Task<IActionResult> Post(CreateClientCommand command)
        {
            return Ok(await Mediator.Send(command));
        }
    }
}
