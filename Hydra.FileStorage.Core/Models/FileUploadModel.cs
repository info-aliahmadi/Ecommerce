

using Hydra.FileStorage.Core.Domain;

namespace Hydra.FileStorage.Core.Models
{
    public class FileUploadModel
    {
        public FileUploadModel() { }
        public FileUploadModel(FileUpload? fileUpload)
        {
            if (fileUpload != null)
            {
                Id = fileUpload.Id;
                FileName = fileUpload.FileName;
                Thumbnail = fileUpload.Thumbnail;
                Directory = fileUpload.Directory;
            }
        }
        public int Id { get; set; }

        public string FileName { get; set; }
        public string Directory { get; set; }

        public string? Thumbnail { get; set; }

        public string Extension { get; set; }

        public long Size { get; set; }

        public string? Tags { get; set; }

        public string? Alt { get; set; }
        public DateTime UploadDate { get; set; }
        public string UserName { get; set; }
        public int UserId { get; set; }

        public string FullPath
        {
            get
            {
                return $"/{Directory}{FileName}";
            }
        }
        public string ThumbnailPath
        {
            get
            {
                return $"/{Directory}{Thumbnail}";
            }
        }
    }
}
