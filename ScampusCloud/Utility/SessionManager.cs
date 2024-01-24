using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ScampusCloud.Utility
{
    public static class SessionManager
    {
        public static Guid? CompanyId
        {
            get {
                if (HttpContext.Current.Session["CompanyId"] == null)
                    return null;
                else
                    return new Guid(HttpContext.Current.Session["CompanyId"].ToString());
            }
            set
            {
                HttpContext.Current.Session["CompanyId"] = value;
            }
        }
        public static Guid? UserId
        {
            get
            {
                if (HttpContext.Current.Session["UserId"] == null)
                    return null;
                else
                    return new Guid(HttpContext.Current.Session["UserId"].ToString());
            }
            set
            {
                HttpContext.Current.Session["UserId"] = value;
            }
        }
        public static String UserName
        {
            get
            {
                if (HttpContext.Current.Session["UserName"] == null)
                    return null;
                else
                    return HttpContext.Current.Session["UserName"].ToString();
            }
            set
            {
                HttpContext.Current.Session["UserName"] = value;
            }
        }
        public static String Name
        {
            get
            {
                if (HttpContext.Current.Session["Name"] == null)
                    return null;
                else
                    return HttpContext.Current.Session["Name"].ToString();
            }
            set
            {
                HttpContext.Current.Session["Name"] = value;
            }
        }

        public static String StaffId
        {
            get
            {
                if (HttpContext.Current.Session["StaffId"] == null)
                    return null;
                else
                    return HttpContext.Current.Session["StaffId"].ToString();
            }
            set
            {
                HttpContext.Current.Session["StaffId"] = value;
            }
        }

        public static String EmailId
        {
            get
            {
                if (HttpContext.Current.Session["EmailId"] == null)
                    return null;
                else
                    return HttpContext.Current.Session["EmailId"].ToString();
            }
            set
            {
                HttpContext.Current.Session["EmailId"] = value;
            }
        }
        public static String Code
        {
            get
            {
                if (HttpContext.Current.Session["Code"] == null)
                    return null;
                else
                    return HttpContext.Current.Session["Code"].ToString();
            }
            set
            {
                HttpContext.Current.Session["Code"] = value;
            }
        }
    }
}