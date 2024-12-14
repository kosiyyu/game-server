namespace GameServer.Server.Library;

public static class TelemetryUtils
{
    public static byte[] Serialize(Telemetry telemetry)
    {
        using var ms = new MemoryStream();
        using var writer = new BinaryWriter(ms);

        writer.Write(telemetry.Id);
        writer.Write(telemetry.Velocity);
        writer.Write(telemetry.Position.x);
        writer.Write(telemetry.Position.y);
        writer.Write(telemetry.Position.z);
        writer.Write(telemetry.SteeringAngle);

        return ms.ToArray();
    }

    public static Telemetry Deserialize(byte[] data)
    {
        var ms = new MemoryStream(data);
        using var reader = new BinaryReader(ms);
            
        var id = reader.ReadInt32();
        var velocity = reader.ReadSingle();
        var position = new Vector3(reader.ReadSingle(), reader.ReadSingle(), reader.ReadSingle());
        var steeringAngle = reader.ReadSingle();

        return new Telemetry()
        {
            Id = id,
            Velocity = velocity,
            Position = position,
            SteeringAngle = steeringAngle
        };
    }
}