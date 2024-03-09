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
    public partial class EventManagment : Form
    {
        EventRep er;
        OracleDataAdapter adapter;
        OracleCommandBuilder builder;
        DataSet ds;

        public void clearData()
        {
            textBox1.Clear();
            textBox2.Clear();
            textBox4.Clear();
            textBox5.Clear();
            textBox7.Clear();
            comboBox1.SelectedIndex = -1;
        }
        public EventManagment()
        {
            InitializeComponent();
        }

        private void button3_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void label7_Click(object sender, EventArgs e)
        {

        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void button4_Click(object sender, EventArgs e)
        {
            dateTimePicker1.Show();
            dataGridView1.Hide();
            label2.Text = "Add Event";
            textBox1.Show();
            textBox2.Show();
            textBox3.Hide();
            textBox4.Show();
            textBox5.Show();
            textBox7.Show();
            label1.Show();
            label3.Show();
            label4.Show();
            label5.Show();
            label6.Show();
            label7.Show();
            label8.Show();
            label9.Hide();
            comboBox1.Show();
            comboBox2.Hide();

            button7.Text = "Add";
            button8.Hide();
            crystalReportViewer1.Hide();
        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            dateTimePicker1.Hide();
            dataGridView1.Hide();
            textBox1.Hide();
            textBox2.Hide();
            textBox3.Hide();
            textBox4.Hide();
            textBox5.Hide();
            textBox7.Hide();
            label1.Show();
            label2.Text = "Delete Event";
            label3.Hide();
            label4.Hide();
            label5.Hide();
            label6.Hide();
            label7.Hide();
            label8.Hide();
            label9.Hide();
            comboBox1.Hide();
            comboBox2.Show();
            button7.Text = "Delete";
            button8.Hide();
            crystalReportViewer1.Hide();

        }

        private void button3_Click_1(object sender, EventArgs e)
        {
            dateTimePicker1.Hide();
            dataGridView1.Show();
            textBox1.Hide();
            textBox2.Hide();
            textBox3.Hide();
            textBox4.Hide();
            textBox5.Hide();
            textBox7.Hide();
            label2.Text = "View Events";
            label1.Hide();
            label3.Hide();
            label4.Hide();
            label5.Hide();
            label6.Hide();
            label7.Hide();
            label8.Hide();
            label9.Hide();
            comboBox1.Hide();
            comboBox2.Hide();

            button7.Text = "View";
            button8.Hide();
            crystalReportViewer1.Hide();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            dateTimePicker1.Hide();
            dataGridView1.Hide();
            label2.Text = "Generate Report";
            textBox1.Hide();
            textBox2.Hide();
            textBox3.Hide();
            textBox4.Hide();
            textBox5.Hide();
            textBox7.Hide();
            label1.Hide();
            label3.Hide();
            label4.Hide();
            label5.Hide();
            label6.Hide();
            label7.Hide();
            label8.Hide();
            label9.Hide();
            button7.Text = "Generate Report";
            button8.Show();
            comboBox1.Hide();
            comboBox2.Hide();
            crystalReportViewer1.Show();
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
                string cmdstr = @"select event_id as Event_ID,e_name as Event_Name,
                              e_date_time as Event_Date,e_location as Event_Location,
                              DESCRIPTION as Event_DESCRIPTION,
                              ATTENDEE_CAPACITY as Event_Capacity,
                              E_ORGANIZERID as Organiser_ID  from event";

                adapter = new OracleDataAdapter(cmdstr, constr);
                ds = new DataSet();
                adapter.Fill(ds);
                dataGridView1.DataSource = ds.Tables[0];
                

                button7.Text = "Update";
            }
            else if (button7.Text == "Get")
            {
                string ordb = "Data source=orcl;User Id=scott;Password=tiger;";
                OracleConnection conn = new OracleConnection(ordb);
                conn.Open();
                OracleCommand cmd = new OracleCommand();
                cmd.Connection = conn;
                cmd.CommandText = "GET_CAPCITY";

                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add("id", int.Parse(comboBox2.Text));
                cmd.Parameters.Add("cap", OracleDbType.Int32, ParameterDirection.Output);

                cmd.ExecuteNonQuery();

                textBox3.Text = (cmd.Parameters["cap"].Value.ToString());
            }
            else if(button7.Text == "Add")
            {
                string ordb = "Data source=orcl;User Id=scott;Password=tiger;";
                OracleConnection conn = new OracleConnection(ordb);
                conn.Open();
                OracleCommand cmd = new OracleCommand();

                cmd.Connection = conn;

                cmd.CommandText = "insert into Event values (:id, :name, :date_time ,:loc,:des,:cap,:oid)";


                cmd.Parameters.Add("id", int.Parse(textBox5.Text));

                cmd.Parameters.Add("name", textBox1.Text);

                cmd.Parameters.Add("date_time", dateTimePicker1.Value);

                cmd.Parameters.Add("loc", textBox2.Text);

                cmd.Parameters.Add("des", textBox7.Text);

                cmd.Parameters.Add("cap",int.Parse(textBox4.Text));

                cmd.Parameters.Add("oid", int.Parse(comboBox1.Text));

                int r = cmd.ExecuteNonQuery();

                if (r != -1)
                {
                    MessageBox.Show("New Event Added Successfully");
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

                cmd.CommandText = @"delete from event  
                                where event_id = :id";
                cmd.Parameters.Add("id", int.Parse(comboBox2.Text));


                int r = cmd.ExecuteNonQuery();

                if (r != -1)
                {
                    MessageBox.Show("Event Deleted Successfully");
                }

            }
            clearData();
        }

        private void EventManagment_Load(object sender, EventArgs e)
        {

            comboBox2.Hide();
            button8.Hide();
            dataGridView1.Hide();
            crystalReportViewer1.Hide();
            er = new EventRep();
            textBox3.Hide();
            label9.Hide();
            
            string ordb = "Data source=orcl;User Id=scott;Password=tiger;";
            OracleConnection conn = new OracleConnection(ordb);
            conn.Open();
            OracleCommand c = new OracleCommand();
            OracleCommand c2 = new OracleCommand();
            
            
            c.Connection = conn;
            c2.Connection = conn;
            c.CommandText = "select organizer_id from organizer ";
            c2.CommandText = "select event_id from event ";
            OracleDataReader dr = c.ExecuteReader();
            OracleDataReader dr2 = c2.ExecuteReader();

            while (dr.Read())
            {

                comboBox1.Items.Add(dr[0]);

            }
            while (dr2.Read())
            {

                comboBox2.Items.Add(dr2[0]);

            }
            dr2.Close();

            dr.Close();
        }

        private void button8_Click(object sender, EventArgs e)
        {
            crystalReportViewer1.ReportSource = er;
        }

        private void button9_Click(object sender, EventArgs e)
        {
            dateTimePicker1.Hide();
            button8.Hide();
            dataGridView1.Hide();
            crystalReportViewer1.Hide();
            label2.Text = "Get Capacity Of An Event";
            textBox1.Hide();
            textBox2.Hide();
            textBox3.Show();
            textBox4.Hide();
            textBox5.Hide();
            textBox7.Hide();
            label1.Show();
            label3.Hide();
            label4.Hide();
            label5.Hide();
            label6.Hide();
            label7.Hide();
            label8.Hide();
            label9.Show();
            comboBox1.Hide();
            comboBox2.Show();
            button7.Text = "Get";
            button8.Hide();
            

        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void label5_Click(object sender, EventArgs e)
        {

        }

        private void label9_Click(object sender, EventArgs e)
        {

        }

    }
}
