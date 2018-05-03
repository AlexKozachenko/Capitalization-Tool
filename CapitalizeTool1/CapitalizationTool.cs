using System;

namespace TitleCapitalizationTool
{
    internal class CapitalizationTool
    {
        private String[] emptyWords = { "A", "An", "And", "At", "But", "By", "For", "In", "Nor", "Not",
            "Of", "On", "Or", "Out", "So", "The", "To", "Up", "Yet" };
        private Char[] punctuationMarks = { ';', ':', '.', ',', '!', '?', '-' };

        public void Сapitalize()
        {
            UInt16 topCursorPosition = 0;
            String line;
            do
            {
                topCursorPosition++;
                Console.WriteLine("Enter title to capitalize: ");
                Console.ForegroundColor = ConsoleColor.Red;
                do
                {
                    Console.SetCursorPosition("Enter title to capitalize: ".Length, topCursorPosition - 1);
                    line = Console.ReadLine();
                    // строка обрабатывается, если она ненулевой длины
                    if (line.Length != 0)
                    {
                        line = line.ToLower();
                        // если по ходу строки встречаются знаки препинания, отделяем их пробелами
                        for (Int16 i = 0; i < line.Length; i++)
                        {
                            if (IsPunctuationMarks(line[i]))
                            {
                                // откат индекса на 1, чтобы в Insert'ах не было выражений с вычитанием
                                i--;
                                // первый пробел вставляется в позицию 1 от нового i
                                const UInt16 offsetIndexBeforePunctuationMark = 1;
                                // соответственно знак препинания будет в позиции 2,
                                // а пробел после него - в позиции 3
                                const UInt16 offsetIndexAfterPunctuationMark = 3;
                                // исходя из этого, следующее i встанет в позицию 4
                                const Int16 offsetIndexAfterSecondSpace = 4;
                                if (i >= -1)
                                {
                                    line = line.Insert(i + offsetIndexBeforePunctuationMark, " ");
                                }
                                line = line.Insert(i + offsetIndexAfterPunctuationMark, " ");
                                i += offsetIndexAfterSecondSpace;
                            }
                        }
                        // делаем из строки массив подстрок
                        String[] substrings = line.Split(new Char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
                        // поднимаем первые буквы подстрок
                        for (UInt16 i = 0; i < substrings.Length; i++)
                        {
                            Char firstLetter = Char.ToUpper(substrings[i][0]);
                            substrings[i] = substrings[i].Remove(0, 1);
                            substrings[i] = substrings[i].Insert(0, new String(firstLetter, 1));
                            //если текущее слово не первое и не последнее и является вспомогательным, опускаем первую букву
                            if (i != 0 && i != substrings.Length - 1 && IsEmptyWords(substrings[i]))
                            {
                                substrings[i] = substrings[i].ToLower();
                            }
                        }
                        // если в конце строки знак препинания, поднимаем первую букву слова перед ним
                        if (substrings.Length > 1 && IsPunctuationMarks(substrings[substrings.Length - 1][0]))
                        {
                            Char firstLetter = Char.ToUpper(substrings[substrings.Length - 2][0]);
                            substrings[substrings.Length - 2] = substrings[substrings.Length - 2].Remove(0, 1);
                            substrings[substrings.Length - 2] = substrings[substrings.Length - 2].Insert(0, new String(firstLetter, 1));
                        }
                        // собираем строку
                        line = String.Join(" ", substrings);
                        // если была введена пробельная строка, StringSplitOptions.RemoveEmptyEntries сделает ее пустой и она не будет обрабатываться, поэтому добавляем пробел
                        if (line.Length == 0)
                        {
                            line = line.Insert(0, " ");
                        }
                        // убираем пробел перед знаком препинания, если это не тире
                        for (UInt16 i = 1; i < line.Length; i++)
                        {
                            if (IsPunctuationMarks(line[i]) && line[i - 1] == ' ' && line[i] != '-')
                            {
                                line = line.Remove(i - 1, 1);
                            }
                        }
                    }
                } while (line.Length == 0);
                Console.ResetColor();
                Console.WriteLine("Capitalized title: ");
                Console.SetCursorPosition("Capitalized title: ".Length, topCursorPosition);
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine(line);
                Console.ResetColor();
                Console.WriteLine(" ");
                topCursorPosition += 2;
            } while (true);
        }

        private Boolean IsEmptyWords(String line)
        {
            Boolean isEmptyWord = false;
            for (UInt16 i = 0; i < emptyWords.Length; i++)
            {
                if (line == emptyWords[i])
                {
                    isEmptyWord = true;
                    break;
                }
            }
            return isEmptyWord;
        }

        private Boolean IsPunctuationMarks(Char mark)
        {
            Boolean isPunctuationMark = false;
            for (UInt16 i = 0; i < punctuationMarks.Length; i++)
            {
                if (mark == punctuationMarks[i])
                {
                    isPunctuationMark = true;
                    break;
                }
            }
            return isPunctuationMark;
        }
    }

    internal class Program
    {
        public static void Main()
        {
            CapitalizationTool capitalizationTool = new CapitalizationTool();
            capitalizationTool.Сapitalize();
        }
    }
}

//© 2018 GitHub, Inc.