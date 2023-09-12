using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace HigBozUzd
{
    public partial class PhoneBook : Form
    {

        private SqlConnection sConn = new SqlConnection(@"Data Source=DESKTOP-MRNEBE4\SQLSERVER;Initial Catalog=PhoneBook;Integrated Security=True");
                
        public string contactId { get; set; }
        public bool editBtn { get; set; }

        public PhoneBook()
        {

            InitializeComponent();
            LoadData();
        }

        //LOAD DB
        private void PhoneBook_Load(object sender, EventArgs e)
        {
            this.phoneBookDataTableAdapter.Fill(this.phoneBookDataSet.phoneBookData);

            if (dataGridView1.Rows.Count > 0)
            {
                dataGridView1.Rows[0].Selected = true;
                contactId = "1";
            }
            dataGridView1.Columns[0].Visible = false;

        }

        public void LoadData()
        {
            using (SqlCommand cmd = new SqlCommand("SelectPhoneBookData", sConn))
            {
                cmd.CommandType = CommandType.StoredProcedure;

                SqlDataAdapter dataAdap = new SqlDataAdapter(cmd);
                DataTable dataTbl = new DataTable();
                dataAdap.Fill(dataTbl);

                dataGridView1.DataSource = dataTbl;
            }
        }

        //MARK ROW, GET CONTACT ID
        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0 && e.ColumnIndex >= 0)
            {
                dataGridView1.Rows[e.RowIndex].Selected = true;

                var dataBoundItem = dataGridView1.Rows[e.RowIndex].DataBoundItem as DataRowView;
                if (dataBoundItem != null)
                {
                    var id = dataBoundItem["ID"];
                    contactId = id.ToString();
                }
            }
        }

        //BUTTONS ACTION
        private void EditButton_Click(object sender, EventArgs e)
        {
            using (AddDialogForm dialogForm = new AddDialogForm())
            {
                dialogForm.contactId = contactId;
                dialogForm.editBtn = true;
                dialogForm.CheckEditButton();

                if (dialogForm.ShowDialog(this) == DialogResult.OK)
                {
                    LoadData();
                }
            }
        }
                
        private void AddButton_Click(object sender, EventArgs e)
        {           
            using (AddDialogForm dialogForm = new AddDialogForm())
            {
                dialogForm.contactId = contactId;
                dialogForm.editBtn = false;
                dialogForm.CheckEditButton();

                if (dialogForm.ShowDialog(this) == DialogResult.OK)
                {
                    LoadData();
                }
            }
        }

        private void DeleteButton_Click(object sender, EventArgs e)
        {
            using (SqlCommand cmd = new SqlCommand("DeleteData", sConn))
            {
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add(new SqlParameter("@ContactID", contactId));

                try
                {
                    sConn.Open();
                    SqlDataReader reader = cmd.ExecuteReader();
                   
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
                finally
                {
                    sConn.Close();
                    LoadData();
                }
            }
        }


    }
}
