using System;
using FluidHTN.Conditions;

namespace FluidHTN.Debug
{
    public static class Debug
    {
        public static string DepthToString(int depth)
        {
            string s = "";
            for (var i = 0; i < depth; i++)
            {
                s += "\t";
            }

            s += "- ";
            return s;
        }
    }
    public interface IBaseDecompositionLogEntry
    {
        string Name { get; set; }
        string Description { get; set; }
        int Depth { get; set; }
        ConsoleColor Color { get; set; }
        string ToString();
    }

    public interface IDecompositionLogEntry<ContextType> : IBaseDecompositionLogEntry
    {
        ContextType Entry { get; set; }
    }

    public struct DecomposedCompoundTaskEntry<StateType> : IDecompositionLogEntry<ITask<StateType>>
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public int Depth { get; set; }
        public ConsoleColor Color { get; set; }
        public ITask<StateType> Entry { get; set; }
    }

    public struct DecomposedConditionEntry<StateType> : IDecompositionLogEntry<ICondition<StateType>> {
        public string Name { get; set; }
        public string Description { get; set; }
        public int Depth { get; set; }
        public ConsoleColor Color { get; set; }
        public ICondition<StateType> Entry { get; set; }
    }

    public struct DecomposedEffectEntry<StateType> : IDecompositionLogEntry<IEffect<StateType>> {
        public string Name { get; set; }
        public string Description { get; set; }
        public int Depth { get; set; }
        public ConsoleColor Color { get; set; }
        public IEffect<StateType> Entry { get; set; }
    }
}
