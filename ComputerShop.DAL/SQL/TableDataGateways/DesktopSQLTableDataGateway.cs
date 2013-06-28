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
    public class DesktopSQLTableDataGateway : SQLTableDataGatewayBase
    {
        #region Constructor
        public DesktopSQLTableDataGateway(string connectionString)
            : base(connectionString)
        {
        }
        #endregion

        public DesktopDataRecord Select(int desktopID)
        {
            DesktopDataRecord record = new DesktopDataRecord();

            string selectSQL = "SELECT * FROM Desktops WHERE desktop_id = @desktop_id";

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
                            record.DesktopID = desktopID;
                            record.ManufacturerID = (int)reader["manufacturer_id"];
                            record.Model = (string)reader["model"];
                            record.Price = (decimal)reader["price"];
                            record.CpuID = (int)reader["cpu_id"];
                        }
                    }
                }
                catch (SqlException ex)
                {
                    Debug.WriteLine(ex.Message);
                    throw ex;
                }

                return record;
            }
        }

        public void Insert(int manufacturerID, string model, decimal price, int cpuID)
        {
            string insertSQL = "INSERT INTO Desktops" +
                "(manufacturer_id, model, price, cpu_id)" +
                " VALUES(@manufacturer_id, @model, @price, @cpu_id)";

            using (SqlCommand cmd = new SqlCommand(insertSQL, this.connection))
            {
                try
                {
                    //manufacturer_id
                    SqlParameter manufacturerIDParam = new SqlParameter("@manufacturer_id", manufacturerID);
                    cmd.Parameters.Add(manufacturerIDParam);

                    //model
                    SqlParameter modelParam = new SqlParameter("@model", model);
                    cmd.Parameters.Add(modelParam);

                    //price
                    SqlParameter priceParam = new SqlParameter("@price", price);
                    cmd.Parameters.Add(priceParam);

                    //cpu_id
                    SqlParameter cpuIDParam = new SqlParameter("@cpu_id", cpuID);
                    cmd.Parameters.Add(cpuIDParam);
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

        public int GetLatestID()
        {
            int latestID = 0;

            // Get the desktop_id of the inserted desktop record.
            string selectLatestIDSQL = "SELECT IDENT_CURRENT('Desktops') as desktop_id";

            using (SqlCommand selectLatestIDSQLCmd = new SqlCommand(selectLatestIDSQL, this.connection))
            {
                using (SqlDataReader reader = selectLatestIDSQLCmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        latestID = (int)(decimal)reader["desktop_id"];
                    }
                }
            }

            return latestID;
        }

        public void Update(int desktopID, int manufacturerID, string model, decimal price, int cpuID)
        {
            string updateSQL = @"UPDATE Desktops
                                 SET manufacturer_id = @manufacturer_id,
                                     model = @model,
                                     price = @price,
                                     cpu_id = @cpu_id
                                 WHERE desktop_id = @desktop_id";

            using (SqlCommand cmd = new SqlCommand(updateSQL, this.connection))
            {
                try
                {
                    //manufacturer_id
                    SqlParameter manufacturerIDParam = new SqlParameter("@manufacturer_id", manufacturerID);
                    cmd.Parameters.Add(manufacturerIDParam);

                    //model
                    SqlParameter modelParam = new SqlParameter("@model", model);
                    cmd.Parameters.Add(modelParam);

                    //price
                    SqlParameter priceParam = new SqlParameter("@price", price);
                    cmd.Parameters.Add(priceParam);

                    //cpu_id
                    SqlParameter cpuIDParam = new SqlParameter("@cpu_id", cpuID);
                    cmd.Parameters.Add(cpuIDParam);

                    //desktop_id
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
            string deleteSQL = "DELETE from Desktops WHERE desktop_id = @desktop_id";

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

        public IList<DesktopDataRecord> GetAll()
        {
            //this will hold the records.
            DataTable desktopsTable = new DataTable();
            List<DesktopDataRecord> list = new List<DesktopDataRecord>();

            //get all the laptop_id's
            string sql = "SELECT desktop_id FROM Desktops ORDER BY desktop_id";

            using (SqlCommand cmd = new SqlCommand(sql, this.connection))
            {
                SqlDataReader reader = cmd.ExecuteReader();
                desktopsTable.Load(reader);
                reader.Close();
            }

            foreach (DataRow row in desktopsTable.Rows)
            {
                DesktopDataRecord desktop = Select((int)row[0]);
                list.Add(desktop);
            }

            return list;
        }
    }
}
