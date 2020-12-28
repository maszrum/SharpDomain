# SharpDomain

## Solution directories structure

- YourApp.Core
  - Models
    - FirstModel.cs
    - SecondModel.cs
    - ...
  - ValueObjects
    - FirstValueObject.cs
    - SecondValueObject.cs
    - ...
  - Events
    - FirstEvent.cs
    - SecondEvent.cs
    - ...
  - Exceptions
    - FirstDomainException.cs
    - SecondDomainException.cs
    - ...
  - EventHandlers
    - SomeActionOnEventReceived.cs
    - AnotherActionOnAnotherEventReceived.cs
    - ...
  - InfrastructureAbstractions
    - IFirstRepository.cs
    - ISecondRepository.cs
    - ...
- YourApp.Application
  - Commands
    - FirstCommand.cs
    - FirstCommandHandler.cs
    - SecondCommand.cs
    - SecondCommandHandler.cs
    - ...
  - Queries
    - FirstQuery.cs
    - FirstQueryHandler.cs
    - SecondQuery.cs
    - SecondQueryHandler.cs
    - ...
  - ViewModels
    - FirstViewModel.cs
    - SecondViewModel.cs
    - ...
- YourApp.Infrastructure
  - Repositories
    - FirstRepository.cs
    - SecondRepository.cs
  - EventHandlers
    - FirstModelCreatedHandler.cs
    - FirstModelChangedHandler.cs
    - ...
  - (depends on the instrastructure libraries)

## YourApp.Core

### Models

```csharp
public class ExampleModel : AggregateRoot // or Aggregate if not root
{
    public ExampleModel(
        Guid id,
        int intProperty,
		string stringProperty,
		List<ChildModel> children)
    {
        Id = id;
        IntProperty = intProperty;
        StringProperty = stringProperty;
        _children = children;
    }
    
    public override Guid Id { get; }
    
    // a property that can only be changed by a method 
    public int IntProperty { get; private set; }
    public const int MaxIntPropertyValue = 10;
    
    // a property that can be changed with setter
    public string StringProperty { get; set; }
	
	// collection of child models
    private readonly List<ChildModel> _children;
    public IReadOnlyList<ChildModel> Children => _children;
    
    public void IncrementIntProperty()
    {
        if (IntProperty >= MaxIntPropertyValue)
        {
            throw new MaxIntPropertyReachedException(IntProperty);
        }
        
        var changedEvent = this.CaptureChangedEvent(
            model => model.IntProperty++);
        
        var incrementedEvent = new IntPropertyIncremented(Id, intProperty);
        
        Events.Add(
            changedEvent, 
            incrementedEvent);
    }
	
    public static ExampleModel Create(string stringProperty, List<ChildModel> children)
    {
        var modelId = Guid.NewGuid();
        
        var model = new ExampleModel(modelId, 0, stringProperty, children);
        
        var createdEvent = new ExampleModelCreated(modelId);
        model.Events.Add(createdEvent);
		
        return model;
    }
}
```

### Value objects

TODO

### Events

TODO

### Exceptions

TODO

### Event handlers

TODO

### Infrastructure abstractions

TODO

## YourApp.Application

### Commands

TODO

### Queries

TODO

### View models

TODO

## YourApp.Infrastructure

### Repositories

TODO

### Event handlers

TODO

## Wiring up layers

```csharp
var domainAssembly = typeof(ExampleModel).Assembly;
var applicationAssembly = typeof(CreateExampleModel).Assembly;
var persistenceAssembly = typeof(ExampleModelRepository).Assembly;

var containerBuilder = new ContainerBuilder()
    .RegisterDomainLayer(domainAssembly)
    .RegisterApplicationLayer(
        assembly: applicationAssembly,
        configurationAction: config =>
        {
            config.ForbidMediatorInHandlers = true;
            config.ForbidWriteRepositoriesInHandlersExceptIn(persistenceAssembly);
        })
    .RegisterPersistenceLayer(persistenceAssembly);
```

# Add-ins

## SharpDomain.AccessControl

TODO

## SharpDomain.AutoMapper

TODO

## SharpDomain.AutoTransaction

TODO

## SharpDomain.FluentValidation

TODO

# Todo list:
- write a decent readme,
- add EF Core, Dapper integration,
- make it modular monolith?