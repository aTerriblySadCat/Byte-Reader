using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Byte_Reader
{
	internal class BlockPrinter
	{
		public static void Print1(byte[] readBlock)
		{
			int lineWidth = 0;
			int consoleWidth = Console.WindowWidth;
			byte lastByte = 0;
			int lastByteCount = 0;
			for (int i = 0; i < readBlock.Length; i++)
			{
				byte b = readBlock[i];
				if (b == lastByte)
				{
					lastByteCount += 1;
				}

				if (b != lastByte || i == readBlock.Length - 1)
				{
					if (lastByteCount > 1)
					{
						int lineLength = lastByteCount.ToString().Length + 7;
						if (lineWidth + lineLength > consoleWidth)
						{
							Console.WriteLine();
							lineWidth = 0;
						}

						Console.Write(lastByteCount + "{" + String.Format(@"\x{0:x2}", lastByte) + "} ");
						lineWidth += lineLength;
					}

					if (b != lastByte)
					{
						lastByte = b;
						lastByteCount = 1;

						if (lineWidth + 7 > consoleWidth)
						{
							Console.WriteLine();
							lineWidth = 0;
						}

						Console.Write("{" + String.Format(@"\x{0:x2}", b) + "} ");
						lineWidth += 7;
					}
				}
			}
		}

		public static void Print2(byte[] readBlock)
		{
			int lineWidth = 0;
			int consoleWidth = Console.WindowWidth;
			for (int i = 0; i < readBlock.Length; i++)
			{
				byte b = readBlock[i];
				if (lineWidth + 7 > consoleWidth)
				{
					Console.WriteLine();
					lineWidth = 0;
				}

				Console.Write("{" + String.Format(@"\x{0:x2}", b) + "} ");
				lineWidth += 7;
			}
		}

		public static void Print3(byte[] readBlock)
		{
			int lineWidth = 0;
			int consoleWidth = Console.WindowWidth;
			for (int i = 0; i < readBlock.Length; i++)
			{
				byte b = readBlock[i];
				if (lineWidth + 5 > consoleWidth)
				{
					Console.WriteLine();
					lineWidth = 0;
				}

				Console.Write(String.Format(@"0x{0:x2}", b) + " ");
				lineWidth += 5;
			}
		}

		public static void Print4(byte[] readBlock)
		{
			int lineWidth = 0;
			int consoleWidth = Console.WindowWidth;
			for (int i = 0; i < readBlock.Length; i++)
			{
				byte b = readBlock[i];
				if (lineWidth + 3 > consoleWidth)
				{
					Console.WriteLine();
					lineWidth = 0;
				}

				Console.Write(String.Format(@"{0:x2}", b) + " ");
				lineWidth += 3;
			}
		}
	}
}
