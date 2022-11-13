using Microsoft.AspNetCore.Mvc.Rendering;
using Newtonsoft.Json;
using System.Net.Http.Headers;
using System.Reflection.Metadata;

namespace PhotoLibrary.Models.Services
{
    public class PhotoServices
    {
        public DetailsVM GetRightAlbum(int id ,List<Photo> photos)
        {
            DetailsVM detailsVm = new DetailsVM();

            List<Photo> listOfPhotos = photos
                .Where(x => id == x.AlbumId)
                .OrderBy(x => x.Id)
                .ToList();
            detailsVm.photos = listOfPhotos;
            detailsVm.albumID = id;
            return detailsVm;

        }
        public AlbumVM BuildAlbumVM(List<Album> albums)
        {
            AlbumVM albumDTO = new AlbumVM();
            albumDTO.albums = albums;
            var albumIds = albums.Select(album => album.UserId);
            var albumIdsSet = new HashSet<int>(albumIds);
            albumDTO.albumIds = albumIdsSet;
            return albumDTO;
            
        }
    }
}
