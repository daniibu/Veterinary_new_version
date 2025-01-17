﻿//Con el comando Update puedo actualizar los datos Ya existentes en mi base de datos
//En update si necesito hacer uso de ID, ya que con este detecto que dato quiero actualizar
//***notas extras***
//Inyección de dependencia: creamos interfaces y nosotros lo que vamos a hacer es comunicar a las capas a travez de ellas. 
//Esto rompe la dependencia de una capa con otra, si cambio el codigo el formato de la interfaz sigue siendo la misma y no deberia influir.
//Esto hace el codigo más limpio y fácil de mantener.
//Realizo inyección de dependencia con el repositorio:El repositorio es el que se va a encargar de manipular los datos contra la base de datos.
//Las task se ejecutan en paralelo a los demas procesos, si es async se ejecuta en paralelo sin detener otras tareas.
using ApplicationsServices.Interfaces;
using ApplicationsServices.Wrappers;
using AutoMapper;
using MediatR;
using Veterinary.DomainClass.Entity;

namespace ApplicationsServices.Features.Commands.UpdateCommands.UpdateClientCommand
{   
    public class UpdateClientCommand : IRequest<Response<long>>
    {
        public long Id { get; set; }
        public string clientName { get; set; }
        public string clientSurname { get; set; }
        public string clientAdress { get; set; }
        public string clientPhoneNum { get; set; }
        public string clientIdn { get; set; }
        public string email { get; set; }
        public long LastModifiedBy { get; set; }
    }
    public class UpdateClientCommandHandler : IRequestHandler<UpdateClientCommand, Response<long>>
    {
        private readonly IRepository<Client> _repository;

        public UpdateClientCommandHandler(IRepository<Client> repository, IMapper mapper)
        {
            _repository = repository;

        }

        public async Task<Response<long>> Handle(UpdateClientCommand request, CancellationToken cancellationToken)
        {
            //Obtiene el registro en base al Id enviado.
            var register = await _repository.GetByIdAsync(request.Id);
            //Consulta si se regreso algún registro desde la base de datos.
            if (register == null)
            {
                throw new KeyNotFoundException($"No se encontro el registro con el Id: {request.Id}");
            }
            else
            {
                register.clientName = request.clientName;
                register.clientSurname = request.clientSurname;
                register.clientAdress = request.clientAdress;
                register.clientPhoneNum = request.clientPhoneNum;
                register.clientIdn = request.clientIdn;
                register.email = request.email;
                await _repository.UpdateAsync(register);
            }
            return new Response<long>(register.Id);
        }
    }


}
