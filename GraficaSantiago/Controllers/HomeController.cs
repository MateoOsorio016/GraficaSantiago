using GraficaSantiago.Models;
using GraficaSantiago.Models.ViewModel;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace GraficaSantiago.Controllers
{
    public class HomeController : Controller
    {
        private readonly GraficoContext _dbcontext;

        public HomeController(GraficoContext context)
        {
            _dbcontext = context;
        }

        public IActionResult resumenVenta()
        {
            DateTime FechaInicio = DateTime.Now;
            FechaInicio = FechaInicio.AddDays(-5);
            var Lista = (from data in _dbcontext.Venta.ToList()
                         group data by data.FechaRegistro into gr
                         select new ViewModelVenta
                         {
                             cantidad = gr.Count(),
                             fecha = (DateTime)gr.Key
                         }
                         );
            return View(Lista);

        }

        public IActionResult resumenProducto()
        {
            List<ViewModelProducto>Lista = (from tbdetalleventa in _dbcontext.DetalleVenta.ToList()
                                            group tbdetalleventa by tbdetalleventa.NombreProducto into grupo
                                            orderby grupo.Count() descending
                                            select new ViewModelProducto
                                            {
                                                producto = grupo.Key,
                                                cantidad = grupo.Count(),
                             
                                            }
                                            ).Take( 4 ).ToList();
            //return View(Lista);
                                            return StatusCode(StatusCodes.Status200OK, Lista);
        }


        public IActionResult Index()
        {
            return View();
        }
        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}