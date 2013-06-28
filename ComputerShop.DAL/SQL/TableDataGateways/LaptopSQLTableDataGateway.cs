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
    public class LaptopSQLTableDataGateway : SQLTableDataGatewayBase
    {
        #region Constructor
        public LaptopSQLTableDataGateway(string connectionString)
            : base(connectionString)
        {
        }
        #endregion

        public LaptopDataRecord Select(int laptopID)
        {
            LaptopDataRecord record = new LaptopDataRecord();

            string selectSQL = "SELECT * from Laptops WHERE laptop_id = @laptop_id";

            using (SqlCommand cmd = new SqlCommand(selectSQL, this.connection))
            {
                try
                {
                    //@laptop_id
                    SqlParameter laptopIDParam = new SqlParameter("@laptop_id", laptopID);
                    cmd.Parameters.Add(laptopIDParam);
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
                            record.LaptopID = laptopID;
                            record.ManufacturerID = (int)reader["manufacturer_id"];
                            record.Model = (string)reader["model"];
                            record.Price = (decimal)reader["price"];
                            record.Weight = (byte)reader["weight"];
                            record.BatteryLife = (byte)reader["battery_life"];
                            float displaySize = 0.0f;
                            Single.TryParse(reader["display_size"].ToString(), out displaySize);
                            record.DisplaySize = displaySize;
                            record.CpuID = (int)reader["cpu_id"];
                            record.HddID = (int)reader["hdd_id"];
                            record.GpuID = (int)reader["gpu_id"];
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

        public void Insert(int manufacturerID, string model, decimal price, int weight, int batteryLife, float displaySize, int cpuID, int hddID, int gpuID)
        {            
            string insertSQL = "INSERT INTO Laptops" +
                "(manufacturer_id, model, price, weight, battery_life, display_size, cpu_id, hdd_id, gpu_id)" +
                " VALUES(@manufacturer_id, @model, @price, @weight, @battery_life, @display_size, @cpu_id, @hdd_id, @gpu_id)";

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

                    //weight
                    SqlParameter weightParam = new SqlParameter("@weight", weight);
                    cmd.Parameters.Add(weightParam);

                    //batteryLife
                    SqlParameter batteryLifeParam = new SqlParameter("@battery_life", batteryLife);
                    cmd.Parameters.Add(batteryLifeParam);

                    //displaySize
                    SqlParameter displaySizeParam = new SqlParameter("@display_size", displaySize);
                    cmd.Parameters.Add(displaySizeParam);

                    //cpuID
                    SqlParameter cpuIDParam = new SqlParameter("@cpu_id", cpuID);
                    cmd.Parameters.Add(cpuIDParam);

                    //hddID
                    SqlParameter hddIDParam = new SqlParameter("@hdd_id", hddID);
                    cmd.Parameters.Add(hddIDParam);

                    //gpuID
                    SqlParameter gpuIDParam = new SqlParameter("@gpu_id", gpuID);
                    cmd.Parameters.Add(gpuIDParam);
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
            string selectLaptopIDSQL = "SELECT IDENT_CURRENT('Laptops') as laptop_id";

            using (SqlCommand selectLaptopIDSQLCmd = new SqlCommand(selectLaptopIDSQL, this.connection))
            {
                using (SqlDataReader reader = selectLaptopIDSQLCmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        latestID = (int)(decimal)reader["laptop_id"];
                    }                    
                }
            }

            return latestID;
        }

        public void Update(int laptopID, int manufacturerID, string model, decimal price, int weight, int batteryLife, float displaySize, int cpuID, int hddID, int gpuID)
        {
            string updateSQL = @"UPDATE Laptops
                                 SET manufacturer_id = @manufacturer_id,
                                     model = @model,
                                     price = @price,
                                     weight = @weight,
                                     battery_life = @battery_life,
                                     display_size = @display_size,
                                     cpu_id = @cpu_id,
                                     hdd_id = @hdd_id,
                                     gpu_id = @gpu_id
                                 WHERE laptop_id = @laptop_id";

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

                    //weight
                    SqlParameter weightParam = new SqlParameter("@weight", weight);
                    cmd.Parameters.Add(weightParam);

                    //batteryLife
                    SqlParameter batteryLifeParam = new SqlParameter("@battery_life", batteryLife);
                    cmd.Parameters.Add(batteryLifeParam);

                    //displaySize
                    SqlParameter displaySizeParam = new SqlParameter("@display_size", displaySize);
                    cmd.Parameters.Add(displaySizeParam);

                    //cpuID
                    SqlParameter cpuIDParam = new SqlParameter("@cpu_id", cpuID);
                    cmd.Parameters.Add(cpuIDParam);

                    //hddID
                    SqlParameter hddIDParam = new SqlParameter("@hdd_id", hddID);
                    cmd.Parameters.Add(hddIDParam);

                    //gpuID
                    SqlParameter gpuIDParam = new SqlParameter("@gpu_id", gpuID);
                    cmd.Parameters.Add(gpuIDParam);

                    //laptopID
                    SqlParameter laptopIDParam = new SqlParameter("@laptop_id", laptopID);
                    cmd.Parameters.Add(laptopIDParam);
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

        public void Delete(int laptopID)
        {
            string deleteSQL = "DELETE from Laptops WHERE laptop_id = @laptop_id";

            using (SqlCommand cmd = new SqlCommand(deleteSQL, this.connection))
            {
                try
                {
                    //gpu_id
                    SqlParameter laptopIDParam = new SqlParameter("@laptop_id", laptopID);
                    cmd.Parameters.Add(laptopIDParam);
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

        public IList<LaptopDataRecord> GetAll()
        {
            //this will hold the records.
            DataTable laptopsTable = new DataTable();
            List<LaptopDataRecord> list = new List<LaptopDataRecord>();

            //get all the laptop_id's
            string sql = "SELECT laptop_id FROM Laptops ORDER BY laptop_id";

            using (SqlCommand cmd = new SqlCommand(sql, this.connection))
            {
                SqlDataReader reader = cmd.ExecuteReader();
                laptopsTable.Load(reader);
                reader.Close();
            }

            foreach (DataRow row in laptopsTable.Rows)
            {
                LaptopDataRecord laptop = Select((int)row[0]);
                list.Add(laptop);
            }

            return list;
        }
    }
}
