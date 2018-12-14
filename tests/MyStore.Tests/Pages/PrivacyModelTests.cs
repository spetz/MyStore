using System;
using FluentAssertions;
using MyStore.Web.Pages;
using Xunit;

namespace MyStore.Tests.Pages
{
    public class PrivacyModelTests
    {
        [Fact]
        public void on_get_should_always_fail()
        {
            var pageModel = new PrivacyModel();

            Action onGet = () => pageModel.OnGet();

            onGet.Should().Throw<ArgumentException>()
                .And.Message.Should().Be("Ooopsss...");
        }
    }
}