using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using FluentValidation;
using MediatR;
using MongoDB.Driver;

namespace aspnetdemo2.domain.luchadores {


    public class EditarLuchadorCommand : IRequest<bool> {
        public EditarLuchadorCommand(string id, string nombre, string marca, int edad, string genero)
        {
            Id = id;
            Nombre = nombre;
            Marca = marca;
            Edad = edad;
            Genero = genero;
        }
        public EditarLuchadorCommand()
        {
            
        }

        public string Id { get;  set; }
        public string Nombre { get; set; }

        public string Marca { get; set; }

        public int Edad { get; set; }

        public string Genero {get; set;}



        

    }


    public class EditarLuchadorCommandValidator : AbstractValidator<EditarLuchadorCommand>
    {
        public EditarLuchadorCommandValidator()
        {
           // RuleFor(m => m.NumeroControl).MinimumLength(6).NotEmpty();
             RuleFor(m => m.Nombre).NotEmpty().MinimumLength(1);
            RuleFor(m => m.Marca).NotEmpty();
            RuleFor(m => m.Edad).NotEmpty().Must( p => p >= 0);
            RuleFor(m => m.Genero).NotEmpty();;
        }
    }


    public class EditarLuchadorCommandHandler
           : IRequestHandler<EditarLuchadorCommand, bool>
    {
        private readonly IMongoCollection<Luchador> luchadores;

        public EditarLuchadorCommandHandler(IEstudiantesDatabaseSettings settings)
        {
            var cliente = new MongoClient(settings.ConnectionString);
            var db = cliente.GetDatabase(settings.DatabaseName);

            luchadores = db.GetCollection<Luchador>("Luchadores");
        }

        public async Task<bool> Handle(EditarLuchadorCommand request, CancellationToken cancellationToken)
        {
            var mgLuchador = (await luchadores
                    .FindAsync<Luchador>( luc => luc.Id == request.Id)
            ).FirstOrDefault();
            mgLuchador.Nombre = request.Nombre;
            mgLuchador.Marca = request.Marca;
            mgLuchador.Edad = request.Edad;
            mgLuchador.Genero = request.Genero;
             luchadores.ReplaceOne(luc => luc.Id == mgLuchador.Id,
                mgLuchador
              );
            
            return true;
        }
    }

    

}