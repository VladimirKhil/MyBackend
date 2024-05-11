﻿using MyBackend.Service.Contract;

namespace MyBackend.Service.Client;

internal sealed class MyBackendServiceClient : IMyBackendServiceClient
{
    public INewsApi News { get; }

    public IAdminApi Admin { get; }

    /// <summary>
    /// Initializes a new instance of <see cref="MyBackendServiceClient" /> class.
    /// </summary>
    /// <param name="client">HTTP client to use.</param>
    public MyBackendServiceClient(HttpClient client)
    {
        News = new NewsApi(client);
        Admin = new AdminApi(client);
    }
}
