using System;
using System.Collections.Generic;
using System.Text;

namespace BisoftMobileApp.HelpClasses
{
    public static class Constants
    {
        public static string SoapUrl
        {
            get
            {
                var defaultUrl = "http://www.bisoftextsystems.se:8080/Service1.svc";

                return defaultUrl;
            }
        }
    }
    
}
