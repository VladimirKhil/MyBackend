param (
    [string]$tag = "latest"
)

docker build . -f src\MyBackend.Service\Dockerfile -t vladimirkhil/mybackend:$tag