namespace GameServer.Server.Library;


public static class CommandUtils
{
    public static byte[] Serialize(Command command)
    {
        using var ms = new MemoryStream();
        using var writer = new BinaryWriter(ms);

        writer.Write(command.Name); // Serialize the Name
        writer.Write((byte)command.ValueType); // Serialize the ValueType

        // Serialize the value based on its type
        switch (command.ValueType)
        {
            case ValueType.String:
                writer.Write((string)command.Value);
                break;
            case ValueType.Int:
                writer.Write((int)command.Value);
                break;
            case ValueType.Float:
                writer.Write((float)command.Value);
                break;
            case ValueType.Bool:
                writer.Write((bool)command.Value);
                break;
            case ValueType.Telemetry:
                var telemetry = (Telemetry)command.Value;
                var bytes = TelemetryUtils.Serialize(telemetry);
                writer.Write(bytes, 0, bytes.Length);
                break;
            default:
                throw new ArgumentOutOfRangeException(nameof(command.ValueType), "Unsupported ValueType");
        }

        return ms.ToArray();
    }

    public static Command Deserialize(byte[] data)
    {
        using var ms = new MemoryStream(data);
        using var reader = new BinaryReader(ms);

        var name = reader.ReadString();
        var valueType = (ValueType)reader.ReadByte();

        object value;
        switch (valueType)
        {
            case ValueType.String:
                value = reader.ReadString();
                break;
            case ValueType.Int:
                value = reader.ReadInt32();
                break;
            case ValueType.Float:
                value = reader.ReadSingle();
                break;
            case ValueType.Bool:
                value = reader.ReadBoolean();
                break;
            case ValueType.Telemetry:
                var length = reader.ReadInt32();
                var telemetryBytes = reader.ReadBytes(length);
                value = TelemetryUtils.Deserialize(telemetryBytes);
                break;
            default:
                throw new ArgumentOutOfRangeException(nameof(valueType), "Unsupported ValueType");
        }

        return new Command(name, valueType, value);
    }


    public static string? GetStringValue(Command command)
    {
        if (command.ValueType == ValueType.String)
        {
            return (string)command.Value;
        }

        return null;
    }

    public static int? GetIntValue(Command command)
    {
        if (command.ValueType == ValueType.Int)
        {
            return (int)command.Value;
        }

        return null;
    }

    public static float? GetFloat(Command command)
    {
        if (command.ValueType == ValueType.Float)
        {
            return (float)command.Value;
        }

        return null;
    }

    public static bool? GetBool(Command command)
    {
        if (command.ValueType == ValueType.Bool)
        {
            return (bool)command.Value;
        }

        return null;
    }

    public static Telemetry? GetTelemetry(Command command)
    {
        if (command.ValueType == ValueType.Telemetry)
        {
            return (Telemetry)command.Value;
        }
        
        return null;
    }

    public static bool IsCommand(Command command)
    {
        if (command is not Command)
        {
            return false;
        }

        if (!ValueTypeUtils.IsValueType(command.ValueType))
        {
            return false;
        }

        return false;
    }
}

