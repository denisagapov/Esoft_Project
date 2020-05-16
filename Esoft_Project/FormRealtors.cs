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
    public partial class FormRealtors : Form
    {
        public FormRealtors()
        {
            InitializeComponent();
            ShowRealtor();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void textBoxFirstName_TextChanged(object sender, EventArgs e)
        {

        }




        private void buttonEdit_Click(object sender, EventArgs e)
        {
            if (listViewRealtor.SelectedItems.Count == 1)
            {
                RealtorsSet RealtorSet = listViewRealtor.SelectedItems[0].Tag as RealtorsSet;
                RealtorSet.FirstName = textBoxFirstName.Text;
                RealtorSet.MiddleName = textBoxMiddleName.Text;
                RealtorSet.LastName = textBoxLastName.Text;
                textBoxDealShare.Text = string.Empty;
               if (textBoxDealShare.Text!= "") { RealtorSet.DealShare = Convert.ToInt32(textBoxDealShare.Text); }
                Program.wftDb.SaveChanges();
                ShowRealtor();
            }
        }

        private void listViewRealtor_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (listViewRealtor.SelectedItems.Count == 1)
            {
                RealtorsSet RealtorSet = listViewRealtor.SelectedItems[0].Tag as RealtorsSet;
                RealtorSet.FirstName = textBoxFirstName.Text;
                RealtorSet.MiddleName = textBoxMiddleName.Text;
                RealtorSet.LastName = textBoxLastName.Text;
                if (textBoxDealShare.Text!= "") { RealtorSet.DealShare = Convert.ToInt32(textBoxDealShare.Text); }
            }
            else
            {
                textBoxFirstName.Text = "";
                textBoxMiddleName.Text = "";
                textBoxLastName.Text = "";
                textBoxDealShare.Text = "";
            }
        }
        void ShowRealtor()
        {
            //предварительно очищаем lastView
            listViewRealtor.Items.Clear();
            //проходимся по коллекции клиентов, которые находятся в базе с помощью foreach
            foreach (RealtorsSet RealtorsSet in Program.wftDb.RealtorsSet)
            {
                //создаем новый элемент в listView
                //для этого создаем новый массив строк
                ListViewItem item = new ListViewItem(new string[]
                {
                    RealtorsSet.Id.ToString(), RealtorsSet.FirstName, RealtorsSet.MiddleName,
                    RealtorsSet.LastName, RealtorsSet.DealShare.ToString()
                }) ;
                //указываем по какому тегу будем брать элементы
                item.Tag = RealtorsSet;
                //добавляем элементы в listView для отображения
                listViewRealtor.Items.Add(item);
            }
            //выравниваем колонки 
            listViewRealtor.AutoResizeColumns(ColumnHeaderAutoResizeStyle.HeaderSize);
        }

        private void buttonAdd_Click(object sender, EventArgs e)
        {
            try
            {
                RealtorsSet RealtorSet = new RealtorsSet();
                //делаем ссылку на объект, который хранится в textBox-ax
                RealtorSet.FirstName = textBoxFirstName.Text;
                RealtorSet.MiddleName = textBoxMiddleName.Text;
                RealtorSet.LastName = textBoxLastName.Text;
                if (textBoxDealShare.Text != "") { RealtorSet.DealShare = Convert.ToInt32(textBoxDealShare.Text); }
                if ((RealtorSet.DealShare < 0) || (RealtorSet.DealShare > 100))
                {
                    throw new Exception("Доля должна находиться в диапазоне от 0 до 100");
                }
                Program.wftDb.RealtorsSet.Add(RealtorSet);
                //сохраняем изменения в модели wftDb
                Program.wftDb.SaveChanges();
                ShowRealtor();
            }
            catch (Exception expection)
            {
                MessageBox.Show(expection.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void buttonDel_Click(object sender, EventArgs e)
        {
            try
            {
                if (listViewRealtor.SelectedItems.Count == 1)
                {
                    RealtorsSet RealtorSet = listViewRealtor.SelectedItems[0].Tag as RealtorsSet;
                    Program.wftDb.RealtorsSet.Remove(RealtorSet);
                    Program.wftDb.SaveChanges();
                    ShowRealtor();
                }
                textBoxFirstName.Text = "";
                textBoxMiddleName.Text = "";
                textBoxLastName.Text = "";
                textBoxDealShare.Text = "";
            }
            catch
            {
                MessageBox.Show("Невозможно удалить, эта запись используется!", "Ошибка!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void textBoxDealShare_KeyPress(object sender, KeyPressEventArgs e)
        {
            char number = e.KeyChar;
            if (!Char.IsDigit(number) && number != 8 && number != 45)
            {
                e.Handled = true;
            }
        }
    }
}
