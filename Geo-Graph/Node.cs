namespace GeoGraph
{
    public class Node
    {
        public float lat { get; }
        public float lon { get; }

        public HashSet<Edge> edges { get; }

        public Node(float lat, float lon)
        {
            this.lat = lat;
            this.lon = lon;
            this.edges = new();
        }

        public Edge? GetEdgeToNode(Node n)
        {
            foreach (Edge e in this.edges)
                if (e.neighbor == n)
                    return e;
            return null;
        }

        public override string ToString()
        {
            string ret = string.Format("{0}#{1}", lat, lon);
            foreach(Edge e in this.edges)
            {
                ret = string.Format("{0}\n{1}", ret, e.ToString());
            }
            return ret;
        }
    }
}
