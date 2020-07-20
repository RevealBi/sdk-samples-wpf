# Reveal Sdk sample for React

A sample embedding Reveal Sdk client into React application

## Building the server

To build and run the server you could execute the following commands:

```
cd RevealSdkSample\RevealSdkSample.Server
dotnet build
dotnet run
```

If everything went smooth you should see something like:

```
info: Microsoft.Hosting.Lifetime[0]
      Now listening on: http://localhost:8080
info: Microsoft.Hosting.Lifetime[0]
      Application started. Press Ctrl+C to shut down.
info: Microsoft.Hosting.Lifetime[0]
      Hosting environment: Development
info: Microsoft.Hosting.Lifetime[0]
      Content root path: C:\Work\EM\Samples-Sdk\RevealSdkSample\RevealSdkSample.Server
```

## Building and running docker image for the server

```
cd RevealSdkSample\RevealSdkSample.Server
docker build -t revealserver .
docker run -dp 8080:8080 revealserver
```
