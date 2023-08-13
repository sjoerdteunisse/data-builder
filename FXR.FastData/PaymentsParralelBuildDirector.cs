/// Copyright (C) 2023 FXR, S. Teunisse All rights reserved.
using FXR.FastData.Entities;
public class PaymentsParralelBuildDirector : ParralelBuildDirctor<Payment>
{
    public PaymentsParralelBuildDirector(List<Guid> objs) 
        : base(objs)
    {
    }

    public override IBuildDirector<Payment> BuildObject(Guid identifier)
    {
        var paymentBuilder = new PaymentBuilder<Payment>()
                       .WithId(identifier)
                       .WithAmount(10);

        var task1 = Task.Run(async () => { await paymentBuilder.WithMerchantNameAsync(GetMerchantName); });
        var task2 = Task.Run(async () => { await paymentBuilder.WithPaymentMethod(GetPaymentMethod); });

        var tasks = new List<Task> { task1, task2 };

        Task.WaitAll(new Task[] { task1, task2 });

        return paymentBuilder;
    }

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

}
