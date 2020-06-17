using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Security.Principal;
using System.Web;

namespace SoloContacts.Core.Security
{
    public static class ApplicationContext
    {
        #region User Environment

        /// <summary>
        /// Get or set the current <see cref="IPrincipal" />
        /// object representing the user's identity.
        /// </summary>
        /// <remarks>
        /// When running under IIS the HttpContext.Current.User value
        /// is used, otherwise the current Thread.CurrentPrincipal
        /// value is used.
        /// </remarks>
        public static ClaimsPrincipal User
        {
            get
            {
                return ClaimsPrincipal.Current;

                //if (HttpContext.Current == null)
                //    return Thread.CurrentPrincipal;
                //else
                //    return HttpContext.Current.User;
            }
            set
            {


                //if (HttpContext.Current != null)
                //    HttpContext.Current.User = value;
                //Thread.CurrentPrincipal = value;
            }
        }

        #endregion
    }
}
