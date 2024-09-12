using System;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace EasyLectureForm
{
    public partial class Login : Form
    {
        private readonly ApiProvider _apiProvider;

        public Login()
        {
            InitializeComponent();
            _apiProvider = new ApiProvider("https://localhost:7107/login");
        }

        private void Login_Load(object sender, EventArgs e)
        {

        }

        private async void LoginBtn_Click(object sender, EventArgs e)
        {
            string mail = textBox1.Text;
            string pass = textBox2.Text;

            if (mail == "admin" && pass == "123")
            {
                MessageBox.Show("Login successful!");
                this.Close();
            }
            else
            {
                var loginData = new
                {
                    id = 0,
                    email = mail,
                    password = pass
                };

                try
                {
                    /*
                    // JSON yanıtını doğrudan string olarak al
                    string response = await _apiProvider.POST("login", loginData);

                    using (JsonDocument doc = JsonDocument.Parse(response))
                    {
                        if (doc.RootElement.TryGetProperty("token", out JsonElement tokenElement))
                        {
                            string token = tokenElement.GetString();
                            if (!string.IsNullOrEmpty(token))
                            {
                                MessageBox.Show("Login successful!");
                                this.Close();
                            }
                        }
                        else
                        {
                            MessageBox.Show("Login unsuccessful!");
                        }
                    }
                    */
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error: " + ex.Message);
                }
            }
        }

    }
}
