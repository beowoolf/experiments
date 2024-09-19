using Microsoft.AspNetCore.Mvc;

namespace Fibonacci.Controllers
{
    [ApiController]
    [Route("csharpasync")]
    public class CSharpAsyncController : ControllerBase
    {
        private readonly ILogger<CSharpAsyncController> _logger;

        public CSharpAsyncController(ILogger<CSharpAsyncController> logger)
        {
            _logger = logger;
        }

        private long Fibo(int i)
        {
            if (i <= 1) return i;
            else return Fibo(i - 2) + Fibo(i - 1);
        }

        [HttpGet(Name = "FiboAsync")]
        public async Task<long> GetFibo(int i)
        {
            return await Task.Run(() => Fibo(i));
        }
    }
}
