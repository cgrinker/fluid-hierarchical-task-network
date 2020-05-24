
using System.Collections.Generic;

namespace FluidHTN.Factory
{
    public interface IFactory
    {
        ContextType[] CreateArray<ContextType>(int length);
        bool FreeArray<ContextType>(ref ContextType[] array);

        Queue<ContextType> CreateQueue<ContextType>();
        bool FreeQueue<ContextType>(ref Queue<ContextType> queue);

        List<ContextType> CreateList<ContextType>();
        bool FreeList<ContextType>(ref List<ContextType> list);

        ContextType Create<ContextType>() where ContextType : new();
        bool Free<ContextType>(ref ContextType obj);
    }
}
