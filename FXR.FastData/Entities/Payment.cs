 /// Copyright (C) 2023 FXR, S. Teunisse All rights reserved.
namespace FXR.FastData.Entities
{
    public class Payment : IBuildable
    {
        public Guid Id { get; set; }
        public double Amount { get; set; }
        public string MerchantName { get; set; }
        public string PaymentMethod { get; set; }

    }
}