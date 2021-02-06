using CleanArchitectureWebAPI.Application.Interfaces;
using CleanArchitectureWebAPI.Application.ViewModels.Balm;
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
    public class BalmsController : Controller
    {
        private readonly IBalmService _balmService;
        private IMemoryCache _memoryCache;
        private string _allBalmsKey = "All_Balms_Cache";
        public BalmsController(IBalmService balmService, IMemoryCache memoryCache)
        {
            _balmService = balmService;
            _memoryCache = memoryCache;
        }

        
        [HttpGet] //URL/api/balms  http metod Get
        [SwaggerResponse(HttpStatusCode.OK, typeof(BalmListViewModel), Description = "Successfully Returned List Of Balms")]
        [SwaggerResponse(HttpStatusCode.NotFound, null, Description = "List Of Balms Is Empty")]
        public IActionResult GetAll()
        {
            BalmListViewModel balmListViewModel;

            // Returns an entry from the cache
            balmListViewModel = (BalmListViewModel)_memoryCache.Get(_allBalmsKey);

            // If there is no entry in the cache for that key, go to database
            if (balmListViewModel == null)
            {
                balmListViewModel = _balmService.GetBalms();
                // Setting cache entry for that key. 
                _memoryCache.Set(_allBalmsKey, balmListViewModel, new MemoryCacheEntryOptions().
                    SetSlidingExpiration(TimeSpan.FromMinutes(15)).
                    SetAbsoluteExpiration(TimeSpan.FromHours(1)));
                // SetSlidingExpiration Sets how long the cache entry can be inactive 
                // SetAbsoluteExpiration Sets an absolute expiration date for the cache entry

                // THIS IS JUST TO DEMONSTRATE HOW CACHE WORKS. YOU CAN ALLWAYS CHANGE THE TIMESPAN
            }

            if (balmListViewModel != null)
            {
                return Ok(balmListViewModel);
            }
            else
            {
                return NotFound();
            }              
        }

        
        [HttpGet("{id}")] //URL/api/balms/id  http metod Get
        [SwaggerResponse(HttpStatusCode.OK, typeof(BalmViewModel), Description = "Successfully Returned Balm Model")]
        [SwaggerResponse(HttpStatusCode.NotFound, null, Description = "There Is No Balm Model With That Id")]
        [SwaggerResponse(HttpStatusCode.BadRequest, null, Description = "The Id Is Not In The Correct Format")]
        [ResponseCache(Duration = 300, VaryByQueryKeys = new string[] { "id" })] // This is the Response Cache
        public IActionResult GetById(Guid id)
        {
            var balm = _balmService.GetBalmById(id);
            if (balm != null)
            {
                return Ok(balm);
            }
            else
            {
                return NotFound(id);
            }
                
        }


        
        [HttpPost] //URL/api/balms  http metod Post
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "Admin")]
        [SwaggerResponse(HttpStatusCode.Unauthorized, null, Description = "You Don't Have Authorization For This Request")]
        [SwaggerResponse(HttpStatusCode.OK, typeof(BalmViewModel), Description = "Ok")]
        [SwaggerResponse(HttpStatusCode.Created, typeof(BalmViewModel), Description = "Balm Model Created")]
        [SwaggerResponse(HttpStatusCode.NotFound, null, Description = "Balm Model Not Found")]
        public IActionResult AddOrEdit(BalmViewModel model)
        {
            if (model.Id == Guid.Empty)
            {
                _balmService.AddBalm(model);
            }
            else
            {
                _balmService.EditBalm(model);
            }

            _memoryCache.Remove(_allBalmsKey);

            return Ok(model);
        }


        
        [HttpDelete("{id}")] //URL/api/balms/id   http metod Delete
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "Admin")]
        [SwaggerResponse(HttpStatusCode.OK, typeof(BalmViewModel), Description = "Successfully Deleted Balm Model")]
        [SwaggerResponse(HttpStatusCode.NotFound, typeof(Guid), Description = "Balm Model You Want To Delete Doesn't Exist")]
        [SwaggerResponse(HttpStatusCode.Unauthorized, null, Description = "You Don't Have Authorization For This Request")]
        public IActionResult Delete(Guid id)
        {
            var balm = _balmService.GetBalmById(id);
            if (balm != null)
            {
                _balmService.DeleteBalm(id);
                _memoryCache.Remove(_allBalmsKey);
                return Ok(balm);
            }
            else
            {
                return NotFound(id);
            }
                
        }
    }
}
