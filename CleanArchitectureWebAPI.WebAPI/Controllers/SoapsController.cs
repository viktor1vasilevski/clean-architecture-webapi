using CleanArchitectureWebAPI.Application.Interfaces;
using CleanArchitectureWebAPI.Application.ViewModels.Soap;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
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
    public class SoapsController : Controller
    {
        private readonly ISoapService _soapService;
        public SoapsController(ISoapService soapService)
        {
            _soapService = soapService;
        }

        [HttpGet] //URL/api/soaps  http metod Get
        [SwaggerResponse(HttpStatusCode.OK, typeof(SoapListViewModel), Description = "Successfully Returned List Of Soaps")]
        [SwaggerResponse(HttpStatusCode.NotFound, null, Description = "List Of Soaps Is Empty")]
        public IActionResult GetAll()
        {

            throw new Exception();
            var soapListViewModel = _soapService.GetSoaps();
            if (soapListViewModel != null)
            {
                return Ok(soapListViewModel);
            }
            else
            {
                return NotFound();
            }
            
        }


        [HttpGet("{id}")]  //URL/api/soaps/id    http metod Get
        [SwaggerResponse(HttpStatusCode.OK, typeof(SoapViewModel), Description = "Successfully Returned Soap Model")]
        [SwaggerResponse(HttpStatusCode.NotFound, null, Description = "There Is No Soap Model With That Id")]
        [SwaggerResponse(HttpStatusCode.BadRequest, null, Description = "The Id Is Not In The Correct Format")]
        public IActionResult GetById(Guid id)
        {
            var soap = _soapService.GetSoapById(id);
            if (soap != null)
            {
                return Ok(soap);
            }
            else
            {
                return NotFound();
            }
        }


        [HttpPost] //URL/api/soaps   http metod Post
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "Admin")]
        [SwaggerResponse(HttpStatusCode.Unauthorized, null, Description = "You Don't Have Authorization For This Request")]
        [SwaggerResponse(HttpStatusCode.OK, typeof(SoapViewModel), Description = "Ok")]
        [SwaggerResponse(HttpStatusCode.Created, typeof(SoapViewModel), Description = "Soap Model Created")]
        [SwaggerResponse(HttpStatusCode.NotFound, null, Description = "Soap Model Not Found")]
        public IActionResult AddOrEdit(SoapViewModel model)
        {
            if (model.Id == Guid.Empty)
            {
                _soapService.AddSoap(model);
            }
            else
            {
                _soapService.EditSoap(model);
            }
            return Ok(model);
        }

        [HttpDelete("{id}")] //URL/api/soaps/id   http metod Delete
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "Admin")]
        [SwaggerResponse(HttpStatusCode.OK, typeof(SoapViewModel), Description = "Successfully Deleted Soap Model")]
        [SwaggerResponse(HttpStatusCode.NotFound, typeof(Guid), Description = "Soap Model You Want To Delete Doesn't Exist")]
        [SwaggerResponse(HttpStatusCode.Unauthorized, null, Description = "You Don't Have Authorization For This Request")]
        public IActionResult Delete(Guid id)
        {
            var soap = _soapService.GetSoapById(id);
            _soapService.DeleteSoap(id);
            if (soap != null)
            {
                return Ok(soap);
            }
            else
            {
                return NotFound();
            }
        }

    }
}
