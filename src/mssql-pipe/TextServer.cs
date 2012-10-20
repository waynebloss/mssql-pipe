using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace mssql_pipe
{
	class TextServer
	{
		readonly TextReader _in;
		readonly TextWriter _out;
		readonly TextWriter _err;

		public TextServer(TextReader textIn, TextWriter textOut) : this(textIn, textOut, textOut) { }

		public TextServer(TextReader textIn, TextWriter textOut, TextWriter textErr)
		{
			_in = textIn;
			_out = textOut;
			_err = textErr;
		}

		public event EventHandler<TextEventArgs> Received;

		void OnError(string text)
		{
			_err.Write(
				text.Length.ToString() +
				_out.NewLine +
				text);
		}

		void OnReceive(string text)
		{
			var handler = this.Received;
			if (handler != null)
				handler(this, new TextEventArgs(text));
		}

		public void Start()
		{
			try
			{
				string header = null;
				while ((header = _in.ReadLine()) != null)
				{
					if (header.Length == 0) continue;
					int lenBlock;
					if (!int.TryParse(header, out lenBlock))
					{
						throw new InvalidDataException("InvalidHeader: " + header);
					}
					var block = new char[lenBlock];
					int lenRead = _in.ReadBlock(block, 0, lenBlock);
					OnReceive(new String(block));
				}
			}
			catch (Exception ex)
			{
				OnError(ex.Message + "\n" + ex.StackTrace);
			}
		}

		public void Send(string text)
		{
			_out.Write(
				text.Length.ToString() +
				_out.NewLine +
				text);
		}

		public void SendError(string text)
		{
			OnError(text);
		}
	}

	class TextEventArgs : EventArgs
	{
		public TextEventArgs(string text)
		{
			_Text = text;
		}

		readonly string _Text;
		public string Text { get { return _Text; } }
	}
}
