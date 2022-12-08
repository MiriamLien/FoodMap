using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DeliciousMap.Helpers
{
    public class TextFormatHelper
    {
        const double num = 60;
        public static string ConvertDigitalToDegrees(double digitalDegree)
        {
            int degree = (int)digitalDegree;
            double tmp = (digitalDegree - degree) * num;
            int minute = (int)tmp;
            double second = (tmp - minute) * num;
            string degrees = "" + degree + "°" + minute + "′" + second + "″";
            return degrees;
        }
    }
}