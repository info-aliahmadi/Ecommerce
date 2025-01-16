using Hydra.FileStorage.Api.Handler;
using Hydra.FileStorage.Api.Services;
using Hydra.FileStorage.Core.Interfaces;
using Hydra.FileStorage.Core.SignatureVerify;
using Hydra.Infrastructure.ModuleExtension;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.DependencyInjection;
using Hydra.Infrastructure.Security.Extension;
using Hydra.FileStorage.Core.Constants;

namespace Hydra.FileStorage.Api.Endpoints
{
    public class FileStorageModule : IModule
    {
        private const string API_SCHEMA = "/FileStorage";
        public IServiceCollection RegisterModules(IServiceCollection services)
        {

            services.AddScoped<IFileTypeVerifier, FileTypeVerifier>();
            services.AddScoped<IValidationService, ValidationService>();
            services.AddScoped<IFileStorageService, FileStorageService>();

            return services;
        }

        public IEndpointRouteBuilder MapEndpoints(IEndpointRouteBuilder endpoints)
        {

            endpoints.MapGet(API_SCHEMA + "/GetFileInfo", FileStorageHandler.GetFileInfo).RequirePermission(FileStoragePermissionTypes.FS_GET_FILE_INFO);
            endpoints.MapPost(API_SCHEMA + "/GetFilesInfo", FileStorageHandler.GetFilesInfo).RequirePermission(FileStoragePermissionTypes.FS_GET_FILE_INFO);
            endpoints.MapGet(API_SCHEMA + "/GetFileInfoByName", FileStorageHandler.GetFileInfoByName).RequirePermission(FileStoragePermissionTypes.FS_GET_FILE_INFO_BY_NAME);
            endpoints.MapGet(API_SCHEMA + "/GetFilesList", FileStorageHandler.GetFilesList).RequirePermission(FileStoragePermissionTypes.FS_GET_FILES_LIST);
            endpoints.MapGet(API_SCHEMA + "/GetGalleyFiles", FileStorageHandler.GetGalleyFiles).RequirePermission(FileStoragePermissionTypes.FS_GET_GALLEY_FILES);
            endpoints.MapGet(API_SCHEMA + "/GetDirectories", FileStorageHandler.GetDirectories).RequirePermission(FileStoragePermissionTypes.FS_GET_DIRECTORIES);
            endpoints.MapGet(API_SCHEMA + "/GetFilesByDirectory", FileStorageHandler.GetFilesByDirectory).RequirePermission(FileStoragePermissionTypes.FS_GET_FILES_BY_DIRECTORY);
            endpoints.MapGet(API_SCHEMA + "/DeleteFile", FileStorageHandler.DeleteFile).RequirePermission(FileStoragePermissionTypes.FS_DELETE_ARTICLE);

            endpoints.MapPost(API_SCHEMA + "/UploadFile", FileStorageHandler.UploadFile).RequireAuthorization();
            endpoints.MapPost(API_SCHEMA + "/UploadBase64File", FileStorageHandler.UploadBase64File).RequireAuthorization();
            endpoints.MapPost(API_SCHEMA + "/UploadSmallFile", FileStorageHandler.UploadSmallFile).RequireAuthorization();
            endpoints.MapPost(API_SCHEMA + "/UploadLargeFile", FileStorageHandler.UploadLargeFile).RequireAuthorization();

            endpoints.MapGet(API_SCHEMA + "/DownloadFile", FileStorageHandler.DownloadFile).AllowAnonymous();
            endpoints.MapGet(API_SCHEMA + "/DownloadFileStream", FileStorageHandler.DownloadFileStream).AllowAnonymous();
            endpoints.MapGet(API_SCHEMA + "/DownloadFileByName", FileStorageHandler.DownloadFileByName).AllowAnonymous();
            endpoints.MapGet(API_SCHEMA + "/DownloadFileStreamByName", FileStorageHandler.DownloadFileStreamByName).AllowAnonymous();

            return endpoints; 
        }

    }
}
