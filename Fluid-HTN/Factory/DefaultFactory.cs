
using System.Collections.Generic;

namespace FluidHTN.Factory
{
    public sealed class DefaultFactory : IFactory
    {
        public ContextType[] CreateArray<ContextType>(int length)
        {
            return new ContextType[length];
        }

        public List<ContextType> CreateList<ContextType>()
        {
            return new List<ContextType>();
        }

        public Queue<ContextType> CreateQueue<ContextType>()
        {
            return new Queue<ContextType>();
        }

        public bool FreeArray<ContextType>(ref ContextType[] array)
        {
            array = null;
            return array == null;
        }

        public bool FreeList<ContextType>(ref List<ContextType> list)
        {
            list = null;
            return list == null;
        }

        public bool FreeQueue<ContextType>(ref Queue<ContextType> queue)
        {
            queue = null;
            return queue == null;
        }

        public ContextType Create<ContextType>() where ContextType : new()
        {
            return new ContextType();
        }

        public bool Free<ContextType>(ref ContextType obj)
        {
            obj = default(ContextType);
            return obj == null;
        }
    }
}
