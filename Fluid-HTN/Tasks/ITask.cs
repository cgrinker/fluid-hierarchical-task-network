using System.Collections.Generic;
using FluidHTN.Compounds;
using FluidHTN.Conditions;

namespace FluidHTN
{
    public interface ITask<StateType>
    {
        /// <summary>
        ///     Used for debugging and identification purposes
        /// </summary>
        string Name { get; set; }

        /// <summary>
        ///     The parent of this task in the hierarchy
        /// </summary>
        ICompoundTask<StateType> Parent { get; set; }

        /// <summary>
        ///     The conditions that must be satisfied for this task to pass as valid.
        /// </summary>
        List<ICondition<StateType>> Conditions { get; }

        /// <summary>
        ///     Last status returned by Update
        /// </summary>
        TaskStatus LastStatus { get; }

        /// <summary>
        ///     Add a new condition to the task.
        /// </summary>
        /// <param name="condition"></param>
        /// <returns></returns>
        ITask<StateType> AddCondition(ICondition<StateType> condition);

        /// <summary>
        ///     Check the task's preconditions, returns true if all preconditions are valid.
        /// </summary>
        /// <param name="ctx"></param>
        /// <returns></returns>
        bool IsValid(IContext<StateType> ctx);

        DecompositionStatus OnIsValidFailed(IContext<StateType> ctx);
    }
}
