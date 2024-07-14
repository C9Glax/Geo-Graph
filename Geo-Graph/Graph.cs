namespace GeoGraph
{
    public class Graph
    {
        private Dictionary<ulong, Node> Nodes { get; }
        private Dictionary<ulong, Way> Ways { get; }
        public int NodeCount => Nodes.Count;
        public int WayCount => Ways.Count;

        public Graph()
        {
            this.Nodes = new();
            this.Ways = new();
        }

        public void Trim()
        {
            List<ulong> toRemove = this.Nodes.Where(node => node.Value.WayIds.Count == 0).Select(kv => kv.Key).ToList();
            foreach(ulong key in toRemove)
                this.Nodes.Remove(key);
            this.Nodes.TrimExcess();
            GC.Collect();
        }

        public void ConcatGraph(Graph graph)
        {
            foreach (KeyValuePair<ulong, Node> kv in graph.Nodes)
                this.AddNode(kv.Key, kv.Value);
            foreach(Way w in graph.Ways.Values)
                this.AddWay(w);
            RecalculateIntersections();
        }

        public void RecalculateIntersections()
        {
            foreach (ulong nodeId in this.Nodes.Keys)
            {
                List<Way> waysWithIntersectionAtThisNode = this.Ways.Values.Where(way => way.NodeIds.Keys.Contains(nodeId)).ToList();
                if(waysWithIntersectionAtThisNode.Count < 2)
                    continue;
                
                foreach (Way way in waysWithIntersectionAtThisNode)
                {
                    way.NodeIds[nodeId] = waysWithIntersectionAtThisNode.Except(new []{way}).Select(w => way.ID).ToArray();
                }                   
            }
        }

        public bool AddNode(ulong id, Node n)
        {
            return this.Nodes.TryAdd(id, n);
        }

        public ulong? GetNodeId(Node n)
        {
            return this.Nodes.ContainsValue(n) ? this.Nodes.First(kv => kv.Value.Equals(n)).Key : null;
        }

        public Node? GetNode(ulong id)
        {
            return this.Nodes.TryGetValue(id, out Node node) ? node : null;
        }

        public bool ContainsNode(ulong id)
        {
            return this.Nodes.ContainsKey(id);
        }

        public bool ContainsNode(Node n)
        {
            return this.Nodes.Values.Contains(n);
        }

        public bool RemoveNode(ulong id)
        {
            return this.Nodes.Remove(id);
        }
        
        public bool RemoveNode(Node n)
        {
            ulong? key = this.GetNodeId(n);
            if (key is null)
                return false;
            return this.RemoveNode((ulong)key);
        }

        public bool AddWay(Way way)
        {
            return this.Ways.TryAdd(way.ID, way);
        }

        public Way? GetWay(ulong id)
        {
            return this.Ways.TryGetValue(id, out Way way) ? way : null;
        }

        public bool RemoveWay(ulong id)
        {
            return this.Ways.Remove(id);
        }

        public bool RemoveWay(Way way)
        {
            return this.RemoveWay(way.ID);
        }
        
        public Dictionary<ulong, Node> GetRandomNodes(uint count)
        {
            Random random = new Random();
            Dictionary<ulong, Node> temp = new();
            for (int i = 0; i < count; i++)
            {
                int r = random.Next(0, this.Nodes.Count);
                KeyValuePair<ulong, Node> kv = this.Nodes.Take(new Range(r, r+1)).First();
                temp.Add(kv.Key, kv.Value);
            }
            return temp;
        }
        
        public ulong? ClosestNodeIdToCoordinates(float lat, float lon)
        {
            ulong? closestId = null;
            double closestDistance = double.MaxValue, distance;

            foreach (KeyValuePair<ulong, Node> kv in this.Nodes)
            {
                distance = Utils.DistanceBetween(kv.Value, lat, lon);
                if (distance < closestDistance)
                {
                    closestDistance = distance;
                    closestId = kv.Key;
                }
            }
            return closestId;
        }
        
        public Node? ClosestNodeToCoordinates(float lat, float lon)
        {
            ulong? id = ClosestNodeIdToCoordinates(lat, lon);
            return id is not null ? this.GetNode((ulong)id) : null;
        }

        public override string ToString()
        {
            return $"Graph Nodes: {this.Nodes.Count}";
        }
    }
}
