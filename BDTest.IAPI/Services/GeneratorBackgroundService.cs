// using System;
// using System.Collections.Generic;
// using System.Linq;
// using System.Threading;
// using System.Threading.Tasks;
// using Microsoft.Extensions.Hosting;
// using BDTest.Core.Models;
// using BDTest.Data;
// using BDTest.Domain;

// namespace BDTest.IAPI.Services
// {
//     public class GeneratorBackgroundService: BackgroundService
//     {
//         private IRepository<Batch> _batchRepository;
//         private IRepository<BatchNumber> _batchNumberRepository;
//         private readonly IGenerator _generator;

//         public GeneratorBackgroundService(IRepository<Batch> batchRepository, IRepository<BatchNumber> batchNumberRepository, IGenerator generator)
//         {
//             _batchRepository = batchRepository;
//             _batchNumberRepository = batchNumberRepository;
//             _generator = generator;
//         }

//         protected override async Task ExecuteAsync(CancellationToken stoppingToken)
//         {
//             while (!stoppingToken.IsCancellationRequested)
//             {
//                 try
//                 {
//                     await Process();
//                 }
//                 catch (Exception) {}

//                 await Task.Delay(1000);
//             }
//         }

//         private async Task Process()
//         {
//             await foreach(Batch batch in _batchRepository.GetAllAsync(r => r.Status == StatusEnum.Generating))
//             {
//                 await foreach(int number in _generator.Generate(batch.Count))
//                 {
//                     await _batchNumberRepository.InsertAsync(new BatchNumber { Batch = batch, Number = number });
//                 }
                
//                 batch.Status = StatusEnum.Done;
//                 await _batchRepository.UpdateAsync(batch);
//             }
//         }
//     }
// }
