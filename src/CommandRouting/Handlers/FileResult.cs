using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.Net.Http.Headers;

namespace CommandRouting.Handlers
{
    public class FileResult: Handled<Stream>
    {
        // default buffer size as defined in BufferedStream type
        private const int BufferSize = 0x1000;

        public MediaTypeHeaderValue ContentType { get; }
        public string FileDownloadName { get; }

        public FileResult(Stream fileStream, string contentType, string fileDownloadName = null)
            : this(fileStream, new MediaTypeHeaderValue(contentType), fileDownloadName)
        {            
        }

        public FileResult(Stream fileStream, MediaTypeHeaderValue contentType, string fileDownloadName = null) 
            : base(fileStream)
        {
            if (fileStream == null) throw new ArgumentNullException(nameof(fileStream));

            ContentType = contentType;
            FileDownloadName = fileDownloadName;
        }

        public async Task WriteResponseAsync(HttpContext httpContext)
        {
            httpContext.Response.ContentType = ContentType.ToString();

            if (!string.IsNullOrEmpty(FileDownloadName))
            {
                // From RFC 2183, Sec. 2.3:
                // The sender may want to suggest a filename to be used if the entity is
                // detached and stored in a separate file. If the receiving MUA writes
                // the entity to a file, the suggested filename should be used as a
                // basis for the actual filename, where possible.
                var contentDisposition = new ContentDispositionHeaderValue("attachment");
                contentDisposition.SetHttpFileName(FileDownloadName);
                httpContext.Response.Headers[HeaderNames.ContentDisposition] = contentDisposition.ToString();
            }

            await WriteFileAsync(httpContext);
        }

        /// <summary>
        /// Writes the file to the specified <paramref name="httpContext"/>.
        /// </summary>
        /// <param name="httpContext">The <see cref="HttpContext"/>.</param>
        /// <returns>
        /// A <see cref="Task"/> that will complete when the file has been written to the response.
        /// </returns>
        protected async Task WriteFileAsync(HttpContext httpContext)
        {
            var outputStream = httpContext.Response.Body;

            var fileStream = Response as Stream;
            using (fileStream)
            {
                await fileStream.CopyToAsync(outputStream, BufferSize);
            }
        }
    }
}