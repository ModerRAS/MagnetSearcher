FROM mcr.microsoft.com/dotnet/runtime:6.0

WORKDIR /app

COPY ./app/out /app

ENTRYPOINT ["dotnet", "MagnetSearcher.dll"]