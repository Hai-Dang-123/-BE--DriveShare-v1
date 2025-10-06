using BLL.Services.Interface;
using Common.Constants;
using Common.DTOs;
using Common.Enums;
using Common.Messages;
using DAL.Entities;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace BLL.Services.Implement
{
    public class EKYCService : IEKYCService
    {
        private readonly HttpClient _httpClient;
        private readonly IVNPTTokenService _vnptTokenService;


        public EKYCService(HttpClient httpClient, IVNPTTokenService vnptTokenService)
        {
            _httpClient = httpClient;
            _vnptTokenService = vnptTokenService;
        }

        public async Task<ResponseDTO> UploadFileAsync(IFormFile file, string title, string description = null)
        {
            try
            {
                var accessToken = await _vnptTokenService.GetAccessTokenAsync();
                var (tokenKey, tokenId) = await _vnptTokenService.GetServiceTokensAsync("eKYC");

                using var request = new HttpRequestMessage(HttpMethod.Post, $"{EKYCConstant.FileUploadEndpoint}");
                request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);
                request.Headers.Add("Token-id", tokenId);
                request.Headers.Add("Token-key", tokenKey);

                using var form = new MultipartFormDataContent();
                using var fileStream = file.OpenReadStream();
                var fileContent = new StreamContent(fileStream);
                fileContent.Headers.ContentType = new MediaTypeHeaderValue(file.ContentType);
                form.Add(fileContent, "file", file.FileName);

                form.Add(new StringContent(title), "title");
                if (!string.IsNullOrEmpty(description))
                    form.Add(new StringContent(description), "description");

                request.Content = form;

                var response = await _httpClient.SendAsync(request);
                using var stream = await response.Content.ReadAsStreamAsync();
                using var doc = await JsonDocument.ParseAsync(stream);

                var message = doc.RootElement.GetProperty("message").GetString();
                if (message != "IDG-00000000")
                {
                    return new ResponseDTO($"Upload file failed: {message}", (int)response.StatusCode, false);
                }

                var hash = doc.RootElement.GetProperty("object").GetProperty("hash").GetString();
                return new ResponseDTO("Upload success", 200, true, hash);
            }
            catch (Exception ex)
            {
                return new ResponseDTO($"Upload file exception: {ex.Message}", 500, false);
            }
        }



        public async Task<ResponseDTO> OcrAsync(DocumentType docType, string fileHash)
        {
            try
            {
                var accessToken = await _vnptTokenService.GetAccessTokenAsync();
                var (tokenKey, tokenId) = await _vnptTokenService.GetServiceTokensAsync("eKYC");

                using var request = new HttpRequestMessage(HttpMethod.Post, $"{EKYCConstant.OcrEndpoint}");
                request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);
                request.Headers.Add("Token-id", tokenId);
                request.Headers.Add("Token-key", tokenKey);

                request.Content = JsonContent.Create(new { hash = fileHash, type = docType.ToString() });

                var response = await _httpClient.SendAsync(request);

                using var stream = await response.Content.ReadAsStreamAsync();
                using var doc = await JsonDocument.ParseAsync(stream);

                var message = doc.RootElement.TryGetProperty("message", out var msgProp) ? msgProp.GetString() : null;

                if (response.IsSuccessStatusCode && message == "IDG-00000000")
                {
                    return new ResponseDTO("OCR success", 200, true, doc.RootElement.GetRawText());
                }

                return new ResponseDTO("OCR failed", (int)response.StatusCode, false, doc.RootElement.GetRawText());
            }
            catch (Exception ex)
            {
                return new ResponseDTO($"OCR exception: {ex.Message}", 500, false);
            }
        }

    }
}
