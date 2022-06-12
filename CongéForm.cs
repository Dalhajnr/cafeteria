using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MySql.Data.MySqlClient;

namespace Gestion_Rh2.Forms
{
    public partial class CongéForm : Form
    {
        string n;
        string p;
        public CongéForm(string k,string j)
        {
            InitializeComponent();
            n = j;
            p = k;
            actul(n);
        }
        MySqlConnection con = new MySqlConnection("Server=Localhost;Database=dbo;Uid=root;Pwd=;");

        private void CongéForm_Load(object sender, EventArgs e)
        {

        }

        private void btnEnregistrer_Click(object sender, EventArgs e)
        {
            String dal = "En Attente";
            try
            {
                con.Open();
                string qur = "select * from congé where Matricule = '" + n + "'";
                MySqlCommand cm = new MySqlCommand(qur, con);
                MySqlDataReader dr = cm.ExecuteReader();

                if (dr.Read())
                {
                    if ("" + dr["Statut"] == "En Attente")
                    {
                        MessageBox.Show("En Attente");
                        
                    }
                    else
                    {
                        con.Close();
                        con.Open();
                        string query = "insert into Congé values('" + p + "','" + txtnom.Text + "','" + date.Value.Date + "','" + date.Value.Date + "','" + cb_type.Text + "','" + dal + "')";
                        MySqlCommand cmd = new MySqlCommand(query, con);
                        if (cmd.ExecuteNonQuery() == 1)
                        {
                            MessageBox.Show("Demande Envoyé");
                        }
                        else
                        {
                            MessageBox.Show("error ");
                        }
                    }

                }
                
                




            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                con.Close();
            }
            con.Close();
            actul(n);

        }

        public void actul(string n)
        {
            try
            {

                con.Open();
                string query = "select * from congé where Matricule = '" + n + "'";
                MySqlDataAdapter sda = new MySqlDataAdapter(query, con);
                MySqlCommandBuilder builder = new MySqlCommandBuilder(sda);
                var ds = new DataSet();
                sda.Fill(ds);
                empdata.DataSource = ds.Tables[0];
                con.Close();

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);

            }



        }

        private void btnModifier_Click(object sender, EventArgs e)
        {
            
        }
    }
}
