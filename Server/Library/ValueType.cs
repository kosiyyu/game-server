namespace GameServer.Server.Library;

public enum ValueType
{
    String = 1,
    Int = 2,
    Float = 3,
    Bool = 4,
    Telemetry = 5,
}

public static class ValueTypeUtils
{
    public static bool IsValueType(ValueType value)
    {
        return value is ValueType.String or ValueType.Int or ValueType.Float or ValueType.Bool or ValueType.Telemetry;
    }
}

