using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.DirectoryServices;
using System.DirectoryServices.Protocols;
using System.IO;
using System.Net;
using System.Text;


namespace DDS_Tz_Helper.Business_Objects
{
    public class LdapAuthentication
    {
        private String _path;
        private String _filterAttribute;

        public LdapAuthentication(String path)
        {
            _path = path;
        }

        public bool IsAuthenticated(String domain, String BaseUrl, String username, String pwd)
        {
            String domainAndUsername = domain + @"\" + username;
            DirectoryEntry entry = new DirectoryEntry(_path, domainAndUsername, pwd);

            //////////////////////////// See http://support.microsoft.com/kb/218185 for full list of LDAP error codes
            //const int ldapErrorInvalidCredentials = 0x31;

            //const string server = "dangote-group.com";
            //const string domain1 = "dangote-group.com";

            //try
            //{
            //    using (var ldapConnection = new LdapConnection(server))
            //    {
            //        var networkCredential = new NetworkCredential(username, pwd, domain1);
            //        ldapConnection.SessionOptions.SecureSocketLayer = false;
            //        ldapConnection.AuthType = AuthType.Negotiate;
            //        ldapConnection.Bind(networkCredential);
            //    }

            //    // If the bind succeeds, the credentials are valid
            //    return true;
            //}
            //catch (LdapException ldapException)
            //{
            //    // Invalid credentials throw an exception with a specific error code
            //    if (ldapException.ErrorCode.Equals(ldapErrorInvalidCredentials))
            //    {
            //        return false;
            //    }

            //    throw;
            //}

            //////////////////////////

            try
            {//Bind to the native AdsObject to force authentication.
                Object obj = entry.NativeObject;

                DirectorySearcher search = new DirectorySearcher(entry);

                search.Filter = "(SAMAccountName=" + username + ")";
                search.PropertiesToLoad.Add("cn");
                SearchResult result = search.FindOne();

                if (null == result)
                {
                    return false;
                }

                //Update the new path to the user in the directory.
                _path = result.Path;
                _filterAttribute = (String)result.Properties["cn"][0];


                //using (DirectoryEntry user = new DirectoryEntry(result.Path))
                //{
                //    foreach (var item in user.Properties.PropertyNames)
                //    {
                //        System.Diagnostics.Debug.WriteLine(item.ToString());
                //    }
                //    byte[] data = user.Properties["thumbnailPhoto"].Value as byte[];

                //    if (data != null)
                //    {
                //        using (MemoryStream s = new MemoryStream(data))
                //        {
                //            String filename = BaseUrl + "\\Content\\Data\\" + username + ".jpg";
                //            System.Drawing.Bitmap.FromStream(s).Save(filename);
                //        }
                //    }

                //    //return null;
                //}
            }
            catch (Exception ex)
            {
                return false;
                //throw new Exception("Error authenticating user. " + ex.Message);
            }

            return true;
        }

        public String GetGroups()
        {
            DirectorySearcher search = new DirectorySearcher(_path);
            search.Filter = "(cn=" + _filterAttribute + ")";
            search.PropertiesToLoad.Add("memberOf");
            StringBuilder groupNames = new StringBuilder();

            try
            {
                SearchResult result = search.FindOne();

                int propertyCount = result.Properties["memberOf"].Count;

                String dn;
                int equalsIndex, commaIndex;

                for (int propertyCounter = 0; propertyCounter < propertyCount; propertyCounter++)
                {
                    dn = (String)result.Properties["memberOf"][propertyCounter];

                    equalsIndex = dn.IndexOf("=", 1);
                    commaIndex = dn.IndexOf(",", 1);
                    if (-1 == equalsIndex)
                    {
                        return null;
                    }

                    groupNames.Append(dn.Substring((equalsIndex + 1), (commaIndex - equalsIndex) - 1));
                    groupNames.Append("|");

                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error obtaining group names. " + ex.Message);
            }
            return groupNames.ToString();
        }
    }
}