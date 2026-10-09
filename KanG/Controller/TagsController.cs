using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using KanG.Models;
using KanG.Services;
using KanG.Services.IService;

namespace KanG.Controller
{
    [Route("api/[controller]")]
    [ApiController]
    public class TagsController : ControllerBase // คอนโทรลเลอร์จัดการข้อมูลป้ายกำกับ
    {
        private readonly ITagService _tagService;

        public TagsController(ITagService tagService)
        {
            _tagService = tagService;
        }

        // GET: api/Tags
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Tag>>> GetTags()
        {
            var tags = await _tagService.GetAllTagsAsync();
            return Ok(tags);
        }

        // GET: api/Tags/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Tag>> GetTag(int id)
        {
            var item = await _tagService.GetTagByIdAsync(id);
            if (item == null)
            {
                return NotFound();
            }
            return Ok(item);
        }
        
        // POST: api/Tags
        [Authorize(Roles = "Admin")]
        [HttpPost]
        public async Task<ActionResult<Tag>> PostTag(Tag item)
        {
            var created = await _tagService.CreateTagAsync(item);
            return CreatedAtAction(nameof(GetTag), new { id = created.Id }, created);
        }
        
        // DELETE: api/Tags/5
        [Authorize(Roles = "Admin")]
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTag(int id)
        {
            var success = await _tagService.DeleteTagAsync(id);
            if (!success)
            {
                return NotFound();
            }
            return NoContent();
        }
    }
}

