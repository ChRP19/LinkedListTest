using System;
using System.Collections.Generic;
using System.IO;

namespace ListNodesSerialization
{
    class ListRandom
    {
        public ListNode Head;
        public ListNode Tail;
        public int Count;

        public void Serialize(Stream s)
        {
            var current = Head;

            using var writer = new StreamWriter(s);
            if (current == null)
                writer.Write(string.Empty);
            else
            {
                writer.WriteLine(Count);
                do
                {
                    writer.WriteLine(current.Data);
                    writer.WriteLine(current.RandomId);
                    current = current.Next;
                } while (current != null);
            }
        }

        public void Deserialize(Stream s)
        {
            var listIndex = new List<int>();

            using var reader = new StreamReader(s);
            string count = reader.ReadLine() ?? string.Empty;

            if (count != string.Empty && int.Parse(count) != 0)
            {
                Count = int.Parse(count);
                listIndex.Capacity = Count;

                // Restoring the list of nodes
                ListNode current;
                for (int i = 0; i < Count; i++)
                {
                    current = new ListNode { Data = reader.ReadLine() };

                    if (Head is null)
                    {
                        Head = current;
                        Tail = Head;
                        listIndex.Add(int.Parse(reader.ReadLine()));
                    }
                    else
                    {
                        current.Previous = Tail;
                        Tail.Next = current;
                        Tail = current;
                        listIndex.Add(int.Parse(reader.ReadLine()));
                    }
                }

                // Restoring links to a random nodes
                current = Head;
                ListNode randomNode = current;
                int id = 0;
                for (int i = 0; i < Count; i++)
                {
                    if (listIndex[id] != -1 && listIndex[id] != i)
                    {
                        randomNode = randomNode.Next;
                        continue;
                    }

                    current.Random = listIndex[id] == i ? randomNode : null;
                    current.RandomId = listIndex[id];
                    if (current.Next != null)
                        current = current.Next;
                    else
                        break;
                    id++;
                    i = -1;
                    randomNode = Head;
                }
            }
        }

        public void Add(string data)
        {
            var newNode = new ListNode { Data = data };

            if (Head is null)
            {
                newNode.Data = "Head";
                Head = newNode;
                Tail = Head;
            }
            else
            {
                newNode.Previous = Tail;
                Tail.Next = newNode;
                Tail = newNode;
            }
        }

        public override bool Equals(object obj)
        {
            if (obj is not ListRandom other)
                return false;

            var temp1 = Head;
            var temp2 = other.Head;

            bool isEqual = true;

            while (isEqual)
            {
                if (temp1 == null && temp2 == null)
                    break;
                isEqual = temp1.Data == temp2.Data;
                temp1 = temp1.Next;
                temp2 = temp2.Next;
            }

            return isEqual;
        }

        public void AddRandomReferences()
        {
            Random rand = new();
            var current = Head;

            for (int i = 0; i < Count; i++)
            {
                var randomId = rand.Next(-1, Count);
                if (randomId == -1)
                {
                    current.Random = null;
                    current.RandomId = randomId;
                    current = current.Next;
                }
                else
                {
                    current.RandomId = randomId;

                    var randomNode = Head;
                    for (var j = 0; j < randomId; j++)
                    {
                        randomNode = randomNode.Next;
                    }

                    current.Random = randomNode;
                    current = current.Next;
                }
            }
        }
    }
}