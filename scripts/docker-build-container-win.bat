cd..
docker build -t braces.plugin-host -f Braces.PluginHost/Dockerfile-Win .
docker run --rm --network="nat" -p 5002:69 --name braces.plugin-host braces.plugin-host
cd scripts
