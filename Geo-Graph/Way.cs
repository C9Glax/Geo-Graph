namespace GeoGraph
{
    public readonly struct Way
    {
        public readonly List<ulong> NodeIds;
        public readonly Dictionary<string, object> Tags;
        public bool OneWay => IsOneWay();
        public bool Forward => IsForward();

        public Way()
        {
            this.NodeIds = new List<ulong>();
            this.Tags = new();
        }

        public Way(List<ulong>? nodeIds = null, Dictionary<string, string>? tags = null)
        {
            this.NodeIds = nodeIds ?? new();
            this.Tags = new();
            if(tags is not null)
                foreach(KeyValuePair<string, string> tag in tags)
                    AddTag(tag.Key, tag.Value);
        }
        
        public void AddTag(string key, string value)
        {
            switch (key)
            {
                case "highway":
                    try
                    {
                        this.Tags.Add(key, (WayType)Enum.Parse(typeof(WayType), value, true));
                    }
                    catch (ArgumentException)
                    {
                        this.Tags.Add(key, WayType.NONE);
                    }
                    break;
                case "maxspeed":
                    try
                    {
                        this.Tags.Add(key, Convert.ToByte(value));
                    }
                    catch (Exception)
                    {
                        this.Tags.Add(key, (byte)this.GetHighwayType());
                    }
                    break;
                case "oneway":
                    switch (value)
                    {
                        case "yes":
                            this.Tags.Add(key, true);
                            break;
                        case "-1":
                            this.Tags.Add("forward", false);
                            break;
                        case "no":
                            this.Tags.Add(key, false);
                            break;
                    }
                    break;
                case "id":
                    this.Tags.Add(key, Convert.ToUInt64(value));
                    break;
            }
        }

        public ulong GetId()
        {
            return this.Tags.ContainsKey("id") ? (ulong)this.Tags["id"] : 0;
        }

        public WayType GetHighwayType()
        {
            return this.Tags.TryGetValue("highway", out object? tag) ? (WayType)tag : WayType.NONE;
        }

        public bool IsOneWay()
        {
            return this.Tags.TryGetValue("oneway", out object? oneway) && (bool)oneway;
        }

        public byte? GetMaxSpeed(SpeedType type)
        {
            if(type == SpeedType.road)
            {
                if (this.Tags.TryGetValue("maxspeed", out object? tag))
                {
                    return (byte)tag;
                }
                else
                {
                    return null;
                }
            }
            else if(type == SpeedType.car)
            {
                if (this.Tags.TryGetValue("maxspeed", out object? tag))
                {
                    return (byte)tag;
                }
                else
                {
                    return WayUtils.SpeedCar[this.GetHighwayType()];
                }
            }
            else
            {
                return WayUtils.SpeedPedestrian[this.GetHighwayType()];
            }
        }

        public bool IsForward()
        {
            return !this.Tags.TryGetValue("forward", out object? forward) && (bool)forward!;
        }
    }
}
