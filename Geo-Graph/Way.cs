namespace GeoGraph
{
    public struct Way
    {
        public List<ulong> nodeIds;
        private Dictionary<string, object> tags;


        public static Dictionary<wayType, byte> speedcar = new() {
                { wayType.NONE, 0 },
                { wayType.motorway, 110 },
                { wayType.trunk, 100 },
                { wayType.primary, 80 },
                { wayType.secondary, 80 },
                { wayType.tertiary, 70 },
                { wayType.unclassified, 20 },
                { wayType.residential, 10 },
                { wayType.motorway_link, 50 },
                { wayType.trunk_link, 50 },
                { wayType.primary_link, 30 },
                { wayType.secondary_link, 25 },
                { wayType.tertiary_link, 25 },
                { wayType.living_street, 10 },
                { wayType.service, 0 },
                { wayType.pedestrian, 0 },
                { wayType.track, 0 },
                { wayType.bus_guideway, 0 },
                { wayType.escape, 0 },
                { wayType.raceway, 0 },
                { wayType.road, 25 },
                { wayType.busway, 0 },
                { wayType.footway, 0 },
                { wayType.bridleway, 0 },
                { wayType.steps, 0 },
                { wayType.corridor, 0 },
                { wayType.path, 0 },
                { wayType.cycleway, 0 },
                { wayType.construction, 0 }
            };

        public static Dictionary<wayType, byte> speedped = new() {
                { wayType.NONE, 0 },
                { wayType.motorway, 0 },
                { wayType.trunk, 0 },
                { wayType.primary, 0 },
                { wayType.secondary, 0 },
                { wayType.tertiary, 0 },
                { wayType.unclassified, 1 },
                { wayType.residential, 3 },
                { wayType.motorway_link, 0 },
                { wayType.trunk_link, 0 },
                { wayType.primary_link, 0 },
                { wayType.secondary_link, 0 },
                { wayType.tertiary_link, 0 },
                { wayType.living_street, 5 },
                { wayType.service, 2 },
                { wayType.pedestrian, 5 },
                { wayType.track, 0 },
                { wayType.bus_guideway, 0 },
                { wayType.escape, 0 },
                { wayType.raceway, 0 },
                { wayType.road, 3 },
                { wayType.busway, 0 },
                { wayType.footway, 4 },
                { wayType.bridleway, 1 },
                { wayType.steps, 2 },
                { wayType.corridor, 3 },
                { wayType.path, 4 },
                { wayType.cycleway, 2 },
                { wayType.construction, 0 }
            };
        public enum wayType { NONE, motorway, trunk, primary, secondary, tertiary, unclassified, residential, motorway_link, trunk_link, primary_link, secondary_link, tertiary_link, living_street, service, pedestrian, track, bus_guideway, escape, raceway, road, busway, footway, bridleway, steps, corridor, path, cycleway, construction }

        public enum speedType { pedestrian, car, road }

        public Way()
        {
            this.nodeIds = new List<ulong>();
            this.tags = new();
        }
        public void AddTag(string key, string value)
        {
            switch (key)
            {
                case "highway":
                    try
                    {
                        this.tags.Add(key, (wayType)Enum.Parse(typeof(wayType), value, true));
                    }
                    catch (ArgumentException)
                    {
                        this.tags.Add(key, wayType.NONE);
                    }
                    break;
                case "maxspeed":
                    try
                    {
                        this.tags.Add(key, Convert.ToByte(value));
                    }
                    catch (Exception)
                    {
                        this.tags.Add(key, (byte)this.GetHighwayType());
                    }
                    break;
                case "oneway":
                    switch (value)
                    {
                        case "yes":
                            this.tags.Add(key, true);
                            break;
                        case "-1":
                            this.tags.Add("forward", false);
                            break;
                        case "no":
                            this.tags.Add(key, false);
                            break;
                    }
                    break;
                case "id":
                    this.tags.Add(key, Convert.ToUInt64(value));
                    break;
            }
        }

        public ulong GetId()
        {
            return this.tags.ContainsKey("id") ? (ulong)this.tags["id"] : 0;
        }

        public wayType GetHighwayType()
        {
            return this.tags.ContainsKey("highway") ? (wayType)this.tags["highway"] : wayType.NONE;
        }

        public bool IsOneWay()
        {
            return this.tags.ContainsKey("oneway") ? (bool)this.tags["oneway"] : false;
        }

        public byte? GetMaxSpeed(speedType type)
        {
            if(type == speedType.road)
            {
                if (this.tags.ContainsKey("maxspeed"))
                {
                    return (byte)this.tags["maxspeed"];
                }
                else
                {
                    return null;
                }
            }
            else if(type == speedType.car)
            {
                if (this.tags.ContainsKey("maxspeed"))
                {
                    return (byte)this.tags["maxspeed"];
                }
                else
                {
                    return speedcar[this.GetHighwayType()];
                }
            }
            else
            {
                return speedped[this.GetHighwayType()];
            }
        }

        public bool IsForward()
        {
            return this.tags.ContainsKey("forward") ? (bool)this.tags["forward"] : true;
        }
    }
}
