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
    public class MultiplyController : Protos.MultiplyController.MultiplyControllerBase
    {
        private readonly ILogger<MultiplyController> _logger;
        private readonly IMultiplier _multiplier;

        public MultiplyController(ILogger<MultiplyController> logger, IMultiplier multiplier)
        {
            _logger = logger;
            _multiplier = multiplier;
        }

        public override async Task<GetProductResponse> MultiplyNumber(MultiplyNumberRequest request, ServerCallContext context)
        {
            _logger.LogInformation($"Multiplication requested for number {request.Number}");

            return new GetProductResponse
            {
                Value = await _multiplier.Multiply(request.Number)
            };
        }    
    }
}