using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using System.Xml.Linq;

namespace LibraryManagementVersion2
{
    public delegate void AddToLibrary(string Name, int NumberOfCopies, string Type, string Author);
    public delegate void AddDelegate();
    public delegate void AddToMembership(string Name, int Age, string Category, string Gender);
    public delegate void BorrowDelegate(string Type);
    public delegate void BorrowAnything(string NameOfItem, string TypeOfItem, string NameOfMember, string BorrowedDate, int NumberOfItems);
    class Program
    {
        public static List<LibraryItem> books = new List<LibraryItem>();
        public static List<LibraryItem> newspapers = new List<LibraryItem>();
        public static List<MemberItem> members = new List<MemberItem>();
        public static List<BorrowItem> borrow = new List<BorrowItem>();
        public static List<ReturnItem> returned = new List<ReturnItem>();

        public static LibraryItemCount libItemCount = new LibraryItemCount();

        public static string BookPath = @"C:\Users\Dell\source\repos\LibraryManagementVersion2\LibraryManagementVersion2\LocalDatabase\BooksStorage.xml";
        public static string NewspaperPath = @"C:\Users\Dell\source\repos\LibraryManagementVersion2\LibraryManagementVersion2\LocalDatabase\NewspapersStorage.xml";
        public static string MemberPath = @"C:\Users\Dell\source\repos\LibraryManagementVersion2\LibraryManagementVersion2\LocalDatabase\MembersStorage.xml";
        public static string BorrowPath = @"C:\Users\Dell\source\repos\LibraryManagementVersion2\LibraryManagementVersion2\LocalDatabase\BorrowStorage.xml";
        public static string ReturnedPath = @"C:\Users\Dell\source\repos\LibraryManagementVersion2\LibraryManagementVersion2\LocalDatabase\ReturnedStorage.xml";

        static void Main(string[] args)
        {
            bool loop = true;

            Console.WriteLine("LIBRARY MANAGEMENT SYSTEM");
            Console.WriteLine("=========================");

            Program obj = new Program();
            LibraryItem lib = new LibraryItem();
            MemberItem mem = new MemberItem();
            BorrowItem br = new BorrowItem();

            AddDelegate add;
            BorrowDelegate borrow;

            if (File.Exists(Program.BookPath))
            {
                Program.books = (
                from x in XDocument.Load(BookPath).Root.Elements("Book")
                select new LibraryItem
                    {
                        ID = (int)x.Attribute("BookID"),
                        Name = (string)x.Element("Name"),
                        NumberOfCopies = (int)x.Element("NumberOfCopies"),
                        Type = (string)x.Element("Type"),
                        Author = (string)x.Element("Author")
                    }
                ).ToList();
            }

            if (File.Exists(Program.NewspaperPath))
            {
                Program.newspapers = (
                    from x in XDocument.Load(NewspaperPath).Root.Elements("Newspaper")
                    select new LibraryItem
                    {
                        ID = (int)x.Attribute("NewspaperID"),
                        Name = (string)x.Element("Name"),
                        NumberOfCopies = (int)x.Element("NumberOfCopies"),
                        Type = (string)x.Element("Type"),
                        Author = (string)x.Element("Author")
                    }
                ).ToList();
            }

            if (File.Exists(Program.MemberPath))
            {
                Program.members = (
                    from x in XDocument.Load(MemberPath).Root.Elements("Member")
                    select new MemberItem
                    {
                        Name = (string)x.Element("Gender"),
                        Age = (int)x.Element("Age"),
                        Category = (string)x.Element("Category"),
                        Gender = (string)x.Element("Gender")
                    }
                ).ToList();
            }

            if (File.Exists(Program.BorrowPath))
            {
                Program.borrow = (
                    from x in XDocument.Load(BorrowPath).Root.Elements("Borrow")
                    select new BorrowItem
                    {
                        NameOfItem = (string)x.Element("NameOfItem"),
                        TypeOfItem = (string)x.Element("TypeOfItem"),
                        NameOfMember = (string)x.Element("NameOfMember"),
                        NumberOfItems = (int)x.Element("NumberOfItems"),
                        BorrowedDate = (string)x.Element("BorrowedDate")
                    }
                ).ToList();
            }

            if (File.Exists(Program.ReturnedPath))
            {
                Program.returned = (
                    from x in XDocument.Load(ReturnedPath).Root.Elements("Return")
                    select new ReturnItem
                    {
                        NameOfItem = (string)x.Element("NameOfItem"),
                        TypeOfItem = (string)x.Element("TypeOfItem"),
                        NameOfMember = (string)x.Element("NameOfMember"),
                        NumberOfItems = (int)x.Element("NumberOfItems"),
                        BorrowedDate = (string)x.Element("BorrowedDate"),
                        ReturnedDate = (string)x.Element("ReturnedDate")
                    }
                ).ToList();
            }

            do
            {
                Console.WriteLine("\n1. Add Book");
                Console.WriteLine("2. Add Newspaper");
                Console.WriteLine("3. Display List of Books");
                Console.WriteLine("4. Display List of Newspapers");
                Console.WriteLine("5. Borrow Book");
                Console.WriteLine("6. Borrow Newspaper");
                Console.WriteLine("7. Return Book");
                Console.WriteLine("8. Return Newspaper");
                Console.WriteLine("9. Add Membership");
                Console.WriteLine("10. Display List of People having Membership");
                Console.WriteLine("11. Display List of People having Books/Newspaper");
                Console.WriteLine("12. Exit");

                Console.Write("\nEnter your option: ");
                int option = Int32.Parse(Console.ReadLine());

                switch (option)
                {
                    case 1:
                        add = obj.AddBook;
                        add.Invoke();
                        break;
                    case 2:
                        add = obj.AddNewsPaper;
                        add.Invoke();
                        break;
                    case 3:
                        lib.Display("Book", Program.books);
                        break;
                    case 4:
                        lib.Display("Newspaper", Program.newspapers);
                        break;
                    case 5:
                        borrow = obj.BorrowItem;
                        borrow.Invoke("Book");
                        break;
                    case 6:
                        borrow = obj.BorrowItem;
                        borrow.Invoke("Newspaper");
                        break;
                    case 7:
                        borrow = obj.ReturnItem;
                        borrow.Invoke("Book");
                        break;
                    case 8:
                        borrow = obj.ReturnItem;
                        borrow.Invoke("Newspaper");
                        break;
                    case 9:
                        add = obj.AddMember;
                        add.Invoke();
                        break;
                    case 10:
                        mem.Display(Program.members);
                        break;
                    case 11:
                        br.Display(Program.borrow);
                        break;
                    case 12:
                        loop = false;
                        break;
                    default:
                        Console.WriteLine("INVALID OPTION!!!");
                        break;
                }
            } while (loop);
        }

        public void AddBook()
        {
            int total = libItemCount.TotalNoOfBooks;

            Console.Write("\nEnter the Name of the book: ");
            string name = Console.ReadLine();
            Console.Write("Enter the Author of the book named {0}: ", name);
            string author = Console.ReadLine();
            Console.Write("Enter the Number of Copies of the book named {0} written by {1}: ", name, author);
            int numberOfCopies = Int32.Parse(Console.ReadLine());

            AddToLibrary atlib = (string Name, int NumberOfCopies, string Type, string Author) =>
            {
                foreach (LibraryItem item in Program.books)
                {
                    if (string.Equals(item.Name, Name, StringComparison.OrdinalIgnoreCase) && string.Equals(item.Author, Author, StringComparison.OrdinalIgnoreCase))
                    {
                        item.NumberOfCopies += NumberOfCopies;

                        XDocument xdoc = XDocument.Load(BookPath);
                        xdoc.Element("Books")
                        .Elements("Book")
                        .Where(X => X.Attribute("BookID").Value == Convert.ToString(item.ID))
                        .SingleOrDefault()
                        .SetElementValue("NumberOfCopies", item.NumberOfCopies);

                        xdoc.Save(Program.BookPath);

                        Console.WriteLine("\nBook successfuly added to the library!");
                        return;
                    }
                }

                LibraryItem libItem = new LibraryItem
                {
                    ID = total + 1,
                    Name = Name,
                    NumberOfCopies = NumberOfCopies,
                    Type = Type,
                    Author = Author
                };

                Program.books.Add(libItem);
                libItemCount.TotalNoOfBooks = total+1;

                if (File.Exists(Program.BookPath))
                {
                    XDocument xdoc = XDocument.Load(BookPath);
                    xdoc.Element("Books").Add(
                       new XElement("Book", new XAttribute("BookID", libItem.ID),
                           new XElement("Name", libItem.Name),
                           new XElement("NumberOfCopies", libItem.NumberOfCopies),
                           new XElement("Type", libItem.Type),
                           new XElement("Author", libItem.Author)
                    ));

                    xdoc.Save(Program.BookPath);
                }
                else
                {
                    XDocument xdoc = new XDocument(
                        new XDeclaration("1.0", "utf-8", "yes"),
                        new XComment("XML Document to store Library Books"),
                        new XElement("Books",
                            new XElement("Book", new XAttribute("BookID", libItem.ID),
                                new XElement("Name", libItem.Name),
                                new XElement("NumberOfCopies", libItem.NumberOfCopies),
                                new XElement("Type", libItem.Type),
                                new XElement("Author", libItem.Author)
                            )
                        )
                    );

                    xdoc.Save(Program.BookPath);
                }

                Console.WriteLine("\nBook successfuly added to the library!");
            };
            atlib.Invoke(name, numberOfCopies, "Book", author);
        }
        public void AddNewsPaper()
        {
            int total = libItemCount.TotalNoOfNewspapers;

            Console.Write("\nEnter the Title of the Newspaper: ");
            string name = Console.ReadLine();
            Console.Write("Enter the Author of the newspaper named {0}: ", name);
            string author = Console.ReadLine();
            Console.Write("Enter the Number of Copies of the newspaper named {0} editted by {1}: ", name, author);
            int numberOfCopies = Int32.Parse(Console.ReadLine());

            AddToLibrary atlib = (string Name, int NumberOfCopies, string Type, string Author) =>
            {
                foreach (LibraryItem item in Program.newspapers)
                {
                    if (string.Equals(item.Name, Name, StringComparison.OrdinalIgnoreCase) && string.Equals(item.Author, Author, StringComparison.OrdinalIgnoreCase))
                    {
                        item.NumberOfCopies += NumberOfCopies;

                        XDocument xdoc = XDocument.Load(NewspaperPath);
                        xdoc.Element("Newspapers")
                        .Elements("Newspaper")
                        .Where(X => X.Attribute("NewspaperID").Value == Convert.ToString(item.ID))
                        .SingleOrDefault()
                        .SetElementValue("NumberOfCopies", item.NumberOfCopies);

                        xdoc.Save(Program.NewspaperPath);

                        Console.WriteLine("\nNewspaper successfuly added to the library!");
                        return;
                    }
                }

                LibraryItem libItem = new LibraryItem
                {
                    ID = total + 1,
                    Name = Name,
                    NumberOfCopies = NumberOfCopies,
                    Type = Type,
                    Author = Author
                };

                Program.newspapers.Add(libItem);
                libItemCount.TotalNoOfNewspapers = total+1;

                if (File.Exists(Program.NewspaperPath))
                {
                    XDocument xdoc = XDocument.Load(NewspaperPath);
                    xdoc.Element("Newspapers").Add(
                       new XElement("Newspaper", new XAttribute("NewspaperID", libItem.ID),
                           new XElement("Name", libItem.Name),
                           new XElement("NumberOfCopies", libItem.NumberOfCopies),
                           new XElement("Type", libItem.Type),
                           new XElement("Author", libItem.Author)
                    ));

                    xdoc.Save(Program.NewspaperPath);
                }
                else
                {
                    XDocument xdoc = new XDocument(
                        new XDeclaration("1.0", "utf-8", "yes"),
                        new XComment("XML Document to store Library Books"),
                        new XElement("Newspapers",
                            new XElement("Newspaper", new XAttribute("NewspaperID", libItem.ID),
                                new XElement("Name", libItem.Name),
                                new XElement("NumberOfCopies", libItem.NumberOfCopies),
                                new XElement("Type", libItem.Type),
                                new XElement("Author", libItem.Author)
                            )
                        )
                    );

                    xdoc.Save(Program.NewspaperPath);
                }

                Console.WriteLine("\nNewspaper successfuly added to the library!");
            };
            atlib.Invoke(name, numberOfCopies, "Newspaper", author);
        }

        public void AddMember()
        {
            Console.Write("\nEnter the name of the new member: ");
            string name = Console.ReadLine();
            Console.Write("Enter the age of {0}: ", name);
            int age = Int32.Parse(Console.ReadLine());
            Console.Write("Enter the gender of {0}: ", name);
            string gender = Console.ReadLine();
            string category = string.Empty;

            if (age < 10)
            {
                Console.WriteLine("\nMember age should not be less than 10");
                return;
            }
            else if (age >= 10 && age < 22)
            {
                category = "Student";
            }
            else if (age >= 22 && age <= 60)
            {
                category = "Employee";
            }
            else
            {
                category = "Retired";
            }

            AddToMembership atmem = (string Name, int Age, string Category, string Gender) =>
            {
                foreach (MemberItem item in Program.members)
                {
                    if (string.Equals(item.Name, Name, StringComparison.OrdinalIgnoreCase))
                    {
                        Console.WriteLine("\nMember named {0} already exists!", Name.ToUpper());
                        return;
                    }
                }

                MemberItem libItem = new MemberItem
                {
                    Name = Name,
                    Age = Age,
                    Category = Category,
                    Gender = Gender
                };

                Program.members.Add(libItem);

                if (File.Exists(Program.MemberPath))
                {
                    XDocument xdoc = XDocument.Load(MemberPath);
                    xdoc.Element("Members").Add(
                       new XElement("Member",
                           new XElement("Name", libItem.Name),
                           new XElement("Age", libItem.Age),
                           new XElement("Category", libItem.Category),
                           new XElement("Gender", libItem.Gender)
                    ));

                    xdoc.Save(Program.MemberPath);
                }
                else
                {
                    XDocument xdoc = new XDocument(
                        new XDeclaration("1.0", "utf-8", "yes"),
                        new XComment("XML Document to store Library Members"),
                        new XElement("Members",
                            new XElement("Member",
                           new XElement("Name", libItem.Name),
                           new XElement("Age", libItem.Age),
                           new XElement("Category", libItem.Category),
                           new XElement("Gender", libItem.Gender)
                            )
                        )
                    );

                    xdoc.Save(Program.MemberPath);
                }

                Console.WriteLine("\nSuccessfully added a new member!");
            };

            atmem.Invoke(name, age, category, gender);
        }

        public void BorrowItem(string type)
        {
            Console.Write("\nEnter the name of the member: ");
            string name = Console.ReadLine();
            Console.Write("Enter the Name of the {0} to be borrowed by {1}: ", type, name);
            string nameOfItem = Console.ReadLine();
            Console.Write("Enter the Date of Borrow: ");
            string date = Console.ReadLine();
            Console.Write("Enter the Number of Items to be borrowed: ");
            int numberOfItems = Int32.Parse(Console.ReadLine());

            BorrowAnything borrowAnything = (string NameOfItem, string TypeOfItem, string NameOfMember, string BorrowedDate, int NumberOfItems) =>
            {
                if (type == "Book")
                {
                    var memberPresent = members.FirstOrDefault(x => string.Equals(x.Name, NameOfMember, StringComparison.OrdinalIgnoreCase));

                    if (memberPresent == null)
                    {
                        Console.WriteLine("\nMember named {0} not present, please add one", NameOfMember);
                        return;
                    }

                    foreach (LibraryItem item in Program.books)
                    {
                        if (string.Equals(item.Name, NameOfItem, StringComparison.OrdinalIgnoreCase))
                        {
                            item.NumberOfCopies -= NumberOfItems;

                            BorrowItem libItem = new BorrowItem
                            {
                                NameOfItem = NameOfItem,
                                TypeOfItem = TypeOfItem,
                                NameOfMember = NameOfMember,
                                NumberOfItems = NumberOfItems,
                                BorrowedDate = BorrowedDate
                            };

                            Program.borrow.Add(libItem);

                            if (File.Exists(Program.BorrowPath))
                            {
                                XDocument xdoc = XDocument.Load(BorrowPath);
                                xdoc.Element("Borrows").Add(
                                   new XElement("Borrow",
                                       new XElement("NameOfItem", libItem.NameOfItem),
                                       new XElement("TypeOfItem", libItem.TypeOfItem),
                                       new XElement("NameOfMember", libItem.NameOfMember),
                                       new XElement("NumberOfItems", libItem.NumberOfItems),
                                       new XElement("BorrowedDate", libItem.BorrowedDate)
                                ));

                                xdoc.Save(Program.BorrowPath);
                            }
                            else
                            {
                                XDocument xdoc = new XDocument(
                                    new XDeclaration("1.0", "utf-8", "yes"),
                                    new XComment("XML Document to store Library Borrows"),
                                    new XElement("Borrows",
                                        new XElement("Borrow",
                                           new XElement("NameOfItem", libItem.NameOfItem),
                                           new XElement("TypeOfItem", libItem.TypeOfItem),
                                           new XElement("NameOfMember", libItem.NameOfMember),
                                           new XElement("NumberOfItems", libItem.NumberOfItems),
                                           new XElement("BorrowedDate", libItem.BorrowedDate)
                                        )
                                    )
                                );

                                xdoc.Save(Program.BorrowPath);
                            }

                            Console.WriteLine("\nBook named {0} successfuly borrowed by {1}!", NameOfItem, NameOfMember);
                            return;
                        }
                    }

                    Console.WriteLine("\nBook named {0} does not exist in the library!", NameOfItem);
                }
                else
                {
                    var memberPresent = members.FirstOrDefault(x => string.Equals(x.Name, NameOfMember, StringComparison.OrdinalIgnoreCase));

                    if (memberPresent == null)
                    {
                        Console.WriteLine("\nMember named {0} not present, please add one", NameOfMember);
                        return;
                    }

                    foreach (LibraryItem item in Program.newspapers)
                    {
                        if (string.Equals(item.Name, NameOfItem, StringComparison.OrdinalIgnoreCase))
                        {
                            item.NumberOfCopies -= NumberOfItems;

                            BorrowItem libItem = new BorrowItem
                            {
                                NameOfItem = NameOfItem,
                                TypeOfItem = TypeOfItem,
                                NameOfMember = NameOfMember,
                                NumberOfItems = NumberOfItems,
                                BorrowedDate = BorrowedDate
                            };

                            Program.borrow.Add(libItem);

                            if (File.Exists(Program.BorrowPath))
                            {
                                XDocument xdoc = XDocument.Load(BorrowPath);
                                xdoc.Element("Borrows").Add(
                                   new XElement("Borrow",
                                       new XElement("NameOfItem", libItem.NameOfItem),
                                       new XElement("TypeOfItem", libItem.TypeOfItem),
                                       new XElement("NameOfMember", libItem.NameOfMember),
                                       new XElement("NumberOfItems", libItem.NumberOfItems),
                                       new XElement("BorrowedDate", libItem.BorrowedDate)
                                ));

                                xdoc.Save(Program.BorrowPath);
                            }
                            else
                            {
                                XDocument xdoc = new XDocument(
                                    new XDeclaration("1.0", "utf-8", "yes"),
                                    new XComment("XML Document to store Library Borrows"),
                                    new XElement("Borrows",
                                        new XElement("Borrow",
                                           new XElement("NameOfItem", libItem.NameOfItem),
                                           new XElement("TypeOfItem", libItem.TypeOfItem),
                                           new XElement("NameOfMember", libItem.NameOfMember),
                                           new XElement("NumberOfItems", libItem.NumberOfItems),
                                           new XElement("BorrowedDate", libItem.BorrowedDate)
                                        )
                                    )
                                );

                                xdoc.Save(Program.BorrowPath);
                            }

                            Console.WriteLine("\nBook named {0} successfuly borrowed by {1}!", NameOfItem, NameOfMember);
                            return;
                        }
                    }

                    Console.WriteLine("\nBook named {0} does not exist in the library!", NameOfItem);
                }
            };

            borrowAnything.Invoke(nameOfItem, type, name, date, numberOfItems);
        }

        public void ReturnItem(string type)
        {
            Console.Write("\nEnter the name of the member: ");
            string name = Console.ReadLine();
            Console.Write("Enter the Name of the {0} to be returned by {1}: ", type, name);
            string nameOfItem = Console.ReadLine();
            Console.Write("Enter the Date of Return: ");
            string date = Console.ReadLine();
            Console.Write("Enter the Number of Items to be returned: ");
            int numberOfItems = Int32.Parse(Console.ReadLine());

            BorrowAnything returnSomething = (string NameOfItem, string TypeOfItem, string NameOfMember, string ReturnedDate, int NumberOfItems) =>
            {
                if (type == "Book")
                {
                    var memberPresent = members.FirstOrDefault(x => string.Equals(x.Name, NameOfMember, StringComparison.OrdinalIgnoreCase));

                    if (memberPresent == null)
                    {
                        Console.WriteLine("\nMember named {0} not present, please add one", NameOfMember);
                        return;
                    }

                    foreach (BorrowItem item in Program.borrow)
                    {
                        if (string.Equals(item.NameOfItem, NameOfItem, StringComparison.OrdinalIgnoreCase) && string.Equals(item.NameOfMember, NameOfMember, StringComparison.OrdinalIgnoreCase))
                        {
                            if (item.NumberOfItems >= NumberOfItems)
                            {
                                var itemIndex = books.FindIndex(x => string.Equals(x.Name, NameOfItem, StringComparison.OrdinalIgnoreCase));
                                books[itemIndex].NumberOfCopies += NumberOfItems;

                                ReturnItem libItem = new ReturnItem
                                {
                                    NameOfItem = NameOfItem,
                                    TypeOfItem = TypeOfItem,
                                    NameOfMember = NameOfMember,
                                    NumberOfItems = NumberOfItems,
                                    ReturnedDate = ReturnedDate,
                                    BorrowedDate = item.BorrowedDate
                                };

                                Program.returned.Add(libItem);

                                if (item.NumberOfItems == NumberOfItems)
                                {
                                    Program.borrow.Remove(item);

                                    XDocument xdoc = XDocument.Load(BorrowPath);
                                    xdoc.Root.Elements("Borrow")
                                    .Where(X => (X.Element("NameOfItem").Value == Convert.ToString(item.NameOfItem)) && (X.Element("NameOfMember").Value == Convert.ToString(item.NameOfMember)))
                                    .FirstOrDefault()
                                    .Remove();
                                }
                                else
                                {
                                    item.NumberOfItems -= NumberOfItems;

                                    XDocument xdoc = XDocument.Load(BorrowPath);
                                    xdoc.Element("Borrows")
                                    .Elements("Borrow")
                                    .Where(X => (X.Element("NameOfItem").Value == Convert.ToString(item.NameOfItem)) && (X.Element("NameOfMember").Value == Convert.ToString(item.NameOfMember)))
                                    .SingleOrDefault()
                                    .SetElementValue("NumberOfItems", item.NumberOfItems);

                                    xdoc.Save(Program.BorrowPath);
                                }

                                if (File.Exists(Program.ReturnedPath))
                                {
                                    XDocument xdoc = XDocument.Load(ReturnedPath);
                                    xdoc.Element("Returns").Add(
                                       new XElement("Return",
                                           new XElement("NameOfItem", libItem.NameOfItem),
                                           new XElement("TypeOfItem", libItem.TypeOfItem),
                                           new XElement("NameOfMember", libItem.NameOfMember),
                                           new XElement("NumberOfItems", libItem.NumberOfItems),
                                           new XElement("BorrowedDate", libItem.BorrowedDate),
                                           new XElement("ReturnedDate", libItem.ReturnedDate)
                                    ));

                                    xdoc.Save(Program.ReturnedPath);
                                }
                                else
                                {
                                    XDocument xdoc = new XDocument(
                                        new XDeclaration("1.0", "utf-8", "yes"),
                                        new XComment("XML Document to store Library Returns"),
                                        new XElement("Returns",
                                            new XElement("Return",
                                               new XElement("NameOfItem", libItem.NameOfItem),
                                               new XElement("TypeOfItem", libItem.TypeOfItem),
                                               new XElement("NameOfMember", libItem.NameOfMember),
                                               new XElement("NumberOfItems", libItem.NumberOfItems),
                                               new XElement("BorrowedDate", libItem.BorrowedDate),
                                               new XElement("ReturnedDate", libItem.ReturnedDate)
                                        ))
                                    );

                                    xdoc.Save(Program.ReturnedPath);
                                }

                                Console.WriteLine("{0} has been returned successfully!", TypeOfItem);
                            }
                            else
                            {
                                Console.WriteLine("\nNumber of {0}s returned is more than borrowed, kindly check!", TypeOfItem);
                                return;
                            }

                            Console.WriteLine("\nBook named {0} successfuly returned by {1}!", NameOfItem, NameOfMember);
                            return;
                        }
                    }

                    Console.WriteLine("\nBook named {0} has not been borrowed from the library!", NameOfItem);
                }
                else
                {
                    var memberPresent = members.FirstOrDefault(x => string.Equals(x.Name, NameOfMember, StringComparison.OrdinalIgnoreCase));

                    if (memberPresent == null)
                    {
                        Console.WriteLine("\nMember named {0} not present, please add one", NameOfMember);
                        return;
                    }

                    foreach (BorrowItem item in Program.borrow)
                    {
                        if (string.Equals(item.NameOfItem, NameOfItem, StringComparison.OrdinalIgnoreCase) && string.Equals(item.NameOfMember, NameOfMember, StringComparison.OrdinalIgnoreCase))
                        {
                            if (item.NumberOfItems == NumberOfItems)
                            {
                                var itemIndex = books.FindIndex(x => string.Equals(x.Name, NameOfItem, StringComparison.OrdinalIgnoreCase));
                                books[itemIndex].NumberOfCopies += NumberOfItems;

                                ReturnItem libItem = new ReturnItem
                                {
                                    NameOfItem = NameOfItem,
                                    TypeOfItem = TypeOfItem,
                                    NameOfMember = NameOfMember,
                                    NumberOfItems = NumberOfItems,
                                    ReturnedDate = ReturnedDate,
                                    BorrowedDate = item.BorrowedDate
                                };

                                Program.returned.Add(libItem);

                                if (item.NumberOfItems == NumberOfItems)
                                {
                                    Program.borrow.Remove(item);
                                }
                                else
                                {
                                    item.NumberOfItems -= NumberOfItems;
                                }

                                if (File.Exists(Program.ReturnedPath))
                                {
                                    XDocument xdoc = XDocument.Load(ReturnedPath);
                                    xdoc.Element("Returns").Add(
                                       new XElement("Return",
                                           new XElement("NameOfItem", libItem.NameOfItem),
                                           new XElement("TypeOfItem", libItem.TypeOfItem),
                                           new XElement("NameOfMember", libItem.NameOfMember),
                                           new XElement("NumberOfItems", libItem.NumberOfItems),
                                           new XElement("BorrowedDate", libItem.BorrowedDate),
                                           new XElement("ReturnedDate", libItem.ReturnedDate)
                                    ));

                                    xdoc.Save(Program.ReturnedPath);
                                }
                                else
                                {
                                    XDocument xdoc = new XDocument(
                                        new XDeclaration("1.0", "utf-8", "yes"),
                                        new XComment("XML Document to store Library Borrows"),
                                        new XElement("Returns",
                                            new XElement("Return",
                                               new XElement("NameOfItem", libItem.NameOfItem),
                                               new XElement("TypeOfItem", libItem.TypeOfItem),
                                               new XElement("NameOfMember", libItem.NameOfMember),
                                               new XElement("NumberOfItems", libItem.NumberOfItems),
                                               new XElement("BorrowedDate", libItem.BorrowedDate),
                                               new XElement("ReturnedDate", libItem.ReturnedDate)

                                        ))
                                    );

                                    xdoc.Save(Program.ReturnedPath);
                                }

                                Console.WriteLine("{0} has been returned successfully!", TypeOfItem);
                            }
                            else
                            {
                                Console.WriteLine("\nNumber of {0}s returned is more than borrowed, kindly check!", TypeOfItem);
                            }

                            Console.WriteLine("\nBook named {0} successfuly returned by {1}!", NameOfItem, NameOfMember);
                            return;
                        }
                    }

                    Console.WriteLine("\nBook named {0} does not exist in the library!", NameOfItem);
                }
            };

            returnSomething.Invoke(nameOfItem, type, name, date, numberOfItems);
        }
    }
}