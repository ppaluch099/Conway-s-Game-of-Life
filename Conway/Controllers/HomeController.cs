using Conway.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using System.Text.Json;

namespace Conway.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private static Population population;
        private static int turn;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            population = new Population(50);
            turn = 0;
            ViewData["Grid"] = population.grid;
            return View();
        }

        class jsonObject {
            public int x { get; set; }
            public int y { get; set; }
        }

        [HttpPost]
        public ActionResult Game(string start)
        {
            if (turn == 0)
            {
                var output = JsonSerializer.Deserialize<List<jsonObject>>(start);
                foreach (var item in output)
                {
                    population.grid[item.x, item.y] = 1;
                }
            }

            Population tempGrid = new Population(50);

            for (int i = 0; i < tempGrid.grid.GetLength(0); i++)
            {
                for (int j = 0; j < tempGrid.grid.GetLength(1); j++)
                {
                    if (population.CellLife(i, j))
                    {
                        tempGrid.grid[i, j] = 1;
                    }
                    else
                    {
                        tempGrid.grid[i, j] = 0;
                    }
                }
            }

            ViewData["Grid"] = tempGrid.grid;
            population.grid = tempGrid.grid;
            turn++;

            return PartialView("_Grid", tempGrid);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}