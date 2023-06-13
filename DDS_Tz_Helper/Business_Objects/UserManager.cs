using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.DirectoryServices;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;



namespace DDS_Tz_Helper.Business_Objects
{
    public class UserManager
    {
        public int user_id;
        public int role_id;
        public String role_name;
        public bool admin = false;
        public static Image user_picture = null;

        public bool change_password = false;

        public string Hash(string password)
        {
            var bytes = new UTF8Encoding().GetBytes(password);
            var hashBytes = System.Security.Cryptography.MD5.Create().ComputeHash(bytes);
            return Convert.ToBase64String(hashBytes);
        }
        public bool IsValid(string username, string password, String BaseUrl)
        {
            using (var db = new DispatchDBEntities()) // use your DbConext
            {
                String HashedPassword = Hash(password);

                // if your users set name is Users

                bool isValidStatus = db.users.Any(u => u.username == username
                    && u.password == HashedPassword && u.active == true && u.approved == true && u.username != "system" && u.status == "ACTIVE" && u.can_approve == true);


                bool isDisabledStatus = db.users.Any(u => u.username == username
                    && u.password == HashedPassword && u.active == true && u.approved == true && u.username != "system" && u.status == "DISABLED");

                bool isCanApproveStatus = db.users.Any(u => u.username == username
                    && u.password == HashedPassword && u.active == true && u.approved == true && u.username != "system" && u.can_approve == false);

                if (isDisabledStatus)
                {
                    return false;
                }

                if (isCanApproveStatus)
                {
                    return false;
                }





                //--return isValidStatus;
                //TODO : Checking AD Authentication

                //if (!isValidStatus)
                //{
                //    //check via Active Directory
                //    String path = "LDAP://dc=dangote-group,dc=com";
                //    LdapAuthentication ldp = new LdapAuthentication(path);

                //    bool ad_login_status = ldp.IsAuthenticated("dangote_nt", BaseUrl, username, password);

                //    if (ad_login_status) //successful ad login
                //    {

                //        user new_user = new user();
                //        new_user.username = username;
                //        new_user.password = HashedPassword;
                //        new_user.active = true;
                //        new_user.role_id = db.roles.Where(a => a.role_name == "newuser").FirstOrDefault().Id;
                //        new_user.admin = false;
                //        db.users.Add(new_user);

                //        db.SaveChanges();

                //        return true;

                //    }
                //}
                return isValidStatus;
            }
        }
        public void FillUserInfo(string username)
        {
            using (var db = new DispatchDBEntities()) // use your DbConext
            {
                // if your users set name is Users
                user current_user = db.users.FirstOrDefault(u => u.username == username);
                user_id = current_user.Id;
                //role_name = current_user.role.role_name;
                //role_id = current_user.role.Id;
                admin = current_user.admin.Value;


                    current_user.last_login = DateTime.Now;

                    db.Entry(current_user).State = EntityState.Modified;
                    db.SaveChanges();
                //}
            }
        }
    }
}