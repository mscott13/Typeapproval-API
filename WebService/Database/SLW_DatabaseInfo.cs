using System;
using System.Collections.Generic;
using WebService.Models;
using System.Data;
using System.Data.SqlClient;
using System.Globalization;
using System.Security.Cryptography;
using System.Text;
using WebService.Other;

namespace WebService.Database
{
    public class SLW_DatabaseInfo
    {
        string SLW_dbConn = Constants.databaseConnection;

        // Type approval search
        public List<TypeApprovalDetails> GetTypeApprovalInfo(string Dealer, string Model)
        {
            SqlConnection conn = new SqlConnection(SLW_dbConn);
            SqlCommand cmd = new SqlCommand();
            SqlDataReader reader = null;
            cmd.CommandText = "sp_getTypeApprovalDetails @Dealer, @Model";
            cmd.Parameters.AddWithValue("@Dealer", Dealer);
            cmd.Parameters.AddWithValue("@Model", Model);
            cmd.Connection = conn;
            List<TypeApprovalDetails> data = new List<TypeApprovalDetails>();
            conn.Open();

            reader = cmd.ExecuteReader();
            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    TypeApprovalDetails details = new TypeApprovalDetails();
                    details.clientCompany = reader["clientCompany"].ToString();
                    details.Dealer = reader["Dealer"].ToString();
                    details.Model = reader["Model"].ToString();
                    details.Description = reader["Description"].ToString();
                    details.Address2 = reader["Address2"].ToString();
                    details.Remarks = reader["Remarks"].ToString();
                    details.issueDate = reader.GetDateTime(6);
                    details.keyTypeApprovalID = Convert.ToInt32(reader["keyTypeApprovalID"]);
                    details.TableInfo = getTypeApprovalTableInfo(Convert.ToInt32(reader["keyTypeApprovalID"]));
                    data.Add(details);
                }
            }

            reader.Close();
            conn.Close();
            return data;
        }

        public DataTable getTypeApprovalTableInfo(int id)
        {
            SqlConnection conn = new SqlConnection(SLW_dbConn);
            SqlCommand cmd = new SqlCommand();
            SqlDataReader reader = null;
            cmd.CommandText = "sp_getTypeApprovalTableInfo @keyTypeApprovalID";
            cmd.Parameters.AddWithValue("@keyTypeApprovalID", id);
            cmd.Connection = conn;
            DataTable data = new DataTable();
            data.Columns.Add("LowerFrequency", typeof(string));
            data.Columns.Add("UpperFrequency", typeof(string));
            data.Columns.Add("PowerOutput", typeof(string));
            data.Columns.Add("FrequencyTolerance", typeof(string));
            data.Columns.Add("EmissionClass", typeof(string));

            conn.Open();

            reader = cmd.ExecuteReader();
            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    data.Rows.Add(reader["LowerFrequency"].ToString(),
                                  reader["UpperFrequency"].ToString(),
                                  reader["PowerOutput"].ToString(),
                                  reader["FrequencyTolerance"].ToString(),
                                  reader["EmissionClass"].ToString());
                }
            }

            reader.Close();
            conn.Close();

            return data;
        }

        public List<String> GetManufacturers(string query)
        {
            SqlConnection conn = new SqlConnection(SLW_dbConn);
            SqlCommand cmd = new SqlCommand();
            SqlDataReader reader = null;
            List<string> manufacturers = new List<string>();
            cmd.CommandText = "sp_getManufacturers @query";
            cmd.Parameters.AddWithValue("@query", query);

            cmd.Connection = conn;
            conn.Open();
            reader = cmd.ExecuteReader();

            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    manufacturers.Add(reader["Dealer"].ToString());
                }
            }
            reader.Close();
            conn.Close();
            return FixDuplicates(manufacturers); ;
        }

        public List<string> GetModels(string query)
        {
            SqlConnection conn = new SqlConnection(SLW_dbConn);
            SqlCommand cmd = new SqlCommand();
            SqlDataReader reader = null;
            List<string> models = new List<string>();
            cmd.CommandText = "sp_getModels @query";
            cmd.Parameters.AddWithValue("@query", query);

            cmd.Connection = conn;
            conn.Open();
            reader = cmd.ExecuteReader();

            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    models.Add(reader["Model"].ToString());
                }
            }
            reader.Close();
            conn.Close();
            return FixDuplicates(models);
        }

        public List<string> FixDuplicates(List<string> data)
        {
            List<string> group = new List<string>();
            List<string> duplicates = new List<string>();
            bool addToGroup = true;

            for (int i = 0; i < data.Count; i++)
            {
                addToGroup = true;
                if (group.Count == 0)
                {
                    group.Add(data[i]);
                }
                else
                {
                    for (int j = 0; j < group.Count; j++)
                    {
                        if (group[j].ToLower() == data[i].ToLower())
                        {
                            duplicates.Add(data[i]);
                            j = group.Count;
                            addToGroup = false;
                        }
                    }

                    if (addToGroup)
                    {
                        group.Add(data[i]);
                    }
                }
            }
            group.Sort((a, b) => a.CompareTo(b));
            return group;
        }

        // User management
        public UserCredentials GetUserCredentials(string user)
        {
            SqlConnection conn = new SqlConnection(SLW_dbConn);
            SqlCommand cmd = new SqlCommand();
            SqlDataReader reader = null;
            cmd.CommandText = "sp_getUserCredentials @user";
            cmd.Parameters.AddWithValue("@user", user);

            cmd.Connection = conn;
            conn.Open();
            reader = cmd.ExecuteReader();

            if (reader.HasRows)
            {
                reader.Read();
                UserCredentials credentials = new UserCredentials(reader["hash"].ToString(), Convert.ToBoolean(reader["password_reset_required"]), Convert.ToInt32(reader["user_type"]), reader["name"].ToString());
                conn.Close();
                return credentials;
            }
            else
            {
                conn.Close();
                return new UserCredentials();
            }
        }

        public void SetNewAccessKey(string user, string access_key)
        {

            SqlConnection conn = new SqlConnection(SLW_dbConn);
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "sp_newKey @username, @access_key, @last_detected_activity, @last_inactivity_mins";
            cmd.Parameters.AddWithValue("@username", user);
            cmd.Parameters.AddWithValue("@access_key", access_key);
            cmd.Parameters.AddWithValue("@last_detected_activity", DateTime.Now);
            cmd.Parameters.AddWithValue("@last_inactivity_mins", 45);

            cmd.Connection = conn;
            conn.Open();
            cmd.ExecuteNonQuery();
            conn.Close();
        }

        public void DeleteUser(string user)
        {
            SqlConnection conn = new SqlConnection(SLW_dbConn);
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "sp_deleteUser @user";
            cmd.Parameters.AddWithValue("@user", user);

            cmd.Connection = conn;
            conn.Open();
            cmd.ExecuteNonQuery();
            conn.Close();
        }

        public void ResetPassword(string user)
        {
            SqlConnection conn = new SqlConnection(SLW_dbConn);
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "sp_resetPassword @user";
            cmd.Parameters.AddWithValue("@user", user);

            cmd.Connection = conn;
            conn.Open();
            cmd.ExecuteNonQuery();
            conn.Close();
        }

        public void UpdatePassword(string user, string hash)
        {
            SqlConnection conn = new SqlConnection(SLW_dbConn);
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "sp_updatePassword @user, @hash";
            cmd.Parameters.AddWithValue("@user", user);
            cmd.Parameters.AddWithValue("@user", hash);

            cmd.Connection = conn;
            conn.Open();
            cmd.ExecuteNonQuery();
            conn.Close();
        }

        public bool CheckUserExist(string user)
        {
            SqlConnection conn = new SqlConnection(SLW_dbConn);
            SqlCommand cmd = new SqlCommand();
            SqlDataReader reader = null;
            cmd.CommandText = "sp_checkUserExist @user";
            cmd.Parameters.AddWithValue("@user", user);

            cmd.Connection = conn;
            conn.Open();
            reader = cmd.ExecuteReader();

            if (reader.HasRows)
            {
                reader.Read();
                if (Convert.ToInt32(reader["count"]) > 0)
                {
                    conn.Close();
                    return true;
                }
                else
                {
                    conn.Close();
                    return false;
                }
            }
            else
            {
                conn.Close();
                return false;
            }
        }

        public void NewUser(string username, string first_name, string last_name, DateTime created_date, int user_type, DateTime last_password_change_date,
                                  DateTime last_login_date, string hash, bool password_reset_required, string email, string company)
        {
            SqlConnection conn = new SqlConnection(SLW_dbConn);
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "sp_createUser @username, @first_name, @last_name, @created_date, @user_type, @last_password_change_date, " +
                              "@last_login_date, @hash, @password_reset_required, @email, @company";

            cmd.Parameters.AddWithValue("@username", username);
            cmd.Parameters.AddWithValue("@first_name", first_name);
            cmd.Parameters.AddWithValue("@last_name", last_name);
            cmd.Parameters.AddWithValue("@created_date", created_date);
            cmd.Parameters.AddWithValue("@user_type", user_type);
            cmd.Parameters.AddWithValue("@last_password_change_date", last_password_change_date);
            cmd.Parameters.AddWithValue("@last_login_date", last_login_date);
            cmd.Parameters.AddWithValue("@hash", hash);
            cmd.Parameters.AddWithValue("@password_reset_required", password_reset_required);
            cmd.Parameters.AddWithValue("@email", email);
            cmd.Parameters.AddWithValue("@company", company);

            cmd.Connection = conn;
            conn.Open();
            cmd.ExecuteNonQuery();
            conn.Close();
        }

        public int GetUserType(string access_key)
        {
            SqlConnection conn = new SqlConnection(SLW_dbConn);
            SqlCommand cmd = new SqlCommand();
            SqlDataReader reader = null;
            cmd.CommandText = "sp_getUserTypeByKey @key";
            cmd.Parameters.AddWithValue("@key", access_key);
            int user_type = -1;

            cmd.Connection = conn;
            conn.Open();
            reader = cmd.ExecuteReader();

            if (reader.HasRows)
            {
                reader.Read();
                user_type = Convert.ToInt32(reader["user_type"]);
            }

            conn.Close();
            return user_type;
        }

        public List<UserType> GetUserTypes()
        {
            SqlConnection conn = new SqlConnection(SLW_dbConn);
            SqlCommand cmd = new SqlCommand();
            SqlDataReader reader = null;
            cmd.CommandText = "sp_getUserTypes";
            List<UserType> user_types = new List<UserType>();

            cmd.Connection = conn;
            conn.Open();
            reader = cmd.ExecuteReader();

            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    user_types.Add(new UserType(Convert.ToInt32(reader["user_type"]), reader["description"].ToString()));
                }

                conn.Close();
                return user_types;
            }
            else
            {
                conn.Close();
                return user_types;
            }
        }

        public void SetNewUserType(int user_type, string description)
        {
            SqlConnection conn = new SqlConnection(SLW_dbConn);
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "sp_newUserType @user_type, @description";

            cmd.Parameters.AddWithValue("@user_type", user_type);
            cmd.Parameters.AddWithValue("@description", description);

            cmd.Connection = conn;
            conn.Open();
            cmd.ExecuteNonQuery();
            conn.Close();
        }

        public KeyDetail GetKeyDetail(string key)
        {
            SqlConnection conn = new SqlConnection(SLW_dbConn);
            SqlCommand cmd = new SqlCommand();
            SqlDataReader reader = null;
            cmd.CommandText = "sp_getKeyDetail @key";
            cmd.Parameters.AddWithValue("@key", key);

            cmd.Connection = conn;
            conn.Open();
            reader = cmd.ExecuteReader();

            if (reader.HasRows)
            {
                reader.Read();
               
                KeyDetail key_detail = new KeyDetail(Convert.ToInt32(reader["user_id"]), reader["access_key"].ToString(), Convert.ToDateTime(reader["last_detected_activity"]), Convert.ToInt32(reader["max_inactivity_mins"]));
                conn.Close();
                return key_detail;
            }
            else
            {
                conn.Close();
                return new KeyDetail();
            }
        }

        // Notifications
        public List<Notification> GetUnreadNotifications()
        {
            SqlConnection conn = new SqlConnection(SLW_dbConn);
            SqlCommand cmd = new SqlCommand();
            SqlDataReader reader = null;
            cmd.CommandText = "sp_getUnreadNotifications";
            List<Notification> notifications = new List<Notification>();

            cmd.Connection = conn;
            conn.Open();
            reader = cmd.ExecuteReader();

            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    notifications.Add(new Notification(reader["notification_id"].ToString(), Convert.ToDateTime(reader["received_date"]),
                                      Convert.ToDateTime(reader["read_date"]), reader["type"].ToString(), reader["target_user"].ToString(), 
                                      Convert.ToBoolean(reader["status_read"]), reader["message"].ToString()));
                }
                 
            }
            conn.Close();
            return notifications;
        }

        public void NotifySpecific(string message, string target_user, string type)
        {
            string datetime = DateTime.UtcNow.ToString("yyyy-MM-dd HH:mm:ss.fff", CultureInfo.InvariantCulture);
            StringBuilder builder = new StringBuilder();

            using (SHA256 hash = SHA256.Create())
            {
                byte[] bytes = hash.ComputeHash(Encoding.UTF8.GetBytes(datetime + message + target_user));

                for(int i=0; i<bytes.Length; i++)
                {
                    builder.Append(bytes[i].ToString("x2"));
                }
            }

            var result = builder.ToString();

            SqlConnection conn = new SqlConnection(SLW_dbConn);
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "sp_newMessage @notification_id, @received_date, @type, @target_user, @status_read, @message";
            cmd.Parameters.AddWithValue("@notification_id", result);
            cmd.Parameters.AddWithValue("@received_date", DateTime.Now);
            cmd.Parameters.AddWithValue("@type", type);
            cmd.Parameters.AddWithValue("@target_user", target_user);
            cmd.Parameters.AddWithValue("@status_read", false);
            cmd.Parameters.AddWithValue("@message", message);

            cmd.Connection = conn;
            conn.Open();
            cmd.ExecuteNonQuery();
            conn.Close();
        }

        //ClientCompanyData
        public List<ClientCompany> getClientDetails(int clientId)
        {
            SqlConnection conn = new SqlConnection(SLW_dbConn);
            SqlCommand cmd = new SqlCommand();
            SqlDataReader reader = null;
            cmd.CommandText = "sp_getClientDetails @clientId";
            cmd.Parameters.AddWithValue("@clientId", clientId);
            cmd.Connection = conn;
            List<ClientCompany> clientCompanies = new List<ClientCompany>();

            conn.Open();
            reader = cmd.ExecuteReader();

            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    clientCompanies.Add(new ClientCompany(reader["clientId"].ToString(), reader["clientCompany"].ToString(), reader["clientTelNum"].ToString(),
                                                          reader["address"].ToString(), reader["clientFaxNum"].ToString(), "city/town", "contact person", reader["nationality"].ToString()));
                }
            }
            conn.Close();
            return clientCompanies;
        }
        
    }
}