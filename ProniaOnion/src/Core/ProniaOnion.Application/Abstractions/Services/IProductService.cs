using Microsoft.AspNetCore.Mvc.RazorPages;
using ProniaOnion.Application.DTOs.Products;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProniaOnion.Application.Abstractions.Services
{
    public interface IProductService
    {
        Task<IEnumerable<ProductItemDto>>GetAllAsync(int page=1,int take=8);
        Task<GetProductDto> GetByIdAsync(int id);
    }
}
