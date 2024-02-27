namespace LMusic.Models
{
    public class PrivateSettings
    {
        // Сделать енумом или как класс???
        // Можно сделать классом, смотря какие у нас будут настройки приватности.
        // Друзья
        // Музыка

        // У плейлиста свои настройки
    }

    public enum Privacy
    {
        ForAll = 0,
        ForFriends = 1,
        ForMe = 2
    }  
}
