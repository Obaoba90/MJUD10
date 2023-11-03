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
            AvalableCommands();
            do
            {
                string[] argument;
                string command;
                Input(out argument, out command);
                if (command == "quit")
                {
                    Console.WriteLine("Goodbye!");
                    break;
                }
                else if (command == "load")
                {
                    if (argument.Length == 2)
                    {
                        LoadList(argument);
                    }
                    else if (argument.Length == 1)
                    {
                        LoadToListCommand(defaultFile);
                    }
                }
                else if (command == "help")
                {
                    AvalableCommands();
                }
                else if (command == "list")
                {
                    ListAllGloss();
                }
                else if (command == "new")
                {
                    NewGloss(argument);
                }
                else if (command == "delete")
                {
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
                    Translate(argument);
                }
                else
                {
                    Console.WriteLine($"Unknown command: '{command}'");
                }
            }
            while (true);
        }

        private static void ListAllGloss()
        {
            //Fix me: Om listan inte är laddad.
            foreach (SweEngGloss gloss in dictionary)
            {
                Console.WriteLine($"{gloss.word_swe,-10}  - {gloss.word_eng,-10}");
            }
        }

        private static void AvalableCommands()
        {
            Console.WriteLine("Avalable commands:");
            Console.WriteLine("load       -  Load a dictionary of swedish words and their english translation.");
            Console.WriteLine("list       -  List a dictionary of swedish words and their english translation.");
            Console.WriteLine("new        -  Add a swedish word and the english translation to the dictionary.");
            Console.WriteLine("delete     -  Remove a translation.");
            Console.WriteLine("translate  -  Ask the translation of a word in swedish or english.");
            Console.WriteLine("quit       -  Exit the program.");
        }

        private static void NewGloss(string[] argument)
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

        private static void Input(out string[] argument, out string command)
        {
            Console.Write("> ");
            argument = Console.ReadLine().Split();
            command = argument[0];
        }

        private static void Translate(string[] argument)
        {
            //Fix me: Om ordet inte finns i listan, ge svar.
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

        private static int RemoveGlossFormat()
        {
            //Fix me: Om ordet inte finns, ge svar.
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