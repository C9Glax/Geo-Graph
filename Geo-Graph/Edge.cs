using static GeoGraph.Way;

namespace GeoGraph
{
    public class Edge
    {
        public ulong id { get; }
        public Node neighbor { get; }
        public byte? maxSpeed { get; }

        public Way.wayType wayType { get; }
        public float distance { get; }

        public Edge(Node neighbor, Way way, float distance, ulong? id)
        {
            this.neighbor = neighbor;
            this.maxSpeed = way.GetMaxSpeed(Way.speedType.road);
            this.wayType = way.GetHighwayType();
            this.distance = distance;
            this.id = (id != null) ? id.Value : 0;
        }

        public override string ToString()
        {
            return string.Format("Edge ID: {0} RoadType: {1} MaxSpeed: {2} Distance: {3:0000.00}m", this.id, this.wayType, this.maxSpeed, this.distance);
        }

        public byte GetMaxSpeed(speedType speedType)
        {
            switch(speedType)
            {
                case speedType.pedestrian:
                    return Way.speedped[this.wayType];
                case speedType.car:
                    return maxSpeed != null ? (byte)maxSpeed : Way.speedcar[this.wayType];
                default:
                    return maxSpeed != null ? (byte)maxSpeed : (byte)0;
            }
        }
    }
}
