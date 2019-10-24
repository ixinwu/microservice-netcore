FROM mcr.microsoft.com/dotnet/core/aspnet:2.2-stretch-slim AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/core/sdk:2.2-stretch AS build
WORKDIR /src
COPY ["src/Services/Identity/NGP.Identity.Api/NGP.Identity.Api.csproj", "src/Services/Identity/NGP.Identity.Api/"]
COPY ["src/Frameworks/NGP.Framework.Core/NGP.Framework.Core.csproj", "src/Frameworks/NGP.Framework.Core/"]

RUN dotnet restore "src/Services/Identity/NGP.Identity.Api/NGP.Identity.Api.csproj"
COPY . .
WORKDIR "/src/src/Services/Identity/NGP.Identity.Api"
RUN dotnet build "NGP.Identity.Api.csproj" -c Release -o /app

FROM build AS publish
RUN dotnet publish "NGP.Identity.Api.csproj" -c Release -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
COPY src/SharedConfiguration/log4net.config ./
ENTRYPOINT ["dotnet", "NGP.Identity.Api.dll"]