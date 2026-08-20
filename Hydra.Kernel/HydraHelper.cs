
using Hydra.Kernel.Constants;
using Hydra.Kernel.GeneralModels;
using Microsoft.AspNetCore.Http;
using System.Reflection;
using System.Security.Claims;
using System.Text.RegularExpressions;
using SkiaSharp;
using System.IO;

namespace Hydra.Kernel
{
    public static class HydraHelper
    {
        public static Assembly[] GetAssemblies(Func<string, bool> func, SearchOption searchOption = SearchOption.TopDirectoryOnly)
        {
            var assemblies = Directory.GetFiles(AppDomain.CurrentDomain.BaseDirectory, "*.dll",
                searchOption)
                .Where(x => func(Path.GetFileName(x)))
                .Select(x => Assembly.LoadFrom(x));

            return assemblies.ToArray();
        }
        public static string GetCurrentDomain(HttpContext context)
        {
            return $"{context.Request.Scheme}://{context.Request.Host.Value}/"; ;
        }
        public static string GetApplicationDirectory()
        {
            return Directory.GetCurrentDirectory();
        }
        public static string GetAvatarDirectory()
        {
            return Path.Combine(Directory.GetCurrentDirectory(), Directory.GetCurrentDirectory() + "images", "avatar");
        }
        public static string GetProductDirectory()
        {
            return Path.Combine(Directory.GetCurrentDirectory(), "images", "product");
        }
        public static string GetUploadsDirectory()
        {
            return Path.Combine(Directory.GetCurrentDirectory(), "uploads");
        }
        public static void DirectorySearch(string dir)
        {
            foreach (string f in Directory.GetFiles(dir))
            {
                Console.WriteLine(Path.GetFileName(f));
            }
            foreach (string d in Directory.GetDirectories(dir))
            {
                Console.WriteLine(Path.GetFileName(d));
                DirectorySearch(d);
            }
        }
        public static FileModel Base64FileToBytes(string input)
        {
            if (string.IsNullOrEmpty(input))
            {
                throw new ArgumentNullException(nameof(input));
            }

            int indexOfSemiColon = input.IndexOf(";", StringComparison.OrdinalIgnoreCase);

            string dataLabel = input.Substring(0, indexOfSemiColon);

            string contentType = dataLabel.Split(':').Last();

            var startIndex = input.IndexOf("base64,", StringComparison.OrdinalIgnoreCase) + 7;

            var fileContents = input.Substring(startIndex);

            var bytes = Convert.FromBase64String(fileContents);

            return new FileModel()
            {
                ContentType = contentType,
                FileBytes = bytes
            };
        }

        public static int GetUserId(this ClaimsPrincipal userPrincipal)
        {
            var userId = userPrincipal.FindFirst(CustomClaimTypes.Identity).Value;
            if (userId is null)
            {
                throw new Exception("USER DOES NOT LOGINED!");
            }
            return int.Parse(userId);
        }

        public static string GetEmail(this ClaimsPrincipal userPrincipal)
        {
            var email = userPrincipal.FindFirst(ClaimTypes.Email).Value;
            if (string.IsNullOrEmpty(email))
            {
                throw new Exception("USER DOES NOT LOGINED!");
            }
            return email;
        }
        public static string GetName(this ClaimsPrincipal userPrincipal)
        {
            var name = userPrincipal.FindFirst(ClaimTypes.Name).Value;
            if (string.IsNullOrEmpty(name))
            {
                throw new Exception("USER DOES NOT LOGINED!");
            }
            return name;
        }
        public static string GetIdentityName(this ClaimsPrincipal userPrincipal)
        {
            var identityName = userPrincipal.Identity.Name;
            if (identityName is null)
            {
                throw new Exception("USER DOES NOT LOGINED!");
            }
            return identityName;
        }
        public static DateTime? GetExpiration(this ClaimsPrincipal userPrincipal)
        {
            return DateTimeOffset.FromUnixTimeSeconds(long.Parse(userPrincipal.FindFirst(CustomClaimTypes.Expiration).Value)).DateTime;
        }

        /// <summary>
        /// remove html from text
        /// </summary>
        /// <param name="text"></param>
        /// <returns></returns>
        public static string SanitizeText(string text)
        {
            if (string.IsNullOrEmpty(text))
                return text;
            var withoutHtml = Regex.Replace(text, "<.*?>", string.Empty);
            return System.Net.WebUtility.HtmlDecode(withoutHtml).Trim();
        }
        /// <summary>
        /// convert 35.2546522 to 35.25
        /// </summary>
        /// <param name="decimals"></param>
        /// <returns></returns>
        public static string ToFixed(this decimal decimals)
        {
            return decimals.ToString("N2");
        }

        public static void SaveThumbnail(string sourcePath, string outputPath, int thumbWidth, int thumbHeight)
        {
            // 1. Decode the bitmap from the file
            using var src = SKBitmap.Decode(sourcePath);
            if (src == null)
                throw new ArgumentException("Could not decode image at the provided path.");

            // 2. Compute dimensions preserving aspect ratio
            var (newW, newH) = FitInside(src.Width, src.Height, thumbWidth, thumbHeight);

            // 3. Create the destination bitmap
            using var dst = new SKBitmap(newW, newH, src.ColorType, src.AlphaType);

            // 4. Scale the pixels (Best Quality)
            src.ScalePixels(
                dst,SKSamplingOptions.Default);

            // 5. Encode to file
            using var image = SKImage.FromBitmap(dst);
            using var data = image.Encode(SKEncodedImageFormat.Jpeg, 85); // 85 is high quality

            using var stream = File.OpenWrite(outputPath);
            data.SaveTo(stream);
        }

        private static (int W, int H) FitInside(int srcW, int srcH, int maxW, int maxH)
        {
            float ratio = Math.Min((float)maxW / srcW, (float)maxH / srcH);
            return ((int)Math.Round(srcW * ratio), (int)Math.Round(srcH * ratio));
        }
    }
}
