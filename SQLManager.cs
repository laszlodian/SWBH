using Npgsql;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
            using (NpgsqlConnection conn=new NpgsqlConnection(Properties.Settings.Default.DBConnectionString))
            {
                try
                {
                    conn.Open();
                   
                  
                  
                    using (NpgsqlCommand insertCommand = new NpgsqlCommand(string.Format("insert into swb_installed(install_date,install_pc_name,optionpackages_count,install_username) values('{0}','{1}',{2},'{3}')", "@date", Environment.MachineName,CommandControler.Instance.PackagesCount,Environment.UserName), conn))
                    {
                        object res = null;
                        insertCommand.Parameters.AddWithValue("@date", DateTime.Now);

                        res = insertCommand.ExecuteNonQuery();

                        if (res == null)
                        {
                            throw new Exception(string.Format("Exception during insert to swb_installs: {0}",insertCommand.CommandText));
                        }

                    }
                    int installs_pk_id = 0;
                    using (NpgsqlCommand queryMaxPkId = new NpgsqlCommand("select MAX(pk_id) from swb_installs;", conn))
                    {
                        object res = null;
                        res = queryMaxPkId.ExecuteScalar();

                        installs_pk_id = Convert.ToInt32(res);
                    }
                    foreach (KeyValuePair<string, string> item in CommandControler.Instance.PackagesInfo)
                    {
                        using (NpgsqlCommand insertCommand = new NpgsqlCommand(string.Format("insert into packages(fk_swb_installs_id,package_name,package_version) values({0},'{1}','{2}')",installs_pk_id, item.Key, item.Value), conn))
                        {
                            object res = null;
                            res = insertCommand.ExecuteNonQuery();

                            if (res == null)
                            {
                                throw new Exception(string.Format("Exception during inserting: {0}", insertCommand.CommandText));
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    conn.Close();
                    throw new Exception(string.Format("Exception at StoreInstall\r\nStackTrace: {0}", ex.StackTrace));
                } finally
                {
                    conn.Close();

                }

            }        }
    }
}
