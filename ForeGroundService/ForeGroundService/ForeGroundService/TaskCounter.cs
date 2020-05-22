using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace ForeGroundService
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
