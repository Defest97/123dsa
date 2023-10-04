using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WebAppDemoApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CalcController : ControllerBase
    {

        // GET api/<CalcController>/5
        [HttpGet("add")]
        public string GetAdd(string a,string b)
        {
            if (double.TryParse(a, out double val1))
            {
                if (double.TryParse(b, out double val2)) 
                {
                    return $"{val1}+{val2}={val1 + val2} ";
                }
            }
            return $"Помилка параметрір a={a} b={b}";

        }


    }
}
