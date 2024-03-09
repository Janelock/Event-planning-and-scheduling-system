using Oracle.DataAccess.Client;
using Oracle.DataAccess.Types;
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
    public partial class AttendeeManagement : Form
    {
        attendeeRep ar;
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
        }
        public AttendeeManagement()
        {
            InitializeComponent();
        }

        

        private void button4_Click(object sender, EventArgs e)
        {
            dataGridView1.Hide();
            dataGridView2.Hide();
            crystalReportViewer1.Hide();
            button8.Hide();
            button7.Text = "Add";
            label2.Text = "Add Attendee";
            label1.Show();
            label3.Show();
            label4.Show();
            label5.Show();
            label6.Show();
            label7.Show();
            label8.Hide();
            textBox1.Show();
            textBox2.Show();
            textBox3.Show();
            textBox4.Show();
            textBox5.Show();
            dateTimePicker1.Show();
            comboBox1.Hide();
            comboBox2.Hide();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            dataGridView1.Hide();
            dataGridView2.Hide();
            crystalReportViewer1.Hide();
            button8.Hide();
            button7.Text = "Delete";
            label2.Text = "Delete Attendee";
            label1.Show();
            label3.Hide();
            label4.Hide();
            label5.Hide();
            label6.Hide();
            label7.Hide();
            label8.Hide();
            textBox1.Hide();
            textBox2.Hide();
            textBox3.Hide();
            textBox4.Hide();
            textBox5.Show();
            dateTimePicker1.Hide();
            comboBox1.Show();
            comboBox2.Hide();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            dataGridView1.Show();
            dataGridView2.Hide();
            crystalReportViewer1.Hide();
            button8.Hide();
            button7.Text = "View";
            label2.Text = "View Attendees";
            label1.Hide();
            label3.Hide();
            label4.Hide();
            label5.Hide();
            label6.Hide();
            label7.Hide();
            label8.Hide();
            textBox1.Hide();
            textBox2.Hide();
            textBox3.Hide();
            textBox4.Hide();
            textBox5.Hide();
            dateTimePicker1.Hide();
            comboBox1.Hide();
            comboBox2.Hide();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            dataGridView1.Hide();
            dataGridView2.Hide();
            label2.Text = "Generate Reports";
            button8.Show();
            crystalReportViewer1.Show();
            label1.Hide();
            label3.Hide();
            label4.Hide();
            label5.Hide();
            label6.Hide();
            label7.Hide();
            label8.Hide();
            textBox1.Hide();
            textBox2.Hide();
            textBox3.Hide();
            textBox4.Hide();
            textBox5.Hide();
            dateTimePicker1.Hide();
            comboBox1.Hide();
            comboBox2.Hide();

        }

        private void button6_Click(object sender, EventArgs e)
        {
            this.Close();
            Form1 f1 = new Form1();
            f1.Show();
        }

        private void button7_Click(object sender, EventArgs e)
        {
            if (button7.Text == "View")
            {
                string constr = "User Id=scott;Password=tiger; Data Source=orcl";

                string cmdstr = @"select attendee_id as Attendee_ID,address as Attendee_Address,
                              job_title as Attendee_JobTitle,phone_number as Attendee_PhoneNumber,
                              email as Attendee_Email from attendees";

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

                cmd.CommandText = "insert into ATTENDEES values (:id, :address, :job ,:phone,:mail, :dob)";

                cmd.Parameters.Add("id", int.Parse(textBox5.Text));

                cmd.Parameters.Add("address", textBox1.Text);

                cmd.Parameters.Add("job", textBox2.Text);

                cmd.Parameters.Add("phone", textBox3.Text);

                cmd.Parameters.Add("mail", textBox4.Text);

                cmd.Parameters.Add("dob", dateTimePicker1.Value);

                int r = cmd.ExecuteNonQuery();

                if (r != -1)
                {
                    MessageBox.Show("New Attendee Added Successfully");
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

                cmd.CommandText = @"delete from attendees  
                                where attendee_id = :id";
                cmd.Parameters.Add("id", int.Parse(comboBox1.Text));


                int r = cmd.ExecuteNonQuery();

                if (r != -1)
                {
                    MessageBox.Show("Attendee Deleted Successfully");
                }


            }
            else if (button7.Text == "View Event's Attendees")
            {
                dataGridView2.Rows.Clear();
                dataGridView2.Refresh();
                string ordb = "Data source=orcl;User Id=scott;Password=tiger;";
                OracleConnection conn = new OracleConnection(ordb);
                OracleCommand cmd = new OracleCommand();
                conn.Open();
                cmd.Connection = conn;
                cmd.CommandText = "GET_ATTENDEEID";

                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add("eid", int.Parse(comboBox2.Text));

                cmd.Parameters.Add("aid", OracleDbType.RefCursor, ParameterDirection.Output);

                OracleDataReader dr = cmd.ExecuteReader();

                dataGridView2.ReadOnly = true;
                while (dr.Read())

                {
                    dataGridView2.Rows.Add(dr[0], dr[1], dr[2], dr[3], dr[4], dr[5]);

                }

                dr.Close();
            }
            clearData();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void AttendeeManagement_Load(object sender, EventArgs e)
        {
            dataGridView1.Hide();
            dataGridView2.Hide();
            label8.Hide();
            button8.Hide();
            crystalReportViewer1.Hide();
            comboBox1.Hide();
            comboBox2.Hide();
            ar = new attendeeRep();

            string ordb = "Data source=orcl;User Id=scott;Password=tiger;";
            OracleConnection conn = new OracleConnection(ordb);
            conn.Open();
            OracleCommand c = new OracleCommand();


            c.Connection = conn;
            c.CommandText = "select attendee_id from attendees ";
            OracleDataReader dr = c.ExecuteReader();

            while (dr.Read())
            {

                comboBox1.Items.Add(dr[0]);

            }

            dr.Close();
        }

        private void button8_Click(object sender, EventArgs e)
        {
            crystalReportViewer1.ReportSource = ar;
        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void crystalReportViewer1_Load(object sender, EventArgs e)
        {

        }

        private void label8_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            dataGridView1.Hide();
            dataGridView2.Show();
            label2.Text = "View An Event's Attendees";
            button8.Hide();
            crystalReportViewer1.Hide();
            label1.Hide();
            label3.Hide();
            label4.Hide();
            label5.Hide();
            label6.Hide();
            label7.Hide();
            label8.Show();
            textBox1.Hide();
            textBox2.Hide();
            textBox3.Hide();
            textBox4.Hide();
            textBox5.Hide();
            dateTimePicker1.Hide();
            comboBox1.Hide();
            comboBox2.Show();
            button7.Text = "View Event's Attendees";

            string ordb = "Data source=orcl;User Id=scott;Password=tiger;";
            OracleConnection conn = new OracleConnection(ordb);
            conn.Open();
            OracleCommand c = new OracleCommand();


            c.Connection = conn;
            c.CommandText = "select event_id from Event ";
            OracleDataReader dr = c.ExecuteReader();

            while (dr.Read())
            {

                comboBox2.Items.Add(dr[0]);

            }

            dr.Close();
        }
    }
}
