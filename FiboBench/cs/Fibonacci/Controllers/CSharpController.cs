using Microsoft.AspNetCore.Mvc;

namespace Fibonacci.Controllers
{
    [ApiController]
    [Route("csharp")]
    public class CSharpController : ControllerBase
    {
        private readonly ILogger<CSharpController> _logger;

        public CSharpController(ILogger<CSharpController> logger)
        {
            _logger = logger;
        }

        [HttpGet(Name = "Fibo")]
        public long Fibo(int i)
        {
            if (i <= 1) return i;
            else return Fibo(i - 2) + Fibo(i - 1);
        }
    }
}
