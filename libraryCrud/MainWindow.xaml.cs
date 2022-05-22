using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace libraryCrud
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            string connString = ConfigurationManager.ConnectionStrings["connbooks"].ConnectionString;
            string Sql = "select FirstName from Authors";
            SqlConnection conn = new SqlConnection(connString);
            conn.Open();
            SqlCommand cmd = new SqlCommand(Sql, conn);
            SqlDataReader DR = cmd.ExecuteReader();

            while (DR.Read())
            {
                cb_auth.Items.Add(DR[0]);

            }



            string Sql1 = "select Name from Categories";
            SqlConnection conn1 = new SqlConnection(connString);
            conn1.Open();
            SqlCommand cmd1 = new SqlCommand(Sql1, conn1);
            SqlDataReader DR1 = cmd1.ExecuteReader();

            while (DR1.Read())
            {
                cb_catg.Items.Add(DR1[0]);
            }

            string ConString = ConfigurationManager.ConnectionStrings["connbooks"].ConnectionString;
            string CmdString = string.Empty;
            using (SqlConnection con = new SqlConnection(ConString))
            {
                CmdString = "SELECT B.Id [Book ID],B.Name [Book Name],B.Pages [Page count],B.YearPress [Year Press],C.Name Category, (A.FirstName+' '+A.LastName) [Author],T.Name [Theme],P.Name [Press],B.Comment [Comment],B.Quantity [Quantity]" +
                   "FROM Books B INNER JOIN Authors A ON B.Id_Author = A.Id INNER JOIN Categories C ON B.Id_Category = C.Id INNER JOIN Themes T ON B.Id_Themes = T.Id INNER JOIN Press P ON B.Id_Press = P.Id";
                SqlCommand command = new SqlCommand(CmdString, con);
                SqlDataAdapter sda = new SqlDataAdapter(command);
                DataTable dt = new DataTable("Books");
                sda.Fill(dt);
                dg_books.ItemsSource = dt.DefaultView;
            }
        }

        private void cb_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if(cb_auth.SelectedItem != null && cb_catg.SelectedItem == null)
            {                
                string ConString = ConfigurationManager.ConnectionStrings["connbooks"].ConnectionString;
                string CmdString = string.Empty;
                
                using (SqlConnection con = new SqlConnection(ConString))
                {
                    CmdString = "SELECT B.Id [Book ID],B.Name [Book Name],B.Pages [Page count],B.YearPress [Year Press],C.Name Category, (A.FirstName+' '+A.LastName) [Author],T.Name [Theme],P.Name [Press],B.Comment [Comment],B.Quantity [Quantity]" +
                   "FROM Books B INNER JOIN Authors A ON B.Id_Author = A.Id INNER JOIN Categories C ON B.Id_Category = C.Id INNER JOIN Themes T ON B.Id_Themes = T.Id INNER JOIN Press P ON B.Id_Press = P.Id"+ " WHERE A.FirstName=@p1";
                    SqlCommand cmd = new SqlCommand(CmdString, con);
                    cmd.Parameters.AddWithValue("@p1", cb_auth.SelectedItem.ToString());
                    SqlDataAdapter sda = new SqlDataAdapter(cmd);
                    DataTable dt = new DataTable("Books");
                    sda.Fill(dt);
                    dg_books.ItemsSource = dt.DefaultView;
                    con.Close();
                }
            }
            else if(cb_auth.SelectedItem != null && cb_catg.SelectedItem != null)
            {
                string ConString = ConfigurationManager.ConnectionStrings["connbooks"].ConnectionString;
                string CmdString = string.Empty;

                using (SqlConnection con = new SqlConnection(ConString))
                {
                    CmdString = "SELECT B.Id [Book ID],B.Name [Book Name],B.Pages [Page count],B.YearPress [Year Press],C.Name Category, (A.FirstName+' '+A.LastName) [Author],T.Name [Theme],P.Name [Press],B.Comment [Comment],B.Quantity [Quantity]" +
                   "FROM Books B INNER JOIN Authors A ON B.Id_Author = A.Id INNER JOIN Categories C ON B.Id_Category = C.Id INNER JOIN Themes T ON B.Id_Themes = T.Id INNER JOIN Press P ON B.Id_Press = P.Id" + " WHERE A.FirstName=@p1 AND C.Name=@p2";
                    SqlCommand cmd = new SqlCommand(CmdString, con);
                    cmd.Parameters.AddWithValue("@p1", cb_auth.SelectedItem.ToString());
                    cmd.Parameters.AddWithValue("@p2", cb_catg.SelectedItem.ToString());
                    SqlDataAdapter sda = new SqlDataAdapter(cmd);
                    DataTable dt = new DataTable("Books");
                    sda.Fill(dt);
                    dg_books.ItemsSource = dt.DefaultView;
                    con.Close();
                }
            }
            else if (cb_auth.SelectedItem == null && cb_catg.SelectedItem != null)
            {
                string ConString = ConfigurationManager.ConnectionStrings["connbooks"].ConnectionString;
                string CmdString = string.Empty;

                using (SqlConnection con = new SqlConnection(ConString))
                {
                    CmdString = "SELECT B.Id [Book ID],B.Name [Book Name],B.Pages [Page count],B.YearPress [Year Press],C.Name Category, (A.FirstName+' '+A.LastName) [Author],T.Name [Theme],P.Name [Press],B.Comment [Comment],B.Quantity [Quantity]" +
                   "FROM Books B INNER JOIN Authors A ON B.Id_Author = A.Id INNER JOIN Categories C ON B.Id_Category = C.Id INNER JOIN Themes T ON B.Id_Themes = T.Id INNER JOIN Press P ON B.Id_Press = P.Id" + " WHERE C.Name=@p1";
                    SqlCommand cmd = new SqlCommand(CmdString, con);
                    cmd.Parameters.AddWithValue("@p1", cb_catg.SelectedItem.ToString());                    
                    SqlDataAdapter sda = new SqlDataAdapter(cmd);
                    DataTable dt = new DataTable("Books");
                    sda.Fill(dt);
                    dg_books.ItemsSource = dt.DefaultView;
                    con.Close();
                }
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            string ConString = ConfigurationManager.ConnectionStrings["connbooks"].ConnectionString;
            string CmdString = string.Empty;
            crud crud = new crud();
            crud.ShowDialog();
            if (crud.DialogResult == true) {
                int idthm = 0;
                int idcat = 0;
                int idprs = 0;
                int idaut = 0;

                using (SqlConnection con = new SqlConnection(ConString))
                {
                    con.Open();
                    CmdString = "SELECT Id FROM Categories WHERE Name=@p1";
                    SqlCommand cmd = new SqlCommand(CmdString, con);
                    cmd.Parameters.AddWithValue("@p1", crud.cb_ctgry.SelectedItem.ToString());
                    SqlDataReader dr = cmd.ExecuteReader();
                    while (dr.Read())
                    {
                        idcat = int.Parse($"{dr["Id"]}");
                    }
                    con.Close();
                }                
                CmdString = string.Empty;

                using (SqlConnection con = new SqlConnection(ConString))
                {
                    con.Open();
                    CmdString = "SELECT Id FROM Authors WHERE FirstName=@p1";
                    SqlCommand cmd = new SqlCommand(CmdString, con);
                    cmd.Parameters.AddWithValue("@p1", crud.cb_author.SelectedItem.ToString());
                    SqlDataReader dr = cmd.ExecuteReader();
                    while (dr.Read())
                    {
                        idaut = int.Parse($"{dr["Id"]}");
                    }
                    con.Close();
                }
                CmdString = string.Empty;

                using (SqlConnection con = new SqlConnection(ConString))
                {
                    con.Open();
                    CmdString = "SELECT Id FROM Themes WHERE Name=@p1";
                    SqlCommand cmd = new SqlCommand(CmdString, con);
                    cmd.Parameters.AddWithValue("@p1", crud.cb_thm.SelectedItem.ToString());
                    SqlDataReader dr = cmd.ExecuteReader();
                    while (dr.Read())
                    {
                        idthm = int.Parse($"{dr["Id"]}");
                    }
                    con.Close();
                }
                CmdString = string.Empty;

                using (SqlConnection con = new SqlConnection(ConString))
                {
                    con.Open();
                    CmdString = "SELECT Id FROM Press WHERE Name=@p1";
                    SqlCommand cmd = new SqlCommand(CmdString, con);
                    cmd.Parameters.AddWithValue("@p1", (crud.cb_prss.SelectedItem.ToString()));
                    SqlDataReader dr = cmd.ExecuteReader();
                    while (dr.Read())
                    {
                        idprs = int.Parse($"{dr["Id"]}");
                    }
                    con.Close();
                }
                CmdString = string.Empty;

                SqlConnection  sqlConnection= new SqlConnection(ConString);
                sqlConnection.Open();
                string query = $"INSERT Books VALUES(@id,@name,@page,@yp,@idt,@idc,@ida,@idp,@com,@qnt)";
                var command = new SqlCommand(query, sqlConnection);
                command.Parameters.AddWithValue("@id",crud.tb_bid.Text);
                command.Parameters.AddWithValue("@name",crud.tb_bname.Text);
                command.Parameters.AddWithValue("@page",crud.tb_bpage.Text);
                command.Parameters.AddWithValue("@yp", crud.tb_yrprs.Text);
                command.Parameters.AddWithValue("@idt",idthm);
                command.Parameters.AddWithValue("@idc",idcat);
                command.Parameters.AddWithValue("@ida",idaut);
                command.Parameters.AddWithValue("@idp",idprs);
                command.Parameters.AddWithValue("@com",crud.tb_bcomm.Text);
                command.Parameters.AddWithValue("@qnt",crud.tb_bqnty.Text);
                command.ExecuteNonQuery();
                crud.Close();
            }
            else
            {
                crud.Close();
            }
            
            CmdString = string.Empty;
            using (SqlConnection con = new SqlConnection(ConString))
            {
                CmdString = "SELECT B.Id [Book ID],B.Name [Book Name],B.Pages [Page count],B.YearPress [Year Press],C.Name Category, (A.FirstName+' '+A.LastName) [Author],T.Name [Theme],P.Name [Press],B.Comment [Comment],B.Quantity [Quantity]" +
                   "FROM Books B INNER JOIN Authors A ON B.Id_Author = A.Id INNER JOIN Categories C ON B.Id_Category = C.Id INNER JOIN Themes T ON B.Id_Themes = T.Id INNER JOIN Press P ON B.Id_Press = P.Id";
                SqlCommand command = new SqlCommand(CmdString, con);
                SqlDataAdapter sda = new SqlDataAdapter(command);
                DataTable dt = new DataTable("Books");
                sda.Fill(dt);
                dg_books.ItemsSource = dt.DefaultView;
            }
        }


        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            crud crud=new crud();


            crud.tb_bid.Text =(dg_books.SelectedItem as DataRowView).Row[0].ToString();

            crud.tb_bname.Text =(dg_books.SelectedItem as DataRowView).Row[1].ToString();

            crud.tb_bpage.Text =(dg_books.SelectedItem as DataRowView).Row[2].ToString();

            crud.tb_yrprs.Text =(dg_books.SelectedItem as DataRowView).Row[3].ToString();

            for (int i = 0; i < crud.cb_thm.Items.Count; i++)
            {                
                if(crud.cb_thm.Items[i].ToString() == (dg_books.SelectedItem as DataRowView).Row[6].ToString())
                {
                    crud.cb_thm.SelectedIndex = i;                    
                    break;
                }
            }
            
            for (int i = 0; i < crud.cb_ctgry.Items.Count; i++)
            {                
                if (crud.cb_ctgry.Items[i].ToString() == (dg_books.SelectedItem as DataRowView).Row[4].ToString())
                {
                    crud.cb_ctgry.SelectedIndex = i;
                    break;
                }
            }

            for (int i = 0; i < crud.cb_author.Items.Count; i++)
            {
                if ((dg_books.SelectedItem as DataRowView).Row[5].ToString().Contains(crud.cb_author.Items[i].ToString()))
                {
                    crud.cb_author.SelectedIndex = i;
                    break;
                }
            }

            for (int i = 0; i < crud.cb_prss.Items.Count; i++)
            {
                if (crud.cb_prss.Items[i].ToString() == (dg_books.SelectedItem as DataRowView).Row[7].ToString())
                {
                    crud.cb_prss.SelectedIndex = i;
                    break;
                }
            }

            crud.tb_bcomm.Text = (dg_books.SelectedItem as DataRowView).Row[8].ToString();

            crud.tb_bqnty.Text = (dg_books.SelectedItem as DataRowView).Row[9].ToString();
            
            crud.ShowDialog();

            if (crud.DialogResult == true)
            {

                string ConString = ConfigurationManager.ConnectionStrings["connbooks"].ConnectionString;
                string CmdString;
                int idthm = 0;
                int idcat = 0;
                int idprs = 0;
                int idaut = 0;

                using (SqlConnection con = new SqlConnection(ConString))
                {
                    con.Open();
                    CmdString = "SELECT Id FROM Categories WHERE Name=@p1";
                    SqlCommand cmd = new SqlCommand(CmdString, con);
                    cmd.Parameters.AddWithValue("@p1", crud.cb_ctgry.SelectedItem.ToString());
                    SqlDataReader dr = cmd.ExecuteReader();
                    while (dr.Read())
                    {
                        idcat = int.Parse($"{dr["Id"]}");
                    }
                    con.Close();
                }
                CmdString = string.Empty;

                using (SqlConnection con = new SqlConnection(ConString))
                {
                    con.Open();
                    CmdString = "SELECT Id FROM Authors WHERE FirstName=@p1";
                    SqlCommand cmd = new SqlCommand(CmdString, con);
                    cmd.Parameters.AddWithValue("@p1", crud.cb_author.SelectedItem.ToString());
                    SqlDataReader dr = cmd.ExecuteReader();
                    while (dr.Read())
                    {
                        idaut = int.Parse($"{dr["Id"]}");
                    }
                    con.Close();
                }
                CmdString = string.Empty;

                using (SqlConnection con = new SqlConnection(ConString))
                {
                    con.Open();
                    CmdString = "SELECT Id FROM Themes WHERE Name=@p1";
                    SqlCommand cmd = new SqlCommand(CmdString, con);
                    cmd.Parameters.AddWithValue("@p1", crud.cb_thm.SelectedItem.ToString());
                    SqlDataReader dr = cmd.ExecuteReader();
                    while (dr.Read())
                    {
                        idthm = int.Parse($"{dr["Id"]}");
                    }
                    con.Close();
                }
                CmdString = string.Empty;

                using (SqlConnection con = new SqlConnection(ConString))
                {
                    con.Open();
                    CmdString = "SELECT Id FROM Press WHERE Name=@p1";
                    SqlCommand cmd = new SqlCommand(CmdString, con);
                    cmd.Parameters.AddWithValue("@p1", (crud.cb_prss.SelectedItem.ToString()));
                    SqlDataReader dr = cmd.ExecuteReader();
                    while (dr.Read())
                    {
                        idprs = int.Parse($"{dr["Id"]}");
                    }
                    con.Close();
                }
                CmdString = string.Empty;

                
                CmdString = "UPDATE Books SET Id=@id,Name=@name,Pages=@page,YearPress=@yp,Id_Category=@idc,Id_Themes=@idt,Id_Author=@ida,Id_Press=@idp,Comment=@com,Quantity=@qnt WHERE Id=@cid";
                SqlConnection sqlConnection = new SqlConnection(ConString);
                sqlConnection.Open();
                var command = new SqlCommand(CmdString, sqlConnection);
                command.Parameters.AddWithValue("@id", crud.tb_bid.Text);
                command.Parameters.AddWithValue("@name", crud.tb_bname.Text);
                command.Parameters.AddWithValue("@page", crud.tb_bpage.Text);
                command.Parameters.AddWithValue("@yp", crud.tb_yrprs.Text);
                command.Parameters.AddWithValue("@idt", idthm);
                command.Parameters.AddWithValue("@idc", idcat);
                command.Parameters.AddWithValue("@ida", idaut);
                command.Parameters.AddWithValue("@idp", idprs);
                command.Parameters.AddWithValue("@com", crud.tb_bcomm.Text);
                command.Parameters.AddWithValue("@qnt", crud.tb_bqnty.Text);
                command.Parameters.AddWithValue("@cid", (dg_books.SelectedItem as DataRowView).Row[0].ToString());
                command.ExecuteNonQuery();              

            }
            string ConString1 = ConfigurationManager.ConnectionStrings["connbooks"].ConnectionString;
            string CmdString1;
            CmdString1 = string.Empty;
            using (SqlConnection con = new SqlConnection(ConString1))
            {
                CmdString1 = "SELECT B.Id [Book ID],B.Name [Book Name],B.Pages [Page count],B.YearPress [Year Press],C.Name Category, (A.FirstName+' '+A.LastName) [Author],T.Name [Theme],P.Name [Press],B.Comment [Comment],B.Quantity [Quantity]" +
                   "FROM Books B INNER JOIN Authors A ON B.Id_Author = A.Id INNER JOIN Categories C ON B.Id_Category = C.Id INNER JOIN Themes T ON B.Id_Themes = T.Id INNER JOIN Press P ON B.Id_Press = P.Id";
                SqlCommand command = new SqlCommand(CmdString1, con);
                SqlDataAdapter sda = new SqlDataAdapter(command);
                DataTable dt = new DataTable("Books");
                sda.Fill(dt);
                dg_books.ItemsSource = dt.DefaultView;
            }
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            string ConString = ConfigurationManager.ConnectionStrings["connbooks"].ConnectionString;
            string CmdString = string.Empty;
            SqlConnection sqlConnection = new SqlConnection(ConString);
            sqlConnection.Open();
            string query = $"DELETE FROM Books WHERE Id=@bid";
            SqlCommand command= new SqlCommand(query, sqlConnection);
            command.Parameters.AddWithValue("@bid", (dg_books.SelectedItem as DataRowView).Row[0].ToString());
            command.ExecuteNonQuery();
            sqlConnection.Close(); string ConString1 = ConfigurationManager.ConnectionStrings["connbooks"].ConnectionString;
            string CmdString1;
            CmdString1 = string.Empty;
            using (SqlConnection con = new SqlConnection(ConString1))
            {
                CmdString1 = "SELECT B.Id [Book ID],B.Name [Book Name],B.Pages [Page count],B.YearPress [Year Press],C.Name Category, (A.FirstName+' '+A.LastName) [Author],T.Name [Theme],P.Name [Press],B.Comment [Comment],B.Quantity [Quantity]" +
                   "FROM Books B INNER JOIN Authors A ON B.Id_Author = A.Id INNER JOIN Categories C ON B.Id_Category = C.Id INNER JOIN Themes T ON B.Id_Themes = T.Id INNER JOIN Press P ON B.Id_Press = P.Id";
                command = new SqlCommand(CmdString1, con);
                SqlDataAdapter sda = new SqlDataAdapter(command);
                DataTable dt = new DataTable("Books");
                sda.Fill(dt);
                dg_books.ItemsSource = dt.DefaultView;
            }
        }
    }
}
