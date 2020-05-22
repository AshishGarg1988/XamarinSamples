using Android.App;
using Android.Content;
using Android.OS;
using System;
using System.Threading;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace ForeGroundService.Droid
{
    [Service]
    public class ForegroundService : Service
    {
        CancellationTokenSource _cts;
        private bool isRunning = false;

        public override IBinder OnBind(Intent intent)
        {
            return null;
        }

        public override StartCommandResult OnStartCommand(Intent intent, StartCommandFlags flags, int startId)
        {
            var notification = new Notification.Builder(this)
       .SetContentTitle("app_name")
       .SetContentText("notification_text")
       .SetSmallIcon(Resource.Mipmap.icon)
       .SetOngoing(true)
       .Build();


            _cts = new CancellationTokenSource();
            isRunning = true;
            try
            {
                var taskCounter = new TaskCounter();
                Device.StartTimer(TimeSpan.FromSeconds(1), () =>
                {

                    Task.Run(async () =>
                    {
                        taskCounter.RunCounterAsync();
                    });
                    return isRunning;
                });
            }
            catch (System.OperationCanceledException)
            {
                isRunning = false;
            }
            finally
            {
                if (_cts.IsCancellationRequested)
                {
                    isRunning = false;
                    Device.BeginInvokeOnMainThread(
                        () => MessagingCenter.Send("Some Error Occur Please Try Again", "CancelledMessage")
                    );
                }
            }
            StartForeground(1001, notification);
            return StartCommandResult.Sticky;
        }

        public override void OnDestroy()
        {

            if (_cts != null)
            {
                isRunning = false;
                _cts.Token.ThrowIfCancellationRequested();

                _cts.Cancel();
            }
            base.OnDestroy();
        }
    }
}