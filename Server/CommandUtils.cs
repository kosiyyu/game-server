namespace GameServer.Server;

    public static class CommandUtils
    {
        public static byte[] Serialize(Command command)
        {
            using var ms = new MemoryStream();
            using var writer = new BinaryWriter(ms);
            
            writer.Write(command.Name);
            writer.Write((byte)command.ValueType);
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
                default:
                    throw new InvalidOperationException("Unsupported value type.");
            }
            
            return ms.ToArray();
        }

        public static Command Deserialize(byte[] data)
        {
            using var ms = new MemoryStream(data);
            using var reader = new BinaryReader(ms);
            
            var name = reader.ReadString();
            var valueType = (ValueType)reader.ReadByte();
            object value = valueType switch
            {
                ValueType.String => reader.ReadString(),
                ValueType.Int => reader.ReadInt32(),
                ValueType.Float => reader.ReadSingle(),
                ValueType.Bool => reader.ReadBoolean(),
                _ => throw new InvalidOperationException("Unsupported value type.")
            };

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
