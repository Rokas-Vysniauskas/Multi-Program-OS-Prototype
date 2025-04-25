namespace VM_OS_Project
{
    public class RealCPU
    {
        public RunMode Mode = RunMode.Continuous;
        public byte TI = 16;  // Timer
        public byte SI = 0;
        public byte PI = 0;
        public ushort PTR;
    }
}