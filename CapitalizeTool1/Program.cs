using System;
using System.Collections.Generic;
using System.Text;

namespace CapitalizationTool
{
    class CapitalizationTool
    {
        private Char[] PunctuationMarks = { ';', ':', '.', ',', '!', '?', '-' };
        private String[] EmptyWords = { "A", "An", "And", "At", "But", "By", "For", "In", "Nor", "Not",
            "Of", "On", "Or", "Out", "So", "The", "To", "Up", "Yet"};
        private Boolean IsEmptyWords(String Line)
        {
            Boolean flag = false;
            for (UInt16 i = 0; i < EmptyWords.Length; i++)
            {
                if (Line == EmptyWords[i])
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
            Console.WriteLine("Enter title to capitalize: ");
            UInt16 TopCursorPosition = 0;
            String Line;
            do
            {
                TopCursorPosition++;
                if (TopCursorPosition > 1)
                {
                    Console.WriteLine("Enter title to capitalize: ");
                    Console.SetCursorPosition("Enter title to capitalize: ".Length, TopCursorPosition);
                }
                Console.ForegroundColor = ConsoleColor.Red;
                do
                {
                    Console.SetCursorPosition("Enter title to capitalize: ".Length, TopCursorPosition - 1);
                    Line = Console.ReadLine();
                    //если нажат Enter, курсор остается на месте
                    if (Line.Length == 0)
                    {
                        Console.SetCursorPosition(0, TopCursorPosition);
                    }
                    else
                    {
                        Line = Line.ToLower();
                        //если по ходу строки встречаются знаки препинания, отделяем их пробелами
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
                        //делаем из строки массив подстрок
                            String[] Line1 = Line.Split(new Char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
                        //поднимаем первые буквы подстрок
                        for (UInt16 i = 0; i < Line1.Length; i++)
                        {
                            Char FirstLetter = Char.ToUpper(Line1[i][0]);
                            Line1[i] = Line1[i].Remove(0, 1);
                            Line1[i] = Line1[i].Insert(0, new String(FirstLetter, 1));
                            //если текущее слово не первое и не последнее и является вспомогательным, опускаем первую букву
                            if (i != 0 && i != Line1.Length - 1 && IsEmptyWords(Line1[i]) == true)
                            {
                                Line1[i] = Line1[i].ToLower();
                            }
                        }
                        //если в конце строки знак препинания, поднимаем первую букву слова перед ним
                        if (Line1.Length > 0 && IsPunctuationMarks(Line1[Line1.Length - 1][0]) == true)
                        {
                            Char FirstLetter = Char.ToUpper(Line1[Line1.Length - 2][0]);
                            Line1[Line1.Length - 2] = Line1[Line1.Length - 2].Remove(0, 1);
                            Line1[Line1.Length - 2] = Line1[Line1.Length - 2].Insert(0, new String(FirstLetter, 1));
                        }
                        //собираем строку
                        Line = String.Join(" ", Line1);
                        //если была введена пробельная строка, StringSplitOptions.RemoveEmptyEntries сделает ее пустой и она не будет обрабатываться, поэтому добавляем пробел
                        if (Line.Length == 0)
                        {
                            Line = Line.Insert(0, " ");
                        }
                        //убираем пробел перед знаком препинания
                        for (UInt16 i = 1; i < Line.Length; i++)
                        {
                            if (IsPunctuationMarks(Line[i]) == true && Line[i - 1] == ' ')
                            {
                                Line = Line.Remove(i - 1, 1);
                            }
                        }
                        //вставляем пробел после знака препинания, если его не было
                        for (UInt16 i = 0; i < Line.Length - 1; i++)
                        {
                            if (IsPunctuationMarks(Line[i]) == true && Line[i + 1] != ' ')
                            {
                                Line = Line.Insert(i + 1, " ");
                            }
                        }
                        //форматируем тире
                        for (UInt16 i = 0; i < Line.Length; i++)
                        {
                            if (Line[i] == '-' && Line[i - 1] != ' ')
                            {
                                i--;
                                Line = Line.Insert(i + 1, " ");
                            }
                        }
                    }
                }
                while (Line.Length == 0);
                Console.ResetColor();
                Console.WriteLine("Capitalized title: ");
                Console.SetCursorPosition("Capitalized title: ".Length, TopCursorPosition); 
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
