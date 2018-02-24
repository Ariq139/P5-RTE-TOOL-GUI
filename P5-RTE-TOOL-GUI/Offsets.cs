using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PS3Lib;
using Persona5HookTest;

namespace P5_RTM_Tool_v2
{
    public static class Offsets
    {
        public static PS3API PS3API = MainWindow.PS3API;
        public static ProcessMemoryAccessor RPCS3API = MainForm.RPCS3API;
        public static bool usingPS3lib = MainForm.usingPS3Lib;

        /*The "slot" argument should be more than or equal to 1.
         If you look at the "GetPersonaOffset" method, you'll see that it's subtracting 1 from the slot argument
         so you don't have to start from 0. Therefore, 1 = 0 in game memory, 2 = 1, 3 = 2, etc...*/

        //Convert a string into a byte array
        public static byte[] StringToByteArray(string hex)
        {
            return Enumerable.Range(0, hex.Length)
                .Where(x => x % 2 == 0)
                .Select(x => Convert.ToByte(hex.Substring(x, 2), 16))
                .ToArray();
        }
        //Convert a string into byte array and set at the offset
        public static void SetStringAsByteArray(uint offset, string hexString)
        {
            if (usingPS3lib)
                PS3API.Extension.WriteBytes(offset, StringToByteArray(hexString));
            else
                RPCS3API.WriteBytes(offset, StringToByteArray(hexString));
        }
        //Gets a byte array, converts it into a string, and then returns it
        public static string GetByteArrayAsString(uint offset, int length)
        {
            byte[] array;

            if (usingPS3lib)
                array = PS3API.Extension.ReadBytes(offset, length);
            else
                array = RPCS3API.ReadBytes(offset, length);

            string construct = "";
            for (int byteIndex = 0; byteIndex < array.Length; byteIndex++)
                construct = construct + array[byteIndex].ToString("X");
            /*This function only handled two bytes at first.
                It is possible to handle more from the "length" argument, but it is not recommended*/
            if (length == 2)
            {
                if (construct.Length == 3)
                    construct = "0" + construct;
                else if (construct.Length == 2)
                    construct = "00" + construct;
            }

            return construct;
        }
        //Get offset of persona. This offset is that start of the persona's sort-of "structure" in memory.
        public static uint GetPersonaOffset(int slot)
        {
            return 0x010af2b6 + ((uint)(slot - 1) * 0x30);
        }
        //Get offset of persona's level
        public static uint GetLevelOffset(int slot)
        {
            return GetPersonaOffset(slot) + 2;
        }
        //Get stat of persona
        public static uint GetStatOffset(int slot, string stat)
        {
            int increment = 26;
            if (stat == "St")
                increment = 26;
            else if (stat == "Ma")
                increment = 27;
            else if (stat == "En")
                increment = 28;
            else if (stat == "Ag")
                increment = 29;
            else if (stat == "Lu")
                increment = 30;

            return GetPersonaOffset(slot) + (uint)increment;
        }
        //Get skill of persona
        public static uint GetSkillOffset(int slot, int skillSlot)
        {
            return (GetPersonaOffset(slot) + 10) + ((uint)(skillSlot - 1) * 2);
        }

        //Set a persona at "slot". Input must be 4 chars long.
        public static void SetPersona(int slot, string hex)
        {
            SetStringAsByteArray(GetPersonaOffset(slot), hex);
        }
        //Set a persona's level.
        public static void SetLevel(int slot, int level)
        {
            if (usingPS3lib)
                PS3API.Extension.WriteByte(GetLevelOffset(slot), Convert.ToByte(level));
            else
                RPCS3API.WriteByte(GetLevelOffset(slot), Convert.ToByte(level));
        }
        //Set a persona's stats. Possible "stat" arguments are "St, Ma, En, Ag, and Lu."
        public static void SetStat(int slot, string stat, int number)
        {
            if (usingPS3lib)
                PS3API.Extension.WriteByte(GetStatOffset(slot, stat), Convert.ToByte(number));
            else
                RPCS3API.WriteByte(GetStatOffset(slot, stat), Convert.ToByte(number));
        }
        //Set a persona's skill. Input must be 4 chars long.
        public static void SetSkill(int slot, int skillSlot, string hex)
        {
            SetStringAsByteArray(GetSkillOffset(slot, skillSlot), hex);
        }
        //Set player's money
        public static void SetMoney(int money)
        {
            byte[] buffer = BitConverter.GetBytes(money);
            Array.Reverse(buffer);
            if (usingPS3lib)
                PS3API.Extension.WriteBytes(0x010B21C4, buffer);
            else
                RPCS3API.WriteBytes(0x010B21C4, buffer);
        }

        //Get persona bytes at "slot"
        public static string GetPersona(int slot)
        {
            return GetByteArrayAsString(GetPersonaOffset(slot), 2);
        }
        //Get persona's level bytes
        public static int GetLevel(int slot)
        {
            if (usingPS3lib)
                return Convert.ToInt32(PS3API.Extension.ReadByte(GetLevelOffset(slot)));
            else
                return Convert.ToInt32(RPCS3API.ReadByte(GetLevelOffset(slot)));
        }
        //Get a persona's stat bytes
        public static int GetStat(int slot, string stat)
        {
            if (usingPS3lib)
                return Convert.ToInt32(PS3API.Extension.ReadByte(GetStatOffset(slot, stat)));
            else
                return Convert.ToInt32(RPCS3API.ReadByte(GetStatOffset(slot, stat)));
        }
        //Get a persona's skill bytes
        public static string GetSkill(int slot, int skillSlot)
        {
            return GetByteArrayAsString(GetSkillOffset(slot, skillSlot), 2);
        }
        //Get the player's money
        public static int GetMoney()
        {
            byte[] buffer;
            if (usingPS3lib)
                buffer = PS3API.Extension.ReadBytes(0x010B21C4, 4);
            else
                buffer = RPCS3API.ReadBytes(0x010B21C4, 4);

            Array.Reverse(buffer);
            return BitConverter.ToInt32(buffer, 0);
        }
    }
}
