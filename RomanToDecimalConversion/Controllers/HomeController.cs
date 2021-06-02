using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using RomanToDecimalConversion.Models;

namespace RomanToDecimalConversion.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HomeController : Controller
    {
        /// <summary>
        /// Default action
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        /// <summary>
        /// Calling conversion method upon button click
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public ActionResult Index([FromForm]ViewModel model)
        {
            Converter conv = new Converter();

            model.DecimalNumber = conv.RomanToDecimal(model.RomanNumber);

            ViewBag.Message = "Converted successfully!";
            return View(model); 
        }
        

        /// <summary>
        /// Error default action
        /// </summary>
        /// <returns></returns>
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }


    /// <summary>
    /// Converter class, code based on https://www.geeksforgeeks.org/converting-roman-numerals-decimal-lying-1-3999/
    /// </summary>
    class Converter
    {
        // The symbols are I, V, X, L, C, D, and M, standing respectively for 
        // 1, 5, 10, 50, 100, 500, and 1,000 
        // Given map:
        private int Value(char n)
        {
            switch (n)
            {
                case 'I':
                    return 1;
                case 'V':
                    return 5;
                case 'X':
                    return 10;
                case 'L':
                    return 50;
                case 'C':
                    return 100;
                case 'D':
                    return 500;
                case 'M':
                    return 1000;
                default:
                    return -1;
            }
        }

        // Actual conversion method
        public virtual string RomanToDecimal(string number)
        {
            int output = 0;

            for (int i = 0; i < number.Length; i++)
            {
                // Getting value of symbol at [i]
                int sym1 = Value(number[i]);

                // Getting value of symbol at [i+1]
                if (i + 1 < number.Length)
                {
                    int sym2 = Value(number[i + 1]);

                    // Comparing both values
                    if (sym1 >= sym2)
                    {
                        // Value of current symbol is greater or equal to the next symbol
                        output += sym1;
                    }
                    else
                    {
                        output += sym2 - sym1;
                        // Value of current symbol is less than the next symbol
                        i++; 
                    }
                }
                else
                {
                    output += sym1;
                    i++;
                }
            }

            return output.ToString();
        }

    }

}
