FROM mcr.microsoft.com/dotnet/aspnet:8.0
COPY bin/Release/net8.0/ API/
WORKDIR /API
ENV ASPNETCORE_ENVIRONMENT=Prod
ENV ASPNETCORE_URLS=http://+:8082
ENTRYPOINT ["dotnet", "ITM.Service.API.dll"]