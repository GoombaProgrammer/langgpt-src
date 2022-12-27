using System;
using System.Text;
using System.Text.RegularExpressions;

namespace langgpt
{
    internal class Program
    {
        // langGPT is a language translator creator
        
        // Learned words dictionary
        private static Dictionary<string, string> learnedWords = new();
        // Learned word order dictionary
        private static Dictionary<string, string> learnedWordOrder = new();
        private static string[] nouns = Properties.Resources.Nouns.Split("\r\n");
        private static string[] adjs = Properties.Resources.Adj.Split("\r\n");

        static void Translate(string sentence)
        {
            // The program will split the sentence into words.
            string[] words = sentence.Split(' ');
            // The program will create a new string for the translation.
            string translation = "";
            List<string> nextnoun = new List<string>();
            List<string> nextadjective = new List<string>();

            // The program will loop through the words.
            for (int i = 0; i < words.Length; i++)
            {
                string word = words[i];
                if (words.Length > (i + 1) && learnedWords.ContainsKey(words[i] + " " + words[i + 1]))
                {
                    word = words[i] + " " + words[i + 1];
                    // The program will check if the word is in the learned words dictionary.
                    if (learnedWords.ContainsKey(word))
                    {
                        // The program will add the translation of the word to the translation.
                        if (learnedWordOrder.ContainsKey(word))
                        {
                            if (learnedWordOrder[word] == "noun")
                            {
                                nextnoun.Add(learnedWords[word]);
                            }
                            else if (learnedWordOrder[word] == "adjective")
                            {
                                nextadjective.Add(learnedWords[word]);
                            }
                        }
                        else
                        {
                            // The program will add the translation of the word to the translation.
                            translation += learnedWords[word] + " ";
                        }
                        i++;
                    }
                }
                else
                {
                    // The program will check if the word is in the learned words dictionary.
                    if (learnedWords.ContainsKey(word))
                    {
                        // The program will add the translation of the word to the translation.
                        if (learnedWordOrder.ContainsKey(word))
                        {
                            if (learnedWordOrder[word] == "noun")
                            {
                                nextnoun.Add(learnedWords[word]);
                            }
                            else if (learnedWordOrder[word] == "adjective")
                            {
                                nextadjective.Add(learnedWords[word]);
                            }
                        }
                        else
                        {
                            // The program will add the translation of the word to the translation.
                            translation += learnedWords[word] + " ";
                        }
                    }
                    else
                    {
                        // The program will add the word to the translation.
                        translation += word + " ";
                    }
                }
                if (nouns.Contains(word))
                {
                    foreach (string a in nextnoun)
                    {
                        translation += a + " ";
                    }
                    nextnoun.Clear();
                }
                if (adjs.Contains(word))
                {
                    foreach (string a in nextadjective)
                    {
                        translation += a + " ";
                    }
                    nextadjective.Clear();
                }
            }
            // The program will print the translation.
            Console.WriteLine(translation);
        }
        static void load()
        {
            // The program will check if the learned words dictionary text file exists.
            if (File.Exists("learnedWords.txt"))
            {
                // The program will read the learned words dictionary text file.
                string learnedWordsString = File.ReadAllText("learnedWords.txt");
                // The program will split the learned words dictionary string into lines.
                string[] learnedWordsLines = learnedWordsString.Split("\r\n").SkipLast(1).ToArray();
                // The program will loop through the lines.
                foreach (string learnedWordsLine in learnedWordsLines)
                {
                    // The program will split the line into the word and the translation.
                    string[] learnedWordsLineSplit = learnedWordsLine.Split(':');
                    // The program will add the word and the translation to the learned words dictionary.
                    learnedWords.Add(learnedWordsLineSplit[0], string.Join(' ', learnedWordsLineSplit[1..]));
                }
            }
            // The program will check if the learned word order dictionary text file exists.
            if (File.Exists("learnedWordOrder.txt"))
            {
                // The program will read the learned word order dictionary text file.
                string learnedWordOrderString = File.ReadAllText("learnedWordOrder.txt");
                // The program will split the learned word order dictionary string into lines.
                string[] learnedWordOrderLines = learnedWordOrderString.Split("\r\n").SkipLast(1).ToArray();
                // The program will loop through the lines.
                foreach (string learnedWordOrderLine in learnedWordOrderLines)
                {
                    // The program will split the line into the word and the word order.
                    string[] learnedWordOrderLineSplit = learnedWordOrderLine.Split(':');
                    // The program will add the word and the word order to the learned word order dictionary.
                    learnedWordOrder.Add(learnedWordOrderLineSplit[0], string.Join(' ', learnedWordOrderLineSplit[1..]));
                }
            }
        }
        static void Main(string[] args)
        {
            bool doTrans = false;
            if (args.Length > 1)
            {
                if (!string.IsNullOrEmpty(args[1]))
                {
                    doTrans = true;
                }
            }
        theLoop:
            string command;
            if (doTrans)
            {
                command = File.ReadAllText(args[1]);
                load();
                Translate(command);
                Environment.Exit(0);
            }
            else
            {
                Console.WriteLine();
                // The program will ask the user to type a commnad.
                Console.Write("? ");
                // Now the program will read the command.
                command = Console.ReadLine() + "";
            }
            // The program will check if the command is "learn"
            if (command.StartsWith("lea"))
            {
                // The program will ask the user to type a word in their language.
                Console.Write("Word: ");
                // Now the program will read the word.
                string word = Console.ReadLine();
                // The program will ask the user to type the translation of the word.
                Console.Write("Translation: ");
                // Now the program will read the translation.
                string translation = Console.ReadLine();
                // The program will add the word and the translation to the learned words dictionary.
                learnedWords.Add(word, translation);
            }
            // The program will check if the command is "translate"
            else if (command.StartsWith("tra"))
            {
                // The program will ask the user to type a sentence in their language.
                Console.Write("Sentence: ");
                // Now the program will read the sentence.
                string sentence = (Console.ReadLine() + "").ToLower();
                // Translate sentence
                Translate(sentence);
            }
            // The program will check if the command is "wordorder"
            else if (command.StartsWith("wor"))
            {
                // The program will ask the user to type a word in their language.
                Console.Write("Word: ");
                // Now the program will read the word.
                string word = Console.ReadLine();
                // The program will ask the user to type the word order of the word.
                Console.Write("Word comes after a (for example, noun, adjective): ");
                // Now the program will read the word order.
                string wordorder = Console.ReadLine();
                // The program will add the word and the word order to the learned word order dictionary.
                learnedWordOrder.Add(word, wordorder);
            }
            // The program will check if the command is "save"
            // The save command saves the learnedWords dictionary and the learnedWordOrder dictionary to a text file
            else if (command == "save")
            {
                // The program will create a new string for the learned words dictionary.
                string learnedWordsString = "";
                // The program will loop through the learned words dictionary.
                foreach (KeyValuePair<string, string> learnedWord in learnedWords)
                {
                    // The program will add the word and the translation to the learned words dictionary string.
                    learnedWordsString += learnedWord.Key + ":" + learnedWord.Value + "\r\n";
                }
                // The program will create a new string for the learned word order dictionary.
                string learnedWordOrderString = "";
                // The program will loop through the learned word order dictionary.
                foreach (KeyValuePair<string, string> learnedWordOrder in learnedWordOrder)
                {
                    // The program will add the word and the word order to the learned word order dictionary string.
                    learnedWordOrderString += learnedWordOrder.Key + ":" + learnedWordOrder.Value + "\r\n";
                }
                // The program will write the learned words dictionary string to a text file.
                File.WriteAllText("learnedWords.txt", learnedWordsString);
                // The program will write the learned word order dictionary string to a text file.
                File.WriteAllText("learnedWordOrder.txt", learnedWordOrderString);
            }
            // The program will check if the command is "load"
            // The load command loads the learnedWords dictionary and the learnedWordOrder dictionary from a text file
            else if (command == "load")
            {
                load();
            }
            // The program will check if the command is "exit"
            else if (command == "exit")
            {
                // The program will exit the program.
                Environment.Exit(0);
            }
            // The program will check if the command is "help"
            else if (command == "help")
            {
                Console.WriteLine("help                  display this help");
                Console.WriteLine("exit                  exits langGPT");
                Console.WriteLine("save                  save translator to disk");
                Console.WriteLine("load                  load translator from disk");
                Console.WriteLine("learn                 add a word to the translator");
                Console.WriteLine("wordorder             make a word appear after another word type");
                Console.WriteLine("translate             runs the translator");
            }
            goto theLoop;
        }
    }
}