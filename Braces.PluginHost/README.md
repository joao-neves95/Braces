- Switch docker to Windows containers.
(from Braces root dir)
docker build -t braces.plugin-host -f Braces.PluginHost/Dockerfile .
docker run -p 6969:69 --network="host" --name braces.plugin-host braces.plugin-host || docker run --name braces.plugin-host braces.plugin-host --network="host"
docker start braces.plugin-host -a

docker rmi braces.plugin-host
