using System.Collections.Generic;
using System;
using System.Text;
using System.Threading;

namespace BinaryCounter_Parallel
{
    public class BinaryCounter
    {
        private LinkedList<List<bool>> binaryCounter;
        private readonly int columns;
        private readonly long rows;

        public BinaryCounter(int n)
        {
            columns = n;
            rows = (long)Math.Pow(2, n);
            Initialize();
            RenderCounter();
        }

        public void Display()
        {
            foreach (List<bool> bools in binaryCounter)
            {
                foreach (bool b in bools)
                {
                    if (b)
                    {
                        Console.ForegroundColor = ConsoleColor.Green;
                        Console.Write('1');
                    }
                    else
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.Write('0');
                    }
                    
                }

                Console.WriteLine();
            }
        }

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            foreach (List<bool> bools in binaryCounter)
            {
                foreach (bool b in bools)
                {
                    sb.Append(b ? '1' : '0');
                }

                sb.AppendLine();
            }

            return sb.ToString();
        }

        private void Initialize()
        {
            binaryCounter = new LinkedList<List<bool>>();
            for(int i = 0; i<rows; i++)
            {
                binaryCounter.AddLast(InitializeRecord());
            }
        }

        private List<bool> InitializeRecord()
        {
            List<bool> result = new List<bool>();
            for (int i = 0; i < columns; i++)
                result.Add(false);
            return result;
        }

        private void RenderCounter()
        {
            long step = rows / 2;
            for (int column = 0; column < columns; column++)
            {
                object[] args = new object[] {column, step};
                Thread thread = new Thread(ColumnFillHandler);
                thread.Start(args);
                step /= 2;
            }
        }

        private void ColumnFillHandler(object args)
        {
            Array argsArray = (Array)args;
            int column = (int) argsArray.GetValue(0);
            long step = (long) argsArray.GetValue(1);

            bool currentValue = false;
            long i = 0;
            int stepCount = 0;
            foreach (List<bool> record in binaryCounter)
            {
                if (stepCount < step)
                    stepCount++;
                else
                {
                    currentValue = !currentValue;
                    stepCount = 1;
                }
                record[column] = currentValue;
                i++;
            }
        }
    }
}