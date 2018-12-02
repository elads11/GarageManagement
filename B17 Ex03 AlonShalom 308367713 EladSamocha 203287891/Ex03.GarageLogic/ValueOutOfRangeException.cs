using System;
using System.Collections.Generic;
using System.Text;

namespace Ex03.GarageLogic
{
    public class ValueOutOfRangeException : Exception
    {
        private float m_MaxValue;
        private float m_MinValue;

        public ValueOutOfRangeException()
        {
        }

        public ValueOutOfRangeException(string i_Message)
            : base(i_Message)
        {
        }

        public ValueOutOfRangeException(float i_MinValue, float i_MaxValue)
        {
            m_MaxValue = i_MaxValue;
            m_MinValue = i_MinValue;
            string rangeOfValuesInfo = string.Format("Out of range, the correct range in this case is {0} and {1}", m_MinValue, m_MaxValue);
            Console.WriteLine(rangeOfValuesInfo);
        }
    }
}