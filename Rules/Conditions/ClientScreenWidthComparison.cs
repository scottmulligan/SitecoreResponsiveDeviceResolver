#region

using System.Web;
using Sitecore.Configuration;
using Sitecore.Rules;
using Sitecore.Rules.Conditions;

#endregion

namespace ResponsiveDeviceResolver.Rules.Conditions
{
    public class ClientScreenWidthComparison<T> :
        IntegerComparisonCondition<T>
        where T : RuleContext
    {
        /// <summary>
        ///     The name of the cookie containing the resolution value
        /// </summary>
        private readonly string _cookieName = Settings.GetSetting("RDR_cookieName");

        protected override bool Execute(T ruleContext)
        {
            //Get the resolution cookie for Sitecore Adaptive Images module if set
            var httpCookie = HttpContext.Current.Request.Cookies[_cookieName];
            return httpCookie != null && Compare(GetCookieResolution());
        }

        /// <summary>
        ///     Gets the cookie resolution.
        /// </summary>
        /// <returns></returns>
        public int GetCookieResolution()
        {
            // Double check that the cookie identifying screen resolution is set
            if (!IsResolutionCookieSet())
                return 0;

            // Split the cookie into resolution and pixel density ratio
            var httpCookie = HttpContext.Current.Request.Cookies[_cookieName];
            if (httpCookie != null)
            {
                string[] cookieResolution = httpCookie.Value.Split(',');
                // If we were able to get the cookie resolution
                if (cookieResolution.Length > 0)
                {
                    int clientWidth;
                    if (int.TryParse(cookieResolution[0], out clientWidth))
                        return clientWidth;
                }
            }

            return 0;
        }

        /// <summary>
        ///     Resolutions the cookie is set.
        /// </summary>
        /// <returns></returns>
        public bool IsResolutionCookieSet()
        {
            return HttpContext.Current.Request.Cookies[_cookieName] != null;
        }
    }
}