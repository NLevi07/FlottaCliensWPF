using System.Net.Http;
using System.Text;
using System.Text.Json;
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
using FlottaCliensWPF.DTO;
using System.Net.Http.Json;
using FlottaCliensWPF.Models;
namespace FlottaCliensWPF
  
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private HttpClient client = new HttpClient()
        {
            BaseAddress = new Uri("http://localhost:5000")
        };
        List<User> userList = new List<User>();
        public MainWindow()
        {
            InitializeComponent();
            dgrUsers.ItemsSource = userList;

        }

        private async void loginbtn(object sender, RoutedEventArgs e)
        {
            LoginDTO logindto = new LoginDTO()
            {
                LoginName = tbxLoginName.Text,
                Password = tbxPassword.Text
            };
            string dtoString = JsonSerializer.Serialize(logindto, JsonSerializerOptions.Default);

            var body = new StringContent(dtoString, Encoding.UTF8,"application/json");
            var valasz = await client.PostAsync("api/Login", body);

            tbkResult.Text = valasz.StatusCode.ToString();
        }

        private async void btnGetAll_Click(object sender, RoutedEventArgs e)
        {
            string url = "api/User/GetAllUser";
            var users = await client.GetFromJsonAsync<List<User>>(url);
            dgrUsers.ItemsSource=users;
        }
    }
}