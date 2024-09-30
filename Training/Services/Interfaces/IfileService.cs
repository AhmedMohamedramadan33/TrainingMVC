namespace Training.Services.Interfaces
{
    public interface IfileService
    {
        public Task<string> UploadFile(IFormFile formFile, string location);
        public bool DeletePhysicalPath(string path);
    }
}
