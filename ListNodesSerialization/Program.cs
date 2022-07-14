using System;
using System.IO;

namespace ListNodesSerialization
{
    public class Program
    {
        static void Main()
        {
            Console.Write("Enter node count: ");
            int count;
            while (!int.TryParse(Console.ReadLine(), out count))
            {
                Console.WriteLine("Input Error! Enter an integer");
                Console.Write("Enter node count: ");
            }

            // Populate the list with random data
            var initialList = PopulateList(count);

            // Reference random nodes
            initialList.AddRandomReferences();

            // Serialize listRandom
            SerializeList(initialList);

            // Deserialize listRandom
            var resultList = DeserializeList();

            // Verification
            Console.WriteLine(initialList.Equals(resultList) ? "Success" : "Failure");

            Console.ReadKey();
        }

        private static ListRandom DeserializeList()
        {
            var listDeserialization = new ListRandom();

            using var fs = new FileStream("data.dat", FileMode.Open);
            listDeserialization.Deserialize(fs);

            return listDeserialization;
        }

        private static void SerializeList(ListRandom listSerialization)
        {
            using FileStream fs = new FileStream("data.dat", FileMode.Create);
            listSerialization.Serialize(fs);
        }
        
        private static ListRandom PopulateList(int count)
        {
            Random rand = new();
            var listSerialization = new ListRandom();
            listSerialization.Count = count;

            for (var i = 0; i < count; i++)
            {
                var data = rand.Next(0, 100).ToString();
                listSerialization.Add(data);
            }

            return listSerialization;
        }
    }
}