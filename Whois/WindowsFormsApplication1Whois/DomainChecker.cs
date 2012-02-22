using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Krysalix
{
    public class DomainChecker
    {
        public bool Exists(string domain)
        {

            WhoisInfo info = GetInfo(domain);

            string res = info.Info;

            string status = string.Empty;
            string name = string.Empty;

            if (status == string.Empty) status = SearchValue2(res, "state", false); //domain: Domain Name: state: Status:
            if (status == string.Empty) status = SearchValue2(res, "Status", false);
            if (status == string.Empty && name == string.Empty) status = SearchValue2(res, "domain", false);
            if (status == string.Empty && name == string.Empty) status = SearchValue2(res, "Domain Name", false);
            if (status == string.Empty && name == string.Empty) status = SearchValue2(res, "Server Name", false);

            return status != string.Empty ? true : (name != string.Empty ? true : false);
        }

        public WhoisInfo GetInfo(string domain)
        {
            domain = domain.ToLower();

            // whois services
            /*
            com whois.verisign-grs.com
            net whois.verisign-grs.com
            org whois.pir.org
            info whois.afilias.net
            ru whois.tcinet.ru
            su whois.tcinet.ru
            рф whois.tcinet.ru
            */

            /*
            ac whois.nic.ac
            ac.cn whois.cnnic.net.cn
            ac.jp whois.nic.ad.jp
            ac.uk whois.ja.net
            ad.jp whois.nic.ad.jp
            adm.br whois.nic.br
            adv.br whois.nic.br
            aero whois.information.aero
            ag whois.nic.ag
            agr.br whois.nic.br
            ah.cn whois.cnnic.net.cn
            al whois.ripe.net
            am whois.amnic.net
            am.br whois.nic.br
            arq.br whois.nic.br
            art.br whois.nic.br
            as whois.nic.as
            asn.au whois.aunic.net
            at whois.nic.at
            ato.br whois.nic.br
            au whois.aunic.net
            av.tr whois.nic.tr
            az whois.ripe.net
            ba whois.ripe.net
            be whois.geektools.com
            bel.tr whois.nic.tr
            bg whois.digsys.bg
            bio.br whois.nic.br
            biz whois.biz
            biz.tr whois.nic.tr
            biz.ua whois.com.ua
            bj.cn whois.cnnic.net.cn
            bmd.br whois.nic.br
            br whois.registro.br
            by whois.ripe.net
            ca whois.cira.ca
            cc whois.verisign-grs.com
            cd whois.cd
            ch whois.nic.ch
            cim.br whois.nic.br
            ck whois.ck-nic.org.ck
            cl whois.nic.cl
            cn whois.cnnic.net.cn
            cng.br whois.nic.br
            cnt.br whois.nic.br
            co.at whois.nic.at
            co.jp whois.nic.ad.jp
            co.ua whois.com.ua
            co.uk whois.nic.uk
            com whois.verisign-grs.com
            com.au whois.aunic.net
            com.br whois.nic.br
            com.cn whois.cnnic.net.cn
            com.eg whois.ripe.net
            com.hk whois.hknic.net.hk
            com.mx whois.nic.mx
            com.ru whois.ripn.ru
            com.tr whois.nic.tr
            com.tw whois.twnic.net
            com.ua whois.com.ua
            com.ua whois.ripe.net
            conf.au whois.aunic.net
            cq.cn whois.cnnic.net.cn
            csiro.au whois.aunic.net
            cx whois.nic.cx
            cy whois.ripe.net
            cz whois.nic.cz
            de whois.denic.de
            dk whois.dk-hostmaster.dk
            dr.tr whois.nic.tr
            dz whois.ripe.net
            ecn.br whois.nic.br
            edu whois.crsnic.net
            edu whois.verisign-grs.net
            edu.au whois.aunic.net
            edu.br whois.nic.br
            edu.tr whois.nic.tr
            ee whois.eenet.ee
            eg whois.ripe.net
            emu.id.au whois.aunic.net
            eng.br whois.nic.br
            es whois.ripe.net
            esp.br whois.nic.br
            etc.br whois.nic.br
            eti.br whois.nic.br
            eu whois.eu
            eun.eg whois.ripe.net
            far.br whois.nic.br
            fi whois.ripe.net
            fj whois.usp.ac.fj
            fj.cn whois.cnnic.net.cn
            fm.br whois.nic.br
            fnd.br whois.nic.br
            fo whois.ripe.net
            fot.br whois.nic.br
            fr whois.nic.fr
            fst.br whois.nic.br
            g12.br whois.nic.br
            gb whois.ripe.net
            gb.com whois.nomination.net
            gb.net whois.nomination.net
            gd whois.adamsnames.com
            gd.cn whois.cnnic.net.cn
            ge whois.ripe.net
            gen.tr whois.nic.tr
            gg whois.gg
            ggf.br whois.nic.br
            gl whois.ripe.net
            gob.mx whois.nic.mx
            gov.au whois.aunic.net
            gov.br whois.nic.br
            gov.cn whois.cnnic.net.cn
            gov.hk whois.hknic.net.hk
            gov.tr whois.nic.tr
            gr whois.ripe.net
            gr.jp whois.nic.ad.jp
            gs whois.adamsnames.tc
            gs.cn whois.cnnic.net.cn
            gx.cn whois.cnnic.net.cn
            gz.cn whois.cnnic.net.cn
            ha.cn whois.cnnic.net.cn
            hb.cn whois.cnnic.net.cn
            he.cn whois.cnnic.net.cn
            hi.cn whois.cnnic.net.cn
            hk whois.hknic.net.hk
            hk.cn whois.cnnic.net.cn
            hl.cn whois.cnnic.net.cn
            hm whois.registry.hm
            hn.cn whois.cnnic.net.cn
            hu whois.ripe.net
            id.au whois.aunic.net
            idv.tw whois.twnic.net
            ie whois.domainregistry.ie
            il whois.isoc.org.il
            imb.br whois.nic.br
            in.ua whois.in.ua
            ind.br whois.nic.br
            ind.ua whois.com.ua
            inf.br whois.nic.br
            info whois.afilias.info
            info.au whois.aunic.net
            info.tr whois.nic.tr
            int whois.iana.org
            int.ru whois.int.ru
            is whois.isnic.is
            it whois.nic.it
            jl.cn whois.cnnic.net.cn
            jor.br whois.nic.br
            jp whois.nic.ad.jp
            jobs whois.verisign-grs.com
            js.cn whois.cnnic.net.cn
            jx.cn whois.cnnic.net.cn
            k12.tr whois.nic.tr
            ke whois.rg.net
            kr whois.krnic.net
            la whois.nic.la
            lel.br whois.nic.br
            li whois.nic.ch
            lk whois.nic.lk
            ln.cn whois.cnnic.net.cn
            lt ns.litnet.lt
            ltd.uk whois.nic.uk
            lu whois.dns.lu
            lv whois.ripe.net
            ma whois.ripe.net
            mat.br whois.nic.br
            mc whois.ripe.net
            md whois.ripe.net
            me.uk whois.nic.uk
            med.br whois.nic.br
            mil whois.nic.mil
            mil.br whois.nic.br
            mil.tr whois.nic.tr
            mk whois.ripe.net
            mn whois.nic.mn
            mo.cn whois.cnnic.net.cn
            ms whois.adamsnames.tc
            mt whois.ripe.net
            mus.br whois.nic.br
            mx whois.nic.mx
            name whois.nic.name
            name.tr whois.nic.tr
            ne.jp whois.nic.ad.jp
            net whois.verisign-grs.com
            net.au whois.aunic.net
            net.br whois.nic.br
            net.cn whois.cnnic.net.cn
            net.eg whois.ripe.net
            net.hk whois.hknic.net.hk
            net.lu whois.dns.lu
            net.mx whois.nic.mx
            net.ru whois.ripn.ru
            net.tr whois.nic.tr
            net.tw whois.twnic.net
            net.ua whois.com.ua
            net.uk whois.nic.uk
            nl whois.domain-registry.nl
            nm.cn whois.cnnic.net.cn
            no whois.norid.no
            no.com whois.nomination.net
            nom.br whois.nic.br
            not.br whois.nic.br
            ntr.br whois.nic.br
            nu whois.nic.nu
            nx.cn whois.cnnic.net.cn
            nz whois.domainz.net.nz
            odo.br whois.nic.br
            oop.br whois.nic.br
            or.at whois.nic.at
            or.jp whois.nic.ad.jp
            org whois.pir.org
            org.au whois.aunic.net
            org.br whois.nic.br
            org.cn whois.cnnic.net.cn
            org.hk whois.hknic.net.hk
            org.lu whois.dns.lu
            org.ru whois.ripn.ru
            org.tr whois.nic.tr
            org.tw whois.twnic.net
            org.ua whois.com.ua
            org.uk whois.nic.uk
            pk whois.pknic.net
            pl whois.ripe.net
            plc.uk whois.nic.uk
            pol.tr whois.nic.tr
            pp.ru whois.ripn.ru
            ppg.br whois.nic.br
            pro.br whois.nic.br
            psc.br whois.nic.br
            psi.br whois.nic.br
            pt whois.ripe.net
            qh.cn whois.cnnic.net.cn
            qsl.br whois.nic.br
            rec.br whois.nic.br
            ro whois.ripe.net
            ru whois.ripn.ru
            sc.cn whois.cnnic.net.cn
            sd.cn whois.cnnic.net.cn
            se whois.nic-se.se
            se.com whois.nomination.net
            se.net whois.nomination.net
            sg whois.nic.net.sg
            sh whois.nic.sh
            sh.cn whois.cnnic.net.cn
            si whois.arnes.si
            sk whois.ripe.net
            slg.br whois.nic.br
            sm whois.ripe.net
            sn.cn whois.cnnic.net.cn
            srv.br whois.nic.br
            st whois.nic.st
            su whois.ripn.net
            sx.cn whois.cnnic.net.cn
            tc whois.adamsnames.tc
            tel.tr whois.nic.tr
            th whois.nic.uk
            tj.cn whois.cnnic.net.cn
            tm whois.nic.tm
            tmp.br whois.nic.br
            tn whois.ripe.net
            to whois.tonic.to
            tr whois.ripe.net
            trd.br whois.nic.br
            tur.br whois.nic.br
            tv whois.verisign-grs.com
            tv.br whois.nic.br
            tw whois.twnic.net
            tw.cn whois.cnnic.net.cn
            ua whois.com.ua
            uk whois.thnic.net
            uk.com whois.nomination.net
            uk.net whois.nomination.net
            us whois.nic.us
            va whois.ripe.net
            vet.br whois.nic.br
            vg whois.adamsnames.tc
            wattle.id.au whois.aunic.net
            web.tr whois.nic.tr
            ws whois.worldsite.ws
            xj.cn whois.cnnic.net.cn
            xz.cn whois.cnnic.net.cn
            yn.cn whois.cnnic.net.cn
            yu whois.ripe.net
            za whois.frd.ac.za
            zj.cn whois.cnnic.net.cn
            zlg.br whois.nic.br
            */
            // http://www.iana.org/domains/root/db/
            // http://hexillion.com/whois/

            string res = string.Empty;
            string server = string.Empty;

            switch (TopDomain(domain))
            {
                case "bz":
                    server = "whois.belizenic.bz";
                    break;
                case "com":
                    server = "whois.internic.net";
                    //server = "whois.networksolutions.com";
                    //server = "whois.verisign-grs.com";
                    //server = "whois.markmonitor.com"; // microsoft.com
                    //server = "whois.tucows.com";
                    //server = "whois.opensrs.net";
                    //server = "whois.godaddy.com";
                    break;
                case "net":
                    server = "whois.internic.net";
                    break;
                case "org":
                    server = "whois.pir.org";
                    break;
                case "ru":
                case "su":
                case "рф":
                    server = "whois.tcinet.ru";
                    break;
                case "kz":
                    server = "whois.nic.kz";
                    break;
                case "tel":
                    server = "whois.nic.tel";
                    break;
                case "biz":
                    server = "whois.biz";
                    break;
                case "mobi":
                    server = "whois.dotmobiregistry.net";
                    break;
                case "tv":
                    server = "tvwhois.verisign-grs.com";
                    break;
                case "me":
                    server = "whois.nic.me";
                    break;
                default:
                    break;
            }

            if (server == string.Empty)
            {
                // search whois service for top level domain
                res = WhoIsWho(domain, "whois.iana.org", 43);
                //return res;
                server = SearchValue(res, "whois");
            }


            res = WhoIsWho(domain, server, 43);
            
            if(res.IndexOf("\"=xxx\"")>=0)
            res = WhoIsWho(domain[0] == '=' ? domain : "=" + domain, server, 43); // =domainname.example

            //return res;

            WhoisInfo info = new WhoisInfo(res, domain);
            
            return info;
        }

        public string TopDomain(string domain)
        {
            int s = domain.LastIndexOf(".") + 1;
            return domain.Substring(s);
        }

        public string SearchValue(string data, string tag, bool matchcase = true)
        {
            string tmp = data;

            if (matchcase == false)
            {
                tag = tag.ToLower();
                tmp = data.ToLower();
            }

            int s = tmp.IndexOf(tag + ":");
            if (s < 0) return String.Empty;
            s += tag.Length+1;
            int e = tmp.IndexOf("\n", s);

            string res = data.Substring(s, e - s).Replace(" ", String.Empty).Trim();

            return res;
        }

        public string SearchValue2(string data, string tag, bool matchcase = true)
        {
            string tmp = data;

            if (matchcase == false)
            {
                tag = tag.ToLower();
                tmp = data.ToLower();
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

            string res = data.Substring(s2, e - s2).Replace(" ", String.Empty).Trim();

            return res;
        }

        private NameWhoIs.WhoIs srv = new NameWhoIs.WhoIs();

        public string WhoIsWho(string domain, string server, int port)
        {
            return srv.WhoIsWho(domain, server, port);
        }   
    }
}
