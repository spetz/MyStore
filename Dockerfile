FROM microsoft/dotnet:2.2-sdk AS build
WORKDIR /app
COPY . .
RUN dotnet publish src/MyStore.Web -c release -o out

FROM microsoft/dotnet:2.2-aspnetcore-runtime
WORKDIR /app
COPY --from=build /app/src/MyStore.Web/out .
EXPOSE 5000
ENV ASPNETCORE_URLS http://*:5000
ENV ASPNETCORE_ENVIRONMENT docker
ENTRYPOINT dotnet MyStore.Web.dll