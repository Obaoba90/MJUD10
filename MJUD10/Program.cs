namespace MJU23v_D10_inl_sveng
{
    internal class Program
    {
        static List<SweEngGloss> dictionary;
        class SweEngGloss
        {
            public string word_swe, word_eng;
            public SweEngGloss(string word_swe, string word_eng)
            {
                this.word_swe = word_swe; this.word_eng = word_eng;
            }
            public SweEngGloss(string line)
            {
                string[] words = line.Split('|');
                this.word_swe = words[0]; this.word_eng = words[1];
            }
        }
        static void Main(string[] args)
        {
            string defaultFile = "sweeng.lis";
            Console.WriteLine("Welcome to the dictionary app!");
            do
            {
                ////ToDo: Extrahera till metod!
                Console.Write("> ");
                string[] argument = Console.ReadLine().Split();
                string command = argument[0];
                if (command == "quit")
                {
                    //Fix me!: Programmet ska även avslutas.
                    Console.WriteLine("Goodbye!");
                    break;
                }
                else if (command == "load")
                {
                    if (argument.Length == 2)
                    {
                        //ToDo: Extrahera till metod!
                        LoadList(argument);
                    }
                    else if (argument.Length == 1)
                    {
                        //ToDo: Extrahera till metod!
                        LoadToListCommand(defaultFile);
                    }
                }
                else if (command == "help")
                {
                    //NYI: Lista alla möjliga kommandon.
                }
                else if (command == "list")
                {
                    foreach (SweEngGloss gloss in dictionary)
                    {
                        Console.WriteLine($"{gloss.word_swe,-10}  - {gloss.word_eng,-10}");
                    }
                }
                else if (command == "new")
                {
                    if (argument.Length == 3)
                    {
                        dictionary.Add(new SweEngGloss(argument[1], argument[2]));
                    }
                    else if (argument.Length == 1)
                    {
                        Console.Write("Write word in Swedish: ");
                        string s = Console.ReadLine();
                        Console.Write("Write word in English: ");
                        string e = Console.ReadLine();
                        dictionary.Add(new SweEngGloss(s, e));
                    }
                }
                else if (command == "delete")
                {
                    //ToDo: Extrahera till metod!
                    if (argument.Length == 3)
                    {
                        int index = RemoveGloss(argument);
                        dictionary.RemoveAt(index);
                    }
                    else if (argument.Length == 1)
                    {
                        ////ToDo: Extrahera till metod!
                        int index = RemoveGlossFormat();
                        dictionary.RemoveAt(index);
                    }
                }
                else if (command == "translate")
                {
                    if (argument.Length == 2)
                    {
                        foreach (SweEngGloss gloss in dictionary)
                        {
                            if (gloss.word_swe == argument[1])
                                Console.WriteLine($"English for {gloss.word_swe} is {gloss.word_eng}");
                            if (gloss.word_eng == argument[1])
                                Console.WriteLine($"Swedish for {gloss.word_eng} is {gloss.word_swe}");
                        }
                    }
                    else if (argument.Length == 1)
                    {
                        //ToDo: Bättre variabelnamn för "s".
                        Console.WriteLine("Write word to be translated: ");
                        string SweOrEngWord = Console.ReadLine();
                        foreach (SweEngGloss gloss in dictionary)
                        {
                            if (gloss.word_swe == SweOrEngWord)
                                Console.WriteLine($"English for {gloss.word_swe} is {gloss.word_eng}");
                            if (gloss.word_eng == SweOrEngWord)
                                Console.WriteLine($"Swedish for {gloss.word_eng} is {gloss.word_swe}");
                        }
                    }
                }
                else
                {
                    Console.WriteLine($"Unknown command: '{command}'");
                }
            }
            while (true);
        }

        private static int RemoveGlossFormat()
        {
            Console.Write("Write word in Swedish: ");
            string s = Console.ReadLine();
            Console.Write("Write word in English: ");
            string e = Console.ReadLine();
            int index = -1;
            for (int i = 0; i < dictionary.Count; i++)
            {
                SweEngGloss gloss = dictionary[i];
                if (gloss.word_swe == s && gloss.word_eng == e)
                    index = i;
            }

            return index;
        }

        private static int RemoveGloss(string[] argument)
        {
            int index = -1;
            for (int i = 0; i < dictionary.Count; i++)
            {
                SweEngGloss gloss = dictionary[i];
                if (gloss.word_swe == argument[1] && gloss.word_eng == argument[2])
                    index = i;
            }

            return index;
        }

        private static void LoadToListCommand(string defaultFile)
        {
            using (StreamReader sr = new StreamReader(defaultFile))
            {
                dictionary = new List<SweEngGloss>(); // Empty it!
                string line = sr.ReadLine();
                while (line != null)
                {
                    SweEngGloss gloss = new SweEngGloss(line);
                    dictionary.Add(gloss);
                    line = sr.ReadLine();
                }
            }
        }

        private static void LoadList(string[] argument)
        {
            using (StreamReader sr = new StreamReader(argument[1]))
            {
                dictionary = new List<SweEngGloss>(); // Empty it!
                string line = sr.ReadLine();
                while (line != null)
                {
                    SweEngGloss gloss = new SweEngGloss(line);
                    dictionary.Add(gloss);
                    line = sr.ReadLine();
                }
            }
        }
    }
}