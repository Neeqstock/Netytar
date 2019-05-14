namespace Netytar
{
    class DimensionStretch : IDimension
    {
        private const int verticalSpacer = 80;
        private const int horizontalSpacer = 160;
        private const int buttonHeight = 13;
        private const int buttonWidth = 13;
        private const int occluderOffset = 20;
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
