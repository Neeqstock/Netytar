namespace Netytar
{
    class ButtonsSettings1 : IButtonsSettings
    {
        private const int nCols = 10; // OLD 8
        private const int nRows = 10; // OLD 8
        private const int spacing = 185; // OLD 160
        private const int generativeNote = 50;
        private const int startPosition = 500;
        private const int occluderAlpha = 20;

        #region Interface
        public int NCols { get => nCols; }
        public int NRows { get => nRows; }
        public int Spacing { get => spacing; }
        public int GenerativeNote { get => generativeNote; }
        public int StartPosition { get => startPosition; }
        public int OccluderAlpha { get => occluderAlpha; }
        #endregion
    }
}
