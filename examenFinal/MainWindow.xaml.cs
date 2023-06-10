using System;
using System.Collections.Generic;
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

namespace examenFinal
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            MostrarDatos();
        }

        SqlConnection conn = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\mario\source\repos\examenFinal\examenFinal\Database1.mdf;Integrated Security=True");
        

        public void MostrarDatos()
        {
            SqlCommand command = new SqlCommand("SELECT Carnet, Nombre, Telefono, Grado FROM Alumno", conn);
            SqlDataAdapter adapter = new SqlDataAdapter(command);
            DataTable dataTable = new DataTable();
            adapter.Fill(dataTable);
            mostrarData.ItemsSource = dataTable.DefaultView;
        }

        public void GuardarDatos()
        {
            string valorCarnet = txtCarnet.Text;
            string valorNombre = txtNombre.Text;
            string valorTelefono = txtTelefono.Text;
            string valorGrado = txtGrado.Text;

            SqlCommand command = new SqlCommand("INSERT INTO Alumno (Carnet, Nombre, Telefono, Grado) VALUES (@ValorCarnet, @ValorNombre, @ValorTelefono, @ValorGrado)", conn);
            conn.Open();
            command.Parameters.AddWithValue("@ValorCarnet", valorCarnet);
            command.Parameters.AddWithValue("@ValorNombre", valorNombre);
            command.Parameters.AddWithValue("@ValorTelefono", valorTelefono);
            command.Parameters.AddWithValue("@ValorGrado", valorGrado);

            command.ExecuteNonQuery();
            conn.Close();
            txtCarnet.Text = "";
            txtNombre.Text = "";
            txtTelefono.Text = "";
            txtGrado.Text = "";

            MostrarDatos();
        }

        public void EliminarDatos()
        {
            conn.Open();
            string valorCarnet = txtCarnet.Text;
            string eliminarData = "DELETE FROM Alumno WHERE Carnet = @ValorCarnet";
            SqlCommand command = new SqlCommand(eliminarData, conn);
            command.Parameters.AddWithValue("@ValorCarnet", valorCarnet);
            command.ExecuteNonQuery();
            conn.Close();
            txtCarnet.Text = "";
            MostrarDatos();
        }

        private void GuardarInfo_Click(object sender, RoutedEventArgs e)
        {
            GuardarDatos();
        }

        private void EliminarInfo_Click(object sender, RoutedEventArgs e)
        {
            EliminarDatos();
        }
    }
}
