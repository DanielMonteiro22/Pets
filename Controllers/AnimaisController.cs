using Animais.Model;
using Microsoft.AspNetCore.Mvc;

namespace Animais.Controllers
{
    public class AnimaisController : Controller
    {
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
