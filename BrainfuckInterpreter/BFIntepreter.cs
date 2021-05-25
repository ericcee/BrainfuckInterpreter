using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace BrainfuckInterpreter
{
    public class BFIntepreter
    {
        private byte[] memory;
        private byte[] code;
        private int mempointer = 0;
        private int codepointer = 0;
        Stack<int> loopPointer = new Stack<int>();
        Dictionary<int, int> savedLoops = new Dictionary<int, int>();
        public BFIntepreter(byte[] code, int memsize)
        {
            this.code = code;
            memory = new byte[memsize];
        }

        private char getch()
        {
            return Console.ReadKey().KeyChar;
        }

        private void putch(char x)
        {
            Console.Write(x);
        }

        public void Run()
        {
            while(codepointer < code.Length)
            {
                executeNextOpperand();
            }
        }

        private void executeNextOpperand()
        {
            switch ((char)code[codepointer]){
                case '+':
                    memory[mempointer]++;
                    break;
                case '-':
                    memory[mempointer]--;
                    break;
                case '<':
                    mempointer--;
                    break;
                case '>':
                    mempointer++;
                    break;
                case ',':
                    memory[mempointer] = (byte)getch();
                    break;
                case '.':
                    putch((char)memory[mempointer]);
                    break;
                case '[':
                    if (memory[mempointer] != 0)
                    {
                        loopPointer.Push(codepointer);
                    }
                    else
                    {
                        if (!savedLoops.ContainsKey(codepointer))
                        {
                            int temp = codepointer;
                            savedLoops.Add(codepointer, 0);
                            int depth = 1;
                            codepointer++;

                            while (true)
                            {
                                if (code[codepointer] == '[') depth++;
                                if (code[codepointer] == ']') depth--;
                                if (depth == 0) break;
                                codepointer++;
                            }
                            savedLoops[temp] = codepointer;
                        }
                        else codepointer = savedLoops[codepointer];
                    }
                    break;
                case ']':
                    if (memory[mempointer] != 0)
                    {
                        codepointer = loopPointer.Pop()-1;
                    }
                    else loopPointer.Pop();
                    break;
                default:
                    //Console.WriteLine($"{memory[mempointer]} at {mempointer}");
                    break;
            }

            codepointer++;
        }
    }
}
