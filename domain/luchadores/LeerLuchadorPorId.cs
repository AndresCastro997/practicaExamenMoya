using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using MongoDB.Driver;

namespace aspnetdemo2.domain.luchadores {


    public class LeerLuchadorPorId : IRequest<LuchadorDetalleModel> {
        public LeerLuchadorPorId(string id)
        {
            Id = id;
        }

        public string Id { get; set; }

     }

     public class LuchadorDetalleModel {

          public string   Id { get; set; }

        public string   Nombre { get; set; }
        public string Marca { get; set; }
        public int Edad { get; set; }
        public string Genero { get; set; }

     }

    public class LeerLuchadorPorIdHandler
           : IRequestHandler<LeerLuchadorPorId, LuchadorDetalleModel>
    {
        private readonly IMongoCollection<Luchador> luchadores;

        public LeerLuchadorPorIdHandler(IEstudiantesDatabaseSettings settings)
        {
            var cliente = new MongoClient(settings.ConnectionString);
            var db = cliente.GetDatabase(settings.DatabaseName);

            luchadores = db.GetCollection<Luchador>("Luchadores");
        }

        public async Task<LuchadorDetalleModel> Handle(LeerLuchadorPorId request, CancellationToken cancellationToken)
        {
            
            var mgLuchador = (await luchadores
                    .FindAsync<Luchador>( 
                            luc => luc.Id == request.Id
                            )
            ).FirstOrDefault();
            
           
           if(mgLuchador is Luchador m ){
            return new LuchadorDetalleModel() {
                Id = m.Id,
                Nombre = m.Nombre,
                Marca = m.Marca,
                Edad = m.Edad,
                Genero = m.Genero
            };
           }else{
               return null;
           }
        }
    }

}