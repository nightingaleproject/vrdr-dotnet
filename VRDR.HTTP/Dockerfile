FROM microsoft/dotnet:sdk AS build-env
WORKDIR /app
COPY bin/Debug/netcoreapp3.1/publish .
EXPOSE 8080
ENTRYPOINT ["dotnet", "VRDR.HTTP.dll"]
