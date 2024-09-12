using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace EasyLectureForm.Admin
{
    public partial class AdminMain : Form
    {
        private readonly ApiProvider _apiProvider;
        public AdminMain()
        {
            InitializeComponent();
            _apiProvider = new ApiProvider("https://localhost:7107/");

        }

        private void AdminMain_Load(object sender, EventArgs e)
        {
            LectureLbx.Hide();
        }
    }
}
