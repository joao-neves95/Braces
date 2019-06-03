FROM mcr.microsoft.com/dotnet/core/aspnet:3.0-nanoserver-1803 AS base
WORKDIR /app
EXPOSE 99
EXPOSE 443
COPY . .
# WORKDIR ./Braces.PluginHost/bin/debug/netcoreapp3.0
# RUN dir .
# WORKDIR /app

ENTRYPOINT [ "Braces.PluginHost/bin/debug/netcoreapp3.0/Braces.PluginHost.exe" ] 
