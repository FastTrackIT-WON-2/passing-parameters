using System;

namespace PassingParameters
{
    class Program
    {
        static void Main(string[] args)
        {
            /*
            int nr = 12;

            Console.WriteLine($"i={nr} (before calling {nameof(Print)})");
            IncrementOut(nr, out nr);
            Console.WriteLine($"i={nr} (after calling {nameof(Print)})");
            */

            int x, y;
            if (TryParseCoordinates("abcd", out x, out y))
            {
                Console.WriteLine($"Parse succeeded, x={x}, y={y}");
            }
            else
            {
                Console.WriteLine($"Parse failed, x={x}, y={y}");
            }

            /*
            Person p = new Person { FirstName = "John", LastName = "Doe" };
            Console.WriteLine($"Person {nameof(p.FirstName)}={p.FirstName}, {nameof(p.LastName)}={p.LastName} (before calling {nameof(PrintPerson)})");
            ChangePerson(ref p);
            Console.WriteLine($"Person {nameof(p.FirstName)}={p.FirstName}, {nameof(p.LastName)}={p.LastName} (after calling {nameof(PrintPerson)})");
            */
        }

        private static void IncrementRef(ref int nr)
        {
            nr = nr + 1;
        }

        private static void IncrementOut(int value, out int nr)
        {
            nr = value;
            nr = nr + 1;
        }

        private static void Print(int nr)
        {
            Console.WriteLine($"nr={nr} (before change)");
            nr = 100;
            Console.WriteLine($"nr={nr} (after change)");
        }

        private static void PrintPerson(in Person p)
        {
            Console.WriteLine($"Person {nameof(p.FirstName)}={p.FirstName}, {nameof(p.LastName)}={p.LastName} (before change)");
            p.LastName = "Test";
            //p = new Person { FirstName = "First", LastName = "Test" };
            Console.WriteLine($"Person {nameof(p.FirstName)}={p.FirstName}, {nameof(p.LastName)}={p.LastName} (after change)");
        }

        private static void ChangePersonRef(ref Person p)
        {
            Console.WriteLine($"Person {nameof(p.FirstName)}={p.FirstName}, {nameof(p.LastName)}={p.LastName} (before change)");
            p = new Person { FirstName = "First", LastName = "Test" };
            Console.WriteLine($"Person {nameof(p.FirstName)}={p.FirstName}, {nameof(p.LastName)}={p.LastName} (after change)");
        }

        public static bool TryParseCoordinates(string coords, out int x, out int y)
        {
            x = 0;
            y = 0;

            if (string.IsNullOrWhiteSpace(coords))
            {
                return false;
            }

            string[] parts = coords.Split(",", StringSplitOptions.RemoveEmptyEntries);
            bool canParseX = (parts.Length > 0) && int.TryParse(parts[0], out x);
            bool canParseY = (parts.Length > 1) && int.TryParse(parts[1], out y);

            return canParseX || canParseY;
        }

        public static bool TryParsePerson(string fullName, out Person p)
        {
            p = null;

            if (string.IsNullOrEmpty(fullName))
            {
                return false;
            }

            string[] parts = fullName.Split(" ", StringSplitOptions.RemoveEmptyEntries);

            bool canParseFirstName = (parts.Length > 0);
            bool canParseLastName = (parts.Length > 1);

            if (canParseFirstName || canParseLastName)
            {
                string firstName = string.Empty;
                string lastName = string.Empty;

                if (canParseFirstName)
                {
                    firstName = parts[0];
                }

                if (canParseLastName)
                {
                    lastName = parts[1];
                }

                p = new Person { FirstName = firstName, LastName = lastName };
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
