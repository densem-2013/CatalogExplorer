using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Hosting;
using System.Web.Http;
using AutoMapper;
using CatalogExplorer.DAL.Models;
using CatalogExplorer.DAL.UnitOfWork;
using File = CatalogExplorer.DAL.Models.File;

namespace CatalogExplorer.Web.ApiControllers
{
    [RoutePrefix("api/file")]
    public class FileController : ApiController
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper = Application.Mapper;

        public FileController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        [HttpGet]
        [Route("{id}")]
        public async Task<IHttpActionResult> GetFile(int id)
        {
            return await Task<IHttpActionResult>.Factory.StartNew(() =>
            {
                var fileRepo = _unitOfWork.Repository<File>();

                var file = fileRepo.FirstOrDefault(f => f.Id == id, null,
                    new List<System.Linq.Expressions.Expression<Func<File, object>>>
                    {
                        f => f.Catalog
                    });

                if (file == null)
                {
                    return NotFound();
                }
                return new FileActionResult($"{file.Catalog.Path}/{file.Catalog.Name}", file.Name);

            }, CancellationToken.None);
        }

        [HttpPost]
        [Route("")]
        public async Task<IHttpActionResult> AddFile()
        {
            // Check if the request contains multipart/form-data.
            if (!Request.Content.IsMimeMultipartContent())
            {
                throw new HttpResponseException(HttpStatusCode.UnsupportedMediaType);
            }

            var fileRepo = _unitOfWork.Repository<File>();
            var file = new File();

            try
            {
                // Read the form data.
                var fprovider =
                    await Request.Content.ReadAsMultipartAsync(new InMemoryMultipartFormDataStreamProvider());
                //access form data  
                var formData = fprovider.FormData;
                //access files  
                string path = null;
                foreach (var key in formData.AllKeys)
                {
                    if (string.IsNullOrEmpty(key)) continue;

                    if (string.Equals(key, "Path")) path = formData.GetValues(key)?[0];

                    foreach (var val in formData.GetValues(key))
                    {

                        var propertyInfo = file.GetType().GetProperty(key);
                        if (propertyInfo != null)
                        {
                            var t = Nullable.GetUnderlyingType(propertyInfo.PropertyType) ?? propertyInfo.PropertyType;

                            var safeValue = val == null
                                ? null
                                : Convert.ChangeType(val, t);

                            propertyInfo.SetValue(file, safeValue, null);
                        }
                        Trace.WriteLine($"{key}: {val}");
                    }
                }
                IList<HttpContent> files = fprovider.Files;

                var file1 = files[0];

                if (file1 != null)
                {
                    var fileContent = await file1.ReadAsByteArrayAsync();

                    var maxContentLength = 1024 * 1024 * 50;
                    if (fileContent.Length > maxContentLength)
                    {
                        return BadRequest("FileLenth larger than 50 MB !!!");
                    }

                    var fileName = file1.Headers.ContentDisposition.FileName.Replace("\"", string.Empty);

                    file.Name = fileName;

                    fileRepo.Create(file);
                    fileRepo.Save();

                    Stream stream = await file1.ReadAsStreamAsync();
                    using (FileStream output = new FileStream($"{HostingEnvironment.MapPath("~/")}/{path}/{fileName}",
                        FileMode.Create))
                    {
                        stream.CopyTo(output);
                    }

                    var view = _mapper.Map<File, FileView>(file);

                    return Ok(view);
                }
                return BadRequest("No files in request!!!");
            }
            catch (Exception e)
            {
                return InternalServerError(e);
            }
        }

        [HttpDelete]
        [Route("{id}")]
        public IHttpActionResult RemoveFile(int id)
        {
            var fileRepo = _unitOfWork.Repository<File>();
            var file = fileRepo.FirstOrDefault(f => f.Id == id, null,
                new List<System.Linq.Expressions.Expression<Func<File, object>>>
                {
                    f => f.Catalog
                });

            if (file == null)
            {
                return BadRequest($"File with id = {id} not found!!!");
            }

            var filename = $"{HostingEnvironment.MapPath("~/")}/{file.Catalog.Path}/{file.Catalog.Name}/{file.Name}";
            if (System.IO.File.Exists(filename))
            {
                System.IO.File.Delete(filename);
            }

            fileRepo.Delete(file);
            fileRepo.Save();

            return Ok();
        }
    }
}
