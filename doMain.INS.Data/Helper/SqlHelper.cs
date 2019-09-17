using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using pryUtil;

namespace doMain.INS.Data
{
    public class SqlHelper
    {

        #region Members

        private static string _connectionString;
        private static SqlConnection _connection;
        private SqlTransaction oTransaccion;

        #endregion


        #region Properties

        private static string ConnectionString
        {
            get { return ConfigurationManager.ConnectionStrings["ConexionDB"].ConnectionString; }
            set { _connectionString = value; }
        }

        private static SqlConnection Connection
        {
            get { return new SqlConnection(ConnectionString); }
            set { _connection = value; }
        }

        #endregion

        #region Methods

        public void ConnectionOpen()
        {
            ConnectionOpen(EstablecerConexion());
        }

        private void ConnectionOpen(SqlConnection oConnection)
        {
            try
            {
                if (oConnection.State == ConnectionState.Closed)
                    oConnection.Open();
            }
            catch (Exception ex)
            {
                throw new HandledException((int)enumGeneric.DataBaseError, clsConstantsGeneric.DataBaseError, ex.Message);
            }
        }

        public void ConnectionClose()
        {
            ConnectionClose(EstablecerConexion());
        }

        private void ConnectionClose(SqlConnection oConnection)
        {
            try
            {
                if (oConnection.State == ConnectionState.Open)
                    oConnection.Close();
            }
            catch (Exception ex)
            {
                throw new HandledException((int)enumGeneric.DataBaseError, clsConstantsGeneric.DataBaseError, ex.Message);
            }
        }

        private SqlConnection EstablecerConexion()
        {
            if (_connection == null)
                _connection = new SqlConnection(_connectionString);
            return _connection;
        }

        
        /// <summary>
        /// Ejecuta un procedimiento almacenado sin parametros. Devuelve un objeto SqlReader.
        /// </summary>
        /// <param name="storeProcedureName"></param>
        /// <param name="parameters"></param>
        /// <returns></returns>
        public static SqlDataReader GetDataReader(string storeProcedureName)
        {
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = Connection;
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = storeProcedureName;
            cmd.CommandTimeout = 300; // 3 minutos
            cmd.Connection.Open();

            SqlDataReader reader = cmd.ExecuteReader();

            return reader;
        }

        /// <summary>
        /// Ejecuta un procedimiento almacenado. Devuelve un objeto SqlReader.
        /// </summary>
        /// <param name="storeProcedureName"></param>
        /// <param name="parameters"></param>
        /// <returns></returns>
        ///        
        public static SqlDataReader GetDataReader(string storeProcedureName, Dictionary<string, object> parameters)
        {
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = Connection;
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = storeProcedureName;
            cmd.CommandTimeout = 360; // 6 minutos
            cmd.Connection.Open();

            if (parameters != null) foreach (var parameter in parameters) cmd.Parameters.AddWithValue(parameter.Key, parameter.Value);

            SqlDataReader reader = cmd.ExecuteReader();

            return reader;
        }

        /// <summary>
        /// Ejecuta un procedimiento almacenado. Devuelve un objeto DataTable.
        /// </summary>
        /// <param name="storeProcedureName"></param>
        /// <param name="searchParameters"></param>
        /// <returns></returns>
        public static DataTable GetDataTable(string storeProcedureName, Dictionary<string, object> parameters)
        {
            DataTable dt = new DataTable();
            dt.Load(GetDataReader(storeProcedureName, parameters));

            return dt;
        }

        /// <summary>
        /// Ejecuta un procedimiento almacenado. Devuelve el numero de filas afectadas.
        /// </summary>
        /// <param name="storeProcedureName"></param>
        /// <param name="parameters"></param>
        /// <returns></returns>
        /// 
        public static int ExecuteNonQuery(string storeProcedureName, Dictionary<string, object> parameters)
        {
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = Connection;
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = storeProcedureName;
            cmd.CommandTimeout = 300; // 5 minutos
            cmd.Connection.Open();

            if (parameters != null) foreach (var parameter in parameters) cmd.Parameters.AddWithValue(parameter.Key, parameter.Value);

            return cmd.ExecuteNonQuery();
        }

        /// <summary>
        /// Ejecuta un procedimiento almacenado como parte de una Transaccion. Devuelve el numero de filas afectadas.
        /// </summary>
        /// <param name="storeProcedureName"></param>
        /// <param name="parameters"></param>
        /// <returns></returns>
        /// 
        public static int ExecuteNonQuery(string storeProcedureName, Dictionary<string, object> parameters, SqlTransaction transaction)
        {
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = transaction.Connection;
            cmd.Transaction = transaction;
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = storeProcedureName;

            if (parameters != null) foreach (var parameter in parameters) cmd.Parameters.AddWithValue(parameter.Key, parameter.Value);

            return cmd.ExecuteNonQuery();
        }

        /// <summary>
        /// Ejecuta un procedimiento almacenado. Devuelve el valor de la primera columna de la primera fila.
        /// </summary>
        /// <param name="storeProcedureName"></param>
        /// <param name="parameters"></param>
        /// <returns></returns>
        public static object ExecuteScalar(string storeProcedureName, Dictionary<string, object> parameters)
        {
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = Connection;
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandTimeout = 300; // 5 minutos
            cmd.CommandText = storeProcedureName;
            cmd.Connection.Open();

            if (parameters != null) foreach (var parameter in parameters) cmd.Parameters.AddWithValue(parameter.Key, parameter.Value);

            return cmd.ExecuteScalar();
        }


        /// <summary>
        /// Ejecuta un procedimiento almacenado como parte de una Transaccion. Devuelve el valor de la primera columna de la primera fila.
        /// </summary>
        /// <param name="storeProcedureName"></param>
        /// <param name="parameters"></param>
        /// <returns></returns>

        public static object ExecuteScalar(string storeProcedureName, Dictionary<string, object> parameters, SqlTransaction transaction)
        {
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = transaction.Connection;
            cmd.Transaction = transaction;
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = storeProcedureName;

            if (parameters != null) foreach (var parameter in parameters) cmd.Parameters.AddWithValue(parameter.Key, parameter.Value);

            return cmd.ExecuteScalar();
        }


        /// <summary>
        /// Ejecuta un procedimiento almacenado. Devuelve un objeto (Dictionary).
        /// </summary>
        /// <param name="storeProcedureName"></param>
        /// <param name="parameters"></param>
        /// <returns></returns>
        public static Dictionary<string, string> GetDictionary(string storeProcedureName, Dictionary<string, object> parameters = null)
        {
            Dictionary<string, string> dictionary = null;

            SqlDataReader reader = GetDataReader(storeProcedureName, parameters);

            if (reader.HasRows)
            {
                dictionary = new Dictionary<string, string>();

                while (reader.Read())
                {
                    dictionary.Add(Convert.ToString(reader["Key"]), Convert.ToString(reader["Value"]));
                }
            }

            return dictionary;
        }

        /// <summary>
        /// Funcion devuelve un parametro OutPut.
        /// </summary>
        /// <param name="storeProcedureName"></param>
        /// <param name="parameters"></param>
        /// <returns></returns>
        public String OutputParameter(SqlCommand Command, string strName)
        {
            if (Command.Parameters[strName].Value == null)
                throw new HandledException((int)enumGeneric.DataBaseError, clsConstantsGeneric.DataBaseError, "Error al Traer el Parametro de Salida: " + strName);
            else if (Command.Parameters[strName].Value.ToString() == "")
                return "";
            else
                return Command.Parameters[strName].Value.ToString();
        }

        /// <summary>
        /// Funcion inicio de Transaccion (Commit).
        /// </summary>
        /// <param name="storeProcedureName"></param>
        /// <param name="parameters"></param>
        /// <returns></returns>
        public void TransactionStart()
        {
            try
            {
                oTransaccion = _connection.BeginTransaction();
            }
            catch (Exception ex)
            {
                throw new HandledException((int)enumGeneric.DataBaseError, clsConstantsGeneric.DataBaseError, ex.Message);

            }
        }

        /// <summary>
        /// Funcion confirmacion de inicio de Transaccion.
        /// </summary>
        /// <param name="storeProcedureName"></param>
        /// <param name="parameters"></param>
        /// <returns></returns>
        public void TransactionConfirm()
        {
            try
            {
                oTransaccion.Commit();
            }
            catch (Exception ex)
            {
                throw new HandledException((int)enumGeneric.DataBaseError, clsConstantsGeneric.DataBaseError, ex.Message);
            }
        }

        /// <summary>
        /// Funcion obtencion de peticion de inicio de Transaccion.
        /// </summary>
        /// <param name="storeProcedureName"></param>
        /// <param name="parameters"></param>
        /// <returns></returns>

        public SqlTransaction TransactionGet()
        {
            return oTransaccion;
        }


        /// <summary>
        /// Funcion cancelacion de Transaccion (Rollback).
        /// </summary>
        /// <param name="storeProcedureName"></param>
        /// <param name="parameters"></param>
        /// <returns></returns>
        public void TransactionCancel()
        {
            if (oTransaccion != null && oTransaccion.Connection != null)
                oTransaccion.Rollback();
        }

        public static SqlConnection GetConnection()
        {
            return Connection;
        }

    }

    #endregion

}
