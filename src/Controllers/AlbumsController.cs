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
    public class AlbumController : ControllerBase
    {
        private readonly IAlbumService _albumService;
        private readonly IMusicService _musicService;
        
        public AlbumController(IAlbumService albumService, IMusicService musicService)
        {
            _albumService = albumService;
            _musicService = musicService;
        }

        [HttpGet("getSingle/album/{id}")]
        public IActionResult GetSingle(int id)
        {
            try
            {
                var result = _albumService.GetSingle(id);

                if (result == null)
                    return NotFound();

                return Ok(result);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex);
            }
        }

        [HttpPost("update/albums/{id}")]
        public IActionResult Update([FromRoute] int id, [FromBody] UpdateAlbumDto updateAlbumDto)
        {
            try
            {
                return Ok(_albumService.Update(id, updateAlbumDto));
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
        
        [HttpPost("create/albums")]
        public IActionResult Create(CreateAlbumDto createAlbumDto)
        {
            try
            {
                var albumDb = _albumService.Create(createAlbumDto);
                return Created($"album/{albumDb.Id}", albumDb);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("delete/albums/{id}")]
        public IActionResult Delete(int id)
        {
            try
            {
                _albumService.Delete(id);
                return Ok();
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
        }
        
        [HttpPost("update/connection/album/music")]
        public async Task<IActionResult> AddMusicToAlbumAsync(AddMusicToAlbumDto addMusicToAlbum)
        {
            try
            {
                await _albumService.AddMusicToAlbum(addMusicToAlbum);
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

        [HttpPost("album/{id}/setPicture")]
        public IActionResult SetPicture([FromRoute] int id, IFormFile file)
        {
            var ms = new MemoryStream();
            file.CopyTo(ms);
            _albumService.SetPicture(id, ms.ToArray());
            return Ok();
        }
        
        [HttpGet("album/{id}/getPicture")]
        public IActionResult GetPicture([FromRoute] int id)
        {
            var avatar = _albumService.GetPicture(id);
            
            // Ne retourne rien si l'image est null (utile pour le front-end)
            if (avatar == null || avatar.Length == 0)
            {
                return NoContent();
            }
            return File(_albumService.GetPicture(id), "image/jpeg");
        }
    }
}