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
    public class DesktopGPUsSQLTableDataGateway : SQLTableDataGatewayBase
    {
        #region Constructor
        public DesktopGPUsSQLTableDataGateway(string connectionString)
            : base(connectionString)
        {
        }
        #endregion

        public IList<DesktopGPUDataRecord> Select(int desktopID)
        {
            IList<DesktopGPUDataRecord> records = new List<DesktopGPUDataRecord>();

            string selectSQL = "SELECT * from DesktopGPUs WHERE desktop_id = @desktop_id";

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
                            DesktopGPUDataRecord record = new DesktopGPUDataRecord();
                            record.DesktopID = desktopID;
                            record.GpuID = (int)reader["gpu_id"];
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

        public void Insert(int desktopID, int gpuID, int quantity)
        {
            string insertSQL = "INSERT INTO DesktopGPUs" +
                "(desktop_id, gpu_id, quantity)" +
                " VALUES(@desktop_id, @gpu_id, @quantity)";

            using (SqlCommand cmd = new SqlCommand(insertSQL, this.connection))
            {
                try
                {
                    //desktop_id
                    SqlParameter desktopIDParam = new SqlParameter("@desktop_id", desktopID);
                    cmd.Parameters.Add(desktopIDParam);

                    //ram_id
                    SqlParameter gpuIDParam = new SqlParameter("@gpu_id", gpuID);
                    cmd.Parameters.Add(gpuIDParam);

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

        public void Update(int desktopID, int gpuID, int quantity)
        {
            string updateSQL = @"UPDATE DesktopGPUs
                                 SET desktop_id = @desktop_id,
                                     gpu_id = @gpu_id,
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
                    SqlParameter gpuIDParam = new SqlParameter("@gpu_id", gpuID);
                    cmd.Parameters.Add(gpuIDParam);

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
            string deleteSQL = "DELETE from DesktopGPUs WHERE desktop_id = @desktop_id";

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
