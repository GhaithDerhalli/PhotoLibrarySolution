using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using System.Linq.Expressions;

namespace PhotoLibrary.Models
{
    public class AlbumVM
    {
        public HashSet<int> albumIds { get; set; }
        public List<Album> albums { get; set; }
        //public IEnumerable<SelectListItem> filteredAlbums { get; set; }

    }
}
