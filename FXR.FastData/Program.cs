/// Copyright (C) 2023 FXR, S. Teunisse All rights reserved.

using FXR.FastData.Entities;
using System.Diagnostics;


Stopwatch s = new Stopwatch();
s.Start();

var paymentBuilder = new PaymentBuilder<Payment>()
    .WithId(Guid.NewGuid())
    .WithAmount(10);

var task1 = Task.Run(async () => { await paymentBuilder.WithMerchantNameAsync(GetMerchantName); });
var task2 = Task.Run(async () => { await paymentBuilder.WithPaymentMethod(GetPaymentMethod); });

var tasks = new List<Task> { task1, task2 };

await Task.WhenAll(tasks);



var result = paymentBuilder.Build();
s.Stop();

Console.WriteLine(s.ElapsedMilliseconds);


Console.WriteLine(result.MerchantName);
async Task<string> GetMerchantName(Guid paymentId)
{
    //Simulate I/O
    Task.Delay(5000).Wait();
    return "henk";
}

async Task<string> GetPaymentMethod(Guid paymentId)
{
    //Simulate I/O
    Task.Delay(5000).Wait();
    return "henk";
}




#region Multi
s.Restart();

List<Guid> paymentIds = new List<Guid>() {
    Guid.NewGuid(),
    Guid.NewGuid(),
    Guid.NewGuid(),
    Guid.NewGuid()
};

var a = new PaymentsParralelBuildDirector(paymentIds);
a.DirectMultiple();

var resultList = a.GetObjects();

s.Stop();

Console.WriteLine(s.ElapsedMilliseconds);
#endregion