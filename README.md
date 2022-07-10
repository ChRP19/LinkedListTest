# ListNodesSerialization

Реализуйте функции сериализации и десериализации двусвязного списка, заданного следующим образом:

    class ListNode
    {
      public ListNode Prev;
      public ListNode Next;
      public ListNode Rand; // произвольный элемент внутри списка
      public string Data;
    }

    class ListRandom
    {
      public ListNode Head;
      public ListNode Tail;
      public int Count;

      public void Serialize(Stream s)
      {
      }

      public void Deserialize(Stream s)
      {
      }
    }

Примечание: сериализация подразумевает сохранение и восстановление полной структуры списка, включая взаимное соотношение его элементов между собой.
Напишите программу, демонстрирующую работу реализованных функций сериализации и десериализации на небольшом наборе тестовых данных (списке из нескольких элементов).
Тест нужно выполнить без использования библиотек/стандартных средств сериализации.
