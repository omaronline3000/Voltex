using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Voltex.Core.DTOs;
using Voltex.Core.Entities.Product;
using Voltex.Core.Interfaces;

namespace Voltex.API.Controllers
{
    public class ProductController : BaseController
    {
        public ProductController(IUnitOfWork unitOfWork, IMapper mapper) : base(unitOfWork, mapper)
        {
        }

        [HttpGet("All")]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                var entities = await _unitOfWork.productRepository.GetAllAsync();
                if (entities is not null)
                    return Ok(entities);
                else return BadRequest();
            }
            catch (Exception ex)
            {

                return BadRequest(ex.Message);
            }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById([FromRoute]int id)
        {
            try
            {
                var entity = await _unitOfWork.productRepository.GetByIdAsync(id);
                if (entity is not null)
                    return Ok(entity);
                else return NotFound();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("Add")]
        public async Task<IActionResult> Add(ProductDTO productFromReq)
        {
            try
            {
                var product = _mapper.Map<Product>(productFromReq);

                await _unitOfWork.productRepository.AddAsync(product);
                return Created();
            }
            catch (Exception ex)
            {

                return BadRequest(ex.Message);
            }
        }
        [HttpPut("Edit")]
        public async Task<IActionResult> Update(UpdateProductDTO productFromReq)
        {
            try
            {
                var product = _mapper.Map<Product>(productFromReq);
                await _unitOfWork.productRepository.UpdateAsync(product);
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
                await _unitOfWork.productRepository.DeleteAsync(id);
                return NoContent();
            }
            catch (Exception ex)
            {

                return BadRequest();
            }
        }


    }
}
