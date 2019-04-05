using NLog;
using BlogsConsole.Models;
using System;
using System.Linq;

namespace BlogsConsole
{
    class MainClass
    {
        private static NLog.Logger logger = NLog.LogManager.GetCurrentClassLogger();
        public static void Main(string[] args)
        {
            logger.Info("Program started");
            try
            {

                // Create and save a new Blog
                displayMenu();
                string input = Console.ReadLine();
                var db = new BloggingContext();
                if (input == "1")
                {
                    Console.Write("Enter a new Blog: ");
                    var name = Console.ReadLine();

                    var blog = new Blog { Name = name };
                    
                    db.AddBlog(blog);
                    logger.Info("Blog added - {name}", name);
                }
                else if (input == "2")
                {
                    var query = db.Blogs.OrderBy(b => b.Name);

                    Console.WriteLine("All blogs in the database:");
                    foreach (var item in query)
                    {
                        Console.WriteLine(item.Name);
                    }
                }
                else if (input == "3")
                {
                    var query = db.Blogs.OrderBy(b => b.Name);

                    Console.WriteLine("All blogs in the database:");
                    foreach (var item in query)
                    {
                        Console.WriteLine(item.Name);
                    }

                    Console.Write("Enter a blog you want to post to: ");
                    var blogName = Console.ReadLine();
                    Blog mainBlog = db.Blogs.Where(ID => ID.Name == blogName).FirstOrDefault();
                    Console.Write("Enter title for your post: ");
                    var postTitle = Console.ReadLine();
                    Console.Write("\nEnter content for the post: ");
                    var postEntry/*content*/ = Console.ReadLine();
                    Post mainPost = new Post { Title = postTitle, Content = postEntry, BlogId = mainBlog.BlogId };
                    db.AddPost(mainPost);
                }
                else if (input == "4")
                {
                    var query2 = db.Posts.OrderBy(p => p.Title);

                    Console.WriteLine("All post titles in the database:");
                    foreach (var item2 in query2)
                    {
                        Console.WriteLine(item2.Title);
                    }
                }
               

                void displayMenu()
                {
                    Console.WriteLine("Enter 1 to add a blog: ");
                    Console.WriteLine("Enter 2 to display all blogs: ");
                    Console.WriteLine("Enter 3 to create a post: ");
                    Console.WriteLine("Enter 4 to display all posts: ");                    
                }
            }
            catch (Exception ex)
            {
                logger.Error(ex.Message);
            }

            Console.WriteLine("Press enter to quit");
            string x = Console.ReadLine();

            logger.Info("Program ended");
        }
    }
}
