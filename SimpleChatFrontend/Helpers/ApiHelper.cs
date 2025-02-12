using System;
using dotenv.net;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using RestSharp;

namespace SimpleChatFrontend.Helpers;

public static class ApiHelper
{
    public static RestClient Client
    {
        get
        {
            RestClientOptions options = new RestClientOptions(DotEnv.Read()["API_URL"])
            {
                ThrowOnAnyError = false,
                ThrowOnDeserializationError = false,
            };
            RestClient client = new RestClient(options);

            return client;
        }
    }
}
