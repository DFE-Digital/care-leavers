using CareLeavers.Web.Controllers;
using Microsoft.AspNetCore.Mvc;
using CareLeavers.Web.Configuration;

using NSubstitute;
using System;
using System.Collections.Generic;
using System.Text;

namespace CareLeavers.Web.Tests.Controllers
{
    public class SecurityTextControllerTests
    {
        private SecurityTextController _controller;
  
        [SetUp]
        public void Init()
        {
            _controller = new SecurityTextController();
        }
        
        [Test]
        public void GetSecurityText_NoUrl_Returns404NotFound()
        {
            SiteConfiguration.SecurityTxtUrl = string.Empty;

            var result = _controller.GetSecurityText();
            Assert.That(result, Is.TypeOf<NotFoundObjectResult>());
                
            var resultType = result as NotFoundObjectResult;
            using (Assert.EnterMultipleScope())
            {
                Assert.That(resultType, Is.Not.Null);
                Assert.That(resultType?.StatusCode, Is.EqualTo(404));
                Assert.That(resultType?.Value, Is.EqualTo("Security file was not found"));
            };
          
        }

        [Test]
        public void GetSecurityText_GotUrl_RedirectsToUrl()
        {
            SiteConfiguration.SecurityTxtUrl = "https://www.google.com";

            var result = _controller.GetSecurityText();
            Assert.That(result, Is.TypeOf<RedirectResult>());

            var resultType = result as RedirectResult;
            using (Assert.EnterMultipleScope())
            {
                Assert.That(resultType, Is.Not.Null);
                Assert.That(resultType?.Url, Is.EqualTo("https://www.google.com"));
            };

        }

        [TearDown]
        public void Teardown()
        {
            _controller.Dispose();
        }
    }
}