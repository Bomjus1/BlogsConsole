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
                Console.Write("Enter a name for a new Blog: ");
                var name = Console.ReadLine();

                var blog = new Blog { Name = name };

                var db = new BloggingContext();
                db.AddBlog(blog);            
                logger.Info("Blog added - {name}", name);

                // Display all Blogs from the database
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
