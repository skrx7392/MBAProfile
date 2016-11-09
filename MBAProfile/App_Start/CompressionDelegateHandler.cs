using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading;
using System.Threading.Tasks;
using System.Web;

namespace MBAProfile
{
    public class CompressionDelegateHandler: DelegatingHandler
    {
        protected override Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            return base.SendAsync(request, cancellationToken).ContinueWith<HttpResponseMessage>(inTerimResponse=> {
                if (!inTerimResponse.IsFaulted && inTerimResponse.IsCompleted)
                {
                    var response = inTerimResponse.Result;
                    var responseHeaders = response.RequestMessage.Headers.AcceptEncoding.Where(encode => encode.Value.ToLowerInvariant() == "gzip" || encode.Value.ToLowerInvariant() == "deflate").FirstOrDefault();
                    if (responseHeaders!=null && response.StatusCode==HttpStatusCode.OK)
                    {
                        Task<byte[]> responseByteArray = response.Content.ReadAsByteArrayAsync();
                        responseByteArray.Wait();
                        MemoryStream memory = new MemoryStream();
                        switch (responseHeaders.Value.ToLower())
                        {
                            case "gzip":
                                using (GZipStream compress = new GZipStream(memory, CompressionMode.Compress, true))
                                {
                                    compress.Write(responseByteArray.Result, 0, responseByteArray.Result.Length);
                                }
                                break;
                            case "deflate":
                                using (DeflateStream compress = new DeflateStream(memory, CompressionMode.Compress, true))
                                {
                                    compress.Write(responseByteArray.Result, 0, responseByteArray.Result.Length);
                                }
                                break;
                        }
                        memory.Position = 0;
                        StreamContent CompressedContent = new StreamContent(memory);
                        CompressedContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");
                        CompressedContent.Headers.ContentEncoding.Add(responseHeaders.Value.ToLower());
                        response.Content = CompressedContent;
                        return response;
                    }
                    return response;
                }
                return inTerimResponse.Result;
            },TaskContinuationOptions.OnlyOnRanToCompletion);
        }
    }
}