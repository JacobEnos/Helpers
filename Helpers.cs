using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Net;
using System.Reflection;
using System.Web.Script.Serialization;

//Git Test

namespace Personal
{
    public static class Helpers
    {
        public static class MySqlDBTools
        {
            public static MySqlCommand BuildCommand(string Proc) {
                //Build Stored Procedure Command
                MySqlCommand objCommand = new MySqlCommand();
                objCommand.CommandType = CommandType.StoredProcedure;
                objCommand.CommandText = Proc;

                return objCommand;
            }


            public static MySqlCommand BuildCommand(string Proc, string InputParam, object Input) {
                //Build Stored Procedure Command
                MySqlCommand objCommand = new MySqlCommand();
                objCommand.CommandType = CommandType.StoredProcedure;
                objCommand.CommandText = Proc;
                objCommand.Parameters.AddWithValue(InputParam, Input);

                return objCommand;
            }


            public static MySqlCommand BuildCommand(string Proc, string[] InputParams, object[] Inputs) {
                //Build Stored Procedure Command
                MySqlCommand objCommand = new MySqlCommand();
                objCommand.CommandType = CommandType.StoredProcedure;
                objCommand.CommandText = Proc;

                int i = 0;
                foreach (string param in InputParams) {

                    //Add coresponding Parameter and Value
                    objCommand.Parameters.AddWithValue(InputParams[i], Inputs[i]);

                    //Increment Param
                    i++;
                }

                return objCommand;
            }

            public static MySqlCommand BuildCommand(string Proc, List<string> InputParams, List<object> Inputs) {
                //Build Stored Procedure Command
                MySqlCommand objCommand = new MySqlCommand();
                objCommand.CommandType = CommandType.StoredProcedure;
                objCommand.CommandText = Proc;

                int i = 0;
                foreach (string param in InputParams) {

                    //Add coresponding Parameter and Value
                    objCommand.Parameters.AddWithValue(InputParams[i], Inputs[i]);

                    //Increment Param
                    i++;
                }

                return objCommand;
            }


            public static MySqlCommand BuildCommand(string Proc, Dictionary<string, object> Params) {
                //Build Stored Procedure Command
                MySqlCommand objCommand = new MySqlCommand();
                objCommand.CommandType = CommandType.StoredProcedure;
                objCommand.CommandText = Proc;

                foreach (KeyValuePair<string, object> param in Params) {

                    //Add coresponding Parameter and Value
                    objCommand.Parameters.AddWithValue(param.Key, param.Value);
                }

                return objCommand;
            }



            public static DataSet ExecuteFetch(MySqlCommand command) {

                //Open Connection
                MyDBConnect newConnection = new MyDBConnect();

                //Execute Command
                DataSet Data = newConnection.GetDataSetUsingCmdObj(command);

                //Close DB Connection
                newConnection.CloseConnection();

                return Data;
            }


            public static DataSet ExecuteFetch(string commandName) {

                //Open Connection
                MyDBConnect newConnection = new MyDBConnect();

                //Execute Command
                MySqlCommand newCommand = BuildCommand(commandName);
                DataSet Data = newConnection.GetDataSetUsingCmdObj(newCommand);

                //Close DB Connection
                newConnection.CloseConnection();

                return Data;
            }






            public static List<DataSet> ExecuteFetchs(List<MySqlCommand> commands) {

                //Open Connection
                MyDBConnect newConnection = new MyDBConnect();
                List<DataSet> results = new List<DataSet>();

                foreach(MySqlCommand c in commands) {
                    //Execute Command
                    results.Add(newConnection.GetDataSetUsingCmdObj(c));
                }

                //Close DB Connection
                newConnection.CloseConnection();

                return results;
            }

            


            public static void ExecuteUpdate(MySqlCommand command) {

                //Open Connection
                MyDBConnect newConnection = new MyDBConnect();

                //Execute Command
                newConnection.DoUpdateUsingCmdObj(command);

                //Close DB Connection
                newConnection.CloseConnection();
            }
        }











        public static class DBTools
        {
            public static SqlCommand BuildCommand(string Proc) {
                //Build Stored Procedure Command
                SqlCommand objCommand = new SqlCommand();
                objCommand.CommandType = CommandType.StoredProcedure;
                objCommand.CommandText = Proc;

                return objCommand;
            }


            public static SqlCommand BuildCommand(string Proc, string InputParam, object Input) {
                //Build Stored Procedure Command
                SqlCommand objCommand = new SqlCommand();
                objCommand.CommandType = CommandType.StoredProcedure;
                objCommand.CommandText = Proc;
                objCommand.Parameters.AddWithValue(InputParam, Input);

                return objCommand;
            }


            public static SqlCommand BuildCommand(string Proc, string[] InputParams, object[] Inputs) {
                //Build Stored Procedure Command
                SqlCommand objCommand = new SqlCommand();
                objCommand.CommandType = CommandType.StoredProcedure;
                objCommand.CommandText = Proc;

                int i = 0;
                foreach (string param in InputParams) {

                    //Add coresponding Parameter and Value
                    objCommand.Parameters.AddWithValue(InputParams[i], Inputs[i]);

                    //Increment Param
                    i++;
                }

                return objCommand;
            }

            public static SqlCommand BuildCommand(string Proc, List<string> InputParams, List<object> Inputs) {
                //Build Stored Procedure Command
                SqlCommand objCommand = new SqlCommand();
                objCommand.CommandType = CommandType.StoredProcedure;
                objCommand.CommandText = Proc;

                int i = 0;
                foreach (string param in InputParams) {

                    //Add coresponding Parameter and Value
                    objCommand.Parameters.AddWithValue(InputParams[i], Inputs[i]);

                    //Increment Param
                    i++;
                }

                return objCommand;
            }


            public static SqlCommand BuildCommand(string Proc, Dictionary<string, object> Params) {
                //Build Stored Procedure Command
                SqlCommand objCommand = new SqlCommand();
                objCommand.CommandType = CommandType.StoredProcedure;
                objCommand.CommandText = Proc;

                foreach (KeyValuePair<string, object> param in Params) {

                    //Add coresponding Parameter and Value
                    objCommand.Parameters.AddWithValue(param.Key, param.Value);
                }

                return objCommand;
            }



            public static DataSet ExecuteFetch(SqlCommand command) {

                //Open Connection
                DBConnect newConnection = new DBConnect();

                //Execute Command
                DataSet Data = newConnection.GetDataSetUsingCmdObj(command);

                //Close DB Connection
                newConnection.CloseConnection();

                return Data;
            }


            public static DataSet ExecuteFetch(string commandName) {

                //Open Connection
                DBConnect newConnection = new DBConnect();

                //Execute Command
                SqlCommand newCommand = BuildCommand(commandName);
                DataSet Data = newConnection.GetDataSetUsingCmdObj(newCommand);

                //Close DB Connection
                newConnection.CloseConnection();

                return Data;
            }



            public static void ExecuteUpdate(SqlCommand command) {

                //Open Connection
                DBConnect newConnection = new DBConnect();

                //Execute Command
                newConnection.DoUpdateUsingCmdObj(command);

                //Close DB Connection
                newConnection.CloseConnection();
            }
        }


        


        


        public static class APITools
        {

            public static List<object> CallAPI(string APIurl) {

                // Make call to DB
                WebRequest request = WebRequest.Create(APIurl);
                WebResponse response = request.GetResponse();

                // Read DB response
                Stream dataStream = response.GetResponseStream();
                StreamReader reader = new StreamReader(dataStream);
                String data = reader.ReadToEnd();
                reader.Close();
                response.Close();

                //Translate DB response
                JavaScriptSerializer jss = new JavaScriptSerializer();
                List<object> apiResponseList = jss.Deserialize<List<object>>(data);

                return apiResponseList;
            }



            public static T CallAPI<T>(string APIurl) {

                // Make call to DB
                WebRequest request = WebRequest.Create(APIurl);
                WebResponse response = request.GetResponse();

                // Read DB response
                Stream dataStream = response.GetResponseStream();
                StreamReader reader = new StreamReader(dataStream);
                String data = reader.ReadToEnd();
                reader.Close();
                response.Close();

                //Translate DB response
                JavaScriptSerializer jss = new JavaScriptSerializer();
                T apiResponseList = jss.Deserialize<T>(data);

                return apiResponseList;
            }
        }




        public static class TypeTools
        {

            public static Y NewType<Y>() where Y : new() {
                return new Y();
            }
        }





        public static class DataTools
        {

            public static bool IsValid(DataSet ds) {

                //if ds does not contain DataRows
                if (ds == null
                     || ds.Tables == null
                     || ds.Tables.Count < 1
                     || ds.Tables[0] == null
                     || ds.Tables[0].Rows == null
                     || ds.Tables[0].Rows.Count < 1) {

                    return false;
                }
                return true;
            }


            public static object SampleColumn(DataTable dt, string columnName) {

                if (dt.Rows != null && dt.Rows[0] != null && dt.Rows[0][columnName] != null) {
                    return dt.Rows[0].Field<string>(columnName);
                }
                
                return null;
            }



            public static DataRow ExtractRow(DataSet ds) {

                //if ds does not contain DataRows
                if (IsValid(ds)) {

                    return ds.Tables[0].Rows[0];
                }
                return null;
            }



            public static DataRow ExtractRowAt(DataSet ds, int index) {

                //if ds does not contain DataRows
                if (IsValid(ds)) {

                    try {
                        return ds.Tables[0].Rows[index];
                    }
                    catch (Exception ex) {
                    }
                }
                return null;
            }
            


            public static Type GetMemberType(object src, string memName) {
                try {

                    Type objectType = src.GetType();
                    MemberInfo[] member = objectType.GetMember(memName);
                    Type memberType = member.GetType();
                    return memberType;
                }
                catch (Exception ex) {
                    return null;
                }
            }



            public static object GetMemberValue(object src, string propName) {
                try {

                    Type memberType = src.GetType();
                    MemberInfo member = memberType.GetProperty(propName);

                    switch (member.MemberType) {
                        
                        //Read Field
                        case (MemberTypes.Field) :
                            return ((FieldInfo)member).GetValue(src);

                        //Read Prop
                        case (MemberTypes.Property):
                            return ((PropertyInfo)member).GetValue(src);

                        default:
                            break;
                    }
                }
                catch (Exception ex) {}
                return null;
            }



            public static object SetMemberValue(object src, string propName, object value) {
                try {

                    Type memberType = src.GetType();
                    MemberInfo member = memberType.GetMember(propName)[0];

                    switch (member.MemberType) {

                        //Read Field
                        case (MemberTypes.Field):
                            ((FieldInfo)member).SetValue(src, value);
                            break;

                        //Read Prop
                        case (MemberTypes.Property):
                            ((PropertyInfo)member).SetValue(src, value);
                            break;

                        default:
                            break;
                    }
                }
                catch (Exception ex) { }
                return null;
            }

            
                

            //Populates an existing object's properties with any mathching columns in a DataRow
            //Returns that object
            public static T SetExactMatches<T>(T toEdit, DataRow dr) {

                Type customType = typeof(T);

                //HashMap of customType Members
                Dictionary<string, MemberInfo> typeMemberDict = new Dictionary<string, MemberInfo>();

                //Add public Fields
                foreach (FieldInfo fi in customType.GetFields()) {
                    typeMemberDict.Add(fi.Name, fi);
                }
                //Add public Properties
                foreach (PropertyInfo pi in customType.GetProperties()) {
                    typeMemberDict.Add(pi.Name, pi);
                }


                //For each column in the row
                foreach (DataColumn column in dr.Table.Columns) {

                    try {
                        //Check HashMap
                        MemberInfo testMI = typeMemberDict[column.ColumnName];

                        //If Column matches Member
                        if (testMI != null) {

                            //Store this Member Type & Value
                            Type memberType = GetMemberType(toEdit, column.ColumnName);
                            object memberValue = GetMemberValue(toEdit, column.ColumnName);

                            //If Member Type has a generic new() function
                            if (memberType.GetMethod("new") != null) {

                                //Get GenericMethod
                                var NewTypeMethod = typeof(TypeTools).GetMethod("NewType");

                                //Build Method for this property type
                                var TypedMethod = NewTypeMethod.MakeGenericMethod(new[] { memberType });

                                //Invoke Method for this property type
                                var typeDefault = TypedMethod.Invoke(null, null);

                                //If the Member is null or default set
                                if (memberValue == null || memberValue == typeDefault) {

                                    //Set Property
                                    if (testMI.MemberType == MemberTypes.Property) {
                                        ((PropertyInfo)testMI).SetValue(toEdit, dr[column.ColumnName], null);
                                    }
                                    //Set Field
                                    else if (testMI.MemberType == MemberTypes.Field) {
                                        ((FieldInfo)testMI).SetValue(toEdit, dr[column.ColumnName]);
                                    }
                                }
                            }
                            //Else - the type does not contain a member for this column
                        }
                    }
                    catch (Exception ex) {
                        string s = ex.Message;
                    }
                }

                return toEdit;
            }

            


            //Contructs a Class Object and populates it from a DataRow
            //Returns that object
            //Accepts an optional 'translation' dictionary that will be used to match Column-names to Member-names
            //Currently only Fields and Properties qualify as 'Members' to this method
            //Dictionary Keys = MemberName, Values = ColumnName
            private static T RowToObject<T>(DataRow dr, Dictionary<string, string> translation = null, bool autoExactMatch = true) {

                Type customType = typeof(T);
                T obj = Activator.CreateInstance<T>();

                //HashMap of potential Members
                Dictionary<string, MemberInfo> typeMemberDict = new Dictionary<string, MemberInfo>();

                //Add public fields
                foreach (FieldInfo fi in customType.GetFields()) {
                    typeMemberDict.Add(fi.Name, fi);
                }
                //Add public Properties
                foreach (PropertyInfo pi in customType.GetProperties()) {
                    typeMemberDict.Add(pi.Name, pi);
                }


                if (translation != null) {

                    //For each translation
                    foreach (KeyValuePair<string, string> TranslationEntry in translation) {

                        try {
                            //Get member using Translation:Key
                            MemberInfo mi = typeMemberDict[TranslationEntry.Key];

                            //Set Member with DB Translation:Value column
                            if (mi != null) {
                                object dbValue = dr[TranslationEntry.Value];
                                SetMemberValue(obj, TranslationEntry.Key, dbValue);
                            }
                        }
                        catch (Exception ex) { }
                    }
                }


                //If wanted: match identical column/properties without including them in the translation
                if (autoExactMatch) SetExactMatches<T>(obj, dr);

                return obj;
            }





            //Calls GetItem() on every Row of a DataTable
            public static List<T> ConvertDataTable<T>(DataTable dt, Dictionary<string, string> translation = null, bool autoExactMatch = true) {

                //Construct a List to hold the constructed Objects
                List<T> data = new List<T>();

                //For each Row
                foreach (DataRow row in dt.Rows) {

                    //Pass settings along, parameters equally optional
                    T item = RowToObject<T>(row, translation, autoExactMatch);
                    data.Add(item);
                }

                return data;
            }







            //Calls ConvertDataTable on a DataSet
            //Returns a list of type T
            //The list contains T objects constructed from the DataRows
            //The Object's property names must exactly match DataBase Column names
            public static List<T> FromDataSet<T>(DataSet data, Dictionary<string, string> translation = null, bool autoMatchExact = true) {

                //If the DataSet has a populated Rows collection
                if (IsValid(data)) {

                    //Make a List of converted (DataRow -> T) Objects using ConvertDataTable()
                    List<T> myList = ConvertDataTable<T>(data.Tables[0], translation, autoMatchExact);

                    return myList;
                }

                return null;
            }




            //Calls ConvertDataTable on each DataTable
            //Builds a parallel List of T objects
            public static List<List<T>> TablesToList<T>(List<DataTable> toParseData, Dictionary<string, string> translation = null) {

                List<List<T>> typedReturn = new List<List<T>>();

                foreach (DataTable toParse in toParseData) {
                    typedReturn.Add(ConvertDataTable<T>(toParse, translation));
                }

                return typedReturn;
            }





            //Breaks DataSet into DataTables based on the groupByColumnName column
            //Builds Dictionary where Key=[shared groupByColumnName value], Value=[DataTable]
            public static Dictionary<object, DataTable> BreakByColumn(DataSet toSort, string groupByColumnName) {

                List<DataTable> result = new List<DataTable>();
                Dictionary<object, DataTable> toReturn = new Dictionary<object, DataTable>();

                //Sort single DataSet into DataTables using groupByColumnName
                if (DataTools.IsValid(toSort)) {

                    result = toSort.Tables[0].AsEnumerable()
                        .GroupBy(row => row.Field<String>(groupByColumnName))
                        .Select(g => g.CopyToDataTable())
                        .ToList();
                }

                //Name individual DataSets in Dictionary
                foreach (DataTable dt in result) {
                    toReturn.Add((string)DataTools.SampleColumn(dt, groupByColumnName), dt);
                }

                return toReturn;
            }



        }
    }
}