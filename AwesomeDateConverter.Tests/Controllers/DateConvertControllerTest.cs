using System.Web.Mvc;
using System;
using AwesomeDateConverter;
using AwesomeDateConverter.Controllers;
using NUnit.Framework;
using FluentAssertions;
using AwesomeDateConverter.Interfaces;
using System.Net.Http;
using System.Web.Http;
using AwesomeDateConverter.Models;

namespace AwesomeDateConverter.Tests.Controllers
{
    [TestFixture]
    public class DateConvertControllerTest
    {
        DateConvertController ctrl;

        [SetUp]
        public void Init()
        {
            ctrl = new DateConvertController();
            ctrl.Request = new HttpRequestMessage();
            ctrl.Configuration = new HttpConfiguration();
        }

        [Test]
        [TestCase("1989-01-28T01:28:00-07:00")]
        public void Post_ValidDateTime(string timeToConvert)
        {
            var requestedDate = new Date { posted = timeToConvert };
            var response = ctrl.Post(requestedDate)
                .ExecuteAsync(new System.Threading.CancellationToken())
                .Result;

            Date dateResp;
            Assert.IsTrue(response.TryGetContentValue<Date>(out dateResp));
            Assert.AreEqual("1989-01-28T01:28:00", dateResp.posted);
            Assert.AreEqual("1989-01-28T08:28:00Z", dateResp.converted);
        }

        [Test]
        [TestCase("0000-01-28T08:28:00.00Z")]
        public void Post_InValidDateTime(string timeToConvert)
        {
            var requestedDate = new Date { posted = timeToConvert };
            var response = ctrl.Post(requestedDate)
                .ExecuteAsync(new System.Threading.CancellationToken());

            Assert.IsFalse(response.Result.IsSuccessStatusCode);
        }
    }
}
