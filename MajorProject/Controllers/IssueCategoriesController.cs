using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MajorProject.Data;
using MajorProject.Models;
using Microsoft.Extensions.Logging;

namespace MajorProject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class IssueCategoriesController : ControllerBase
    {
        private readonly MajorProjectDbContext _context;
        private readonly ILogger<IssueCategoriesController> _logger;

        public IssueCategoriesController(
            MajorProjectDbContext context,
            ILogger<IssueCategoriesController> logger)
        {
            _context = context;
            _logger = logger;
        }

        // GET: api/IssueCategories
        [HttpGet]
        public async Task<IActionResult> GetIssueCategories()
        {
            try
            {
                var pc = await _context.IssueCategories.ToListAsync(); // pc =  product categories
                if (pc == null)
                {
                    _logger.LogWarning("Hr- No Categories were found");
                    return NotFound();
                }
                _logger.LogInformation("Extracted all the categories");
                return Ok(pc);
            }
            catch
            {
                _logger.LogError("Attempt made to retrieve information");
                return BadRequest();
            }
        }


        //public async Task<ActionResult<IEnumerable<IssueCategory>>> GetIssueCategories()
        //{
        //    return await _context.IssueCategories.ToListAsync();
        //}

        // GET: api/IssueCategories/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetIssueCategory(int? id)
        {
            if (!id.HasValue)
            {
                return BadRequest();
            }
            try
            {
                var issueCategory = await _context.IssueCategories.FindAsync(id);
                if(issueCategory == null) { return NotFound(); }
                return Ok(issueCategory);
            }
            catch 
            {
                return BadRequest();
            }

            
        }

        // PUT: api/IssueCategories/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutIssueCategory(int id, IssueCategory issueCategory)
        {
            if (id != issueCategory.IssueCategoryId)
            {
                return BadRequest();
            }

            _context.Entry(issueCategory).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!IssueCategoryExists(id))
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

        // POST: api/IssueCategories
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<IActionResult> PostIssueCategory(IssueCategory issueCategory)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {


                _context.IssueCategories.Add(issueCategory);
                int countaffected = await _context.SaveChangesAsync();
                if(countaffected > 0)
                {
                    var result =   CreatedAtAction("GetIssueCategory", new { id = issueCategory.IssueCategoryId }, issueCategory);
                    return Ok(result);
                }
                else
                {
                    return NotFound();
                }
            }
            catch(System.Exception ex)
            {
                ModelState.AddModelError("Post", ex.Message);
                return BadRequest(ModelState);
            }
            
        }

        // DELETE: api/IssueCategories/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteIssueCategory(int? id)
        {
            if (!id.HasValue)
            {
                return BadRequest();
            }
            try
            {
                var a = await _context.IssueCategories.FindAsync(id);
                if (a == null)
                {
                    return NotFound();
                }

                _context.IssueCategories.Remove(a);
                await _context.SaveChangesAsync();

                return Ok(a);
            }
            catch
            {
                return BadRequest();
            }
            
        }

        private bool IssueCategoryExists(int id)
        {
            return _context.IssueCategories.Any(e => e.IssueCategoryId == id);
        }
    }
}
