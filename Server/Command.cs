namespace GameServer.Server;

public record Command(string Name, ValueType ValueType, object Value);