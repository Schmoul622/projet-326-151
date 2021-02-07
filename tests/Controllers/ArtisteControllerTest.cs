using System.Collections.Generic;
using System.Threading.Tasks;
using Epsic.Gestion_artistes.Rpg.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace tests.Controllers
{
    [TestClass]
    public class ArtistesControllerTests : ApiControllerTestBase
    {
        [TestMethod]
        public async Task CreateArtisteNameValidatorSuccess()
        {
            var artisteDto = new CreateArtisteDto { Speudo = "93 Empire" };

            var response = await PostAsync<CreateArtisteDto, RfcError>("/create/artiste", artisteDto);

            Assert.IsNull(response.Errors);
        }
    }
}