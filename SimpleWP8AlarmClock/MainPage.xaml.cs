using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
using SimpleWP8AlarmClock.Resources;
using Microsoft.Phone.Scheduler;

namespace SimpleWP8AlarmClock
{
    public partial class MainPage : PhoneApplicationPage
    {
        // Constants
        const String ALARM_NAME = "Simple WP8 Alarm Clock";
        const String ALARM_CONTENT = "Simple WP8 Alarm Clock";

        // Constructor
        public MainPage()
        {
            InitializeComponent();

            updateUI();
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            updateUI();
        }

        private void updateUI()
        {
            Alarm existingAlarm = (Alarm) ScheduledActionService.Find(ALARM_NAME);

            if (existingAlarm != null && existingAlarm.BeginTime > DateTime.Now)
            {
                DateTime beginTime = existingAlarm.BeginTime;
                String alarmInfo = String.Format("Alarm will sound at {0:t}.", beginTime);

                alarmInfo_textBlock.Text = alarmInfo;
                alarmInfo_panel.Visibility = Visibility.Visible;
                setAlarm_panel.Visibility = Visibility.Collapsed;
                ((ApplicationBarIconButton)ApplicationBar.Buttons[0]).IsEnabled = false;
                ((ApplicationBarIconButton)ApplicationBar.Buttons[1]).IsEnabled = true;
            }
            else
            {
                if (existingAlarm != null)
                {
                    alarmTimePicker.Value = existingAlarm.BeginTime;
                }
                alarmInfo_panel.Visibility = Visibility.Collapsed;
                setAlarm_panel.Visibility = Visibility.Visible;
                ((ApplicationBarIconButton)ApplicationBar.Buttons[0]).IsEnabled = true;
                ((ApplicationBarIconButton)ApplicationBar.Buttons[1]).IsEnabled = false;
            }
        }

        private void ApplicationBarDeleteButton_Click(object sender, EventArgs e)
        {           
            Alarm existingAlarm = (Alarm) ScheduledActionService.Find(ALARM_NAME);

            if (existingAlarm != null)
            {
                ScheduledActionService.Remove(ALARM_NAME);
            }
            updateUI();
        }

        private void ApplicationBarSaveButton_Click(object sender, EventArgs e)
        {
            DateTime beginTime = (DateTime)alarmTimePicker.Value;

            if (beginTime < DateTime.Now)
                beginTime = beginTime.AddDays(1);
            DateTime expirationTime = beginTime.AddSeconds(1);

            RecurrenceInterval recurrence = RecurrenceInterval.None;

            Alarm alarm = new Alarm(ALARM_NAME);
            alarm.Content = ALARM_CONTENT;
            alarm.Sound = new Uri("/Assets/Sounds/Alarm-06.wma", UriKind.Relative);
            alarm.BeginTime = beginTime;
            alarm.ExpirationTime = expirationTime;
            alarm.RecurrenceType = recurrence;

            if (ScheduledActionService.Find(ALARM_NAME) != null)
            {
                ScheduledActionService.Remove(ALARM_NAME);
            }
            ScheduledActionService.Add(alarm);

            updateUI();
        }

        private void alarmInfo_panel_Tap(object sender, System.Windows.Input.GestureEventArgs e)
        {
            Alarm existingAlarm = (Alarm) ScheduledActionService.Find(ALARM_NAME);

            if (existingAlarm != null)
            {
                String alarmInfo;
                TimeSpan remainingTime = existingAlarm.BeginTime.Subtract(DateTime.Now);

                if (remainingTime.Hours > 0)
                {
                    alarmInfo = String.Format("Alarm will sound in {0} hours and {1} minutes.", remainingTime.Hours, remainingTime.Minutes);
                }
                else if (remainingTime.Minutes > 0)
                {
                    alarmInfo = String.Format("Alarm will sound in {0} minutes.", remainingTime.Minutes);
                }
                else
                {
                    alarmInfo = "Alarm will sound in less than one minute.";
                }

                MessageBox.Show(alarmInfo);
            }
        }
    }
}