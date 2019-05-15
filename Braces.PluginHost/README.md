- Switch docker to Windows containers.

docker build -t braces.plugin-host .
(from previous dir)
docker build -t braces.plugin-host -f Braces.PluginHost/Dockerfile .
