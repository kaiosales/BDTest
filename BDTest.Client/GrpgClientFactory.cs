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
    public class GrpgClientFactory : IGrpgClientFactory, IDisposable
    {
        private static readonly GrpcChannelOptions _grpcOptions;
        private bool _disposed = false;
        private GrpcChannel _channel;
        private ConcurrentDictionary<string, object> _clients;

        static GrpgClientFactory()
        {
            HttpClientHandler httpHandler = new HttpClientHandler();
            httpHandler.ServerCertificateCustomValidationCallback = HttpClientHandler.DangerousAcceptAnyServerCertificateValidator;
            _grpcOptions =  new GrpcChannelOptions { HttpHandler = httpHandler };
        }

        public GrpgClientFactory(string address)
        {
            _channel = GrpcChannel.ForAddress(address, _grpcOptions);
            _clients = new ConcurrentDictionary<string, object>();
        }

        public T CreateClient<T>() where T : ClientBase
        {
            return CreateClient<T>(typeof(T).Name);
        }

        public T CreateClient<T>(string name) where T : ClientBase
        {
            object client = _clients.GetOrAdd(name, x => Activator.CreateInstance(typeof(T), _channel));
            return client as T;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (_disposed)
                return;

            if (disposing)
            {
                _channel?.Dispose();
            }

            _disposed = true;
        }
    }

}