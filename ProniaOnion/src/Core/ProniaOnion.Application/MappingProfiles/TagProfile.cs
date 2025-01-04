using AutoMapper;
using ProniaOnion.Application.DTOs.Categories;
using ProniaOnion.Application.DTOs.Tags;
using ProniaOnion.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProniaOnion.Application.MappingProfiles
{
    internal class TagProfile : Profile
    {
        public TagProfile()
        {
            CreateMap<Tag, GetTagDto>().ReverseMap();
            CreateMap<CreateTagDto, Tag>();
            CreateMap<UpdateTagDto, Tag>().ForMember(c => c.Id, opt => opt.Ignore())
                .ForSourceMember(c => c.Name, opt => opt.DoNotValidate());
            CreateMap<Tag, TagItemDto>();
        }
    }
}
