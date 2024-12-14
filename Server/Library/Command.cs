namespace GameServer.Server.Library;

public record Command(string Name, ValueType ValueType, object Value);