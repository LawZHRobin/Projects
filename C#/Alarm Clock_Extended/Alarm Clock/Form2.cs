using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Alarm_Clock
{
    public partial class editTimerBox : Form
    {
        public editTimerBox()
        {
            InitializeComponent();
            btnStart.DialogResult = DialogResult.OK;

            for (int i=1; i<=60; i++)
            {
                cboxEditMinutes.Items.Add("                        " + i);
            }
            cboxEditMinutes.SelectedIndex = 4;
        }

        public String editMins { get; set; }

        private void btnStart_Click(object sender, EventArgs e)
        {
            editMins = cboxEditMinutes.Text;
            this.Close();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void cboxEditMinutes_KeyPress(object sender, KeyPressEventArgs e)
        {
            Char key = e.KeyChar;
            if (!Char.IsDigit(key) && key != 8)
            {
                // 8 is backspace
                e.Handled = true;
                MessageBox.Show("Please enter only numeric integers.");
            }
        }
    }
}
