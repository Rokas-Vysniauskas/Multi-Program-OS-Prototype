using System;
using System.Collections.Generic;
using VM_OS_Project;

public static class TestVM
{
    public static void RunBasicTest()
    {
        RealMachine rm = new RealMachine();
        VirtualMachine vm = new VirtualMachine(rm);

        //Paprasta testine programa
        List<ushort> program = new List<ushort> { 0x1234, 0xFFFF };
        vm.LoadProgram(program);

        vm.RunProgram(RunMode.Step);
    }
}
