﻿
namespace MauticApiClient.Net.Auth
{
    public class BasicAuthentication : IAuthentication
    {
        private readonly string _username;
        private readonly string _password;

        public BasicAuthentication(string username, string password)
        {
            _username = username;
            _password = password;
        }
    }
}
