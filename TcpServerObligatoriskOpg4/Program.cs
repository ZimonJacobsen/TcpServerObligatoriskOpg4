// See https://aka.ms/new-console-template for more information
using System;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Reflection.PortableExecutable;
using static System.Runtime.InteropServices.JavaScript.JSType;


Console.WriteLine("TCP Server obligatorisk opgave");

TcpListener listener = new TcpListener(IPAddress.Any, 7);

listener.Start();
while (true)
{
	TcpClient socket = listener.AcceptTcpClient();
	IPEndPoint clientEndPoint = socket.Client.RemoteEndPoint as IPEndPoint;
	Console.WriteLine("Client connected: " + clientEndPoint.Address);

	NetworkStream stream = socket.GetStream();
	StreamReader sr = new StreamReader(stream);
	StreamWriter writer = new StreamWriter(stream);
	while (socket.Connected)
	{
		string? message = sr.ReadLine();
		Console.WriteLine(message);
		writer.WriteLine(message);
		writer.Flush();
		if (message == "Stop")
		{
			writer.WriteLine("Server shutting down");
			writer.Flush();
			socket.Close();
		}

		else if (message == "Random")
		{
			writer.WriteLine("Enter 2 numbers");
			writer.Flush();
			string[] message1 = sr.ReadLine().Split(" ");
			int c = Int32.Parse(message1[0]);
			int d = Int32.Parse(message1[1]);

			Random random = new Random();
			int result1 = random.Next(c, d);
			writer.WriteLine(result1);
			writer.Flush();
		}
		else if (message == "Add")
		{
			writer.WriteLine("Enter two numbers to add");
			writer.Flush();
			string[] message2 = sr.ReadLine().Split(" ");
			int a = Int32.Parse(message2[0]);
			int b = Int32.Parse(message2[1]);
			int result2 = a + b;
			writer.WriteLine(result2);
			writer.Flush();
		}
		else if (message == "Subtract")
			writer.WriteLine("Enter two numbers to subtract");
		writer.Flush();
		string[] message3 = sr.ReadLine().Split(" ");
		int e = Int32.Parse(message3[0]);
		int f = Int32.Parse(message3[1]);
		int result3 = e - f;
		writer.WriteLine(result3);
		writer.Flush();
	}

	socket.Close();
	listener.Stop();
}
