namespace CommandRouting
{
    /// <summary>
    /// Can be used as a result type for commands that don't return any result. For
    /// this reason, it's a singleton.
    /// </summary>
    public sealed class Unit
    {
        public static Unit Result { get; } = new Unit();

        private Unit()
        {
            
        }
    }
}
