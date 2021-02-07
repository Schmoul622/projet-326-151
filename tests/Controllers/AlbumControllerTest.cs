using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Epsic.Gestion_artistes.Rpg.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace tests.Controllers
{
    [TestClass]
    public class AlbumsControllerTests : ApiControllerTestBase
    {
        [TestMethod]
        public async Task AlbumsGetAll()
        {
            var reponse = await GetAsync<IList<Album>>("/getAll/albums");

            Assert.AreEqual(3, reponse.Count);
        }

        [TestMethod]
        [DataRow(1)]
        [DataRow(2)]
        [DataRow(3)]
        public async Task AlbumGetSingle(int id)
        {
            var reponse = await GetAsync<Album>($"/getSingle/album/{id}");

            Assert.AreEqual(id, reponse.Id);
        }
        
        [TestMethod]
        [DataRow(123)]
        [DataRow(1234)]
        [DataRow(5)]
        public async Task AlbumGetSingleIdNotExist(int id)
        {
            // Act
            var response = await GetAsync($"/getSingle/album/{id}");

            // Assert
            Assert.AreEqual(Microsoft.AspNetCore.Http.StatusCodes.Status404NotFound, (int)response.StatusCode);
        }

        [TestMethod]
        [DataRow(4, "TestAlbum", 10)]
        public async Task AlbumCreate(int id, string name, int numberTitle)
        {
            var album = new Album {Id = id, Name = name, NumberTitles = numberTitle};

            var reponse = await PostAsync<Album>("/create/albums", album);

            Assert.AreEqual(id, reponse.Id);
            Assert.AreEqual(name, reponse.Name);
            Assert.AreEqual(numberTitle, reponse.NumberTitles);
        }
        
        [TestMethod]
        [DataRow("Jeannine")]
        [DataRow("Flip")]
        [DataRow("Amina")]
        public async Task AddNameExist(string name)
        {
            //Arrange
            var model = new Album { Name = name, Id = 6 };

            // Act
            var response = await PostBasicAsync($"/create/albums", model);

            // Assert
            Assert.AreEqual(Microsoft.AspNetCore.Http.StatusCodes.Status400BadRequest, (int)response.StatusCode);
        }
    }
}