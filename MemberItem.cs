using System;
using System.Collections.Generic;
namespace LibraryManagementVersion2
{
    class MemberItem
    {
        public string Name;
        public int Age;
        public string Category;
        public string Gender;

        public void Display(List<MemberItem> members)
        {
            if (members.Count > 0)
            {
                Console.WriteLine();
                foreach (MemberItem item in members)
                {
                    Console.WriteLine("Name: {0} | Age: {1} | Category: {2} | Gender: {3}", item.Name, item.Age, item.Category, item.Gender);
                }
            }
            else
            {
                Console.WriteLine("\nSorry! No Member exists in the library, please add one");
            }
        }
    }
}
