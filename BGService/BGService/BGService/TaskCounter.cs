using System;
using System.Threading;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace BGService
{ 
	public class TaskCounter
    {
		int counter = 0;
		public async Task RunCounterAsync()
		{
			
			Device.BeginInvokeOnMainThread(async () => {
				var message = new CounterMessage
				{
					Message = counter.ToString()

				};
				MessagingCenter.Send<CounterMessage>(message, "TickedMessage");
				counter = counter + 5;
			});
		}
	}
}