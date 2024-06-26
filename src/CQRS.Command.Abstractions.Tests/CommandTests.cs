using FluentAssertions;

namespace CQRS.Command.Abstractions.Tests;

public class UnitTest1
{
    [Fact]
    public void ShouldSetAndGetResult()
    {
        // Arrange
        var command = new TestCommand(42);

        // Act
        command.SetResult(42);

        // Assert
        Assert.Equal(42, command.GetResult());
    }

    [Fact]
    public void ShouldThrowExceptionWhenResultIsNotSet()
    {
        // Arrange  
        var command = new TestCommand(42);

        // Act
        Action act = () => command.GetResult();

        // Assert
        act.Should().Throw<InvalidOperationException>();
    }

    [Fact]
    public void ShouldSetHasResult()
    {
        var command = new TestCommand(42);

        command.HasResult().Should().BeFalse();

        command.SetResult(42);

        command.HasResult().Should().BeTrue();
    }
}

public record TestCommand(int Value) : Command<int>;
