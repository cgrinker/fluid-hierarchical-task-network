using System;

namespace FluidHTN.Effects
{
    public class ActionEffect<ContextType, StateType> : IEffect<StateType> where ContextType : IContext<StateType>
    {
        // ========================================================= FIELDS

        private readonly Action<ContextType, EffectType> _action;

        // ========================================================= CONSTRUCTION

        public ActionEffect(string name, EffectType type, Action<ContextType, EffectType> action)
        {
            Name = name;
            Type = type;
            _action = action;
        }

        // ========================================================= PROPERTIES

        public string Name { get; }
        public EffectType Type { get; }

        // ========================================================= FUNCTIONALITY

        public void Apply(IContext<StateType> ctx)
        {
            if (ctx is ContextType c)
            {
                if (ctx.LogDecomposition) ctx.Log(Name, $"ActionEffect.Apply:{Type}", ctx.CurrentDecompositionDepth+1, this);
                _action?.Invoke(c, Type);
            }
            else
                throw new Exception("Unexpected context type!");
        }
    }
}
