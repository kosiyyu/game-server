namespace GameServer.Server
{
    class InputData
    {
        public static string PlayerId;
        public static float X;
        public static float Y;

        public InputData(string playerId, float x, float y)
        {
            PlayerId = playerId;
            X = x;
            Y = y;
        }

        public static InputData FromBytes(byte[] data)
        {
            var dataString = System.Text.Encoding.UTF8.GetString(data);
            var parts = dataString.Split(',');

            PlayerId = parts[0];
            X = float.Parse(parts[1]);
            Y = float.Parse(parts[2]);
            
            return new InputData(PlayerId, X, Y);
        }

        public byte[] ToBytes()
        {
            return System.Text.Encoding.UTF8.GetBytes(PlayerId + "," + X + "," + Y);
        }
    }
}