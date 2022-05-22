using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace libraryCrud
{
    /// <summary>
    /// Interaction logic for crud.xaml
    /// </summary>
    public partial class crud : Window
    {
        public crud()
        {
            InitializeComponent();
            string connString = ConfigurationManager.ConnectionStrings["connbooks"].ConnectionString;
            SqlConnection conn = new SqlConnection(connString);
            string Sql = "select FirstName from Authors";
            conn.Open();
            SqlCommand cmd = new SqlCommand(Sql, conn);
            SqlDataReader DR = cmd.ExecuteReader();

            while (DR.Read())
            {
                cb_author.Items.Add(DR[0]);
            }
            conn.Close();
            Sql = "select Name from Categories";
            conn.Open();
            SqlCommand cmd1 = new SqlCommand(Sql, conn);
            SqlDataReader DR1 = cmd1.ExecuteReader();

            while (DR1.Read())
            {
                cb_ctgry.Items.Add(DR1[0]);
            }
            conn.Close();
            Sql = "select Name from Press";
            conn.Open();
            SqlCommand cmd2 = new SqlCommand(Sql, conn);
            SqlDataReader DR2 = cmd2.ExecuteReader();

            while (DR2.Read())
            {
                cb_prss.Items.Add(DR2[0]);
            }
            conn.Close();
            Sql = "select Name from Themes";
            conn.Open();
            SqlCommand cmd3 = new SqlCommand(Sql, conn);
            SqlDataReader DR3 = cmd3.ExecuteReader();

            while (DR3.Read())
            {
                cb_thm.Items.Add(DR3[0]);
            }
            conn.Close();
        }

        private void btn_ok_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = true;
        }

        private void btn_cnl_Click(object sender, RoutedEventArgs e)
        {
            DialogResult= false;
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
           

        }

        private void tb_bid_TextChanged(object sender, TextChangedEventArgs e)
        {

            if (tb_bid.Text != null)
            {
                string connString = ConfigurationManager.ConnectionStrings["connbooks"].ConnectionString;
                SqlConnection conn = new SqlConnection(connString);
                conn.Close();
                string Sql = "select Count(*) c from Books Where Id=@p1";
                conn.Open();
                SqlCommand cmd1 = new SqlCommand(Sql, conn);
                cmd1.Parameters.AddWithValue("@p1",tb_bid.Text);
                SqlDataReader dr = cmd1.ExecuteReader();
                int t = 0 ;
                while (dr.Read())
                {
                    t = int.Parse($"{dr["c"]}");
                }
                if(t != 0)
                {
                    tb_bid.BorderBrush = new SolidColorBrush(Color.FromRgb(255, 0, 0));
                }
                else
                {
                    tb_bid.BorderBrush = new SolidColorBrush(Color.FromRgb(255, 255, 255));
                }
            }
            else
            {
                tb_bid.BorderBrush = new SolidColorBrush(Color.FromRgb(255, 0, 0));
            }
        }
    }
}
