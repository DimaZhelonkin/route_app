using Ardalis.SmartEnum;

namespace Ark.IdentityServer.Domain.Sexes;

public abstract class SexEnum : SmartEnum<SexEnum>
{
    public static readonly SexEnum Unknown = new UnknownType();
    public static readonly SexEnum Male = new MaleType();
    public static readonly SexEnum Female = new FemaleType();

    protected SexEnum(string name, int value) : base(name, value)
    {
    }

    #region Nested type: FemaleType

    private class FemaleType : SexEnum
    {
        public FemaleType() : base("Female", 2)
        {
        }
    }

    #endregion

    #region Nested type: MaleType

    private class MaleType : SexEnum
    {
        public MaleType() : base("Male", 1)
        {
        }
    }

    #endregion

    #region Nested type: UnknownType

    private class UnknownType : SexEnum
    {
        public UnknownType() : base("Unknown", 0)
        {
        }
    }

    #endregion
}