using System;
using Xunit;

namespace TestProject1
{
    public class UnitTest1
    {
        
        public (int quotient, int remainder) Divide(int num, int del)
        {
            int answer = 0;

            for (int i = 0; Math.Abs(i * del) <= Math.Abs(num); i++)
            {
                answer = i;
            }

            int ost = num - (answer * del);
            return (answer, ost);
        }

        [Fact]
        public void Test1()
        {
            
            int num = 10;
            int del = 3;
            var expectedQuotient = 3;
            var expectedRemainder = 1;

            
            var (quotient, remainder) = Divide(num, del);

            
            Assert.Equal(expectedQuotient, quotient);
            Assert.Equal(expectedRemainder, remainder);
        }
    }
}