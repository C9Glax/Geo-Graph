
namespace GeoGraph
{
    public class Graph
    {
        private Dictionary<ulong, Node> nodes { get; }

        public Graph()
        {
            this.nodes = new();
        }

        public void Trim()
        {
            List<ulong> toRemove = new();
            foreach(KeyValuePair<ulong, Node> pair in this.nodes)
            {
                if(pair.Value.edges.Count == 0)
                    toRemove.Add(pair.Key);
            }
            foreach(ulong key in toRemove)
                this.nodes.Remove(key);
            this.nodes.TrimExcess();
        }

        public bool AddNode(ulong id, Node n)
        {
            return this.nodes.TryAdd(id, n);
        }

        public int GetNodeCount()
        {
            return this.nodes.Count;
        }

        public ulong? GetNodeId(Node n)
        {
            foreach(KeyValuePair<ulong, Node> kv in this.nodes)
            {
                if(kv.Value.Equals(n)) return kv.Key;
            }
            return null;
        }

        public Node? GetNode(ulong id)
        {
            if (this.nodes.TryGetValue(id, out Node? n))
                return n;
            else
                return null;
        }

        public bool ContainsNode(ulong id)
        {
            return this.nodes.ContainsKey(id);
        }

        public bool ContainsNode(Node n)
        {
            return this.nodes.Values.Contains(n);
        }

        public bool RemoveNode(ulong id)
        {
            return this.nodes.Remove(id);
        }

        /// <summary>
        /// DEPRECATED Use RemoveNode(ulong id) wherever possible for better runtime.
        /// </summary>
        /// <param name="n">Node n to remove</param>
        /// <returns></returns>
        public bool RemoveNode(Node n)
        {
            ulong? key = null;
            foreach(KeyValuePair<ulong, Node> kv in this.nodes)
            {
                if (kv.Value.Equals(n))
                {
                    key = kv.Key;
                    break;
                }
            }
            if(key != null)
            {
                this.nodes.Remove((ulong)key);
                return true;
            }
            else
            {
                return false;
            }
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
                int r = random.Next(0, this.nodes.Count);
                KeyValuePair<ulong, Node> kv = this.nodes.Take(new Range(r, r+1)).First();
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
        public ulong? ClosestNodeIdToCoordinates(float lat, float lon)
        {
            ulong? closestId = null;
            double closestDistance = double.MaxValue, distance;

            foreach (KeyValuePair<ulong, Node> kv in this.nodes)
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
        public Node? ClosestNodeToCoordinates(float lat, float lon)
        {
            ulong? id = ClosestNodeIdToCoordinates(lat, lon);
            return id != null ? this.nodes[(ulong)id] : null;
        }

        public override string ToString()
        {
            return String.Format("Graph Nodes: {0}", this.nodes.Count);
        }
    }
}
