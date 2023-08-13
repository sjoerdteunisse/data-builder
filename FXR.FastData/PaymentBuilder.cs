/// Copyright (C) 2023 FXR, S. Teunisse All rights reserved.

using FXR.FastData.Entities;

public class PaymentBuilder<T> : IBuildDirector<T> where T : Payment, new()
{
    private T _payment;

    public PaymentBuilder()
    {
        _payment = new T();
    }

    public PaymentBuilder<T> WithAmount(double amount)
    {
        _payment.Amount = amount;
        return this;
    }

    public PaymentBuilder<T> WithId(Guid Id)
    {
        _payment.Id = Id;
        return this;
    }

    public async Task<PaymentBuilder<T>> WithMerchantNameAsync(Func<Guid, Task<string>> merchantNameRetrieval)
    {
        _payment.MerchantName = await merchantNameRetrieval(_payment.Id);
        return this;
    }

    public async Task<PaymentBuilder<T>> WithPaymentMethod(Func<Guid, Task<string>> paymentMethodRetrieval)
    {
        _payment.PaymentMethod = await paymentMethodRetrieval(_payment.Id);
        return this;
    }

    public T Build()
    {
        return _payment;
    }
}