using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

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
            }
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

            if (count == string.Empty || Int32.Parse(count) == 0)
            {
                Head = null;
            }
            else
            {
                Count = Int32.Parse(count);
                listIndex.Capacity = Count;
                
                ListNode current;
                for (int i = 0; i < Count; i++)
                {
                    current = new ListNode { Data = reader.ReadLine() };
                    
                    if (Head is null)
                    {
                        Head = current;
                        Tail = Head;
                        listIndex.Add(Int32.Parse(reader.ReadLine()));
                    }
                    else
                    {
                        current.Previous = Tail;
                        Tail.Next = current;
                        Tail = current;
                        listIndex.Add(Int32.Parse(reader.ReadLine()));
                    }
                }
                
                current = Head;
                ListNode randomNode = current;
                int id = 0;
                for (int i = 0; i < Count; i++)
                {
                    if (listIndex[id] == i)
                    {
                        current.Random = randomNode;
                        current.RandomId = listIndex[id];
                        if (current.Next != null)
                            current = current.Next;
                        else
                            break;
                        id++;
                        i = -1;
                        randomNode = Head;
                    }
                    else
                        randomNode = randomNode.Next;
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

            var temp1 = this.Head;
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
    }
}