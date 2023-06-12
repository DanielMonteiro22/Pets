using Animais.Model;
using Microsoft.AspNetCore.Mvc;

namespace Animais.Controllers
{
    public class AnimaisController : Controller
    {
        public const int ItemPag = 5;

        public static List<AnimaisModel> pets = new List<AnimaisModel>

        {
            new AnimaisModel { id= 1, nome = "Bily", raca = "Vira-Lata", animal = "Cachorro"},

            new AnimaisModel { id= 2, nome = "Megue", raca = "Vira-Lata", animal = "Cachorro"},

            new AnimaisModel { id= 3, nome = "Sofia", raca = "Siames", animal = "Gato"},

            new AnimaisModel { id= 4, nome = "Pudim", raca = "SRD", animal = "Gato"},

            new AnimaisModel { id= 5, nome = "Cara-Preta", raca = "Bombaim", animal = "Gato"},

            new AnimaisModel { id= 6, nome = "Branquela", raca = "Angorá", animal = "Gato"}



        };

        [HttpGet]
        [Route("api/PaginasPets")]
        
        public ActionResult<IEnumerable<string>> PaginarPets(int pagina = 1)
        {
            var totalPagina = (int)Math.Ceiling((double)pets.Count / ItemPag);
            var currentPage = pagina > 0 ? pagina : 1;

            var pagedPets = pets.Skip((currentPage - 1) * ItemPag).Take(ItemPag);
                
            var resultado = new
            {
                Pagina = currentPage,
                totalPagina = totalPagina,
                pets = pagedPets
            };
            return Ok(resultado);
        }

        //[HttpGet]
        //[Route("api/PaginasPets")]

        //public IActionResult PaginasPets (int page)
        //{
        //    int petsPerPage = 5;
        //    int totalPets = pets.Count;
        //    int totalPages = (int)Math.Ceiling((double)totalPets / ItemPag);

        //    return Ok(pets);
        //}

        [HttpPost]
        [Route("api/AdicionarPets")]
        public IActionResult AdicionarPets([FromBody] AnimaisModel PetsNovos)
        {
            if(PetsNovos == null)
            {
                return BadRequest();
            }
            PetsNovos.id = pets.Count + 1;
            pets.Add(PetsNovos);
            return Ok(PetsNovos);
        }

        [HttpGet]
        [Route("api/SumarioPets")]
        public IActionResult SumarioPets()
        {
            var sumario = pets.GroupBy(p => p.animal)
                              .Select(g => new { animal = g.Key, Quantidade = g.Count() })
                              .ToDictionary(x => x.animal, x => x.Quantidade);
            return Ok(sumario);
        }

        [HttpGet]
        [Route("api/ListarPets")]

        public IActionResult ListarTodosPets()
        {
            return Ok(pets);
        }

        [HttpGet]
        [Route("api/ListarPets/{raca}")]

        public IActionResult listarPetsRaca(string raca)
        {
            var especie = pets.Where(p => p.raca == raca);
            if (especie == null)
            {
                return NoContent();
            }
            return Ok(especie);
        }
    }
}
