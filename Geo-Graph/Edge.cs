namespace GeoGraph
{
    public class Edge
    {
        public ulong id { get; }
        public Node neighbor { get; }
        public float time { get; }
        public float distance { get; }

        public Edge(Node neighbor, float time, float distance, ulong? id)
        {
            this.neighbor = neighbor;
            this.time = time;
            this.distance = distance;
            this.id = (id != null) ? id.Value : 0;
        }

        public override string ToString()
        {
            return string.Format("Edge ID: {0} Time: {1} Distance: {2:0000.00}m", this.id, TimeSpan.FromSeconds(this.time), this.distance);
        }
    }
}
