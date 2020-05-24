using System;
using System.Collections.Generic;
using FluidHTN.Conditions;

namespace FluidHTN.Compounds
{
    public abstract class CompoundTask<StateType> : ICompoundTask<StateType>
    {
        // ========================================================= PROPERTIES

        public string Name { get; set; }
        public ICompoundTask<StateType> Parent { get; set; }
        public List<ICondition<StateType>> Conditions { get; } = new List<ICondition<StateType>>();
        public TaskStatus LastStatus { get; private set; }
        public List<ITask<StateType>> Subtasks { get; } = new List<ITask<StateType>>();

        // ========================================================= VALIDITY

        public virtual DecompositionStatus OnIsValidFailed(IContext<StateType> ctx)
        {
            return DecompositionStatus.Failed;
        }

        // ========================================================= ADDERS

        public ITask<StateType> AddCondition(ICondition<StateType> condition)
        {
            Conditions.Add(condition);
            return this;
        }

        public ICompoundTask<StateType> AddSubtask(ITask<StateType> subtask)
        {
            Subtasks.Add(subtask);
            return this;
        }

        // ========================================================= DECOMPOSITION

        public DecompositionStatus Decompose(IContext<StateType> ctx, int startIndex, out Queue<ITask<StateType>> result)
        {
            if (ctx.LogDecomposition) ctx.CurrentDecompositionDepth++;
            var status = OnDecompose(ctx, startIndex, out result);
            if (ctx.LogDecomposition) ctx.CurrentDecompositionDepth--;
            return status;
        }

        protected abstract DecompositionStatus OnDecompose(IContext<StateType> ctx, int startIndex, out Queue<ITask<StateType>> result);

        protected abstract DecompositionStatus OnDecomposeTask(IContext<StateType> ctx, ITask<StateType> task, int taskIndex, int[] oldStackDepth, out Queue<ITask<StateType>> result);

        protected abstract DecompositionStatus OnDecomposeCompoundTask(IContext<StateType> ctx, ICompoundTask<StateType> task, int taskIndex, int[] oldStackDepth, out Queue<ITask<StateType>> result);

        protected abstract DecompositionStatus OnDecomposeSlot(IContext<StateType> ctx, Slot<StateType> task, int taskIndex, int[] oldStackDepth, out Queue<ITask<StateType>> result);

        // ========================================================= VALIDITY

        public virtual bool IsValid(IContext<StateType> ctx)
        {
            foreach (var condition in Conditions)
            {
                var result = condition.IsValid(ctx);
                if (ctx.LogDecomposition) Log(ctx, $"PrimitiveTask.IsValid:{(result ? "Success" : "Failed")}:{condition.Name} is{(result ? "" : " not")} valid!", result ? ConsoleColor.DarkGreen : ConsoleColor.DarkRed);
                if (result == false)
                {
                    return false;
                }
            }

            return true;
        }

        // ========================================================= LOGGING

        protected virtual void Log(IContext<StateType> ctx, string description, ConsoleColor color = ConsoleColor.White)
        {
            ctx.Log(Name, description, ctx.CurrentDecompositionDepth, this, color);
        }
    }
}
