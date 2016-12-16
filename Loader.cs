using System;
using System.Net;
using System.Xml.Linq;

namespace FuryTech.OdataTypescriptServiceGenerator
{
    public class Loader
    {
        public static XDocument Load(string source)
        {
            Logger.Log("Loading Metadata...");
            XDocument xml;

            try
            {
                xml = XDocument.Load(source);
                return xml;
            }
            catch (WebException wex)
            {
                if (wex.Message == "The remote server returned an error: (401) Unauthorized.")
                {
                    Logger.Warning("Authorization error, trying NTLM...");
                    var req = WebRequest.Create(source);
                    req.Credentials = CredentialCache.DefaultCredentials;
                    var stream = req.GetResponse().GetResponseStream();
                    if (stream != null)
                    {
                        xml = XDocument.Load(stream);
                        return xml;
                    }
                }
                Logger.Error("Failed to read metadata.");
                throw;
            }
            catch (Exception)
            {
                Logger.Error("Error reading metadata.");
                throw;
            }
        }
    }
}
