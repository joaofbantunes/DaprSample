# DaprSample

Very basic sample using [Dapr](https://dapr.io).

`ToDo.Web` is an API that allows the management of TODOs, which are persisted using Dapr's APIs.

`ToDo.EventListener` subscribes to events published by `ToDo.Web`, again done through Dapr's APIs.

To run `ToDo.Web`, in its folder, execute:

```sh
dapr run --app-id todo-web --app-port 5000 dotnet run -- --environment Development
```

To run `ToDo.EventListener`, in its folder, execute:

```sh
dapr run --app-id todo-events --app-port 5002 dotnet run -- --environment Development
```

Pre-requisites:

- Docker - Dapr will run some auxiliary containers, like Redis which is used as the default state store and pub/sub provider
- Install and init Dapr (instructions in Dapr's site)
