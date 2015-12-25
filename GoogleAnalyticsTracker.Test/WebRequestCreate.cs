using System;
using System.Net;

namespace GoogleAnalyticsTracker.Test
{
    class WebRequestCreate : IWebRequestCreate
    {
        /// <summary>
        /// Creates a <see cref="T:System.Net.WebRequest"/> instance.
        /// </summary>
        /// <returns>
        /// A <see cref="T:System.Net.WebRequest"/> instance.
        /// </returns>
        /// <param name="uri">The uniform resource identifier (URI) of the Web resource.</param>
        /// <exception cref="T:System.NotSupportedException">The request scheme specified in <paramref name="uri"/> is
        /// not supported by this <see cref="T:System.Net.IWebRequestCreate"/> instance.</exception>
        /// <exception cref="T:System.ArgumentNullException"><paramref name="uri"/> is null. </exception>
        /// <exception cref="T:System.UriFormatException">In the .NET for Windows Store apps or the Portable Class
        /// Library, catch the base class exception, <see cref="T:System.FormatException"/>, instead.The URI specified
        /// in <paramref name="uri"/> is not a valid URI.</exception>
        public WebRequest Create(Uri uri)
        {
            return WebRequest.Create(uri);
        }
    }
}
