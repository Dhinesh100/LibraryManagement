using System;
using System.Collections.Generic;

namespace LibraryManagementVersion2
{
    class BorrowItem
    {
        public string NameOfItem { get; set; }
        public string TypeOfItem { get; set; }
        public string NameOfMember { get; set; }
        public int NumberOfItems { get; set; }

        public string BorrowedDate { get; set; }

        public void Display(List<BorrowItem> items)
        {
            if (items.Count > 0)
            {
                Console.WriteLine();
                foreach (BorrowItem item in items)
                {
                    Console.WriteLine("Name of Member: {0} | Name Of Item Borrowed: {1} | Type of Item Borrowed: {2} | Number of Items Borrowed: {3} | Date Borrowed {4}", item.NameOfMember, item.NameOfItem, item.TypeOfItem, item.NumberOfItems, item.BorrowedDate);
                }
            }
            else
            {
                Console.WriteLine("\nSorry! No Member has borrowed from the library!");
            }
        }
    }
}
