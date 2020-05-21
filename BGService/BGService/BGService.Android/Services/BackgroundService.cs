using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Xamarin.Forms;

namespace BGService.Droid.Services
{
	[Service]
	public class BackgroundService : Service
	{
		CancellationTokenSource _cts;
		private bool isRunning = false;

		public override IBinder OnBind(Intent intent)
		{
			return null;
		}

		public override StartCommandResult OnStartCommand(Intent intent, StartCommandFlags flags, int startId)
		{
			_cts = new CancellationTokenSource();
			isRunning = true;
			try
			{
				var taskCounter = new TaskCounter();
				Device.StartTimer(TimeSpan.FromSeconds(5), () =>
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