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
            string ret = string.Format("Node {0:000.00000}#{1:000.00000}", lat, lon);
            foreach(Edge e in this.edges)
            {
                ret = string.Format("{0}\n{1}", ret, e.ToString());
            }
            return ret;
        }

        public override bool Equals(object? obj)
        {
            if(obj != null && obj.GetType().Equals(this.GetType()))
            {
                Node n = (Node)obj;
                if (n.lat == this.lat && n.lon == this.lon)
                    return true;
                else
                    return false;
            }
            else
            {
                return false;
            }
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(lat, lon);
        }
    }
}
