using CleanArchitectureWebAPI.Application.Interfaces;
using CleanArchitectureWebAPI.Application.ViewModels.Oil;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using NSwag.Annotations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace CleanArchitectureWebAPI.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OilsController : Controller
    {
        private readonly IOilService _oilService;
        private IMemoryCache _memoryCache;
        private string _allOilsKey = "All_Oils_Cache";
        public OilsController(IOilService oilService, IMemoryCache memoryCache)
        {
            _oilService = oilService;
            _memoryCache = memoryCache;
        }


        
        [HttpGet] //URL/api/oils  http metod Get
        [SwaggerResponse(HttpStatusCode.OK, typeof(OilListViewModel), Description = "Successfully Returned List Of Oils")]
        [SwaggerResponse(HttpStatusCode.NotFound, null, Description = "List Of Oils Is Empty")]
        public IActionResult GetAll()
        {
            OilListViewModel oilListViewModel;
            oilListViewModel = (OilListViewModel)_memoryCache.Get(_allOilsKey);

            if (oilListViewModel == null)
            {
                oilListViewModel = _oilService.GetOils();

                _memoryCache.Set(_allOilsKey, oilListViewModel, new MemoryCacheEntryOptions()
                    .SetSlidingExpiration(TimeSpan.FromMinutes(15))
                    .SetAbsoluteExpiration(TimeSpan.FromHours(1)));
            }
            if (oilListViewModel != null)
            {
                return Ok(oilListViewModel);
            }         
            else
            {
                return NotFound();
            }    
        }

        
        [HttpGet("{id}")] //URL/api/oils/id   http metod Get
        [SwaggerResponse(HttpStatusCode.OK, typeof(OilViewModel), Description = "Successfully Returned Oil Model")]
        [SwaggerResponse(HttpStatusCode.NotFound, null, Description = "There Is No Oil Model With That Id")]
        [SwaggerResponse(HttpStatusCode.BadRequest, null, Description = "The Id Is Not In The Correct Format")]
        public IActionResult GetById(Guid id)
        {
            var oil = _oilService.GetOilById(id);
            if (oil != null)
            {
                return Ok(oil);
            }
            else
            {
                return NotFound(id);
            }               
        }

        
        [HttpPost] //URL/api/oils   http metod Post
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "Admin")]
        [SwaggerResponse(HttpStatusCode.Unauthorized, null, Description = "You Don't Have Authorization For This Request")]
        [SwaggerResponse(HttpStatusCode.OK, typeof(OilViewModel), Description = "Ok")]
        [SwaggerResponse(HttpStatusCode.Created, typeof(OilViewModel), Description = "Oil Model Created")]
        [SwaggerResponse(HttpStatusCode.NotFound, null, Description = "Oil Model Not Found")]
        public IActionResult AddOrEdit(OilViewModel model)
        {
            if (model.Id == Guid.Empty)
            {
                _oilService.AddOil(model);
            }
            else
            {
                _oilService.EditOil(model);
            }
            _memoryCache.Remove(_allOilsKey);
            return Ok(model);
        }

        
        [HttpDelete("{id}")] //URL/api/oils/id  http metod Delete
        [ResponseCache(Duration = 300, VaryByQueryKeys = new string[] { "id" })]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "Admin")]
        [SwaggerResponse(HttpStatusCode.OK, typeof(OilViewModel), Description = "Successfully Deleted Oil Model")]
        [SwaggerResponse(HttpStatusCode.NotFound, typeof(Guid), Description = "Oil Model You Want To Delete Doesn't Exist")]
        [SwaggerResponse(HttpStatusCode.Unauthorized, null, Description = "You Don't Have Authorization For This Request")]
        public IActionResult Delete(Guid id)
        {
            var oil = _oilService.GetOilById(id);
            _oilService.DeleteOil(id);
            if (oil != null)
            {
                _memoryCache.Remove(_allOilsKey);
                return Ok(oil);
            }
            else
            {
                return NotFound(id);
            }               
        }
    }
}
