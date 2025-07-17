FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

COPY https/aspnetapp.pfx https/aspnetapp.pfx

FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
WORKDIR /JF.OrdemServico

# Copia os arquivos de solução e todos os projetos
COPY JF.OrdemServico.sln .
COPY . .

# Restaura a solução inteira (isso garante restaurar todos os projetos)
RUN dotnet restore

WORKDIR /JF.OrdemServico/src/Services/JF.OrdemServico.API
RUN dotnet publish -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=build /app/publish .

#ENV ASPNETCORE_URLS="http://+:80"
ENV ASPNETCORE_URLS="https://+:443;http://+:80"
ENV ASPNETCORE_Kestrel__Certificates__Default__Path=https/aspnetapp.pfx
ENV ASPNETCORE_Kestrel__Certificates__Default__Password=123456

ENTRYPOINT ["dotnet", "JF.OrdemServico.API.dll"]
