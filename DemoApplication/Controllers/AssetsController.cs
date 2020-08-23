using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using DemoApplication.Data;
using DemoApplication.Domain;
using DemoApplication.Service;
using DemoApplication.Models;

namespace DemoApplication.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AssetsController : ControllerBase
    {
        private readonly DemoContext _context;
        private readonly ICognitiveAnalyzeService _cognitiveAnalyzeService;

        public AssetsController(DemoContext context, ICognitiveAnalyzeService cognitiveAnalyzeService)
        {
            _context = context;
            _cognitiveAnalyzeService = cognitiveAnalyzeService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Asset>>> GetAssets()
        {
            return await _context.Assets.ToListAsync();
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Folder>>> GetFolders()
        {
            return await _context.Folders.ToListAsync();
        }

        // GET: api/Assets/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Asset>> GetAsset(int id)
        {
            var asset = await _context.Assets.FindAsync(id);

            if (asset == null)
            {
                return NotFound();
            }

            return asset;
        }

        // PUT: api/Assets/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutAsset(int id, Asset asset)
        {
            if (id != asset.Id)
            {
                return BadRequest();
            }

            _context.Entry(asset).State = EntityState.Modified;
            CognitiveResponseModel response = (await _cognitiveAnalyzeService.GetMetadatasFromAzureCognitive(asset.Data));
            List<Variant> variants = response.Categories.Select(p => new Variant() { Description = p.Name }).ToList();
            asset.SetVariants(variants);

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!AssetExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Assets
        [HttpPost]
        public async Task<ActionResult<Asset>> PostAsset(Asset asset)
        {
            _context.Assets.Add(asset);
            CognitiveResponseModel response = (await _cognitiveAnalyzeService.GetMetadatasFromAzureCognitive(asset.Data));
            List<Variant> variants = response.Categories.Select(p => new Variant() { Description = p.Name }).ToList();
            asset.SetVariants(variants);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetAsset", new { id = asset.Id }, asset);
        }

        // DELETE: api/Assets/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Asset>> DeleteAsset(int id)
        {
            var asset = await _context.Assets.FindAsync(id);
            if (asset == null)
            {
                return NotFound();
            }

            _context.Assets.Remove(asset);
            await _context.SaveChangesAsync();

            return asset;
        }

        private bool AssetExists(int id)
        {
            return _context.Assets.Any(e => e.Id == id);
        }
    }
}
