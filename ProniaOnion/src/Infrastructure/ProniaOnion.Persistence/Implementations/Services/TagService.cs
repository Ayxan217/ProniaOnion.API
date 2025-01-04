using AutoMapper;
using Microsoft.EntityFrameworkCore;
using ProniaOnion.Application.Abstractions.Repositories;
using ProniaOnion.Application.Abstractions.Services;
using ProniaOnion.Application.DTOs.Categories;
using ProniaOnion.Application.DTOs.Tags;
using ProniaOnion.Domain.Entities;
using ProniaOnion.Persistence.Implementations.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProniaOnion.Persistence.Implementations.Services
{
    public class TagService : ITagService
    {
        private readonly ITagRepository _tagRepository;
        private readonly IMapper _mapper;

        public TagService(ITagRepository tagRepository, IMapper mapper)
        {
            _tagRepository = tagRepository;
            _mapper = mapper;
        }
        public async Task CreateAsync(CreateTagDto tagDto)
        {
            if (await _tagRepository.AnyAsync(c => c.Name == tagDto.Name)) throw new Exception("Alredy Exists");

            var tag = _mapper.Map<Tag>(tagDto);
            tag.CreatedAt = DateTime.Now;
            tag.ModifiedAt = DateTime.Now;
            await _tagRepository.AddAsync(tag);
            await _tagRepository.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            Tag tag = await _tagRepository.GetByIdAsync(id);
            if (tag is null) throw new Exception("Not Found");

            _tagRepository.Delete(tag);
            await _tagRepository.SaveChangesAsync();
        }

        public async Task<IEnumerable<TagItemDto>> GetAllAsync(int page, int take)
        {
            IEnumerable<Tag> tags = await _tagRepository
               .GetAll(skip: (page - 1) * take, take: take)
               .ToListAsync();

            return _mapper.Map<IEnumerable<TagItemDto>>(tags);
        }

        public async Task<GetTagDto> GetByIdAsync(int id)
        {
            var tag = await _tagRepository.GetByIdAsync(id);

            if (tag is null) return null;
           

            var tagDto = _mapper.Map<GetTagDto>(tag);



            return tagDto;
        }

        public async Task UpdateAsync(int id, UpdateTagDto tagDto)
        {
            var tag = await _tagRepository.GetByIdAsync(id);
            if (tag is null)
                throw new Exception("not found");


            bool exists = await _tagRepository
                .AnyAsync(c => c.Name == tagDto.Name && c.Id != id);

            if (exists)
            {
                throw new Exception(" already exists");
            }


            _mapper.Map(tagDto, tag);

            tag.ModifiedAt = DateTime.Now;

            _tagRepository.Update(tag);
            await _tagRepository.SaveChangesAsync();
        }
    }
}
