using UrlShortener.Models;

namespace UrlShortener.Business
{
    public interface IShortenerTool
    {
        ResponseModel ShortenTheUrl(string url);

        ResponseModel RedirectionUrl(string shortUrl);

        ResponseModel CustomUrl(UrlModel model);
    }
}
