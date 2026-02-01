using System;
using System.Collections.Generic;
using System.Linq;

namespace Strategy
{
    public class DigitalService_v2 : IDigitService
    {
        public bool Validate(uint firstNumber, uint secondNumber)
        {
            Dictionary<uint, int> map = new Dictionary<uint, int>();
            while (firstNumber != 0)
            {
                uint remainder = firstNumber % 10;
                map[remainder] = map.TryGetValue(remainder, out int value) ? value + 1 : 1;
                firstNumber /= 10;
            }

            while (secondNumber != 0)
            {
                uint remainder = secondNumber % 10;
                if (!map.ContainsKey(remainder) || map[remainder] == 0)
                    return false;
                if (map[remainder] == 1)
                    map.Remove(remainder);
                else
                    map[remainder]--;

                secondNumber /= 10;
            }
            return true;
        }
    }
}
