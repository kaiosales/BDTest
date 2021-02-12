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
    public class GenerateServiceClient : IGenerateServiceClient
    {
        private IGrpgClientFactory _factory;
        public GenerateServiceClient(IGrpgClientFactory factory)
        {
            _factory = factory;
        }

        public async IAsyncEnumerable<int> GenerateNumbers(int count)
        {
            GenerateController.GenerateControllerClient client = _factory.CreateClient<GenerateController.GenerateControllerClient>();
            
            GenerateNumbersRequest request = new GenerateNumbersRequest { Count = count };

            using (AsyncServerStreamingCall<GetNumbersResponse> streamingCall = client.GenerateNumber(request))
            {
                await foreach (GetNumbersResponse response in streamingCall.ResponseStream.ReadAllAsync())
                    yield return response.Number;
            }           
        }
    }

}