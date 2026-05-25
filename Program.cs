namespace ExceptionsDemo;

internal class Program
{
    static void Main(string[] args)
    {
        {
            Console.WriteLine("------- Start av programmet ***********");

            try
            {
                Console.WriteLine("Försöker läsa fil och räkna...");
                var path = Path.Combine(AppContext.BaseDirectory, "numbers.txt");
                var result = ProcessFile(path);
              
                Console.WriteLine($"\nResultat: {result}");
            }
            catch (FileNotFoundException ex)
            {
               
                Console.WriteLine($"Filen hittades inte: {ex.Message}");
            }
            catch (FormatException ex)
            {
               
                Console.WriteLine($"Formatfel: {ex.Message}");
            }
            catch (DivideByZeroException ex)
            {
                Console.WriteLine($"Kan inte dividera med noll: {ex.Message}");
            }
            catch (Exception ex)
            {
              
                Console.WriteLine($"Okänt fel: {ex.Message}");
            }
            finally
            {
           
                Console.WriteLine("Cleanup: Logging avslutat anrop.");
            }

            Console.WriteLine("Programmet avslutas normalt.");
        }

     
        static double ProcessFile(string fileName)
        {
           
            if (string.IsNullOrWhiteSpace(fileName))
            {
                throw new ArgumentException("Filnamn får inte vara tomt eller null.", nameof(fileName));
            }

            StreamReader? reader = null;
            try
            {
                reader = new StreamReader(fileName);

                string? line = reader.ReadLine();
                if (line == null)
                    throw new InvalidOperationException("Filen är tom.");

             
                int number = int.Parse(line); 

            
                return 100.0 / number;
            }
            catch (FormatException ex)
            {
                
                Console.WriteLine($"Formatfel i ProcessFile: {ex.Message}");
                // Vi kan välja att låta metoden "kasta upp" felet
                throw; // När du i `catch` bara vill logga/analysera,
                       // men låta anroparen (t.ex. en högre nivå i applikationen)
                       // bestämma hur man ska återhämta sig. 
            }
            catch (Exception ex)
            {
                // Om vi vill ge en mer meningsfull feltyp till anroparen
                throw new InvalidOperationException(
                "Det gick inte att processa filen.",
                ex); // InnerException = ursprunglig fel
            }
            finally
            {
                // Garanterad stängning av resurs
                reader?.Close();
                Console.WriteLine("finally i ProcessFile: StreamReader stängd.");
            }
        }
    }
}

