using Microsoft.AspNetCore.Mvc;
using UrlShortener.Business;
using UrlShortener.Models;

namespace UrlShortener.Controllers
{
    [Route("api/urlShortener")]
    [ApiController]
    public class UrlShortenerController : Controller
    {
        IShortenerTool _shortenerTool;

        public UrlShortenerController(IShortenerTool shortenerTool)
        {
            _shortenerTool = shortenerTool;
        }

        [Route("shortening")]
        [HttpPost]
        public IActionResult Shortening(string url)
        {
            ResponseModel response = _shortenerTool.ShortenTheUrl(url);
            if (response.StatusCode is 200)
            {
                return Ok(response.ReturnUrl);
            }
            else
            {
                return BadRequest(response.Message);
            }
        }

        [Route("redirection")]
        [HttpPost]
        public IActionResult Redirection(string shortUrl)
        {
            ResponseModel response = _shortenerTool.RedirectionUrl(shortUrl);
            if (response.StatusCode is 200)
            {
                return Ok(response.ReturnUrl);
            }
            else
            {
                return NotFound(response.Message);
            }
        }

        [Route("customize")]
        [HttpPost]
        public IActionResult CustomUrl(UrlModel model)
        {
            ResponseModel response = _shortenerTool.CustomUrl(model);
            if (response.StatusCode is 200)
            {
                return Ok(response.ReturnUrl);
            }
            else
            {
                return BadRequest();
            }
        }
    }
}
