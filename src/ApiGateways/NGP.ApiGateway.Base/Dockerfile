FROM mcr.microsoft.com/dotnet/core/aspnet:2.2-stretch-slim AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/core/sdk:2.2-stretch AS build
WORKDIR /src
COPY ["src/ApiGateways/NGP.ApiGateway.Base/NGP.ApiGateway.Base.csproj", "src/ApiGateways/NGP.ApiGateway.Base/"]
COPY ["src/Frameworks/NGP.Framework.Core/NGP.Framework.Core.csproj", "src/Frameworks/NGP.Framework.Core/"]

RUN dotnet restore "src/ApiGateways/NGP.ApiGateway.Base/NGP.ApiGateway.Base.csproj"
COPY . .
WORKDIR "/src/src/ApiGateways/NGP.ApiGateway.Base"
RUN dotnet build "NGP.ApiGateway.Base.csproj" -c Release -o /app

FROM build AS publish
RUN dotnet publish "NGP.ApiGateway.Base.csproj" -c Release -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
COPY src/SharedConfiguration/log4net.config ./
ENTRYPOINT ["dotnet", "NGP.ApiGateway.Base.dll"]