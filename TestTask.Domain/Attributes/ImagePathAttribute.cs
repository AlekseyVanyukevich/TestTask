using System.ComponentModel.DataAnnotations;
using System.Globalization;
using System.Net;

namespace TestTask.Domain.Attributes
{
    public class ImagePathAttribute : ValidationAttribute
    {
        public override bool IsValid(object value)
        {
            if (!(value is string path))
            {
                return false;
            }
            var req = WebRequest.Create(path);
            req.Method = "HEAD";
            using var resp = req.GetResponse();
            return resp.ContentType.ToLower(CultureInfo.InvariantCulture)
                .StartsWith("image/");
        }
    }
}