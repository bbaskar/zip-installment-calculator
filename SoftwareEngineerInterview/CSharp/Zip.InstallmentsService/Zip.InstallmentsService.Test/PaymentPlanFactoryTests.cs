using Shouldly;
using System;
using Xunit;

namespace Zip.InstallmentsService.Test
{
    public class PaymentPlanFactoryTests
    {
        private const decimal PurchaseAmount = 444.88M;
        private const decimal InstallmentAmount = PurchaseAmount / 4;
        private static readonly DateTime DueDate = new(2022, 11, 23);

        [Fact]
        public void WhenCreatePaymentPlanWithValidOrderAmount_ShouldReturnValidPaymentPlan()
        {
            // Arrange
            var paymentPlanFactory = new PaymentPlanFactory();

            // Act
            var paymentPlan = paymentPlanFactory.CreatePaymentPlan(PurchaseAmount);

            // Assert
            paymentPlan.ShouldNotBeNull();
        }

        [Fact]
        public void WhenCreatePaymentPlanWithInvalidOrderAmount_ShouldReturnEmptyPaymentPlan()
        {
            // Arrange
            var paymentPlanFactory = new PaymentPlanFactory();

            // Act
            var paymentPlan = paymentPlanFactory.CreatePaymentPlan(0);

            // Assert
            paymentPlan.ShouldBeNull();
        }

        [Fact]
        public void WhenCreatePaymentPlanWithPurchaseAmount_PaymentPlanShouldReturnSamePurchaseAmount()
        {
            // Arrange
            var paymentPlanFactory = new PaymentPlanFactory();

            // Act
            var paymentPlan = paymentPlanFactory.CreatePaymentPlan(PurchaseAmount);

            // Assert
            paymentPlan.PurchaseAmount.ShouldBe(PurchaseAmount);
        }

        [Fact]
        public void WhenCreatePaymentPlanWithValidOrderAmount_ShouldReturnFourInstallments()
        {
            // Arrange
            var paymentPlanFactory = new PaymentPlanFactory();

            // Act
            var paymentPlan = paymentPlanFactory.CreatePaymentPlan(PurchaseAmount);

            // Assert
            paymentPlan.Installments.Length.ShouldBe(4);
        }

        [Fact]
        public void WhenCreatePaymentPlanWithValidOrderAmount_AssertAllDueAmount()
        {
            // Arrange
            var paymentPlanFactory = new PaymentPlanFactory();

            // Act
            var paymentPlan = paymentPlanFactory.CreatePaymentPlan(PurchaseAmount);

            // Assert
            paymentPlan.ShouldSatisfyAllConditions(
                () => paymentPlan.Installments[0].Amount.ShouldBe(InstallmentAmount),
                () => paymentPlan.Installments[1].Amount.ShouldBe(InstallmentAmount),
                () => paymentPlan.Installments[2].Amount.ShouldBe(InstallmentAmount),
                () => paymentPlan.Installments[3].Amount.ShouldBe(InstallmentAmount));
        }

        [Fact]
        public void WhenCreatePaymentPlanWithValidOrderAmount_AssertAllDueDate()
        {
            // Arrange
            var paymentPlanFactory = new PaymentPlanFactory();

            // Act
            var paymentPlan = paymentPlanFactory.CreatePaymentPlan(PurchaseAmount);

            // Assert
            paymentPlan.ShouldSatisfyAllConditions(
                () => paymentPlan.Installments[0].DueDate.Date.ShouldBe(DueDate),
                () => paymentPlan.Installments[1].DueDate.Date.ShouldBe(DueDate.AddDays(14)),
                () => paymentPlan.Installments[2].DueDate.Date.ShouldBe(DueDate.AddDays(28)),
                () => paymentPlan.Installments[3].DueDate.Date.ShouldBe(DueDate.AddDays(42)));
        }
    }
}