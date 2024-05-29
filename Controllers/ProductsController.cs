using AutoMapper;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MinhaApi.Context;
using MinhaApi.DTOs;
using MinhaApi.Models;
using MinhaApi.Repositories.Interfaces;

namespace MinhaApi.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public ProductsController(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        [HttpGet]
        public ActionResult<IEnumerable<ProductDTO>> Get()
        {
            return Ok(_mapper.Map<List<ProductDTO>>(_unitOfWork.ProductRepository.GetAll()));
        }

        [HttpGet("{id:int}", Name = "ObterProduto")]
        public ActionResult<ProductDTO> GetById(int id)
        {
            var product = _unitOfWork.ProductRepository.Get(p => p.ProductId == id);

            if (product is null) return NotFound($"Produto com id={id} não encontrado.");

            return Ok(_mapper.Map<ProductDTO>(product));
        }

        [HttpGet("byCategories")]
        public ActionResult<IEnumerable<ProductDTO>> GetByCategory(int id)
        {
            return Ok(_mapper.Map<List<ProductDTO>>(_unitOfWork.ProductRepository.GetProductsByCatetogy(id)));
        }

        [HttpPost]
        public ActionResult<ProductDTO> Post(ProductDTO productDto)
        {
            var product = _mapper.Map<Product>(productDto);

            _unitOfWork.ProductRepository.Create(product);
            _unitOfWork.Commit();

            var productDtoResponse = _mapper.Map<ProductDTO>(product);

            return new CreatedAtRouteResult("ObterProduto", new { id = productDtoResponse.ProductId }, productDtoResponse);
        }

        [HttpPut("{id:int}")]
        public ActionResult<ProductDTO> Put(int id, ProductDTO productDto)
        {
            var product = _mapper.Map<Product>(productDto);

            if (product.ProductId != id) BadRequest("Dados inválidos.");

            _unitOfWork.ProductRepository.Update(product);
            _unitOfWork.Commit();

            return Ok(_mapper.Map<ProductDTO>(product));
        }

        [HttpPatch("{id}/PartialUpdate")]
        public ActionResult<ProductDTOUpdateResponse> Patch(int id,
            JsonPatchDocument<ProductDTOUpdateRequest> patchProductDTO)
        {
            if (patchProductDTO is null || id <= 0) return BadRequest("Dados inválidos.");

            var product = _unitOfWork.ProductRepository.Get(p => p.ProductId == id);

            if (product is null) return NotFound($"Produto com id={id} não encontrado.");

            var productUpdateRequest = _mapper.Map<ProductDTOUpdateRequest>(product);
            patchProductDTO.ApplyTo(productUpdateRequest, ModelState);

            if (!ModelState.IsValid || TryValidateModel(productUpdateRequest))
                return BadRequest("Dados inválidos.");

            _mapper.Map(productUpdateRequest, product);
            _unitOfWork.ProductRepository.Update(product);
            _unitOfWork.Commit();

            return Ok(_mapper.Map<ProductDTOUpdateResponse>(product));
        }

        [HttpDelete("{id:int}")]
        public ActionResult<ProductDTO> Delete(int id)
        {
            var product = _unitOfWork.ProductRepository.Get(p => p.ProductId == id);

            if (product is null) return NotFound($"Produto com id={id} não encontrado.");

            _unitOfWork.ProductRepository.Delete(product);
            _unitOfWork.Commit();

            return Ok(_mapper.Map<ProductDTO>(product));
        }
    }
}
