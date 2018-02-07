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

namespace Library_Data
{

    public partial class Form2 : Form
    {
        SqlConnection sqlConnection;
        public Form2()
        {
            InitializeComponent();
            WindowState = FormWindowState.Maximized;
        }

        public Form2(Form1 firstform)
        {
            InitializeComponent();
        }

        private async void button1_Click(object sender, EventArgs e)
        {
            listBox1.Items.Clear();
            string Name = textBox1.Text;
            string Author = textBox2.Text;
            string Year = textBox3.Text;
            bool Name_F = false;
            bool Author_F = false;

            if (Name.Length == 0 && Author.Length == 0)
            {
                MessageBox.Show("Wrong query!", "Search", MessageBoxButtons.OK, MessageBoxIcon.Error);
                this.Close();
            }

            string connectionString = @"Data Source=DESKTOP-N0618Q8\SQLEXPRESS;Initial Catalog=Library;Integrated Security=True";

            sqlConnection = new SqlConnection(connectionString);

            await sqlConnection.OpenAsync();

            SqlDataReader sqlReader = null;

            SqlCommand command = new SqlCommand("SELECT * FROM Author INNER JOIN Book ON Author.AuthID = Book.AuthID INNER JOIN StatusBar ON Book.BID = StatusBar.BID", sqlConnection);

            string str = "";

            try
            {
                sqlReader = await command.ExecuteReaderAsync();

                while (await sqlReader.ReadAsync())
                {
                    Name_F = false;
                    Author_F = false;
                    if (Name.Length != 0)
                    {
                        if (Convert.ToString(sqlReader["Name"]).ToLower().Contains(Name.ToLower()))
                            Name_F = true;
                        if (Name_F && Author.Length != 0 && !((Convert.ToString(sqlReader["FirstName"]) + " " + Convert.ToString(sqlReader["LastName"])).ToLower().Contains(Author.ToLower())))
                            continue;
                        if (Name_F && Year.Length != 0 && !(Convert.ToString(sqlReader["Year"]).Contains(Year)))
                            continue;
                    }
                    if(Author.Length != 0)
                    {
                        if ((Convert.ToString(sqlReader["FirstName"]) + " " + Convert.ToString(sqlReader["LastName"])).ToLower().Contains(Author.ToLower()))
                            Author_F = true;
                        if (Author_F && Name.Length != 0 && !(Convert.ToString(sqlReader["Name"]).ToLower().Contains(Name.ToLower())))
                            continue;
                        if (Author_F && Year.Length != 0 && !(Convert.ToString(sqlReader["Year"]).Contains(Year)))
                            continue;
                    }

                    if(Name_F || Author_F)
                    {
                        str = "";
                        str += Convert.ToString(sqlReader["BID"]);
                        for (int i = 0; str.Length < 3; i++)
                        {
                            str += " ";
                        }
                        str += (Convert.ToString(sqlReader["FirstName"]) + " " + Convert.ToString(sqlReader["LastName"]));

                        for (int i = 0; str.Length < 23; i++)
                        {
                            str += " ";
                        }
                        str += Convert.ToString(sqlReader["Name"]);

                        for (int i = 0; str.Length < 48; i++)
                        {
                            str += " ";
                        }
                        str += Convert.ToString(sqlReader["LID"]); ;

                        for (int i = 0; str.Length < 55; i++)
                        {
                            str += " ";
                        }
                        str += (Convert.ToString(sqlReader["Status"]) == "False") ? "Not available!" : "Are available!";
                        listBox1.Items.Add(str);
                    }
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
                if (listBox1.Items.Count == 0)
                    MessageBox.Show("Error #404 \nBook not found", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }    
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void Form2_Load(object sender, EventArgs e)
        {

        }
    }
}
