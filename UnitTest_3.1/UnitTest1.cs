using System;
using System.Linq;
using Xunit;

namespace TestProject1
{
    public class UnitTest1
    {
        
        public (string binarySum, int decimalSum) AddBinary(int x, int y)
        {
            string str_x = Convert.ToString(Math.Abs(x), 2);
            string str_y = Convert.ToString(Math.Abs(y), 2);
            int maxLength = Math.Max(str_x.Length, str_y.Length);

            
            str_x = str_x.PadLeft(maxLength, '0');
            str_y = str_y.PadLeft(maxLength, '0');

            
            string[] str_sum = new string[maxLength + 1];
            int carry = 0;

            for (int i = maxLength - 1; i >= 0; i--)
            {
                int bitX = str_x[i] - '0';
                int bitY = str_y[i] - '0';
                int sum = bitX + bitY + carry;

                str_sum[i + 1] = (sum % 2).ToString();
                carry = sum / 2;
            }

           
            str_sum[0] = carry.ToString();
            string binarySum = string.Join("", str_sum).TrimStart('0');
            int decimalSum = Convert.ToInt32(binarySum, 2);

            return (binarySum, decimalSum);
        }

        [Fact]
        public void Test1()
        {
            
            int x = 7;
            int y = 7;
            var expectedBinary = "1110";
            var expectedDecimal = 14;

            var (binarySum, decimalSum) = AddBinary(x, y);

            
            Assert.Equal(expectedBinary, binarySum);
            Assert.Equal(expectedDecimal, decimalSum);
        }

        [Fact]
        public void Test2()
        {
            
            int x = 5;   
            int y = 3;   
            var expectedBinary = "1000";
            var expectedDecimal = 8;

            
            var (binarySum, decimalSum) = AddBinary(x, y);

            
            Assert.Equal(expectedBinary, binarySum);
            Assert.Equal(expectedDecimal, decimalSum);
        }

        [Fact]
        public void Test3()
        {
            
            int x = 0;
            int y = 10;
            var expectedBinary = "1010";
            var expectedDecimal = 10;

            var (binarySum, decimalSum) = AddBinary(x, y);

            
            Assert.Equal(expectedBinary, binarySum);
            Assert.Equal(expectedDecimal, decimalSum);
        }
    }
}