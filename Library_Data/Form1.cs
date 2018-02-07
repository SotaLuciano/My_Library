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
using System.Runtime.InteropServices;

namespace Library_Data
{
    public partial class Form1 : Form
    {
        SqlConnection sqlConnection;

        public Form1()
        {
            InitializeComponent();
        }

        private async void refreshBooksToolStripMenuItem_Click(object sender, EventArgs e)
        {
            listBox1.Items.Clear();
            string connectionString = @"Data Source=DESKTOP-N0618Q8\SQLEXPRESS;Initial Catalog=Library;Integrated Security=True";

            sqlConnection = new SqlConnection(connectionString);

            await sqlConnection.OpenAsync();

            SqlDataReader sqlReader = null;

            SqlCommand command = new SqlCommand("SELECT * FROM [Book]", sqlConnection);

            string str = "";
            try
            {
                sqlReader = await command.ExecuteReaderAsync();
                while (await sqlReader.ReadAsync())
                {
                    str = "";
                    str += Convert.ToString(sqlReader["BID"]);
                    for (int i = 0; str.Length < 5; i++)
                    {
                        str += " ";
                    }
                    str += Convert.ToString(sqlReader["Name"]);

                    for (int i = 0; str.Length < 36; i++)
                    {
                        str += " ";
                    }
                    str += Convert.ToString(sqlReader["Year"]); 

                    for (int i = 0; str.Length < 44; i++)
                    {
                        str += " ";
                    }
                    str += Convert.ToString(sqlReader["Pages"]);

                    for (int i = 0; str.Length < 50; i++)
                    {
                        str += " ";
                    }
                    str += Convert.ToString(sqlReader["AuthID"]); ;
                    listBox1.Items.Add(str);
                    
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString(), ex.Source.ToString(), MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                if (sqlReader != null)
                    sqlReader.Close();
            }
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (sqlConnection != null && sqlConnection.State != ConnectionState.Closed)
                sqlConnection.Close();
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (sqlConnection != null && sqlConnection.State != ConnectionState.Closed)
                sqlConnection.Close();
            this.Close();
        }


        private async void refreshAuthorsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            listBox2.Items.Clear();
            
            string connectionString = @"Data Source=DESKTOP-N0618Q8\SQLEXPRESS;Initial Catalog=Library;Integrated Security=True";

            sqlConnection = new SqlConnection(connectionString);

            await sqlConnection.OpenAsync();

            SqlDataReader sqlReader = null;

            SqlCommand command = new SqlCommand("SELECT * FROM [Author]", sqlConnection);

            string str = "";

            try
            {
                sqlReader = await command.ExecuteReaderAsync();

                while (await sqlReader.ReadAsync())
                {
                    str = "";
                    str += Convert.ToString(sqlReader["AuthID"]);
                    for (int i = 0; str.Length < 3; i++)
                    {
                        str += " ";
                    }
                    str += (Convert.ToString(sqlReader["FirstName"]) + " " + Convert.ToString(sqlReader["LastName"]));

                    for (int i = 0; str.Length < 21; i++)
                    {
                        str += " ";
                    }
                    str += Convert.ToString(sqlReader["Born"]).Substring(0, 10);

                    for (int i = 0; str.Length < 32; i++)
                    {
                        str += " ";
                    }

                    if (sqlReader["Died"].ToString().Length == 0)
                        str += "ALIVE";
                    else
                        str += Convert.ToString(sqlReader["Died"]).Substring(0, 10);         
           
                    for (int i = 0; str.Length < 43; i++)
                    {
                        str += " ";
                    }

                    str += Convert.ToString(sqlReader["Language"]);

                    for (int i = 0; str.Length < 51; i++)
                    {
                        str += " ";
                    }
                    str += Convert.ToString(sqlReader["Nationality"]);

                    listBox2.Items.Add(str);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString(), ex.Source.ToString(), MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                if (sqlReader != null)
                    sqlReader.Close();
            }
        }

        private void findBookToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form2 finder = new Form2();
            finder.Show();
        }

        private async void Form1_Load(object sender, EventArgs e)
        {
            #region FirstLoadBooks
            string connectionString = @"Data Source=DESKTOP-N0618Q8\SQLEXPRESS;Initial Catalog=Library;Integrated Security=True";

            sqlConnection = new SqlConnection(connectionString);

            await sqlConnection.OpenAsync();

            SqlDataReader sqlReader = null;

            SqlCommand command = new SqlCommand("SELECT * FROM [Book]", sqlConnection);

            string str = "";
            try
            {
                sqlReader = await command.ExecuteReaderAsync();
                while (await sqlReader.ReadAsync())
                {
                    str = "";
                    str += Convert.ToString(sqlReader["BID"]);
                    for (int i = 0; str.Length < 5; i++)
                    {
                        str += " ";
                    }
                    str += Convert.ToString(sqlReader["Name"]);

                    for (int i = 0; str.Length < 36; i++)
                    {
                        str += " ";
                    }
                    str += Convert.ToString(sqlReader["Year"]);

                    for (int i = 0; str.Length < 44; i++)
                    {
                        str += " ";
                    }
                    str += Convert.ToString(sqlReader["Pages"]);

                    for (int i = 0; str.Length < 50; i++)
                    {
                        str += " ";
                    }
                    str += Convert.ToString(sqlReader["AuthID"]); ;
                    listBox1.Items.Add(str);

                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString(), ex.Source.ToString(), MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                if (sqlReader != null)
                    sqlReader.Close();
            }
            #endregion

            #region FirstLoadAuthors

            connectionString = @"Data Source=DESKTOP-N0618Q8\SQLEXPRESS;Initial Catalog=Library;Integrated Security=True";

            sqlConnection = new SqlConnection(connectionString);

            await sqlConnection.OpenAsync();

            sqlReader = null;

            command = new SqlCommand("SELECT * FROM [Author]", sqlConnection);

            str = "";

            try
            {
                sqlReader = await command.ExecuteReaderAsync();

                while (await sqlReader.ReadAsync())
                {
                    str = "";
                    str += Convert.ToString(sqlReader["AuthID"]);
                    for (int i = 0; str.Length < 3; i++)
                    {
                        str += " ";
                    }
                    str += (Convert.ToString(sqlReader["FirstName"]) + " " + Convert.ToString(sqlReader["LastName"]));

                    for (int i = 0; str.Length < 21; i++)
                    {
                        str += " ";
                    }
                    str += Convert.ToString(sqlReader["Born"]).Substring(0, 10);

                    for (int i = 0; str.Length < 32; i++)
                    {
                        str += " ";
                    }

                    if (sqlReader["Died"].ToString().Length == 0)
                        str += "ALIVE";
                    else
                        str += Convert.ToString(sqlReader["Died"]).Substring(0, 10);

                    for (int i = 0; str.Length < 43; i++)
                    {
                        str += " ";
                    }

                    str += Convert.ToString(sqlReader["Language"]);

                    for (int i = 0; str.Length < 51; i++)
                    {
                        str += " ";
                    }
                    str += Convert.ToString(sqlReader["Nationality"]);

                    listBox2.Items.Add(str);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString(), ex.Source.ToString(), MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                if (sqlReader != null)
                    sqlReader.Close();
            }
            #endregion

        }

        private void aBOUTToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show("My Library \nVersion 3.2.2351 Update 1 \n(c) My Corporation. \nAll rights reserved.", "About My Library", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
    }

}
