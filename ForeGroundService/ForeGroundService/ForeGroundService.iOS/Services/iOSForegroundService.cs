using System;
using System.Threading;
using System.Threading.Tasks;
using UIKit;
using Xamarin.Forms;

namespace ForeGroundService.iOS
{
    internal class iOSForegroundService
    {
		nint _taskId;
		CancellationTokenSource _cts;
		private bool isRunning = false;
		public async Task Start()
		{
			_cts = new CancellationTokenSource();

			_taskId = UIApplication.SharedApplication.BeginBackgroundTask("StartLongRunningTaskMessage", OnExpiration);
			isRunning = true;
			try
			{
				var counter = new TaskCounter();
				Device.StartTimer(TimeSpan.FromSeconds(1), () =>
				{
					Task.Run(async () =>
					{
						await counter.RunCounterAsync();
					});
					return isRunning;
				});
			}
			catch (OperationCanceledException)
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

			UIApplication.SharedApplication.EndBackgroundTask(_taskId);
		}

		public void Stop()
		{
			isRunning = false;
			_cts.Cancel();
		}

		void OnExpiration()
		{
			isRunning = false;
			_cts.Cancel();
		}
	}
}