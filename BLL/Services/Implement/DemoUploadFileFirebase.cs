using Common.Settings;
using Google.Apis.Auth.OAuth2;
using Google.Cloud.Storage.V1;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;

namespace BLL.Services.Implement
{
    public class DemoUploadFileFirebase
    {
        private readonly FirebaseSetting _firebaseSetting;

        public DemoUploadFileFirebase(IOptions<FirebaseSetting> firebaseSetting)
        {
            _firebaseSetting = firebaseSetting.Value;
        }

        public async Task<string> UploadImageToFirebaseAsync(IFormFile file)
        {
            if (file == null || file.Length == 0)
                throw new ArgumentException("File không hợp lệ");

            // Lấy config từ appsettings.json
            var bucketName = _firebaseSetting.BucketName;
            var credentialPath = Path.Combine(
                Directory.GetCurrentDirectory(),
                "firebase",
                _firebaseSetting.TokenPath);

            // Load credential từ file JSON
            var credential = GoogleCredential.FromFile(credentialPath);
            var storage = await StorageClient.CreateAsync(credential);

            // Tạo tên file mới
            var fileName = $"{Guid.NewGuid()}{Path.GetExtension(file.FileName)}";

            using (var stream = file.OpenReadStream())
            {
                await storage.UploadObjectAsync(
                    bucketName,
                    fileName,
                    null,
                    stream,
                    new UploadObjectOptions
                    {
                        PredefinedAcl = PredefinedObjectAcl.PublicRead // Cho phép public
                    });
            }

            // Trả về URL public của ảnh
            return $"https://firebasestorage.googleapis.com/v0/b/{bucketName}/o/{Uri.EscapeDataString(fileName)}?alt=media";
        }
    }
}
