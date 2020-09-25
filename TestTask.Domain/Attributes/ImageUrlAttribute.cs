using System.ComponentModel.DataAnnotations;
using System.Globalization;
using System.Net;

namespace TestTask.Domain.Attributes
{
    public class ImageUrlAttribute : ValidationAttribute
    {
        public override bool IsValid(object value)
        {
            if (!(value is string url))
            {
                return false;
            }
            var req = WebRequest.Create(url);
            req.Method = "HEAD";
            using var resp = req.GetResponse();
            return resp.ContentType.ToLower(CultureInfo.InvariantCulture)
                .StartsWith("image/");
        }
    }
}