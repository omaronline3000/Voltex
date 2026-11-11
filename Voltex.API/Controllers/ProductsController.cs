using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Voltex.API.Helper;
using Voltex.Core.DTOs;
using Voltex.Core.Entities.Product;
using Voltex.Core.Helper;
using Voltex.Core.Interfaces;
using Voltex.Core.Services;
using Voltex.Core.Sharing;

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
        public async Task<IActionResult> GetAll([FromQuery] QueryParameters parameters)
        {
            var Products = await _unitOfWork.productRepository.GetAllAsync(parameters,includes: [p => p.images]);
            
            return Ok(new ResponseApi<PagedResults<ProductDTO>>
                (message: "Successfully Returned Data", data: Products));

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

        [HttpPut("{id:int}")]
        public async Task<IActionResult> Update(int id , UpdateProductDTO ProductDTO)
        {
            var Product = await _unitOfWork.productRepository.GetByIdAsync(id, p=> p.images);
            if (Product is not null)
            {
                // Update Products
                _mapper.Map(ProductDTO, Product);
                await _unitOfWork.productRepository.UpdateAsync(Product);

                // Delete Old Images from Storage
                foreach(var image in Product.images)
                {
                    _imageManagementService.DeleteImageAsync(image.ImageName);
                }
                // Delete Old Images Urls From Database
                foreach(var image in Product.images)
                {
                    await _unitOfWork.imageRepository.DeleteAsync(image);
                }

                // Add New images to the server storage
                var ImageUrls = await _imageManagementService.SaveImageAync(ProductDTO.Images, ProductDTO.Name);
                // Add New Images Urls to Database
                var Images = ImageUrls.Select(i => new Image()
                {
                    ImageName = i,
                    ProductId = Product.Id
                });
                await _unitOfWork.imageRepository.AddRangeAsync(Images);


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
                // Also Products will be deleted due to the Cascade Delete Behavior
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
