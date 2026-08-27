# Etapa de compilação
FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
WORKDIR /src
COPY ["ZeloApp.csproj", "./"]
RUN dotnet restore "ZeloApp.csproj"
COPY . .
RUN dotnet publish "ZeloApp.csproj" -c Release -o /app/publish

# Etapa de execução no Render
FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS base
WORKDIR /app
EXPOSE 8080

# Desativa o monitor de arquivos via inotify do Linux para não estourar o limite da Render
ENV DOTNET_UsePollForFileChanges=1
ENV DOTNET_HOSTBUILDER__RELOADCONFIGONCHANGE=false
ENV ASPNETCORE_ENVIRONMENT=Production

COPY --from=build /app/publish .
ENTRYPOINT ["dotnet", "ZeloApp.dll"]