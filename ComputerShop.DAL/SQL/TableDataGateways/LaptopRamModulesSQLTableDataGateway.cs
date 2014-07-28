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
    public class LaptopRamModulesSQLTableDataGateway : SQLTableDataGatewayBase
    {
        #region Constructor
        public LaptopRamModulesSQLTableDataGateway(string connectionString)
            : base(connectionString)
        {
        }
        #endregion

        public IList<LaptopRamModuleDataRecord> Select(int laptopID)
        {
            IList<LaptopRamModuleDataRecord> records = new List<LaptopRamModuleDataRecord>();

            string selectSQL = "SELECT * from LaptopRamModules WHERE laptop_id = @laptop_id";

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
                    throw;
                }

                try
                {
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            LaptopRamModuleDataRecord record = new LaptopRamModuleDataRecord();
                            record.LaptopID = laptopID;
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
                    throw;
                }

                return records;
            }
        }

        public void Insert(int laptopID, int ramID, int quantity)
        {
            string insertSQL = "INSERT INTO LaptopRamModules" +
                "(laptop_id, ram_id, quantity)" +
                " VALUES(@laptop_id, @ram_id, @quantity)";

            using (SqlCommand cmd = new SqlCommand(insertSQL, this.connection))
            {
                try
                {
                    //laptop_id
                    SqlParameter laptopIDParam = new SqlParameter("@laptop_id", laptopID);
                    cmd.Parameters.Add(laptopIDParam);

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

        public void Update(int laptopID, int ramID, int quantity)
        {
            string updateSQL = @"UPDATE LaptopRamModules
                                 SET laptop_id = @laptop_id,
                                     ram_id = @ram_id,
                                     quantity = @quantity
                                 WHERE laptop_id = @laptop_id";

            using (SqlCommand cmd = new SqlCommand(updateSQL, this.connection))
            {
                try
                {
                    //laptop_id
                    SqlParameter laptopIDParam = new SqlParameter("@laptop_id", laptopID);
                    cmd.Parameters.Add(laptopIDParam);

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

        public void Delete(int laptopID)
        {
            string deleteSQL = "DELETE from LaptopRamModules WHERE laptop_id = @laptop_id";

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

        /*
        public IList<LaptopRamModuleDataRecord> GetAll()
        {
            //this will hold the records
            DataTable laptopRamModulesTable = new DataTable();
            IList<LaptopRamModuleDataRecord> list = new List<LaptopRamModuleDataRecord>();

            //get all the records.
            string sql = "SELECT laptop_id FROM LaptopRamModules ORDER BY laptop_id";

            using (SqlCommand cmd = new SqlCommand(sql, this.connection))
            {
                SqlDataReader reader = cmd.ExecuteReader();
                laptopRamModulesTable.Load(reader);
                reader.Close();
            }

            foreach (DataRow row in laptopRamModulesTable.Rows)
            {
                IList<LaptopRamModuleDataRecord> laptopRamModuleRecord = Select((int)row[0]);
                list.Add(laptopRamModuleRecord);
            }

            return list;
        }
         * */
    }
}
