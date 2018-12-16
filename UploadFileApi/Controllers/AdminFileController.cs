using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json.Linq;
using UploadFileApi.Model;

namespace UploadFileApi.Controllers
{
    //[Authorize(Roles = "admin")]
    [Route("api/admin")]
    [ApiController]
    public class AdminFileController : ControllerBase
    {

        private readonly FileContext _context;

        public AdminFileController(FileContext context)
        {
            _context = context;
        }
        [HttpGet]
        [Route("files")]
        public IEnumerable<FileUpload> ViewAllFiles()
        {
            
            return _context.FileUploads;
        }


        
        [HttpGet]
        public IActionResult Download(string id )
        {
            var fileDescription = _context.FileUploads.Single(t => t.Id == id);
            var curentpath = fileDescription.Path;
            var stream = new FileStream(curentpath, FileMode.Open);
            return File(stream, fileDescription.FileContent,fileDescription.FileName);
            
        }

    }
}
