using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Text;

namespace WinDBToBackEndGenerator.Libs
{
   public class SqlObjectLibs
    {
        #region related to sql connection

        public bool IsConnected { get; set; }
        public string dbconnectionstring { get; set; }
        public List<SqlConnectionStringTemplates> ConnectionStrings
        {
            get
            {
                return new List<SqlConnectionStringTemplates>() {
            new SqlConnectionStringTemplates(){  name=@"Trusted Connection Default",connectionstring=@"Data Source=LAPTOP-2NJJ8N9I\SQLEXPRESS;initial catalog=FunnyTime2019;Integrated Security=True;MultipleActiveResultSets=True;"},
            new SqlConnectionStringTemplates(){  name=@"Trusted Connection",connectionstring="Data Source=Loal_sql_server;initial catalog=your_dbname;Integrated Security=True;MultipleActiveResultSets=True;"},
            new SqlConnectionStringTemplates(){  name="Standard Security",connectionstring="Data Source=server_name_ip;initial catalog=your_dbname;persist security info=True;user id=db_user_id;password=db_password;MultipleActiveResultSets=True;"}
            };
            }
        }

        internal bool GetConnected(string dbconn)
        {
            bool ret = false;
            try
            {
                using (SqlConnection conn = new SqlConnection(dbconn))
                {
                    conn.Open();
                    dbconnectionstring = dbconn;
                    ret = true;
                }
            }
            catch (Exception ex)
            {
            }
            IsConnected = ret;
            return ret;
        }

        internal void Disconnected()
        {
            IsConnected = false; dbconnectionstring = string.Empty;
        } 
        #endregion

        #region class model related 

        internal List<SqlObjects> GetTables()
        {
            List<SqlObjects> tables = new List<SqlObjects>();
            DataTable schema = new DataTable();// connection.GetSchema("Tables");
            if (IsConnected)
            {
                using (SqlConnection connection = new SqlConnection(dbconnectionstring))
                {
                    connection.Open();
                    using (SqlCommand cmd = new SqlCommand("SELECT * FROM sys.Tables where type_desc='USER_TABLE'", connection))
                    {
                        using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                        {
                            da.Fill(schema);
                        }
                    }
                    List<string> TableNames = new List<string>();
                    foreach (DataRow row in schema.Rows)
                    {
                        if (!row["name"].ToString().Contains("sys"))
                        {
                            tables.Add(new SqlObjects() { ObjectName = row["name"].ToString(), ObjectType = row["type_desc"].ToString() });
                        }
                    }
                }
            }
            return tables;
        }

        internal void DeleteFolder(string path)
        {
            if (Directory.Exists(path))
            {
                Directory.Delete(path,true);
            }
        }

        internal void CreateModelFile(string executablePath, string className, string script)
        {
            if (!Directory.Exists(executablePath))
            {
                Directory.CreateDirectory(executablePath);
            }
            string fileName = Path.Combine(executablePath, className + ".cs");
            if (File.Exists(fileName))
            {
                File.Delete(fileName);
            }
            System.IO.File.WriteAllText(fileName, script);
        }
      
        internal string CreateTableModel(string tableName, string namespacename = "mynamespace", string prefix = "mynamespace")
        {
            string script = string.Empty;
            DataTable schema = GetSQLTableSchema(tableName);
            script = GenerateTableModel(tableName, schema, namespacename, prefix);
            return script;
        }

        private DataTable GetSQLTableSchema(string tableName)
        {
            DataTable schema = new DataTable();// connection.GetSchema("Tables");
            if (IsConnected)
            {
                using (SqlConnection connection = new SqlConnection(dbconnectionstring))
                {
                    connection.Open();
                    using (SqlCommand cmd = new SqlCommand("SELECT * FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME = @tableName ORDER BY ORDINAL_POSITION", connection))
                    {
                        cmd.CommandType = CommandType.Text;
                        cmd.Parameters.AddWithValue("@tableName", tableName);
                        using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                        {
                            da.Fill(schema);
                        }
                    }
                }
            }

            return schema;
        }

        private string GenerateTableModel(string tableName, DataTable schema, string namespacename = "mynamespace", string prefix = "")
        {
            StringBuilder stringBuilder = new StringBuilder();
            //namespace WinDBToBackEndGenerator
            stringBuilder.AppendLine("using System;");
            stringBuilder.AppendLine("using System.Collections.Generic;");
            stringBuilder.AppendLine("namespace " + namespacename + ".Models {\n");
            stringBuilder.AppendLine("public class " + prefix + tableName + "{\n");
            foreach (DataRow item in schema.Rows)
            {
                stringBuilder.AppendLine("      public " + SqlToCSharpDBType(item["DATA_TYPE"].ToString()) + "  " + item["COLUMN_NAME"].ToString() + " { get; set; }");
            }
            stringBuilder.AppendLine("}\n");
            stringBuilder.AppendLine("}");

            return stringBuilder.ToString();
        }

        private string SqlToCSharpDBType(string sqldbtype)
        {

            string retValue = " object ";
            switch (sqldbtype)
            {
                case "bigint":
                    retValue = "int";
                    break;
                case "char":
                    retValue = "string";
                    break;
                case "varchar":
                    retValue = "string";
                    break;
                case "nvarchar":
                    retValue = "string";
                    break;
                case "int":
                    retValue = "int";
                    break;
                case "decimal":
                    retValue = "decimal";
                    break;
                case "datetime":
                    retValue = "DateTime";
                    break;
                case "date":
                    retValue = "DateTime";
                    break;
                case "bit":
                    retValue = "bool";
                    break;
                case "float":
                    retValue = "Double";
                    break;
                case "image":
                    retValue = "Byte[]";
                    break;
                default:
                    // code block
                    break;
            }
            return retValue;
        }
        #endregion
        #region SQL CRUD Scripts
       
        /// <summary>
        /// This function will create PROC:
        /// Insert , Update , Delete(int) , ListAll(pagenumber,size) , Detail(int)
        /// </summary>
        /// <param name="tableName"></param>
        /// <returns></returns>
        internal string CreateSQLCRUD(string tableName, string path, string scriptname,  bool appendfile)
        {

            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }
            string fileName = Path.Combine(path, "DBScript.sql");
            string script = string.Empty;
            string classNamealtered = tableName.Replace("_", "");
            classNamealtered = classNamealtered.Substring(0, 1).ToUpper() + "" + classNamealtered.Substring(1, classNamealtered.Length - 1);
          
            if (appendfile)
            {
                script = script + GenerateInsertSQL(tableName, classNamealtered);
                script = script + "GO\n";
                script = script + GenerateUpdateSQL(tableName, classNamealtered);
                script = script + "GO\n";
                script = script + GenerateDeleteSQL(tableName, classNamealtered);
                script = script + "GO\n";
                script = script + GenerateDetailSQL(tableName, classNamealtered);
                script = script + "GO\n";
                script = script + GenerateListAllSQL(tableName, classNamealtered);
                script = script + "GO\n";
                System.IO.File.AppendAllText(fileName, script);
               
            }
            else
            {
                script = GenerateInsertSQL(tableName, classNamealtered);
                fileName = Path.Combine(path, "Insert" + classNamealtered + ".sql");
                System.IO.File.WriteAllText(fileName, script);
                script =  GenerateUpdateSQL(tableName, classNamealtered);
                fileName = Path.Combine(path, "Update" + classNamealtered + ".sql");
                System.IO.File.WriteAllText(fileName, script);
                script =  GenerateDeleteSQL(tableName, classNamealtered);
                fileName = Path.Combine(path, "Delete" + classNamealtered + ".sql");
                System.IO.File.WriteAllText(fileName, script);
                script = GenerateDetailSQL(tableName, classNamealtered);
                fileName = Path.Combine(path, "Detail" + classNamealtered + ".sql");
                System.IO.File.WriteAllText(fileName, script);
                script = GenerateListAllSQL(tableName, classNamealtered);
                fileName = Path.Combine(path, "ListAll" + classNamealtered + ".sql");
                System.IO.File.WriteAllText(fileName, script);
            }
            return script;
        }

        private string GenerateListAllSQL(string tableName, string objectName)
        {
            DataTable schema = GetSQLTableSchema(tableName);
            StringBuilder stringBuilder = new StringBuilder();
            stringBuilder.AppendLine("CREATE PROCEDURE [dbo].[ListAll" + objectName + "]");
            stringBuilder.AppendLine("AS\n");
            stringBuilder.AppendLine("BEGIN\n");
            stringBuilder.AppendLine("SELECT * FROM [dbo].[" + objectName + "]");
            stringBuilder.AppendLine("END");
            return stringBuilder.ToString();
        }

        private string GenerateDetailSQL(string tableName, string objectName)
        {
            DataTable schema = GetSQLTableSchema(tableName);
            StringBuilder stringBuilder = new StringBuilder();
            stringBuilder.AppendLine("CREATE PROCEDURE [dbo].[Detail" + objectName + "]");
            //Add PROC parameters
            string columnParam = string.Empty;
            for (int i = 0; i < schema.Rows.Count; i++)
            {
                DataRow item = schema.Rows[i];
                if (CheckIdentity(tableName, item["COLUMN_NAME"].ToString()))
                {
                    if (item["IS_NULLABLE"].ToString() == "YES")
                    {
                        columnParam = columnParam + "@" + item["COLUMN_NAME"].ToString() + "  " + GetSqlDbTypeWithVariableLength(item) + "=" + GetSqlDetault(item);
                    }
                    else
                    {
                        columnParam = columnParam + "@" + item["COLUMN_NAME"].ToString() + "  " + GetSqlDbTypeWithVariableLength(item);
                    }
                }
            }

            stringBuilder.AppendLine(columnParam);

            stringBuilder.AppendLine("AS\n");
            stringBuilder.AppendLine("BEGIN\n");
            stringBuilder.AppendLine("SELECT * FROM [dbo].[" + objectName + "]");
            string columnwhere = string.Empty;

            for (int i = 0; i < schema.Rows.Count; i++)
            {
                DataRow item = schema.Rows[i];
                string endcomma = string.Empty;
                if (i != (schema.Rows.Count - 1))
                {
                    endcomma = ",\n";
                }
                if (CheckIdentity(tableName, item["COLUMN_NAME"].ToString()))
                {
                    columnwhere = " WHERE " + item["COLUMN_NAME"].ToString() + "=@" + item["COLUMN_NAME"].ToString();
                }
            }
            stringBuilder.AppendLine(columnwhere);
            stringBuilder.AppendLine("END");
            return stringBuilder.ToString();
        }

        private string GenerateDeleteSQL(string tableName, string objectName)
        {
            DataTable schema = GetSQLTableSchema(tableName);
            StringBuilder stringBuilder = new StringBuilder();
            stringBuilder.AppendLine("CREATE PROCEDURE [dbo].[Delete" + objectName + "]");
            //Add PROC parameters
            string columnParam = string.Empty;
            for (int i = 0; i < schema.Rows.Count; i++)
            {
                DataRow item = schema.Rows[i];
                if (CheckIdentity(tableName, item["COLUMN_NAME"].ToString()))
                {
                    if (item["IS_NULLABLE"].ToString() == "YES")
                    {
                        columnParam = columnParam + "@" + item["COLUMN_NAME"].ToString() + "  " + GetSqlDbTypeWithVariableLength(item) + "=" + GetSqlDetault(item) ;
                    }
                    else
                    {
                        columnParam = columnParam + "@" + item["COLUMN_NAME"].ToString() + "  " + GetSqlDbTypeWithVariableLength(item);
                    }
                }
            }

            stringBuilder.AppendLine(columnParam);

            stringBuilder.AppendLine("AS\n");
            stringBuilder.AppendLine("BEGIN\n");
            stringBuilder.AppendLine("DELETE FROM  [dbo].[" + objectName + "]");
            string columnwhere = string.Empty;

            for (int i = 0; i < schema.Rows.Count; i++)
            {
                DataRow item = schema.Rows[i];
                string endcomma = string.Empty;
                if (i != (schema.Rows.Count - 1))
                {
                    endcomma = ",\n";
                }
                if (CheckIdentity(tableName, item["COLUMN_NAME"].ToString()))
                {
                    columnwhere = " WHERE " + item["COLUMN_NAME"].ToString() + "=@" + item["COLUMN_NAME"].ToString();
                }
            }
            stringBuilder.AppendLine(columnwhere);
            stringBuilder.AppendLine("END");
            return stringBuilder.ToString();
        }

        private string GenerateUpdateSQL(string tableName, string objectName)
        {
            DataTable schema = GetSQLTableSchema(tableName);
            StringBuilder stringBuilder = new StringBuilder();
            stringBuilder.AppendLine("CREATE PROCEDURE [dbo].[Update" + objectName + "]");
            //Add PROC parameters
            string columnParam = string.Empty;
            for (int i = 0; i < schema.Rows.Count; i++)
            {
                string endcomma = string.Empty;
                if (i != (schema.Rows.Count - 1))
                {
                    endcomma = ",\n";
                }
                DataRow item = schema.Rows[i];
                 
                if (item["IS_NULLABLE"].ToString() == "YES")
                {
                    columnParam = columnParam + "@" + item["COLUMN_NAME"].ToString() + "  " + GetSqlDbTypeWithVariableLength(item) + "=" + GetSqlDetault(item) + endcomma;
                }
                else
                {
                    columnParam = columnParam + "@" + item["COLUMN_NAME"].ToString() + "  " + GetSqlDbTypeWithVariableLength(item) + endcomma;
                }
            }

            stringBuilder.AppendLine(columnParam);

            stringBuilder.AppendLine("AS\n");
            stringBuilder.AppendLine("BEGIN\n");
            stringBuilder.AppendLine("UPDATE [dbo].[" + objectName + "] \n SET ");
            string columnnames = string.Empty;
            string columnwhere = string.Empty;

            for (int i = 0; i < schema.Rows.Count; i++)
            {
                DataRow item = schema.Rows[i];
                string endcomma = string.Empty;
                if (i != (schema.Rows.Count - 1))
                {
                    endcomma = ",\n";
                }
                if (CheckIdentity(tableName, item["COLUMN_NAME"].ToString()))
                {
                    columnwhere =  " WHERE " + item["COLUMN_NAME"].ToString() + "=@" + item["COLUMN_NAME"].ToString();
                }
                else
                {
                    columnnames = columnnames + " " + item["COLUMN_NAME"].ToString() + "=@" + item["COLUMN_NAME"].ToString() + endcomma;
                }
            }
            stringBuilder.AppendLine(columnnames+"\n"+ columnwhere);
            stringBuilder.AppendLine("END");
            return stringBuilder.ToString();
        }

        private string GenerateInsertSQL(string tableName, string objectName)
        {
            DataTable schema = GetSQLTableSchema(tableName);
            StringBuilder stringBuilder = new StringBuilder();
            stringBuilder.AppendLine("CREATE PROCEDURE [dbo].[Insert" + objectName+"]");
            //Add PROC parameters
            string columnParam = string.Empty;
            for (int i = 0; i < schema.Rows.Count; i++)
            {
                string endcomma = string.Empty;
                if (i != (schema.Rows.Count - 1)) {
                    endcomma = ",\n";
                }
                DataRow item = schema.Rows[i];
                if (!CheckIdentity(tableName, item["COLUMN_NAME"].ToString()))
                {
                    //select columnproperty(object_id('Categories'),'Id','IsIdentity')
                    if (item["IS_NULLABLE"].ToString() == "YES")
                    {
                        columnParam = columnParam + "@" + item["COLUMN_NAME"].ToString() + "  " + GetSqlDbTypeWithVariableLength(item) + "=" + GetSqlDetault(item) + endcomma;
                        //stringBuilder.AppendLine("@" + item["COLUMN_NAME"].ToString() + "  " + item["DATA_TYPE"].ToString() + "=" + GetSqlDetault(item)+",");
                    }
                    else
                    {
                        columnParam = columnParam + "@" + item["COLUMN_NAME"].ToString() + "  " + GetSqlDbTypeWithVariableLength(item) + endcomma;
                        // stringBuilder.AppendLine("@" + item["COLUMN_NAME"].ToString() + "  " + item["DATA_TYPE"].ToString() + ",");
                    }
                }
            }
             
            stringBuilder.AppendLine(columnParam);

            stringBuilder.AppendLine("AS\n");
            stringBuilder.AppendLine("BEGIN\n");
            stringBuilder.AppendLine("INSERT INTO  [dbo].[" + objectName + "] (");
            string columnnames = string.Empty;
            for (int i = 0; i < schema.Rows.Count; i++)
            {
                DataRow item = schema.Rows[i];
                string endcomma = string.Empty;
                if (i != (schema.Rows.Count - 1))
                {
                    endcomma = ",\n";
                }
                if (!CheckIdentity(tableName, item["COLUMN_NAME"].ToString()))
                {
                    columnnames = columnnames + item["COLUMN_NAME"].ToString() + endcomma;
                }
            }
            
            stringBuilder.AppendLine(columnnames);
            stringBuilder.AppendLine(")");
            stringBuilder.AppendLine("VALUES(");
            columnnames = string.Empty;
            for (int i = 0; i < schema.Rows.Count; i++)
            {
                DataRow item = schema.Rows[i];
                string endcomma = string.Empty;
                if (i != (schema.Rows.Count - 1))
                {
                    endcomma = ",\n";
                }
                if (!CheckIdentity(tableName, item["COLUMN_NAME"].ToString()))
                {
                    columnnames = columnnames + "@" + item["COLUMN_NAME"].ToString() + endcomma;
                }
            }
            stringBuilder.AppendLine(columnnames);
            stringBuilder.AppendLine(")");
            stringBuilder.AppendLine("END");
            return stringBuilder.ToString();
        }

        private string GetSqlDbTypeWithVariableLength(DataRow item)
        {
            
            string datatype = item["DATA_TYPE"].ToString();
            string maxvalue = item["CHARACTER_MAXIMUM_LENGTH"].ToString();
            string numprecision = item["NUMERIC_PRECISION"].ToString();
            string numscale = item["NUMERIC_SCALE"].ToString();

            string defaultValue = "";
             
            if (datatype.ToLower() == "varchar" || datatype.ToLower() == "nvarchar" || datatype.ToLower() == "char")
            {
                if (maxvalue == "-1")
                {
                    defaultValue = " (max)";
                }
                else
                {
                    defaultValue = " (" + maxvalue + ")";
                }
            }
            if (datatype.ToLower() == "decimal" || datatype.ToLower() == "float" || datatype.ToLower() == "money")
            {
                defaultValue = " (" + numprecision + ","+ numscale +")";
            }
            return item["DATA_TYPE"].ToString() +"" +defaultValue;
        }

        private bool CheckIdentity(string tableName, string column)
        {
            bool isidentity = false;
            DataTable schema = new DataTable(); 
            if (IsConnected)
            {
                using (SqlConnection connection = new SqlConnection(dbconnectionstring))
                {
                    connection.Open();
                    using (SqlCommand cmd = new SqlCommand("select columnproperty(object_id(@tableName),@column,'IsIdentity')", connection))
                    {
                        cmd.CommandType = CommandType.Text;
                        cmd.Parameters.AddWithValue("@tableName", tableName);
                        cmd.Parameters.AddWithValue("@column", column);
                        using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                        {
                            da.Fill(schema);
                            if (schema.Rows.Count > 0)
                            {
                                if (schema.Rows[0][0].ToString()=="1") {
                                    isidentity = true;
                                }
                            }   
                        }
                    }
                }
            }


            return isidentity ;
        }

        private string GetSqlDetault(DataRow item)
        {
            string defaultValue = "null";
            string datatype = item["DATA_TYPE"].ToString();
            if (datatype == "bigint" || datatype == "int" || datatype == "decimal" || datatype == "money" || datatype == "bit" || datatype == "float") {
                defaultValue = "0";
            }
            return defaultValue;
        }



        #endregion


        /// <summary>
        ///  This function will save the file into a C# file in DAL Folder 
        ///  calling SQL Procs
        /// </summary>
        /// <param name="path"></param>
        /// <param name="scriptName"></param>
        /// <param name="script"></param>
        internal void CreateDALFile(string executablePath, string scriptName, string script)
        {
            if (!Directory.Exists(executablePath))
            {
                Directory.CreateDirectory(executablePath);
            }
            string fileName = Path.Combine(executablePath, scriptName + "Dal.cs");
            if (File.Exists(fileName))
            {
                File.Delete(fileName);
            }
            System.IO.File.WriteAllText(fileName, script);
        }
        /// <summary>
        /// This function will create Data access layer calling PROC below
        /// Insert , Update , Delete(int) , ListAll(pagenumber,size) , Detail(int) created in database
        /// </summary>
        /// <param name="tableName"></param>
        /// <returns></returns>
        internal string CreateTableDAL(string tableName, string namespacename = "mynamespace", string prefix = "")
        {
            string script = string.Empty;
            DataTable schema = GetSQLTableSchema(tableName);
            script = GenerateTableDAL(tableName, schema, namespacename, prefix);
            return script;
        }

        private string GenerateTableDAL(string tableName, DataTable schema, string namespacename = "mynamespace", string prefix = "")
        {
            string script = string.Empty;
            StringBuilder stringBuilder = new StringBuilder();
            
            stringBuilder.AppendLine("using System;");
            stringBuilder.AppendLine("using System.Collections.Generic;");
            stringBuilder.AppendLine("using System.Data.SqlClient;");
            stringBuilder.AppendLine("using System.Data;");
            stringBuilder.AppendLine("using " + namespacename + ".Models;");
            stringBuilder.AppendLine("namespace " + namespacename + ".DAL {\n");
            stringBuilder.AppendLine("public class " + prefix + tableName + "Dal {\n");
        
            script = GenerateInsertMethod(tableName, schema,prefix);
            stringBuilder.AppendLine(script+ "\n");

            script = GenerateUpdateMethod(tableName, schema, prefix);
            stringBuilder.AppendLine(script + "\n");

            script = GenerateDeleteMethod(tableName, schema, prefix);
            stringBuilder.AppendLine(script + "\n");

            script = GenerateDetailMethod(tableName, schema, prefix);
            stringBuilder.AppendLine(script + "\n");

            script = GenerateLoadAllMethod(tableName, schema, prefix);
            stringBuilder.AppendLine(script + "\n");

            stringBuilder.AppendLine("}\n");
            stringBuilder.AppendLine("}");

            return stringBuilder.ToString();
        }

        private string GenerateLoadAllMethod(string tableName, DataTable schema, string prefix="")
        {
            StringBuilder stringBuilder = new StringBuilder();
           
            stringBuilder.AppendLine("internal List<" + tableName + "> ListAll" + tableName + "() {");
            stringBuilder.AppendLine("List<" + tableName + ">" + " model =new List<" + tableName + ">();");
            stringBuilder.AppendLine("try{");
            stringBuilder.AppendLine("using (SqlConnection db = new SqlConnection(Util.GetConnection())){");
            stringBuilder.AppendLine("db.Open();");
            stringBuilder.AppendLine("using (SqlCommand cmd = new SqlCommand(\"ListAll" + tableName + "\", db)){");
            stringBuilder.AppendLine("cmd.CommandType = CommandType.StoredProcedure;");
            stringBuilder.AppendLine("using (SqlDataAdapter da=new SqlDataAdapter(cmd))");
            stringBuilder.AppendLine("{");
            stringBuilder.AppendLine("DataTable dt = new DataTable();");

            stringBuilder.AppendLine("da.Fill(dt);");

            // stringBuilder.AppendLine("List<" + tableName + ">" + " model =new List<" + tableName + ">();");
            stringBuilder.AppendLine("for (int j = 0; j < dt.Rows.Count; j++)");
            stringBuilder.AppendLine("{");

            stringBuilder.AppendLine(tableName + " item =new " + tableName + "();");
            for (int i = 0; i < schema.Rows.Count; i++)
            {
                DataRow item = schema.Rows[i];
                //if (!CheckIdentity(tableName, item["COLUMN_NAME"].ToString()))
                {
                    if (item["DATA_TYPE"].ToString() == "bigint" || item["DATA_TYPE"].ToString() == "int")
                    {
                        stringBuilder.AppendLine("item." + item["COLUMN_NAME"].ToString() + "=Util.ToInt32(dt.Rows[j][\"" + item["COLUMN_NAME"].ToString() + "\"]);");
                    }
                    if (item["DATA_TYPE"].ToString().Contains("char") || item["DATA_TYPE"].ToString().Contains("text"))
                    {
                        stringBuilder.AppendLine("item." + item["COLUMN_NAME"].ToString() + "=dt.Rows[j][\"" + item["COLUMN_NAME"].ToString() + "\"].ToString();");
                    }
                    if (item["DATA_TYPE"].ToString().Contains("decimal") || item["DATA_TYPE"].ToString().Contains("money"))
                    {
                        stringBuilder.AppendLine("item." + item["COLUMN_NAME"].ToString() + "=Util.ToDecimal(dt.Rows[j][\"" + item["COLUMN_NAME"].ToString() + "\"]);");
                    }
                    if (item["DATA_TYPE"].ToString().Contains("float"))
                    {
                        stringBuilder.AppendLine("item." + item["COLUMN_NAME"].ToString() + "=Util.ToDouble(dt.Rows[j][\"" + item["COLUMN_NAME"].ToString() + "\"]);");
                    }
                    if (item["DATA_TYPE"].ToString().Contains("bit"))
                    {
                        stringBuilder.AppendLine("item." + item["COLUMN_NAME"].ToString() + "=Util.ToBoolean(dt.Rows[j][\"" + item["COLUMN_NAME"].ToString() + "\"]);");
                    }
                    if (item["DATA_TYPE"].ToString().Contains("datetime"))
                    {
                        stringBuilder.AppendLine("item." + item["COLUMN_NAME"].ToString() + "=Util.ToDate(dt.Rows[j][\"" + item["COLUMN_NAME"].ToString() + "\"]);");
                    }
                }
            }
            stringBuilder.AppendLine("model.Add(item);");
            stringBuilder.AppendLine("}");
            stringBuilder.AppendLine("}");

            stringBuilder.AppendLine("}");
            stringBuilder.AppendLine("}");
            stringBuilder.AppendLine("}");
            stringBuilder.AppendLine("catch (Exception ex){\n");
            stringBuilder.AppendLine(" throw ex;");
            stringBuilder.AppendLine("}");
            stringBuilder.AppendLine("return model;");
            stringBuilder.AppendLine("}");
            return stringBuilder.ToString();
        }

        private string GenerateDetailMethod(string tableName, DataTable schema, string prefix="")
        {
            StringBuilder stringBuilder = new StringBuilder();
            bool isIdentityFound = false;
            for (int i = 0; i < schema.Rows.Count; i++)
            {
                DataRow item = schema.Rows[i];
                if (CheckIdentity(tableName, item["COLUMN_NAME"].ToString()))
                {
                    stringBuilder.AppendLine("internal "+ tableName +" Detail" + tableName + "(" + SqlToCSharpDBType(item["DATA_TYPE"].ToString()) + " " + item["COLUMN_NAME"].ToString() + ") {");
                    isIdentityFound = true;
                }
            }
            //if (!isIdentityFound)
            //{
            //    stringBuilder.AppendLine("internal " + tableName + " Detail" + tableName + "(" + SqlToCSharpDBType(item["DATA_TYPE"].ToString()) + " " + item["COLUMN_NAME"].ToString() + ") {");

            //}

            stringBuilder.AppendLine(tableName+" model =new "+ tableName + "();");
            stringBuilder.AppendLine("try{");
            stringBuilder.AppendLine("using (SqlConnection db = new SqlConnection(Util.GetConnection())){");
            stringBuilder.AppendLine("db.Open();");
            stringBuilder.AppendLine("using (SqlCommand cmd = new SqlCommand(\"Detail" + tableName + "\", db)){");
            stringBuilder.AppendLine("cmd.CommandType = CommandType.StoredProcedure;");
            //passing parameters to sql
            for (int i = 0; i < schema.Rows.Count; i++)
            {
                DataRow item = schema.Rows[i];
                if (CheckIdentity(tableName, item["COLUMN_NAME"].ToString()))
                {
                    stringBuilder.AppendLine("cmd.Parameters.AddWithValue(\"@" + item["COLUMN_NAME"].ToString() + "\"," + item["COLUMN_NAME"].ToString() + ");");
                }
            }
            

            stringBuilder.AppendLine("using (SqlDataAdapter da=new SqlDataAdapter(cmd))");
            stringBuilder.AppendLine("{");
            stringBuilder.AppendLine("DataTable dt = new DataTable();");

            stringBuilder.AppendLine("da.Fill(dt);");
            stringBuilder.AppendLine("if (dt.Rows.Count > 0)");
            stringBuilder.AppendLine("{");
            for (int i = 0; i < schema.Rows.Count; i++)
            {
                DataRow item = schema.Rows[i];
                //if (!CheckIdentity(tableName, item["COLUMN_NAME"].ToString()))
                {
                   if (item["DATA_TYPE"].ToString()== "bigint" || item["DATA_TYPE"].ToString() == "int")
                    {
                        stringBuilder.AppendLine("model." + item["COLUMN_NAME"].ToString() + "=Util.ToInt(dt.Rows[0][\"" + item["COLUMN_NAME"].ToString() + "\"]);");
                    }
                    if (item["DATA_TYPE"].ToString().Contains("char") || item["DATA_TYPE"].ToString().Contains("text"))
                    {
                        stringBuilder.AppendLine("model." + item["COLUMN_NAME"].ToString() + "=dt.Rows[0][\"" + item["COLUMN_NAME"].ToString() + "\"].ToString();");
                    }
                    if (item["DATA_TYPE"].ToString().Contains("decimal") || item["DATA_TYPE"].ToString().Contains("money"))
                    {
                        stringBuilder.AppendLine("model." + item["COLUMN_NAME"].ToString() + "=Util.ToDecimal(dt.Rows[0][\"" + item["COLUMN_NAME"].ToString() + "\"]);");
                    }
                    if (item["DATA_TYPE"].ToString().Contains("float"))
                    {
                        stringBuilder.AppendLine("model." + item["COLUMN_NAME"].ToString() + "=Util.ToDouble(dt.Rows[0][\"" + item["COLUMN_NAME"].ToString() + "\"]);");
                    }
                    if (item["DATA_TYPE"].ToString().Contains("datetime"))
                    {
                        stringBuilder.AppendLine("model." + item["COLUMN_NAME"].ToString() + "=Util.ToDateTime(dt.Rows[0][\"" + item["COLUMN_NAME"].ToString() + "\"]);");
                    }
                }
            }
           
            stringBuilder.AppendLine("}");
            stringBuilder.AppendLine("}");

            stringBuilder.AppendLine("}");
            stringBuilder.AppendLine("}");
            stringBuilder.AppendLine("}");
            stringBuilder.AppendLine("catch (Exception ex){\n");
            stringBuilder.AppendLine(" throw ex;");
            stringBuilder.AppendLine("}");
            stringBuilder.AppendLine("return model;");
            stringBuilder.AppendLine("}");
            return stringBuilder.ToString();
        }

        private string GenerateDeleteMethod(string tableName, DataTable schema, string prefix)
        {
            StringBuilder stringBuilder = new StringBuilder();
            bool isIdentityFound = false;
            for (int i = 0; i < schema.Rows.Count; i++)
            {
                DataRow item = schema.Rows[i];
                if (CheckIdentity(tableName, item["COLUMN_NAME"].ToString()))
                {
                    stringBuilder.AppendLine("internal void Delete" + tableName + "("+ SqlToCSharpDBType(item["DATA_TYPE"].ToString()) + " "+ item["COLUMN_NAME"].ToString() + ") {");
                    isIdentityFound = true;
                }
            }
            if (!isIdentityFound)
            {
                stringBuilder.AppendLine("internal void Delete" + tableName + "(" + prefix + tableName + " model) {");
            }
            stringBuilder.AppendLine("try{");
            stringBuilder.AppendLine("using (SqlConnection db = new SqlConnection(Util.GetConnection())){");
            stringBuilder.AppendLine("db.Open();");
            stringBuilder.AppendLine("using (SqlCommand cmd = new SqlCommand(\"Delete" + tableName + "\", db)){");
            stringBuilder.AppendLine("cmd.CommandType = CommandType.StoredProcedure;");

            for (int i = 0; i < schema.Rows.Count; i++)
            {
                DataRow item = schema.Rows[i];
                if (CheckIdentity(tableName, item["COLUMN_NAME"].ToString()))
                {
                    stringBuilder.AppendLine("cmd.Parameters.AddWithValue(\"@" + item["COLUMN_NAME"].ToString() + "\"," + item["COLUMN_NAME"].ToString() + ");");
                }
            }
            stringBuilder.AppendLine("cmd.ExecuteNonQuery();");
            stringBuilder.AppendLine("}");
            stringBuilder.AppendLine("}");
            stringBuilder.AppendLine("}");
            stringBuilder.AppendLine("catch (Exception ex){\n");
            stringBuilder.AppendLine(" throw ex;");
            stringBuilder.AppendLine("}");
            stringBuilder.AppendLine("}");
            return stringBuilder.ToString();
        }

        private string GenerateUpdateMethod(string tableName, DataTable schema, string prefix)
        {
            StringBuilder stringBuilder = new StringBuilder();
            stringBuilder.AppendLine("internal void Update" + tableName + "(" + prefix + tableName + " model) {");
            stringBuilder.AppendLine("try{");
            stringBuilder.AppendLine("using (SqlConnection db = new SqlConnection(Util.GetConnection())){");
            stringBuilder.AppendLine("db.Open();");
            stringBuilder.AppendLine("using (SqlCommand cmd = new SqlCommand(\"Update" + tableName + "\", db)){");
            stringBuilder.AppendLine("cmd.CommandType = CommandType.StoredProcedure;");

            for (int i = 0; i < schema.Rows.Count; i++)
            {
                DataRow item = schema.Rows[i];
                stringBuilder.AppendLine("cmd.Parameters.AddWithValue(\"@" + item["COLUMN_NAME"].ToString() + "\",model." + item["COLUMN_NAME"].ToString() + ");");
                
            }
            stringBuilder.AppendLine("cmd.ExecuteNonQuery();");
            stringBuilder.AppendLine("}");
            stringBuilder.AppendLine("}");
            stringBuilder.AppendLine("}");
            stringBuilder.AppendLine("catch (Exception ex){\n");
            stringBuilder.AppendLine(" throw ex;");
            stringBuilder.AppendLine("}");
            stringBuilder.AppendLine("}");
            return stringBuilder.ToString();
        }

        private string GenerateInsertMethod(string tableName, DataTable schema,string prefix="")
        {
            StringBuilder stringBuilder = new StringBuilder();
            stringBuilder.AppendLine("internal void Insert"+ tableName + "("+ prefix + tableName + " model) {");
            stringBuilder.AppendLine("try{");
            stringBuilder.AppendLine("using (SqlConnection db = new SqlConnection(Util.GetConnection())){");
            stringBuilder.AppendLine("db.Open();");
            stringBuilder.AppendLine("using (SqlCommand cmd = new SqlCommand(\"Insert"+ tableName + "\", db)){");
            stringBuilder.AppendLine("cmd.CommandType = CommandType.StoredProcedure;");
           
            for (int i = 0; i < schema.Rows.Count; i++)
            {
                DataRow item = schema.Rows[i];
                if (!CheckIdentity(tableName, item["COLUMN_NAME"].ToString()))
                {
                    stringBuilder.AppendLine("cmd.Parameters.AddWithValue(\"@"+item["COLUMN_NAME"].ToString()+"\",model."+ item["COLUMN_NAME"].ToString() + ");");
                }
            }
            stringBuilder.AppendLine("cmd.ExecuteNonQuery();");
            stringBuilder.AppendLine("}");
            stringBuilder.AppendLine("}");
            stringBuilder.AppendLine("}");
            stringBuilder.AppendLine("catch (Exception ex){\n");
            stringBuilder.AppendLine(" throw ex;");
            stringBuilder.AppendLine("}");
            stringBuilder.AppendLine("}");
            return stringBuilder.ToString();

        }
    }
    /// <summary>
    /// supporting class to list connections
    /// </summary>
    public class SqlConnectionStringTemplates { 
        public string name { get; set; }
        public string connectionstring { get; set; }

    }
    public class SqlObjects
    {
        public string ObjectName { get; set; }
        public string ObjectType { get; set; }

    }

}
