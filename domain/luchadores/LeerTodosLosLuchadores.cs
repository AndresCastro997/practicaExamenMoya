using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using MongoDB.Driver;

namespace aspnetdemo2.domain.luchadores {


    public class LeerTodosLosLuchadores : IRequest<List<LuchadorIndexModel>> {

     }

     public class LuchadorIndexModel {
         public string Id {get; set;} 
        public string   Nombre { get; set; }
        public string Marca { get; set; }
        public int Edad { get; set; }

        public string Genero {get; set;}

     }

    public class LeerTodosLosLuchadoresHandler : IRequestHandler<LeerTodosLosLuchadores,List<LuchadorIndexModel>>
    {
        private readonly IMongoCollection<Luchador> luchadores;

        public LeerTodosLosLuchadoresHandler(IEstudiantesDatabaseSettings settings)
        {
            var cliente = new MongoClient(settings.ConnectionString);
            var db = cliente.GetDatabase(settings.DatabaseName);

            luchadores = db.GetCollection<Luchador>("Luchadores");
        }

        public async Task<List<LuchadorIndexModel>> Handle(LeerTodosLosLuchadores request, CancellationToken cancellationToken)
        {
            var resultado = await luchadores.FindAsync<Luchador>(luc => true);
            var res = resultado.ToList().Select(luc => 
                    new LuchadorIndexModel() {
                        Id = luc.Id,
                        Nombre = luc.Nombre,
                        Marca = luc.Marca,
                        Edad = luc.Edad,
                        Genero = luc.Genero
                    }
            );

            return res.ToList();
        }
    }

}