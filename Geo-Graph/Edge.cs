namespace GeoGraph
{
    public class Edge
    {
        public ulong id { get; }
        public Node neighbor { get; }
        public byte maxspeed { get; }
        public float distance { get; }

        public Edge(Node neighbor, byte maxspeed, float distance, ulong? id)
        {
            this.neighbor = neighbor;
            this.maxspeed = maxspeed;
            this.distance = distance;
            this.id = (id != null) ? id.Value : 0;
        }

        public override string ToString()
        {
            return string.Format("Edge ID: {0} Maxspeed: {1} Distance: {2:0000.00}m", this.id, this.maxspeed, this.distance);
        }
    }
}
