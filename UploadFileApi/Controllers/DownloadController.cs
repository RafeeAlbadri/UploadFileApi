using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using UploadFileApi.Model;

namespace UploadFileApi.Controllers
{

    [Authorize]
    [Route("download")]
    [ApiController]
    public class DownloadController : ControllerBase
    {

        private IMapper _mapper;

        private readonly FileContext _context;

        public DownloadController(FileContext context)
        {
            _context = context;

        }


        [HttpGet("{id}")]
        public async Task<IActionResult>GetFiles( string id )
        {
            var accessToken = await HttpContext.GetTokenAsync("access_token");
            var token = new JwtSecurityToken(jwtEncodedString: accessToken);
            var userId = (token.Claims.First(c => c.Type == "unique_name").Value);

            var file =  _context.FileUploads.SingleOrDefault(s => s.UserId == userId && s.Id == id );

            var curentpath = file.Path;
            var stream = new FileStream(curentpath, FileMode.Open);
            return File(stream, file.FileContent);






        }



    }
}