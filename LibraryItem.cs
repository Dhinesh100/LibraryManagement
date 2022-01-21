using System;
using System.Collections.Generic;

namespace LibraryManagementVersion2
{
    class LibraryItem
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public int NumberOfCopies { get; set; }
        public string Type { get; set; }
        public string Author { get; set; }

        public void Display(string type, List<LibraryItem> items)
        {
            if (items.Count > 0)
            {
                Console.WriteLine();
                foreach (LibraryItem item in items)
                {
                    Console.WriteLine("ID: {0} | Name: {1} | Number Of Copies: {2} | Type: {3} | Author: {4}", item.ID, item.Name, item.NumberOfCopies, item.Type, item.Author);
                }
            }
            else
            {
                Console.WriteLine("\nSorry! No {0} found in the library, please add one", type);
            }
        }
    }
}
