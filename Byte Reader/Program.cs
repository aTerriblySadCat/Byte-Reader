using Byte_Reader;
using System.Text;

Console.OutputEncoding = Encoding.UTF8;

Console.WriteLine("Please enter the path to the file from where to read:");
string filePath = Console.ReadLine();

Console.WriteLine();
Console.Write("Now enter the amount of bytes per enter: ");
string lineCountStr = Console.ReadLine();
long byteCount = long.Parse(lineCountStr);

Console.WriteLine();
Console.Write("Now enter the amount of bytes to skip: ");
string skipCountStr = Console.ReadLine();
long skipCount = 0;
if (skipCountStr != null && skipCountStr != "")
{
	skipCount = long.Parse(skipCountStr);
}

Console.WriteLine();
Console.WriteLine("And finally, please choose the manner in which the hexadecimal is displayed.");
Console.WriteLine("1. {\\x00} with collapsed repeats.");
Console.WriteLine("2. {\\x00}");
Console.WriteLine("3. 0x00");
Console.WriteLine("4. 00");
string choiceStr = Console.ReadLine();
int choice = int.Parse(choiceStr);

using (Stream s = new FileStream(filePath, FileMode.Open, FileAccess.Read, FileShare.ReadWrite))
{
	s.Position = skipCount;
	long bytesRead = 0;
	long totalBytesRead = skipCount;

	Console.WriteLine();
	Console.WriteLine("Press Enter to read " + lineCountStr + " bytes from the given file.");
	Console.WriteLine("Press Space to display the last shown text in UTF8.");
	Console.WriteLine("Press Escape to quit.");
	Console.WriteLine();

	byte[] lastBlock = new byte[byteCount];
	while (true)
	{
		ConsoleKeyInfo keyInfo = Console.ReadKey();
		if (keyInfo.Key == ConsoleKey.Enter)
		{
			Console.WriteLine();

			long readCount = 0;
			Console.WriteLine(totalBytesRead);
			while (true)
			{
				byte[] readBlock = null;
				if (byteCount > int.MaxValue / sizeof(byte))
				{
					int readLength = int.MaxValue / sizeof(byte);
					if(readCount + readLength > byteCount)
					{
						readLength = (int)(byteCount - readCount);
					}

					readBlock = new byte[int.MaxValue / sizeof(byte)];
					bytesRead = s.Read(readBlock, 0, readLength);
					readCount += bytesRead;
				}
				else
				{
					readBlock = new byte[byteCount];
					bytesRead = s.Read(readBlock, 0, (int)byteCount);
					readCount += bytesRead;
				}

				totalBytesRead += bytesRead;
				if (readBlock != null)
				{
					lastBlock = readBlock;
					switch(choice)
					{
						default:
						case 1:
							BlockPrinter.Print1(readBlock);
							break;
						case 2:
							BlockPrinter.Print2(readBlock);
							break;
						case 3:
							BlockPrinter.Print3(readBlock);
							break;
						case 4:
							BlockPrinter.Print4(readBlock);
							break;
					}
				}

				if (readCount >= byteCount)
				{
					break;
				}
			}

			if (byteCount > bytesRead)
			{
				Console.WriteLine("End of file reached!");
				Console.WriteLine("Press any key to exit.");
				Console.ReadKey();
				return;
			}
		}
		else if (keyInfo.Key == ConsoleKey.Spacebar)
		{
			Console.WriteLine();
			Console.WriteLine(Encoding.UTF8.GetString(lastBlock));
		}
		else if (keyInfo.Key == ConsoleKey.Escape)
		{
			break;
		}
	}
}