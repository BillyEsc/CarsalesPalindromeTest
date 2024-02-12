using Microsoft.AspNetCore.Mvc;

namespace CarsalesPalindromeTest.Controllers;

public class PalindromeController : Controller
{
    // GET
    [Route("api/[controller]")]
    [ApiController]
    public class StringOperationsController : ControllerBase
    {
        [HttpPost("operations")]
        public ActionResult<StringOperationsResult> PerformOperations([FromBody] List<string> words)
        {
            var result = new StringOperationsResult
            {
                Palindromes = words.Where(IsPalindrome).ToList(),
                CountRepeatedStrings = words.GroupBy(w => w).ToDictionary(g => g.Key, g => g.Count()),
                SortedStrings = words.OrderBy(w => w).ToList(),
                AverageStringLength = words.Any() ? words.Average(w => w.Length) : 0,
                NumberStrings = words.Where(w => w.All(char.IsDigit)).ToList()
            };

            return Ok(result);
        }

        private bool IsPalindrome(string str)
        {
            int i = 0, j = str.Length - 1;
            while (i < j)
            {
                if (str[i] != str[j])
                    return false;
                i++;
                j--;
            }
            return true;
        }
    }

    public class StringOperationsResult
    {
        public List<string> Palindromes { get; set; }
        public Dictionary<string, int> CountRepeatedStrings { get; set; }
        public List<string> SortedStrings { get; set; }
        public double AverageStringLength { get; set; }
        public List<string> NumberStrings { get; set; }
    }
}