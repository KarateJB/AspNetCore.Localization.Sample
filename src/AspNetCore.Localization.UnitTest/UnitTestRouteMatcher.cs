using AspNetCore.Localization.WebApi.Utils;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace AspNetCore.Localization.UnitTest;

[TestClass]
public class UnitTestRouteMatcher
{
    [TestMethod]
    public void TestMatches()
    {
        using var routeMatcher = new RouteMatcher();
        const string expected = "zh-CN";
        const string template = "api/Locale/Get/{locale}";
        var routeValues = routeMatcher.Matches(template, $"/api/Locale/Get/{expected}");
        routeValues.TryGetValue("locale", out var actual);

        Assert.AreEqual(expected, actual);
    }
}
