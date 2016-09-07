using Newtonsoft.Json;
using System;
using System.Windows.Forms;
using WebAPI_Teacher.SupportClasses;

namespace WebAPI_Teacher
{
    public partial class NSForm : Form
    {    
        public NSForm()
        {
            InitializeComponent();
        }

        private string TeacherLogin;
        private string TeacherPassword;

        private async void SignInBTN_Click(object sender, EventArgs e)
        {
            if ((LoginTB.Text == "") || (PasswordTB.Text == ""))
            {
                MessageBox.Show("Все поля должны быть заполнены!", "Ошибка.");
                return;
            }
            TeacherLogin = LoginTB.Text;
            TeacherPassword = PasswordTB.Text;
            string answer = null;
            label5.Text = "Идет процесс авторизации. Пожалуйста, ожидайте...";
            try
            {               
                answer = await Server.GetResponseAsync("http://localhost:57755/Account/SignInTeacher?Login=" + TeacherLogin + "&" + "Password=" + TeacherPassword);               
            }
            catch (System.Net.WebException)
            {
                label5.Text = "";
                MessageBox.Show("К сожалению не удалось установить соединение с сервером, которое необходимо для работы программы. Попробуйте позже.", "Ошибка.");
                return;
            }
            label5.Text = "";
            if (Convert.ToInt32(answer) == -1)
            {
                MessageBox.Show(@"Неверно указан логин и\или пароль.", "Ошибка.");
                return;
            }
            LoginTB.Clear();
            PasswordTB.Clear();
            label1.Visible = false;
            label2.Visible = false;
            LoginTB.Visible = false;
            PasswordTB.Visible = false;
            SignInBTN.Visible = false;
            DepartmentChB.Visible = true;
            DepartmentChB.Enabled = false;
            GroupChB.Visible = true;
            GroupChB.Enabled = false;
            AllDepChB.Visible = true;
            AllDepChB.Enabled = false;
            SendFacultyRB.Visible = true;
            SendDepartmentRB.Visible = true;
            SendGroupRB.Visible = true;
            label3.Visible = true;
            MessageRTB.Visible = true;
            MessageRTB.Enabled = false;
            SendBTN.Visible = true;
            DepartmentCB.Visible = true;
            DepartmentCB.Enabled = false;
            ThemeTB.Visible = true;
            ThemeTB.Enabled = false;
            label4.Visible = true;
        }

        private void NSForm_Load(object sender, EventArgs e)
        {
            DepartmentChB.Visible = false;
            GroupChB.Visible = false;
            AllDepChB.Visible = false;
            SendFacultyRB.Visible = false;
            SendDepartmentRB.Visible = false;
            SendGroupRB.Visible = false;
            label3.Visible = false;
            MessageRTB.Visible = false;
            SendBTN.Visible = false;
            DepartmentCB.Visible = false;
            ThemeTB.Visible = false;
            label4.Visible = false;
        }

        private async void DepartmentChB_SelectedIndexChanged(object sender, EventArgs e)
        {
            int l = DepartmentChB.Items.Count;
            string g = null;
            try
            {
                g = await Server.GetResponseAsync("http://localhost:57755/GetData/GetGroupsFromDepartment?DepartmentName=" + DepartmentChB.SelectedItem.ToString());                
            }
            catch (System.Net.WebException)
            {
                MessageBox.Show("К сожалению не удалось установить соединение с удаленным сервером. Попробуйте позже", "Ошибка.");
                return;
            }
            var group = JsonConvert.DeserializeObject<string[]>(g);
            if (DepartmentChB.GetItemChecked(DepartmentChB.SelectedIndex) == true)
            {
                for (int j = 0; j < group.Length; j++)
                {
                    GroupChB.Items.Add(group[j]);
                }
            }
            else
            {
                for (int i = 0; i < group.Length; i++)
                {
                    GroupChB.Items.Remove(group[i]);
                }
                GroupChB.Update();
            }
        }

        private void AllDepChB_CheckedChanged(object sender, EventArgs e)
        {
            if (AllDepChB.Checked == true)
            {
                GroupChB.Items.Clear();
                for(int i = 0; i < DepartmentChB.Items.Count; i++)
                {
                    DepartmentChB.SetItemChecked(i, true);
                    DepartmentChB.SetSelected(i, true);                   
                }              
            }
            else
            {
                for(int i = 0; i < DepartmentChB.Items.Count; i++)
                {
                    DepartmentChB.SetItemChecked(i, false);
                }
                GroupChB.Update();                
                GroupChB.Items.Clear();
            }           
        }

        private async void SendGroupRB_CheckedChanged(object sender, EventArgs e)
        {
            if (SendGroupRB.Checked == true)
            {
                AllDepChB.Enabled = true;
                DepartmentChB.Enabled = true;
                GroupChB.Enabled = true;
                MessageRTB.Enabled = true;
                ThemeTB.Enabled = true;
                DepartmentCB.Items.Clear();
                DepartmentCB.Text = "";
                DepartmentCB.Enabled = false;

                string f = null;
                string temp_res = null;
                string[] department = null;               
                try
                {
                    temp_res = await Server.GetResponseAsync("http://localhost:57755/GetData/GetTeacherFaculty?Login=" + TeacherLogin);
                    f = JsonConvert.DeserializeObject<string>(temp_res);
                    var d = await Server.GetResponseAsync("http://localhost:57755/GetData/GetDepartmentsFromFaculty?FacultyName=" + f);
                    department = JsonConvert.DeserializeObject<string[]>(d);                   
                }
                catch (System.Net.WebException)
                {
                    MessageBox.Show("К сожалению не удалось установить соединение с удаленным сервером. Попробуйте позже", "Ошибка.");
                    return;
                }
                for (int i = 0; i < department.Length; i++)
                {
                    DepartmentChB.Items.Add(department[i]);
                }
            }
        }

        private async void SendDepartmentRB_CheckedChanged(object sender, EventArgs e)
        {
            AllDepChB.Enabled = false;
            DepartmentChB.Enabled = false;
            GroupChB.Enabled = false;
            DepartmentChB.Items.Clear();
            GroupChB.Items.Clear();
            AllDepChB.Checked = false;
            MessageRTB.Enabled = true;
            DepartmentCB.Enabled = true;
            ThemeTB.Enabled = true;
            DepartmentCB.Items.Clear();


            string f = null;
            string temp_res = null;
            string[] department = null;
            try
            {
                temp_res = await Server.GetResponseAsync("http://localhost:57755/GetData/GetTeacherFaculty?Login=" + TeacherLogin);
                f = JsonConvert.DeserializeObject<string>(temp_res);
                var d = await Server.GetResponseAsync("http://localhost:57755/GetData/GetDepartmentsFromFaculty?FacultyName=" + f);
                department = JsonConvert.DeserializeObject<string[]>(d);               
            }
            catch (System.Net.WebException)
            {
                MessageBox.Show("К сожалению не удалось установить соединение с удаленным сервером. Попробуйте позже", "Ошибка.");
                return;
            }
            for (int i = 0; i < department.Length; i++)
            {
                DepartmentCB.Items.Add(department[i]);
            }
            DepartmentCB.SelectedIndex = 0;
        }

        private void SendFacultyRB_CheckedChanged(object sender, EventArgs e)
        {
            AllDepChB.Enabled = false;
            DepartmentChB.Enabled = false;
            GroupChB.Enabled = false;
            MessageRTB.Enabled = true;
            ThemeTB.Enabled = true;
            DepartmentCB.Items.Clear();
            DepartmentCB.Text = "";
            DepartmentCB.Enabled = false;
        }

        private async void SendBTN_Click(object sender, EventArgs e)
        {
            bool flag = false;
            int count = 0;
            string answer = string.Empty;

            if ((ThemeTB.Text == "" || (MessageRTB.Text == ""))) {
                MessageBox.Show("Для отправки сообщения необходимо ввести его тему и текст!", "Ошибка.");
                return;
            }
            if (SendFacultyRB.Checked == true)
            {
                label6.Text = "Идет рассылка сообщений...";
                try
                {
                   answer = await  Server.GetResponseAsync("http://localhost:57755/Send/SendAllFacultyAsync?Theme=" + ThemeTB.Text + "&" +
                    "Text=" + MessageRTB.Text + "&" +
                    "Login=" + TeacherLogin + "&" +
                    "Password=" + TeacherPassword);
                }
                catch (System.Net.WebException)
                {
                    label6.Text = "Связь с сервером потеряна...";
                    MessageBox.Show("Удаленный сервер не отвечает. Рассылка временно невозможна.", "Ошибка.");
                    return;
                }
                if (Convert.ToInt16(answer) == -2)
                {
                    MessageBox.Show("Ваше сообщение было отправлено только на Android-приложение для студентов. СМС рассылка временно недоступна.", "Небольшой сбой.");
                }
                label6.Text = "Рассылка сообщений окончена.";
            }
            else if (SendDepartmentRB.Checked == true)
            {
                label6.Text = "Идет рассылка сообщений...";
                try
                {
                   answer = await Server.GetResponseAsync("http://localhost:57755/Send/SendAllDepartmentAsync?Theme=" + ThemeTB.Text + "&" +
                       "Text=" + MessageRTB.Text + "&" +
                       "DepartmentName=" + DepartmentCB.SelectedItem.ToString() + "&" +
                       "Login=" + TeacherLogin + "&" +
                       "Password=" + TeacherPassword);                   
                }
                catch (System.Net.WebException)
                {
                    label6.Text = "Связь с сервером потеряна...";
                    MessageBox.Show("Удаленный сервер не отвечает. Рассылка временно невозможна.", "Ошибка.");
                    return;
                }
                if (Convert.ToInt16(answer) == -2)
                {
                    MessageBox.Show("Ваше сообщение было отправлено только на Android-приложение для студентов. СМС рассылка временно недоступна.", "Небольшой сбой.");
                }
                label6.Text = "Рассылка сообщений окончена.";
            }
            else
            {
                for (int i = 0; i < GroupChB.Items.Count; i++)
                {
                    if (GroupChB.GetItemChecked(i) == true)
                    {
                        flag = true;
                        count++;
                    }
                }
                if (!flag)
                {
                    MessageBox.Show("Выберите получателя!", "Ошибка.");
                    return;
                }
                string[] GroupList = new string[count];
                var a = GroupChB.CheckedItems;
                for (int i = 0; i < a.Count; i++)
                {
                    GroupList[i] = a[i].ToString();
                }
                SentOptions P = new SentOptions(GroupList.Length);
                P.Theme = ThemeTB.Text;
                P.Text = MessageRTB.Text;
                P.Login = TeacherLogin;
                P.Password = TeacherPassword;
                for (int i = 0; i < P.Groups.Length; i++)
                {
                    P.Groups[i] = GroupList[i];
                }
                string sobj = JsonConvert.SerializeObject(P);
                try
                {
                    answer = await Server.GetResponseAsync("http://localhost:57755/Send/SendGroupsSet?options=" + sobj);
                }
                catch (System.Net.WebException)
                {
                    label6.Text = "Связь с сервером потеряна...";
                    MessageBox.Show("Удаленный сервер не отвечает. Рассылка временно невозможна.", "Ошибка.");
                    return;
                }
                if (Convert.ToInt16(answer) == -2)
                {
                    MessageBox.Show("Ваше сообщение было отправлено только на Android-приложение для студентов. СМС рассылка временно недоступна.", "Небольшой сбой.");
                }
                label6.Text = "Рассылка сообщений окончена.";
            }
            ThemeTB.Clear();
            MessageRTB.Clear();
        }
    }
}
