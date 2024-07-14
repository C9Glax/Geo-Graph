using Half = SystemHalf.Half;

namespace GeoGraph
{
    public readonly struct Node
    { 
        public readonly Half Lat;
        public readonly Half Lon;

        public List<ulong> WayIds { get; }

        public Node(string lat, string lon) : this(Half.Parse(lat), Half.Parse(lon))
        {
            
        }

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
            this.WayIds = new();
        }

        public override string ToString()
        {
            return $"Node {Lat} {Lon}\n\tPart of Way(s): {string.Join(", ", WayIds)}";
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
