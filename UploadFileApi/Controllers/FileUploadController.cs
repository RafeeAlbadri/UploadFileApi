using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.IO;
using System.Linq;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using UploadFileApi.Model;
using System.IdentityModel.Tokens;
using AutoMapper;

namespace UploadFileApi.Controllers
{
    [Authorize]
    [Route("api/")]
    [ApiController]
    public class FileUploadController : ControllerBase
    {
        private IMapper _mapper;

        private readonly FileContext _context;

        public FileUploadController(FileContext context)
        {
            _context = context;

        }

       
        

        // POST api/values
        [HttpPost]
        [Route("upload-file")]
        public async Task <IActionResult> Post (IFormFile file)
        {
            if (file == null )
                return Content("files not selected");

            var filename = ContentDispositionHeaderValue.Parse(
                        file.ContentDisposition).FileName.ToString().Trim('"');
            var path = Path.Combine(
                        Directory.GetCurrentDirectory(), "wwwroot",
                        filename);
            var ext = Path.GetExtension(path).ToLowerInvariant();

            var accessToken = await HttpContext.GetTokenAsync("access_token");
            var token = new JwtSecurityToken(jwtEncodedString: accessToken);
            var userId =(token.Claims.First(c => c.Type == "unique_name").Value);

            using (var stream = new FileStream(path, FileMode.Create))
                {
                    await file.CopyToAsync(stream);
                }

            var files = new FileUpload
            {
                Id = Guid.NewGuid().ToString(),
                UserId = userId,
                FileName = filename,
                UploadTime = DateTime.UtcNow,
                FileType = ext,
                FileContent = file.ContentType,
                Path = path,
                

            };


            _context.FileUploads.Add(files);
            await _context.SaveChangesAsync();
            return Ok(files.Id);

        }

    }
}
