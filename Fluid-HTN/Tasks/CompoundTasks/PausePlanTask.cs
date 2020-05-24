using System;
using System.Collections.Generic;
using FluidHTN.Compounds;
using FluidHTN.Conditions;

namespace FluidHTN
{
    public class PausePlanTask<StateType> : ITask<StateType>
    {
        // ========================================================= PROPERTIES

        public string Name { get; set; }
        public ICompoundTask<StateType> Parent { get; set; }
        public List<ICondition<StateType>> Conditions { get; } = null;
        public List<IEffect<StateType>> Effects { get; } = null;
        public TaskStatus LastStatus { get; }

        // ========================================================= VALIDITY

        public DecompositionStatus OnIsValidFailed(IContext<StateType> ctx)
        {
            return DecompositionStatus.Failed;
        }

        // ========================================================= ADDERS

        public ITask<StateType> AddCondition(ICondition<StateType> condition)
        {
            throw new Exception("Pause Plan tasks does not support conditions.");
        }

        public ITask<StateType> AddEffect(IEffect<StateType> effect)
        {
            throw new Exception("Pause Plan tasks does not support effects.");
        }

        // ========================================================= FUNCTIONALITY

        public void ApplyEffects(IContext<StateType> ctx)
        {
        }

        // ========================================================= VALIDITY

        public bool IsValid(IContext<StateType> ctx)
        {
            if (ctx.LogDecomposition) Log(ctx, $"PausePlanTask.IsValid:Success!");
            return true;
        }

        // ========================================================= LOGGING

        protected virtual void Log(IContext<StateType> ctx, string description)
        {
            ctx.Log(Name, description, ctx.CurrentDecompositionDepth, this, ConsoleColor.Green);
        }
    }
}
