using System;
using System.Collections.Generic;
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
using System.Data.OleDb;
using System.Data.Sql;
using System.Data.SqlClient;
namespace MTC
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        MTCDataContext dc = new MTCDataContext(Properties.Settings.Default.MTCConnectionString);

        public MainWindow()
        {
            InitializeComponent();

           
        }

        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            SqlConnection con;
            con = new SqlConnection(@"Data Source=IVAN\SQLEXPRESS;Initial Catalog=MTC;Integrated Security=True");
            try
            {
                con.Open();

                SqlCommand com = new SqlCommand("Select * from Users where login='" + tbLogin.Text + "' and password='" + tbPassword.Password + "'", con);
                if (com.ExecuteScalar() == null)
                {
                    System.Windows.MessageBox.Show("Пароль введен не верно");
                }
                else
                {
                    SqlCommand comRole = new SqlCommand("select Роль from Users where login='" + tbLogin.Text + "' and password='" + tbPassword.Password + "'", con);
                    if (((string)comRole.ExecuteScalar()).ToString().Replace(" ", "") == "Технолог")
                    {
                        AddClient win = new AddClient();
                        win.Show();
                        this.Close();
                    }
                    if (((string)comRole.ExecuteScalar()).ToString().Replace(" ", "") == "Оператор")
                    {
                        addCall win = new addCall();
                        win.Show();
                        this.Close();
                    }
                    if (((string)comRole.ExecuteScalar()).ToString().Replace(" ", "") == "Администратор")
                    {
                        AddUser win = new AddUser();
                        win.Show();
                        this.Close();
                    }
                }
            }

            catch (System.Data.SqlClient.SqlException ex) //Catch SqlException
            {
                MessageBox.Show("Отсутствует подключение к БД");
            }
            catch (Exception ex) //Catch Other Exception
            {
                MessageBox.Show(ex.Message);
            }

            
        }
    }
}
