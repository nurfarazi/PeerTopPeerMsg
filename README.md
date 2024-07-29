
To run a .NET app from the console, you can follow these steps:

1. Open a command prompt or terminal.

For example, if your project file is named `MyApp.csproj`, you would run the following command:

```
dotnet run 5000
```

This will build and run your .NET app, and you should see the output in the console.

Note: Make sure you have the .NET SDK installed on your machine before running the `dotnet` command. You can download it from the official .NET website.

**TODO: Enhance Peer Discovery**
Consider using protocols like mDNS or a DHT for more robust peer discovery. These protocols can help improve the efficiency and reliability of discovering peers in your application.

**TODO: Improve Message Routing**
Implement a more sophisticated routing protocol to handle complex topologies. This will ensure that messages are delivered efficiently and reliably, even in scenarios with multiple hops or dynamic network conditions.

**TODO: Add Security**
Implement encryption and authentication mechanisms to ensure secure communication between peers. This will help protect sensitive data and prevent unauthorized access to your application.

**TODO: Build a GUI**
Create a graphical user interface using a framework like WPF or MAUI. This will provide a more intuitive and visually appealing user experience, making your application more user-friendly.

**TODO: Handle Message Persistence**
Consider using a database like LiteDB to store messages locally. This will allow you to persist messages across sessions and provide reliable message retrieval and management capabilities.
