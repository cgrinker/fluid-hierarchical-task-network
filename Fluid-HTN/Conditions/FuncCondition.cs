using System;

namespace FluidHTN.Conditions
{
    public class FuncCondition<ContextType, StateType> : ICondition<StateType> where ContextType : IContext<StateType>
    {
        // ========================================================= FIELDS

        private readonly Func<ContextType, bool> _func;

        // ========================================================= CONSTRUCTION

        public FuncCondition(string name, Func<ContextType, bool> func)
        {
            Name = name;
            _func = func;
        }

        // ========================================================= PROPERTIES

        public string Name { get; }

        // ========================================================= VALIDITY

        public bool IsValid(IContext<StateType> ctx)
        {
            if (ctx is ContextType c)
            {
                var result = _func?.Invoke(c) ?? false;
                if (ctx.LogDecomposition) ctx.Log(Name, $"FuncCondition.IsValid:{result}", ctx.CurrentDecompositionDepth+1, this, result ? ConsoleColor.DarkGreen : ConsoleColor.DarkRed);
                return result;
            }

            throw new Exception("Unexpected context type!");
        }
    }
}
