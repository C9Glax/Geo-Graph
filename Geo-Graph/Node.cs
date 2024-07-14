using System.Globalization;

namespace GeoGraph
{
    public readonly struct Node
    { 
        public readonly float Lat;
        public readonly float Lon;

        public List<ulong> WayIds { get; }

        public Node(string lat, string lon) : this(float.Parse(lat, NumberStyles.Float, NumberFormatInfo.InvariantInfo), float.Parse(lon, NumberStyles.Float, NumberFormatInfo.InvariantInfo))
        {
            
        }

        public Node(float lat, float lon)
        {
            this.Lat = lat;
            this.Lon = lon;
            this.WayIds = new();
        }

        public override string ToString()
        {
            return $"Node {Lat:00.000000} {Lon:000.000000}\n\tPart of Way(s): {string.Join(", ", WayIds)}";
        }

        public override bool Equals(object? obj)
        {
            if(obj != null && obj.GetType() == this.GetType())
            {
                Node n = (Node)obj;
                return Math.Abs(n.Lat - this.Lat) < 0.0001 && Math.Abs(n.Lon - this.Lon) < 0.0001;
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
