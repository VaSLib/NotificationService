FROM mcr.microsoft.com/dotnet/aspnet:8.0-alpine3.18
EXPOSE 8080
WORKDIR /app
RUN apk update && apk add icu-libs
COPY bin/Debug/net8.0 .
ENTRYPOINT ["dotnet", "./NotificationService.dll"]