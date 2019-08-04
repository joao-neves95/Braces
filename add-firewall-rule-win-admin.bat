powershell -noprofile -executionpolicy bypass -Command "netsh advfirewall firewall add rule name=\"Http Port 5000\" dir=in action=allow protocol=TCP localport=5000"
