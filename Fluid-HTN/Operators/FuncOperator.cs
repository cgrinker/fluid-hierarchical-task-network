using System;

namespace FluidHTN.Operators
{
    public class FuncOperator<ContextType, StateType> : IOperator<StateType> where ContextType : IContext<StateType>
    {
        // ========================================================= FIELDS

        private readonly Func<ContextType, TaskStatus> _func;
        private readonly Action<ContextType> _funcStop;

        // ========================================================= CONSTRUCTION

        public FuncOperator(Func<ContextType, TaskStatus> func, Action<ContextType> funcStop = null)
        {
            _func = func;
            _funcStop = funcStop;
        }

        // ========================================================= FUNCTIONALITY

        public TaskStatus Update(IContext<StateType> ctx)
        {
            if (ctx is ContextType c)
                return _func?.Invoke(c) ?? TaskStatus.Failure;
            throw new Exception("Unexpected context type!");
        }

        public void Stop(IContext<StateType> ctx)
        {
            if (ctx is ContextType c)
                _funcStop?.Invoke(c);
            else
                throw new Exception("Unexpected context type!");
        }
    }
}
