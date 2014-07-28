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
    public class GPUSQLTableDataGateway : SQLTableDataGatewayBase
    {
        #region Constructor
        public GPUSQLTableDataGateway(string connectionString)
            :base(connectionString)
        {
        }
        #endregion

        public GPUDataRecord Select(int gpuID)
        {
            GPUDataRecord record = new GPUDataRecord();

            using(SqlCommand cmd = new SqlCommand("GetGPUByID", this.connection))
            {
                cmd.CommandType = CommandType.StoredProcedure;

                try
                {
                    //@gpu_id
                    SqlParameter gpuIDParam = new SqlParameter("@gpu_id", gpuID);
                    cmd.Parameters.Add(gpuIDParam);
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
                            record.GpuID = gpuID;
                            record.Model = (string)reader["model"];
                            record.Price = (decimal)reader["price"];
                            record.GpuModel = (string)reader["gpu_model"];
                            record.GpuClockSpeed = (ushort)(short)reader["gpu_clock_speed"];
                            record.VRamSize = (ushort)(short)reader["vram_size"];
                            record.VRamClockSpeed = (ushort)(short)reader["vram_clock_speed"];
                            record.VRamType = (string)reader["vram_type"];
                            record.GpuType = (string)reader["gpu_type"];
                            record.ManufacturerID = (int)reader["manufacturer_id"];
                        }
                    }
                }
                catch (SqlException ex)
                {
                    Debug.WriteLine(ex.Message);
                    throw;
                }

                return record;
            }
        }

        public void Insert(int manufacturerID, string model, decimal price, string gpuModel, ushort gpuClockSpeed, ushort vramSize, ushort vramClockSpeed, string vramType, string gpuType)
        {
            string insertSQL = "INSERT INTO GPUs" +
                "(manufacturer_id, model, price, gpu_model, gpu_clock_speed, vram_size, vram_clock_speed, vram_type, gpu_type)" +
                " VALUES(@manufacturer_id, @model, @price, @gpu_model, @gpu_clock_speed, @vram_size, @vram_clock_speed, @vram_type, @gpu_type)";

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

                    //gpu_model
                    SqlParameter gpuModelParam = new SqlParameter("@gpu_model", gpuModel);
                    gpuModelParam.SqlDbType = SqlDbType.NChar;
                    gpuModelParam.Size = 10;
                    cmd.Parameters.Add(gpuModelParam);

                    //gpu_clock_speed
                    SqlParameter gpuClockSpeedParam = new SqlParameter("@gpu_clock_speed", gpuClockSpeed);
                    gpuClockSpeedParam.SqlDbType = SqlDbType.SmallInt;
                    cmd.Parameters.Add(gpuClockSpeedParam);

                    //vram_size
                    SqlParameter vramSizeParam = new SqlParameter("@vram_size", vramSize);
                    vramSizeParam.SqlDbType = SqlDbType.SmallInt;
                    cmd.Parameters.Add(vramSizeParam);

                    //vram_clock_speed
                    SqlParameter vramClockSpeedParam = new SqlParameter("@vram_clock_speed", vramClockSpeed);
                    vramClockSpeedParam.SqlDbType = SqlDbType.SmallInt;
                    cmd.Parameters.Add(vramClockSpeedParam);

                    //vram_type
                    SqlParameter vramTypeParam = new SqlParameter("@vram_type", vramType);
                    vramTypeParam.SqlDbType = SqlDbType.NChar;
                    vramTypeParam.Size = 10;
                    cmd.Parameters.Add(vramTypeParam);

                    //gpu_type
                    SqlParameter gpuTypeParam = new SqlParameter("@gpu_type", gpuType);
                    gpuTypeParam.SqlDbType = SqlDbType.NChar;
                    gpuTypeParam.Size = 10;
                    cmd.Parameters.Add(gpuTypeParam);
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

        public void Update(int gpuID, int manufacturerID, string model, decimal price, string gpuModel, ushort gpuClockSpeed, ushort vramSize, ushort vramClockSpeed, string vramType, string gpuType)
        {
            string updateSQL = @"UPDATE GPUs
                                 SET manufacturer_id = @manufacturer_id,
                                     model = @model,
                                     price = @price,
                                     gpu_model = @gpu_model,
                                     gpu_clock_speed = @gpu_clock_speed,
                                     vram_size = @vram_size,
                                     vram_clock_speed = @vram_clock_speed,
                                     vram_type = @vram_type,
                                     gpu_type = @gpu_type
                                 WHERE  gpu_id = @gpu_id";

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

                    //gpu_model
                    SqlParameter gpuModelParam = new SqlParameter("@gpu_model", gpuModel);
                    gpuModelParam.SqlDbType = SqlDbType.NChar;
                    gpuModelParam.Size = 10;
                    cmd.Parameters.Add(gpuModelParam);

                    //gpu_clock_speed
                    SqlParameter gpuClockSpeedParam = new SqlParameter("@gpu_clock_speed", gpuClockSpeed);
                    gpuClockSpeedParam.SqlDbType = SqlDbType.SmallInt;
                    cmd.Parameters.Add(gpuClockSpeedParam);

                    //vram_size
                    SqlParameter vramSizeParam = new SqlParameter("@vram_size", vramSize);
                    vramSizeParam.SqlDbType = SqlDbType.SmallInt;
                    cmd.Parameters.Add(vramSizeParam);

                    //vram_clock_speed
                    SqlParameter vramClockSpeedParam = new SqlParameter("@vram_clock_speed", vramClockSpeed);
                    vramClockSpeedParam.SqlDbType = SqlDbType.SmallInt;
                    cmd.Parameters.Add(vramClockSpeedParam);

                    //vram_type
                    SqlParameter vramTypeParam = new SqlParameter("@vram_type", vramType);
                    vramTypeParam.SqlDbType = SqlDbType.NChar;
                    vramTypeParam.Size = 10;
                    cmd.Parameters.Add(vramTypeParam);

                    //gpu_type
                    SqlParameter gpuTypeParam = new SqlParameter("@gpu_type", gpuType);
                    gpuTypeParam.SqlDbType = SqlDbType.NChar;
                    gpuTypeParam.Size = 10;
                    cmd.Parameters.Add(gpuTypeParam);

                    //gpu_id
                    SqlParameter gpuIDParam = new SqlParameter("@gpu_id", gpuID);
                    cmd.Parameters.Add(gpuIDParam);
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

        public void Delete(int gpuID)
        {
            string deleteSQL = "DELETE from GPUs WHERE gpu_id = @gpu_id";

            using (SqlCommand cmd = new SqlCommand(deleteSQL, this.connection))
            {
                try
                {
                    //gpu_id
                    SqlParameter gpuIDParam = new SqlParameter("@gpu_id", gpuID);
                    cmd.Parameters.Add(gpuIDParam);
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

        public IList<GPUDataRecord> GetAll()
        {
            //this will hold the recods.
            DataTable gpusTable = new DataTable();
            List<GPUDataRecord> list = new List<GPUDataRecord>();

            //get all the gpu_id's
            string sql = "SELECT gpu_id FROM GPUs ORDER BY gpu_id";

            using (SqlCommand cmd = new SqlCommand(sql, this.connection))
            {
                SqlDataReader reader = cmd.ExecuteReader();
                gpusTable.Load(reader);
                reader.Close();
            }

            foreach (DataRow row in gpusTable.Rows)
            {
                GPUDataRecord gpu = Select((int)row[0]);
                list.Add(gpu);
            }

            return list;
        }

    }
}
