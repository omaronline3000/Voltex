using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Voltex.Core.DTOs;
using Voltex.Core.Entities.Product;
using Voltex.Core.Interfaces;

namespace Voltex.API.Controllers
{
    public class CategoriesController : BaseController
    {
        public CategoriesController(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }

        [HttpGet("All")]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                var categories = await _unitOfWork.categoryRepository.GetAllAsync();
                if (categories is not null)
                    return Ok(categories);
                return BadRequest();
            }
            catch (Exception ex) { 
                return NotFound(ex.Message);
            }
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            try
            {
                var category = await _unitOfWork.categoryRepository.GetByIdAsync(id);
                if(category is not null) return Ok(category);
                return BadRequest();
            }
            catch (Exception ex)
            {

                return NotFound(ex.Message);
            }
        }
        [HttpPost("add")]
        public async Task<IActionResult> Add(CategoryDTO categoryDTO)
        {
            try
            {
                var category = new Category() { CategoryTitle = categoryDTO.Title, Description = categoryDTO.Description };
                await _unitOfWork.categoryRepository.AddAsync(category);
                return Created();
            }
            catch (Exception ex)
            {

                return BadRequest(ex.Message);
            }
        }

        [HttpPut("Edit")]
       public async Task<IActionResult> Update(UpdateCategoryDTO categoryDTO)
        {
            try
            {
                var category = new Category()
                {
                    CategoryTitle = categoryDTO.Titel,
                    Description = categoryDTO.Description,
                    Id = categoryDTO.id
                };
                await _unitOfWork.categoryRepository.UpdateAsync(category);
                return Ok();
            }
            catch (Exception ex)
            {

                return BadRequest(ex.Message);
            }
        }
        [HttpDelete("Delete")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                await _unitOfWork.categoryRepository.DeleteAsync(id);
                return NoContent();
            }
            catch (Exception)
            {

                return BadRequest();
            }
        }

    }
}
