using System;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace ContactListManger
{
    public partial class ContactPopup : Form
    {
        public Contact Contact { get; set; }

        public ContactPopup()
        {
            InitializeComponent();
            Contact = new Contact();
        }

        public ContactPopup(Contact existingContact) : this()
        {
            txtName.Text = existingContact.Name;
            txtEmail.Text = existingContact.Email;
            txtPhoneNumber.Text = existingContact.PhoneNumber;
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            // Validate Name
            if (string.IsNullOrWhiteSpace(txtName.Text))
            {
                MessageBox.Show("Please enter a name.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Validate Email
            if (!IsValidEmail(txtEmail.Text))
            {
                MessageBox.Show("Please enter a valid email address.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Validate Phone Number
            if (!IsValidPhoneNumber(txtPhoneNumber.Text))
            {
                MessageBox.Show("Please enter a valid phone number.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            Contact.Name = txtName.Text;
            Contact.Email = txtEmail.Text;
            Contact.PhoneNumber = txtPhoneNumber.Text;

            DialogResult = DialogResult.OK;
            this.Close();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            this.Close();
        }

        private bool IsValidEmail(string email)
        {
            if (string.IsNullOrWhiteSpace(email))
                return false;

            // Check for valid email format: something@something.something
            string pattern = @"^[^@\s]+@[^@\s]+\.[^@\s]+$";
            return Regex.IsMatch(email, pattern);
        }

        private bool IsValidPhoneNumber(string phoneNumber)
        {
            if (string.IsNullOrWhiteSpace(phoneNumber))
                return false;

            // Allow digits, spaces, dashes, parentheses, and plus sign
            // Must be at least 7 characters (minimum phone number length)
            string pattern = @"^[\d\s\-\(\)\+]+$";
            return Regex.IsMatch(phoneNumber, pattern) && phoneNumber.Length >= 7;
        }
    }
}
