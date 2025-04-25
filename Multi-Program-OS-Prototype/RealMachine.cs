namespace VM_OS_Project
{
    public class RealMachine
    {
        public RealCPU CPU;
        public ChannelDevice Channel;
        public ushort[,] Memory = new ushort[67, 16];

        public RealMachine()
        {
            CPU = new RealCPU();
            Channel = new ChannelDevice();
        }
    }
}