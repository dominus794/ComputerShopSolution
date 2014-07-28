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

namespace ComputerShop.DAL.SQL.TableDataGateways
{
	public abstract class SQLTableDataGatewayBase : IDisposable
	{
		#region Fields

        private bool disposed;
		protected string connectionString;
		protected SqlConnection connection;        

		#endregion

		#region Constructor

		public SQLTableDataGatewayBase(string connectionString)
		{
            this.disposed = false;
			this.connectionString = connectionString;
			this.connection = new SqlConnection(this.connectionString);
			OpenConnection();
		}

		#endregion

        #region Destructor

        ~SQLTableDataGatewayBase()
        {
            Dispose(false);            
        }

        private void Dispose(bool disposing)
        {
            if (!disposed)
            {
                if (disposing)
                {
                    CloseConnection();
                }
            }

            disposed = true;
        }

        #endregion

        #region Connection Management

        /// <summary>
		/// Opens an SQL Server connection.
		/// </summary>
		/// <param name="connectionString">Connection String to the database.</param>
		protected void OpenConnection()
		{
			//Try open a connection with provided connectionString.
			try
			{
				connection.Open();
			}
			catch (SqlException ex)
			{
				Debug.WriteLine(ex.Message);
				throw;
			}
			catch (ArgumentException ex)
			{
				Debug.WriteLine(ex.Message);
				throw;
			}
		}

		protected void CloseConnection()
		{            
			try
			{ 
                if (connection.State == ConnectionState.Open)
                    connection.Close();                    
			}
			catch (SqlException ex)
			{
				Debug.WriteLine(ex.Message);
				throw;
			}            
		}

		#endregion

		#region IDisposable Members

		public void Dispose()
		{
            Dispose(true);
            GC.SuppressFinalize(this);
		}

		#endregion
	}
}
