# Command Routing

Command Routing can be used to route HTTP requests pipelines of command handlers. By contrast, the standard MVC 6 routing mechanism routes requests to action methods that you defined on MVC Controller classes.

## How does it look?

Here's an example from the Sample project:

```csharp
RouteBuilder routeBuilder = new RouteBuilder {ServiceProvider = app.ApplicationServices};

CommandRouteBuilder commandRoutes = new CommandRouteBuilder(routeBuilder);
commandRoutes
    .Route("hello/{name:alpha}")
    .As<SayHelloRequest>()
    .To<IgnoreBob, SayHello>();

app.UseRouter(routeBuilder.Build());
```

That code snippet configures a route that maps all requests matching `"hello/{name:alpha}"` to a route handler which will create an instance  `SayHelloRequest` from the HTTP request and then dispatch that *Command/Request* to a  *Command Pipeline* consisting of two *Command Handlers* (`IgnoreBob`, `SayHello`).

That's quite a mouthful... so take a look at the [Getting Started](http://commandrouting.readthedocs.org/en/latest/Getting-Started/) documentation if you want to learn more about *Commands*, *Requests* and *Command Handlers*.
