namespace FluidHTN.Conditions
{
    public interface ICondition<StateType>
    {
        string Name { get; }
        bool IsValid(IContext<StateType> ctx);
    }
}
