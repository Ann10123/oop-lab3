using System;
using System.Collections.Generic;
using System.IO;

class Program
{
    static void Main()
    {
        string[] fileNames = new string[]
        {
            "10.txt", "11.txt", "12.txt", "13.txt", "14.txt", "15.txt",
            "16.txt", "17.txt", "18.txt", "19.txt", "20.txt", "21.txt",
            "22.txt", "23.txt", "24.txt", "25.txt", "26.txt", "27.txt",
            "28.txt", "29.txt"
        };

        List<string> noFile = new List<string>();
        List<string> badData = new List<string>();
        List<string> overflow = new List<string>();
        List<int> products = new List<int>();

        foreach (string fileName in fileNames)
        {
            try
            {
                ProcessFile(fileName, products, overflow);
            }
            catch (FileNotFoundException)
            {
                noFile.Add(fileName);
            }
            catch (FormatException)
            {
                badData.Add(fileName);
            }
        }

        WriteToFile("no_file.txt", noFile);
        WriteToFile("bad_data.txt", badData);
        WriteToFile("overflow.txt", overflow);
        Console.WriteLine("Products:");
        int sum = 0;
        int count = 0;
        foreach (int product in products)
        {
            Console.WriteLine(product);
            sum += product;
            count++;
        }

        Console.WriteLine($"All sum: {sum}");
        Console.WriteLine($"Count: {count}");

        double average = sum/count;
        Console.WriteLine($"Average of products: {average}");
    }

    static void ProcessFile(string fileName, List<int> products, List<string> overflow)
    {
        string[] lines = File.ReadAllLines(fileName);

        int num1 = ParseInt(lines[0]);
        int num2 = ParseInt(lines[1]);

        try
        {
            checked
            {
                int product = num1 * num2;
                products.Add(product);
            }
        }
        catch (OverflowException)
        {
            overflow.Add(fileName);
        }
    }

    static int ParseInt(string line)
    {
        if (!int.TryParse(line, out int result))
        {
            throw new FormatException(); 
        }
        return result;
    }

    static void WriteToFile(string fileName, List<string> lines)
    {
        File.WriteAllLines(fileName, lines);
    }
}

