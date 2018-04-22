using System;
using System.Collections.Generic;
using System.Text;

namespace CapitalizationTool
{
    class CapitalizationTool
    {
        private Char[] PunctuationMarks = { ';', ':', '.', ',', '!', '?', '-' };
        private String[] ShortWords = { "A", "An", "And", "At", "But", "By", "For", "In", "Into", "Nor", "Not",
            "Of", "On", "Or", "Out", "So", "The", "To", "Up", "Yet"};
        private Boolean IsShortWords(String Line)
        {
            Boolean flag = false;
            for (UInt16 i = 0; i < ShortWords.Length; i++)
            {
                if (Line == ShortWords[i])
                {
                    flag = true;
                    break;
                }
            }
            return flag;
        }
        private Boolean IsPunctuationMarks(Char Mark)
        {
            Boolean flag = false;
            for (UInt16 i = 0; i < PunctuationMarks.Length; i++)
            {
                if (Mark == PunctuationMarks[i])
                {
                    flag = true;
                    break;
                }
            }
            return flag;
        }
        public void Capitalize()
        {
            Console.WriteLine("Enter string:");
            UInt16 TopCursorPosition = 0;
            String Line;
            do
            {
                TopCursorPosition++;
                if (TopCursorPosition > 1)
                {
                    Console.WriteLine("Enter string:");
                    TopCursorPosition++;
                }
                Console.ForegroundColor = ConsoleColor.Red;
                do
                {
                    Line = Console.ReadLine();
                    if (Line == "")
                    {
                        Console.SetCursorPosition(0, TopCursorPosition);
                    }
                    Line = Line.ToLower();
                    for (UInt16 i = 0; i < Line.Length; i++)
                    {
                        if (IsPunctuationMarks(Line[i]) == true)
                        {
                            i--;
                            Line = Line.Insert(i + 1, " ");
                            Line = Line.Insert(i + 3, " ");
                            i += 4;
                        }
                    }
                    String[] Line1 = Line.Split(new Char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);

                    for (UInt16 i = 0; i < Line1.Length; i++)
                    {
                        Char FirstLetter = Char.ToUpper(Line1[i][0]);
                        Line1[i] = Line1[i].Remove(0, 1);
                        Line1[i] = Line1[i].Insert(0, new String(FirstLetter, 1));
                        if (i != 0 && i != Line1.Length - 1 && IsShortWords(Line1[i]) == true)
                        {
                            Line1[i] = Line1[i].ToLower();
                        }
                    }
                    if (IsPunctuationMarks(Line1[Line1.Length - 1][0]) == true)
                    {
                        Char FirstLetter = Char.ToUpper(Line1[Line1.Length - 2][0]);
                        Line1[Line1.Length - 2] = Line1[Line1.Length - 2].Remove(0, 1);
                        Line1[Line1.Length - 2] = Line1[Line1.Length - 2].Insert(0, new String(FirstLetter, 1));
                    }
                    Line = String.Join(" ", Line1);
                    for (UInt16 i = 1; i < Line.Length; i++)
                    {
                        if (IsPunctuationMarks(Line[i]) == true && Line[i - 1] == ' ')
                        {
                            Line = Line.Remove(i - 1, 1);
                        }
                    }
                    for (UInt16 i = 0; i < Line.Length - 1; i++)
                    {
                        if (IsPunctuationMarks(Line[i]) == true && Line[i + 1] != ' ')
                        {
                            Line = Line.Insert(i + 1, " ");
                        }
                    }
                    for (UInt16 i = 0; i < Line.Length; i++)
                    {
                        if (Line[i] == '-' && Line[i - 1] != ' ')
                        {
                            i--;
                            Line = Line.Insert(i + 1, " ");
                        }
                    }
                }
                while (Line == "");
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine(Line);
                Console.ResetColor();
                TopCursorPosition++;
            }
            while (true);
        }
        public static void Main()
        {
            CapitalizationTool CapitalizationTool = new CapitalizationTool();
            CapitalizationTool.Capitalize();
        }
    }
}
