﻿//El controlador es el que se encarga de devolver a la vista la informacio que traigamos de la base de datos o viceversa. 
//La  informacion fluye: de la vista al cotrolador, del controlador a los repositorios y del repositorio a la base de datos y viceversa.
//Vamos a tener tantos controladores como datos que se van a querer modificar en la base de datos.

using ApplicationsServices.Features.Commands.CreateCommands.CreateClientCommand;
using ApplicationsServices.Features.Commands.DeleteCommands;
using ApplicationsServices.Features.Commands.DeleteCommands.DeleteClientCommand;
using ApplicationsServices.Features.Commands.UpdateCommands.UpdateClientCommand;
using ApplicationsServices.Features.Queries.SelectAllQueries;
using ApplicationsServices.Features.Queries.SelectAllQueries.SelectClientQuery;
using ApplicationsServices.Features.Queries.SelectByQueries;
using ApplicationsServices.Features.Queries.SelectByQueries.SelectClientByIdQuery;
using ApplicationsServices.Filters.ClientResponseFilter;
using Microsoft.AspNetCore.Mvc;

namespace Veterinary.WebApi.Controllers.v1
{
    [ApiVersion("1.0")]
    public class ClientController : BaseApiController
    {
        [HttpGet]
        public async Task<IActionResult> GetAllClient([FromQuery] ClientResponseFilter filter)
        {
            return Ok(await Mediator.Send(new SelectClientQuery
            {
                PageNumber = filter.PageNumber,
                PageSize = filter.PageSize,
                clientName = filter.clientName,
                clientSurname = filter.clientSurname,
                IsDeleted=filter.IsDeleted
            }));
        }

        [HttpGet("{id:long}")]
        public async Task<IActionResult> GetById(long id)
        {
            return Ok(await Mediator.Send(new SelectClientByIdQuery { Id = id }));
        }

        [HttpPost]
        public async Task<IActionResult> Post(CreateClientCommand command)
        {
            return Ok(await Mediator.Send(command));
        }

        [HttpPut("{id:long}")]
        public async Task<IActionResult> Put(long id, UpdateClientCommand command)
        {
            if (id != command.Id)
                return BadRequest("Error en el Id suministrado no corresponde al registro a actualizar");
            return Ok(await Mediator.Send(command));
        }

        [HttpDelete("{id:long}")]
        public async Task<IActionResult> Delete(long id)
        {
            return Ok(await Mediator.Send(new DeleteClientCommand { Id = id }));
        }
    }
}
