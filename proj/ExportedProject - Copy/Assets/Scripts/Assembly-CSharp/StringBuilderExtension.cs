using System.Text;

public static class StringBuilderExtension
{
	private static char[] charToInt = new char[10] { '0', '1', '2', '3', '4', '5', '6', '7', '8', '9' };

	public static void Swap(this StringBuilder sb, int startIndex, int endIndex)
	{
		int num = (endIndex - startIndex + 1) / 2;
		for (int i = 0; i < num; i++)
		{
			char value = sb[startIndex + i];
			sb[startIndex + i] = sb[endIndex - i];
			sb[endIndex - i] = value;
		}
	}

	public static void AppendNumber(this StringBuilder sb, int number)
	{
		int length = sb.Length;
		uint num;
		bool flag;
		if (number < 0)
		{
			num = (uint)((number != int.MinValue) ? (-number) : number);
			flag = true;
		}
		else
		{
			num = (uint)number;
			flag = false;
		}
		do
		{
			sb.Append(charToInt[num % 10]);
			num /= 10;
		}
		while (num != 0);
		if (flag)
		{
			sb.Append('-');
		}
		sb.Swap(length, sb.Length - 1);
	}

	public static void AppendNumber(this StringBuilder sb, uint unumber)
	{
		int length = sb.Length;
		do
		{
			sb.Append(charToInt[unumber % 10]);
			unumber /= 10;
		}
		while (unumber != 0);
		sb.Swap(length, sb.Length - 1);
	}
}
