using Half = SystemHalf.Half;

namespace GeoGraph
{
    public class Graph
    {
        private Dictionary<ulong, Node> Nodes { get; }
        public int NodeCount => Nodes.Count;

        public Graph()
        {
            this.Nodes = new();
        }

        public void Trim()
        {
            List<ulong> toRemove = this.Nodes.Where(node => node.Value.Edges.Count == 0).Select(kv => kv.Key).ToList();
            foreach(ulong key in toRemove)
                this.Nodes.Remove(key);
            this.Nodes.TrimExcess();
        }

        public bool AddNode(ulong id, Node n)
        {
            return this.Nodes.TryAdd(id, n);
        }

        public ulong? GetNodeId(Node n)
        {
            return this.Nodes.FirstOrDefault(node => node.Value == n).Key;
        }

        public Node? GetNode(ulong id)
        {
            return this.Nodes.FirstOrDefault(node => node.Key == id).Value;
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

        /// <summary>
        /// DEPRECATED Use RemoveNode(ulong id) wherever possible for better runtime.
        /// </summary>
        /// <param name="n">Node n to remove</param>
        /// <returns></returns>
        public bool RemoveNode(Node n)
        {
            ulong? key = this.GetNodeId(n);
            if (key is null)
                return false;
            return this.RemoveNode((ulong)key);
        }

        /// <summary>
        /// Returns a Dictionary with count as length of randomly selected Nodes
        /// </summary>
        /// <param name="count">Count of Dictionary Entries</param>
        /// <returns>Dictionary of Node-ids and Node</returns>
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

        /// <summary>
        /// Returns the Node-id closest to the given parameter-coordinates
        /// </summary>
        /// <param name="lat"></param>
        /// <param name="lon"></param>
        /// <returns>id or null if graph is empty</returns>
        public ulong? ClosestNodeIdToCoordinates(Half lat, Half lon)
        {
            ulong? closestId = null;
            Half closestDistance = Half.MaxValue, distance;

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

        /// <summary>
        /// Returns the Node closest to the given parameter-coordinates
        /// </summary>
        /// <param name="lat"></param>
        /// <param name="lon"></param>
        /// <returns>Node or null if graph is empty</returns>
        public Node? ClosestNodeToCoordinates(Half lat, Half lon)
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
