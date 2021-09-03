// Вставьте сюда финальное содержимое файла BrainfuckBasicCommands.cs

using System;

namespace func.brainfuck
{
    public class BrainfuckBasicCommands
    {
        public static void RegisterTo(IVirtualMachine vm, Func<int> read, Action<char> write)
        {
            vm.RegisterCommand('.', b => write(Convert.ToChar(b.Memory[b.MemoryPointer])));
            vm.RegisterCommand(',', b => b.Memory[b.MemoryPointer] = Convert.ToByte(read()));
            vm.RegisterCommand('+', b =>
            {
                if (b.Memory[b.MemoryPointer] < 255) b.Memory[b.MemoryPointer]++;
                else b.Memory[b.MemoryPointer] = 0;
            });
            vm.RegisterCommand('-', b =>
            {
                if (b.Memory[b.MemoryPointer] > 0) b.Memory[b.MemoryPointer]--;
                else b.Memory[b.MemoryPointer] = 255;
            });
            vm.RegisterCommand('>', b =>
            {
                b.MemoryPointer = (b.MemoryPointer + 1) % b.Memory.Length;
            });
            vm.RegisterCommand('<', b =>
            {
                b.MemoryPointer = (b.MemoryPointer + b.Memory.Length - 1) % b.Memory.Length;
            });
            CreateCharArray(vm);
        }

        private static void CreateCharArray(IVirtualMachine vm)
        {
            for (var upperCase = 'A'; upperCase <= 'Z'; upperCase++)
            {
                var symbol = upperCase;
                vm.RegisterCommand(symbol, b => { b.Memory[b.MemoryPointer] = Convert.ToByte(symbol); });
            }
            for (var lowerCase = 'a'; lowerCase <= 'z'; lowerCase++)
            {
                var symbol = lowerCase;
                vm.RegisterCommand(symbol, b => { b.Memory[b.MemoryPointer] = Convert.ToByte(symbol); });
            }
            for (var number = '0'; number <= '9'; number++)
            {
                var symbol = number;
                vm.RegisterCommand(symbol, b => { b.Memory[b.MemoryPointer] = Convert.ToByte(symbol); });
            }
        }
    }
}