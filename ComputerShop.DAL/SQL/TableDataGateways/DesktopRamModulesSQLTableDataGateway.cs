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
    public class DesktopRamModulesSQLTableDataGateway : SQLTableDataGatewayBase
    {
        #region Constructor
        public DesktopRamModulesSQLTableDataGateway(string connectionString)
            : base(connectionString)
        {
        }
        #endregion

        public IList<DesktopRamModuleDataRecord> Select(int desktopID)
        {
            IList<DesktopRamModuleDataRecord> records = new List<DesktopRamModuleDataRecord>();

            string selectSQL = "SELECT * from DesktopRamModules WHERE desktop_id = @desktop_id";

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
                    throw ex;
                }

                try
                {
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            DesktopRamModuleDataRecord record = new DesktopRamModuleDataRecord();
                            record.DesktopID = desktopID;
                            record.RamID = (int)reader["ram_id"];
                            record.Quantity = (int)reader["quantity"];
                            // add to the list
                            records.Add(record);
                        }
                    }
                }
                catch (SqlException ex)
                {
                    Debug.WriteLine(ex.Message);
                    throw ex;
                }

                return records;
            }
        }

        public void Insert(int desktopID, int ramID, int quantity)
        {
            string insertSQL = "INSERT INTO DesktopRamModules" +
                "(desktop_id, ram_id, quantity)" +
                " VALUES(@desktop_id, @ram_id, @quantity)";

            using (SqlCommand cmd = new SqlCommand(insertSQL, this.connection))
            {
                try
                {
                    //desktop_id
                    SqlParameter desktopIDParam = new SqlParameter("@desktop_id", desktopID);
                    cmd.Parameters.Add(desktopIDParam);

                    //ram_id
                    SqlParameter ramIDParam = new SqlParameter("@ram_id", ramID);
                    cmd.Parameters.Add(ramIDParam);

                    //quantity
                    SqlParameter quantityParam = new SqlParameter("@quantity", quantity);
                    cmd.Parameters.Add(quantityParam);
                }
                catch (Exception ex)
                {
                    Debug.WriteLine(ex.Message);
                    throw ex;
                }

                try
                {
                    //Execute the query
                    cmd.ExecuteNonQuery();
                }
                catch (SqlException ex)
                {
                    Debug.WriteLine(ex.Message);
                    throw ex;
                }
            }
        }

        public void Update(int desktopID, int ramID, int quantity)
        {
            string updateSQL = @"UPDATE DesktopRamModules
                                 SET desktop_id = @desktop_id,
                                     ram_id = @ram_id,
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
                    SqlParameter ramIDParam = new SqlParameter("@ram_id", ramID);
                    cmd.Parameters.Add(ramIDParam);

                    //quantity
                    SqlParameter quantityParam = new SqlParameter("@quantity", quantity);
                    cmd.Parameters.Add(quantityParam);
                }
                catch (Exception ex)
                {
                    Debug.WriteLine(ex.Message);
                    throw ex;
                }

                try
                {
                    //Execute the query
                    cmd.ExecuteNonQuery();
                }
                catch (SqlException ex)
                {
                    Debug.WriteLine(ex.Message);
                    throw ex;
                }
            }
        }

        public void Delete(int desktopID)
        {
            string deleteSQL = "DELETE from DesktopRamModules WHERE desktop_id = @desktop_id";

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
                    throw ex;
                }

                try
                {
                    cmd.ExecuteNonQuery();
                }
                catch (SqlException ex)
                {
                    Debug.WriteLine(ex.Message);
                    throw ex;
                }
            }
        }

    }
}
