using System;
using System.Threading.Tasks;
using RestSharp;
using RestSharp.Authenticators;

namespace SimpleChatFrontend.Helpers;

public class TokenAuthenticator : IAuthenticator
{
    public ValueTask Authenticate(IRestClient client, RestRequest request)
    {
        client.AddDefaultQueryParameter("token", LocalStorage.Get("token") ?? "");
        client.AddDefaultQueryParameter("userId", LocalStorage.Get("userId") ?? "");
        return ValueTask.CompletedTask;
    }
}
