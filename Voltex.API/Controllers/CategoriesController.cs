using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Voltex.Core.DTOs;
using Voltex.Core.Entities.Product;
using Voltex.Core.Interfaces;
using Voltex.API.Helper;
namespace Voltex.API.Controllers
{
    public class CategoriesController : BaseController
    {
        public CategoriesController(IUnitOfWork unitOfWork, IMapper mapper) : base(unitOfWork, mapper)
        {
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var categories = await _unitOfWork.categoryRepository.GetAllAsync();
                    return Ok(new ResponseApi<IReadOnlyList<Category>>
                        (message: "Successfully Returned Data" , data: categories));

        }
        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetById(int id)
        {
                var category = await _unitOfWork.categoryRepository.GetByIdAsync(id);

                if(category is not null) 
                return Ok(new ResponseApi<Category>
                        (message: "Successfully Returned Data", data: category));
                else return NotFound(new ProblemDetails()
                {
                    Status = 404,
                    Title = "Not Found",
                    Detail = "Not Found Category with that Id"
                });
        }
        [HttpPost]
        public async Task<IActionResult> Add(CategoryDTO categoryDTO)
        {
                 //TODO: Add business Logic Validation for Duplicates Names

                var category = _mapper.Map<Category>(categoryDTO);

                var entity = await _unitOfWork.categoryRepository.AddAsync(category);

                var NewCategory = _mapper.Map<CategoryDTO>(entity); 

                return CreatedAtAction(nameof(GetById) ,
                    new {id = category.Id } ,
                    new ResponseApi<CategoryDTO>(message: "Successfully Created Resource" , data: NewCategory));
            
        }

        [HttpPut]
       public async Task<IActionResult> Update(UpdateCategoryDTO categoryDTO)
        {
            var category = await _unitOfWork.categoryRepository.GetByIdAsync(categoryDTO.id);
            if (category is not null)
            {
                _mapper.Map(categoryDTO, category);
                await _unitOfWork.categoryRepository.UpdateAsync(category);
                return NoContent();
            }
            else return NotFound(new ProblemDetails()
            {
                Status = 404,
                Title = "Not Found",
                Detail = "Not Found Category with that Id"
            });
                
           
        }
        [HttpDelete]
        public async Task<IActionResult> Delete(int id)
        {

            var category = await _unitOfWork.categoryRepository.GetByIdAsync(id);
            if (category is not null)
            {
                await _unitOfWork.categoryRepository.DeleteAsync(category);
                return NoContent();
            }
            else return NotFound(new ProblemDetails()
            {
                Status = 404,
                Title = "Not Found",
                Detail = "Not Found Category with that Id"
            });

            
        }

    }
}
