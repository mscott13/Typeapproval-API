using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Security.Cryptography;
using System.Data.SqlClient;
using System.Text;

namespace WebService.Utilities
{
    public class PasswordManager
    {
        public string GetHash(string pswd)
        {
            RNGCryptoServiceProvider rng = new RNGCryptoServiceProvider();
            byte[] salt = new byte[20];
            rng.GetBytes(salt);

            var rfc2898 = new Rfc2898DeriveBytes(pswd, salt, 10000);
            byte[] hash = rfc2898.GetBytes(20);

            byte[] combine = new byte[40];
            Array.Copy(salt, 0, combine, 0, 20);
            Array.Copy(hash, 0, combine, 20, 20);

            var result = Convert.ToBase64String(combine);
            return result;
        }

        public bool VerifyCredentials(string pswd, string dbhash)
        {
            try
            {
                byte[] combine = Convert.FromBase64String(dbhash);
                byte[] salt = new byte[20];

                Array.Copy(combine, 0, salt, 0, 20);
                var rfc2898 = new Rfc2898DeriveBytes(pswd, salt, 10000);
                byte[] pwhash = rfc2898.GetBytes(20);

                for (int i = 0; i < 20; i++)
                {
                    if (combine[i + 20] != pwhash[i])
                    {
                        throw new UnauthorizedAccessException();
                    }
                }
                return true;
            }
            catch (Exception e)
            {
                string errorMsg = e.Message;
                return false;
            }
        }

        public void ResetPassword(string user)
        {
            Database.SLW_DatabaseInfo db = new Database.SLW_DatabaseInfo();
            db.ResetPassword(user);
        }

        public bool ChangePassword(string user, string old_psw, string new_psw)
        {
            Database.SLW_DatabaseInfo db = new Database.SLW_DatabaseInfo();
            Models.UserCredentials credentials = db.GetUserCredentials(user);

            if(VerifyCredentials(old_psw, credentials.hash))
            {

                db.UpdatePassword(user, GetHash(new_psw));
                return true;
            }
            else
            {
                return false;
            }
        }

        public string GenerateNewAccessKey(string user)
        {
            byte[] uname = Encoding.ASCII.GetBytes(user); 
            byte[] time = BitConverter.GetBytes(DateTime.UtcNow.ToBinary());
            byte[] guid = Guid.NewGuid().ToByteArray();
            string key = Convert.ToBase64String(time.Concat(guid).Concat(uname).ToArray());

            return key;
        }
    }
}