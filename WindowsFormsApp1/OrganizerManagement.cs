using Oracle.DataAccess.Client;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApp1
{

    public partial class OrganizerManagement : Form
    {

        organizerRep cr;
        OracleDataAdapter adapter;
        OracleCommandBuilder builder;
        DataSet ds;

        public void clearData()
        {
            textBox1.Clear();
            textBox2.Clear();
            textBox3.Clear();
            textBox4.Clear();
            textBox5.Clear();
            comboBox1.SelectedIndex = -1;
        }
        public OrganizerManagement()
        {
            InitializeComponent();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            dataGridView1.Hide();
            button8.Hide();
            crystalReportViewer1.Hide();
            label2.Text = "Add Organizer";
            button7.Text = "Add";
            label1.Show();
            label3.Show();
            label4.Show();
            label5.Show();
            label6.Show();
            label6.Show();
            textBox1.Show();
            textBox2.Show();
            textBox3.Show();
            textBox4.Show();
            textBox5.Show();
            comboBox1.Hide();
            
            
        }


        private void button2_Click(object sender, EventArgs e)
        {
            dataGridView1.Hide();
            button8.Hide();
            crystalReportViewer1.Hide();
            label2.Text = "Delete Organizer";
            button7.Text = "Delete";
            label1.Show();
            label3.Hide();
            label4.Hide();
            label5.Hide();
            label6.Hide();
            textBox1.Hide();
            textBox2.Hide();
            textBox3.Hide();
            textBox4.Hide();
            textBox5.Hide();
            comboBox1.Show();


            string ordb = "Data source=orcl;User Id=scott;Password=tiger;";
            OracleConnection conn = new OracleConnection(ordb);
            conn.Open();
            OracleCommand c = new OracleCommand();

            c.Connection = conn;
            c.CommandText = "select organizer_id from organizer ";
            OracleDataReader dr = c.ExecuteReader();

            while (dr.Read())
            {

                comboBox1.Items.Add(dr[0]);

            }

        }

        private void button3_Click(object sender, EventArgs e)
        {
            dataGridView1.Show();
            button8.Hide();
            crystalReportViewer1.Hide();
            label2.Text = "View Organizers";
            button7.Text = "View";
            label1.Hide();
            label3.Hide();
            label4.Hide();
            label5.Hide();
            label6.Hide();
            textBox1.Hide();
            textBox2.Hide();
            textBox3.Hide();
            textBox4.Hide();
            textBox5.Hide();
            comboBox1.Hide();


        }

        private void button5_Click(object sender, EventArgs e)
        {
            dataGridView1.Hide();
            crystalReportViewer1.Show();
            button8.Show();
            label2.Text = "Generate Report";
            label1.Hide();
            label3.Hide();
            label4.Hide();
            label5.Hide();
            label6.Hide();
            textBox1.Hide();
            textBox2.Hide();
            textBox3.Hide();
            textBox4.Hide();
            textBox5.Hide();
            comboBox1.Hide();

        }

        private void button6_Click(object sender, EventArgs e)
        {
            this.Close();
            Form1 f1 = new Form1();
            f1.Show();
        }

        private void OrganizerManagement_Load(object sender, EventArgs e)
        {
            comboBox1.Hide();
            dataGridView1.Hide();
            crystalReportViewer1.Hide();
            button8.Hide();
            cr = new organizerRep();

        }

        private void button8_Click(object sender, EventArgs e)
        {
            crystalReportViewer1.ReportSource = cr;
        }

        private void crystalReportViewer1_Load(object sender, EventArgs e)
        {

        }

        private void button7_Click(object sender, EventArgs e)
        {
            if (button7.Text == "View")
            {
                string constr = "User Id=scott;Password=tiger; Data Source=orcl";

                string cmdstr = @"select ORGANIZER_id as Organizer_ID,company as Organizer_Company,
                              bio as Organizer_Bio,website as Organizer_Website,
                              rating as Organizer_raiting from ORGANIZER";

                adapter = new OracleDataAdapter(cmdstr, constr);
                ds = new DataSet();
                adapter.Fill(ds);
                dataGridView1.DataSource = ds.Tables[0];
                OracleCommandBuilder builder;
                builder = new OracleCommandBuilder(adapter);
                adapter.Update(ds.Tables[0]);
                button7.Text = "Update";
            }
            else if (button7.Text == "Add")
            {
                string ordb = "Data source=orcl;User Id=scott;Password=tiger;";
                OracleConnection conn = new OracleConnection(ordb);
                conn.Open();
                OracleCommand cmd = new OracleCommand();
                cmd.Connection = conn;

                cmd.CommandText = "insert into organizer values (:id, :company, :bio ,:web,:rate)";

                cmd.Parameters.Add("id", int.Parse(textBox5.Text));

                cmd.Parameters.Add("company", textBox1.Text);

                cmd.Parameters.Add("bio", textBox2.Text);

                cmd.Parameters.Add("web", textBox3.Text);

                cmd.Parameters.Add("rate", textBox4.Text);

                int r = cmd.ExecuteNonQuery();

                if (r != -1)
                {
                    MessageBox.Show("New Organizer Added Successfully");
                }

            }
            else if (button7.Text == "Update")
            {
                builder = new OracleCommandBuilder(adapter);
                adapter.Update(ds.Tables[0]);
                MessageBox.Show("Updated Successfully");
            }
            else if (button7.Text == "Delete")
            {

                string ordb = "Data source=orcl;User Id=scott;Password=tiger;";
                OracleConnection conn = new OracleConnection(ordb);
                conn.Open();
                OracleCommand cmd = new OracleCommand();
                cmd.Connection = conn;

                cmd.CommandText = @"delete from organizer  
                                where ORGANIZER_ID = :id";
                cmd.Parameters.Add("id", int.Parse(comboBox1.Text));


                int r = cmd.ExecuteNonQuery();

                if (r != -1)
                {
                    MessageBox.Show("Organizer Deleted Successfully");
                }
            }
            clearData();
        }
    }
}
