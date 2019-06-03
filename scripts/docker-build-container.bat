cd..
docker build -t braces.plugin-host -f Braces.PluginHost/Dockerfile .
docker run --network="host" --name braces.plugin-host braces.plugin-host
cd scripts

