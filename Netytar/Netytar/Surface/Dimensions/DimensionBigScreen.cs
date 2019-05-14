using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Netytar
{
    class DimensionBigScreen : IDimension
    {
        private const int verticalSpacer = 130;
        private const int horizontalSpacer = 260;
        private const int buttonHeight = 13;
        private const int buttonWidth = 13;
        private const int occluderOffset = 40;
        private const int ellipseStrokeDim = 15;
        private const int ellipseStrokeSpacer = 15;
        private const int lineThickness = 3;

        #region Interface
        public int VerticalSpacer { get => verticalSpacer; }
        public int HorizontalSpacer { get => horizontalSpacer; }
        public int ButtonHeight { get => buttonHeight;  }
        public int ButtonWidth { get => buttonWidth;  }
        public int OccluderOffset { get => occluderOffset; }
        public int EllipseStrokeDim { get => ellipseStrokeDim; }
        public int EllipseStrokeSpacer { get => ellipseStrokeSpacer; }
        public int LineThickness { get => lineThickness; }
        #endregion
    }
}
