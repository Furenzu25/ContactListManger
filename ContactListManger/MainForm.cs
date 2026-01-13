using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows.Forms;

namespace ContactListManger
{
    public partial class MainForm : Form
    {
        private List<Contact> contacts = new List<Contact>();

        public MainForm()
        {
            InitializeComponent();
            LoadContactsFromCSV();
        }

        private void LoadContactsFromCSV()
        {
            if (!File.Exists("contacts.csv"))
            {
                File.WriteAllText("contacts.csv", "Name,Email,PhoneNumber");
            }

            string[] lines = File.ReadAllLines("contacts.csv");
            foreach (string line in lines.Skip(1)) // Skip header
            {
                var parts = line.Split(',');
                contacts.Add(new Contact
                {
                    Name = parts[0],
                    Email = parts[1],
                    PhoneNumber = parts[2]
                });
            }

            UpdateContactGrid();
        }

        private void UpdateContactGrid()
        {
            dgvContacts.DataSource = null;
            dgvContacts.DataSource = contacts;
        }

        private void SaveContactsToCSV()
        {
            var lines = new List<string> { "Name,Email,PhoneNumber" };
            foreach (var contact in contacts)
            {
                lines.Add($"{contact.Name},{contact.Email},{contact.PhoneNumber}");
            }
            File.WriteAllLines("contacts.csv", lines);
            MessageBox.Show("Contacts saved successfully!");
        }

        private void btnAddContact_Click(object sender, EventArgs e)
        {
            ContactPopup popup = new ContactPopup();
            if (popup.ShowDialog() == DialogResult.OK)
            {
                contacts.Add(popup.Contact);
                UpdateContactGrid();
            }
        }

        private void btnEditContact_Click(object sender, EventArgs e)
        {
            if (dgvContacts.SelectedRows.Count == 0)
            {
                MessageBox.Show("Please select a contact to edit.");
                return;
            }
            var selectedContact = (Contact)dgvContacts.SelectedRows[0].DataBoundItem;

            ContactPopup popup = new ContactPopup(selectedContact);
            if (popup.ShowDialog() == DialogResult.OK)
            {
                selectedContact.Name = popup.Contact.Name;
                selectedContact.Email = popup.Contact.Email;
                selectedContact.PhoneNumber = popup.Contact.PhoneNumber;
                UpdateContactGrid();
            }
        }

        private void btnSaveToCSV_Click(object sender, EventArgs e)
        {
            SaveContactsToCSV();
        }
    }
}
