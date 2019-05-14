namespace Netytar
{
    public static class NetytarRack
    {
        private static NetytarDMIBox dmibox = new NetytarDMIBox();
        public static NetytarDMIBox DMIBox { get => dmibox; set => dmibox = value; }
    }
}
