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

            endpoints.MapGet(API_SCHEMA + "/GetFileInfo", FileStorageHandler.GetFileInfo).RequirePermission(FileStoragePermissionTypes.FS_GALLEY_VIEW);
            endpoints.MapPost(API_SCHEMA + "/GetFilesInfo", FileStorageHandler.GetFilesInfo).RequirePermission(FileStoragePermissionTypes.FS_GALLEY_VIEW);
            endpoints.MapGet(API_SCHEMA + "/GetFileInfoByName", FileStorageHandler.GetFileInfoByName).RequirePermission(FileStoragePermissionTypes.FS_GALLEY_VIEW);
            endpoints.MapGet(API_SCHEMA + "/GetFilesList", FileStorageHandler.GetFilesList).RequirePermission(FileStoragePermissionTypes.FS_GALLEY_VIEW);
            endpoints.MapGet(API_SCHEMA + "/GetGalleyFiles", FileStorageHandler.GetGalleyFiles).RequirePermission(FileStoragePermissionTypes.FS_GALLEY_VIEW);
            endpoints.MapGet(API_SCHEMA + "/GetDirectories", FileStorageHandler.GetDirectories).RequirePermission(FileStoragePermissionTypes.FS_GALLEY_VIEW);
            endpoints.MapGet(API_SCHEMA + "/GetFilesByDirectory", FileStorageHandler.GetFilesByDirectory).RequirePermission(FileStoragePermissionTypes.FS_GALLEY_VIEW);

            endpoints.MapGet(API_SCHEMA + "/DeleteFile", FileStorageHandler.DeleteFile).RequirePermission(FileStoragePermissionTypes.FS_FILE_UPLOAD);
            endpoints.MapPost(API_SCHEMA + "/UploadFile", FileStorageHandler.UploadFile).RequirePermission(FileStoragePermissionTypes.FS_FILE_UPLOAD);
            endpoints.MapPost(API_SCHEMA + "/UploadBase64File", FileStorageHandler.UploadBase64File).RequirePermission(FileStoragePermissionTypes.FS_FILE_UPLOAD);
            endpoints.MapPost(API_SCHEMA + "/UploadSmallFile", FileStorageHandler.UploadSmallFile).RequirePermission(FileStoragePermissionTypes.FS_FILE_UPLOAD);
            endpoints.MapPost(API_SCHEMA + "/UploadLargeFile", FileStorageHandler.UploadLargeFile).RequirePermission(FileStoragePermissionTypes.FS_FILE_UPLOAD);

            endpoints.MapGet(API_SCHEMA + "/DownloadFile", FileStorageHandler.DownloadFile).AllowAnonymous();
            endpoints.MapGet(API_SCHEMA + "/DownloadFileStream", FileStorageHandler.DownloadFileStream).AllowAnonymous();
            endpoints.MapGet(API_SCHEMA + "/DownloadFileByName", FileStorageHandler.DownloadFileByName).AllowAnonymous();
            endpoints.MapGet(API_SCHEMA + "/DownloadFileStreamByName", FileStorageHandler.DownloadFileStreamByName).AllowAnonymous();

            return endpoints; 
        }

    }
}
