namespace GameServer.Server.Library;

public class Telemetry
{
    public int Id;
    public float Velocity;
    public Vector3 Position;
    public float SteeringAngle;

    public override string ToString() 
    {
        return $"Id: {Id}, Velocity: {Velocity} Position: {Position}, SteeringAngle: {SteeringAngle}";
    }
}