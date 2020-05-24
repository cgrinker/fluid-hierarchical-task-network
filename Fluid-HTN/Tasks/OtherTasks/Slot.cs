using System;
using System.Collections.Generic;
using FluidHTN.Conditions;

namespace FluidHTN.Compounds
{
    public class Slot<StateType> : ITask<StateType>
    {
        // ========================================================= PROPERTIES

        public int SlotId { get; set; }
        public string Name { get; set; }
        public ICompoundTask<StateType> Parent { get; set; }
        public List<ICondition<StateType>> Conditions { get; } = null;
        public TaskStatus LastStatus { get; private set; }
        public ICompoundTask<StateType> Subtask { get; private set; } = null;

        // ========================================================= VALIDITY

        public DecompositionStatus OnIsValidFailed(IContext<StateType> ctx)
        {
            return DecompositionStatus.Failed;
        }

        // ========================================================= ADDERS

        public ITask<StateType> AddCondition(ICondition<StateType> condition)
        {
            throw new Exception("Slot tasks does not support conditions.");
        }

        // ========================================================= SET / REMOVE

        public bool Set(ICompoundTask<StateType> subtask)
        {
            if(Subtask != null)
            {
                return false;
            }

            Subtask = subtask;
            return true;
        }

        public void Clear()
        {
            Subtask = null;
        }

        // ========================================================= DECOMPOSITION

        public DecompositionStatus Decompose(IContext<StateType> ctx, int startIndex, out Queue<ITask<StateType>> result)
        {
            if(Subtask != null)
            {
                return Subtask.Decompose(ctx, startIndex, out result);
            }

            result = null;
            return DecompositionStatus.Failed;
        }

        // ========================================================= VALIDITY

        public virtual bool IsValid(IContext<StateType> ctx)
        {
            var result = Subtask != null;
            if (ctx.LogDecomposition) Log(ctx, $"Slot.IsValid:{(result ? "Success" : "Failed")}!", result ? ConsoleColor.Green : ConsoleColor.Red);
            return result;
        }

        // ========================================================= LOGGING

        protected virtual void Log(IContext<StateType> ctx, string description, ConsoleColor color = ConsoleColor.White)
        {
            ctx.Log(Name, description, ctx.CurrentDecompositionDepth, this, color);
        }
    }
}
