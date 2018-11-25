using System;
using System.Collections.Generic;
using System.Windows.Forms;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;

namespace ReminderWF
{
    public partial class Form1 : Form
    {
        List<Reminder> reminders = new List<Reminder>();

        public Form1()
        {
            InitializeComponent();
            timer1.Start();
           
            BinaryFormatter formatter = new BinaryFormatter();
            using (FileStream fs = new FileStream("Reminder.bin", FileMode.OpenOrCreate))
            {
                reminders = (List<Reminder>)formatter.Deserialize(fs);
                reminders.Sort();
                foreach (Reminder r in reminders)
                {
                    remindListBox.Items.Add(r);
                }
            }
        }

        private void addButton_Click(object sender, EventArgs e)
        {
            Reminder reminder = new Reminder();
            reminder.Name = nameTextBox.Text;
            reminder.Description = descriptionTextBox.Text;
            reminder.Date = datePicker.Value;
            remindListBox.Items.Add(reminder);
            reminders.Add(reminder);
            reminders.Sort();
            nameTextBox.Clear();
            descriptionTextBox.Clear();
            datePicker.Value = DateTime.Now;
        }

        private void saveButton_Click(object sender, EventArgs e)
        {
           if(remindListBox.SelectedIndex != -1)
            {
                reminders[remindListBox.SelectedIndex].Name = nameTextBox.Text;
                reminders[remindListBox.SelectedIndex].Description = descriptionTextBox.Text;
                reminders[remindListBox.SelectedIndex].Date = datePicker.Value;
                reminders.Sort();
            }
           
            remindListBox.ClearSelected();
            remindListBox.Items.Clear();
            foreach (Reminder r in reminders)
            {
                remindListBox.Items.Add(r);
            }
          
        }

        private void remindListBox_SelectedIndexChanged(object sender, EventArgs e)
        {
                    
            if(remindListBox.SelectedIndex != -1)
            {
                editButton.Enabled = true;
                removeButton.Enabled = true;
                addButton.Enabled = false;
                nameTextBox.ReadOnly = true;
                descriptionTextBox.ReadOnly = true;
                datePicker.Enabled = false;
                nameTextBox.Text = reminders[remindListBox.SelectedIndex].Name;
                descriptionTextBox.Text = reminders[remindListBox.SelectedIndex].Description;
                datePicker.Value = reminders[remindListBox.SelectedIndex].Date;
            }
            else
            {
                editButton.Enabled = false;
                removeButton.Enabled = false;
                addButton.Enabled = true;
                nameTextBox.Clear();
                nameTextBox.ReadOnly = false;
                descriptionTextBox.Clear();
                descriptionTextBox.ReadOnly = false;
                datePicker.Enabled = true;
                datePicker.Value = DateTime.Now;
            }
            
        }

        private void editButton_Click(object sender, EventArgs e)
        {
            nameTextBox.ReadOnly = false;
            descriptionTextBox.ReadOnly = false;
            datePicker.Enabled = true;
        }

        private void removeButton_Click(object sender, EventArgs e)
        {
           if(MessageBox.Show("Удалить напоминание?",  "Сообщение", 
        MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                reminders.RemoveAt(remindListBox.SelectedIndex);
                remindListBox.Items.RemoveAt(remindListBox.SelectedIndex);
            }
            reminders.Sort();
            remindListBox.ClearSelected();
        }

        private void timePicker_ValueChanged(object sender, EventArgs e)
        {

        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            Predicate<Reminder> removedRemind = (Reminder r) => { return r.Date <= DateTime.Now; };
            List<Reminder> rem = reminders.FindAll(removedRemind);
            foreach (Reminder r in rem)
            {
                MessageBox.Show(r.Name);
                remindListBox.Items.Remove(r);
            }                   
            reminders.RemoveAll(removedRemind);
            
            reminders.Sort();
        }

        private void remindListBox_DoubleClick(object sender, EventArgs e)
        {
            remindListBox.ClearSelected();      
           
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            BinaryFormatter formatter = new BinaryFormatter();

            using (FileStream fs = new FileStream("Reminder.bin", FileMode.OpenOrCreate))
            {
                formatter.Serialize(fs, reminders);

            }
        }

        private void cancelButton_Click(object sender, EventArgs e)
        {
            BinaryFormatter formatter = new BinaryFormatter();
            using (FileStream fs = new FileStream("Reminder.bin", FileMode.OpenOrCreate))
            {
                reminders = (List<Reminder>)formatter.Deserialize(fs);
                reminders.Sort();
                remindListBox.Items.Clear();
                foreach (Reminder r in reminders)
                {
                    remindListBox.Items.Add(r);
                }
            }
            remindListBox.ClearSelected();
            nameTextBox.Enabled = true;
            nameTextBox.Clear();
            descriptionTextBox.Enabled = true;
            descriptionTextBox.Clear();
            datePicker.Value = DateTime.Now;
            datePicker.Enabled = true;
            addButton.Enabled = true;
            editButton.Enabled = false;
            removeButton.Enabled = false;
        }

        private void remindListBox_MeasureItem(object sender, MeasureItemEventArgs e)
        {
           
        }
       
    }
}
