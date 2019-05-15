FROM mcr.microsoft.com/dotnet/core/aspnet:3.0-nanoserver-1803 AS base
WORKDIR /app
EXPOSE 99
EXPOSE 443
#COPY /bin/Debug/netcoreapp3.0 .
#COPY /app .

#ENTRYPOINT Braces.PluginHost.exe
ENTRYPOINT /bin/Debug/netcoreapp3.0/Braces.PluginHost.exe
