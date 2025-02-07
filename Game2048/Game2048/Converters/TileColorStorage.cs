using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game2048.Converters
{
    public static class TileColorStorage
    {
        public static readonly CustomColor Title2 = new(192, 192, 192);
        public static readonly CustomColor Title4 = new(169, 169, 169);
        public static readonly CustomColor Title8 = new(100, 149, 237);
        public static readonly CustomColor Title16 = new(65, 105, 225);
        public static readonly CustomColor Title32 = new(0, 0, 255);
        public static readonly CustomColor Title64 = new(0, 255, 0);
        public static readonly CustomColor Title128 = new(50, 205, 50);
        public static readonly CustomColor Title256 = new(0, 128, 0);
        public static readonly CustomColor Title512 = new(255, 165, 0);
        public static readonly CustomColor Title1024 = new(255, 140, 0);
        public static readonly CustomColor Title2048 = new(255, 69, 0);
        public static readonly CustomColor TitleEmpty = new(18, 18, 18);
    }
}
