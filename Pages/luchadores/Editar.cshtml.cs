using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using System.ComponentModel.DataAnnotations;
using MediatR;
using aspnetdemo2.domain.luchadores;
using Microsoft.AspNetCore.Http;
using System.IO;
using Microsoft.Extensions.Configuration;
using Azure.Storage.Blobs;

namespace aspnetdemo2.Pages.luchadores
{
    public class EditarModel : PageModel
    {

        
        [BindProperty]
        public LuchadorAEditar Detalle {get; set;}
        private readonly ILogger<EditarModel> _logger;
        private readonly IMediator mediator;
        private readonly IConfiguration configuracion;

        //private IEstudiantesRespository repo;

       

        public EditarModel(ILogger<EditarModel> logger,
         IMediator mediat,
         IConfiguration config)
        {
            _logger = logger;
            
            mediator = mediat;
            configuracion = config;
        }

        public async Task<IActionResult> OnGet(string id)
        {
            var luchador = await mediator.Send(new LeerLuchadorPorId(id));
          
            if(luchador == null){
                return NotFound();
            }
            Detalle = new LuchadorAEditar() {
                Id = luchador.Id,
                Nombre = luchador.Nombre,
                Marca = luchador.Marca,
                Edad = luchador.Edad,
                Genero = luchador.Genero
            };

            return Page();
        }

        public async Task<IActionResult> OnPost(EditarLuchadorCommand cmd){

            if(!ModelState.IsValid){
                Detalle = new LuchadorAEditar() {
                Id = cmd.Id, 
                Nombre = cmd.Nombre,
                Marca = cmd.Marca,
                Edad = cmd.Edad,
                Genero = cmd.Genero
                };
                return Page();
            }

            var luchador = await mediator.Send(new LeerLuchadorPorId(cmd.Id));
          
            if(luchador == null){
                return NotFound();
            }
            
            var res = await mediator.Send(cmd);


            return RedirectToPage("./Index");
        }

        public class LuchadorAEditar {
            public string Id { get; set; }  
             public string Nombre { get; set; }
            public string Marca { get; set; }
            public int Edad { get; set; }
            public string Genero { get; set; }
            
        }
    }
}
