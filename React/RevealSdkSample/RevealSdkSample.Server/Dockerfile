FROM mcr.microsoft.com/dotnet/sdk:5.0 AS base
WORKDIR /app

#Was not able to build in a container, because of the private nuget is not working in netcore 2
FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /src
#COPY [".template/NuGetCacheRefreshService.csproj", ".template/nuget.config", "./"]

COPY RevealSdkSample.Server.csproj /src
COPY nuget.config /src
RUN  mkdir /src/nuget

RUN curl -sL https://deb.nodesource.com/setup_10.x |  bash -
RUN apt-get install -y nodejs

# ENV DOTNET_SYSTEM_NET_HTTP_USESOCKETSHTTPHANDLER=0
# ENV http_proxy=http://172.17.0.1:3128 \
#     https_proxy=http://172.17.0.1:3128 \
#     ftp_proxy=http://172.17.0.1:3128

#This is intentionally on a separate layer, so it can be reused between all images
RUN dotnet restore "RevealSdkSample.Server.csproj"

COPY . .
WORKDIR /src/.
RUN npm install
#RUN dotnet restore "ReportPlus.Web/ReportPlus.Web.NetCore.csproj"
RUN dotnet build "RevealSdkSample.Server.csproj" -c release -f net5.0 -o /app

#This combination is recomended by MS to produce the smallest image. TODO: Understand why build and publish are separate
FROM build AS publish
RUN dotnet publish "RevealSdkSample.Server.csproj" -c release -o /app --no-restore

FROM base AS final
WORKDIR /app
COPY --from=publish /app .

EXPOSE 8080
ENV ASPNETCORE_URLS=http://*:8080

ENV ASPNETCORE_ENVIRONMENT=NetCore
ENTRYPOINT ["dotnet", "RevealSdkSample.Server.dll"]
