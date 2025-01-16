﻿
using Hydra.Infrastructure;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Http;
using Hydra.FileStorage.Core.Interfaces;
using Hydra.FileStorage.Core.Domain;
using Hydra.FileStorage.Core.Models;
using Hydra.FileStorage.Core.Settings;
using Hydra.Infrastructure.GeneralModels;
using Hydra.Infrastructure.Setting.Interface;
using Hydra.Infrastructure.Data.Interface;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Processing;

namespace Hydra.FileStorage.Api.Services
{
    public class FileStorageService : IFileStorageService
    {
        private readonly IValidationService _validationService;
        private readonly IUploadFileSetting _fileStorageSetting;
        private readonly ICommandRepository _commandRepository;
        private readonly IQueryRepository _queryRepository;
        string uploadsPaths = HydraHelper.GetUploadsDirectory();

        public FileStorageService(
            IUploadFileSetting fileStorageSetting,
            IValidationService validationService,
            ICommandRepository commandRepository,
            IQueryRepository queryRepository

            )
        {
            _fileStorageSetting = fileStorageSetting;
            _validationService = validationService;
            _commandRepository = commandRepository;
            _queryRepository = queryRepository;
        }


        /// <summary>
        /// </summary>
        /// <param name="objectId"></param>
        /// <returns></returns>
        public bool IsExist(string fileName)
        {
            var isExist = _queryRepository.Table<FileUpload>().Any(x => x.FileName == fileName);

            return isExist;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="objectId"></param>
        /// <returns></returns>
        public async Task<Result<List<FileUploadModel>>> GetFilesList()
        {
            var result = new Result<List<FileUploadModel>>();
            var List = _queryRepository.Table<FileUpload>().Include(x => x.User).ToList().Select(x => new FileUploadModel()
            {
                Id = x.Id,
                FileName = x.FileName,
                Directory = x.Directory,
                Thumbnail = x.Thumbnail,
                Extension = x.Extension,
                Size = x.Size,
                Alt = x.Alt,
                Tags = x.Tags,
                UploadDate = x.UploadDate,
                UserName = x.User.UserName
            }).ToList();

            result.Data = List;
            return result;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="directoryname"></param>
        /// <returns></returns>
        public async Task<Result<List<FileUploadModel>>> GetFilesListByDirectory(string directoryname)
        {
            var result = new Result<List<FileUploadModel>>();
            var List = _queryRepository.Table<FileUpload>().Include(x => x.User).Where(x => x.Directory == directoryname).ToList().Select(x => new FileUploadModel()
            {
                Id = x.Id,
                FileName = x.FileName,
                Directory = x.Directory,
                Thumbnail = x.Thumbnail,
                Extension = x.Extension,
                Size = x.Size,
                Alt = x.Alt,
                Tags = x.Tags,
                UploadDate = x.UploadDate,
                UserName = x.User.UserName
            }).ToList();

            result.Data = List;
            return result;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="objectId"></param>
        /// <returns></returns>
        public async Task<Result<List<DirectoryModel>>> GetDirectoryList()
        {
            var result = new Result<List<DirectoryModel>>();
            var directories = GetDirectories();
            var groupedFiles = await (from f in _queryRepository.Table<FileUpload>()
                                      group f by f.Directory into grouped
                                      select new DirectoryModel()
                                      {
                                          DirectoryName = grouped.Key,
                                          DirectorySize = grouped.Sum(x => x.Size),
                                          FilesCount = grouped.Count()
                                      }).ToListAsync();
            var notExistedDirectory = directories.Where(c => !groupedFiles.Select(x => x.DirectoryName).Contains(c));
            foreach (var directory in notExistedDirectory)
            {
                groupedFiles.Add(new DirectoryModel()
                {
                    DirectoryName = directory,
                    DirectorySize = 0,
                    FilesCount = 0
                });
            }

            result.Data = groupedFiles;
            return result;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public async Task<GalleryResultModel> GetGalleyFiles(HttpContext context)
        {
            var result = new GalleryResultModel();

            var Files = await _queryRepository.Table<FileUpload>().Where(x => _fileStorageSetting.ImagesExtensions.Contains(x.Extension))
                .Select(x => new ImageModel()
                {
                    Name = x.FileName,
                    Src = HydraHelper.GetCurrentDomain(context) + x.Directory + x.FileName,
                    Tag = x.Tags ?? "",
                    Alt = x.Alt ?? ""
                }).ToListAsync();
            result.Result = Files;
            result.statusCode = 200;
            return result;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="objectId"></param>
        /// <returns></returns>
        public async Task<Result<FileUploadModel>> GetFileInfoById(int fileId)
        {
            var result = new Result<FileUploadModel>();

            var fileUpload = await _queryRepository.Table<FileUpload>().FirstOrDefaultAsync(x => x.Id == fileId);

            if (fileUpload is null)
            {
                result.Status = ResultStatusEnum.NotFound;
                result.Message = "File Not Found";
                return result;
            }

            var fileUploadModel = new FileUploadModel()
            {
                Id = fileUpload.Id,
                FileName = fileUpload.FileName,
                Directory = fileUpload.Directory,
                Thumbnail = fileUpload.Thumbnail,
                Extension = fileUpload.Extension,
                Size = fileUpload.Size,
                Alt = fileUpload.Alt,
                Tags = fileUpload.Tags,
                UploadDate = fileUpload.UploadDate
            };
            result.Data = fileUploadModel;

            return result;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="objectId"></param>
        /// <returns></returns>
        public async Task<Result<List<FileUploadModel>>> GetFilesInfoByIds(int[] fileIds)
        {
            var result = new Result<List<FileUploadModel>>();

            var fileUploads = await _queryRepository.Table<FileUpload>().Where(x => fileIds.Contains(x.Id)).Select(fileUpload => new FileUploadModel()
            {
                Id = fileUpload.Id,
                FileName = fileUpload.FileName,
                Directory = fileUpload.Directory,
                Thumbnail = fileUpload.Thumbnail,
                Extension = fileUpload.Extension,
                Size = fileUpload.Size,
                Alt = fileUpload.Alt,
                Tags = fileUpload.Tags,
                UploadDate = fileUpload.UploadDate
            }).ToListAsync();


            result.Data = fileUploads;

            return result;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="objectId"></param>
        /// <returns></returns>
        public async Task<Result<FileUploadModel>> GetFileInfoByName(string fileName)
        {
            var result = new Result<FileUploadModel>();
            var fileUpload = await _queryRepository.Table<FileUpload>().FirstOrDefaultAsync(x => x.FileName == fileName);

            if (fileUpload is null)
            {
                result.Status = ResultStatusEnum.NotFound;
                result.Message = "File Not Found";
                return result;
            }

            var fileUploadModel = new FileUploadModel()
            {
                Id = fileUpload.Id,
                FileName = fileUpload.FileName,
                Directory = fileUpload.Directory,
                Thumbnail = fileUpload.Thumbnail,
                Extension = fileUpload.Extension,
                Size = fileUpload.Size,
                Alt = fileUpload.Alt,
                Tags = fileUpload.Tags,
                UploadDate = fileUpload.UploadDate
            };
            result.Data = fileUploadModel;
            return result;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="fileName"></param>
        /// <param name="bytes"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public async Task<Result<FileUploadModel>> UploadFromBytesAsync(int userId, FileUploadModel uploadModel, string uploadAction, byte[] bytes, CancellationToken cancellationToken = default)
        {
            var result = new Result<FileUploadModel>();
            try
            {
                // Don't trust any file name, file extension, and file data from the request unless you trust them completely
                // Otherwise, it is very likely to cause problems such as virus uploading, disk filling, etc
                // In short, it is necessary to restrict and verify the upload
                // Here, we just use the temporary folder and a random file name
                var firstBytes = bytes.Take(64).ToArray();
                var validateResult =
                    _validationService.ValidateFile(firstBytes, uploadModel.FileName, bytes.Length, FileSizeEnum.Small);
                if (validateResult != ValidationFileEnum.Ok)
                {
                    result.Status = ResultStatusEnum.InvalidValidation;
                    result.Message = _validationService.GetValidationMessage(validateResult);
                    return result;
                }
                var isExisted = IsExist(uploadModel.FileName);
                if (isExisted && uploadAction != "Rename" && uploadAction != "Replace")
                {
                    result.Status = ResultStatusEnum.ItsDuplicate;
                    result.Message = "The file Is already Existed";
                    return result;
                }
                var extension = Path.GetExtension(uploadModel.FileName).ToLowerInvariant();
                var directory = GetDirectory(extension);
                var fileNameWithPath = uploadsPaths + directory;

                if (isExisted && uploadAction == "Rename")
                {
                    uploadModel.FileName = Rename(uploadModel.FileName);
                }

                fileNameWithPath += uploadModel.FileName;


                var fileInfo = new FileInfo(fileNameWithPath);

                using (var fs = new FileStream(fileNameWithPath, FileMode.Create, FileAccess.Write))
                {
                    await fs.WriteAsync(bytes, 0, bytes.Length, cancellationToken);
                }

                var registerDate = DateTime.UtcNow;
                var thumbnail = GenerateThumbnail(fileInfo);

                if (isExisted && uploadAction == "Replace")
                {
                    var existedFileUpload = _queryRepository.Table<FileUpload>().First(x => x.FileName == uploadModel.FileName);

                    existedFileUpload.Size = bytes.Length;
                    existedFileUpload.Thumbnail = thumbnail;
                    existedFileUpload.Alt = uploadModel.Alt;
                    existedFileUpload.Tags = uploadModel.Tags;
                    existedFileUpload.UserId = userId;
                    existedFileUpload.UploadDate = registerDate;

                    _commandRepository.UpdateAsync(existedFileUpload);
                    await _commandRepository.SaveChangesAsync();

                    uploadModel.Id = existedFileUpload.Id;
                    uploadModel.FileName = uploadModel.FileName;
                    uploadModel.Directory = directory;
                    uploadModel.Size = existedFileUpload.Size;
                    uploadModel.Thumbnail = existedFileUpload.Thumbnail;
                    uploadModel.Extension = existedFileUpload.Extension;
                    uploadModel.UploadDate = registerDate;
                    uploadModel.UserId = userId;

                    result.Data = uploadModel;

                    return result;
                }
                var fileUpload = new FileUpload()
                {
                    FileName = uploadModel.FileName,
                    Directory = directory,
                    Size = bytes.Length,
                    Extension = fileInfo.Extension,
                    Thumbnail = thumbnail,
                    Alt = uploadModel.Alt,
                    Tags = uploadModel.Tags,
                    UploadDate = registerDate,
                    UserId = userId
                };
                await _commandRepository.InsertAsync(fileUpload);
                await _commandRepository.SaveChangesAsync();

                uploadModel.Id = fileUpload.Id;
                uploadModel.FileName = uploadModel.FileName;
                uploadModel.Directory = directory;
                uploadModel.Size = fileUpload.Size;
                uploadModel.Thumbnail = fileUpload.Thumbnail;
                uploadModel.Extension = fileUpload.Extension;
                uploadModel.UserId = userId;


                result.Data = uploadModel;

                return result;


            }
            catch (Exception e)
            {
                result.Status = ResultStatusEnum.ExceptionThrowed;
                result.Message = e.Message;
                return result;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="fileName"></param>
        /// <param name="contentType"></param>
        /// <param name="stream"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public async Task<Result<FileUploadModel>> Upload(int userId, IFormFile fileForm, string uploadAction, CancellationToken cancellationToken = default)
        {
            var result = new Result<FileUploadModel>();
            try
            {

                var stream = fileForm.OpenReadStream();
                var validBuffer = new byte[64];
                stream.Read(validBuffer, 0, validBuffer.Length);
                stream.Position = 0;
                var firstBytes = validBuffer.Take(64).ToArray();
                var fileName = fileForm.FileName;

                var validateResult =
                    _validationService.ValidateFile(firstBytes, fileName, fileForm.Length, FileSizeEnum.Small);

                if (validateResult != ValidationFileEnum.Ok)
                {
                    result.Status = _validationService.GetValidationStatus(validateResult);

                    result.Message = _validationService.GetValidationMessage(validateResult);
                    return result;
                }
                var isExisted = IsExist(fileName);

                if (isExisted && uploadAction != "Rename" && uploadAction != "Replace")
                {
                    result.Status = ResultStatusEnum.ItsDuplicate;
                    result.Message = "The file Is already Existed";
                    return result;
                }
                var extension = Path.GetExtension(fileName).ToLowerInvariant();
                var directory = GetDirectory(extension);
                var fileNameWithPath = uploadsPaths + directory;

                if (isExisted && uploadAction == "Rename")
                {
                    fileName = Rename(fileName);
                }
                fileNameWithPath += fileName;

                var fileInfo = new FileInfo(fileNameWithPath);

                using (var fileStream = File.Create(fileNameWithPath))
                {
                    await stream.CopyToAsync(fileStream);
                }

                var registerDate = DateTime.UtcNow;
                var thumbnail = GenerateThumbnail(fileInfo);
                var uploadModel = new FileUploadModel();
                if (isExisted && uploadAction == "Replace")
                {
                    var existedFileUpload = _queryRepository.Table<FileUpload>().First(x => x.FileName == fileName);

                    existedFileUpload.Size = fileForm.Length;
                    existedFileUpload.Thumbnail = thumbnail;
                    existedFileUpload.UploadDate = registerDate;
                    existedFileUpload.UserId = userId;

                    _commandRepository.UpdateAsync(existedFileUpload);
                    await _commandRepository.SaveChangesAsync();

                    uploadModel.Id = existedFileUpload.Id;
                    uploadModel.Size = existedFileUpload.Size;
                    uploadModel.Thumbnail = existedFileUpload.Thumbnail;
                    uploadModel.Extension = existedFileUpload.Extension;
                    uploadModel.Directory = directory;
                    uploadModel.UserId = userId;

                    result.Data = uploadModel;

                    return result;
                }

                var fileUpload = new FileUpload()
                {
                    FileName = fileName,
                    Directory = directory,
                    Size = fileForm.Length,
                    Extension = fileInfo.Extension,
                    Thumbnail = thumbnail,
                    UploadDate = registerDate,
                    UserId = userId
                };
                await _commandRepository.InsertAsync(fileUpload);

                await _commandRepository.SaveChangesAsync();

                uploadModel.Id = fileUpload.Id;
                uploadModel.FileName = fileUpload.FileName;
                uploadModel.UserId = userId;
                uploadModel.Extension = fileUpload.Extension;
                uploadModel.Thumbnail = fileUpload.Thumbnail;
                uploadModel.Directory = directory;
                uploadModel.Size = fileUpload.Size;

                result.Data = uploadModel;

                return result;
            }
            catch (Exception e)
            {
                result.Status = ResultStatusEnum.ExceptionThrowed;
                result.Message = e.Message;
                return result;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="fileName"></param>
        /// <param name="contentType"></param>
        /// <param name="stream"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public async Task<Result<FileUploadModel>> UploadSmallFileStreamAsync(int userId, string? fileName, string uploadAction, string? contentType, Stream stream, CancellationToken cancellationToken = default)
        {

            var result = await UploadFromStreamAsync(userId, fileName, uploadAction, FileSizeEnum.Small, stream, contentType,
                cancellationToken);

            return result;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="fileName"></param>
        /// <param name="contentType"></param>
        /// <param name="stream"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public async Task<Result<FileUploadModel>> UploadLargeFileStreamAsync(int userId, string? fileName, string uploadAction, string? contentType, Stream stream, CancellationToken cancellationToken = default)
        {

            var result = await UploadFromStreamAsync(userId, fileName, uploadAction, FileSizeEnum.Large, stream, contentType,
                cancellationToken);

            return result;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="fileName"></param>
        /// <param name="fileSize"></param>
        /// <param name="source"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public async Task<Result<FileUploadModel>> UploadFromStreamAsync(int userId, string? fileName, string uploadAction, FileSizeEnum fileSize, Stream stream, string contentType, CancellationToken cancellationToken = default)
        {
            var result = new Result<FileUploadModel>();
            try
            {


                // for email files
                stream.Position = 0;


                var validateWhiteList = _validationService.ValidateFileWhiteList(fileName);
                if (validateWhiteList != ValidationFileEnum.Ok)
                {
                    result.Status = ResultStatusEnum.IsNotAllowed;
                    result.Message = _validationService.GetValidationMessage(validateWhiteList);
                    return result;
                }
                if (fileSize == FileSizeEnum.Small)
                {
                    var validateFileLength = _validationService.ValidateFileLength(stream.Length, fileSize);
                    if (validateFileLength != ValidationFileEnum.Ok)
                    {
                        result.Status = validateFileLength == ValidationFileEnum.FileIsTooLarge ? ResultStatusEnum.FileIsTooLarge : ResultStatusEnum.FileIsTooSmall;
                        result.Message = _validationService.GetValidationMessage(validateFileLength);
                        return result;
                    }
                }

                var isExisted = IsExist(fileName);
                if (isExisted && uploadAction != "Rename" && uploadAction != "Replace")
                {
                    result.Status = ResultStatusEnum.ItsDuplicate;
                    result.Message = "The file Is already Existed";
                    return result;
                }
                var extension = Path.GetExtension(fileName).ToLowerInvariant();
                var directory = GetDirectory(extension);
                var fileNameWithPath = uploadsPaths + directory;

                if (isExisted && uploadAction == "Rename")
                {
                    fileName = Rename(fileName);
                }
                fileNameWithPath += fileName;

                var fileInfo = new FileInfo(fileNameWithPath);


                byte[] buffer = new byte[8 * 1024];
                int len;
                int totalLength = 0;
                using (var streamFile = new FileStream(fileNameWithPath, FileMode.Create, FileAccess.ReadWrite, FileShare.None, 1024))
                {
                    // first time we check the signature with operation the turn to while
                    len = await stream.ReadAsync(buffer, 0, buffer.Length);
                    totalLength = totalLength + len;
                    var validateSignature = _validationService.ValidateFileSignature(buffer.Take(60).ToArray(), fileInfo.Extension);
                    if (validateSignature != ValidationFileEnum.Ok)
                    {
                        result.Status = ResultStatusEnum.InvalidValidation;
                        result.Message = _validationService.GetValidationMessage(validateSignature);
                        return result;
                    }

                    await streamFile.WriteAsync(buffer, 0, len);

                    while ((len = await stream.ReadAsync(buffer, 0, buffer.Length)) > 0)
                    {
                        totalLength = totalLength + len;
                        await streamFile.WriteAsync(buffer, 0, len);
                    }

                }
                stream.Close();

                if (totalLength > _fileStorageSetting.MaxSizeLimitLargeFile && fileSize == FileSizeEnum.Large)
                {
                    File.Delete(fileNameWithPath);
                    result.Status = ResultStatusEnum.FileIsTooLarge;
                    result.Message = _validationService.GetValidationMessage(ValidationFileEnum.FileIsTooLarge);
                    return result;
                }
                var registerDate = DateTime.UtcNow;
                var thumbnail = GenerateThumbnail(fileInfo);
                var uploadModel = new FileUploadModel();

                if (isExisted && uploadAction == "Replace")
                {
                    var existedFileUpload = _queryRepository.Table<FileUpload>().First(x => x.FileName == fileName);

                    existedFileUpload.Size = totalLength;
                    existedFileUpload.Thumbnail = thumbnail;
                    existedFileUpload.UploadDate = registerDate;
                    existedFileUpload.UserId = userId;

                    _commandRepository.UpdateAsync(existedFileUpload);
                    await _commandRepository.SaveChangesAsync();

                    uploadModel.Id = existedFileUpload.Id;
                    uploadModel.Directory = directory;
                    uploadModel.Size = existedFileUpload.Size;
                    uploadModel.Thumbnail = existedFileUpload.Thumbnail;
                    uploadModel.Extension = existedFileUpload.Extension;
                    uploadModel.UploadDate = registerDate;
                    uploadModel.UserId = userId;

                    result.Data = uploadModel;

                    return result;
                }
                var fileUpload = new FileUpload()
                {
                    FileName = fileName,
                    Directory = directory,
                    Size = totalLength,
                    Extension = fileInfo.Extension,
                    Thumbnail = thumbnail,
                    UploadDate = registerDate,
                    UserId = userId,
                };
                await _commandRepository.InsertAsync(fileUpload);
                await _commandRepository.SaveChangesAsync();

                uploadModel.Id = fileUpload.Id;
                uploadModel.FileName = fileUpload.FileName;
                uploadModel.Directory = directory;
                uploadModel.Thumbnail = fileUpload.Thumbnail;
                uploadModel.Extension = fileUpload.Extension;
                uploadModel.Size = fileUpload.Size;
                uploadModel.UploadDate = registerDate;
                uploadModel.UserId = userId;

                result.Data = uploadModel;
                return result;
            }
            catch (Exception e)
            {
                result.Status = ResultStatusEnum.ExceptionThrowed;
                result.Message = e.Message;
                return result;
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="base64File"></param>
        /// <param name="fileName"></param>
        /// <returns></returns>
        public async Task<Result<FileUploadModel>> UploadBase64File(int userId, Base64FileUploadModel base64File, string uploadAction)
        {
            var result = new Result<FileUploadModel>();
            try
            {

                var validateWhiteList = _validationService.ValidateFileWhiteList(base64File.FileName);
                if (validateWhiteList != ValidationFileEnum.Ok)
                {
                    result.Status = ResultStatusEnum.IsNotAllowed;
                    result.Message = _validationService.GetValidationMessage(validateWhiteList);
                    return result;
                }
                var validateFileLength = _validationService.ValidateFileLength(base64File.FileLength, FileSizeEnum.Small);
                if (validateFileLength != ValidationFileEnum.Ok)
                {
                    result.Status = validateFileLength == ValidationFileEnum.FileIsTooLarge ? ResultStatusEnum.FileIsTooLarge : ResultStatusEnum.FileIsTooSmall;
                    result.Message = _validationService.GetValidationMessage(validateFileLength);
                    return result;
                }
                var isExisted = IsExist(base64File.FileName);
                if (isExisted && uploadAction != "Rename" && uploadAction != "Replace")
                {
                    result.Status = ResultStatusEnum.ItsDuplicate;
                    result.Message = "The file Is already Existed";
                    return result;
                }

                var extension = Path.GetExtension(base64File.FileName).ToLowerInvariant();
                var directory = GetDirectory(extension);
                var fileNameWithPath = uploadsPaths + directory;

                if (isExisted && uploadAction == "Rename")
                {
                    base64File.FileName = Rename(base64File.FileName);
                }

                fileNameWithPath += base64File.FileName;

                var fileInfo = new FileInfo(fileNameWithPath);
                if (!string.IsNullOrEmpty(base64File.Base64File))
                {
                    var fileBytes = HydraHelper.Base64FileToBytes(base64File.Base64File);
                    await File.WriteAllBytesAsync(string.Format(fileNameWithPath, base64File.FileName), fileBytes.FileBytes);
                }
                var registerDate = DateTime.UtcNow;
                var thumbnail = GenerateThumbnail(fileInfo);
                var uploadModel = new FileUploadModel();

                if (isExisted && uploadAction == "Replace")
                {
                    var existedFileUpload = _queryRepository.Table<FileUpload>().First(x => x.FileName == base64File.FileName);

                    existedFileUpload.Size = base64File.FileLength;
                    existedFileUpload.Thumbnail = thumbnail;
                    existedFileUpload.UploadDate = registerDate;
                    existedFileUpload.UserId = userId;

                    _commandRepository.UpdateAsync(existedFileUpload);
                    await _commandRepository.SaveChangesAsync();

                    uploadModel.Id = existedFileUpload.Id;
                    uploadModel.Directory = directory;
                    uploadModel.Size = existedFileUpload.Size;
                    uploadModel.Thumbnail = existedFileUpload.Thumbnail;
                    uploadModel.Extension = existedFileUpload.Extension;
                    uploadModel.UploadDate = registerDate;
                    uploadModel.UserId = userId;

                    result.Data = uploadModel;

                    return result;
                }
                var fileUpload = new FileUpload()
                {
                    FileName = base64File.FileName,
                    Directory = directory,
                    Size = base64File.FileLength,
                    Extension = fileInfo.Extension,
                    Thumbnail = GenerateThumbnail(fileInfo),
                    UploadDate = registerDate,
                    UserId = userId,

                };
                await _commandRepository.InsertAsync(fileUpload);
                await _commandRepository.SaveChangesAsync();

                uploadModel.Id = fileUpload.Id;
                uploadModel.FileName = fileUpload.FileName;
                uploadModel.Directory = directory;
                uploadModel.Thumbnail = fileUpload.Thumbnail;
                uploadModel.Extension = fileUpload.Extension;
                uploadModel.Size = fileUpload.Size;
                uploadModel.UploadDate = registerDate;
                uploadModel.UserId = userId;

                result.Data = uploadModel;
                return result;

            }
            catch (Exception e)
            {
                result.Status = ResultStatusEnum.ExceptionThrowed;
                result.Message = e.Message;
                return result;
            }
        }

        public async Task<Result> Delete(int userId, int fileId)
        {
            var result = new Result();
            var fileUpload = await _queryRepository.Table<FileUpload>().FirstOrDefaultAsync(x => x.Id == fileId && x.UserId == userId);
            if (fileUpload is null)
            {
                result.Status = ResultStatusEnum.NotFound;
                return result;
            }
            var directory = GetDirectory(fileUpload.Extension);

            if (File.Exists(uploadsPaths + directory + fileUpload.FileName))
            {
                File.Delete(uploadsPaths + directory + fileUpload.FileName);
            }
            if (File.Exists(uploadsPaths + directory + fileUpload.Thumbnail))
            {
                File.Delete(uploadsPaths + directory + fileUpload.Thumbnail);
            }
            _commandRepository.DeleteAsync(fileUpload);
            await _commandRepository.SaveChangesAsync();

            return result;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="fileInfo"></param>
        /// <returns></returns>
        public string? GenerateThumbnail(FileInfo fileInfo)
        {
            var imagesExtension = _fileStorageSetting.ImagesExtensions.Split(',');
            var videoExtension = _fileStorageSetting.VideosExtensions.Split(',');

            if (imagesExtension.Contains(fileInfo.Extension))
            {

                var thumbnailSize = _fileStorageSetting.ImageThumbnailSize;

                var extension = fileInfo.Extension;
                var fileNameOnly = fileInfo.Name.Substring(0, fileInfo.Name.Length - extension.Length);

                SixLabors.ImageSharp.Image image = SixLabors.ImageSharp.Image.Load(fileInfo.FullName);

                var imageHeight = image.Height;
                var imageWidth = image.Width;

                if (imageHeight > imageWidth)
                {
                    imageWidth = (int)((float)imageWidth / (float)imageHeight * thumbnailSize);
                    imageHeight = thumbnailSize;
                }
                else
                {
                    imageHeight = (int)((float)imageHeight / (float)imageWidth * thumbnailSize);
                    imageWidth = thumbnailSize;
                }

                image.Mutate(x => x.Resize(imageWidth, imageHeight, KnownResamplers.Lanczos3));

                var newSImageName = fileNameOnly + "-Thumb.png";

                image.SaveAsPng(fileInfo.Directory + "/" + newSImageName);
                image.Dispose();
                return newSImageName;
            }
            else if (videoExtension.Contains(fileInfo.Extension))
            {
                var extension = fileInfo.Extension;
                var fileNameOnly = fileInfo.Name.Substring(0, fileInfo.Name.Length - extension.Length);
                var newSVideoName = fileNameOnly + "-Thumb.jpg";
                var ffMpeg = new NReco.VideoConverter.FFMpegConverter();
                ffMpeg.GetVideoThumbnail(fileInfo.FullName, fileInfo.Directory + @"\" + newSVideoName, 3.0f);
                return newSVideoName;
            }
            return null;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="bytes"></param>
        /// <returns></returns>
        public string ConvertSizeToString(long bytes)
        {
            var fileSize = new decimal(bytes);
            var kilobyte = new decimal(1024);
            var megabyte = new decimal(1024 * 1024);
            var gigabyte = new decimal(1024 * 1024 * 1024);

            return fileSize switch
            {
                _ when fileSize < kilobyte => "Less then 1KB",
                _ when fileSize < megabyte =>
                    $"{Math.Round(fileSize / kilobyte, fileSize < 10 * kilobyte ? 2 : 1, MidpointRounding.AwayFromZero):##,###.##}KB",
                _ when fileSize < gigabyte =>
                    $"{Math.Round(fileSize / megabyte, fileSize < 10 * megabyte ? 2 : 1, MidpointRounding.AwayFromZero):##,###.##}MB",
                _ when fileSize >= gigabyte =>
                    $"{Math.Round(fileSize / gigabyte, fileSize < 10 * gigabyte ? 2 : 1, MidpointRounding.AwayFromZero):##,###.##}GB",
                _ => "n/a"
            };
        }

        /// <summary>
        /// </summary>
        /// <param name="objectId"></param>
        /// <returns></returns>
        public string GetDirectory(string extension)
        {
            if (_fileStorageSetting.ImagesExtensions.Split(',').Contains(extension))
            {
                return "images/";
            }
            if (_fileStorageSetting.VideosExtensions.Split(',').Contains(extension))
            {
                return "videos/";
            }
            if (_fileStorageSetting.AudioExtensions.Split(',').Contains(extension))
            {
                return "audio/";
            }
            if (_fileStorageSetting.DocumentsExtensions.Split(',').Contains(extension))
            {
                return "documents/";
            }
            return "others/";
        }

        /// <summary>
        /// </summary>
        /// <param name="objectId"></param>
        /// <returns></returns>
        public string[] GetDirectories()
        {
            string[] directories = { "images/", "videos/", "audio/", "documents/", "others/", };
            return directories;
        }
        /// <summary>
        /// </summary>
        /// <param name="objectId"></param>
        /// <returns></returns>
        public string Rename(string fileName)
        {

            var extension = Path.GetExtension(fileName).ToLower();
            var fileNameOnly = fileName.Substring(0, fileName.Length - extension.Length);

            for (int i = 1; i < 2000; i++)
            {
                var newFileName = fileNameOnly + "-" + i + extension;
                if (!IsExist(newFileName))
                {
                    return newFileName;
                }
            }
            return fileName;
        }
    }
}