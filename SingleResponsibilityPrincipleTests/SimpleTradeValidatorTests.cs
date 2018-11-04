using Microsoft.VisualStudio.TestTools.UnitTesting;
using SingleResponsibilityPrinciple;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SingleResponsibilityPrinciple.Tests
{
    [TestClass()]
    public class SimpleTradeValidatorTests
    {
        [TestMethod()]
        public void TestNormalTrade()
        {
            //Arrange
            var logger = new ConsoleLogger();
            var tradeValidator = new SimpleTradeValidator(logger);
            string[] strData = { "GBPUSD", "4444", "1.5" };
            //Act
            bool result = tradeValidator.Validate(strData);
            //Assert
            Assert.IsTrue(result);
        }
        [TestMethod()]
        public void TestAmount999()
        {
            //Arrange
            var logger = new ConsoleLogger();
            var tradeValidator = new SimpleTradeValidator(logger);
            string[] strData = { "GBPUSD", "999", "1.5" };
            //Act
            bool result = tradeValidator.Validate(strData);
            //Assert
            Assert.IsFalse(result);
        }
        [TestMethod()]
        public void TestAmount1000()
        {
            //Arrange
            var logger = new ConsoleLogger();
            var tradeValidator = new SimpleTradeValidator(logger);
            string[] strData = { "GBPUSD", "1000", "1.5" };
            //Act
            bool result = tradeValidator.Validate(strData);
            //Assert
            Assert.IsTrue(result);
        }
        [TestMethod()]
        public void TestAmountNeg10000()
        {
            //Arrange
            var logger = new ConsoleLogger();
            var tradeValidator = new SimpleTradeValidator(logger);
            string[] strData = { "GBPUSD", "-10000", "1.51" };
            //Act
            bool result = tradeValidator.Validate(strData);
            //Assert
            Assert.IsFalse(result);
        }
        [TestMethod()]
        public void TestAmount100k()
        {
            //Arrange
            var logger = new ConsoleLogger();
            var tradeValidator = new SimpleTradeValidator(logger);
            //string[] strData = { "GBPUSD", "1000000", "1.5" }; Wrong amount value here.
            string[] strData = { "GBPUSD", "100000", "1.5" };
            //Act
            bool result = tradeValidator.Validate(strData);
            //Assert
            Assert.IsTrue(result);
        }
        [TestMethod()]
        public void TestAmount101k()
        {
            //Arrange
            var logger = new ConsoleLogger();
            var tradeValidator = new SimpleTradeValidator(logger);
            string[] strData = { "GBPUSD", "1000001", "1.5" };
            //Act
            bool result = tradeValidator.Validate(strData);
            //Assert
            Assert.IsFalse(result);
        }

    }
}