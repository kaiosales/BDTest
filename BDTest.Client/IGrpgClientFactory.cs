using System;
using System.Collections.Generic;
using System.Collections.Concurrent;
using System.Net.Http;
using System.Threading;
using System.Threading.Channels;
using System.Threading.Tasks;
using Grpc.Core;
using Grpc.Net.Client;

namespace BDTest.Client
{
    public interface IGrpgClientFactory
    {
        T CreateClient<T>() where T : ClientBase;
        T CreateClient<T>(string name) where T : ClientBase;
    }
}