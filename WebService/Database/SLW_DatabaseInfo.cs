using System;
using System.Collections.Generic;
using WebService.Models;
using System.Data;
using System.Data.SqlClient;

namespace WebService.Database
{
    public class SLW_DatabaseInfo
    {
        string SLW_dbConn = @"Data Source=SMA-DBSRV\ASMSDEV;Initial Catalog=SLW_Database;Integrated Security=True";

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
    }
}