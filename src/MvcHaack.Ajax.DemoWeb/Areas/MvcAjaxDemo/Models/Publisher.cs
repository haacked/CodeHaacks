using System.Collections.Generic;

namespace MvcHaack.Ajax.Sample.Models {
    public class Publisher {
        public int Id { get; set; }
        public string Name { get; set; }
        public virtual ICollection<ComicBook> ComicBooks { get; set; }
    }
}