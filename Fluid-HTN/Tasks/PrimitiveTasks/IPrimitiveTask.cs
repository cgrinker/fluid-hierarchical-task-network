using System.Collections.Generic;
using FluidHTN.Conditions;
using FluidHTN.Operators;

namespace FluidHTN.PrimitiveTasks
{
    public interface IPrimitiveTask<StateType> : ITask<StateType>
    {
        /// <summary>
        ///     Executing conditions are validated before every call to Operator.Update(...)
        /// </summary>
        List<ICondition<StateType>> ExecutingConditions { get; }

        /// <summary>
        ///     Add a new executing condition to the primitive task. This will be checked before
        ///		every call to Operator.Update(...)
        /// </summary>
        /// <param name="condition"></param>
        /// <returns></returns>
        ITask<StateType> AddExecutingCondition(ICondition<StateType> condition);

        IOperator<StateType> Operator { get; }
        void SetOperator(IOperator<StateType> action);

        List<IEffect<StateType>> Effects { get; }
        ITask<StateType> AddEffect(IEffect<StateType> effect);
        void ApplyEffects(IContext<StateType> ctx);

        void Stop(IContext<StateType> ctx);
    }
}
