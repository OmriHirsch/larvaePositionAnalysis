using System;

namespace larvaePositionAnalysis
{
    using Microsoft.VisualBasic;
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public class Node
    {
        public string Type { get; set; }
        public double X { get; set; }
        public double Y { get; set; }

        public Node(string type, double x, double y)
        {
            Type = type;
            X = x;
            Y = y;
        }

        // Method to compute distance between this node and another node
        public double DistanceTo(Node other)
        {
            double dx = X - other.X;
            double dy = Y - other.Y;
            return Math.Sqrt(dx * dx + dy * dy);
        }
    }

    public class NodeGroup
    {
        public List<Node> Nodes { get; private set; }

        public NodeGroup()
        {
            Nodes = new List<Node>();
        }

        public void AddNode(Node node)
        {
            Nodes.Add(node);
        }

            // Method to compute the average and standard deviation of distances between nodes of the same type
    public Dictionary<string, (double Average, double StandardDeviation)> AverageDistanceToSameType()
    {
        var result = new Dictionary<string, (double Average, double StandardDeviation)>();
        var groupedNodes = Nodes.GroupBy(n => n.Type);

        foreach (var group in groupedNodes)
        {
            var nodesOfType = group.ToList();
            List<double> distances = new List<double>();

            for (int i = 0; i < nodesOfType.Count; i++)
            {
                for (int j = i + 1; j < nodesOfType.Count; j++)
                {
                    distances.Add(nodesOfType[i].DistanceTo(nodesOfType[j]));
                }
            }

            if (distances.Count == 0)
            {
                result[group.Key] = (0, 0);
            }
            else
            {
                double average = distances.Average();
                double standardDeviation = Math.Sqrt(distances.Average(d => Math.Pow(d - average, 2)));
                result[group.Key] = (average, standardDeviation);
            }
        }

        return result;
    }

    // Method to compute the average and standard deviation of distances between nodes of one type and all other types
    public Dictionary<string, (double Average, double StandardDeviation)> AverageDistanceToOtherTypes()
    {
        var result = new Dictionary<string, (double Average, double StandardDeviation)>();
        var groupedNodes = Nodes.GroupBy(n => n.Type);

        foreach (var group in groupedNodes)
        {
            var nodesOfType = group.ToList();
            var nodesNotOfType = Nodes.Where(n => n.Type != group.Key).ToList();

            if (nodesNotOfType.Count == 0)
            {
                result[group.Key] = (0, 0);
                continue;
            }

            List<double> distances = new List<double>();

            foreach (var node in nodesOfType)
            {
                foreach (var otherNode in nodesNotOfType)
                {
                    distances.Add(node.DistanceTo(otherNode));
                }
            }

            double average = distances.Average();
            double standardDeviation = Math.Sqrt(distances.Average(d => Math.Pow(d - average, 2)));
            result[group.Key] = (average, standardDeviation);
        }

        return result;
    }
    }
    public static class Extentions
    {
        public static string Random(this List<string> strings, Random rand)
        {
            if (strings.Count == 0) return "";
            if(strings.Count == 1)return strings[0];

            return strings[rand.Next(strings.Count)];
        }
    }
    internal class LarvaeAnalysisManager
    {
        public List<NodeGroup> Frames { get; private set; } = new();

        public List<string> AvailableTypes { get; private set; } = new();

        public LarvaeAnalysisManager(List<Frame> frames)
        {
            Frames = frames.Select(x =>
            {
                var nodes = new NodeGroup();
                x.Larvae.ForEach(larva =>
                {
                    if(!AvailableTypes.Contains(larva.LarvaType)) AvailableTypes.Add(larva.LarvaType);
                    nodes.AddNode(new Node(larva.LarvaType, larva.Position.X, larva.Position.Y));
                });
                return nodes;
            }).ToList();
        }
        private LarvaeAnalysisManager(List<NodeGroup> frames, List<string> availableTypes)
        {
            Frames = frames;
            AvailableTypes = availableTypes;
        }

        public static LarvaeAnalysisManager CreateMonteCarloAnalysisManager(List<string> availableTypes, int countOfFrames = 600, int countOfNodesInGroup = 10)
        {
            Random random = new Random(420);
            List<NodeGroup> MonteCarloNodeGroups = new List<NodeGroup>();
            for (int i = 0; i < countOfFrames; i++)
            {
                Dictionary<string, bool> usedTypes = new Dictionary<string, bool>();
                NodeGroup currentGroup = new NodeGroup();
                availableTypes.ForEach(x => usedTypes.Add(x, false));
                for(int nodeNum = 0; nodeNum < 10; nodeNum++)
                {
                    string currentNodeType = availableTypes.Random(random); // TODO: finish this;
                    usedTypes[currentNodeType] = true;
                    Node randomNode = new Node(currentNodeType, random.NextDouble() * 1024, random.NextDouble() * 1024);
                    currentGroup.AddNode(randomNode);
                }
                if (usedTypes.ContainsValue(false))
                {
                    foreach(KeyValuePair<string,bool> type in usedTypes)
                    {
                        if(type.Value == false)
                        {
                            currentGroup.AddNode(new Node(type.Key, random.NextDouble() * 1024, random.NextDouble() * 1024));
                        }
                    }
                }
                MonteCarloNodeGroups.Add(currentGroup);
            }
            return new LarvaeAnalysisManager(MonteCarloNodeGroups, availableTypes);
        }
    }

    public class DataPoint
    {
        public double Average { get; set; }
        public double StandardDeviation { get; set; }
    }
}
