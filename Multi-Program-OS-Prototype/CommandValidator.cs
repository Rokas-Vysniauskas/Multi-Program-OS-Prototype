using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace VM_OS_Project
{
    public static class CommandValidator
    {
        private static readonly HashSet<string> ValidCommands = new HashSet<string>
        {
            "PU", "PO", "ADD_", "SUB_", "MUL_", "DIV_",
            "JP", "JZ", "JN", "JE", "JB", "JA",
            "IN__", "OUT_", "PD", "LD", "SR", "HALT"
        };

        public static bool IsValid(string line)
        {
            string trimmed = line.Trim();
            if (trimmed.Length < 2) return false;

            if (ValidCommands.Contains(trimmed)) return true;

            string prefix2 = trimmed.Length >= 2 ? trimmed.Substring(0, 2) : "";
            string prefix4 = trimmed.Length >= 4 ? trimmed.Substring(0, 4) : "";

            return ValidCommands.Contains(prefix2) || ValidCommands.Contains(prefix4);
        }
    }
    
}
