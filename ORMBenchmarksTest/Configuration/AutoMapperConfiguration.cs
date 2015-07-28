using AutoMapper;
using ORMBenchmarksTest.DTOs;
using ORMBenchmarksTest.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ORMBenchmarksTest.Configuration
{
    public static class AutoMapperConfiguration
    {
        public static void Configure()
        {
            Mapper.CreateMap<Player, PlayerDTO>();
            Mapper.CreateMap<Team, TeamDTO>();
            Mapper.CreateMap<Sport, SportDTO>();
        }
    }
}
