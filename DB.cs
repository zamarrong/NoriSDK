using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace NoriSDK
{
    public class DB
    {
        private string _connectionString { get; set; }
        private SqlConnection _sqlConnection { get; set; }

        public DB()
        {
            _connectionString = Nori.CrearContexto().Connection.ConnectionString;
        }

        public DataTable ExecuteQuery(string commandText)
        {
            var dataTable = new DataTable();
            var _sqlConnection = new SqlConnection(_connectionString);
            var cmd = new SqlCommand(commandText, _sqlConnection);
            var da = new SqlDataAdapter(cmd);

            try
            {
                _sqlConnection.Open();
                da.Fill(dataTable);
            }
            catch (Exception ex)
            {
                var errorText = string.Format("Error : Command={0} :: Error={1}", commandText, ex.Message);
                Global.Error = new Nori.Error(errorText, ex);
            }
            finally
            {
                da.Dispose();
                _sqlConnection.Dispose();
            }

            return dataTable;
        }

        public DataTable ExecuteParameterizedQuery(string commandText, Dictionary<string, string> parameters)
        {
            var dataTable = new DataTable();
            var _sqlConnection = new SqlConnection(_connectionString);
            var cmd = new SqlCommand(commandText, _sqlConnection);

            var da = new SqlDataAdapter(cmd);
            foreach (var entry in parameters)
            {
                cmd.Parameters.AddWithValue(entry.Key, entry.Value);
            }

            try
            {
                _sqlConnection.Open();
                da.Fill(dataTable);
            }
            catch (Exception ex)
            {
                var errorText = string.Format("Error : Command={0} :: Error={1}", commandText, ex.Message);
                Global.Error = new Nori.Error(errorText, ex);
            }
            finally
            {
                da.Dispose();
                _sqlConnection.Dispose();
            }

            return dataTable;
        }

        public DataTable ExecuteParameterizedQueryObjects(string commandText, Dictionary<string, object> parameters)
        {
            var dataTable = new DataTable();
            var _sqlConnection = new SqlConnection(_connectionString);
            var cmd = new SqlCommand(commandText, _sqlConnection);

            var da = new SqlDataAdapter(cmd);
            foreach (var entry in parameters)
            {
                cmd.Parameters.AddWithValue(entry.Key, entry.Value);
            }

            try
            {
                _sqlConnection.Open();
                da.Fill(dataTable);
            }
            catch (Exception ex)
            {
                var errorText = string.Format("Error : Command={0} :: Error={1}", commandText, ex.Message);
                Global.Error = new Nori.Error(errorText, ex);
            }
            finally
            {
                da.Dispose();
                _sqlConnection.Dispose();
            }

            return dataTable;
        }

        public int ExecuteNonQuery(string commandText)
        {
            var _sqlConnection = new SqlConnection(_connectionString);
            var rowsAffected = 0;

            try
            {
                var cmd = new SqlCommand(commandText, _sqlConnection);
                _sqlConnection.Open();
                rowsAffected = cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                var errorText = string.Format("Error : Command={0} :: Error={1}", commandText, ex.Message);
                Global.Error = new Nori.Error(errorText, ex);
            }
            finally
            {
                _sqlConnection.Dispose();
            }

            return rowsAffected;
        }

        public int ExecuteParameterizedNonQuery(string commandText, Dictionary<string, string> parameters)
        {
            var _sqlConnection = new SqlConnection(_connectionString);
            var rowsAffected = 0;
            var cmd = new SqlCommand(commandText, _sqlConnection);

            foreach (var entry in parameters)
            {
                cmd.Parameters.AddWithValue(entry.Key, entry.Value);
            }

            try
            {
                _sqlConnection.Open();
                rowsAffected = cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                var errorText = string.Format("Error : Command={0} :: Error={1}", commandText, ex.Message);
                Global.Error = new Nori.Error(errorText, ex);
            }
            finally
            {
                _sqlConnection.Dispose();
            }

            return rowsAffected;
        }

        public int ExecuteParameterizedNonQueryObjects(string commandText, Dictionary<string, object> parameters)
        {
            var _sqlConnection = new SqlConnection(_connectionString);
            var rowsAffected = 0;
            var cmd = new SqlCommand(commandText, _sqlConnection);

            foreach (var entry in parameters)
            {
                cmd.Parameters.AddWithValue(entry.Key, entry.Value);
            }

            try
            {
                _sqlConnection.Open();
                rowsAffected = cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                var errorText = string.Format("Error : Command={0} :: Error={1}", commandText, ex.Message);
                Global.Error = new Nori.Error(errorText, ex);
            }
            finally
            {
                _sqlConnection.Dispose();
            }

            return rowsAffected;
        }

        public int ExecuteStoredProcedure(string storedProcedure, Dictionary<string, object> parameters)
        {
            var _sqlConnection = new SqlConnection(_connectionString);
            var rowsAffected = 0;
            var cmd = new SqlCommand(storedProcedure, _sqlConnection);
            cmd.CommandType = CommandType.StoredProcedure;

            foreach (var entry in parameters)
            {
                cmd.Parameters.AddWithValue(entry.Key, entry.Value);
            }

            try
            {
                _sqlConnection.Open();
                rowsAffected = cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                var errorText = string.Format("Error : Command={0} :: Error={1}", storedProcedure, ex.Message);
                Global.Error = new Nori.Error(errorText, ex);
            }
            finally
            {
                _sqlConnection.Dispose();
            }

            return rowsAffected;
        }

        public int ExecuteParameterizedScalarObjectsWithID(string commandText, Dictionary<string, object> parameters)
        {
            var _sqlConnection = new SqlConnection(_connectionString);
            int rowsAffected = 0;
            commandText += "; SELECT SCOPE_IDENTITY();";
            var cmd = new SqlCommand(commandText, _sqlConnection);

            foreach (var entry in parameters)
            {
                cmd.Parameters.AddWithValue(entry.Key, entry.Value);
            }

            try
            {
                _sqlConnection.Open();
                rowsAffected = int.Parse(cmd.ExecuteScalar().ToString());
            }
            catch (Exception ex)
            {
                var errorText = string.Format("Error : Command={0} :: Error={1}", commandText, ex.Message);
                Global.Error = new Nori.Error(errorText, ex);
            }
            finally
            {
                _sqlConnection.Dispose();
            }

            return rowsAffected;
        }

        public int ExecuteScalar(string commandText)
        {
            var _sqlConnection = new SqlConnection(_connectionString);
            var rowsAffected = 0;

            try
            {
                var cmd = new SqlCommand(commandText, _sqlConnection);
                _sqlConnection.Open();
                rowsAffected = (int)cmd.ExecuteScalar();
            }
            catch (Exception ex)
            {
                var errorText = string.Format("Error : Command={0} :: Error={1}", commandText, ex.Message);
                Global.Error = new Nori.Error(errorText, ex);
            }
            finally
            {
                _sqlConnection.Dispose();
            }

            return rowsAffected;
        }

        public decimal ExecuteDecimal(string commandText)
        {
            var _sqlConnection = new SqlConnection(_connectionString);
            var rowsAffected = 0.0M;

            try
            {
                var cmd = new SqlCommand(commandText, _sqlConnection);
                _sqlConnection.Open();
                rowsAffected = (decimal)cmd.ExecuteScalar();
            }
            catch (Exception ex)
            {
                var errorText = string.Format("Error : Command={0} :: Error={1}", commandText, ex.Message);
                Global.Error = new Nori.Error(errorText, ex);
            }
            finally
            {
                _sqlConnection.Dispose();
            }

            return rowsAffected;
        }

        public string ExecuteScalarString(string commandText)
        {
            var _sqlConnection = new SqlConnection(_connectionString);
            string row = string.Empty;

            try
            {
                var cmd = new SqlCommand(commandText, _sqlConnection);
                _sqlConnection.Open();
                row = cmd.ExecuteScalar().ToString();
            }
            catch (Exception ex)
            {
                var errorText = string.Format("Error : Command={0} :: Error={1}", commandText, ex.Message);
                Global.Error = new Nori.Error(errorText, ex);
            }
            finally
            {
                _sqlConnection.Dispose();
            }

            return row.ToString();
        }

        public int ScopeIdentity(string table)
        {
            var _sqlConnection = new SqlConnection(_connectionString);
            var rowsAffected = 0;
            string commandText = "SELECT dbo.SCOPE_IDENDTITY() FROM " + table;

            try
            {
                var cmd = new SqlCommand(commandText, _sqlConnection);
                _sqlConnection.Open();
                rowsAffected = (int)cmd.ExecuteScalar();
            }
            catch (Exception ex)
            {
                var errorText = string.Format("Error : Command={0} :: Error={1}", commandText, ex.Message);
                Global.Error = new Nori.Error(errorText, ex);
            }
            finally
            {
                _sqlConnection.Dispose();
            }

            return rowsAffected;
        }

    }
}
