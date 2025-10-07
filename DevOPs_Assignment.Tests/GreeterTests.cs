using DevOPs_Assignment;
using Xunit;

namespace DevOPs_Assignment.Tests;

public class GreeterTests
{
    [Fact]
    public void GetMessage_ReturnsExpectedString()
    {
        var msg = Greeter.GetMessage();
        Assert.Equal("Hello, DevOps Assignment!", msg);
    }
}

