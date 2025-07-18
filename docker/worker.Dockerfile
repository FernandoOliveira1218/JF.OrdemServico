# Usar SDK para build
FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
WORKDIR /app

# Copiar csproj e restaurar dependências
COPY *.sln .
COPY src/Services/JF.OrdemServico.Worker/*.csproj ./src/Services/JF.OrdemServico.Worker/
RUN dotnet restore src/Services/JF.OrdemServico.Worker/JF.OrdemServico.Worker.csproj

# Copiar todo o código e publicar
COPY . .
RUN dotnet publish src/Services/JF.OrdemServico.Worker/JF.OrdemServico.Worker.csproj -c Release -o /app/publish /p:UseAppHost=false

# Runtime
FROM mcr.microsoft.com/dotnet/aspnet:9.0
WORKDIR /app
COPY --from=build /app/publish .
ENTRYPOINT ["dotnet", "JF.OrdemServico.Worker.dll"]
