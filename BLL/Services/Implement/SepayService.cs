using BLL.Services.Interface;
using Common.Settings;
using Google.Apis.Http;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace BLL.Services.Implement
{
    public class SepayService : ISepayService
    {
        //private readonly IHttpClientFactory _httpClientFactory;
        //private readonly string _sepayToken;

        //public SepayService(IHttpClientFactory httpClientFactory, IOptions<SePaySetting> sepayOptions)
        //{
        //    _httpClientFactory = httpClientFactory;
        //    _sepayToken = sepayOptions.Value.Token;
        //}

        //public async Task<string> CreateSepayPaymentUrlAsync(Donation donation)
        //{
        //    // 🏦 Thông tin tài khoản ngân hàng của tổ chức bạn (cấu hình cố định)
        //    var bankCode = "MBBank";
        //    var accountNumber = "0337147985";
        //    var template = "compact";

        //    // 💰 Thông tin từ donation
        //    var amount = (int)donation.Amount;
        //    var donationId = donation.DonationId.ToString();
        //    var description = $"DONATE_{donationId}";

        //    // ✅ Encode nội dung chuyển khoản để đảm bảo URL hợp lệ
        //    var encodedDes = HttpUtility.UrlEncode(description);

        //    // 📷 Tạo URL ảnh QR từ SePay
        //    var qrUrl = $"https://qr.sepay.vn/img?bank={bankCode}&acc={accountNumber}&amount={amount}&des={encodedDes}&template={template}";

        //    return qrUrl;
        //}
    }
}
