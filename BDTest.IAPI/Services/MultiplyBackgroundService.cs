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
//     public class MultiplyBackgroundService: BackgroundService
//     {
//         private IRepository<Batch> _batchRepository;
//         private IRepository<BatchNumber> _batchNumberRepository;
//         private IRepository<BatchProduct> _batchProductRepository;
//         private readonly IMultiplier _multiplier;

//         public MultiplyBackgroundService(
//             IRepository<Batch> batchRepository, 
//             IRepository<BatchNumber> batchNumberRepository, 
//             IRepository<BatchProduct> batchProductRepository, 
//             IMultiplier multiplier)
//         {
//             _batchRepository = batchRepository;
//             _batchNumberRepository = batchNumberRepository;
//             _batchProductRepository = batchProductRepository;
//             _multiplier = multiplier;
//         }

//         protected override async Task ExecuteAsync(CancellationToken stoppingToken)
//         {
//             while (!stoppingToken.IsCancellationRequested)
//             {
//                 try
//                 {
//                     await Process();
//                 }
//                 catch(Exception) {}

//                 await Task.Delay(1500);
//             }
//         }

//         private async Task Process()
//         {
//             await foreach(Batch batch in _batchRepository.GetAllAsync(r => r.Status == StatusEnum.Multiplying))
//             {
//                 await foreach(BatchNumber number in _batchNumberRepository.GetAllAsync(n => n.Batch.Id == batch.Id))
//                 {
//                     int product = await _multiplier.Multiply(number.Number);
//                     await _batchProductRepository.InsertAsync(new BatchProduct { Batch = batch, Value = product });
//                 }
                
//                 batch.Status = StatusEnum.Done;
//                 await _batchRepository.UpdateAsync(batch);
//             }
//         }
//     }
// }
