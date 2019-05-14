namespace Netytar
{
    public interface IButtonsSettings
    {
        int NCols { get; }
        int NRows { get; }
        int Spacing { get; }
        int GenerativeNote { get; }
        int StartPosition { get; }
        int OccluderAlpha { get; }
    }
}
