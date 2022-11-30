using System;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.Configuration;
using System.Data;

namespace ADOnetStudent
{
    public partial class Form2 : Form
    {
        SqlConnection con;
        SqlDataAdapter da;
        SqlCommandBuilder scb;
        DataSet ds;
        public Form2()
        {
            InitializeComponent();
            string str = ConfigurationManager.ConnectionStrings["defaultConnection"].ConnectionString;
            con = new SqlConnection(str);
        }

        public DataSet GetAllStud()
        {
            da = new SqlDataAdapter("select * from stud", con);
            da.MissingSchemaAction = MissingSchemaAction.AddWithKey;
            scb = new SqlCommandBuilder(da);
            ds = new DataSet();
            da.Fill(ds, "stud");
            return ds;

        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                ds = GetAllStud();
                DataRow row = ds.Tables["stud"].NewRow();
                row["name"] = txtName.Text;
                row["city"] = txtCity.Text;
                row["percentage"] = txtPercentage.Text;
                ds.Tables["stud"].Rows.Add(row);
                int result = da.Update(ds.Tables["stud"]);
                if (result == 1)
                {
                    MessageBox.Show("Record inserted");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            try
            {
                ds = GetAllStud();
                DataRow row = ds.Tables["stud"].Rows.Find(txtRollno.Text);
                if (row != null)
                {
                    row["name"] = txtName.Text;
                    row["city"] = txtCity.Text;
                    row["percentage"] = txtPercentage.Text;

                    int result = da.Update(ds.Tables["stud"]);
                    if (result == 1)
                    {
                        MessageBox.Show("Record updated");
                    }
                }
                else
                {
                    MessageBox.Show("Id not found to update");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            try
            {
                ds = GetAllStud();
                DataRow row = ds.Tables["stud"].Rows.Find(txtRollno.Text);
                if (row != null)
                {
                    row.Delete();
                    int result = da.Update(ds.Tables["stud"]);
                    if (result == 1)
                    {
                        MessageBox.Show("Record deleted");
                    }
                }
                else
                {
                    MessageBox.Show("Id not found to delete");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            try
            {
                ds = GetAllStud();
                DataRow row = ds.Tables["stud"].Rows.Find(txtRollno.Text);
                if (row != null)
                {
                    txtName.Text = row["name"].ToString();
                    txtCity.Text = row["city"].ToString();
                    txtPercentage.Text = row["percentage"].ToString();
                }
                else
                {
                    MessageBox.Show("Record not found");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnShowAll_Click(object sender, EventArgs e)
        {
            try
            {
                ds = GetAllStud();
                dataGridView1.DataSource = ds.Tables["stud"]; ;

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
