using System.IO;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Hosting;
using System.Web.Http;

namespace CatalogExplorer.Web
{
    public class FileActionResult : IHttpActionResult
    {
        public FileActionResult(string path, string filename)
        {
            _path = path;
            _filename = filename;
        }

        private readonly string _path;
        private readonly string _filename;

        public Task<HttpResponseMessage> ExecuteAsync(CancellationToken cancellationToken)
        {
            HttpResponseMessage response = new HttpResponseMessage
            {
                StatusCode = HttpStatusCode.OK,
                Content = new StreamContent(new FileStream($"{HostingEnvironment.MapPath("~/")}/{_path}/{_filename}", FileMode.Open, FileAccess.Read))
            };
            response.Content.Headers.ContentDisposition = new ContentDispositionHeaderValue("attachment")
            {
                FileName = _filename
            };
            response.Content.Headers.ContentType =
                new MediaTypeHeaderValue("application/octet-stream");

            return Task.FromResult(response);
        }
    }
}