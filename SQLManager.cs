using Npgsql;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SWB_OptionPackageInstaller
{
    public class SQLManager
    {
        public static SQLManager Instance = null;

        public SQLManager()
        {
            Instance = this;
        }

        public void StoreInstall()
        {
            using (NpgsqlConnection conn = new NpgsqlConnection(Properties.Settings.Default.DBConnectionString))
            {
                try
                {
                    conn.Open();

                    Trace.TraceInformation("Connected!");

                    #region Insert values to installation (main) table

                    using (NpgsqlCommand insertCommand = new NpgsqlCommand(string.Format("insert into installation(user_name,machine_name,install_date) values('{0}','{1}',{2})", Environment.UserName, Environment.MachineName, "@date"), conn))
                    {
                        object res = null;
                        insertCommand.Parameters.AddWithValue("@date", DateTime.Now);

                        res = insertCommand.ExecuteNonQuery();

                        if (res == null)
                        {
                            Trace.TraceError("Exception during insert to swb_installs: {0}", insertCommand.CommandText);
                            throw new Exception(string.Format("Exception during insert to swb_installs: {0}", insertCommand.CommandText));
                        }
                        else
                            Trace.TraceInformation("Inserting values to installation (main) table is successfull, result: {0}", res);
                    }

                    #endregion Insert values to installation (main) table

                    #region Querying the max pk_id from main table

                    int installs_pk_id = 0;
                    using (NpgsqlCommand queryMaxPkId = new NpgsqlCommand("select MAX(pk_id) from installation;", conn))
                    {
                        object res = null;
                        res = queryMaxPkId.ExecuteScalar();

                        if (res == null)
                        {
                            Trace.TraceError("Exception during query themax pk_id from main table: {0}", queryMaxPkId.CommandText);
                            throw new Exception(string.Format("Exception during query themax pk_id from main table: {0}", queryMaxPkId.CommandText));
                        }
                        else
                        {
                            Trace.TraceInformation("Query of MAX pk_id from installation (main) table is successfull, result: {0}", Convert.ToInt32(res));
                            installs_pk_id = Convert.ToInt32(res);
                        }
                    }

                    #endregion Querying the max pk_id from main table

                    #region Inserting to packages table

                    foreach (KeyValuePair<string, string> item in CommandControler.Instance.PackagesInfo)
                    {
                        using (NpgsqlCommand insertCommand = new NpgsqlCommand(string.Format("insert into packages(fk_installations_id,package_name,package_version) values({0},'{1}','{2}')", installs_pk_id, item.Key, item.Value), conn))
                        {
                            object res = null;
                            res = insertCommand.ExecuteNonQuery();

                            if (res == null)
                            {
                                Trace.TraceError("Exception during insert to packages: {0}", insertCommand.CommandText);
                                throw new Exception(string.Format("Exception during inserting to packages: {0}", insertCommand.CommandText));
                            }
                            else
                                Trace.TraceInformation("Inserting values to packages table is successfull, result: {0}", res);
                        }
                    }

                    #endregion Inserting to packages table

                    #region Inserting to sunriseworkbench table

                    using (NpgsqlCommand insertCommand = new NpgsqlCommand(string.Format("insert into sunriseworkbench(fk_installation_id,swb_version) values({0},'{1}')", installs_pk_id, CommandControler.Instance.SunriseWorkbenchVersion), conn))
                    {
                        object res = null;
                        res = insertCommand.ExecuteNonQuery();

                        if (res == null)
                        {
                            Trace.TraceError("Exception during insert to sunriseworkbench table: {0}", insertCommand.CommandText);
                            throw new Exception(string.Format("Exception during inserting to sunriseworkbench table: {0}", insertCommand.CommandText));
                        }
                        else
                            Trace.TraceInformation("Inserting values to sunriseworkbench table is successfull, result: {0}", res);
                    }

                    #endregion Inserting to sunriseworkbench table
                }
                catch (Exception ex)
                {
                    conn.Close();
                    Trace.TraceError("Exception during store install, exception message: {0};stacktrace: {1}", ex.Message, ex.StackTrace);
                    throw new Exception(string.Format("Exception at StoreInstall\r\nStackTrace: {0}", ex.StackTrace));
                }
                finally
                {
                    conn.Close();
                    Form1.Instance.UpdateStatus("Installation and storeage has been done successfully!");
                }
            }
        }
    }
}