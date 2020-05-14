using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Esoft_Project
{
    public partial class FormClient : Form
    {
        public FormClient()
        {
            InitializeComponent();
            ShowClient();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void listView1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (listViewClient.SelectedItems.Count==1)
            {
                ClientsSet clientSet = listViewClient.SelectedItems[0].Tag as ClientsSet;
                clientSet.FirstName = textBoxFirstName.Text;
                clientSet.MiddleName = textBoxMiddleName.Text;
                clientSet.LastName = textBoxLastName.Text;
                clientSet.Phone = textBoxPhone.Text;
                clientSet.Email = textBoxEmail.Text;
            }
            else
            {
                textBoxFirstName.Text = "";
                textBoxMiddleName.Text = "";
                textBoxLastName.Text = "";
                textBoxPhone.Text = "";
                textBoxEmail.Text = "";
            }
        }
        void ShowClient()
        {
            //предварительно очищаем lastView
            listViewClient.Items.Clear();
            //проходимся по коллекции клиентов, которые находятся в базе с помощью foreach
            foreach (ClientsSet clientsSet in Program.wftDb.ClientsSet)
            {
                //создаем новый элемент в listView
                //для этого создаем новый массив строк
                ListViewItem item = new ListViewItem(new string[]
                {
                    clientsSet.Id.ToString(), clientsSet.FirstName, clientsSet.MiddleName,
                    clientsSet.LastName, clientsSet.Phone, clientsSet.Email
                });
                //указываем по какому тегу будем брать элементы
                item.Tag = clientsSet;
                //добавляем элементы в listView для отображения
                listViewClient.Items.Add(item);
            }
            //выравниваем колонки 
            listViewClient.AutoResizeColumns(ColumnHeaderAutoResizeStyle.HeaderSize);
        }

        private void buttonAdd_Click(object sender, EventArgs e)
        {
            //создаем новый экземпляр класса Клиент
            ClientsSet clientSet = new ClientsSet();
            //делаем ссылку на объект, который хранится в textBox-ax
            clientSet.FirstName = textBoxFirstName.Text;
            clientSet.MiddleName = textBoxMiddleName.Text;
            clientSet.LastName = textBoxLastName.Text;
            clientSet.Phone = textBoxPhone.Text;
            clientSet.Email = textBoxEmail.Text;
            //добавляем в таблицу ClientSet нового клиента clientSet
            Program.wftDb.ClientsSet.Add(clientSet);
            //сохраняем изменения в модели wftDb
            Program.wftDb.SaveChanges();
            ShowClient();

        }

        private void textBoxMiddleName_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBoxFirstName_TextChanged(object sender, EventArgs e)
        {

        }

        private void FormClient_Load(object sender, EventArgs e)
        {

        }

        private void buttonEdit_Click(object sender, EventArgs e)
        {
            if (listViewClient.SelectedItems.Count == 1)
            {
                ClientsSet clientSet = listViewClient.SelectedItems[0].Tag as ClientsSet;
                clientSet.FirstName = textBoxFirstName.Text;
                clientSet.MiddleName = textBoxMiddleName.Text;
                clientSet.LastName = textBoxLastName.Text;
                clientSet.Phone = textBoxPhone.Text;
                clientSet.Email = textBoxEmail.Text;
                Program.wftDb.SaveChanges();
                ShowClient();
            }
        }

        private void buttonDel_Click(object sender, EventArgs e)
        {
            try
            {
                if (listViewClient.SelectedItems.Count==1)
                {
                    ClientsSet clientSet = listViewClient.SelectedItems[0].Tag as ClientsSet;
                    Program.wftDb.ClientsSet.Remove(clientSet);
                    Program.wftDb.SaveChanges();
                    ShowClient();
                }
                textBoxFirstName.Text = "";
                textBoxMiddleName.Text = "";
                textBoxLastName.Text = "";
                textBoxPhone.Text = "";
                textBoxEmail.Text = "";
            }
            catch
            {
                MessageBox.Show("Невозможно удалить, эта запись используется!", "Ошибка!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
