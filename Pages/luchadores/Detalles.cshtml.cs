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

namespace aspnetdemo2.Pages.luchadores
{
    public class DetallesModel : PageModel
    {

        
        public LuchadorDetalleModel Detalle {get; set;}
        private readonly ILogger<CrearModel> _logger;
        private IMediator mediator;

        public DetallesModel(ILogger<CrearModel> logger
        ,IMediator media)
        {
            _logger = logger;
            
            mediator = media;
        }

        public async Task<IActionResult> OnGet(string Id)
        {
           var luchador = await  mediator.Send(new LeerLuchadorPorId(Id));
            if(luchador == null){
                return NotFound();
            }
            Detalle = luchador;

            return Page();
        }

    }
}
