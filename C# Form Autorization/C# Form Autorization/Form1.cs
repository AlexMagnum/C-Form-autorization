using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace C__Form_Autorization
{
    public partial class Form1: Form
    {
        SqlConnection conn = new SqlConnection(Properties.Settings.Default.computerConnectionString);
        public Form1()
        { 
            InitializeComponent();
        }

        private void bunifuMaterialTextbox2_OnValueChanged(object sender, EventArgs e)
        {
            password.isPassword = true;
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void bunifuFlatButton3_Click(object sender, EventArgs e)
        {
            Validation();
        }

        private bool Validation()
        {
            if (login.Text.ToString().Trim() == string.Empty ||
                password.Text.ToString().Trim() == string.Empty)
            {
                MessageBox.Show("Будь ласка введіть логін та пароль!", "Не заповнені обов'язкові поля",
                    MessageBoxButtons.OK,MessageBoxIcon.Warning);
                return false;
            }
            else
                return true;
        }

        private string Encrypt(string password)
        {
            string salt = "kn221";
            byte[] data = UTF8Encoding.UTF8.GetBytes(password);

            MD5CryptoServiceProvider md5 = new MD5CryptoServiceProvider();
            TripleDESCryptoServiceProvider tripDes = new TripleDESCryptoServiceProvider();
            tripDes.Key = md5.ComputeHash(UTF8Encoding.UTF8.GetBytes(salt));
            tripDes.Mode = CipherMode.ECB;

            ICryptoTransform transform = tripDes.CreateDecryptor();
            byte[] result = transform.TransformFinalBlock(data, 0, data.Length);

            return Convert.ToBase64String(result);
        }
    }
}
