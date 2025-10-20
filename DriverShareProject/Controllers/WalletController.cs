using BLL.Services.Interface;
using Microsoft.AspNetCore.Mvc;

namespace DriverShareProject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]


    public class WalletController : ControllerBase
    {
        private readonly IWalletService _walletService;
        public WalletController(IWalletService walletService)
        {
            _walletService = walletService;
        }

        [HttpGet]
        [Route("user")]
        public async Task<IActionResult> GetWalletByUserId()
        {
            var response = await _walletService.GetWalletByUserId();
            return StatusCode(response.StatusCode, response);
        }

        [HttpPost]
        public async Task<IActionResult> CreateWallet()
        {
            var response = await _walletService.CreateWalletAsync();
            return StatusCode(response.StatusCode, response);
        }

        [HttpPost]
        [Route("deposit")]
        public async Task<IActionResult> Deposit([FromQuery] decimal amount)
        {
            var response = await _walletService.DepositAsync(amount);
            return StatusCode(response.StatusCode, response);
        }

        [HttpPost]
        [Route("withdraw")]
        public async Task<IActionResult> Withdraw([FromQuery] decimal amount)
        {
            var response = await _walletService.WithdrawAsync(amount);
            return StatusCode(response.StatusCode, response);
        }

    }
}
