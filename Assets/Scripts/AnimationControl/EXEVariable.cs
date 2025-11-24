namespace OALProgramControl
{
    public class EXEVariable
    {
        public readonly string Name;
        public EXEValueBase Value;

        public EXEVariable(string name, EXEValueBase value)
        {
            this.Name = name;
            this.Value = value;
        }
    }
}