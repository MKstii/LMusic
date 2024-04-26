using LMusic.Models;

namespace LMusic.Registries
{
    public class PictureRegistry : Registry<Picture>
    {
        public Picture? GetDefaultPicture(PictureType type)
        {
            using (var db = new ContextDataBase())
            {
                var pic = db.Pictures.Where(x => x.IsDefault == true).Where(x => x.Type == type).FirstOrDefault();
                return pic;
            }
        }
    }
}
