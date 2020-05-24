namespace FluidHTN.Operators
{
    public interface IOperator<StateType>
    {
        TaskStatus Update(IContext<StateType> ctx);
        void Stop(IContext<StateType> ctx);
    }
}
