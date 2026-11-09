using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Voltex.API.Helper;
using Voltex.Core.DTOs;
using Voltex.Core.Entities.Product;
using Voltex.Core.Interfaces;
using Voltex.Core.Services;

namespace Voltex.API.Controllers
{
    public class ProductController : BaseController
    {
        private readonly IImageManagementService _imageManagementService;
        public ProductController(IUnitOfWork unitOfWork, IMapper mapper , IImageManagementService imageManagementService) : base(unitOfWork, mapper)
        {
            _imageManagementService = imageManagementService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var Products = await _unitOfWork.productRepository.GetAllAsync(p => p.category , p => p.images);
            
            var ProductsDto = _mapper.Map<List<ProductDTO>>(Products);

            return Ok(new ResponseApi<IReadOnlyList<ProductDTO>>
                (message: "Successfully Returned Data", data: ProductsDto));

        }
        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetById(int id)
        {
            var Product = await _unitOfWork.productRepository.GetByIdAsync(id,p => p.category , p => p.images);

            if (Product is not null)
            {
             var ProductDto = _mapper.Map<ProductDTO>(Product);
                return Ok(new ResponseApi<ProductDTO>
                        (message: "Successfully Returned Data", data: ProductDto));
            }
                
            else return NotFound(new ProblemDetails()
            {
                Status = 404,
                Title = "Not Found",
                Detail = "Not Found Product with that Id"
            });
        }
        [HttpPost]
        public async Task<IActionResult> Add(AddProductDTO ProductDTO)
        {
            //TODO: Add business Logic Validation for Duplicates Names


            // Saving Product
            var Product = _mapper.Map<Product>(ProductDTO);
            var entity = await _unitOfWork.productRepository.AddAsync(Product);

            // Saving Images
            var ImagesUrls = await _imageManagementService.SaveImageAync(ProductDTO.Images, ProductDTO.Name);
            var Images = ImagesUrls.Select(i => new Image()
            {
                ImageName = i,
                ProductId = entity.Id
            }).ToList();
            await _unitOfWork.imageRepository.AddRangeAsync(Images);
            var NewProduct = _mapper.Map<ProductDTO>(entity);

            return CreatedAtAction(nameof(GetById),
                new { id = Product.Id },
                new ResponseApi<ProductDTO>(message: "Successfully Created Resource", data: NewProduct));

        }

        [HttpPut]
        public async Task<IActionResult> Update(UpdateProductDTO ProductDTO)
        {
            var Product = await _unitOfWork.productRepository.GetByIdAsync(ProductDTO.id);
            if (Product is not null)
            {
                _mapper.Map(ProductDTO, Product);
                await _unitOfWork.productRepository.UpdateAsync(Product);
                return NoContent();
            }
            else return NotFound(new ProblemDetails()
            {
                Status = 404,
                Title = "Not Found",
                Detail = "Not Found Product with that Id"
            });


        }
        [HttpDelete]
        public async Task<IActionResult> Delete(int id)
        {

            var Product = await _unitOfWork.productRepository.GetByIdAsync(id);
            if (Product is not null)
            {
                await _unitOfWork.productRepository.DeleteAsync(Product);
                return NoContent();
            }
            else return NotFound(new ProblemDetails()
            {
                Status = 404,
                Title = "Not Found",
                Detail = "Not Found Product with that Id"
            });


        }

    }
}
