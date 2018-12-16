using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UploadFileApi.Model;

namespace UploadFileApi
{
    interface IFileRepository
    {
        FileUpload GetFileUpload(int id);
    }
}
