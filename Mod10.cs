using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ejercicioSanitizar
{
    public class Mod10
    {
        private string _serialNumber;


        public string ActivationCardNumber
        {
            get
            {
                return string.Format("{0}{1}", _serialNumber, CheckDigit);
            }
        }

        public int CheckDigit
        {
            get
            {
                List<int> serialNumberArray = new List<int>();
                var modCheck = _serialNumber.Length % 2;
                var loopCounter = 0;
                while (loopCounter < _serialNumber.Length)
                {
                    var digit = Convert.ToInt32(_serialNumber.Substring(loopCounter, 1));
                    serialNumberArray.Add(digit);
                    loopCounter++;
                }

                for (var i = 0; i < serialNumberArray.Count; i++)
                {
                    var check = i + 1;
                    if ((check % 2) == modCheck)
                    {
                        serialNumberArray[i] = serialNumberArray[i] * 2;
                    }
                }

                loopCounter = 0;
                var additiveResult = 0;
                var arrayItemValue = 0;
                var checkSum = 0;

                while (loopCounter < _serialNumber.Length)
                {
                    arrayItemValue = serialNumberArray[loopCounter];
                    if (arrayItemValue > 9)
                    {
                        additiveResult = 1 + (arrayItemValue % 10);
                    }
                    else
                    {
                        additiveResult = arrayItemValue;
                    }
                    checkSum = checkSum + additiveResult;
                    loopCounter++;
                }

                var checkDigit = 0;
                if ((checkSum % 10) > 0)
                {
                    checkDigit = 10 - (checkSum % 10);
                }

                return checkDigit;
            }
        }
    }
}
