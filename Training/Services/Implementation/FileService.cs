   using Training.Services.Interfaces;

namespace Training.Services.Implementation
{
    public class FileService : IfileService
    {
        private readonly IWebHostEnvironment _IWebHostEnvironment;
        public FileService(IWebHostEnvironment webHostEnvironment)
        {
            _IWebHostEnvironment = webHostEnvironment;
        }

        public bool DeletePhysicalPath(string path)
        {
            string DirectorytPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot" + path);
            if (File.Exists(DirectorytPath))
            {
                File.Delete(DirectorytPath);
                return true;
            }
            return false;
        }

        public async Task<string> UploadFile(IFormFile formFile, string location)
        {
            try
            {
                var RootPath = _IWebHostEnvironment.WebRootPath + location;
                var Extention = Path.GetExtension(formFile.FileName);
                var FileName = Guid.NewGuid().ToString() + Extention;
                if (!Directory.Exists(RootPath))
                {
                    Directory.CreateDirectory(RootPath);
                }
                using (FileStream filestream = File.Create(RootPath + FileName))
                {
                    await formFile.CopyToAsync(filestream);
                    filestream.Flush();
                    return $"{location}/{FileName}";
                }

            }
            catch(Exception ex)
            {
                return ex.Message+"=="+ex.InnerException;
            }
        }
    }
}
