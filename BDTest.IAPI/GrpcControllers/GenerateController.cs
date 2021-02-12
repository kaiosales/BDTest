using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Channels;
using System.Threading.Tasks;
using Google.Protobuf.WellKnownTypes;
using Grpc.Core;
using Microsoft.Extensions.Logging;
using BDTest.Domain;
using BDTest.Core.Protos;
using Protos = BDTest.Core.Protos;

namespace BDTest.IAPI.GrpcControllers
{
    public class GenerateController : Protos.GenerateController.GenerateControllerBase
    {
        private readonly ILogger<GenerateController> _logger;
        private readonly IGenerator _generator;

        public GenerateController(ILogger<GenerateController> logger, IGenerator generator)
        {
            _logger = logger;
            _generator = generator;
        }

        public override async Task GenerateNumber(GenerateNumbersRequest request, IServerStreamWriter<GetNumbersResponse> responseStream, ServerCallContext context)
        {
            _logger.LogInformation($"{request.Count} number generation requested");

            await foreach(int number in _generator.Generate(request.Count))
                await responseStream.WriteAsync(new GetNumbersResponse { Number = number });
        }    
    }
}