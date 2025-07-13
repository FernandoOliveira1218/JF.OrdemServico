FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
COPY ./src/Services/JF.OrdemServico.Worker ./
ENTRYPOINT ["dotnet", "JF.OrdemServico.Worker.dll"]