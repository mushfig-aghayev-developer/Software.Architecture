using System;

namespace Strategy
{
    public class DigitService : IDigitService
    {
        public bool Validate(uint firstNumber, uint secondNumber)
        {
            Dictionary<char, int> map = new Dictionary<char, int>();
            string first_number = firstNumber.ToString();
            string second_number = secondNumber.ToString();

            foreach (char c in first_number)
                map[c] = map.TryGetValue(c, out int count) ? count + 1 : 1;

            foreach (char c in second_number)
            {
                if (!map.ContainsKey(c) || map[c] == 0)
                    return false;


                if (map[c] == 1)
                    map.Remove(c);
                else
                    map[c]--;
            }
            return true;
        }
    }
}
