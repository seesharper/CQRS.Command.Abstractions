### CQRS.Command.Abstractions

This repo contains the ONLY the abstractions/interfaces for implementing the command part of the [CQRS](https://martinfowler.com/bliki/CQRS.html) pattern on the .Net Platform.

A command handler represents something that alters the state of the system such as updating data in a database.

The `ICommandHandler<TCommand>` interface is implemented by all command handlers.

```c#
public interface ICommandHandler<in TCommand>
{
    Task HandleAsync(TCommand command, CancellationToken cancellationToken = default);
}
```

The `ICommandExecutor` interface can optionally be implemented to execute command handlers. 

```c#
public interface ICommandExecutor
{
    Task ExecuteAsync<TCommand>(TCommand command, CancellationToken cancellationToken = default);
}
```

Typically we use dependency injection to inject either an `ICommandHandler<TCommand>` or an `ICommandExecutor`.

Please refer to [CQRS.LightInject](https://github.com/seesharper/CQRS.LightInject) and/or [CQRS.Microsoft.Extensions.DependencyInjection](https://github.com/seesharper/CQRS.Microsoft.Extensions.DependencyInjection) for examples of registration of command handlers. 

### Command<TResult>

In general we say that command don't have results, but in some cases it is needed even for commands to return something.
An example could be if the primary key is automatically generated and we want to return the id for the new row in the database.

To generalize around this scenario we can inherit from `Command<TResult>` which provide the `SetResult` method for setting the result and the `GetResult` method for getting the value.

The `GetResult` method will throw an exception if the `SetResult` method is not called prior the the `GetResult` method.

```c#
public record InsertCustomerCommand(string Name) : Command<int>
```

The command handler looking something like this

```c#
public class InsertCustomer : ICommandHandler<InsertCustomerCommand>
{
    public async Task HandleAsync(InsertCustomerCommand command, CancellationToken cancellationToken = default)
    {
        // Insert the customer into the database and get the CustomerId
        command.SetResult(CustomerId)
    }
}
```