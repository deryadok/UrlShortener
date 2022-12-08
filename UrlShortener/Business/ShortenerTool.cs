using UrlShortener.Models;

namespace UrlShortener.Business
{
    public class ShortenerTool : IShortenerTool
    {
        private static Dictionary<string, string> _urls = new Dictionary<string, string>();

        public ResponseModel ShortenTheUrl(string url)
        {
            ResponseModel responseModel = new ResponseModel();
            Uri uri = new Uri(url);
            if (uri.AbsolutePath.Length > 7)
            {
                // Map to store 62 possible characters 
                char[] map = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789".ToCharArray();

                string shorturl = "";
                Random rnd = new Random();

                for (int i = 0; i < 6; i++)
                {
                    shorturl += map[rnd.Next(0, map.Length)];
                }

                responseModel.ReturnUrl = uri.Scheme + "://" + uri.Host + "/" + shorturl;
                responseModel.StatusCode = 200;
                responseModel.Message = "OK";
                _urls.Add(shorturl, url);
            }
            else
            {
                responseModel.StatusCode = 400;
                responseModel.Message = "The url is already short!";
            }
            // Reverse shortURL to complete base conversion 
            return responseModel;
        }

        public ResponseModel RedirectionUrl(string shortUrl)
        {
            ResponseModel responseModel = new ResponseModel();
            Uri uri = new Uri(shortUrl);
            string key = uri.AbsolutePath.Substring(1, uri.AbsolutePath.Length - 1);
            string value = string.Empty;
            bool isExist = _urls.TryGetValue(key, out value);

            if (!isExist)
            {
                responseModel.StatusCode = 404;
                responseModel.Message = "Not found URL";
            }
            else
            {
                responseModel.StatusCode = 200;
                responseModel.ReturnUrl = value;
                responseModel.Message = "OK";
            }

            return responseModel;
        }

        public ResponseModel CustomUrl(UrlModel model)
        {
            ResponseModel responseModel = new ResponseModel();
            Uri uri = new Uri(model.URL);
            responseModel.StatusCode = 200;
            responseModel.Message = "OK";
            responseModel.ReturnUrl = uri.Scheme + "://" + uri.Host + "/" + model.CustomUrl;
            _urls.Add(model.CustomUrl, model.URL);

            return responseModel;
        }
    }
}
