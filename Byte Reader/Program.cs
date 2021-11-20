Console.OutputEncoding = System.Text.Encoding.Unicode;

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
Console.WriteLine("Skipping...");
using (StreamReader sr = new StreamReader(filePath))
{
	long readCount = 0;

	char[] skipBlock = null;
	long bytesRead = 0;
	if (skipCount > int.MaxValue / sizeof(char))
	{
		skipBlock = new char[int.MaxValue / sizeof(char)];
		while (true)
		{
			int length = skipBlock.Length;
			if(readCount + length > skipCount)
			{
				length = (int)(skipCount - readCount);
			}

			bytesRead = sr.ReadBlock(skipBlock, 0, length);
			readCount += bytesRead;
			if (readCount >= skipCount || length > bytesRead)
			{
				break;
			}
		}
	}
	else
	{
		skipBlock = new char[skipCount];
		bytesRead = sr.ReadBlock(skipBlock, 0, (int)skipCount);
		readCount += bytesRead;
	}

	Array.Clear(skipBlock);

	Console.WriteLine();
	Console.WriteLine("Press Enter to read " + lineCountStr + " bytes from the given file.");
	Console.WriteLine("Press Space to display the last shown text in hexadecimal.");
	Console.WriteLine("Press Escape to quit.");
	Console.WriteLine();

	long lastBytesRead = 0;
	char[] lastBlock = new char[byteCount];
	while (true)
	{
		ConsoleKeyInfo keyInfo = Console.ReadKey();
		if (keyInfo.Key == ConsoleKey.Enter)
		{
			while (true)
			{
				char[] readBlock = null;
				if(byteCount > int.MaxValue / sizeof(char))
				{
					readBlock = new char[int.MaxValue / sizeof(char)];
					for(long i = 0; i < byteCount; i += bytesRead)
					{
						bytesRead = sr.ReadBlock(readBlock, 0, readBlock.Length);
						if(readBlock.Length > bytesRead)
						{
							break;
						}
					}
				}
				else
				{
					readBlock = new char[byteCount];
					bytesRead = sr.ReadBlock(readBlock, 0, (int)byteCount);
				}
				
				if (readBlock != null)
				{
					lastBlock = readBlock;
					lastBytesRead = bytesRead;

					string readStr = new string(readBlock);
					if (readStr.Length > 0 && !String.IsNullOrWhiteSpace(readStr) && !String.IsNullOrEmpty(readStr) && !readStr.All(x => x == 0))
					{
						Console.WriteLine(readCount + "|" + readStr);
						readCount += bytesRead;
						break;
					}
					else
					{
						readCount += bytesRead;
						continue;
					}
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
			Console.Write((readCount - lastBytesRead) + "|");
			foreach (char c in lastBlock)
			{
				Console.Write("{" + String.Format(@"\x{0:x2}", (byte)c) + "} ");
			}
			Console.WriteLine();
		}
		else if (keyInfo.Key == ConsoleKey.Escape)
		{
			break;
		}
	}
}