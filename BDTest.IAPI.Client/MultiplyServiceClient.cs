using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using BDTest.Client;
using BDTest.Core.Protos;
using Grpc.Core;

namespace BDTest.IAPI.Client
{
    public class MultiplyServiceClient : IMultiplyServiceClient
    {
        private IGrpgClientFactory _factory;
        public MultiplyServiceClient(IGrpgClientFactory factory)
        {
            _factory = factory;
        }

        public async Task<int> MultiplyNumber(int number)
        {
            MultiplyController.MultiplyControllerClient client = _factory.CreateClient<MultiplyController.MultiplyControllerClient>();
            
            MultiplyNumberRequest request = new MultiplyNumberRequest { Number = number };

            GetProductResponse response = await client.MultiplyNumberAsync(request);

            return response.Value;
        }
    }

}