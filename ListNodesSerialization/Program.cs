using System;
using System.IO;

namespace ListNodesSerialization
{
    public class Program
    {
        private static readonly Random Rand = new();
        
        static void Main()
        {
            Console.Write("Enter node count: ");
            int count;
            while (!int.TryParse(Console.ReadLine(), out count))
                Console.Write("Input Error! Enter an integer: ");

            // Populate the list with random data
            var initialList = PopulateList(count);

            // Reference random nodes
            AddRandomReferences(initialList, count);

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

        private static void AddRandomReferences(ListRandom listSerialization, int count)
        {
            ListNode current = listSerialization.Head;

            for (int i = 0; i < count; i++)
            {
                var randomId = Rand.Next(0, count);
                current.RandomId = randomId;
                
                var randomNode = listSerialization.Head;
                for (var j = 0; j < randomId; j++)
                {
                    randomNode = randomNode.Next;
                }

                current.Random = randomNode;
                current = current.Next;
            }
        }

        private static ListRandom PopulateList(int count)
        {
            var listSerialization = new ListRandom();

            for (var i = 0; i < count; i++)
            {
                var data = Rand.Next(0, 100).ToString();
                listSerialization.Add(data);
            }

            return listSerialization;
        }
    }
}