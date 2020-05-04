namespace Netytar
{
    internal static class Rack
    {
        private static NetytarDMIBox dmibox = new NetytarDMIBox();
        public static NetytarDMIBox DMIBox { get => dmibox; set => dmibox = value; }
    }
}