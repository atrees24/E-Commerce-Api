﻿using AutoMapper;
using AutoMapper.Execution;
using Domain.Models;
using Microsoft.Extensions.Configuration;
using Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.MappingProfiles
{
    public class PictureURLResolver(IConfiguration configuration) : IValueResolver< Product, ProductResultDTO, string>
    {
        public string Resolve(Product source, ProductResultDTO destination,string destmember , ResolutionContext context )
        {
            if(string.IsNullOrWhiteSpace(source.PictureUrl)) return string.Empty;

            return $"{configuration["BaseUrl"]}{source.PictureUrl}";
        }
    }
}
