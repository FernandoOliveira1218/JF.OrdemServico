# Etapa base com ASP.NET
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app

EXPOSE 80
EXPOSE 443

# Copia certificado HTTPS
COPY ./https/aspnetapp.pfx /https/aspnetapp.pfx

# Etapa de build com SDK
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

# Copia o .csproj e restaura
COPY ../src/services/JF.OrdemServico.API/JF.OrdemServico.API.csproj ./JF.OrdemServico.API/
RUN dotnet restore ./JF.OrdemServico.API/JF.OrdemServico.API.csproj

# Copia o restante do código e publica
COPY ../src/services/JF.OrdemServico.API ./JF.OrdemServico.API
WORKDIR /src/JF.OrdemServico.API
RUN dotnet publish -c Release -o /app/publish

# Imagem final
FROM base AS final
WORKDIR /app

COPY --from=build /app/publish .

ENV ASPNETCORE_URLS="https://+:443;http://+:80"
ENV ASPNETCORE_Kestrel__Certificates__Default__Path=/https/aspnetapp.pfx
ENV ASPNETCORE_Kestrel__Certificates__Default__Password=123456

ENTRYPOINT ["dotnet", "JF.OrdemServico.API.dll"]
