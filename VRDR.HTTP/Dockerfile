FROM mcr.microsoft.com/dotnet/core/sdk:3.1 AS build-env
COPY . /app
WORKDIR /app/VRDR.HTTP
RUN dotnet publish -c Release -r linux-x64 -o out

FROM mcr.microsoft.com/dotnet/core/runtime:3.1 AS runtime
WORKDIR /app

COPY --from=build-env /app/VRDR.HTTP/out .
EXPOSE 8080
ENTRYPOINT ["./FhirDeathRecord.HTTP"]
