using System;
using WebApplication1.Extensions;

namespace WebApplication1.Models
{
    public class Operand
    {
        public Operand(int value, Operation operation)
        {
            Operation = operation;
            Value = value;
        }

        public Operation Operation { get; set; }

        public int Value { get; set; }

        public int Apply(int argument)
        {
            switch (Operation)
            {
                case Operation.Add:
                    return argument + Value;
                case Operation.Subtract:
                    return argument - Value;
                case Operation.Multiply:
                    return argument * Value;
                case Operation.Divide:
                    return argument / Value;
                default:
                    throw new InvalidOperationException("Unknown operand.Operation value:" + Operation);
            }
        }

        public override string ToString()
        {
            return $"{Operation.GetDisplayName()} {Value}";
        }
    }
}
