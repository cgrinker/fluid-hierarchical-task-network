namespace FluidHTN
{
    public interface IEffect<StateType>
    {
        string Name { get; }
        EffectType Type { get; }
        void Apply(IContext<StateType> ctx);
    }
}
