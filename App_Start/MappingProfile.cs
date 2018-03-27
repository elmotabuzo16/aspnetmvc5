using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using AutoMapper;
using WebApplication2.Models;
using WebApplication2.Dtos;

namespace WebApplication2.App_Start
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            Mapper.CreateMap<CustomerDto, Customer>();
            Mapper.CreateMap<Customer, CustomerDto>();

            Mapper.CreateMap<MovieDto, Movie>();
            Mapper.CreateMap<Movie, MovieDto>();

            Mapper.CreateMap<MembershipType, MembershipTypeDto>();
            Mapper.CreateMap<Genre, GenreDto>();


        }
    }
}