using System;
using Epsic.Gestion_artistes.Rpg.Exceptions;
using Epsic.Gestion_artistes.Rpg.Models;
using Epsic.Gestion_artistes.Rpg.Services;
using Microsoft.AspNetCore.Mvc;

namespace Epsic.Gestion_artistes.Rpg.Controllers
{
    [ApiController]
    public class MusicController : ControllerBase
    {
        private readonly IMusicService _musicService;

        public MusicController(IMusicService musicService)
        {
            _musicService = musicService;
        }
        
        [HttpGet("getSingle/music/{id}")]
        public IActionResult GetSingleById(int id)
        {
            try
            {
                var result = _musicService.GetSingle(id);

                if (result == null)
                    return NotFound();

                return Ok(result);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("update/music/{id}")]
        public IActionResult Update([FromRoute] int id, [FromBody] UpdateMusicDto updateMusicDto)
        {
            try
            {
                return Ok(_musicService.Update(id, updateMusicDto));
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
        
        [HttpPost("create/music")]
        public IActionResult Create(CreateMusicDto createMusicDto)
        {
            try
            {
                var musicDb = _musicService.Create(createMusicDto);
                return Created($"music/{musicDb.Id}", musicDb);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
        }
        
        [HttpDelete("delete/music/{id}")]
        public IActionResult Delete(int id)
        {
            try
            {
                _musicService.Delete(id);
                return Ok();
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}