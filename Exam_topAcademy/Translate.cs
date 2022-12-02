using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Exam_topAcademy
{
    internal class Translate
    {
        private List<string> words = new List<string>();

        private Dictionary<string, List<string>> dictionaryENG_RUS;
        private Dictionary<string, List<string>> dictionaryRUS_ENG;

        // Существует ли слово в словаре
        public void CreateDictionary(string typeDictionary)
        {
            if (typeDictionary == "eng")
            {
                dictionaryENG_RUS = new Dictionary<string, List<string>>();
            }
            else if (typeDictionary == "rus")
            {
                dictionaryRUS_ENG = new Dictionary<string, List<string>>();
            }
        }
        // Проверка слова на то является оно английским или русским
        private string checkWord(string word)
        {
            if (Regex.IsMatch(word, @"\p{IsCyrillic}"))
            {
                return "eng";
            }
            else
            {
                return "rus";
            }
        }
        // Добавление слова и его вариантов перевода
        public void addWord(string typeDictionary, string wordENG, string wordRUS)
        {
            if (typeDictionary == "eng")
            {
                if (dictionaryENG_RUS != null)
                {
                    if (checkWord(wordENG) == "eng")
                    {
                        words.Add(wordRUS);
                        dictionaryENG_RUS.Add(wordENG, words);
                        Console.WriteLine("Слово добавлено в ENG-RUS словарь");
                    }
                }
                else
                {
                    Console.WriteLine("Словарь не существует");
                }
            }
            else if (typeDictionary == "rus")
            {
                if (dictionaryRUS_ENG != null)
                {
                    if (checkWord(wordRUS) == "rus")
                    {
                        words.Add(wordRUS);
                        dictionaryRUS_ENG.Add(wordRUS, words);
                        Console.WriteLine("Слово добавлено в RUS-ENG словарь");
                    }
                }
                else
                {
                    Console.WriteLine("Словарь не существует");
                }
            }
        }

        // Метод нерабочий
        private string isCheckWordInDictionary(string word)
        {
            if (checkWord(word) == "eng")
            {
                foreach (var item in dictionaryENG_RUS)
                {
                    if (item.Key == word)
                    {
                        return "eng";
                    }
                    else if (words.IndexOf(word) != -1)
                    {
                        return "eng";
                    }
                    else
                    {
                        return "null";
                    }
                }
            }
            else if (checkWord(word) == "rus")
            {
                foreach (var item in dictionaryRUS_ENG)
                {
                    if (item.Key == word)
                    {
                        return "rus";
                    }
                    else if (words.IndexOf(word) != -1)
                    {
                        return "rus";
                    }
                    else
                    {
                        return "null";
                    }
                }
            }
            else
            {
                return "null";
            }
        }

        // Метод для сохранения всех вариантов перевода слова выбранного слова
        private List<string> SaveTranslateWord(string word)
        {
            List<string> tmpWord = new List<string>();
            string wordValue = "";

            if (isCheckWordInDictionary(word) == "eng" || isCheckWordInDictionary(word) == "rus")
            {
                foreach (var itemKey in dictionaryENG_RUS)
                {
                    if (itemKey.Key == word)
                    {
                        foreach (var itemValue in words)
                        {
                            wordValue = itemValue;
                            tmpWord.Add(wordValue);
                        }
                    }
                }
            }

            return tmpWord;
        }
        public void wordSubstitution(string typeDictionary, string wordENG, string wordRUS)
        {
            var tmp = "";
            Console.WriteLine("В каком словаре вы хотите заменить слово? type: eng or rus");
            tmp = Console.ReadLine();

            if (tmp == isCheckWordInDictionary(wordENG))
            {
                Console.Clear();
                Console.WriteLine("Такое слово в ENG-RUS словаре есть. На какое меняем?");
                tmp = Console.ReadLine();

                // Записываю сохраненные переводы(Value) слова в отдельную переменную чтобы вернуть их после замены слова(Key)
                List<string> translateWord = SaveTranslateWord(wordENG);

                dictionaryENG_RUS.Remove(wordENG);
                dictionaryENG_RUS.Add(tmp, translateWord);

                // Удаляю слово во втором словаре
                translateWord = SaveTranslateWord(wordRUS);

                dictionaryRUS_ENG.Remove(wordRUS);
                dictionaryRUS_ENG.Add(tmp, translateWord);
            }
            else if (tmp == isCheckWordInDictionary(wordRUS))
            {
                Console.Clear();
                Console.WriteLine("Такое слово в RUS-ENG словаре есть. На какое меняем?");
                tmp = Console.ReadLine();

                // Записываю сохраненные переводы(Value) слова в отдельную переменную чтобы вернуть их после замены слова(Key)
                List<string> translateWord = SaveTranslateWord(wordRUS);

                dictionaryRUS_ENG.Remove(wordRUS);
                dictionaryRUS_ENG.Add(tmp, translateWord);

                // Удаляю слово во втором словаре
                translateWord = SaveTranslateWord(wordENG);
                dictionaryENG_RUS.Remove(wordENG);
                dictionaryENG_RUS.Add(tmp, translateWord);
            }
            else if (isCheckWordInDictionary(wordENG) == "null")
            {
                Console.WriteLine("Такого слова в словаре нет.");
            }
        }

        // Метод для удаления слова
        public bool DeleteWord(string word)
        {
            Console.WriteLine("В каком словаре удалить слово? eng or rus");
            Console.WriteLine("Удаляем слово или перевод? 1 or 2");
            var tmp = Console.ReadLine();
            switch (tmp)
            {
                case "1":
                    Console.Clear();
                    Console.WriteLine("В каком словаре удалить слово? eng or rus");
                    var choice = Console.ReadLine();

                    // Удаляю слово и все его варианты перевода англ вариант
                    if (checkWord(word) == "eng")
                    {
                        dictionaryENG_RUS.Remove(word);
                        return true;

                    }
                    else if (checkWord(word) == "rus")
                    {
                        dictionaryRUS_ENG.Remove(word);
                        return true;
                    }
                    else
                    {
                        Console.WriteLine("Введено некорректное значение.");
                        return false;
                    }
                case "2":
                    Console.WriteLine("Какой перевод слово удаляем?");
                    foreach (var item in words)
                    {
                        Console.WriteLine($"{item}\n");
                    }
                    break;


            }
        }
    }
}
