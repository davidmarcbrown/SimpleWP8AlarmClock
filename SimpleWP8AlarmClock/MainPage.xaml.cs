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

            if (existingAlarm != null && existingAlarm.IsEnabled == true)
            {
                alarmInfo_panel.Visibility = Visibility.Visible;
                setAlarm_panel.Visibility = Visibility.Collapsed;
                ((ApplicationBarIconButton)ApplicationBar.Buttons[0]).IsEnabled = false;
                ((ApplicationBarIconButton)ApplicationBar.Buttons[1]).IsEnabled = true;
            }
            else
            {
                alarmInfo_panel.Visibility = Visibility.Collapsed;
                setAlarm_panel.Visibility = Visibility.Visible;
                ((ApplicationBarIconButton)ApplicationBar.Buttons[0]).IsEnabled = true;
                ((ApplicationBarIconButton)ApplicationBar.Buttons[1]).IsEnabled = false;
            }
        }

        private void ApplicationBarDeleteButton_Click(object sender, EventArgs e)
        {

        }

        private void ApplicationBarSaveButton_Click(object sender, EventArgs e)
        {

        }

        private void alarmTimePicker_ValueChanged(object sender, DateTimeValueChangedEventArgs e)
        {

        }
    }
}