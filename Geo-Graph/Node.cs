using Half = SystemHalf.Half;

namespace GeoGraph
{
    public class Node
    {
        public readonly Half Lat;
        public readonly Half Lon;

        public List<Edge> Edges { get; }

        public Node(double lat, double lon) : this(new Half(lat), new Half(lon))
        {
        }
        
        public Node(float lat, float lon) : this(new Half(lat), new Half(lon))
        {
        }

        public Node(Half lat, Half lon)
        {
            this.Lat = lat;
            this.Lon = lon;
            this.Edges = new();
        }

        public Edge? GetEdgeToNode(Node n)
        {
            return Edges.FirstOrDefault(e => e.Neighbor == n);
        }

        public override string ToString()
        {
            return $"Node {this.Lat:000.00000}#{this.Lon:000.00000}\n" +
                   $"\t{string.Join("\n\t", Edges.Select(e => e.ToString()))}";
        }

        public override bool Equals(object? obj)
        {
            if(obj != null && obj.GetType() == this.GetType())
            {
                Node n = (Node)obj;
                return n.Lat == this.Lat && n.Lon == this.Lon;
            }
            else
            {
                return false;
            }
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(this.Lat, this.Lon);
        }
    }
}
