
namespace GeoGraph
{
    public class Graph
    {
        private Dictionary<ulong, Node> nodes { get; }

        public Graph()
        {
            this.nodes = new();
        }

        public bool AddNode(ulong id, Node n)
        {
            return this.nodes.TryAdd(id, n);
        }

        public Node NodeAtIndex(int i)
        {
            return this.nodes.Values.ToArray()[i];
        }

        public int GetNodeCount()
        {
            return this.nodes.Count;
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

        public Dictionary<ulong, Node> GetRandomNodes(uint count)
        {
            Random random = new Random();
            Dictionary<ulong, Node> temp = new();
            for (int i = 0; i < count; i++)
            {
                int r = random.Next();
                temp.Concat(this.nodes.Take(new Range(r, r)));
            }
            return temp;
        }

        public ulong? ClosestNodeIdToCoordinates(float lat, float lon)
        {
            if (this.nodes.Count == 0)
                return null;

            ulong? closestId = null;
            double closestDistance = double.MaxValue, distance;

            foreach (KeyValuePair<ulong, Node> kv in this.nodes)
            {
                distance = Utils.DistanceBetween(lat, lon, kv.Value.lat, kv.Value.lon);
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
            return id != null ? this.nodes[(ulong)id] : null;
        }

        public override string ToString()
        {
            return String.Format("Graph Nodes: {0}", this.nodes.Count);
        }
    }
}
