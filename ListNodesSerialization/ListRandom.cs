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
            {
                writer.Write(string.Empty);
                return;
            }

            writer.WriteLine(Count);
            do
            {
                writer.WriteLine(current.Data);
                if (current.Random != null)
                    writer.WriteLine(current.Random.Data.GetHashCode());
                else
                    writer.WriteLine(-1);
                
                current = current.Next;
            } while (current != null);
        }


        public void Deserialize(Stream s)
        {
            using var reader = new StreamReader(s);
            var count = reader.ReadLine() ?? string.Empty;

            if (count == string.Empty || int.Parse(count) == 0) return;

            Count = int.Parse(count);
            var hashArray = new int[Count];
            
            // Restoring the list of nodes
            ListNode current;
            for (var i = 0; i < Count; i++)
            {
                current = new ListNode { Data = reader.ReadLine() };

                if (Head is null)
                {
                    Head = current;
                    Tail = Head;
                    hashArray[i] = int.Parse(reader.ReadLine()!);
                }
                else
                {
                    current.Previous = Tail;
                    Tail.Next = current;
                    Tail = current;
                    hashArray[i] = int.Parse(reader.ReadLine()!);
                }
            }

            // Restoring links to a random nodes
            current = Head;
            var randomNode = current;
            for (var i = 0; i < Count; i++)
            {
                do
                {
                    if (hashArray[i] == randomNode.Data.GetHashCode() || hashArray[i] == -1)
                    {
                        current.Random = hashArray[i] == randomNode.Data.GetHashCode() ? randomNode : null;
                        current = current.Next;
                        break;
                    }

                    randomNode = randomNode.Next;
                } while (randomNode != null);

                randomNode = Head;
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

            var isEqual = true;

            while (isEqual)
            {
                if (temp1 == null && temp2 == null)
                    break;
                isEqual = temp1!.Data.GetHashCode() == temp2.Data.GetHashCode() &&
                          temp1.Random?.Data.GetHashCode() == temp2.Random?.Data.GetHashCode();
                temp1 = temp1.Next;
                temp2 = temp2.Next;
            }

            return isEqual;
        }

        public void AddRandomReferences()
        {
            Random rand = new();
            var current = Head;

            for (var i = 0; i < Count; i++)
            {
                var randomId = rand.Next(-1, Count);
                if (randomId == -1)
                {
                    current.Random = null;
                    current = current.Next;
                }
                else
                {
                    var randomNode = Head;
                    for (var j = 0; j < randomId; j++) randomNode = randomNode.Next;

                    current.Random = randomNode;
                    current = current.Next;
                }
            }
        }
    }
}