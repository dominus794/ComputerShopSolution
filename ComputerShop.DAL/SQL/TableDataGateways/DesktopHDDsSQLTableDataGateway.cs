using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

//SQL Server Interfaces
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;

//Debugging
using System.Diagnostics;

// Import DataRecord Classes
using ComputerShop.DAL.SQL.DataRecords;

namespace ComputerShop.DAL.SQL.TableDataGateways
{
    public class DesktopHDDsSQLTableDataGateway : SQLTableDataGatewayBase
    {
        #region Constructor
        public DesktopHDDsSQLTableDataGateway(string connectionString)
            : base(connectionString)
        {
        }
        #endregion

        public IList<DesktopHDDDataRecord> Select(int desktopID)
        {
            IList<DesktopHDDDataRecord> records = new List<DesktopHDDDataRecord>();

            string selectSQL = "SELECT * from DesktopHDDs WHERE desktop_id = @desktop_id";

            using (SqlCommand cmd = new SqlCommand(selectSQL, this.connection))
            {
                try
                {
                    //@desktop_id
                    SqlParameter desktopIDParam = new SqlParameter("@desktop_id", desktopID);
                    cmd.Parameters.Add(desktopIDParam);
                }
                catch (Exception ex)
                {
                    Debug.WriteLine(ex.Message);
                    throw;
                }

                try
                {
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            DesktopHDDDataRecord record = new DesktopHDDDataRecord();
                            record.DesktopID = desktopID;
                            record.HddID = (int)reader["hdd_id"];
                            record.Quantity = (int)reader["quantity"];
                            // add to the list
                            records.Add(record);
                        }
                    }
                }
                catch (SqlException ex)
                {
                    Debug.WriteLine(ex.Message);
                    throw;
                }

                return records;
            }
        }

        public void Insert(int desktopID, int hddID, int quantity)
        {
            string insertSQL = "INSERT INTO DesktopHDDs" +
                "(desktop_id, hdd_id, quantity)" +
                " VALUES(@desktop_id, @hdd_id, @quantity)";

            using (SqlCommand cmd = new SqlCommand(insertSQL, this.connection))
            {
                try
                {
                    //desktop_id
                    SqlParameter desktopIDParam = new SqlParameter("@desktop_id", desktopID);
                    cmd.Parameters.Add(desktopIDParam);

                    //ram_id
                    SqlParameter hddIDParam = new SqlParameter("@hdd_id", hddID);
                    cmd.Parameters.Add(hddIDParam);

                    //quantity
                    SqlParameter quantityParam = new SqlParameter("@quantity", quantity);
                    cmd.Parameters.Add(quantityParam);
                }
                catch (Exception ex)
                {
                    Debug.WriteLine(ex.Message);
                    throw;
                }

                try
                {
                    //Execute the query
                    cmd.ExecuteNonQuery();
                }
                catch (SqlException ex)
                {
                    Debug.WriteLine(ex.Message);
                    throw;
                }
            }
        }

        public void Update(int desktopID, int hddID, int quantity)
        {
            string updateSQL = @"UPDATE DesktopHDDs
                                 SET desktop_id = @desktop_id,
                                     hdd_id = @hdd_id,
                                     quantity = @quantity
                                 WHERE desktop_id = @desktop_id";

            using (SqlCommand cmd = new SqlCommand(updateSQL, this.connection))
            {
                try
                {
                    //desktop_id
                    SqlParameter desktopIDParam = new SqlParameter("@desktop_id", desktopID);
                    cmd.Parameters.Add(desktopIDParam);

                    //ram_id
                    SqlParameter hddIDParam = new SqlParameter("@hdd_id", hddID);
                    cmd.Parameters.Add(hddIDParam);

                    //quantity
                    SqlParameter quantityParam = new SqlParameter("@quantity", quantity);
                    cmd.Parameters.Add(quantityParam);
                }
                catch (Exception ex)
                {
                    Debug.WriteLine(ex.Message);
                    throw;
                }

                try
                {
                    //Execute the query
                    cmd.ExecuteNonQuery();
                }
                catch (SqlException ex)
                {
                    Debug.WriteLine(ex.Message);
                    throw;
                }
            }
        }

        public void Delete(int desktopID)
        {
            string deleteSQL = "DELETE from DesktopHDDs WHERE desktop_id = @desktop_id";

            using (SqlCommand cmd = new SqlCommand(deleteSQL, this.connection))
            {
                try
                {
                    //gpu_id
                    SqlParameter desktopIDParam = new SqlParameter("@desktop_id", desktopID);
                    cmd.Parameters.Add(desktopIDParam);
                }
                catch (Exception ex)
                {
                    Debug.WriteLine(ex.Message);
                    throw;
                }

                try
                {
                    cmd.ExecuteNonQuery();
                }
                catch (SqlException ex)
                {
                    Debug.WriteLine(ex.Message);
                    throw;
                }
            }
        }
    }
}
