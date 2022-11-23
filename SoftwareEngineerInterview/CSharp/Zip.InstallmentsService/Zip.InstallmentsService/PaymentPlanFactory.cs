using System;
using System.Collections.Generic;

namespace Zip.InstallmentsService
{
    /// <summary>
    /// This class is responsible for building the PaymentPlan according to the Zip product definition.
    /// </summary>
    public class PaymentPlanFactory
    {
        /// <summary>
        /// Builds the PaymentPlan instance.
        /// </summary>
        /// <param name="purchaseAmount">The total amount for the purchase that the customer is making.</param>
        /// <returns>The PaymentPlan created with all properties set.</returns>
        public PaymentPlan CreatePaymentPlan(decimal purchaseAmount)
        {
            if (purchaseAmount == 0)
                return null;

            // Placeholder for due date and due amount.
            DateTime dueDate = DateTime.Now;
            decimal dueAmount = purchaseAmount / 4;

            // Create 4 installments with a frequency of 14 days.
            List<Installment> installments = new() {
               new Installment(){ Id = Guid.NewGuid(), DueDate = dueDate, Amount = dueAmount},
               new Installment(){ Id = Guid.NewGuid(), DueDate = dueDate.AddDays(14), Amount = dueAmount},
               new Installment(){ Id = Guid.NewGuid(), DueDate = dueDate.AddDays(28), Amount = dueAmount},
               new Installment(){ Id = Guid.NewGuid(), DueDate = dueDate.AddDays(42), Amount = dueAmount}
            };

            // Construct the payment plan based on the computed installments
            PaymentPlan paymentPlan = new()
            {
                Id = Guid.NewGuid(),
                PurchaseAmount = purchaseAmount,
                Installments = installments.ToArray()
            };

            // Return payment plan
            return paymentPlan;
        }
    }
}