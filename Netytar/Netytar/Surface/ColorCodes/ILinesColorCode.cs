using System.Collections.Generic;
using System.Windows.Media;

namespace Netytar
{
    public interface ILinesColorCode
    {
        SolidColorBrush NotInScaleBrush { get; }
        SolidColorBrush MinorBrush { get; }
        SolidColorBrush MajorBrush { get; }
    }
}
