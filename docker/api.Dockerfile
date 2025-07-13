FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
COPY ./src/Services/JF.OrdemServico.API ./
EXPOSE 5000
ENTRYPOINT ["dotnet", "JF.OrdemServico.API.dll"]