using System;
using System.Net;

namespace GoogleAnalyticsTracker.Test
{
    public interface IWebRequestCreate
    {
        WebRequest Create(Uri uri);
    }
}