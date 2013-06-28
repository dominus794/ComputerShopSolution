using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Data;
using System.Data.Common;
using System.Data.SqlClient;

//Debugging
using System.Diagnostics;

// Import DataRecord Classes
using ComputerShop.DAL.SQL.DataRecords;

namespace ComputerShop.DAL.SQL.TableDataGateways
{
    public class RamModuleSQLTableDataGateway : SQLTableDataGatewayBase
    {
        #region Constructor
		public RamModuleSQLTableDataGateway(string connectionString)
			: base(connectionString)
		{
		}
		#endregion

        public RamModuleDataRecord Select(int ramID) 
        {
            RamModuleDataRecord record = new RamModuleDataRecord();

            using (SqlCommand cmd = new SqlCommand("GetRamModuleByID", this.connection))
            {
                cmd.CommandType = CommandType.StoredProcedure;

                try
                {
                    //@ram_id
                    SqlParameter ramIDParam = new SqlParameter("@ram_id", ramID);
                    cmd.Parameters.Add(ramIDParam);
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
                            record.RamModuleID = ramID;
                            record.Model = (string)reader["model"];
                            record.Price = (decimal)reader["price"];
                            record.ClockSpeed = (ushort)(short)reader["bus_speed"];
                            record.Size = (ushort)(short)reader["size"];
                            record.DDRVersion = (string)reader["ddr_version"];
                            record.RamFormFactor = (string)reader["ram_form_factor"];
                            record.ManufacturerID = (int)reader["manufacturer_id"];
                        }
                    }
                }
                catch (SqlException ex)
                {
                    Debug.WriteLine(ex.Message);
                    throw ex;
                }              
            }

            return record;
        }

        public void Insert(int manufacturerID, string model, decimal price, int clockSpeed, int size, string ddrVersion, string ramFormFactor)
        {
            string insertSQL = "INSERT INTO RamModules" +
                "(manufacturer_id, model, price, bus_speed, size, ddr_version, ram_form_factor)" +
                " VALUES(@manufacturer_id, @model, @price, @bus_speed, @size, @ddr_version, @ram_form_factor)";

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

                    //bus_speed
                    SqlParameter busSpeedParam = new SqlParameter("@bus_speed", clockSpeed);
                    cmd.Parameters.Add(busSpeedParam);

                    //size
                    SqlParameter sizeParam = new SqlParameter("@size", size);
                    cmd.Parameters.Add(sizeParam);

                    //ddr_version
                    SqlParameter ddrVersionParam = new SqlParameter("@ddr_version", ddrVersion);
                    ddrVersionParam.SqlDbType = SqlDbType.NChar;
                    ddrVersionParam.Size = 4;
                    cmd.Parameters.Add(ddrVersionParam);

                    //ram_form_factor
                    SqlParameter ramFormFactorParam = new SqlParameter("@ram_form_factor", ramFormFactor);
                    ramFormFactorParam.SqlDbType = SqlDbType.NChar;
                    ramFormFactorParam.Size = 10;
                    cmd.Parameters.Add(ramFormFactorParam);
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

        public void Update(int ramID, int manufacturerID, string model, decimal price, int clockSpeed, int size, string ddrVersion, string ramFormFactor) 
        {
            string updateSQL = @"UPDATE RamModules
                                 SET manufacturer_id = @manufacturer_id,
                                     model = @model,
                                     price = @price,
                                     bus_speed = @bus_speed,
                                     size = @size,
                                     ddr_version = @ddr_version,
                                     ram_form_factor = @ram_form_factor
                                 WHERE ram_id = @ram_id";

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

                    //bus_speed
                    SqlParameter busSpeedParam = new SqlParameter("@bus_speed", clockSpeed);
                    cmd.Parameters.Add(busSpeedParam);

                    //size
                    SqlParameter sizeParam = new SqlParameter("@size", size);
                    cmd.Parameters.Add(sizeParam);

                    //ddr_version
                    SqlParameter ddrVersionParam = new SqlParameter("@ddr_version", ddrVersion);
                    ddrVersionParam.SqlDbType = SqlDbType.NChar;
                    ddrVersionParam.Size = 4;
                    cmd.Parameters.Add(ddrVersionParam);

                    //ram_form_factor
                    SqlParameter ramFormFactorParam = new SqlParameter("@ram_form_factor", ramFormFactor);
                    ramFormFactorParam.SqlDbType = SqlDbType.NChar;
                    ramFormFactorParam.Size = 10;
                    cmd.Parameters.Add(ramFormFactorParam);

                    //ram_id
                    SqlParameter ramIDParam = new SqlParameter("@ram_id", ramID);
                    cmd.Parameters.Add(ramIDParam);
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

        public void Delete(int ramID) 
        {
            string deleteSQL = "DELETE from RamModules WHERE ram_id = @ram_id";

            using (SqlCommand cmd = new SqlCommand(deleteSQL, this.connection))
            {
                try
                {
                    //ram_id
                    SqlParameter ramIDParam = new SqlParameter("@ram_id", ramID);
                    cmd.Parameters.Add(ramIDParam);
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

        public IList<RamModuleDataRecord> GetAll() 
        {
            // this will hold all the records.
            DataTable ramModulesTable = new DataTable();
            List<RamModuleDataRecord> list = new List<RamModuleDataRecord>();

            // get all the ram_id's
            string sql = "SELECT ram_id FROM RamModules ORDER BY ram_id";

            using (SqlCommand cmd = new SqlCommand(sql, this.connection))
            {
                SqlDataReader reader = cmd.ExecuteReader();
                ramModulesTable.Load(reader);
                reader.Close();
            }

            foreach (DataRow row in ramModulesTable.Rows)
            {
                RamModuleDataRecord ramModule = Select((int)row[0]);
                list.Add(ramModule);
            }

            return list;
        }
    }
}
