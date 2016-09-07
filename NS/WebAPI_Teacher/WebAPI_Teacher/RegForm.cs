using Newtonsoft.Json;
using System;
using System.Windows.Forms;
using WebAPI.Models;

namespace WebAPI_Teacher
{
    public partial class RegForm : Form
    {
        public RegForm()
        {
            InitializeComponent();
        }

        private async void RegForm_Load(object sender, EventArgs e)
        {
            string f = null;
            string[] faculty = null;
            try
            {
                f = await Server.GetResponseAsync("http://localhost:57755/GetData/GetFaculties");                
            }
            catch (System.Net.WebException)
            {
                MessageBox.Show("К сожалению не удалось установить соединение с сервером, которое необходимо для работы программы. Попробуйте позже.", "Ошибка.");
                this.Close();
                return;
            }
            faculty = JsonConvert.DeserializeObject<string[]>(f);
            for (int i = 0; i < faculty.Length; i++)
            {
                FacultyCB.Items.Add(faculty[i]);
            }
            DepartmentCB.Enabled = false;
            RegBTN.Enabled = false;
            FacultyCB.Text = FacultyCB.Items[0].ToString();
        }

        private async void RegBTN_Click(object sender, EventArgs e)
        {
            if (PasswordTB.Text != CPasswordTB.Text)
            {
                MessageBox.Show("Не совпадают введенные пароли!", "Ошибка регистрации.");
                return;
            }

            if((LoginTB.Text=="") || (PasswordTB.Text=="") || (CPasswordTB.Text=="") || (SurnameTB.Text=="") || (NameTB.Text=="") || (PatronymicTB.Text == ""))
            {
                MessageBox.Show("Все поля должны быть заполнены.", "Ошибка регистрации.");
                return;
            }
            TeacherJson Teacher = new TeacherJson();
            Teacher.Login = LoginTB.Text;
            Teacher.Password = PasswordTB.Text;
            Teacher.Surname = SurnameTB.Text;
            Teacher.Name = NameTB.Text;
            Teacher.Patronymic = PatronymicTB.Text;
            Teacher.Department = DepartmentCB.SelectedItem.ToString();
            string s = JsonConvert.SerializeObject(Teacher);
            string answer = null;
            try
            {
                answer = await Server.GetResponseAsync("http://localhost:57755/Account/AddTeacher?Teacher=" + s);                               
            }
            catch (System.Net.WebException)
            {
                MessageBox.Show("Удаленный сервер не отвечает. Регистрация временно невозможна.", "Ошибка регистрации.");
            }

            if (Convert.ToInt32(answer) == -1)
            {
                MessageBox.Show("Пользователь с таким логином уже существует.", "Ошибка регистрации.");
                return;
            }
            else
            {
                ClearFields();
                MessageBox.Show("     Поздравляем! Регистрация прошла успешно!     ", "Регистрация.");
            }
        }

        private async void FacultyCB_SelectedIndexChanged(object sender, EventArgs e)
        {
            DepartmentCB.Items.Clear();
            DepartmentCB.Text = "";
            DepartmentCB.Enabled = true;
            string d = null;
            try
            {
                d =  await Server.GetResponseAsync("http://localhost:57755/GetData/GetDepartmentsFromFaculty?FacultyName=" + FacultyCB.SelectedItem);                
            }
            catch (System.Net.WebException)
            {
                MessageBox.Show("К сожалению не удалось установить соединение с сервером, которое необходимо для работы программы. Попробуйте позже.", "Ошибка.");
            }

            var department = JsonConvert.DeserializeObject<string[]>(d);
            for (int i = 0; i < department.Length; i++)
            {
                DepartmentCB.Items.Add(department[i]);
            }
        }

        private void DepartmentCB_SelectedIndexChanged(object sender, EventArgs e)
        {
            RegBTN.Enabled = true;
        }

        private void LoginTB_TextChanged(object sender, EventArgs e)
        {
            if (LoginTB.Text == "")
            {
                RegBTN.Enabled = false;
            }
            else
            {
                RegBTN.Enabled = true;
            }
        }

        private void PasswordTB_TextChanged(object sender, EventArgs e)
        {
            if (PasswordTB.Text == "")
            {
                RegBTN.Enabled = false;
            }
            else
            {
                RegBTN.Enabled = true;
            }
        }

        private void CPasswordTB_TextChanged(object sender, EventArgs e)
        {
            if (CPasswordTB.Text == "")
            {
                RegBTN.Enabled = false;
            }
            else
            {
                RegBTN.Enabled = true;
            }
        }

        private void SurnameTB_TextChanged(object sender, EventArgs e)
        {
            if (SurnameTB.Text == "")
            {
                RegBTN.Enabled = false;
            }
            else
            {
                RegBTN.Enabled = true;
            }
        }

        private void NameTB_TextChanged(object sender, EventArgs e)
        {
            if (NameTB.Text == "")
            {
                RegBTN.Enabled = false;
            }
            else
            {
                RegBTN.Enabled = true;
            }
        }

        private void PatronymicTB_TextChanged(object sender, EventArgs e)
        {
            if (PatronymicTB.Text == "")
            {
                RegBTN.Enabled = false;
            }
            else
            {
                RegBTN.Enabled = true;
            }
        }

        private void ClearFields()
        {
            LoginTB.Clear();
            PasswordTB.Clear();
            CPasswordTB.Clear();
            SurnameTB.Clear();
            NameTB.Clear();
            PatronymicTB.Clear();
            FacultyCB.Text = "";
            DepartmentCB.Text = "";
            DepartmentCB.Enabled = false;
        }
    }
}
