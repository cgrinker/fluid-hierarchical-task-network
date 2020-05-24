using System;
using System.Collections.Generic;
using FluidHTN.Compounds;
using FluidHTN.Conditions;
using FluidHTN.Debug;
using FluidHTN.Factory;

namespace FluidHTN
{
    /// <summary>
    ///     The state our context can be in. This is essentially planning or execution state.
    /// </summary>
    public enum ContextState
    {
        Planning,
        Executing
    }

    public struct PartialPlanEntry<StateType>
    {
        public ICompoundTask<StateType> Task;
        public int TaskIndex;
    }

    public interface IContext<StateType>
    {
        bool IsInitialized { get; }
        bool IsDirty { get; set; }
        ContextState ContextState { get; set; }
        int CurrentDecompositionDepth { get; set; }

        IFactory Factory { get; set; }

        /// <summary>
        ///     The Method Traversal Record is used while decomposing a domain and
        ///     records the valid decomposition indices as we go through our
        ///     decomposition process.
        ///     It "should" be enough to only record decomposition traversal in Selectors.
        ///     This can be used to compare LastMTR with the MTR, and reject
        ///     a new plan early if it is of lower priority than the last plan.
        ///     It is the user's responsibility to set the instance of the MTR, so that
        ///     the user is free to use pooled instances, or whatever optimization they
        ///     see fit.
        /// </summary>
        List<int> MethodTraversalRecord { get; set; }

        List<string> MTRDebug { get; set; }

        /// <summary>
        ///     The Method Traversal Record that was recorded for the currently
        ///     running plan.
        ///     If a plan completes successfully, this should be cleared.
        ///     It is the user's responsibility to set the instance of the MTR, so that
        ///     the user is free to use pooled instances, or whatever optimization they
        ///     see fit.
        /// </summary>
        List<int> LastMTR { get; }

        List<string> LastMTRDebug { get; set; }

        /// <summary>
        /// Whether the planning system should collect debug information about our Method Traversal Record.
        /// </summary>
        bool DebugMTR { get; }

        /// <summary>
        /// </summary>
        Queue<IBaseDecompositionLogEntry> DecompositionLog { get; set; }

        /// <summary>
        /// Whether our planning system should log our decomposition. Specially condition success vs failure.
        /// </summary>
        bool LogDecomposition { get; }

        /// <summary>
        /// 
        /// </summary>
        Queue<PartialPlanEntry<StateType>> PartialPlanQueue { get; set; }

        bool HasPausedPartialPlan { get; set; }

        StateType[] WorldState { get; }

        /// <summary>
        ///     A stack of changes applied to each world state entry during planning.
        ///     This is necessary if one wants to support planner-only and plan&execute effects.
        /// </summary>
        Stack<KeyValuePair<EffectType, StateType>>[] WorldStateChangeStack { get; }

        /// <summary>
        ///     Reset the context state to default values.
        /// </summary>
        void Reset();

        void TrimForExecution();
        void TrimToStackDepth(int[] stackDepth);

        bool HasState(int state, StateType value);
        StateType GetState(int state);
        void SetState(int state, StateType value, bool setAsDirty = true, EffectType e = EffectType.Permanent);

        int[] GetWorldStateChangeDepth(IFactory factory);

        void Log(string name, string description, int depth, ITask<StateType> task, ConsoleColor color = ConsoleColor.White);
        void Log(string name, string description, int depth, ICondition<StateType> condition, ConsoleColor color = ConsoleColor.DarkGreen);
        void Log(string name, string description, int depth, IEffect<StateType> effect, ConsoleColor color = ConsoleColor.DarkYellow);
    }
}
