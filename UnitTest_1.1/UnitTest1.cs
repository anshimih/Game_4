using System;
using System.Collections.Generic;
using Xunit;

namespace TestProject1
{
    public class UnitTest1
    {
        
        public List<int> GetPrimes(int limit)
        {
            var result = new List<int>();

            bool is_Prime(int num)
            {
                for (int del = 2; del <= Math.Sqrt(num); del++)
                {
                    if (num % del == 0)
                    {
                        return false;
                    }
                }
                return true;
            }

            for (int num = 2; num < limit; num++)
            {
                if (is_Prime(num))
                    result.Add(num);
            }
            return result;
        }

        
        [Fact]
        public void Test1()
        {
            int limit = 10;
            var expected = new List<int> { 2, 3, 5, 7 };
            var result = GetPrimes(limit);
            Assert.Equal(expected, result);
        }

        
        [Fact]
        public void Test2()
        {
            int limit = 1;
            var expected = new List<int>();
            var result = GetPrimes(limit);
            Assert.Equal(expected, result);
        }

        
        [Fact]
        public void Test3()
        {
            int limit = 20;
            var expected = new List<int> { 2, 3, 5, 7, 11, 13, 17, 19 };
            var result = GetPrimes(limit);
            Assert.Equal(expected, result);
        }

        
        [Fact]
        public void GetPrimes_WithLimit11_ReturnsExpectedPrimes()
        {
            int limit = 11;
            var expected = new List<int> { 2, 3, 5, 7 };
            var result = GetPrimes(limit);
            Assert.Equal(expected, result);
        }
    }
}