using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Globalization;
using System.Security.Cryptography;
using System.Text;
using WebService.Commons;
using WebService.Models;

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
                items.Add(new SearchCategory(manufaturers[i], "http://localhost:3348/search?dealer=" + manufaturers[i] + "&model=", "", "Manufacturers"));
            }

            for (int i = 0; i < models.Count; i++)
            {
                items.Add(new SearchCategory(models[i], "http://localhost:3348/search?dealer=&model=" + models[i], "", "Models"));
            }

            return new ApplicationSearchResultMain(items);
        }

        // User management

        public UserDetails GetUserDetails(string username)
        {
            UserDetails userDetails = new UserDetails();
            SqlConnection conn = new SqlConnection(SLW_dbConn);
            SqlCommand cmd = new SqlCommand();
            SqlDataReader reader = null;
            cmd.CommandText = "sp_getUserDetails @username";
            cmd.Parameters.AddWithValue("@username", username);
            cmd.Connection = conn;

            conn.Open();
            reader = cmd.ExecuteReader();

            if (reader.HasRows)
            {
                reader.Read();
                userDetails.username = reader["username"].ToString();
                userDetails.first_name = reader["first_name"].ToString();
                userDetails.last_name = reader["last_name"].ToString();
                userDetails.fullname = reader["fullname"].ToString();
                userDetails.email = reader["email"].ToString();
                userDetails.user_type = reader["user_type"].ToString();
                userDetails.created_date = Convert.ToDateTime(reader["created_date"]);
                userDetails.created_date_str = userDetails.created_date.ToString();
                userDetails.last_detected_activity = Convert.ToDateTime(reader["last_detected_activity"]);
                userDetails.last_detected_activity_str = userDetails.last_detected_activity.ToString();
                conn.Close();

                return userDetails;
            }
            else
            {
                conn.Close();
                return null;
            }
        }

        public List<UserDetails> GetAllUsersDetails()
        {
            List<UserDetails> userDetails = new List<UserDetails>();
            SqlConnection conn = new SqlConnection(SLW_dbConn);
            SqlCommand cmd = new SqlCommand();
            SqlDataReader reader = null;
            cmd.CommandText = "sp_getUserDetails @username";
            cmd.Parameters.AddWithValue("@username", "*all");
            cmd.Connection = conn;

            conn.Open();
            reader = cmd.ExecuteReader();

            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    UserDetails userDetail = new UserDetails();
                    userDetail.username = reader["username"].ToString();
                    userDetail.first_name = reader["first_name"].ToString();
                    userDetail.last_name = reader["last_name"].ToString();
                    userDetail.fullname = reader["fullname"].ToString();
                    userDetail.email = reader["email"].ToString();
                    userDetail.user_type = reader["user_type"].ToString();
                    userDetail.created_date = Convert.ToDateTime(reader["created_date"]);
                    userDetail.last_detected_activity = Convert.ToDateTime(reader["last_detected_activity"]);
                    userDetails.Add(userDetail);
                }
            }
            conn.Close();
            return userDetails;
        }

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

        public List<string> GetAllUsernames()
        {

            SqlConnection conn = new SqlConnection(SLW_dbConn);
            SqlCommand cmd = new SqlCommand();
            List<string> usernames = new List<string>();
            SqlDataReader reader = null;
            cmd.CommandText = "sp_getAllUsernames";

            cmd.Connection = conn;
            conn.Open();
            reader = cmd.ExecuteReader();

            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    usernames.Add(reader["username"].ToString());
                }
                conn.Close();
                return usernames;
            }
            else
            {
                conn.Close();
                return usernames;
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

        public void ResetPassword(string user, string hash)
        {
            SqlConnection conn = new SqlConnection(SLW_dbConn);
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "sp_resetPassword @user, @hash";
            cmd.Parameters.AddWithValue("@user", user);
            cmd.Parameters.AddWithValue("@hash", hash);

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
            cmd.Parameters.AddWithValue("@hash", hash);

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

        public int NewUser(string username, string first_name, string last_name, DateTime created_date, int user_type, DateTime last_password_change_date,
                                  DateTime last_login_date, string hash, bool password_reset_required, string email, string company, int clientId, string source)
        {
            SqlConnection conn = new SqlConnection(SLW_dbConn);
            SqlDataReader reader = null;
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "sp_createUser @username, @first_name, @last_name, @created_date, @user_type, @last_password_change_date, " +
                              "@last_login_date, @hash, @password_reset_required, @email, @company, @clientId, @source";

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
            cmd.Parameters.AddWithValue("@source", source);

            cmd.Connection = conn;
            conn.Open();
            reader = cmd.ExecuteReader();

            reader.Read();
            int id = Convert.ToInt32(reader["client_id"]);
            conn.Close();
            return id;
        }

        public bool CheckLocalClientExist(int client_id)
        {
            SqlConnection conn = new SqlConnection(SLW_dbConn);
            SqlCommand cmd = new SqlCommand();
            SqlDataReader reader = null;
            cmd.CommandText = "sp_checkLocalClientExist @client_id";
            cmd.Parameters.AddWithValue("@client_id", client_id);
            cmd.Connection = conn;

            conn.Open();
            reader = cmd.ExecuteReader();
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
                company.source = reader["source"].ToString();
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
                              "@other, @antenna_type, @antenna_gain, @channel,@separation, @additional_info, @name_of_test, @country, @status, @category";

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
            cmd.Parameters.AddWithValue("@additional_info", form.additional_info);
            cmd.Parameters.AddWithValue("@name_of_test", form.name_of_test);
            cmd.Parameters.AddWithValue("@country", form.country);
            cmd.Parameters.AddWithValue("@status", form.status);
            cmd.Parameters.AddWithValue("category", form.category);

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
                string created_date = Utilities.DateFormatter.GetPrettyDate(Convert.ToDateTime(reader["created_date"]));
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
                string created_date = Convert.ToDateTime(reader["created_date"]).ToShortDateString() + " " + Convert.ToDateTime(reader["created_date"]).ToShortTimeString();
                string last_update = Convert.ToDateTime(reader["last_updated"]).ToShortDateString() + " " + Convert.ToDateTime(reader["last_updated"]).ToShortTimeString();
                string status = reader["status"].ToString();
                string current_status = reader["current_status"].ToString();
                recentDocuments.Add(new RecentDocuments(application_id, created_date, status, last_update, current_status));
            }
            conn.Close();
            return recentDocuments;
        }

        public List<SavedApplications> GetSavedApplications(string _username_)
        {
            SqlConnection conn = new SqlConnection(SLW_dbConn);
            SqlCommand cmd = new SqlCommand();
            SqlDataReader reader = null;
            cmd.CommandText = " sp_getSavedApplications @username, @category";

            List<SavedApplications> savedApplications = new List<SavedApplications>();
            cmd.Parameters.AddWithValue("@username", _username_);
            cmd.Parameters.AddWithValue("@category", Constants.TYPE_APPROVAL); //temporary implementation
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
                form.status = reader["current_status"].ToString();
                form.category = reader["category"].ToString();
                form.additional_info = reader["additional_info"].ToString();
                form.name_of_test = reader["name_of_test"].ToString();
                form.country = reader["country"].ToString();
                form.name_of_test = reader["name_of_test"].ToString();
                form.country = reader["country"].ToString();
            }

            conn.Close();
            form.frequencies = GetFrequencies(application_id);
            return form;
        }

        public List<string> GetApplicationIDsForUser(string username)
        {
            SqlConnection conn = new SqlConnection(SLW_dbConn);
            SqlCommand cmd = new SqlCommand();
            SqlDataReader reader = null;
            List<string> applications = new List<string>();

            cmd.CommandText = " sp_getApplicationIdsForUser @username";
            cmd.Parameters.AddWithValue("@username", username);
            cmd.Connection = conn;

            conn.Open();
            reader = cmd.ExecuteReader();

            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    applications.Add(reader["application_id"].ToString());
                }
            }
            conn.Close();
            return applications;
        }

        public string GetApplicationStatus(string application_id)
        {
            SqlConnection conn = new SqlConnection(SLW_dbConn);
            SqlCommand cmd = new SqlCommand();
            SqlDataReader reader = null;
            cmd.CommandText = "sp_getApplicationStatus @application_id";
            cmd.Connection = conn;
            string status = "";

            conn.Open();
            reader = cmd.ExecuteReader();

            if (reader.HasRows)
            {
                reader.Read();
                status = reader["status"].ToString();
            }
            conn.Close();
            return status;
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

                    frequencies.Add(new Frequency("", application_id, sequence, lower_freq, upper_freq, power, tolerance, emmision_desig, freq_type));
                }
            }
            conn.Close();
            return frequencies;
        }

        public ApplicationCounters GetApplicationCounters(string username)
        {
            SqlConnection conn = new SqlConnection(SLW_dbConn);
            SqlCommand cmd = new SqlCommand();
            SqlDataReader reader = null;
            cmd.CommandText = "sp_getApplicationCounters @username";

            ApplicationCounters applicationCounters = new ApplicationCounters();
            cmd.Parameters.AddWithValue("@username", username);
            cmd.Connection = conn;

            conn.Open();
            reader = cmd.ExecuteReader();
            if (reader.HasRows)
            {
                reader.Read();
                applicationCounters.licensed_applications = Convert.ToInt32(reader["licensed_apps"]);
                applicationCounters.pending_applications = Convert.ToInt32(reader["pending_apps"]);
                applicationCounters.incomplete_applications = Convert.ToInt32(reader["incomplete_apps"]);
            }
            conn.Close();
            return applicationCounters;
        }

        public List<RecentActivity> GetRecentActivities(string username)
        {
            SqlConnection conn = new SqlConnection(SLW_dbConn);
            SqlCommand cmd = new SqlCommand();
            SqlDataReader reader = null;
            cmd.CommandText = "sp_getRecentActivities @username";
            List<RecentActivity> recentActivities = new List<RecentActivity>();

            cmd.Parameters.AddWithValue("@username", username);
            cmd.Connection = conn;

            conn.Open();
            reader = cmd.ExecuteReader();

            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    recentActivities.Add(new RecentActivity(reader["application_id"].ToString(), reader["manufacturer"].ToString(), reader["model"].ToString(), Convert.ToDateTime(reader["created_date"]).ToShortDateString(), Convert.ToDateTime(reader["licensed_date"]).ToShortDateString(), reader["author"].ToString(), reader["category"].ToString(), reader["status"].ToString()));
                }
            }
            conn.Close();
            return recentActivities;
        }

        public List<LicensedApplication> GetLicensedApplications(string username)
        {
            SqlConnection conn = new SqlConnection(SLW_dbConn);
            SqlCommand cmd = new SqlCommand();
            SqlDataReader reader = null;
            cmd.CommandText = "sp_getLicensedApplications @username";
            List<LicensedApplication> licensedApplications = new List<LicensedApplication>();

            cmd.Parameters.AddWithValue("@username", username);
            cmd.Connection = conn;

            conn.Open();
            reader = cmd.ExecuteReader();

            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    licensedApplications.Add(new LicensedApplication(reader["application_id"].ToString(), reader["manufacturer"].ToString(), reader["model"].ToString(), reader["created_date"].ToString(), reader["licensed_date"].ToString(), reader["author"].ToString(), reader["category"].ToString()));
                }
            }
            conn.Close();
            return licensedApplications;
        }

        public List<PendingApproval> GetPendingApprovals(string username)
        {
            SqlConnection conn = new SqlConnection(SLW_dbConn);
            SqlCommand cmd = new SqlCommand();
            SqlDataReader reader = null;
            cmd.CommandText = "sp_getPendingApprovals @username";
            List<PendingApproval> pendingApprovals = new List<PendingApproval>();

            cmd.Parameters.AddWithValue("@username", username);
            cmd.Connection = conn;

            conn.Open();
            reader = cmd.ExecuteReader();

            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    pendingApprovals.Add(new PendingApproval(reader["application_id"].ToString(), reader["manufacturer"].ToString(), reader["model"].ToString(), reader["created_date"].ToString(), reader["licensed_date"].ToString(), reader["author"].ToString(), reader["category"].ToString()));
                }
            }
            conn.Close();
            return pendingApprovals;
        }

        public Dashboard GetDashboardData(string username)
        {
            Dashboard dashboard = new Dashboard();
            ApplicationCounters applicationCounters = GetApplicationCounters(username);

            dashboard.licensed_app_count = applicationCounters.licensed_applications;
            dashboard.pending_app_count = applicationCounters.pending_applications;
            dashboard.incomplete_app_count = applicationCounters.incomplete_applications;

            dashboard.recentActivities = GetRecentActivities(username);
            dashboard.licensedApplications = GetLicensedApplications(username);
            dashboard.pendingApprovals = GetPendingApprovals(username);

            return dashboard;
        }

        public void NewEmailSetting(string email, string company)
        {
            SqlConnection conn = new SqlConnection(SLW_dbConn);
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "sp_setNewEmailSettings @email, @company";

            cmd.Parameters.AddWithValue("@email", email);
            cmd.Parameters.AddWithValue("@company", company);
            cmd.Connection = conn;

            conn.Open();
            cmd.ExecuteNonQuery();
            conn.Close();
        }

        public EmailSetting GetEmailSetting()
        {
            SqlConnection conn = new SqlConnection(SLW_dbConn);
            SqlCommand cmd = new SqlCommand();
            SqlDataReader reader = null;
            EmailSetting emailSetting = new EmailSetting();
            cmd.Connection = conn;
            cmd.CommandText = "sp_getCurrentEmailSettings";

            conn.Open();
            reader = cmd.ExecuteReader();

            if (reader.HasRows)
            {
                reader.Read();
                emailSetting.email = reader["email"].ToString();
                emailSetting.last_accessed = Convert.ToDateTime(reader["last_accessed"]);
                emailSetting.company_name = reader["company_name"].ToString();
            }
            conn.Close();
            return emailSetting;
        }

        public void AddFileReference(string file_id, string filename, DateTime created_date, string path, string application_id, string name_of_test, string country, string username, string purpose)
        {
            SqlConnection conn = new SqlConnection(SLW_dbConn);
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "sp_addFile @file_id, @filename, @created_date, @path, @application_id, @username, @purpose";

            cmd.Parameters.AddWithValue("@file_id", file_id);
            cmd.Parameters.AddWithValue("@filename", filename);
            cmd.Parameters.AddWithValue("@created_date", created_date);
            cmd.Parameters.AddWithValue("@path", path);
            cmd.Parameters.AddWithValue("@application_id", application_id);
            cmd.Parameters.AddWithValue("@username", username);
            cmd.Parameters.AddWithValue("@purpose", purpose);
            cmd.Connection = conn;

            conn.Open();
            cmd.ExecuteNonQuery();
            conn.Close();
        }

        public void DeleteFileReference(string file_id)
        {
            SqlConnection conn = new SqlConnection(SLW_dbConn);
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "sp_removeFileReference @file_id";
            cmd.Parameters.AddWithValue("@file_id", @file_id);
            cmd.Connection = conn;
            conn.Open();
            cmd.ExecuteNonQuery();
            conn.Close();
        }

        public Certificate GetCertificate(string application_id)
        {
            SqlConnection conn = new SqlConnection(SLW_dbConn);
            SqlCommand cmd = new SqlCommand();
            SqlDataReader reader = null;
            Certificate certificate = new Certificate();

            cmd.CommandText = "sp_getCertificateMain @application_id";
            cmd.Parameters.AddWithValue("@application_id", application_id);
            cmd.Connection = conn;

            conn.Open();
            reader = cmd.ExecuteReader();

            if (reader.HasRows)
            {
                reader.Read();
                certificate.manufacturer_name = reader["manufacturer_name"].ToString();
                certificate.manufacturer_address = reader["manufacturer_address"].ToString();
                certificate.product_identification = reader["product_identifiation"].ToString();
                certificate.equipment_description = reader["equipment_description"].ToString();
            }

            certificate.frequencies = GetFrequencies(application_id);
            return certificate;
        }

        public string GenerateAppID()
        {
            SqlConnection conn = new SqlConnection(SLW_dbConn);
            SqlCommand cmd = new SqlCommand();
            SqlDataReader reader = null;
            string result = "";

            cmd.CommandText = "sp_getNewAppId";
            cmd.Connection = conn;
            conn.Open();

            reader = cmd.ExecuteReader();
            reader.Read();
            result = reader["id"].ToString();
            conn.Close();
            return result;
        }

        private List<ManufacturerModel> GetCurrentManufacturerModels(string username)
        {
            SqlConnection conn = new SqlConnection(SLW_dbConn);
            SqlCommand cmd = new SqlCommand();
            SqlDataReader reader = null;
            List<ManufacturerModel> manufacturerModels = new List<ManufacturerModel>();
            cmd.CommandText = "sp_getCurrentManufacturerModels @username";

            cmd.Parameters.AddWithValue("@username", username);
            cmd.Connection = conn;

            conn.Open();
            reader = cmd.ExecuteReader();
            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    manufacturerModels.Add(new ManufacturerModel(reader["application_id"].ToString(), reader["manufacturer_name"].ToString(), reader["product_identifiation"].ToString(), reader["status"].ToString()));
                }
            }
            conn.Close();
            return manufacturerModels;
        }

        private ManufacturerModel GetASMSManufacturerModel(string manufacturer, string model)
        {
            SqlConnection conn = new SqlConnection(SLW_dbConn);
            SqlCommand cmd = new SqlCommand();
            SqlDataReader reader = null;
            ManufacturerModel manufacturerModel = null;
            cmd.CommandText = "sp_getASMSApplication @manufacturer, @model";

            cmd.Parameters.AddWithValue("@manufacturer", manufacturer);
            cmd.Parameters.AddWithValue("@model", model);
            cmd.Connection = conn;
            conn.Open();
            reader = cmd.ExecuteReader();

            if (reader.HasRows)
            {
                reader.Read();
                manufacturerModel = new ManufacturerModel("", reader["manufacturer"].ToString(), reader["model"].ToString(), reader["Status"].ToString());
            }

            conn.Close();
            return manufacturerModel;
        }

        public void UpdateApplicationStatus(string application_id, string status)
        {
            SqlConnection conn = new SqlConnection(SLW_dbConn);
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "sp_updateApplicationStatus @application_id, @status";

            cmd.Parameters.AddWithValue("@application_id", application_id);
            cmd.Parameters.AddWithValue("@status", status);
            cmd.Connection = conn;

            conn.Open();
            cmd.ExecuteNonQuery();
            conn.Close();
        }

        public void CheckForApplicationUpdates(string username)
        {
            List<ManufacturerModel> currentManufacturerModels = GetCurrentManufacturerModels(username);
            for (int i = 0; i < currentManufacturerModels.Count; i++)
            {
                ManufacturerModel asmsManufacturerModel = GetASMSManufacturerModel(currentManufacturerModels[i].manufacturer, currentManufacturerModels[i].model);
                if (asmsManufacturerModel != null)
                {
                    switch (asmsManufacturerModel.status)
                    {
                        case "Licensed":
                            UpdateApplicationStatus(currentManufacturerModels[i].application_id, Constants.LICENSED_TYPE);
                            break;
                        case "Pending":
                            UpdateApplicationStatus(currentManufacturerModels[i].application_id, Constants.PENDING_TYPE);
                            break;
                        case "Rejected":
                            UpdateApplicationStatus(currentManufacturerModels[i].application_id, Constants.REJECTED);
                            break;
                        case "Invoiced":
                            UpdateApplicationStatus(currentManufacturerModels[i].application_id, Constants.INVOICED_TYPE);
                            break;
                    }
                }
            }
        }

        public void CheckForApplicationUpdatesAllUsers()
        {
            List<string> usernames = GetAllUsernames();
            for (int i = 0; i < usernames.Count; i++)
            {
                CheckForApplicationUpdates(usernames[i]);
            }
        }

        public List<EngineerUser> GetEngineerUsers()
        {
            List<EngineerUser> engineerUsers = new List<EngineerUser>();
            SqlConnection conn = new SqlConnection(SLW_dbConn);
            SqlCommand cmd = new SqlCommand();
            SqlDataReader reader = null;
            cmd.CommandText = "sp_getSMAEngineers";

            cmd.Connection = conn;
            conn.Open();

            reader = cmd.ExecuteReader();
            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    engineerUsers.Add(new EngineerUser(reader["username"].ToString(), reader["name"].ToString(), reader["email"].ToString()));
                }
            }
            conn.Close();
            return engineerUsers;
        }

        public List<ClientUser> GetClientUsers()
        {
            List<ClientUser> clientUsers = new List<ClientUser>();
            SqlConnection conn = new SqlConnection(SLW_dbConn);
            SqlCommand cmd = new SqlCommand();
            SqlDataReader reader = null;
            cmd.CommandText = "sp_getClientUsers";

            cmd.Connection = conn;
            conn.Open();

            reader = cmd.ExecuteReader();
            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    ClientUser clientUser = new ClientUser();
                    clientUser.username = reader["username"].ToString();
                    clientUser.first_name = reader["first_name"].ToString();
                    clientUser.last_name = reader["last_name"].ToString();
                    clientUsers.Add(clientUser);
                }
            }
            conn.Close();
            return clientUsers;
        }

        public void DeleteUnassignedTask(string application_id)
        {
            SqlConnection conn = new SqlConnection(SLW_dbConn);
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "sp_deleteUnassignedTask @application_id";

            cmd.Parameters.AddWithValue("@application_id", application_id);
            cmd.Connection = conn;

            conn.Open();
            cmd.ExecuteNonQuery();
            conn.Close();
        }

        public void DeleteOngoingTask(string application_id)
        {
            SqlConnection conn = new SqlConnection(SLW_dbConn);
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "sp_deleteOngoingTask @application_id";

            cmd.Parameters.AddWithValue("@application_id", application_id);
            cmd.Connection = conn;

            conn.Open();
            cmd.ExecuteNonQuery();
            conn.Close();
        }

        public void NewUnassignedTask(string application_id, string submitted_by, DateTime created_date)
        {
            SqlConnection conn = new SqlConnection(SLW_dbConn);
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "sp_newUnassignedTask @application_id, @username, @created_date";

            cmd.Parameters.AddWithValue("@application_id", application_id);
            cmd.Parameters.AddWithValue("@username", submitted_by);
            cmd.Parameters.AddWithValue("@created_date", created_date);
            cmd.Connection = conn;

            conn.Open();
            cmd.ExecuteNonQuery();
            conn.Close();
        }

        public void NewOngoingTask(string application_id, string assigned_to, string submitted_by, string status, DateTime created_date)
        {
            SqlConnection conn = new SqlConnection(SLW_dbConn);
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "sp_newOngoingTask @application_id, @assigned_to, @submitted_by_username, @created_date";

            cmd.Parameters.AddWithValue("@application_id", application_id);
            cmd.Parameters.AddWithValue("@assigned_to", assigned_to);
            cmd.Parameters.AddWithValue("@submitted_by_username", submitted_by);
            cmd.Parameters.AddWithValue("@created_date", created_date);
            cmd.Connection = conn;

            conn.Open();
            cmd.ExecuteNonQuery();
            conn.Close();
        }

        public List<UnassignedTask> GetUnassignedTasks()
        {
            List<UnassignedTask> unassignedTasks = new List<UnassignedTask>();
            SqlConnection conn = new SqlConnection(SLW_dbConn);
            SqlCommand cmd = new SqlCommand();
            SqlDataReader reader = null;
            cmd.CommandText = "sp_getUnassignedTasks";

            cmd.Connection = conn;
            conn.Open();

            reader = cmd.ExecuteReader();
            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    unassignedTasks.Add(new UnassignedTask(reader["application_id"].ToString(), String.Format("{0:g}", Convert.ToDateTime(reader["created_date"])), Convert.ToDateTime(reader["created_date"]), reader["submitted_by"].ToString(), reader["username"].ToString()));
                }
            }
            conn.Close();
            return unassignedTasks;
        }

        public List<OngoingTask> GetOngoingTasks()
        {
            List<OngoingTask> ongoingTasks = new List<OngoingTask>();
            SqlConnection conn = new SqlConnection(SLW_dbConn);
            SqlCommand cmd = new SqlCommand();
            SqlDataReader reader = null;
            cmd.CommandText = "sp_getOngoingTasks";

            cmd.Connection = conn;
            conn.Open();

            reader = cmd.ExecuteReader();
            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    ongoingTasks.Add(new OngoingTask(reader["application_id"].ToString(), String.Format("{0:g}", Convert.ToDateTime(reader["created_date"])), Convert.ToDateTime(reader["created_date"]), reader["assigned_to"].ToString(), String.Format("{0:g}", Convert.ToDateTime(reader["date_assigned"])), reader["status"].ToString(), reader["submitted_by"].ToString(), reader["submitted_by_username"].ToString()));
                }
            }
            conn.Close();
            return ongoingTasks;
        }

        public OngoingTask GetSingleOngoingTask(string applicationId)
        {
            OngoingTask ongoing = new OngoingTask();
            SqlConnection conn = new SqlConnection(SLW_dbConn);
            SqlCommand cmd = new SqlCommand();
            SqlDataReader reader = null;
            cmd.CommandText = "sp_getSingleOngoingTask @application_id";
            cmd.Parameters.AddWithValue("@application_id", applicationId);

            cmd.Connection = conn;
            conn.Open();

            reader = cmd.ExecuteReader();
            if (reader.HasRows)
            {
                reader.Read();
                ongoing = new OngoingTask(reader["application_id"].ToString(), String.Format("{0:g}", Convert.ToDateTime(reader["created_date"])), Convert.ToDateTime(reader["created_date"]), reader["assigned_to"].ToString(), String.Format("{0:g}", Convert.ToDateTime(reader["date_assigned"])), reader["status"].ToString(), reader["submitted_by"].ToString(), reader["submitted_by_username"].ToString());
            }

            conn.Close();
            return ongoing;
        }

        public UnassignedTask GetSingleUnassignedTask(string applicationId)
        {
            UnassignedTask unassigned = new UnassignedTask();
            SqlConnection conn = new SqlConnection(SLW_dbConn);
            SqlCommand cmd = new SqlCommand();
            SqlDataReader reader = null;
            cmd.CommandText = "sp_getSingleUnassignedTask @application_id";
            cmd.Parameters.AddWithValue("@application_id", applicationId);

            cmd.Connection = conn;
            conn.Open();

            reader = cmd.ExecuteReader();
            if (reader.HasRows)
            {
                reader.Read();
                unassigned = new UnassignedTask(reader["application_id"].ToString(), String.Format("{0:g}", Convert.ToDateTime(reader["created_date"])), Convert.ToDateTime(reader["created_date"]), reader["submitted_by"].ToString(), reader["username"].ToString());
            }

            conn.Close();
            return unassigned;
        }

        public void ReassignTask(string application_id, string assign_to)
        {
            SqlConnection conn = new SqlConnection(SLW_dbConn);
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "sp_updateAssignedUser @application_id, @assigned_to";

            cmd.Parameters.AddWithValue("@application_id", application_id);
            cmd.Parameters.AddWithValue("@assigned_to", assign_to);
            cmd.Connection = conn;

            conn.Open();
            cmd.ExecuteNonQuery();
            conn.Close();
        }

        public bool CheckTaskExist(string applicationId)
        {
            SqlConnection conn = new SqlConnection(SLW_dbConn);
            SqlCommand cmd = new SqlCommand();
            SqlDataReader reader = null;
            cmd.CommandText = "sp_checkTaskExist @applicationId";
            cmd.Parameters.AddWithValue("@applicationId", applicationId);

            cmd.Connection = conn;
            conn.Open();

            reader = cmd.ExecuteReader();
            reader.Read();

            if (reader["exist"].ToString() == "0")
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        public List<AssignedTask> GetStaffAssignedTasks(string username)
        {
            SqlConnection conn = new SqlConnection(SLW_dbConn);
            SqlCommand cmd = new SqlCommand();
            List<AssignedTask> assignedTasks = new List<AssignedTask>();
            SqlDataReader reader = null;
            cmd.CommandText = "sp_getAssignedTasks @username";
            cmd.Parameters.AddWithValue("@username", username);
            cmd.Connection = conn;

            conn.Open();
            reader = cmd.ExecuteReader();
            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    AssignedTask assigned = new AssignedTask();
                    assigned.application_id = reader["application_id"].ToString();
                    assigned.created_date = String.Format("{0:g}", Convert.ToDateTime(reader["created_date"]));
                    assigned.submitted_by = reader["submitted_by"].ToString();
                    assigned.assigned_date = String.Format("{0:g}", Convert.ToDateTime(reader["date_assigned"]));
                    assigned.status = reader["status"].ToString();
                    assignedTasks.Add(assigned);
                }
            }
            conn.Close();
            return assignedTasks;
        }

        public List<ApplicationFiles> GetApplicationFiles(string application_id)
        {
            SqlConnection conn = new SqlConnection(SLW_dbConn);
            SqlCommand cmd = new SqlCommand();
            List<ApplicationFiles> applicationFiles = new List<ApplicationFiles>();
            SqlDataReader reader = null;
            cmd.CommandText = "sp_getApplicationFiles @application_id";
            cmd.Parameters.AddWithValue("@application_id", application_id);
            cmd.Connection = conn;

            conn.Open();
            reader = cmd.ExecuteReader();

            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    ApplicationFiles applicationFile = new ApplicationFiles();
                    applicationFile.application_id = reader["application_id"].ToString();
                    applicationFile.file_id = reader["file_id"].ToString();
                    applicationFile.filename = reader["filename"].ToString();
                    applicationFile.path = reader["path"].ToString();
                    applicationFile.created_date = String.Format("{0:g}", Convert.ToDateTime(reader["created_date"]));
                    applicationFile.purpose = reader["purpose"].ToString();
                    applicationFile.username = reader["username"].ToString();
                    applicationFiles.Add(applicationFile);
                }
            }
            conn.Close();
            return applicationFiles;
        }
        public List<Models.ApplicationFiles> GetTechnicalSpecificationFiles(string application_id)
        {
            SqlConnection conn = new SqlConnection(SLW_dbConn);
            SqlCommand cmd = new SqlCommand();
            List<ApplicationFiles> applicationFiles = new List<ApplicationFiles>();
            SqlDataReader reader = null;
            cmd.CommandText = "sp_getFilesByType @purpose, @application_id";
            cmd.Parameters.AddWithValue("@purpose", Commons.Constants.TECHNICAL_SPECIFICATION_FILE);
            cmd.Parameters.AddWithValue("@application_id", application_id);
            cmd.Connection = conn;

            conn.Open();
            reader = cmd.ExecuteReader();

            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    ApplicationFiles applicationFile = new ApplicationFiles();
                    applicationFile.application_id = reader["application_id"].ToString();
                    applicationFile.file_id = reader["file_id"].ToString();
                    applicationFile.filename = reader["filename"].ToString();
                    applicationFile.created_date = String.Format("{0:g}", Convert.ToDateTime(reader["created_date"]));
                    applicationFile.purpose = reader["purpose"].ToString();
                    applicationFile.username = reader["username"].ToString();
                    applicationFiles.Add(applicationFile);
                }
            }
            conn.Close();
            return applicationFiles;
        }

        public List<Models.ApplicationFiles> GetTestReportFiles(string application_id)
        {
            SqlConnection conn = new SqlConnection(SLW_dbConn);
            SqlCommand cmd = new SqlCommand();
            List<ApplicationFiles> applicationFiles = new List<ApplicationFiles>();
            SqlDataReader reader = null;
            cmd.CommandText = "sp_getFilesByType @purpose, @application_id";
            cmd.Parameters.AddWithValue("@purpose", Commons.Constants.TEST_REPORT_FILE);
            cmd.Parameters.AddWithValue("@application_id", application_id);
            cmd.Connection = conn;

            conn.Open();
            reader = cmd.ExecuteReader();

            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    ApplicationFiles applicationFile = new ApplicationFiles();
                    applicationFile.application_id = reader["application_id"].ToString();
                    applicationFile.file_id = reader["file_id"].ToString();
                    applicationFile.filename = reader["filename"].ToString();
                    applicationFile.created_date = String.Format("{0:g}", Convert.ToDateTime(reader["created_date"]));
                    applicationFile.purpose = reader["purpose"].ToString();
                    applicationFile.username = reader["username"].ToString();
                    applicationFiles.Add(applicationFile);
                }
            }
            conn.Close();
            return applicationFiles;
        }

        public List<Models.ApplicationFiles> GetAccreditationFiles(string application_id)
        {
            SqlConnection conn = new SqlConnection(SLW_dbConn);
            SqlCommand cmd = new SqlCommand();
            List<ApplicationFiles> applicationFiles = new List<ApplicationFiles>();
            SqlDataReader reader = null;
            cmd.CommandText = "sp_getFilesByType @purpose, @application_id";
            cmd.Parameters.AddWithValue("@purpose", Commons.Constants.ACCREDITATION_FILE);
            cmd.Parameters.AddWithValue("@application_id", application_id);
            cmd.Connection = conn;

            conn.Open();
            reader = cmd.ExecuteReader();

            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    ApplicationFiles applicationFile = new ApplicationFiles();
                    applicationFile.application_id = reader["application_id"].ToString();
                    applicationFile.file_id = reader["file_id"].ToString();
                    applicationFile.filename = reader["filename"].ToString();
                    applicationFile.created_date = String.Format("{0:g}", Convert.ToDateTime(reader["created_date"]));
                    applicationFile.purpose = reader["purpose"].ToString();
                    applicationFile.username = reader["username"].ToString();
                    applicationFiles.Add(applicationFile);
                }
            }
            conn.Close();
            return applicationFiles;
        }

        public ApplicationFileCategories GetApplicationFileCategories(string application_id)
        {
            ApplicationFileCategories applicationFileCategories = new ApplicationFileCategories();
            applicationFileCategories.technicalSpecifications = GetTechnicalSpecificationFiles(application_id);
            applicationFileCategories.testReport = GetTestReportFiles(application_id);
            applicationFileCategories.accreditation = GetAccreditationFiles(application_id);
            return applicationFileCategories;
        }

        public Manufacturer NewLocalManufacturer(Manufacturer manufacturer)
        {
       
            SqlConnection conn = new SqlConnection(SLW_dbConn);
            SqlCommand cmd = new SqlCommand();
            SqlDataReader reader = null;
            Manufacturer _manufacturer = new Manufacturer();

            cmd.CommandText = "sp_newManufacturer @dealer, @address, @telephone, @fax, @contact_person";
            cmd.Parameters.AddWithValue("@dealer", manufacturer.name);
            cmd.Parameters.AddWithValue("@address", manufacturer.address);
            cmd.Parameters.AddWithValue("@telephone", manufacturer.telephone);
            cmd.Parameters.AddWithValue("@fax", manufacturer.fax);
            cmd.Parameters.AddWithValue("@contact_person", manufacturer.contact_person);
            cmd.Connection = conn;

            conn.Open();
            reader = cmd.ExecuteReader();
            reader.Read();

            manufacturer.name = reader["dealer"].ToString();
            manufacturer.address = reader["address"].ToString();
            manufacturer.telephone = reader["telephone"].ToString();
            manufacturer.fax = reader["fax"].ToString();
            manufacturer.contact_person = reader["fax"].ToString();
            conn.Close();
            return manufacturer;
        }

        public List<Manufacturer> GetLocalManufacturers()
        {
            SqlConnection conn = new SqlConnection(SLW_dbConn);
            SqlCommand cmd = new SqlCommand();
            List<Manufacturer> manufacturers = new List<Manufacturer>();
            SqlDataReader reader = null;
            cmd.CommandText = " sp_getLocalManufacturers @manufacturer_id";
            cmd.Parameters.AddWithValue("@manufacturer_id", "");
            cmd.Connection = conn;

            conn.Open();
            reader = cmd.ExecuteReader();

            if (reader.HasRows)
            {
                while (reader.Read()) {
                    manufacturers.Add(new Manufacturer(reader["dealer"].ToString(), reader["address"].ToString(), reader["telephone"].ToString(), reader["fax"].ToString(), reader["contact_person"].ToString()));
                }
            }

            conn.Close();
            return manufacturers;
        }

        public List<Manufacturer> GetLocalManufacturers(int manufacturer_id)
        {
            SqlConnection conn = new SqlConnection(SLW_dbConn);
            SqlCommand cmd = new SqlCommand();
            List<Manufacturer> manufacturers = new List<Manufacturer>();
            SqlDataReader reader = null;
            cmd.CommandText = " sp_getLocalManufacturers @manufacturer_id";
            cmd.Parameters.AddWithValue("@manufacturer_id", manufacturer_id);
            cmd.Connection = conn;

            conn.Open();
            reader = cmd.ExecuteReader();

            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    manufacturers.Add(new Manufacturer(reader["dealer"].ToString(), reader["address"].ToString(), reader["telephone"].ToString(), reader["fax"].ToString(), reader["contact_person"].ToString()));
                }
            }

            conn.Close();
            return manufacturers;
        }

        public ClientCompany GetLocalClientCompany(int client_id)
        {
            SqlConnection conn = new SqlConnection(SLW_dbConn);
            SqlCommand cmd = new SqlCommand();
            ClientCompany clientCompany = new ClientCompany();
            SqlDataReader reader = null;
            cmd.CommandText = "sp_getLocalClientCompany @client_id";
            cmd.Parameters.AddWithValue("@client_id", client_id);
            cmd.Connection = conn;

            conn.Open();
            reader = cmd.ExecuteReader();

            if (reader.HasRows)
            {
                reader.Read();

                clientCompany = new ClientCompany(reader["client_id"].ToString(), reader["name"].ToString(), reader["telephone"].ToString(),
                                                      reader["address"].ToString(), reader["fax"].ToString(), reader["cityTown"].ToString(), reader["contactPerson"].ToString(), reader["nationality"].ToString());

            }
            conn.Close();
            return clientCompany;
        }

        public List<ClientCompany> GetLocalClientCompanies()
        {
            SqlConnection conn = new SqlConnection(SLW_dbConn);
            SqlCommand cmd = new SqlCommand();
            List<ClientCompany> clientCompanies = new List<ClientCompany>();
            SqlDataReader reader = null;
            cmd.CommandText = "sp_getLocalClientCompany @client_id";
            cmd.Parameters.AddWithValue("@client_id", "");
            cmd.Connection = conn;

            conn.Open();
            reader = cmd.ExecuteReader();

            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    clientCompanies.Add(new ClientCompany(reader["client_id"].ToString(), reader["name"].ToString(), reader["telephone"].ToString(),
                                                          reader["address"].ToString(), reader["fax"].ToString(), reader["cityTown"].ToString(), reader["contactPerson"].ToString(), reader["nationality"].ToString()));
                }
            }
            conn.Close();
            return clientCompanies;
        }


        public int NewLocalClient(ClientCompany client)
        {
            SqlConnection conn = new SqlConnection(SLW_dbConn);
            SqlCommand cmd = new SqlCommand();
            SqlDataReader reader = null;

            cmd.CommandText = "sp_newClientCompany @name, @telephone, @address, @fax, @cityTown, @contactPerson, @nationality";
            cmd.Parameters.AddWithValue("@name", client.name);
            cmd.Parameters.AddWithValue("@telephone", client.telephone);
            cmd.Parameters.AddWithValue("@address", client.address);
            cmd.Parameters.AddWithValue("@fax", client.fax);
            cmd.Parameters.AddWithValue("@cityTown", client.cityTown);
            cmd.Parameters.AddWithValue("@contactPerson", client.contactPerson);
            cmd.Parameters.AddWithValue("@nationality", client.nationality);
            cmd.Connection = conn;
            

            conn.Open();
            reader = cmd.ExecuteReader();
            reader.Read();

            int client_id = Convert.ToInt32(reader["client_id"]);
            conn.Close();
            return client_id;
        }
    }
}