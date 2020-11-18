using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using FluentValidation;
using MediatR;
using MongoDB.Driver;

namespace aspnetdemo2.domain.luchadores {


    public class BorrarLuchadorCommand : IRequest<bool> {
        public BorrarLuchadorCommand(string id)
        {
            Id = id;
        }
        public BorrarLuchadorCommand()
        {
            
        }

        public string Id { get;  set;}
        

    }


    public class BorrarLuchadorCommandValidator : AbstractValidator<BorrarLuchadorCommand>
    {
        public BorrarLuchadorCommandValidator()
        {
           
        }
    }


    public class BorrarLuchadorCommandHandler
           : IRequestHandler<BorrarLuchadorCommand, bool>
    {
        private readonly IMongoCollection<Luchador> luchadores;

        public BorrarLuchadorCommandHandler(IEstudiantesDatabaseSettings settings)
        {
            var cliente = new MongoClient(settings.ConnectionString);
            var db = cliente.GetDatabase(settings.DatabaseName);

            luchadores = db.GetCollection<Luchador>("Luchadores");
        }

        public async Task<bool> Handle(BorrarLuchadorCommand request, CancellationToken cancellationToken)
        {
           await luchadores
                    .DeleteOneAsync( luc => luc.Id == request.Id);
            
            return true;
        }
    }

    

}