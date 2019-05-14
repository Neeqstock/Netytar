using System.Windows.Media;

namespace Netytar
{
    class LinesColorCodeStandard : ILinesColorCode
    {
        private readonly SolidColorBrush notInScaleBrush = new SolidColorBrush(Color.FromArgb(20,0,0,0));
        private readonly SolidColorBrush minorBrush = new SolidColorBrush(Colors.Blue);
        private readonly SolidColorBrush majorBrush = new SolidColorBrush(Colors.Red);

        public SolidColorBrush NotInScaleBrush { get => notInScaleBrush; }
        public SolidColorBrush MinorBrush { get => minorBrush; }
        public SolidColorBrush MajorBrush { get => majorBrush; }
    }
}
