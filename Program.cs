using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryManagementSystem
{
    internal class Program
    {
        /// <summary>
        /// This is a console-based library management system that allows users to
        /// add, view, search, borrow and return books
        /// </summary>
        /// <param name="args"></param>
        static void Main(string[] args)
        {
            // create a list of dictionary
            List<Dictionary<string, string>> books = new List<Dictionary<string, string>>();
            bool exitLibrary = false;
            string userInput;
            char userOption;
            List<string> genres = new List<string>();

            try
            {
                while (!exitLibrary)
                {
                    // print options
                    Console.WriteLine("Press 'A' to add book");
                    Console.WriteLine("Press 'V' to view available books");
                    Console.WriteLine("Press 'S' to search book");
                    Console.WriteLine("Press 'B' to borrow book");
                    Console.WriteLine("Press 'R' to return book");
                    Console.WriteLine("Press 'E' to exit");
                    Console.WriteLine();

                    // take user option choice
                    Console.Write("Option: ");
                    userInput = Console.ReadLine().Trim();
                    char temporary;

                    // validate user option
                    while (!char.TryParse(userInput, out temporary))
                    {
                        Console.WriteLine("One character is expected");
                        Console.Write("Option: ");
                        userInput = Console.ReadLine().Trim();
                    }

                    userOption = Convert.ToChar(userInput.ToLower());

                    // call a function depending on the user option choice
                    switch (userOption)
                    {
                        case 'a':
                            AddBook(ref books, ref genres);
                            break;
                        case 'v':
                            ViewBooks(books, genres);
                            break;
                        case 's':
                            SearchBook(books);
                            break;
                        case 'b':
                            BorrowBook(books);
                            break;
                        case 'r':
                            ReturnBook(books);
                            break;
                        case 'e':
                            return;
                        default:
                            Console.WriteLine("Please only pick from options");
                            break;
                    }
                    Console.WriteLine();
                }
            } catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return;
            }
        }

        public static void AddBook(ref List<Dictionary<string, string>> books, ref List<string> genres)
        {
            // BigO notation here is 0(1)
            // It takes a constant amount of time to add a book to list regardless of list count
            try
            {
                // take and validate book details
                Console.Write("Enter title: ");
                String title = Console.ReadLine().Trim();

                while (string.IsNullOrWhiteSpace(title))
                {
                    Console.Write("Please enter a valid title: ");
                    title = Console.ReadLine().Trim();
                }

                Console.Write("Enter author: ");
                String author = Console.ReadLine().Trim();

                while (string.IsNullOrWhiteSpace(author))
                {
                    Console.Write("Please enter a valid author: ");
                    author = Console.ReadLine().Trim();
                }

                Console.Write("Enter publication year: ");
                String year = Console.ReadLine().Trim();

                while (string.IsNullOrWhiteSpace(year))
                {
                    Console.Write("Please enter a valid year: ");
                    year = Console.ReadLine().Trim();
                }

                Console.Write("Enter genre: ");
                String genre = Console.ReadLine().Trim();

                while (string.IsNullOrWhiteSpace(genre))
                {
                    Console.Write("Please enter a valid genre: ");
                    genre = Console.ReadLine().Trim();
                }

                genres.Add(genre);

                // create a new dictionary and add book details
                Dictionary<string, string> bookDetails = new Dictionary<string, string>();

                bookDetails.Add("title", title);
                bookDetails.Add("author", author);
                bookDetails.Add("year", year);
                bookDetails.Add("genre", genre);
                bookDetails.Add("status", "available");

                // add book details to list of books
                books.Add(bookDetails);
                Console.WriteLine($"{bookDetails["title"]} added to library");
            } catch (Exception) 
            {
                throw;
            }
        }

        public static void ViewBooks(List<Dictionary<string, string>> books, List<string> genres)
        {
            // BigO notation here is 0(n)
            // It prints all the books so the higher the list of books, the more time it takes
            try
            {
                // if list of books is empty, show user it is empty
                if (books.Count == 0)
                {
                    Console.WriteLine("Library catalogue empty");
                    return;
                }

                // if book list is not empty, users have two options
                Console.Write("Press '1' to view all books or '2' to select a genre: ");
                string optionInput = Console.ReadLine().Trim();

                if (optionInput == "1")
                {
                    int number = 1;
                    // print all books in list if user press '1'
                    foreach (var book in books)
                    {
                        if (book["status"] == "available")
                        {
                            Console.WriteLine($"{number}. {book["title"]} by {book["author"]} ({book["year"]})");
                            number++;
                        }
                    }
                }
                else if (optionInput == "2")
                {
                    // print available genres in book list
                    foreach (var genre in genres)
                    {
                        Console.WriteLine(genre);
                    }

                    Console.Write("Enter any of these genres to view books: ");
                    String genreOption = Console.ReadLine().Trim();

                    int number = 1;
                    // print books based on genre chosen by user
                    foreach (var book in books)
                    {
                        if (genreOption.ToLower() == book["genre"].ToLower() && book["status"] == "available")
                        {
                            Console.WriteLine($"{number}. {book["title"]} by {book["author"]} ({book["year"]})");
                            number++;
                        }
                    }
                }
            } catch (Exception) 
            {
                throw;
            }
        }

        public static void SearchBook(List<Dictionary<string, string>> books)
        {
            // BigO notation here is 0(n)
            // A linear search algorithm is used to search book; the higher the list count, the
            // more time it takes
            try
            {
                // take title of book input from user
                Console.Write("Enter title of book: ");
                string bookInput = Console.ReadLine();

                // validate the input
                while (string.IsNullOrWhiteSpace(bookInput))
                {
                    Console.Write("Enter a valid title: ");
                    bookInput = Console.ReadLine();
                }

                // check if the title matches any of the books in list and give the user
                foreach (var book in books)
                {
                    if (bookInput.ToLower() == book["title"].ToLower() && book["status"] == "available")
                    {
                        Console.WriteLine($"{book["title"]} by {book["author"]} ({book["year"]}) is available");
                        return;
                    }
                    else if (bookInput.ToLower() == book["title"].ToLower() && book["status"] == "unavailable")
                    {
                        Console.WriteLine($"{book["title"]} by {book["author"]} ({book["year"]}) is currently unavailable");
                        return;
                    }
                }
                Console.WriteLine($"{bookInput} is not in library catalogue");
            } catch (Exception)
            {
                throw;
            }
        }

        public static void BorrowBook(List<Dictionary<string, string>> books)
        {
            // BigO notation here is 0(n)
            // A linear search algorithm is used to search book; the higher the list count, the
            // more time it takes
            try
            {
                // take input from user (title of book to borrow)
                Console.Write("Enter book title to borrow: ");
                string borrowInput = Console.ReadLine();

                // validate the input
                while (string.IsNullOrWhiteSpace(borrowInput))
                {
                    Console.Write("Enter a valid title: ");
                    borrowInput = Console.ReadLine();
                }

                // check if the book exists and the book is currently available
                // updates the book's status to unavailable
                foreach (var book in books)
                {
                    if (borrowInput.ToLower() == book["title"].ToLower() && book["status"] == "available")
                    {
                        book["status"] = "unavailable";
                        Console.WriteLine($"Here you go - {book["title"]} by {book["author"]} ({book["year"]})");
                        return;
                    }
                    else if (borrowInput.ToLower() == book["title"].ToLower() && book["status"] == "unavailable")
                    {
                        Console.WriteLine($"{book["title"]} by {book["author"]} ({book["year"]}) is currently unavailable");
                        return;
                    }
                }
                Console.WriteLine($"{borrowInput} is not in library catalogue");
            } catch (Exception)
            {
                throw;
            }
        }

        public static void ReturnBook(List<Dictionary<string, string>> books)
        {
            // BigO notation here is 0(n)
            // A linear search algorithm is used to search book; the higher the list count, the
            // more time it takes
            try
            {
                // take input from user (title of book to return)
                Console.Write("Enter book title to return: ");
                string returnInput = Console.ReadLine();

                // validate the input
                while (string.IsNullOrWhiteSpace(returnInput))
                {
                    Console.Write("Enter a valid title: ");
                    returnInput = Console.ReadLine();
                }

                // check if the book exists in the list of books and if its currently unavailable
                // updates the book's status to available
                foreach (var book in books)
                {
                    if (returnInput.ToLower() == book["title"].ToLower() && book["status"] == "unavailable")
                    {
                        book["status"] = "available";
                        Console.WriteLine($"{book["title"]} by {book["author"]} ({book["year"]}) returned successfully");
                        return;
                    }
                    else if (returnInput.ToLower() == book["title"].ToLower() && book["status"] == "available")
                    {
                        Console.WriteLine($"{book["title"]} by {book["author"]} ({book["year"]}) has been returned earlier");
                        return;
                    }
                }
                Console.WriteLine($"{returnInput} was not borrowed from this library");
            }
            catch (Exception)
            {
                throw;
            }
        }  
    }
}
