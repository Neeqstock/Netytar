using System.Collections.Generic;
using System.Windows.Media;

namespace Netytar
{
    class KeysColorCodeStandard : IKeysColorCode
    {
        private readonly List<Color> colorCode = new List<Color>()
        {
            Colors.Red,
            Colors.Orange,
            Colors.Yellow,
            Colors.LightGreen,
            Colors.Blue,
            Colors.Purple,
            Colors.Coral
        };

        #region Interface
        public List<Color> ColorCode
        {
            get { return colorCode; }
        }
        #endregion
    }
}
