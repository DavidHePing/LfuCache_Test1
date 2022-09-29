using FluentAssertions;
using LfuCache_Test1;

namespace TestProject1;

public class Tests
{
    [Test]
    public void leetCode_Test_Case1()
    {
        var lfuCache = new LfuCache(2);
        lfuCache.Put("1", "1");
        lfuCache.Put("2", "2");
        
        var result = lfuCache.Get("1");
        result.Should().Be("1");
        
        lfuCache.Put("3", "3");
        
        result = lfuCache.Get("2");
        result.Should().Be(null);
        result = lfuCache.Get("3");
        result.Should().Be("3");
        
        lfuCache.Put("4", "4");
        
        result = lfuCache.Get("1");
        result.Should().Be(null);
        result = lfuCache.Get("3");
        result.Should().Be("3");
        result = lfuCache.Get("4");
        result.Should().Be("4");
    }
    
    [Test]
    public void leetCode_Test_Case2()
    {
        var lfuCache = new LfuCache(2);
        lfuCache.Put("0", "0");
        
        var result = lfuCache.Get("0");
        result.Should().Be("0");
    }
    
    [Test]
    public void leetCode_Test_Case3()
    {
        var lfuCache = new LfuCache(3);
        lfuCache.Put("1", "1");
        lfuCache.Put("2", "2");
        lfuCache.Put("3", "3");
        lfuCache.Put("4", "4");
        
        var result = lfuCache.Get("4");
        result.Should().Be("4");
        result = lfuCache.Get("3");
        result.Should().Be("3");
        result = lfuCache.Get("2");
        result.Should().Be("2");
        result = lfuCache.Get("1");
        result.Should().Be(null);
        
        lfuCache.Put("5", "5");
        
        result = lfuCache.Get("1");
        result.Should().Be(null);
        result = lfuCache.Get("2");
        result.Should().Be("2");
        result = lfuCache.Get("3");
        result.Should().Be("3");
        result = lfuCache.Get("4");
        result.Should().Be(null);
        result = lfuCache.Get("5");
        result.Should().Be("5");
    }
}