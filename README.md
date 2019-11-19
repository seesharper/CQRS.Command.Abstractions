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



