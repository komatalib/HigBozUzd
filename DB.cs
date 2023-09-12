using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HigBozUzd
{
    class DB
    {
        MySqlConnection connection = new MySqlConnection("Data Source=DESKTOP-MRNEBE4/SQLSERVER;Initial Catalog=PhoneBook;Integrated Security=True");
    }
}
