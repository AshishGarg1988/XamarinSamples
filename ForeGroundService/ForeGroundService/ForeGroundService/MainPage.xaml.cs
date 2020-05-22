using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace ForeGroundService
{
    // Learn more about making custom code visible in the Xamarin.Forms previewer
    // by visiting https://aka.ms/xamarinforms-previewer
    [DesignTimeVisible(false)]
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
            StartService.Clicked += (s, e) => {
                //var message = new StartLongRunningTaskMessage();
                MessagingCenter.Send("Start", "ForegroundService");
            };

            stopService.Clicked += (s, e) => {
                //var message = new StopLongRunningTaskMessage();
                MessagingCenter.Send("Stop", "ForegroundService");
            };

            HandleReceivedMessages();
        }

        void HandleReceivedMessages()
        {
            MessagingCenter.Subscribe<CounterMessage>(this, "TickedMessage", message => {
                Device.BeginInvokeOnMainThread(() => {
                    if (message.Message != null)
                    {
                        counter.Text = message.Message.ToString();
                    }
                    else
                    {
                        counter.Text = "No GPS";
                    }

                });
            });

            MessagingCenter.Subscribe<string>(this, "CancelledMessage", message => {
                Device.BeginInvokeOnMainThread(() => {
                    counter.Text = message;// "Cancelled";
                });
            });
        }
    }
}
