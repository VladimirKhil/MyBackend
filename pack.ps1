param (
    [string]$version = "1.0.0",
    [string]$apikey = ""
)

dotnet pack src\MyBackend.Service.Contract\MyBackend.Service.Contract.csproj -c Release /property:Version=$version
dotnet pack src\MyBackend.Service.Client\MyBackend.Service.Client.csproj -c Release /property:Version=$version
dotnet nuget push bin\.Release\MyBackend.Service.Contract\VKhil.MyBackend.Service.Contract.$version.nupkg --api-key $apikey --source https://api.nuget.org/v3/index.json
dotnet nuget push bin\.Release\MyBackend.Service.Client\VKhil.MyBackend.Service.Client.$version.nupkg --api-key $apikey --source https://api.nuget.org/v3/index.json