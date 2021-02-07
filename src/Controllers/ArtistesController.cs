using System;
using System.IO;
using System.Threading.Tasks;
using Epsic.Gestion_artistes.Rpg.Exceptions;
using Epsic.Gestion_artistes.Rpg.Models;
using Epsic.Gestion_artistes.Rpg.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Epsic.Gestion_artistes.Rpg.Controllers
{
    [ApiController]
    public class ArtistesController : ControllerBase
    {
        private readonly IArtisteService _artisteService;
        private readonly IAlbumService _albumService;

        public ArtistesController(IArtisteService artisteService, IAlbumService albumService)
        {
            _artisteService = artisteService;
            _albumService = albumService;
        }

        [HttpGet("getSingle/artiste/{id}")]
        public IActionResult GetSingleById(int id)
        {
            try
            {
                var result = _artisteService.GetSingle(id);

                if (result == null)
                    return NotFound();

                return Ok(result);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
        }
        
        [HttpGet("getAll/artistes")]
        public IActionResult GetAllArtiste()
        {
            try
            {
                return Ok( _artisteService.GetAllArtiste());
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("update/artiste/{id}")]
        public IActionResult Update([FromRoute] int id, [FromBody] UpdateArtisteDto model)
        {
            try
            {
                return Ok(_artisteService.Update(id, model));
            }
            catch (DataNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
        }
        
        [HttpPost("create/artiste")]
        public IActionResult Create(CreateArtisteDto model)
        {
            try
            {
                var modelDb = _artisteService.Create(model);
                return Created($"artiste/{modelDb.Id}", modelDb);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
        }
        
        [HttpDelete("delete/artiste/{id}")]
        public IActionResult Delete(int id)
        {
            try
            {
                _artisteService.Delete(id);
                return Ok();
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("update/connection/artiste/album")]
        public async Task<IActionResult> AddAlbumToArtisteAsync(AddAlbumToArtisteDto addAlbumToArtiste)
        {
            try
            {
                await _artisteService.AddAlbumToArtiste(addAlbumToArtiste);
                return Ok();
            }
            catch (DataNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("artistes/{id}/setAvatar")]
        public IActionResult SetPictureAsync([FromRoute] int id, IFormFile file)
        {
            var ms = new MemoryStream();
            file.CopyTo(ms);
            _artisteService.SetPicture(id, ms.ToArray());
            return Ok();
        }

        [HttpGet("artistes/{id}/getAvatar")]
        public IActionResult GetPictureAsync([FromRoute] int id)
        {
            var avatar = _artisteService.GetPicture(id);
            
            // Ne retourne rien si l'image est null (utile pour le front-end)
            if (avatar == null || avatar.Length == 0)
            {
                return NoContent();
            }
            return File(_artisteService.GetPicture(id), "image/jpeg");
        }
    }
}