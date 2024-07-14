namespace GeoGraph
{
    public readonly struct Way
    {
        public readonly Dictionary<ulong, ulong[]?> NodeIds; //Keys are node-ids, values way-ids if way intersects another at this node.
        public readonly Dictionary<string, string> Tags;

        public ulong ID => GetId();
        public WayType WayType => GetHighwayType();
        public bool IsOneWay => GetOneWay();

        public Way(List<ulong>? nodeIds = null, Dictionary<string, string>? tags = null)
        {
            this.NodeIds = new();
            if(nodeIds is not null)
                foreach(ulong id in nodeIds)
                    NodeIds.TryAdd(id, null);
            this.Tags = new();
            this.Tags = tags ?? new();
        }

        private ulong GetId()
        {
            return this.Tags.TryGetValue("id", out string? idTag) ? Convert.ToUInt64(idTag) : 0;
        }

        private WayType GetHighwayType()
        {
            if (this.Tags.TryGetValue("highway", out string? highwayTag))
            {
                try
                {
                    return (WayType)Enum.Parse(typeof(WayType), highwayTag, true);
                }
                catch (ArgumentException)
                {
                    return WayType.NONE;
                }
            }
            return WayType.NONE;
        }

        private bool GetOneWay()
        {
            if (this.Tags.TryGetValue("oneway", out string? onewayTag))
            {
                return onewayTag switch
                {
                    "yes" => true,
                    "-1" => false,
                    "no" => false,
                    _ => false
                };
            }

            return true;
        }

        private byte? GetMaxSpeed(SpeedType type)
        {
            return type switch
            {
                SpeedType.road => this.Tags.TryGetValue("maxspeed", out string? tag)
                    ? Convert.ToByte(tag)
                    : null,
                SpeedType.car => this.Tags.TryGetValue("maxspeed", out string? tag)
                    ? Convert.ToByte(tag)
                    : WayUtils.SpeedCar[this.GetHighwayType()],
                SpeedType.pedestrian => this.Tags.TryGetValue("maxspeed", out string? tag)
                    ? Convert.ToByte(tag)
                    : WayUtils.SpeedPedestrian[this.GetHighwayType()],
                _ => null
            };
        }

        public override string ToString()
        {
            return $"Way {ID} along Nodes with intersecting Ways {string.Join(", ", NodeIds.Select(i => $"{i.Key}{(i.Value is null ? "" : $"/{i.Value}")}"))}\n" +
                   $"\tTags:\t{string.Join("\n\t\t", Tags.Select(t => $"{t.Key}={t.Value}"))}";
        }
    }
}
