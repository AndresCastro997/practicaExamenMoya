using System;
using System.Collections.Generic;
using System.Linq;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Driver;


namespace aspnetdemo2 {


 public class Luchador {
        
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }
        public string Nombre { get; set; }  
        public string Marca { get; set; }
        public int Edad { get; set; }
        public string Genero  { get; set; }

       
        
        }

}