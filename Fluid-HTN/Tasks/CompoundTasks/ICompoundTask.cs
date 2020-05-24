
using System.Collections.Generic;

namespace FluidHTN.Compounds
{
    public interface ICompoundTask<StateType> : ITask<StateType>
    {
        List<ITask<StateType>> Subtasks { get; }
        ICompoundTask<StateType> AddSubtask(ITask<StateType> subtask);

        /// <summary>
        ///     Decompose the task onto the tasks to process queue, mind it's depth first
        /// </summary>
        /// <param name="ctx"></param>
        /// <returns></returns>
        DecompositionStatus Decompose(IContext<StateType> ctx, int startIndex, out Queue<ITask<StateType>> result);
    }
}
