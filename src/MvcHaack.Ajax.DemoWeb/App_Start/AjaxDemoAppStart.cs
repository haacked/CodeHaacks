using System.Data.Entity;
using MvcHaack.Ajax.Sample.Models;

[assembly: WebActivator.PreApplicationStartMethod(typeof(MvcHaack.Ajax.Sample.App_Start.AjaxDemoAppStart), "Start")]

namespace MvcHaack.Ajax.Sample.App_Start
{
    public static class AjaxDemoAppStart
    {
        public static void Start()
        {
            Database.SetInitializer(new MyDropCreateDatabaseIfModelChanges());
        }
    }

    public class MyDropCreateDatabaseIfModelChanges : DropCreateDatabaseIfModelChanges<ComicContext>
    {
        protected override void Seed(ComicContext context)
        {
            var darkHorse = new Publisher { Name = "Dark Horse" };
            context.Publishers.Add(darkHorse);
            var marvel = new Publisher { Name = "Marvel" };
            context.Publishers.Add(marvel);
            var dc = new Publisher { Name = "DC" };
            context.Publishers.Add(dc);

            context.ComicBooks.Add(new ComicBook { Title = "Groo", Publisher = darkHorse });
            context.ComicBooks.Add(new ComicBook { Title = "Spiderman", Publisher = marvel });
            context.ComicBooks.Add(new ComicBook { Title = "Superman", Publisher = dc });
            context.ComicBooks.Add(new ComicBook { Title = "Batman", Publisher = dc });

            context.SaveChanges();
        }

    }
}
