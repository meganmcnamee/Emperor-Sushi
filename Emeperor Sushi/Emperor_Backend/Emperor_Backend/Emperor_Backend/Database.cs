using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Security.Cryptography;

namespace Emperor_Backend
{
    public class Database
    {
        public DatabaseModel databaseModel;
        public Database()
        {
            databaseModel = new DatabaseModel();
        }
        ~Database()
        {
            databaseModel.Dispose();
        }

        public bool UserExists(string Username)
        {
            return databaseModel.Users.SingleOrDefault(x => x.Username == Username) != null;
        }

        public byte[] GenerateHash(string Username, string Password)
        {
            var PasswordData = new byte[Username.Length + Password.Length];
            Array.Copy(Encoding.UTF8.GetBytes(Username), PasswordData, 0);
            Array.Copy(Encoding.UTF8.GetBytes(Password), PasswordData, Username.Length);
            SHA256 mySHA256 = SHA256.Create();
            return mySHA256.ComputeHash(PasswordData);
        }

        public bool GenerateUserAccount(string Username, string Password)
        {
            if (UserExists(Username))
                return false;

            var PasswordHash = GenerateHash(Username, Password);

            databaseModel.Users.Add(new User() { Username = Username, Password = Convert.ToBase64String(PasswordHash), SessionKey = "", LastSeen = DateTime.Now });
            databaseModel.SaveChanges();
            return true;
        }

        public bool VerifyAccount(string Username, string Password)
        {
            if (!UserExists(Username))
                return false;

            var PasswordHash = Convert.ToBase64String(GenerateHash(Username, Password));

            var user = databaseModel.Users.SingleOrDefault(x => x.Username == Username);

            if(user != null)
            {
                if (user.Password == PasswordHash)
                    return true;
            }

            return false;
        }

        public string GenerateSessionKey(int length)
        {
            var key = new byte[length];

            using (var rng = new RNGCryptoServiceProvider())
            {
                rng.GetBytes(key);
            }

            return Convert.ToBase64String(key);
        }

        public string Login(string Username, string Password)
        {
            if(VerifyAccount(Username, Password))
            {
                var sessionKey = GenerateSessionKey(32);
                var user = databaseModel.Users.SingleOrDefault(x => x.Username == Username);
                if(user != null)
                {
                    user.SessionKey = sessionKey;
                    user.LastSeen = DateTime.Now;
                    databaseModel.SaveChanges();
                    return sessionKey;
                }
            }
            return null;
        }

        public bool Logout(string SessionKey)
        {
            var user = databaseModel.Users.SingleOrDefault(x => x.SessionKey == SessionKey);
            if(user != null)
            {
                user.SessionKey = "";
                user.LastSeen = DateTime.Now;
                databaseModel.SaveChanges();
                return true;
            }
            return false;
        }

        public bool VerifySession(string SessionKey)
        {
            var user = databaseModel.Users.SingleOrDefault(x => x.SessionKey == SessionKey);
            if (user != null)
            {
                return true;
            }
            return false;
        }

        public string UpdateSession(string SessionKey)
        {
            var user = databaseModel.Users.SingleOrDefault(x => x.SessionKey == SessionKey);
            if (user != null)
            {
                var sessionKey = GenerateSessionKey(32);
                user.SessionKey = sessionKey;
                databaseModel.SaveChanges();
                return sessionKey;
            }
            return null;
        }
    }
}
