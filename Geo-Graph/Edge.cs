using Half = SystemHalf.Half;

namespace GeoGraph
{
    public class Edge
    {
        public ulong ID { get; }
        public Node Neighbor { get; }
        public byte? MaxSpeed { get; }

        public WayType WayType { get; }
        public Half Distance { get; }

        public Edge(Node start, Node neighbor, Way way, ulong? id)
        {
            this.Neighbor = neighbor;
            this.MaxSpeed = way.GetMaxSpeed(SpeedType.road);
            this.WayType = way.GetHighwayType();
            this.Distance = Utils.DistanceBetween(start, neighbor);
            this.ID = id??0;
        }

        public Edge(Node neighbor, Way way, Half distance, ulong? id)
        {
            this.Neighbor = neighbor;
            this.MaxSpeed = way.GetMaxSpeed(SpeedType.road);
            this.WayType = way.GetHighwayType();
            this.Distance = distance;
            this.ID = id??0;
        }

        public override string ToString()
        {
            return $"Edge ID: {ID} RoadType: {WayType} MaxSpeed: {MaxSpeed} Distance: {Distance:0000.00}m";
        }

        public byte GetMaxSpeed(SpeedType speedType)
        {
            switch(speedType)
            {
                case SpeedType.pedestrian:
                    return WayUtils.SpeedPedestrian[this.WayType];
                case SpeedType.car:
                    return MaxSpeed ?? WayUtils.SpeedCar[this.WayType];
                default:
                    return MaxSpeed ?? (byte)0;
            }
        }
    }
}
