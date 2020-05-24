using System.Collections.Generic;
using FluidHTN.Compounds;

namespace FluidHTN
{
    public interface IDomain<StateType>
    {
        TaskRoot<StateType> Root { get; }
        void Add(ICompoundTask<StateType> parent, ITask<StateType> subtask);
        void Add(ICompoundTask<StateType> parent, Slot<StateType> slot);
    }
}
