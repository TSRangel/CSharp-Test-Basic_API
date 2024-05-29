using AutoMapper;
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
    public class CategoriesController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILogger<CategoriesController> _logger;
        private readonly IMapper _mapper;
        public CategoriesController(ILogger<CategoriesController> logger, 
            IUnitOfWork unitOfWork, IMapper mapper)
        {
            _logger = logger;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        [HttpGet]
        public ActionResult<IEnumerable<CategoryDTO>> Get()
        {
            return Ok(_mapper.Map<List<CategoryDTO>>(_unitOfWork.CategoryRepository.GetAll()));
        }

        [HttpGet("{id:int}", Name = "ObterCategoria")]
        public ActionResult<CategoryDTO> GetById(int id)
        {
            var category = _unitOfWork.CategoryRepository.Get(c => c.CategoryId == id);
            if (category is null) 
            {
                _logger.LogWarning($"Categoria com id={id} não encontrada");
                return NotFound($"Categoria com id={id} não encontrada");
            }

            return Ok(_mapper.Map<CategoryDTO>(category));
        }

        [HttpPost]
        public async Task<ActionResult<CategoryDTO>> Post(CategoryDTO categoryDto)
        {
            var category = _mapper.Map<Category>(categoryDto);

            if (category is null) 
            {
                _logger.LogWarning("Dados inválidos.");        
                return BadRequest("Dados inválidos.");
            }

            _unitOfWork.CategoryRepository.Create(category);
            _unitOfWork.Commit();

            var catagoryDtoResponse = _mapper.Map<CategoryDTO>(category);

            return new CreatedAtRouteResult("ObterCategoria", new { id = catagoryDtoResponse.CategoryId }, catagoryDtoResponse);
        }

        [HttpPut("{id:int}")]
        public ActionResult<CategoryDTO> Put(int id, CategoryDTO categoryDto)
        {
            var category = _mapper.Map<Category>(categoryDto);

            if (category.CategoryId != id) 
            {
                _logger.LogWarning("Dados inválidos.");
                return BadRequest("Dados inválidos."); 
            }

            _unitOfWork.CategoryRepository.Update(category);
            _unitOfWork.Commit();

            return Ok(_mapper.Map<CategoryDTO>(category));
        }

        [HttpDelete("{id:int}")]
        public ActionResult<CategoryDTO> Delete(int id)
        {
            var category = _unitOfWork.CategoryRepository.Get(c => c.CategoryId == id);
            if (category is null) 
            {
                _logger.LogWarning($"Categoria com id= {id} não encontrada.");
                return NotFound($"Categoria com id= {id} não encontrada."); 
            }

            _unitOfWork.CategoryRepository.Delete(category);
            _unitOfWork.Commit();

            return Ok(_mapper.Map<CategoryDTO>(category));
        }
    }
}
