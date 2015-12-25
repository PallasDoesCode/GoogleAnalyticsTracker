using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.IO;
using System.Net;

namespace GoogleAnalyticsTracker.Test
{
    [TestClass]
    public class TrackerTests
    {
        private readonly string url = "http://www.google-analytics.com/collect";

        private readonly Mock<IWebRequestCreate> _MockWebRequestCreate = new Mock<IWebRequestCreate>();
        private readonly Mock<HttpWebRequest> _MockWebRequest = new Mock<HttpWebRequest>();
        private readonly Mock<Stream> _MockRequestStream = new Mock<Stream>();
        private readonly Mock<HttpWebResponse> _MockResponse = new Mock<HttpWebResponse>();

        /// <summary>
        /// Sets up mock objects
        /// </summary>
        [TestInitialize]
        public void Initialize()
        {
            this._MockWebRequestCreate
                .Setup(s => s.Create(It.IsAny<Uri>()))
                .Returns(this._MockWebRequest.Object);

            this._MockWebRequest
                .Setup(s => s.GetRequestStream())
                .Returns(this._MockRequestStream.Object);

            this._MockRequestStream
                .Setup(s => s.CanWrite)
                .Returns(true);

            this._MockWebRequest
              .Setup(s => s.GetResponse())
              .Returns(this._MockResponse.Object);

            this._MockResponse
                .Setup(s => s.StatusCode)
                .Returns(HttpStatusCode.OK);
        }

        [TestMethod]
        public void TestTrackMethod()
        {
            // Arrange
            var request = (HttpWebRequest)this._MockWebRequestCreate.Object.Create(new Uri(url));
            request.Method = WebRequestMethods.Http.Post;

            // Act
            var requestStream = request.GetRequestStream();

            using (var writer = new StreamWriter(requestStream))
            {
                writer.Write("v=1&t=event&tid=UA-70051512-1&cid=729&ec=Category&ea=Action&el=Label");
            }

            // Assert
            using (var response = (HttpWebResponse)request.GetResponse())
            {
                Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
            }
        }
    }
}
