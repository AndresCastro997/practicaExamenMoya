using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using FluentValidation;
using MediatR;
using MongoDB.Driver;

namespace aspnetdemo2.domain.luchadores {


    public class CrearLuchadorCommand : IRequest<bool> {
        public CrearLuchadorCommand( 
                string nombre, 
                string marca, 
                int edad,
                string genero
                )
        {
            Nombre = nombre;
            Marca = marca;
            Edad = edad;
            Genero = genero ;
            
        }
        public CrearLuchadorCommand()
        {
            
        }

       
        public string Nombre { get; set; }
        public string Marca { get;set; }
        public int Edad { get; set; }
        public string Genero { get; set; }
    }


    public class CrearLuchadorCommandValidator : AbstractValidator<CrearLuchadorCommand>
    {
        public CrearLuchadorCommandValidator()
        {
            
            RuleFor(m => m.Nombre).NotEmpty().MinimumLength(1);
            RuleFor(m => m.Marca).NotEmpty();
            RuleFor(m => m.Edad).NotEmpty().Must( p => p >= 0);
            RuleFor(m => m.Genero).NotEmpty();


            
                
        }
    }


    public class CrearLuchadorCommandHandler
           : IRequestHandler<CrearLuchadorCommand, bool>
    {
        private readonly IMongoCollection<Luchador> luchadores;

        public CrearLuchadorCommandHandler(IEstudiantesDatabaseSettings settings)
        {
            var cliente = new MongoClient(settings.ConnectionString);
            var db = cliente.GetDatabase(settings.DatabaseName);

            luchadores = db.GetCollection<Luchador>("Luchadores");
        }

        public async Task<bool> Handle(CrearLuchadorCommand request, CancellationToken cancellationToken)
        {
           
           var mgLuchador = new Luchador() {
                Nombre = request.Nombre,
                Marca = request.Marca,
                Edad = request.Edad,
                Genero = request.Genero
            };

            await luchadores.InsertOneAsync(mgLuchador);

            return true;
            
        }
    }

    

}