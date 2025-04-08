class Program
{
    static void Main(string[] args)
    {
        Console.WriteLine("Witaj w programie wyszukującym indeks równowagi w tablicy!");
        Console.WriteLine("Podaj liczby całkowite oddzielone spacją:");

        // Wczytanie danych wejściowych od użytkownika
        string input = Console.ReadLine();
        int[] nums;

        try
        {
            nums = input.Split(' ').Select(int.Parse).ToArray();
        }
        catch
        {
            Console.WriteLine("Podano nieprawidłowe dane. Upewnij się, że wprowadzasz liczby całkowite oddzielone spacją.");
            Console.WriteLine("Naciśnij dowolny klawisz, aby zakończyć...");
            Console.ReadKey();
            return;
        }

        // Znalezienie indeksu równowagi
        int equilibriumIndex = FindEquilibriumIndex(nums);

        // Wyświetlenie wyniku
        if (equilibriumIndex != -1)
        {
            Console.WriteLine($"Znaleziono indeks równowagi: {equilibriumIndex}");
        }
        else
        {
            Console.WriteLine("Nie znaleziono indeksu równowagi w podanej tablicy.");
        }

        // Zabezpieczenie przed zamknięciem konsoli
        Console.WriteLine("Naciśnij dowolny klawisz, aby zakończyć...");
        Console.ReadKey();
    }

    /******************************************************
    nazwa funkcji: FindEquilibriumIndex
    typ zwracany: int, indeks równowagi lub -1, jeśli nie istnieje
    informacje: Funkcja implementuje algorytm wyszukiwania indeksu równowagi w tablicy.
                Indeks równowagi to taki, dla którego suma elementów po lewej stronie
                jest równa sumie elementów po prawej stronie.
    argumenty: nums (int[]) - tablica liczb całkowitych
    autor: nic00la1
    ******************************************************/
    static int FindEquilibriumIndex(int[] nums)
    {
        int totalSum = nums.Sum();
        int leftSum = 0;

        for (int i = 0; i < nums.Length; i++)
        {
            // Oblicz sumę po prawej stronie
            int rightSum = totalSum - leftSum - nums[i];

            if (leftSum == rightSum)
            {
                return i; // Znaleziono indeks równowagi
            }

            // Aktualizacja sumy po lewej stronie
            leftSum += nums[i];
        }

        return -1; // Brak indeksu równowagi
    }
}
