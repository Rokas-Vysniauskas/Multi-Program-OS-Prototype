using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace VM_OS_Project
{
    public class ProgramLoader
    {
        public List<string> LoadProgramFromHDD(string filePath, string programName)
        {
            if (!File.Exists(filePath))
                throw new FileNotFoundException("HDD not found: " + filePath);

            List<string> allLines = File.ReadAllLines(filePath).ToList();

            List<string> programLines = new List<string>();
            bool reading = false;

            for (int i = 0; i < allLines.Count; i++)
            {
                string line = allLines[i].Trim();

                if (line == "$AMJ" && i + 1 < allLines.Count && allLines[i + 1].Trim() == programName)
                {
                    reading = true;
                    i++; // skip program name line
                    continue;
                }

                if (reading && line == "$END")
                    break;
                
                if (reading)
                    programLines.Add(line);
            }

            if (programLines.Count == 0)
                throw new Exception("Program with such name not found or is empty.");

            // Tikriname ti CODE segmento turini
            bool inCodeSegment = false;
            foreach (string line in programLines)
            {
                string trimmed = line.Trim();

                if (trimmed == "CODE")
                {
                    inCodeSegment = true;
                    continue;
                }

                if (!inCodeSegment) continue;

                if (trimmed == "$" || trimmed == "DATA") break;

                if (!CommandValidator.IsValid(trimmed))
                    throw new Exception($"Command is not valid: {line}");
            }

            return programLines;
        }

        public void LoadToSupervisorMemory(List<string> programLines, RealMachine rm)
        {
            int blockIndex = 0; // supervizorinė atmintis: blokai nuo 0 iki 15
            int wordIndex = 0;

            foreach (string line in programLines)
            {
                if (wordIndex >= 16)
                {
                    blockIndex++;
                    wordIndex = 0;
                }

                if (blockIndex >= 16)
                    throw new Exception("Program is to big for supervizor memory.");

                ushort encodedInstruction = EncodeInstruction(line);
                rm.Memory[blockIndex, wordIndex++] = encodedInstruction;
            }
        }

        private ushort EncodeInstruction(string line)
        {
            // Kol kas labai supaprastintas kodavimas:
            // Pvz.: "PU32" -> PU (kodų žemėlapis) + operandas 32
            // "ADD_" -> instrukcija be operandų
            
            string trimmed = line.Trim();
            string code = trimmed.Substring(0, 2);
            string arg = trimmed.Length > 2 ? trimmed.Substring(2) : "00";

            Dictionary<string, byte> opcodeMap = new Dictionary<string, byte>
            {
                {"PU", 0x10}, {"PO", 0x11}, {"ADD", 0x20}, {"SUB", 0x21},
                {"MUL", 0x22}, {"DIV", 0x23}, {"JP", 0x30}, {"JZ", 0x31},
                {"JN", 0x32}, {"JE", 0x33}, {"JB", 0x34}, {"JA", 0x35},
                {"IN", 0x40}, {"OUT", 0x41}, {"PD", 0x42}, {"LD", 0x43},
                {"SR", 0x44}, {"HA", 0xFF} // HALT
            };

            if (!opcodeMap.ContainsKey(code))
                return 0;

            byte opcode = opcodeMap[code];
            byte operand = 0;
            byte.TryParse(arg, out operand);

            return (ushort)((opcode << 8) | operand);
        }
    }
    
}
