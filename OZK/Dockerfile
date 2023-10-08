#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:5.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /src
COPY ["OZK/OZK.csproj", "OZK/"]
COPY ["OZK.CQRS/OZK.CQRS.csproj", "OZK.CQRS/"]
COPY ["OZK.Datalayer/OZK.Datalayer.csproj", "OZK.Datalayer/"]
RUN dotnet restore "OZK/OZK.csproj"
COPY . .
WORKDIR "/src/OZK"
RUN dotnet build "OZK.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "OZK.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "OZK.dll"]