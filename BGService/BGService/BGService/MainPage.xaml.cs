using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace BGService
{
    // Learn more about making custom code visible in the Xamarin.Forms previewer
    // by visiting https://aka.ms/xamarinforms-previewer
    [DesignTimeVisible(false)]
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
            StartbackgroundService.Clicked += (s, e) => {
                //var message = new StartLongRunningTaskMessage();
                MessagingCenter.Send("Start", "BackgroundService");
            };

            stopbackgroundService.Clicked += (s, e) => {
                //var message = new StopLongRunningTaskMessage();
                MessagingCenter.Send("Stop", "BackgroundService");
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
