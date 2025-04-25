using System;
using System.Collections.Generic;

namespace VM_OS_Project
{
    public class VirtualMachine
    {
        public ushort IC { get; set; }  // Instruction Counter
        public ushort SP { get; set; }  // Stack Pointer
        public ushort CS { get; set; }  // Code Segment
        public ushort DS { get; set; }  // Data Segment
        public ushort SS { get; set; }  // Stack Segement

        public byte SF { get; set; }    // Status Flags
        
        public ushort[,] Memory = new ushort[16, 16];  // 16 bloku po 16 zodziu
        public RealMachine RM;

        public VirtualMachine(RealMachine realMachine)
        {
            RM = realMachine;
        }

        public void LoadProgram(List<ushort> prgramWords)
        {
            // Laikinai ikelia programa i atminti be puslapiavimo
            int wordIndex = 0;
            for (int block = 0; block < 16; block++)
            {
                for (int word = 0; word < 16; word++)
                {
                    if (wordIndex < prgramWords.Count)
                        Memory[block, word] = prgramWords[wordIndex++];
                    else return;
                }
            }
        }

        public void RunProgram(RunMode mode)
        {
            Console.WriteLine("\n[VM] Starting execution in mode: " + mode);
            while(true)
            {
                ushort instruction = Memory[IC / 16, IC % 16];
                Console.WriteLine($"[VM] Executing instruction at IC={IC}: {instruction:X4}"); // :X4 - reiskia 16-tainis skaicus su 4 simboliais

                IC++;  // Kol kas - suolio nera, tiesiog zingsnis i prieki

                if (mode == RunMode.Step)
                {
                    Console.WriteLine("[VM] Press any key to continue...");
                    Console.ReadKey();
                }
                if (instruction == 0xFFFF)  // HALT (pvz.)
                {
                    Console.WriteLine("[VM] HALT encountered. Stopping execution.");
                    break;
                }
            }
        }

    }
}