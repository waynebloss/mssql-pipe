using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ServiceStack.Text;

namespace mssql_pipe
{
	class Program
	{
		static void Main(string[] args)
		{
			var server = new TextServer(Console.In, Console.Out, Console.Error);
			server.Received += new EventHandler<TextEventArgs>(server_Received);
			server.Start();

			// this works swimmingly:
			//var msg = @"{""connectionString"": ""Hello"", ""commandText"": ""select * from somewhere"", ""type"": ""xml""}";
			//var q = ServiceStack.Text.JsonSerializer.DeserializeFromString<SqlCommandQuery>(msg);
			//Console.ReadLine();
		}

		static void server_Received(object sender, TextEventArgs e)
		{
			
		}
	}
}
