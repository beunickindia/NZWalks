using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NZWalks.API.Controllers.Data;
using NZWalks.API.Controllers.Models.Domain;
using NZWalks.API.Models.DTO;

namespace NZWalks.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RegionsController : ControllerBase
    {
        private readonly NZWalksDbContext dbContext;

        public RegionsController(NZWalksDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        //Get single region by id
        [HttpGet]
        [Route("{id:guid}")]

        public IActionResult GetById([FromRoute]Guid id)
        {
           // var region = dbContext.Regions.Find(id);
           //Get Region Domain Model From Database
            var regionDomain = dbContext.Regions.FirstOrDefault(s => s.Id == id);
            if (regionDomain == null)
            {
                return NotFound();
            }
            //Map/convert REgion Domain Model to Region DTO

            var regionDTO = new RegionDTO
            {
                Id=regionDomain.Id,
                Code=regionDomain.Code,
                Name=regionDomain.Name,
                RegionNameUrl=regionDomain.RegionNameUrl,
            };
            //return DTO back to Client
            return Ok(regionDTO);
        }


        //Get All regions 
        [HttpGet]
        public IActionResult GetAll()
        {
            //Get data From database - Domain models
            var regionsDomain=dbContext.Regions.ToList();

            //map Domain models to DTOs
            var regionDTO = new List<RegionDTO>();
            foreach (var regionDomain in regionsDomain)
            {
                regionDTO.Add(new RegionDTO
                {
                    Id = regionDomain.Id,
                    Name = regionDomain.Name,
                    Code = regionDomain.Code,
                    RegionNameUrl = regionDomain.RegionNameUrl,
                });
            }

            return Ok(regionDTO);
        }
        //public IActionResult GetAll()
        //{

        //var regions = new List<Region>
        //{
        //    new Region
        //    {
        //        Id=Guid.NewGuid(),
        //        Name="Aukland Region",
        //        RegionNameUrl="https://www.pexels.com/photo/majestic-alpine-castle-with-snow-capped-mountains-30157896/"

        //    },
        //    new Region
        //    {
        //        Id=Guid.NewGuid(),
        //          Name="Aukland Region",
        //        RegionNameUrl="https://www.pexels.com/photo/scenic-view-of-bay-of-kotor-in-montenegro-30129200/"
        //    }


        //};


        //    return Ok(regions);

        //}


        //POST to create new region
        //POST: http://localhost:portNumber/api/regions

        [HttpPost]
        public IActionResult Create([FromBody] AddRegionRequestDto addRegionRequestDto)
        { 
            //Map or Convert DTO to domain Model
            var regionDomainModel=new Region
            {
                Code=addRegionRequestDto.Code,
                Name=addRegionRequestDto.Name,
                RegionNameUrl=addRegionRequestDto.RegionNameUrl
            }

            //Use Domain Model to Create Region
            dbContext.Regions.Add(regionDomainModel);

        }


    }
}
