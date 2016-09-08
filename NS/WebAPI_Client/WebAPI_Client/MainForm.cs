using Newtonsoft.Json;
using System;
using System.Windows.Forms;
using WebAPI.Models;

namespace WebAPI_Client
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
        }

        private async void RegBTN_Click(object sender, EventArgs e)
        {
            if (RegCB.Checked)
            {
                if (PasswordTB.Text != CPasswordTB.Text)
                {
                    MessageBox.Show("Не совпадают введенные пароли!", "Ошибка регистрации.");
                    return;
                }
                if((BookNumberTB.Text=="") || (PhoneNumberTB.Text=="") || (EmailAddressTB.Text=="") ||(PasswordTB.Text=="") || (CPasswordTB.Text==""))
                {
                    MessageBox.Show("Все поля должны быть заполнены.", "Ошибка регистрации.");
                    return;
                }
                StudentJson Student = new StudentJson();
                Student.BookNumber = BookNumberTB.Text;
                Student.PhoneNumber = PhoneNumberTB.Text;
                Student.EmailAddress = EmailAddressTB.Text;
                Student.Password = PasswordTB.Text;                
                Student.Group = GroupCB.SelectedItem.ToString();
                string s = JsonConvert.SerializeObject(Student);
                string answer = null;
                try
                {
                    answer = await Server.GetResponseAsync("http://localhost:57755/Account/AddStudent?Student=" + s);                    
                }
                catch (System.Net.WebException)
                {
                    MessageBox.Show("Удаленный сервер не отвечает. Регистрация временно невозможна.", "Ошибка регистрации.");
                }
                switch (Convert.ToInt32(answer))
                {
                    case -1:
                        MessageBox.Show("Пользователь с таким номером зачетной книги уже существует.", "Ошибка регистрации.");
                        break;
                    case -2:
                        MessageBox.Show("Пользователь с таким Email адресом уже существует.", "Ошибка регистрации.");
                        break;
                    case -3:
                        MessageBox.Show("Пользователь с таким номером телефона уже существует.", "Ошибка регистрации.");
                        break;
                    case 1:
                        ClearFields();
                        MessageBox.Show("Поздравляем! Регистрация прошла успешно!.", "Регистрация студента.");
                        break;
                }
            }
            else
            {
                MessageBox.Show("Для регистрации вам необходимо ввести свои данные и дать согласие на их обработку.", "Ошибка регистрации.");
            }
        }

        private async void MainForm_Load(object sender, EventArgs e)
        {
            DepartmentCB.Enabled = false;
            GroupCB.Enabled = false;
            RegBTN.Enabled = false;           
            PhoneNumberTB.Text = "+7";
            string f = null;
            try
            {
                f = await Server.GetResponseAsync("http://localhost:57755/GetData/GetFaculties");                
            }
            catch (System.Net.WebException)
            {
                MessageBox.Show("К сожалению не удалось установить соединение с сервером, которое необходимо для работы программы. Попробуйте позже.", "Ошибка.");
                Application.Exit();
            }
            var faculty = JsonConvert.DeserializeObject<string[]>(f);
            for (int i = 0; i < faculty.Length; i++)
            {
                FacultyCB.Items.Add(faculty[i]);
            }
        }

        private async void FacultyCB_SelectedIndexChanged(object sender, EventArgs e)
        {
            DepartmentCB.Enabled = true;
            GroupCB.Enabled = false;
            GroupCB.Items.Clear();
            GroupCB.Text = "";
            DepartmentCB.Items.Clear();
            DepartmentCB.Text = "";
            string d = null;
            try
            {
                d = await Server.GetResponseAsync("http://localhost:57755/GetData/GetDepartmentsFromFaculty?FacultyName=" + FacultyCB.SelectedItem);                
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

        private async void DepartmentCB_SelectedIndexChanged(object sender, EventArgs e)
        {
            GroupCB.Enabled = true;
            GroupCB.Items.Clear();
            GroupCB.Text = "";
            string g = null;
            try
            {
                g = await Server.GetResponseAsync("http://localhost:57755/GetData/GetGroupsFromDepartment?DepartmentName=" + DepartmentCB.SelectedItem);               
            }
            catch (System.Net.WebException)
            {
                MessageBox.Show("К сожалению не удалось установить соединение с сервером, которое необходимо для работы программы. Попробуйте позже.", "Ошибка.");
            }
            var group = JsonConvert.DeserializeObject<string[]>(g);
            for (int i = 0; i < group.Length; i++)
            {
                GroupCB.Items.Add(group[i]);
            }
        }

        private void ClearFields()
        {
            BookNumberTB.Clear();
            PhoneNumberTB.Clear();
            EmailAddressTB.Clear();
            PasswordTB.Clear();
            CPasswordTB.Clear();
            FacultyCB.Text = "";
            DepartmentCB.Text = "";
            DepartmentCB.Enabled = false;
            GroupCB.Text = "";
            GroupCB.Enabled = false;
            RegCB.Checked = false;
        }

        private void BookNumberTB_TextChanged(object sender, EventArgs e)
        {
            try
            {
                Convert.ToInt32(BookNumberTB.Text);
            }
            catch (System.FormatException)
            {
                BookNumberTB.Clear();
            }
            if (BookNumberTB.Text == "")
            {
                RegBTN.Enabled = false;
            }
            else
            {
                RegBTN.Enabled = true;
            }
        }

        private void PhoneNumberTB_TextChanged(object sender, EventArgs e)
        {            
            if (PhoneNumberTB.Text == "")
            {
                RegBTN.Enabled = false;
            }
            else
            {
                RegBTN.Enabled = true;
            }

            if (PhoneNumberTB.Text.Length == 1)
            {
                PhoneNumberTB.Text = "+7";
            }
        }

        private void EmailAddressTB_TextChanged(object sender, EventArgs e)
        {
            if (EmailAddressTB.Text == "")
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

        public string get()
        {
            return "asdasd";
        }

    }
}
