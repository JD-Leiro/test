using AspNetCore.Reporting;
using Microsoft.AspNetCore.Mvc;
using System.Data;
using System.Diagnostics;
using System.Diagnostics.Eventing.Reader;
using WebReportRdl.Models;

namespace WebReportRdl.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IWebHostEnvironment _webHostEnvironment;
        public HomeController(ILogger<HomeController> logger, IWebHostEnvironment webHostEnvironment)
        {
            _logger = logger;
            _webHostEnvironment = webHostEnvironment;   
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
        public IActionResult Imprimir()
        {
            System.Text.Encoding.RegisterProvider(System.Text.CodePagesEncodingProvider.Instance);
            DataTable dt = new();
            dt.Columns.Add("NoVisita");
            dt.Columns.Add("Agencia");
            dt.Columns.Add("Banco");
            dt.Columns.Add("Cantidad");
            dt.Columns.Add("Km");
            for (int i = 1; i < 10; i++)
            {
                dt.Rows.Add(i, "Agencia Nombre", "Banco Nombre", "12", "23km");
            }
            var ruta = "C:\\AgentesCercanosAdminPDF.rdl";

            string webRootPath = _webHostEnvironment.WebRootPath;
            string contentRootPath = _webHostEnvironment.ContentRootPath;
            string a = "";
            //string path = "";
            //path = Path.Combine(webRootPath, "AgentesCercanosAdminPDF.rdl");
            //or path = Path.Combine(contentRootPath , "wwwroot" ,"CSS" );
            LocalReport localReport = new(ruta);
            localReport.AddDataSource("DetalleAgentesCercanos", dt);
            Dictionary<string, string> parametros = new Dictionary<string, string>
            {
                {"NombreImplementador", "Jesús David Leiro Pacheco" },
                {"Region", "Acapulco" },
                {"FechaDelReporte", "01/09/2022" },
                {"Semana", "32" },
                {"TotalVisitas", "4" },
            };
            var rpt = localReport.Execute(RenderType.Pdf, 1, parametros);
            return File(rpt.MainStream, "application/pdf");
        }

        public IActionResult ImprimirOtro()
        {
            System.Text.Encoding.RegisterProvider(System.Text.CodePagesEncodingProvider.Instance);
            //DataTable dt = new();
            //dt.Columns.Add("NoVisita");
            //dt.Columns.Add("Agencia");
            //dt.Columns.Add("Banco");
            //dt.Columns.Add("Cantidad");
            //dt.Columns.Add("Km");
            //for (int i = 1; i < 10; i++)
            //{
            //    dt.Rows.Add(i, "Agencia Nombre", "Banco Nombre", "12", "23km");
            //}
            var ruta = "C:\\animal.rdl";

            string webRootPath = _webHostEnvironment.WebRootPath;
            string contentRootPath = _webHostEnvironment.ContentRootPath;

            string path = "";
            path = Path.Combine(webRootPath, "animal.rdl");
            //or path = Path.Combine(contentRootPath , "wwwroot" ,"CSS" );
            LocalReport localReport = new(ruta);
            //localReport.AddDataSource("DetalleAgentesCercanos", dt);
            //Dictionary<string, string> parametros = new Dictionary<string, string>
            //{
            //    {"NombreImplementador", "Jesús David Leiro Pacheco" },
            //    {"Region", "Acapulco" },
            //    {"FechaDelReporte", "01/09/2022" },
            //    {"Semana", "32" },
            //    {"TotalVisitas", "4" },
            //};
            var rpt = localReport.Execute(RenderType.Pdf);
            return File(rpt.MainStream, "application/pdf");
        }
    }
}