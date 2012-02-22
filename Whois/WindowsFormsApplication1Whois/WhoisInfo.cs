using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Krysalix
{
    public class WhoisInfo
    {
        public WhoisInfo(string info, string domain)
        {
            this.info = info;
            this.domain = domain;
        }

        private string info;
        private string domain;

        public string Info
        {
            get
            {
                return info;
            }
        }

        public string GetValue(string tag)
        {
            bool matchcase = false;

            string tmp = Info;

            if (matchcase == false)
            {
                tag = tag.ToLower();
                tmp = Info.ToLower();
            }

            int s1 = tmp.IndexOf(tag);
            if (s1 < 0) return String.Empty;
            s1 += tag.Length;
            int s2 = tmp.IndexOf(":", s1);

            for (int i = s1; i < s2; ++i)
            {
                char c = tmp[i];
                if (!(c == ' ' || c == '.')) return string.Empty;
            }

            if (s2 < 0) return String.Empty;
            ++s2;
            int e = tmp.IndexOf("\n", s2);

            string res = Info.Substring(s2, e - s2).Replace(" ", String.Empty).Trim();

            return res;
        }

        public bool IsExists()
        {
            //return Info.ToLower().IndexOf(domain) >= 0;

            string status = string.Empty;
            string name = string.Empty;

            if (status == string.Empty) status = GetValue("state"); //domain: Domain Name: state: Status:
            if (status == string.Empty) status = GetValue("Status");
            if (status == string.Empty && name == string.Empty) status = GetValue("domain");
            if (status == string.Empty && name == string.Empty) status = GetValue("Domain Name");
            if (status == string.Empty && name == string.Empty) status = GetValue("Server Name");

            return status != string.Empty ? true : (name != string.Empty ? true : false);
        }

    }
}
