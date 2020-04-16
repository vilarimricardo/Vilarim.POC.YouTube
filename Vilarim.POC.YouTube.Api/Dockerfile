FROM mcr.microsoft.com/dotnet/core/aspnet:3.1 AS base
WORKDIR /app
EXPOSE 80
#EXPOSE 443

FROM mcr.microsoft.com/dotnet/core/sdk:3.1 AS build
WORKDIR /src
COPY ["Vilarim.POC.YouTube.Api/Vilarim.POC.YouTube.Api.csproj", "Vilarim.POC.YouTube.Api/"]
RUN dotnet restore "Vilarim.POC.YouTube.Api/Vilarim.POC.YouTube.Api.csproj"
COPY . .
WORKDIR "/src/Vilarim.POC.YouTube.Api"
RUN dotnet build "Vilarim.POC.YouTube.Api.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Vilarim.POC.YouTube.Api.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Vilarim.POC.YouTube.Api.dll"]
