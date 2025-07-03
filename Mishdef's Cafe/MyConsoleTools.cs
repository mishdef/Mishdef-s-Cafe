using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace MyFunctions
{

    public static class MessageBox
    {
        public enum Buttons { Ok, YesNo, None }
        public enum Button { Ok, Yes, No, None }

        private const int MIN_CONTENT_WIDTH = 20;
        private const int PADDING_HORIZONTAL_TEXT = 2;

        public static Button Show(string message, string header = "Message", Buttons buttons = Buttons.None)
        {
            string actualHeader = " " + header.ToUpper() + " ";
            string actualButtonsString = ButtonsToString(buttons);

            int maxContentLength = message.Length;
            maxContentLength = Math.Max(maxContentLength, actualHeader.Length);
            if (actualButtonsString != null)
            {
                maxContentLength = Math.Max(maxContentLength, actualButtonsString.Length);
            }

            int contentWidth = Math.Max(MIN_CONTENT_WIDTH, maxContentLength + PADDING_HORIZONTAL_TEXT);

            DrawHorizontalLine('┏', '━', '┓', contentWidth);

            Console.Write($"┃{CenterString(actualHeader, contentWidth, '░')}┃\n");

            DrawHorizontalLine('┣', '━', '┫', contentWidth);

            Console.Write($"┃{message.PadRight(contentWidth)}┃\n");

            if (actualButtonsString != null)
            {
                DrawHorizontalLine('┣', '━', '┫', contentWidth);

                Console.Write($"┃{CenterString(actualButtonsString, contentWidth, ' ')}┃\n");
            }

            DrawHorizontalLine('┗', '━', '┛', contentWidth);

            if (buttons == Buttons.None)
            {
                return Button.None;
            }
            else
            {
                return GetChar(buttons);
            }
        }

        private static void DrawHorizontalLine(char leftCorner, char fillChar, char rightCorner, int length)
        {
            Console.Write(leftCorner);
            for (int i = 0; i < length; i++)
            {
                Console.Write(fillChar);
            }
            Console.Write(rightCorner);
            Console.Write("\n");
        }

        private static string CenterString(string s, int width, char fillChar = ' ')
        {
            if (string.IsNullOrEmpty(s))
            {
                return new string(fillChar, width);
            }

            if (s.Length >= width)
            {
                return s.Substring(0, width);
            }

            int totalPadding = width - s.Length;
            int leftPadding = totalPadding / 2;
            int rightPadding = totalPadding - leftPadding;

            return new string(fillChar, leftPadding) + s + new string(fillChar, rightPadding);
        }

        public static void BoxItem(string item)
        {
            Console.Write("┏");
            for (int i = 0; i < item.Length + 2; i++) { Console.Write('━'); }
            Console.Write("┓\n");
            Console.Write($"┃ {item} ┃\n");
            Console.Write("┗");
            for (int i = 0; i < item.Length + 2; i++) { Console.Write('━'); }
            Console.Write("┛\n");
        }

        private static Button GetChar(Buttons buttons)
        {
            Console.CursorVisible = false;
            try
            {
                if (buttons == Buttons.Ok)
                {
                    do
                    {
                        ConsoleKeyInfo keyInfo = Console.ReadKey(true);
                        if (keyInfo.Key == ConsoleKey.Enter)
                        {
                            Console.WriteLine();
                            return Button.Ok;
                        }
                    } while (true);
                }
                if (buttons == Buttons.YesNo)
                {
                    do
                    {
                        ConsoleKeyInfo keyInfo = Console.ReadKey(true);
                        char key = char.ToLower(keyInfo.KeyChar);

                        if (key == 'y' || key == 'н')
                        {
                            Console.WriteLine(" -> Yes");
                            return Button.Yes;
                        }
                        if (key == 'n' || key == 'т')
                        {
                            Console.WriteLine(" -> No");
                            return Button.No;
                        }
                    } while (true);
                }
            }
            finally
            {
                Console.CursorVisible = true;
            }
            return Button.None;
        }

        private static string ButtonsToString(Buttons buttons)
        {
            switch (buttons)
            {
                case Buttons.Ok:
                    return "Ok (Enter)";
                case Buttons.YesNo:
                    return "Yes (Y) / No (N)";
                case Buttons.None:
                default:
                    return null;
            }
        }
    }

    public static class Tools
    {
        public enum InputType { With, Without }

        public static void DrawLine(int n, char ch = '─')
        {
            for (int i = 0; i < n; i++)
            {
                Console.Write(ch);
            }
            Console.WriteLine();
        }

        public static int InputInt(string promt, InputType inputType = InputType.With, int min = int.MinValue, int max = int.MaxValue)
        {
            int num;
            string maxStr, minStr;

            if (min == int.MinValue) minStr = "minimum int value"; else minStr = min.ToString();
            if (max == int.MaxValue) maxStr = "maximum int value"; else maxStr = max.ToString();

            do
            {
                try
                {
                    Console.Write(promt);
                    num = int.Parse(Console.ReadLine());
                    if (inputType == InputType.With)
                    {
                        if (num < min || num > max) throw new ArgumentException("inclusive");
                    }
                    if (inputType == InputType.Without)
                    {
                        if (num <= min || num >= max) throw new ArgumentException("exclusive");
                    }
                    break;
                }
                catch (ArgumentException ex) { Console.WriteLine(" ERROR! The number must be in the range from " + minStr + " to " + maxStr + $" ({ex.Message}). Please try again!"); }
                catch (FormatException) { Console.WriteLine(" ERROR! Invalid format! Please try again!"); }
                catch (OverflowException) { Console.WriteLine(" ERROR! Number is too large! Please try again!"); }
                catch (Exception ex) { Console.WriteLine($" ERROR! {ex.Message} Please try again!"); }
            }
            while (true);
            return num;
        }

        public static double InputDouble(string promt, InputType inputType = InputType.With, double min = double.MinValue, double max = double.MaxValue)
        {
            double num;
            string maxStr, minStr;

            if (min == double.MinValue) minStr = "minimum double value"; else minStr = min.ToString();
            if (max == double.MaxValue) maxStr = "maximum double value"; else maxStr = max.ToString();

            do
            {
                try
                {
                    Console.Write(promt);
                    num = double.Parse(Console.ReadLine());

                    if (inputType == InputType.With)
                    {
                        if (num < min || num > max) throw new ArgumentException("inclusive");
                    }
                    else
                    {
                        if (num <= min || num >= max) throw new ArgumentException("exclusive");
                    }
                    break;
                }
                catch (ArgumentException ex) { Console.WriteLine(" ERROR! The number must be in the range from " + minStr + " to " + maxStr + $" ({ex.Message}). Please try again!"); }
                catch (FormatException) { Console.WriteLine(" ERROR! Invalid format! Please try again!"); }
                catch (OverflowException) { Console.WriteLine(" ERROR! Number is too large! Please try again!"); }
                catch (Exception ex) { Console.WriteLine($" ERROR! {ex.Message} Please try again!"); }
            }
            while (true);
            return num;
        }

        public static string InputFileName(string promt, string fileExtention)
        {
            string fileName;

            do
            {
                Console.Write(promt);
                fileName = Console.ReadLine();

                if (fileName.EndsWith(fileExtention))
                {
                    fileName = fileName.Substring(0, fileName.Length - fileExtention.Length);
                }

                if (1 > fileName.Length || fileName.Length > 20)
                {
                    Console.WriteLine("File name must be between 1 and 20 characters long.");
                }
            }
            while ((1 > fileName.Length || fileName.Length > 20));
            return fileName + fileExtention;
        }
    }
}