using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Media;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Alarm_Clock
{
    public partial class alarmClock : Form
    {
        Time timeSet = new Time();
        SoundPlayer player = new SoundPlayer();

        bool alarmOnce = false;
        bool alarmSet = false;
        bool snooze = false;
        bool snoozeEnd = false;
        bool soundAlarm = false;

        bool checkOnce = false;

        bool timer5Min = false;

        int count = 0;
        int temp = 0;

        public alarmClock()
        {
            InitializeComponent();
        }

        private void alarmClock_Load(object sender, EventArgs e)
        {
            timer1.Start();
            player.SoundLocation = @"..\..\bin\Debug\Alarm-sound-effect.wav";
            lblAlarmTime.Text = "";
            btnSnooze.Enabled = false;

            btnOne.Enabled = false;
            btnTwo.Enabled = false;
            btnThree.Enabled = false;
            btnFour.Enabled = false;
            btnFive.Enabled = false;
            btnSix.Enabled = false;
            btnSeven.Enabled = false;
            btnEight.Enabled = false;
            btnNine.Enabled = false;
            btnZero.Enabled = false;
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            DateTime dateTime = DateTime.Now;
            this.lblCurrentTime.Text = dateTime.ToString("H:mm:ss tt").ToUpper();

            if (alarmSet == true)
            {
                if(!snooze && dateTime.Hour == timeSet.getSetHours && dateTime.Minute == timeSet.getSetMinutes)
                {
                    player.PlayLooping();

                    BackColor = Color.Red;
                    soundAlarm = true;
                    btnSnooze.Enabled = true;
                }
            }

            if (snoozeEnd)
            {
                player.PlayLooping();
            }
        }

        private void timer2_Tick(object sender, EventArgs e)
        {
            count++;

            if (count == 10)
            {
                timer2.Stop();
                BackColor = Color.Red;
                count = 0;
                btnSnooze.Enabled = true;
                snoozeEnd = true;

                btnSetAlarm.Text = "Stop Alarm";
            }
        }

        private void btnSetAlarm_Click(object sender, EventArgs e)
        {
            
            if(alarmOnce == false)
            {
                alarmOnce = true;

                btnOne.Enabled = true;
                btnTwo.Enabled = true;
                btnThree.Enabled = true;
                btnFour.Enabled = true;
                btnFive.Enabled = true;
                btnSix.Enabled = true;
                btnSeven.Enabled = true;
                btnEight.Enabled = true;
                btnNine.Enabled = true;
                btnZero.Enabled = true;

                btnXMinutes.Enabled = false;
                btnSnooze.Enabled = true;

                btnSetAlarm.Text = "Start";
                btnSnooze.Text = "Erase Alarm";

                MessageBox.Show("Please set the alarm using the number pad.");
            }
            else
            {
                btnXMinutes.Enabled = false;
                if (lblAlarmTime.Text.Length == 5)
                {
                    string[] values = lblAlarmTime.Text.Split(new char[] { ':' });
                    timeSet.getSetHours = int.Parse(values[0]);
                    timeSet.getSetMinutes = int.Parse(values[1]);

                    alarmSet = true;

                    btnSetAlarm.Enabled = false;
                    btnSnooze.Enabled = false;
                    btnSnooze.Text = "Snooze";

                    btnOne.Enabled = false;
                    btnTwo.Enabled = false;
                    btnThree.Enabled = false;
                    btnFour.Enabled = false;
                    btnFive.Enabled = false;
                    btnSix.Enabled = false;
                    btnSeven.Enabled = false;
                    btnEight.Enabled = false;
                    btnNine.Enabled = false;
                    btnZero.Enabled = false;
                }
                else
                {
                    btnXMinutes.Enabled = true;
                    MessageBox.Show("Please set the alarm using the number pad.");
                }
            }
        }

        private void btnSnooze_Click(object sender, EventArgs e)
        {
            if(btnSnooze.Text == "Erase Alarm")
            {
                lblAlarmTime.Text = "";
            }
            if (soundAlarm == true && btnSnooze.Text != "Reset")
            {
                BackColor = Color.Yellow;
                btnSnooze.Enabled = false;
                snooze = true;
                snoozeEnd = false;
                player.Stop();

                timer2.Start();
            }

            if (btnSnooze.Text == "Reset")
            {
                count = temp - 10;
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            lblAlarmTime.Text = "";
            timeSet.getSetHours = 0;
            timeSet.getSetMinutes = 0;

            count = 0;

            alarmOnce = false;
            alarmSet = false;
            snooze = false;
            soundAlarm = false;
            snoozeEnd = false;
            checkOnce = false;

            btnXMinutes.Enabled = true;
            btnSetAlarm.Enabled = true;
            btnSetAlarm.Text = "Set Alarm";
            btnSnooze.Text = "Snooze";

            btnOne.Enabled = false;
            btnTwo.Enabled = false;
            btnThree.Enabled = false;
            btnFour.Enabled = false;
            btnFive.Enabled = false;
            btnSix.Enabled = false;
            btnSeven.Enabled = false;
            btnEight.Enabled = false;
            btnNine.Enabled = false;
            btnZero.Enabled = false;

            player.Stop();

            BackColor = Color.Green;
            timer2.Stop();

            btnSnooze.Enabled = false;
            btnXMinutes.Enabled = true;

            if (timer5Min)
            {
                timer5Min = false;
                btnSetAlarm.Enabled = true;
                lblAlarmTime.Text = "";
                btnSnooze.Text = "Snooze";
                count = 0;
                timer3.Stop();
            }
        }

        private void btnXMinutes_Click(object sender, EventArgs e)
        {
            using (editTimerBox form2 = new editTimerBox())
            {
                if (form2.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                {
                    if(Int32.Parse(form2.editMins) > 0 && Int32.Parse(form2.editMins) <= 60)
                    {
                        count = Int32.Parse(form2.editMins) * 60 - 10;
                        temp = count + 10;

                        timer5Min = true;
                        btnSetAlarm.Enabled = false;
                        btnSnooze.Enabled = true;
                        btnXMinutes.Enabled = false;
                        btnSnooze.Text = "Reset";
                        lblAlarm.Text = "Timer Time:";

                        btnOne.Enabled = false;
                        btnTwo.Enabled = false;
                        btnThree.Enabled = false;
                        btnFour.Enabled = false;
                        btnFive.Enabled = false;
                        btnSix.Enabled = false;
                        btnSeven.Enabled = false;
                        btnEight.Enabled = false;
                        btnNine.Enabled = false;
                        btnZero.Enabled = false;

                        // Example: 5 minutes = 300 seconds, - 10 seconds.
                        timer3.Start();
                    }
                    else
                    {
                        MessageBox.Show("Please enter a value between 1 and 60.");
                    }
                }
                else
                {
                    // Do nothing...
                }
            }
        }

        private void timer3_Tick(object sender, EventArgs e)
        {
            count--;
            lblAlarmTime.Text = count / 60 + ":" + ((count % 60) >= 10 ? (count % 60).ToString() : "0" + (count % 60));
            if (count == 0)
            {
                player.PlayLooping();
                BackColor = Color.Red;
                checkOnce = true;
                count = temp - 10;
            }
            if (checkOnce && count == temp - 15)
            {
                player.Stop();
                BackColor = Color.Green;
            }
        }

        private void btnOne_Click(object sender, EventArgs e)
        {
            if (lblAlarmTime.Text.Length < 2)
            {
                if (lblAlarmTime.Text.Length == 1)
                {
                    lblAlarmTime.Text += "1";
                    lblAlarmTime.Text += ":";
                }
                else
                    lblAlarmTime.Text += "1";

            }
            else if (lblAlarmTime.Text.Length > 2 && lblAlarmTime.Text.Length < 5)
            {
                lblAlarmTime.Text += "1";
            }
        }

        private void btnTwo_Click(object sender, EventArgs e)
        {
            if (lblAlarmTime.Text.Length < 2)
            {
                if (lblAlarmTime.Text.Length == 1)
                {
                    lblAlarmTime.Text += "2";
                    lblAlarmTime.Text += ":";
                }
                else
                    lblAlarmTime.Text += "2";

            }
            else if (lblAlarmTime.Text.Length > 2 && lblAlarmTime.Text.Length < 5)
            {
                lblAlarmTime.Text += "2";
            }
        }

        private void btnThree_Click(object sender, EventArgs e)
        {
            if (lblAlarmTime.Text.Length == 1)
            {
                lblAlarmTime.Text += "3";
                lblAlarmTime.Text += ":";
            }
            else if (lblAlarmTime.Text.Length > 2 && lblAlarmTime.Text.Length < 5)
            {
                lblAlarmTime.Text += "3";
            }
        }

        private void btnFour_Click(object sender, EventArgs e)
        {
            if (lblAlarmTime.Text.Length == 1)
            {
                lblAlarmTime.Text += "4";
                lblAlarmTime.Text += ":";
            }
            else if (lblAlarmTime.Text.Length > 2 && lblAlarmTime.Text.Length < 5)
            {
                lblAlarmTime.Text += "4";
            }
        }

        private void btnFive_Click(object sender, EventArgs e)
        {
            if (lblAlarmTime.Text.Length == 1)
            {
                lblAlarmTime.Text += "5";
                lblAlarmTime.Text += ":";
            }
            else if (lblAlarmTime.Text.Length > 2 && lblAlarmTime.Text.Length < 5)
            {
                lblAlarmTime.Text += "5";
            }
        }

        private void btnSix_Click(object sender, EventArgs e)
        {
            if(lblAlarm.Text != "2")
            {
                if (lblAlarmTime.Text.Length == 1)
                {
                    lblAlarmTime.Text += "6";
                    lblAlarmTime.Text += ":";
                }
                else if (lblAlarmTime.Text.Length == 4)
                {
                    lblAlarmTime.Text += "6";
                }
            }
        }

        private void btnSeven_Click(object sender, EventArgs e)
        {
            if (lblAlarmTime.Text.Length == 1)
            {
                lblAlarmTime.Text += "7";
                lblAlarmTime.Text += ":";
            }
            else if (lblAlarmTime.Text.Length == 4)
            {
                lblAlarmTime.Text += "7";
            }
        }

        private void btnEight_Click(object sender, EventArgs e)
        {
            if (lblAlarmTime.Text.Length == 1)
            {
                lblAlarmTime.Text += "8";
                lblAlarmTime.Text += ":";
            }
            else if (lblAlarmTime.Text.Length == 4)
            {
                lblAlarmTime.Text += "8";
            }
        }

        private void btnNine_Click(object sender, EventArgs e)
        {
            if (lblAlarmTime.Text.Length == 1)
            {
                lblAlarmTime.Text += "9";
                lblAlarmTime.Text += ":";
            }
            else if (lblAlarmTime.Text.Length == 4)
            {
                lblAlarmTime.Text += "9";
            }
        }

        private void btnZero_Click(object sender, EventArgs e)
        {
            if (lblAlarmTime.Text.Length < 1)
            {
                // Do nothing...
            }

            else if (lblAlarmTime.Text.Length < 2)
            {
                if (lblAlarmTime.Text.Length == 1)
                {
                    lblAlarmTime.Text += "0";
                    lblAlarmTime.Text += ":";
                }
                else
                    lblAlarmTime.Text += "0";

            }

            else if (lblAlarmTime.Text.Length > 2 && lblAlarmTime.Text.Length < 5)
            {
                lblAlarmTime.Text += "0";
            }
        }
    }
}
