// Вставьте сюда финальное содержимое файла BrainfuckLoopCommands.cs

using System.Collections.Generic;

namespace func.brainfuck
{
    public class BrainfuckLoopCommands
    {
        public static Dictionary<int, int> Bracket = new Dictionary<int, int>();
        public static Stack<int> Stack = new Stack<int>();

        public static void RegisterTo(IVirtualMachine vm)
        {
            for (var i = 0; i < vm.Instructions.Length; i++)
            {
                if (vm.Instructions[i] == '[') Stack.Push(i);
                else if (vm.Instructions[i] == ']')
                {
                    Bracket[i] = Stack.Peek();
                    Bracket[Stack.Pop()] = i;
                }
            }
            vm.RegisterCommand('[', b =>
            {
                if (b.Memory[b.MemoryPointer] == 0)
                    b.InstructionPointer = Bracket[b.InstructionPointer];
            });
            vm.RegisterCommand(']', b =>
            {
                if (b.Memory[b.MemoryPointer] != 0)
                    b.InstructionPointer = Bracket[b.InstructionPointer];
            });
        }
    }
}