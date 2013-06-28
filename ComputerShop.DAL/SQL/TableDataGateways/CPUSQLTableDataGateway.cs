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
	public class CPUSQLTableDataGateway : SQLTableDataGatewayBase
	{
		#region Constructor
		public CPUSQLTableDataGateway(string connectionString)
			: base(connectionString)
		{
		}
		#endregion

		public CPUDataRecord Select(int cpuID)
		{
			CPUDataRecord record = new CPUDataRecord();

			using (SqlCommand cmd = new SqlCommand("GetCPUByID", this.connection))
			{
				cmd.CommandType = CommandType.StoredProcedure;

				try
				{
					//@cpu_id
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
					using (SqlDataReader reader = cmd.ExecuteReader())
					{
						while (reader.Read())
						{
							record.CpuID = cpuID;
							record.Model = (string)reader["model"];
							record.Price = (decimal)reader["price"];
                            //record.ClockSpeed = (float)reader["clock_speed"];
                            float clockspeed = 0.0f;
                            Single.TryParse(reader["clock_speed"].ToString(), out clockspeed);
                            record.ClockSpeed = clockspeed;
							record.NoOfCores = (string)reader["no_of_cores"];
							record.CpuFormFactor = (string)reader["cpu_form_factor"];
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

		public void Insert(int manufacturerID, string model, decimal price, float clockSpeed, string noOfCores, string cpuFormFactor)
		{
			string insertSQL = "INSERT INTO CPUs" +
				"(manufacturer_id, model, price, clock_speed, no_of_cores, cpu_form_factor)" +
				" VALUES(@manufacturer_id, @model, @price, @clock_speed, @no_of_cores, @cpu_form_factor)";

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

					//clock_speed
					SqlParameter clockSpeedParam = new SqlParameter("@clock_speed", clockSpeed);
					cmd.Parameters.Add(clockSpeedParam);

					//no_of_cores
					SqlParameter noOfCoresParam = new SqlParameter("@no_of_cores", noOfCores);
					noOfCoresParam.SqlDbType = SqlDbType.NChar;
					noOfCoresParam.Size = 10;
					cmd.Parameters.Add(noOfCoresParam);

					//form_factor
					SqlParameter cpuFormFactorParam = new SqlParameter("@cpu_form_factor", cpuFormFactor);
					cpuFormFactorParam.SqlDbType = SqlDbType.NChar;
					cpuFormFactorParam.Size = 10;
					cmd.Parameters.Add(cpuFormFactorParam);
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

		public void Update(int cpuID, int manufacturerID, string model, decimal price, float clockSpeed, string noOfCores, string cpuFormFactor)
		{
			string updateCPUSQL = @"UPDATE CPUs 
									SET manufacturer_id = @manufacturer_id, 
										model = @model,
										price = @price,
										clock_speed = @clock_speed,
										no_of_cores = @no_of_cores,
										cpu_form_factor = @cpu_form_factor
									WHERE cpu_id = @cpu_id";

			using (SqlCommand cmd = new SqlCommand(updateCPUSQL, this.connection))
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

					//clock_speed
					SqlParameter clockSpeedParam = new SqlParameter("@clock_speed", clockSpeed);
					cmd.Parameters.Add(clockSpeedParam);

					//no_of_cores
					SqlParameter noOfCoresParam = new SqlParameter("@no_of_cores", noOfCores);
					noOfCoresParam.SqlDbType = SqlDbType.NChar;
					noOfCoresParam.Size = 10;
					cmd.Parameters.Add(noOfCoresParam);

					//form_factor
					SqlParameter cpuFormFactorParam = new SqlParameter("@cpu_form_factor", cpuFormFactor);
					cpuFormFactorParam.SqlDbType = SqlDbType.NChar;
					cpuFormFactorParam.Size = 10;
					cmd.Parameters.Add(cpuFormFactorParam);

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

		public void Delete(int cpuID)
		{
			string deleteSQL = "DELETE from CPUs WHERE cpu_id = @cpu_id";

			using (SqlCommand cmd = new SqlCommand(deleteSQL, this.connection))
			{
				try
				{
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
					cmd.ExecuteNonQuery();
				}
				catch (SqlException ex)
				{
					Debug.WriteLine(ex.Message);
					throw ex;
				}
			}
		}

		public IList<CPUDataRecord> GetAll()
		{
			//this will hold the records.
			DataTable processorsTable = new DataTable();
			List<CPUDataRecord> list = new List<CPUDataRecord>();

			//get all the cpu_id's
			string sql = "SELECT cpu_id FROM CPUs ORDER BY cpu_id";

			using (SqlCommand cmd = new SqlCommand(sql, this.connection))
			{
				SqlDataReader reader = cmd.ExecuteReader();

				processorsTable.Load(reader);
				reader.Close();
			}

			foreach (DataRow row in processorsTable.Rows)
			{
				CPUDataRecord cpu = Select((int)row[0]);
				list.Add(cpu);
			}

			return list;
		}
	}
}
