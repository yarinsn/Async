using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Async
{
	internal class Program
	{
		private static void Main()
		{
			//Ex01();
			//Ex02();

			DoAsyncStuff(() => CountToAsync(1, 5));
			DoAsyncStuff(() => CountToAsync(2, 5));
		}

		private static void DoAsyncStuff(Func<Task> asyncMethod)
		{
			var task = asyncMethod();

			var index = PrintWaiting();
			while (!task.IsCompleted)
			{
				SomeMoreWaiting(index++);
			}

			task.Wait();
			GetMail();
			Console.ReadLine();
		}

		public static int PrintWaiting()
		{
			const string waiting = "waiting";
			Console.SetCursorPosition(0, 0);
			Console.Write(waiting);

			return waiting.Length;
		}

		public static void SomeMoreWaiting(int index)
		{
			Thread.Sleep(1000);
			Console.SetCursorPosition(index, 0);
			Console.Write(".");
		}

		public static void GetMail()
		{
			Console.SetCursorPosition(0, 0);
			Console.Clear();
			Console.WriteLine("Got mail!");
		}

		private static async Task CountToAsync(int key, int countTo)
		{
			for (var i = 0; i < countTo; i++)
			{
				await Task.Delay(1000); //for the task not to run sychronously?
				Console.SetCursorPosition(0, key);
				Console.WriteLine("{0}: {1}", key, i + 1);
			}
		}

		//private static async Task CountToFiftyAsync()
		//{
		//	var taskA = CountToAsync("a", 5); //async method start running when you call them
		//	var taskB = CountToAsync("b", 5); //they run sycnhronously until the first await
		//	var taskC = CountToAsync("c", 5); //and then they return a task that represents them
		//	var taskD = CountToAsync("d", 5);
		//	var taskE = CountToAsync("e", 5);

		//	await Task.WhenAll(taskA, taskB, taskC, taskD, taskE);
		//}

		//private static async Task CountToFiftyAsync()
		//{
		//	await CountToAsync(1, 5);
		//}
	}
}
