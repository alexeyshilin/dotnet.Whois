using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Net.Sockets;
using NameWhoIs;
using System.Text.RegularExpressions;


namespace NameWhoIs
{
    public class WhoIs
    {
        // Networking and IO objects we'll be using throughout the application
        TcpClient tcpWhois;

        NetworkStream nsWhois;
        BufferedStream bfWhois;

        StreamWriter swSend;
        StreamReader srReceive;

        public string WhoIsWho(string domain, string server = "whois.internic.net", int port = 43)
        {
            // Answer Server
            string txtResponse;

            try
            {
                // The TcpClient should connect to the who-is server, on port 43 (default who-is)
                //tcpWhois = new TcpClient("whois.internic.net", 43);
                tcpWhois = new TcpClient(server, port); // whois.iana.org whois.internic.net whois.nic.ru whois.reg.ru whois.ripn.net whois.centrohost.ru whois.godaddy.com
                // Set up the network stream
                nsWhois = tcpWhois.GetStream();
                // Hook up the buffered stream to the network stream
                bfWhois = new BufferedStream(nsWhois);
            }
            catch
            {
                txtResponse = "Could not open a connection to the Who-Is server.";
            }

            // Send to the server the host-name that we want to get information on
            swSend = new StreamWriter(bfWhois);
            swSend.WriteLine(domain);
            swSend.Flush();

            // Clear the textbox of anything existing content
            txtResponse = "";
           
            try
            {
                srReceive = new StreamReader(bfWhois);
                string strResponse;
                // Read the response line by line and put it in the textbox
                while ((strResponse = srReceive.ReadLine()) != null)
                {
                    txtResponse += strResponse + "\r\n";
                }
            }
            catch
            {
                txtResponse = "Could not read data from the Who-Is server.";
            }
            // We're done with the connection
            tcpWhois.Close();

            return txtResponse;
        }


        public string DomenState(string domen)
        {
            string answer = WhoIsWho(domen);

            Match match = Regex.Match(answer, "No match for \"(.*)\".");
            if (match.Groups[1].ToString().ToLower() == domen.ToLower())
                return "Свободен";
            else
                return "Занят";
        }


    }
}