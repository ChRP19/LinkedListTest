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
            var listNodes = new Dictionary<ListNode, int>();
            var current = Head;

            using var writer = new BinaryWriter(s);

            for (int i = 0; i < Count; i++)
            {
                listNodes.Add(current, i);
                current = current.Next;
            }
            
            current = Head;
            writer.Write(Count);
            for (int i = 0; i < Count; i++)
            {
                writer.Write(current.Data);
                var randomIndex = current.Random is null ? -1 : listNodes[current.Random];
                writer.Write(randomIndex);

                current = current.Next;
            } 
        }


        public void Deserialize(Stream s)
        {
            using var reader = new BinaryReader(s);
            
            Count = reader.ReadInt32();
            var listNodes = new ListNode[Count];
            var randomIndexes = new int[Count];
            
            // Restoring the list of nodes
            ListNode current;
            for (int i = 0; i < Count; i++)
            {
                current = new ListNode { Data = reader.ReadString() };

                if (Head is null)
                {
                    Head = current;
                    Tail = Head;
                    listNodes[i] = current;
                    randomIndexes[i] = reader.ReadInt32();
                }
                else
                {
                    current.Previous = Tail;
                    Tail.Next = current;
                    Tail = current;
                    listNodes[i] = current;
                    randomIndexes[i] = reader.ReadInt32();
                }
            }

            // Restoring links to a random nodes
            current = Head;
            for (int i = 0; i < Count; i++)
            {
                var randomIndex = randomIndexes[i];
                if (randomIndex != -1)
                {
                    current.Random = listNodes[randomIndex];
                }
                current = current.Next;
            }
        }

        public void Add(string data)
        {
            var newNode = new ListNode { Data = data };

            if (Head is null)
            {
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
                isEqual = temp1.Data.GetHashCode() == temp2.Data.GetHashCode() &&
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

            for (int i = 0; i < Count; i++)
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
                    for (int j = 0; j < randomId; j++) randomNode = randomNode.Next;

                    current.Random = randomNode;
                    current = current.Next;
                }
            }
        }
    }
}