using System;
using System.Collections.Generic;
using System.Net;
using NUnit.Framework;
using PayPal.Exception;
using PayPal.Manager;

namespace PayPal.UnitTest.Manager
{
    [TestFixture]
    public class ConnectionManagerTest
    {
        ConnectionManager connectionMngr;
        HttpWebRequest httpRequest;

        [Test]
        public void CreateNewConnection()
        {
            connectionMngr = ConnectionManager.Instance;
            ConfigManager configMngr = ConfigManager.Instance;
            httpRequest = connectionMngr.GetConnection("http://paypal.com/");
            Assert.IsNotNull(httpRequest);
            Assert.AreEqual("http://paypal.com/", httpRequest.RequestUri.AbsoluteUri);
            Assert.AreEqual(configMngr.GetProperty("connectionTimeout"), httpRequest.Timeout.ToString());
        }

        [Test, ExpectedException(typeof(ConfigException))]
        public void CreateNewConnectionWithInvalidURL()
        {
            connectionMngr = ConnectionManager.Instance;
            httpRequest = connectionMngr.GetConnection("Not a url");
        }
    }
}
