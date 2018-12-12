using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Globalization;
using System.Security.Cryptography;
using System.Text;
using WebService.Models;
using WebService.Commons;

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
                    details.TableInfo = GetTypeApprovalTableInfo(Convert.ToInt32(reader["keyTypeApprovalID"]));
                    data.Add(details);
                }
            }

            reader.Close();
            conn.Close();
            return data;
        }

        public DataTable GetTypeApprovalTableInfo(int id)
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

        public List<string> GetManufacturersByName(string query)
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

        public List<Manufacturer> GetManufacturers(string query)
        {
            SqlConnection conn = new SqlConnection(SLW_dbConn);
            SqlCommand cmd = new SqlCommand();
            SqlDataReader reader = null;
            List<Manufacturer> manufacturers = new List<Manufacturer>();
            cmd.CommandText = "sp_getManufacturers @query";
            cmd.Parameters.AddWithValue("@query", query);

            cmd.Connection = conn;
            conn.Open();
            reader = cmd.ExecuteReader();

            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    manufacturers.Add(new Manufacturer(reader["Dealer"].ToString(), reader["Address2"].ToString(), reader["TelNum"].ToString(), reader["FaxNum"].ToString(), ""));
                }
            }
            reader.Close();
            conn.Close();
            
            return FixDuplicates(manufacturers);
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

        public List<Manufacturer> FixDuplicates(List<Manufacturer> data)
        {
            List<Manufacturer> group = new List<Manufacturer>();
            List<Manufacturer> duplicates = new List<Manufacturer>();
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
                        if (group[j].name.ToLower() == data[i].name.ToLower())
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
            group.Sort((a, b) => a.name.CompareTo(b.name));
            return group;
        }

        public ApplicationSearchResultMain GetMultiSearchApplicationResults(string q)
        {
            List<string> manufaturers = GetManufacturersByName(q);
            List<string> models = GetModels(q);
            List<SearchCategory> items = new List<SearchCategory>();
            for (int i = 0; i < manufaturers.Count; i++)
            {
                items.Add(new SearchCategory(manufaturers[i], "http://localhost:63616/search?dealer="+manufaturers[i]+"&model=", "", "Manufacturers"));
            }

            for (int i = 0; i < models.Count; i++)
            {
                items.Add(new SearchCategory(models[i], "http://localhost:63616/search?dealer=&model="+models[i], "", "Models"));
            }

            return new ApplicationSearchResultMain(items);
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
                                  DateTime last_login_date, string hash, bool password_reset_required, string email, string company, int clientId)
        {
            SqlConnection conn = new SqlConnection(SLW_dbConn);
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "sp_createUser @username, @first_name, @last_name, @created_date, @user_type, @last_password_change_date, " +
                              "@last_login_date, @hash, @password_reset_required, @email, @company, @clientId";

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
            cmd.Parameters.AddWithValue("@clientId", clientId);

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

                KeyDetail key_detail = new KeyDetail(Convert.ToInt32(reader["user_id"]), reader["username"].ToString(), reader["access_key"].ToString(), Convert.ToDateTime(reader["last_detected_activity"]), Convert.ToInt32(reader["max_inactivity_mins"]));
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

                for (int i = 0; i < bytes.Length; i++)
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

        //Data
        public List<ClientCompany> GetClientDetails(string query)
        {
            SqlConnection conn = new SqlConnection(SLW_dbConn);
            SqlCommand cmd = new SqlCommand();
            SqlDataReader reader = null;
            cmd.CommandText = "sp_getClientDetails @clientCompany";
            cmd.Parameters.AddWithValue("@clientCompany", query);
            cmd.Connection = conn;
            List<ClientCompany> clientCompanies = new List<ClientCompany>();

            conn.Open();
            reader = cmd.ExecuteReader();

            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    clientCompanies.Add(new ClientCompany(reader["clientId"].ToString(), reader["clientCompany"].ToString(), reader["clientTelNum"].ToString(),
                                                          reader["address"].ToString(), reader["clientFaxNum"].ToString(), "", "", reader["nationality"].ToString()));
                }
            }
            conn.Close();
            return clientCompanies;
        }

        public ClientCompany GetClientDetail(int clientId)
        {
            SqlConnection conn = new SqlConnection(SLW_dbConn);
            SqlCommand cmd = new SqlCommand();
            SqlDataReader reader = null;
            cmd.CommandText = "sp_getClientDetail @clientId";
            cmd.Parameters.AddWithValue("@clientId", clientId);
            cmd.Connection = conn;
            ClientCompany clientCompany;

            conn.Open();
            reader = cmd.ExecuteReader();

            if (reader.HasRows)
            {
                reader.Read();
                clientCompany = new ClientCompany(reader["clientId"].ToString(), reader["clientCompany"].ToString(), reader["clientTelNum"].ToString(), reader["address"].ToString(), reader["clientFaxNum"].ToString(), "", "", reader["nationality"].ToString());
                conn.Close();
                return clientCompany;
            }
            else
            {
                conn.Close();
                return null;
            }
        }

        public AssignedCompany GetAssignedCompany(string username)
        {
            SqlConnection conn = new SqlConnection(SLW_dbConn);
            SqlCommand cmd = new SqlCommand();
            SqlDataReader reader = null;
            cmd.CommandText = " sp_getAssignedCompany @username";
            cmd.Parameters.AddWithValue("@username", username);
            cmd.Connection = conn;
            AssignedCompany company = new AssignedCompany();

            conn.Open();
            reader = cmd.ExecuteReader();

            if (reader.HasRows)
            {
                reader.Read();
                company.clientId = Convert.ToInt32(reader["clientId"]);
                company.company = reader["company"].ToString();
                conn.Close();
                return company;
            }
            else
            {
                conn.Close();
                return null;
            }
        }

        public void SaveApplication(Form form)
        {
            SqlConnection conn = new SqlConnection(SLW_dbConn);
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "sp_saveFormDetails @applicationId, @username, @applicant_name, @applicant_tel, @applicant_address," +
                              "@applicant_fax, @applicant_city_town, @applicant_contact_person, @applicant_nationality, " +
                              "@manufacturer_name, @manufacturer_tel, @manufacturer_address, @manufacturer_fax," +
                              "@manufacturer_contact_person," +
                              "@equipment_type, @equipment_description," +
                              "@product_identification, @ref#, @make, @software, @type_of_equipment," +
                              "@other, @antenna_type, @antenna_gain, @channel,@separation, @aspect," +
                              "@compatibility, @security, @equipment_comm_type, @fee_code, @status";

            cmd.Parameters.AddWithValue("@applicationId", form.application_id);
            cmd.Parameters.AddWithValue("@username", form.username);
            cmd.Parameters.AddWithValue("@applicant_name", form.applicant_name);
            cmd.Parameters.AddWithValue("@applicant_tel", form.applicant_tel);
            cmd.Parameters.AddWithValue("@applicant_address", form.applicant_address);
            cmd.Parameters.AddWithValue("@applicant_fax", form.applicant_fax);
            cmd.Parameters.AddWithValue("@applicant_city_town", form.applicant_city_town);
            cmd.Parameters.AddWithValue("@applicant_contact_person", form.applicant_contact_person);
            cmd.Parameters.AddWithValue("@applicant_nationality", form.applicant_nationality);
            cmd.Parameters.AddWithValue("@manufacturer_name", form.manufacturer_name);
            cmd.Parameters.AddWithValue("@manufacturer_tel", form.manufacturer_tel);
            cmd.Parameters.AddWithValue("@manufacturer_address", form.manufacturer_address);
            cmd.Parameters.AddWithValue("@manufacturer_fax", form.manufacturer_fax);
            cmd.Parameters.AddWithValue("@manufacturer_contact_person", form.manufacturer_contact_person);
            cmd.Parameters.AddWithValue("@equipment_type", form.equipment_type);
            cmd.Parameters.AddWithValue("@equipment_description", form.equipment_description);
            cmd.Parameters.AddWithValue("@product_identification", form.product_identification);
            cmd.Parameters.AddWithValue("@ref#", form.refNum);
            cmd.Parameters.AddWithValue("@make", form.make);
            cmd.Parameters.AddWithValue("@software", form.software);
            cmd.Parameters.AddWithValue("@type_of_equipment", form.type_of_equipment);
            cmd.Parameters.AddWithValue("@other", form.other);
            cmd.Parameters.AddWithValue("@antenna_type", form.antenna_type);
            cmd.Parameters.AddWithValue("@antenna_gain", form.antenna_gain);
            cmd.Parameters.AddWithValue("@channel", form.channel);
            cmd.Parameters.AddWithValue("@separation", form.separation);
            cmd.Parameters.AddWithValue("@aspect", form.aspect);
            cmd.Parameters.AddWithValue("@compatibility", form.compatibility);
            cmd.Parameters.AddWithValue("@security", form.security);
            cmd.Parameters.AddWithValue("@equipment_comm_type", form.equipment_comm_type);
            cmd.Parameters.AddWithValue("@fee_code", form.fee_code);
            cmd.Parameters.AddWithValue("@status", form.status);

            cmd.Connection = conn;
            conn.Open();
            cmd.ExecuteNonQuery();
            conn.Close();

            for (int i = 0; i < form.frequencies.Count; i++)
            {
                form.frequencies[i].application_id = form.application_id;
            }

            if (form.frequencies.Count > 0)
            {
                SaveFrequencies(form.frequencies);
            }
        }

        private void SaveFrequencies(List<Frequency> frequencies)
        {
            DeleteFrequencies(frequencies[0].application_id);
            SqlConnection conn = new SqlConnection(SLW_dbConn);
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "sp_saveFrequencyDetails @applicationId, @sequence, @lower_freq, @upper_freq, @power, @tolerance, @emmission_desig, @freq_type";
            cmd.Connection = conn;
            conn.Open();

            for (int i = 0; i < frequencies.Count; i++)
            {
                cmd.Parameters.AddWithValue("@applicationId", frequencies[i].application_id);
                cmd.Parameters.AddWithValue("@sequence", frequencies[i].sequence);
                cmd.Parameters.AddWithValue("@lower_freq", frequencies[i].lower_freq);
                cmd.Parameters.AddWithValue("@upper_freq", frequencies[i].upper_freq);
                cmd.Parameters.AddWithValue("@power", frequencies[i].power);
                cmd.Parameters.AddWithValue("@emmission_desig", frequencies[i].emmission_desig);
                cmd.Parameters.AddWithValue("@tolerance", frequencies[i].tolerance);
                cmd.Parameters.AddWithValue("@freq_type", frequencies[i].freq_type);

                cmd.ExecuteNonQuery();
                cmd.Parameters.Clear();
            }
            conn.Close();
        }

        private void DeleteFrequencies(string application_id)
        {
            SqlConnection conn = new SqlConnection(SLW_dbConn);
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "sp_deleteAllFreqs @applicationId";
            cmd.Connection = conn;

            conn.Open();
            cmd.Parameters.AddWithValue("@applicationId", application_id);
            cmd.ExecuteNonQuery();
            cmd.Parameters.Clear();
            conn.Close();
        }

        public void SaveActivity(Models.UserActivity activity)
        {
            SqlConnection conn = new SqlConnection(SLW_dbConn);
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "sp_newUserActivity @username, @type, @description, @extras, @priority";

            cmd.Parameters.AddWithValue("@username", activity.username);
            cmd.Parameters.AddWithValue("@type", activity.type);
            cmd.Parameters.AddWithValue("@description", activity.description);
            cmd.Parameters.AddWithValue("@extras", activity.extras);
            cmd.Parameters.AddWithValue("@priority", activity.priority);
            cmd.Connection = conn;

            conn.Open();
            cmd.ExecuteNonQuery();
            conn.Close();
        }

        public List<Models.UserActivity> GetUserActivities(string _username_)
        {
            int _priority_ = 1;
            SqlConnection conn = new SqlConnection(SLW_dbConn);
            SqlCommand cmd = new SqlCommand();
            SqlDataReader reader = null;
            cmd.CommandText = " sp_getUserActivity @username, @priority";

            List<Models.UserActivity> userActivities = new List<Models.UserActivity>();
            cmd.Parameters.AddWithValue("@username", _username_);
            cmd.Parameters.AddWithValue("@priority", _priority_);
            cmd.Connection = conn;

            conn.Open();
            reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                int sequence = Convert.ToInt32(reader["sequence"]);
                int priority = Convert.ToInt32(reader["priority"]);
                string username = reader["username"].ToString();
                string type = reader["type"].ToString();
                string created_date = Convert.ToDateTime(reader["created_date"]).ToShortDateString()+" "+ Convert.ToDateTime(reader["created_date"]).ToShortTimeString();
                string description = reader["description"].ToString();
                string extras = reader["extras"].ToString();
                string current_status = reader["current_status"].ToString();

                userActivities.Add(new Models.UserActivity(username, type, description, extras, priority, created_date, current_status));
            }
            return userActivities;
        }

        public List<RecentDocuments> GetRecentDocuments(string _username_)
        {
            SqlConnection conn = new SqlConnection(SLW_dbConn);
            SqlCommand cmd = new SqlCommand();
            SqlDataReader reader = null;
            cmd.CommandText = " sp_getRecentDocs @username, @days_range";

            List<RecentDocuments> recentDocuments = new List<RecentDocuments>();
            cmd.Parameters.AddWithValue("@username", _username_);
            cmd.Parameters.AddWithValue("@days_range", 10);
            cmd.Connection = conn;

            conn.Open();
            reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                string application_id = reader["application_id"].ToString();
                string created_date = Convert.ToDateTime(reader["created_date"]).ToShortDateString()+" " + Convert.ToDateTime(reader["created_date"]).ToShortTimeString();
                string last_update = Convert.ToDateTime(reader["last_updated"]).ToShortDateString() + " " + Convert.ToDateTime(reader["last_updated"]).ToShortTimeString();
                string status = reader["status"].ToString();
                string current_status = reader["current_status"].ToString();
                recentDocuments.Add(new RecentDocuments(application_id, created_date, status,last_update, current_status));
            }
            conn.Close();
            return recentDocuments;
        }

        public List<SavedApplications> GetSavedApplications(string _username_)
        {
            SqlConnection conn = new SqlConnection(SLW_dbConn);
            SqlCommand cmd = new SqlCommand();
            SqlDataReader reader = null;
            cmd.CommandText = " sp_getSavedApplications @username";

            List<SavedApplications> savedApplications = new List<SavedApplications>();
            cmd.Parameters.AddWithValue("@username", _username_);
            cmd.Connection = conn;

            conn.Open();
            reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                string application_id = reader["application_id"].ToString();
                string created_date = Convert.ToDateTime(reader["created_date"]).ToShortDateString() + " " + Convert.ToDateTime(reader["created_date"]).ToShortTimeString();
                string last_update = Convert.ToDateTime(reader["last_updated"]).ToShortDateString() + " " + Convert.ToDateTime(reader["last_updated"]).ToShortTimeString();
              
                savedApplications.Add(new SavedApplications(application_id, created_date, last_update));
            }
            conn.Close();
            return savedApplications;
        }

        public Form GetApplication(string application_id)
        {
            SqlConnection conn = new SqlConnection(SLW_dbConn);
            SqlCommand cmd = new SqlCommand();
            SqlDataReader reader = null;
            cmd.CommandText = "sp_getForm @application_id";

            Form form = new Form();
            cmd.Parameters.AddWithValue("@application_id", application_id);
            cmd.Connection = conn;

            conn.Open();
            reader = cmd.ExecuteReader();

            if (reader.HasRows)
            {
                reader.Read();
                form.application_id = application_id;
                form.applicant_name = reader["applicant_name"].ToString();
                form.applicant_tel = reader["applicant_tel"].ToString();
                form.applicant_address = reader["applicant_address"].ToString();
                form.applicant_fax = reader["applicant_fax"].ToString();
                form.applicant_contact_person = reader["applicant_contact_person"].ToString();
                form.applicant_city_town = reader["applicant_city_town"].ToString();
                form.applicant_nationality = reader["applicant_nationality"].ToString();
                form.manufacturer_name = reader["manufacturer_name"].ToString();
                form.manufacturer_tel = reader["manufacturer_tel"].ToString();
                form.manufacturer_address = reader["manufacturer_address"].ToString();
                form.manufacturer_fax = reader["manufacturer_fax"].ToString();
                form.manufacturer_contact_person = reader["manufacturer_contact_person"].ToString();
                form.equipment_type = reader["equipment_type"].ToString();
                form.equipment_description = reader["equipment_description"].ToString();
                form.product_identification = reader["product_identifiation"].ToString();
                form.refNum = reader["ref#"].ToString();
                form.make = reader["make"].ToString();
                form.software = reader["software"].ToString();
                form.type_of_equipment = reader["type_of_equipment"].ToString();
                form.other = reader["other"].ToString();
                form.antenna_type = reader["antenna_type"].ToString();
                form.antenna_gain = reader["antenna_gain"].ToString();
                form.channel = reader["channel"].ToString();
                form.separation = reader["separation"].ToString();
                form.aspect = reader["aspect"].ToString();
                form.compatibility = reader["compatibility"].ToString();
                form.security = reader["security"].ToString();
                form.equipment_comm_type = reader["equipment_comm_type"].ToString();
                form.fee_code = reader["fee_code"].ToString();
                form.status = reader["current_status"].ToString();
            }
            conn.Close();

            form.frequencies = GetFrequencies(application_id);
            return form;
        }

        public List<Frequency> GetFrequencies(string application_id)
        {
            SqlConnection conn = new SqlConnection(SLW_dbConn);
            SqlCommand cmd = new SqlCommand();
            SqlDataReader reader = null;
            cmd.CommandText = "sp_getFrequencies @application_id";

            List<Frequency> frequencies = new List<Frequency>();
            cmd.Parameters.AddWithValue("@application_id", application_id);
            cmd.Connection = conn;

            conn.Open();
            reader = cmd.ExecuteReader();

            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    int sequence = Convert.ToInt32(reader["sequence"]);
                    string lower_freq = reader["lower_freq"].ToString();
                    string upper_freq = reader["upper_freq"].ToString();
                    string power = reader["power"].ToString();
                    string tolerance = reader["tolerance"].ToString();
                    string emmision_desig = reader["emmision_desig"].ToString();
                    string freq_type = reader["freq_type"].ToString();

                    frequencies.Add(new Frequency(application_id, sequence, lower_freq, upper_freq, power, tolerance, emmision_desig, freq_type));
                }
            }
            conn.Close();
            return frequencies;
        }
    }
}