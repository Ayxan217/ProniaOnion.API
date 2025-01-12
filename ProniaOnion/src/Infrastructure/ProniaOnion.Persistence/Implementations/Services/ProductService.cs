using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using ProniaOnion.Application.Abstractions.Repositories;
using ProniaOnion.Application.Abstractions.Services;
using ProniaOnion.Application.DTOs.Products;

namespace ProniaOnion.Persistence.Implementations.Services
{
    internal class ProductService : IProductService
    {
        private readonly IProductRepository _productRepository;
        private readonly IMapper _mapper;
        private readonly ICategoryRepository _categoryRepository;
        private readonly IColorRepository _colorRepository;

        public ProductService(IProductRepository productRepository
            ,ICategoryRepository categoryRepository
            ,IMapper mapper
            ,IColorRepository colorRepository)
        {
            _productRepository = productRepository;
            _mapper = mapper;
            _categoryRepository = categoryRepository;
            _colorRepository = colorRepository;
        }
        public async Task<IEnumerable<ProductItemDto>> GetAllAsync(int page = 1, int take = 8)
        {
            var product = _mapper.Map<IEnumerable<ProductItemDto>>(await _productRepository
                .GetAll(skip: (page-1)*take, take: take).ToListAsync());
            return product;
        }

        public async Task<GetProductDto> GetByIdAsync(int id)
        {
           var product =_mapper.Map<GetProductDto>(await _productRepository.GetByIdAsync(id, "Category", "ProductColors.Color"));
            if (product is null) throw new Exception("Not Exits");
            return product;
        }

        public async Task CreateAsync(CreateProductDto productDto)
        {
            if (!await _categoryRepository.AnyAsync(c => c.Id == productDto.CategoryId))
                throw new Exception("category does not exists");

            //if (!await _colorRepository.AnyAsync(c => !productDto.ColorIds.Contains(c.Id)));


        }
    }
}
