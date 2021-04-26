using BankingChallenge.Application.Entities;
using BankingChallenge.Application.Exceptions;
using NUnit.Framework;

namespace BankingChallenge.Application.Tests
{
    public class LoanTests
    {
        LoanTerms _terms;
        Loan _loan;

        [SetUp]
        public void Setup()
        {
            _terms = new LoanTerms(5, 1, 10000);
            _loan = Loan.Create(500000, 120, _terms);
        }

        [Test]
        public void Create_Should_ThrowWhenAmountIs0()
        {
            Assert.Throws<ValidationException>(() => Loan.Create(0, 120, _terms));
        }

        [Test]
        public void Create_Should_ThrowWhenDurationIs0()
        {
            var terms = new LoanTerms(5, 1, 10000);
            Assert.Throws<ValidationException>(() => Loan.Create(500000, 0, _terms));
        }

        [Test]
        public void Create_Should_ThrowWhenTermsNotProvided()
        {
            var terms = new LoanTerms(5, 1, 10000);
            Assert.Throws<ValidationException>(() => Loan.Create(500000, 120, null));
        }

        [Test]
        public void CalculateAdministrationFee_Should_ReturnCorrectAmount()
        {
            var administrationFee = _loan.CalculateAdministrationFee();

            Assert.AreEqual(5000, administrationFee);
        }

        [Test]
        public void CalculateAdministrationFee_Should_LimitAmountToMaxValue()
        {
            // max administration fee is 1000 which is lower that 1% of 500000
            var terms = new LoanTerms(5, 1, 1000);
            var loan = Loan.Create(500000, 120, terms);

            var administrationFee = loan.CalculateAdministrationFee();

            Assert.AreEqual(1000, administrationFee);
        }

        [Test]
        public void CalculateTotalInterest_Should_ReturnCorrectAmount()
        {
            var totalInterest = _loan.CalculateTotalInterest();

            Assert.AreEqual(136393.09, (double)totalInterest, 0.005);
        }

        [Test]
        public void Installment_Should_ReturnCorrectAmount()
        {
            Assert.AreEqual(5303.28, (double)_loan.Installment, 0.005);
        }

        [Test]
        public void CalculateEffectiveApr_Should_ReturnCorrectAmount()
        {
            var effectiveApr = _loan.CalculateEffectiveApr();
            
            Assert.AreEqual(5.35, (double)effectiveApr, 0.005);
        }
    }
}