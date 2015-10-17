using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Web;

namespace GoogleAnalyticsTracker
{
    public class Tracker
    {
        private string endpoint = "http://www.google-analytics.com/collect";
        private string googleTrackingID = "UA-XXXXXXXX-XX";
        private string googleClientID = "555";

        public Tracker(string trackingID, string clientID)
        {
            this.googleTrackingID = trackingID;
            this.googleClientID = clientID;
        }

        public void TrackEvent(string category, string action, string label, int? value = null)
        {
            if (string.IsNullOrEmpty(category)) throw new ArgumentNullException("category");
            if (string.IsNullOrEmpty(action)) throw new ArgumentNullException("action");

            var values = DefaultValues;

            values.Add("t", HitType.@event.ToString());             // Event hit type
            values.Add("ec", category);                             // Event Category. Required.
            values.Add("ea", action);                               // Event Action. Required.
            if (label != null) values.Add("el", label);             // Event label.
            if (value != null) values.Add("ev", value.ToString());  // Event value.

            Track(values);
        }

        public void TrackPageview(string hostname, string page, string title)
        {
            if (string.IsNullOrEmpty(hostname)) throw new ArgumentNullException("hostname");
            if (string.IsNullOrEmpty(page)) throw new ArgumentNullException("action");

            var values = DefaultValues;

            values.Add("t", HitType.@pageview.ToString());          // Pageview hit type
            values.Add("dh", hostname);                             // Document hostname. Required.
            values.Add("dp", page);                                 // Page. Required.
            if (title != null) values.Add("dt", title);             // Title. Required.

            Track(values);
        }

        private void Track(Dictionary<string, string> values)
        {
            var request = (HttpWebRequest)WebRequest.Create(endpoint);
            request.Method = "POST";
            request.KeepAlive = false;

            var postDataString = values
                .Aggregate("", (data, next) => string.Format("{0}&{1}={2}", data, next.Key,
                                                             HttpUtility.UrlEncode(next.Value)))
                .TrimEnd('&');

            // set the Content-Length header to the correct value
            request.ContentLength = Encoding.UTF8.GetByteCount(postDataString);

            // write the request body to the request
            using (var writer = new StreamWriter(request.GetRequestStream()))
            {
                writer.Write(postDataString);
            }

            try
            {
                // Send the response to the server
                var webResponse = (HttpWebResponse)request.GetResponse();

                if (webResponse.StatusCode != HttpStatusCode.OK)
                {
                    throw new HttpException((int)webResponse.StatusCode, "Google Analytics tracking did not return OK 200");
                }

                webResponse.Close();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        private enum HitType
        {
            // t is the HitType
            @event,     // Used for event tracking, measuring actions, combining impressions and actions, measuring refunds
            @pageview,  // Used for page tracking, measuring impressions, measuring purchases, measuring the checkout process
        }

        private Dictionary<string, string> DefaultValues
        {
            get
            {
                var data = new Dictionary<string, string>()
                {
                    { "v", "1" },                   // The protocol version. The value should be 1.
                    { "tid", googleTrackingID },    // Tracking ID / Web property / Property ID.
                    { "cid", googleClientID }       // Anonymous Client ID (must be unique).
                };

                return data;
            }
        }
    }
}
