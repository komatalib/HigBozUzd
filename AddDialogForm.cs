using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace HigBozUzd
{ 
    public partial class AddDialogForm : Form
    {

        private SqlConnection sConn = new SqlConnection(@"Data Source=DESKTOP-MRNEBE4\SQLSERVER;Initial Catalog=PhoneBook;Integrated Security=True");

        public string fullName { get; set; }
        public string phoneNumber { get; set; }
        public DateTime birthDate { get; set; }
        public string contactId { get; set; }
        public bool editBtn { get; set; }
        public AddDialogForm(string contactId, bool editBtn)
        {            
            this.editBtn = editBtn;
            this.contactId = contactId;

            InitializeComponent();
            
            if (editBtn)
            {
                LoadContactData(contactId);
                label1.Text = "Update Contact";
            }            
        }

        //LOAD DB
        public void LoadContactData(string contactID)
        {
            using (SqlCommand cmd = new SqlCommand("UpdateFormLoadData", sConn))
            {
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add(new SqlParameter("@ContactID", contactID));

                try
                {
                    sConn.Open();
                    SqlDataReader reader = cmd.ExecuteReader();

                    if (reader.Read())
                    {
                        nameInputTxt.Text = reader["Name Surname"].ToString();
                        phoneInputTxt.Text = reader["Phone Nr."].ToString();
                        dateTimePicker1.Text = reader["Birthday"].ToString();
                    }
                    reader.Close();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
                finally
                {
                    sConn.Close();
                }
            }
        }

        private void AddToDatabase(string vardasPavarde, string telefonas, DateTime gimimoMetai)
        {
            using (SqlCommand cmd = new SqlCommand("InsertPhoneBookData", sConn))
            {
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add(new SqlParameter("@vardas_pavarde", vardasPavarde));
                cmd.Parameters.Add(new SqlParameter("@telefonas", telefonas));
                cmd.Parameters.Add(new SqlParameter("@gimimo_metai", gimimoMetai));

                try
                {
                    sConn.Open();
                    cmd.ExecuteNonQuery();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
                finally
                {
                    sConn.Close();
                }
            }
        }

        private void UpdateContact(string vardasPavarde, string telefonas, DateTime gimimoMetai, string contactID)
        {
            using (SqlCommand cmd = new SqlCommand("SaveUpdateData", sConn))
            {
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add(new SqlParameter("@vardas_pavarde", vardasPavarde));
                cmd.Parameters.Add(new SqlParameter("@telefonas", telefonas));
                cmd.Parameters.Add(new SqlParameter("@gimimo_metai", gimimoMetai));
                cmd.Parameters.Add(new SqlParameter("@ContactID", contactID));

                try
                {
                    sConn.Open();
                    cmd.ExecuteNonQuery();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
                finally
                {
                    sConn.Close();
                }
            }
        }

        private void okBtn_Click_1(object sender, EventArgs e)
        {
            this.fullName = nameInputTxt.Text;
            this.phoneNumber = phoneInputTxt.Text;
            this.birthDate = dateTimePicker1.Value;
            this.contactId = contactId;

            if (editBtn)
            {
                UpdateContact(fullName, phoneNumber, birthDate, contactId);
            }
            else
            {
                AddToDatabase(fullName, phoneNumber, birthDate);
            }
                      
            this.DialogResult = DialogResult.OK;
            this.Close();            
        }
        
        private void cancelBtn_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }



}
