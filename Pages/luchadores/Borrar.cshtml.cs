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
using Microsoft.AspNetCore.Authorization;

namespace aspnetdemo2.Pages.luchadores
{
    [Authorize(Roles = "Admin")]
    public class BorrarModel : PageModel
    {

        
        [BindProperty]
        public LuchadorABorrar Detalle {get; set;}
        private readonly ILogger<BorrarModel> _logger;
        private readonly IMediator mediator;

        public BorrarModel(ILogger<BorrarModel> logger,
         IMediator mediat)
        {
            _logger = logger;
            mediator = mediat;
            
        }

        public async Task<IActionResult> OnGet(string Id)
        {
           var luchador = await mediator.Send(new LeerLuchadorPorId(Id));
          
            if(luchador == null){
                return NotFound();
            }
            Detalle = new LuchadorABorrar() {
                Id = luchador.Id,
                Nombre = luchador.Nombre,
            };

            return Page();
        }

        public async Task<IActionResult> OnPost(BorrarLuchadorCommand cmd){

            if(!ModelState.IsValid){
                Detalle = new LuchadorABorrar() {
                Id = cmd.Id,
               
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

        public class LuchadorABorrar {
            public string Id { get; set; }  
            public string Nombre { get; set; }
            
            
        }
    }
}
