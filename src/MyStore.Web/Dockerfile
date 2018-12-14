FROM microsoft/dotnet:2.2-aspnetcore-runtime
WORKDIR /app
COPY ./bin/docker .
EXPOSE 5000
ENV ASPNETCORE_URLS http://*:5000
ENV ASPNETCORE_ENVIRONMENT docker
ENTRYPOINT dotnet MyStore.Web.dll