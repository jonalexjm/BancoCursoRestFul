using Application.DTOs;
using Application.Interfaces;
using Application.Specifications;
using Application.Wrappers;
using AutoMapper;
using Domain.Entities;
using MediatR;
using Microsoft.Extensions.Caching.Distributed;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;


namespace Application.Features.Clientes.Queries.GetAllClientes
{
    public class GetAllClientesQuery : IRequest<PageResponse<List<ClienteDto>>>
    {
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
        public string Nombre { get; set; }
        public string Apellido { get; set; }
    }

    public class GetAllClientesQueryHandler : IRequestHandler<GetAllClientesQuery, PageResponse<List<ClienteDto>>>
    {
        private readonly IRepositoryAsync<Cliente> _repositoryAsync;

        private readonly IMapper _mapper;

        private readonly IDistributedCache _distributedCache;
        public GetAllClientesQueryHandler(IRepositoryAsync<Cliente> repositoryAsync,
                                          IMapper mapper,
                                          IDistributedCache distributedCache)
        {
            _repositoryAsync = repositoryAsync;
            _mapper = mapper;
            _distributedCache = distributedCache;
        }
        public async Task<PageResponse<List<ClienteDto>>> Handle(GetAllClientesQuery request, CancellationToken cancellationToken)
        {
            var cackeKey = $"listadoClientes_{request.PageSize}_{request.PageNumber}_{request.Nombre}_{request.Apellido}";
            string serializedListadoClientes;
            var listadoClientes = new List<Cliente>();
            var redisListadoClientes = await _distributedCache.GetAsync(cackeKey);
            if (redisListadoClientes != null)
            {
                serializedListadoClientes = Encoding.UTF8.GetString(redisListadoClientes);
                listadoClientes = JsonConvert.DeserializeObject<List<Cliente>>(serializedListadoClientes);
            }
            else
            {
                listadoClientes = await _repositoryAsync.ListAsync(new PageClienteSpecification(request.PageSize,
                                                                                   request.PageNumber,
                                                                                   request.Nombre,
                                                                                  request.Apellido));
                serializedListadoClientes = JsonConvert.SerializeObject(listadoClientes);
                redisListadoClientes = Encoding.UTF8.GetBytes(serializedListadoClientes);

                var options = new DistributedCacheEntryOptions()
                                 .SetAbsoluteExpiration(DateTime.Now.AddMinutes(10)) //vencimiento del cache 2 minutos
                                 .SetSlidingExpiration(TimeSpan.FromMinutes(2)); // si no se solicita en este tiempo
                //grabar en redis cache
                await _distributedCache.SetAsync(cackeKey, redisListadoClientes, options);
            }

            
            var clientesDto = _mapper.Map<List<ClienteDto>>(listadoClientes);

            return new PageResponse<List<ClienteDto>>(clientesDto, request.PageNumber, request.PageSize);
        }
    }
}
