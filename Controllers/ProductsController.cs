using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Talabat.APIs.DTOs;
using Talabat.APIs.Errors;
using Talabat.APIs.Helpers;
using Talabat.Core.Entites;
using Talabat.Core.Repositories;
using Talabat.Core.Specifications;

namespace Talabat.APIs.Controllers
{

    public class ProductsController : APIBaseController
    {
        private readonly IGenericRepository<Product> _productRepo;
        private readonly IMapper _mapper;
        private readonly IGenericRepository<ProductType> _typeRepo;
        private readonly IGenericRepository<ProductBrand> _brandRepo;

        public ProductsController(IGenericRepository<Product> ProductRepo
                                , IMapper mapper,
                                 IGenericRepository<ProductType> TypeRepo,
                                 IGenericRepository<ProductBrand> BrandRepo)
        {
            _productRepo = ProductRepo;
            _mapper = mapper;
            _typeRepo = TypeRepo;
            _brandRepo = BrandRepo;
        }

        //Get All Products
        //BaseUrl/api/Products -> GET
        [HttpGet]
        public async Task<ActionResult<Pagination<ProductToReturnDto>>> GetProducts([FromQuery]ProductSpecParams Params)
        {
            var Spec = new ProductWithBrandAndTybeSpecifications(Params);
            var Products = await _productRepo.GetAllWithSpecAsync(Spec);
            var MappedProducts = _mapper.Map<IReadOnlyList<Product>, IReadOnlyList<ProductToReturnDto>>(Products);
            var CountSpec = new ProductWithFiltrationForCountAsync(Params);
            var Count =await _productRepo.GetCountWithSpecAsync(CountSpec);
            return Ok(new Pagination<ProductToReturnDto>(Params.PageIndex,Params.PageSize, MappedProducts,Count));
        }

        //Get Product by Id
        ////BaseUrl/api/Products/1 -> GET
        [HttpGet("{id}")]
        [ProducesResponseType(typeof(ProductToReturnDto), 200)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status404NotFound)]//404
        public async Task<ActionResult<ProductToReturnDto>> GetProduc(int id)
        {
            var Spec = new ProductWithBrandAndTybeSpecifications(id);
            var Product = await _productRepo.GetByIdWithSpecAsync(Spec);
            if (Product is null) return NotFound(new ApiResponse(404));
            var MappedProducts = _mapper.Map<Product, ProductToReturnDto>(Product);

            return Ok(MappedProducts);
        }

        //GET ALL TYPES
        //BaseUrl/api/Product/Types
        [HttpGet("Types")]
        public async Task<ActionResult<IReadOnlyList<ProductType>>> GetTypes()
        {
            var Types = await _typeRepo.GetAllAsync();
            return Ok(Types);
        }


        //GET ALL BRANDS
        [HttpGet("Brands")]
        //BaseUrl/api/Brands/Types
        public async Task<ActionResult<IReadOnlyList<ProductBrand>>> GetBrands()
        {
            var Brands =await _brandRepo.GetAllAsync();
            return Ok(Brands);
        }

    }
}
