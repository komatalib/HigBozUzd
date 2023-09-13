using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HigBozUzd
{
    class DB
    {
        public void InitializeDatabase()
        {
            string script = File.ReadAllText(@"C:\Users\denis\Desktop\Uzduotis\HigBozUzd\phoneBookScript.sql");

            using (SqlConnection sConn = new SqlConnection(@"Data Source=DESKTOP-MRNEBE4\SQLSERVER;Initial Catalog=PhoneBook;Integrated Security=True"))
            {
                using (SqlCommand cmd = new SqlCommand(script, sConn))
                {
                    try
                    {
                        sConn.Open();
                        cmd.ExecuteNonQuery();
                        Console.WriteLine("Database created / modified successfully");
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine("An error occurred: " + ex.Message);
                    }
                }
            }
        }
    }
}
