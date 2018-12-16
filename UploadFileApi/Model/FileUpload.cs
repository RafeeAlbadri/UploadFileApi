using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace UploadFileApi.Model
{
    public class FileUpload
    {
        
        public string Id { get; set; }
        public string UserId { get; set; }
        public string FileName { get; set; }
        public DateTime UploadTime { get; set; }
        public string FileType { get; set; }
        public string FileContent { get; set; }
        public string Path { get; set; }
    }
}
