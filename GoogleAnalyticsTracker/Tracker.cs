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

        public void InitializeValues(string type)
        {

        }

        public string TrackEvent(string category, string action, string label, int? value = null)
        {
            if (string.IsNullOrEmpty(category)) throw new ArgumentNullException("category");
            if (string.IsNullOrEmpty(action)) throw new ArgumentNullException("action");

            var values = DefaultValues;

            values.Add("t", HitType.@event.ToString());                                 // Event hit type
            values.Add("ec", category);                                                 // Event Category. Required.
            values.Add("ea", action);                                                   // Event Action. Required.
            if (!string.IsNullOrWhiteSpace(label)) values.Add("el", label);              // Event label.
            if (value.HasValue) values.Add("ev", value.ToString());                      // Event value.

            return GetUrl(values);
        }

        public string TrackPageview(string hostname, string page, string title)
        {
            var values = DefaultValues;

            values.Add("t", HitType.@pageview.ToString());                              // Pageview hit type
            values.Add("dh", hostname);                                                 // Document hostname.
            values.Add("dp", page);                                                     // Page.
            if (!string.IsNullOrWhiteSpace(title)) values.Add("dt", title);              // Title.

            return GetUrl(values);
        }

        public string TrackTransaction(string id, string affiliation, string revenue, string shipping, string tax, string code)
        {
            if (string.IsNullOrEmpty(id)) throw new ArgumentNullException("id");

            var values = DefaultValues;

            values.Add("t", HitType.@transaction.ToString());                           // Transaction hit type
            values.Add("ti", id);                                                       // Transaction ID. Required.
            values.Add("ta", affiliation);                                              // Transaction affiliation.
            values.Add("tr", revenue);                                                  // Transaction revenue.
            values.Add("ts", shipping);                                                 // Transaction shipping.
            values.Add("tt", tax);                                                      // Transaction tax.
            if (!string.IsNullOrWhiteSpace(code)) values.Add("cu", code);                // Currency Code.

            return GetUrl(values);
        }

        public string TrackTransactionItem(string id, string name, string price, string quantity, string sku, string variation, string code)
        {
            if (string.IsNullOrEmpty(id)) throw new ArgumentNullException("id");
            if (string.IsNullOrEmpty(name)) throw new ArgumentNullException("name");

            var values = DefaultValues;

            values.Add("t", HitType.@item.ToString());                                  // Item hit type
            values.Add("ti", id);                                                       // Item ID. Required.
            values.Add("in", name);                                                     // Item name. Required.
            values.Add("ip", price);                                                    // Item price.
            values.Add("iq", quantity);                                                 // Item quantity.
            values.Add("ic", sku);                                                      // Item code/SKU.
            values.Add("iv", variation);                                                // Item variation/category.
            if (!string.IsNullOrWhiteSpace(code)) values.Add("cu", code);                // Currency Code.

            return GetUrl(values);
        }

        public string TrackSocial(string action, string network, string target)
        {
            if (string.IsNullOrEmpty(action)) throw new ArgumentNullException("action");
            if (string.IsNullOrEmpty(network)) throw new ArgumentNullException("network");
            if (string.IsNullOrEmpty(target)) throw new ArgumentNullException("target");

            var values = DefaultValues;

            values.Add("t", HitType.@social.ToString());                                // Social hit type
            values.Add("sa", action);                                                   // Social Action. Required.
            values.Add("sn", network);                                                  // Social Network. Required.
            if (!string.IsNullOrWhiteSpace(target)) values.Add("st", target);            // Social Target. Required.

            return GetUrl(values);
        }

        public string TrackException(string description, string isFatal)
        {
            var values = DefaultValues;

            values.Add("t", HitType.@exception.ToString());                                 // Exception hit type
            if (!string.IsNullOrWhiteSpace(description)) values.Add("exd", description);     // Exception description
            if (isFatal != null) values.Add("exf", isFatal);                                // Exception is fatal?

            return GetUrl(values);
        }

        public string TrackUserTiming(string category, string variable, string time, string label, string dns, string pageDLtime, string redirectTime, string tcpConnectTime, string serverResponseTime)
        {
            var values = DefaultValues;

            values.Add("t", HitType.@timing.ToString());                                                    // Timing hit type
            values.Add("utc", category);                                                                    // Timing category.
            values.Add("utv", variable);                                                                    // Timing variable.
            values.Add("utt", time);                                                                        // Timing time.
            values.Add("utl", label);                                                                       // Timing label.

            if (!string.IsNullOrWhiteSpace(dns)) values.Add("dns", dns);                                     // DNS load time.
            if (!string.IsNullOrWhiteSpace(pageDLtime)) values.Add("pdt", pageDLtime);                       // Page download time.
            if (!string.IsNullOrWhiteSpace(redirectTime)) values.Add("rrt", redirectTime);                   // Redirect time.
            if (!string.IsNullOrWhiteSpace(tcpConnectTime)) values.Add("tcp", tcpConnectTime);               // TCP connect time.
            if (!string.IsNullOrWhiteSpace(serverResponseTime)) values.Add("srt", serverResponseTime);       // Server response time.

            return GetUrl(values);
        }

        public string TrackScreenview(string name, string version, string id, string installerId, string description)
        {
            var values = DefaultValues;

            values.Add("t", HitType.@screenview.ToString());                            // Screenview hit type
            values.Add("an", name);                                                     // App name.
            values.Add("av", version);                                                  // App version.
            values.Add("aid", id);                                                      // App Id.
            values.Add("aiid", installerId);                                            // App Installer Id.
            values.Add("cd", description);                                              // Screen name/content description.

            return GetUrl(values);
        }

        private string GetUrl(Dictionary<string, string> values)
        {
            var url = values
                .Aggregate("", (data, next) => string.Format("{0}&{1}={2}", data, next.Key, HttpUtility.UrlEncode(next.Value)))
                .TrimEnd('&');

            return url;
        }

        private void Track(string url)
        {
            var request = (HttpWebRequest)WebRequest.Create(endpoint);
            request.Method = "POST";
            request.KeepAlive = false;

            // set the Content-Length header to the correct value
            request.ContentLength = Encoding.UTF8.GetByteCount(url);

            // write the request body to the request
            using (var writer = new StreamWriter(request.GetRequestStream()))
            {
                writer.Write(url);
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
            @event,
            @pageview,
            @transaction,
            @item,
            @social,
            @exception,
            @timing,
            @screenview,
        }

        private Dictionary<string, string> DefaultValues
        {
            get
            {
                var data = new Dictionary<string, string>()
                {
                    { "v", "1" },                                                       // The protocol version. The value should be 1.
                    { "tid", googleTrackingID },                                        // Tracking ID / Web property / Property ID.
                    { "cid", googleClientID }                                           // Anonymous Client ID (must be unique).
                };

                return data;
            }
        }
    }
}
