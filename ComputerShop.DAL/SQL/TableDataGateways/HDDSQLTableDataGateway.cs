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
    public class HDDSQLTableDataGateway : SQLTableDataGatewayBase
    {
        #region Constructor
        public HDDSQLTableDataGateway(string connectionString)
            :base(connectionString)
        {
        }
        #endregion

        public HDDDataRecord Select(int hddID) 
        {
            HDDDataRecord record = new HDDDataRecord();

            using (SqlCommand cmd = new SqlCommand("GetHDDByID", this.connection))
            {
                cmd.CommandType = CommandType.StoredProcedure;

                try
                {
                    //@hdd_id
                    SqlParameter hddIDParam = new SqlParameter("@hdd_id", hddID);
                    cmd.Parameters.Add(hddIDParam);
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
                            record.HddID = hddID;
                            record.Model = (string)reader["model"];
                            record.Price = (decimal)reader["price"];
                            record.Capacity = (ushort)(short)reader["capacity"];
                            record.Speed = (ushort)(short)reader["speed"];
                            record.HDDType = (string)reader["hdd_type"];
                            record.HDDFormFactor = (string)reader["hdd_form_factor"];
                            record.HDDInterface = (string)reader["hdd_interface"];
                            record.ManufacturerID = (int)reader["manufacturer_id"];
                        }
                    }
                }
                catch (SqlException ex)
                {
                    Debug.WriteLine(ex.Message);
                    throw;
                }
            }

            return record;
        }

        public void Insert(int manufacturerID, string model, decimal price, ushort capacity, ushort speed, string hddType, string hddFormFactor, string hddInterface)
        {
            string insertSQL = "INSERT INTO HDDs" +
                "(manufacturer_id, model, price, capacity, speed, hdd_type, hdd_form_factor, hdd_interface)" +
                " VALUES(@manufacturer_id, @model, @price, @capacity, @speed, @hdd_type, @hdd_form_factor, @hdd_interface)";

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

                    //capacity
                    SqlParameter capacityParam = new SqlParameter("@capacity", capacity);
                    capacityParam.SqlDbType = SqlDbType.SmallInt;
                    cmd.Parameters.Add(capacityParam);

                    //speed
                    SqlParameter speedParam = new SqlParameter("@speed", speed);
                    speedParam.SqlDbType = SqlDbType.SmallInt;
                    cmd.Parameters.Add(speedParam);

                    //hdd_type
                    SqlParameter hddTypeParam = new SqlParameter("@hdd_type", hddType);
                    hddTypeParam.SqlDbType = SqlDbType.NChar;
                    hddTypeParam.Size = 10;
                    cmd.Parameters.Add(hddTypeParam);

                    //hdd_form_factor
                    SqlParameter hddFormFactorParam = new SqlParameter("@hdd_form_factor", hddFormFactor);
                    hddFormFactorParam.SqlDbType = SqlDbType.NChar;
                    hddFormFactorParam.Size = 10;
                    cmd.Parameters.Add(hddFormFactorParam);

                    //hdd_interface
                    SqlParameter hddInterfaceParam = new SqlParameter("@hdd_interface", hddInterface);
                    hddInterfaceParam.SqlDbType = SqlDbType.NChar;
                    hddInterfaceParam.Size = 10;
                    cmd.Parameters.Add(hddInterfaceParam);
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

        public void Update(int hddId, int manufacturerID, string model, decimal price, ushort capacity, ushort speed, string hddType, string hddFormFactor, string hddInterface)
        {
            string updateSQL = @"UPDATE HDDs
                                 SET manufacturer_id = @manufacturer_id,
                                     model = @model,
                                     price = @price,
                                     capacity = @capacity,
                                     speed = @speed,
                                     hdd_type = @hdd_type,
                                     hdd_form_factor = @hdd_form_factor,
                                     hdd_interface = @hdd_interface
                                 WHERE hdd_id = @hdd_id";

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

                    //capacity
                    SqlParameter capacityParam = new SqlParameter("@capacity", capacity);
                    capacityParam.SqlDbType = SqlDbType.SmallInt;
                    cmd.Parameters.Add(capacityParam);

                    //speed
                    SqlParameter speedParam = new SqlParameter("@speed", speed);
                    speedParam.SqlDbType = SqlDbType.SmallInt;
                    cmd.Parameters.Add(speedParam);

                    //hdd_type
                    SqlParameter hddTypeParam = new SqlParameter("@hdd_type", hddType);
                    hddTypeParam.SqlDbType = SqlDbType.NChar;
                    hddTypeParam.Size = 10;
                    cmd.Parameters.Add(hddTypeParam);

                    //hdd_form_factor
                    SqlParameter hddFormFactorParam = new SqlParameter("@hdd_form_factor", hddFormFactor);
                    hddFormFactorParam.SqlDbType = SqlDbType.NChar;
                    hddFormFactorParam.Size = 10;
                    cmd.Parameters.Add(hddFormFactorParam);

                    //hdd_interface
                    SqlParameter hddInterfaceParam = new SqlParameter("@hdd_interface", hddInterface);
                    hddInterfaceParam.SqlDbType = SqlDbType.NChar;
                    hddInterfaceParam.Size = 10;
                    cmd.Parameters.Add(hddInterfaceParam);

                    //hdd_id
                    SqlParameter hddIDParam = new SqlParameter("@hdd_id", hddId);
                    cmd.Parameters.Add(hddIDParam);
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

        public void Delete(int hddID) 
        {
            string deleteSQL = "DELETE from HDDs WHERE hdd_id = @hdd_id";

            using (SqlCommand cmd = new SqlCommand(deleteSQL, this.connection))
            {
                try
                {
                    //hdd_id
                    SqlParameter hddIDParam = new SqlParameter("@hdd_id", hddID);
                    cmd.Parameters.Add(hddIDParam);
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

        public IList<HDDDataRecord> GetAll() 
        {
            //this will hold the records.
            DataTable hddsTable = new DataTable();
            List<HDDDataRecord> list = new List<HDDDataRecord>();

            //get all the hdd_id's
            string sql = "SELECT hdd_id FROM HDDs ORDER BY hdd_id";

            using (SqlCommand cmd = new SqlCommand(sql, this.connection))
            {
                SqlDataReader reader = cmd.ExecuteReader();

                hddsTable.Load(reader);
                reader.Close();
            }

            foreach (DataRow row in hddsTable.Rows)
            {
                HDDDataRecord hdd = Select((int)row[0]);
                list.Add(hdd);
            }

            return list;
        }
    }
}
