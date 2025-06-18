# Fase base com o runtime
FROM mcr.microsoft.com/dotnet/aspnet:9.0-preview AS base
WORKDIR /app
EXPOSE 80

# Fase de build com o SDK
FROM mcr.microsoft.com/dotnet/sdk:9.0-preview AS build
WORKDIR /src
COPY . .
RUN dotnet restore
RUN dotnet publish -c Release -o /app/publish

# Fase final (imagem enxuta só com runtime + app compilado)
FROM base AS final
WORKDIR /app
COPY --from=build /app/publish .
ENTRYPOINT ["dotnet", "e-commerce.dll"]