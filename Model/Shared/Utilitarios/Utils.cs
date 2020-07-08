using System.Drawing;
using System.IO;

namespace Model.Shared.Utilitarios
{
    public static class Utils
    {
        public static Image ConverteFoto(byte[] imagem)
        {
            return Image.FromStream(new MemoryStream(imagem));
        }
    }
}
