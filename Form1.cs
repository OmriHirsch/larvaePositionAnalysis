namespace larvaePositionAnalysis
{
    public partial class Form1 : Form
    {
        LarvaeAnalysisManager analysisManager;
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            CSVParser cSVParser = new CSVParser();
            CSVToFrameConverter cSVToFrameConverter;
            if (cSVParser._columns.Count != 0)
            {
                cSVToFrameConverter = new CSVToFrameConverter(cSVParser._columns);
                if(cSVToFrameConverter.Frames != null && cSVToFrameConverter.Frames.Count != 0)
                {
                    analysisManager = new LarvaeAnalysisManager(cSVToFrameConverter.Frames.Values.ToList());
                    this.treeView1.Nodes.Clear();
                    analysisManager.AvailableTypes.ForEach(x =>
                    {
                        var typeNode = new TreeNode(x);
                        typeNode.Nodes.Add($"{x} DistToSelf", "Distance To Self");
                        typeNode.Nodes.Add($"{x} DistToOther", "Distance To Other");
                        treeView1.Nodes.Add(typeNode);
                    });
                    this.Text = Path.GetFileNameWithoutExtension(cSVParser.CsvPath);
                }
            }

        }

        private void treeView1_AfterSelect(object sender, TreeViewEventArgs e)
        {
            if(e.Node != null && e.Node.Parent != null)
            {
                string type = e.Node.Name.Split(" ")[0];
                this.dataGridView1.Rows.Clear();
                this.dataGridView1.Columns.Clear();
                this.dataGridView1.Columns.Add("Average", "Average");
                this.dataGridView1.Columns.Add("StandardDiv", "Standard Diviation");
                switch (e.Node.Name.Split(" ")[1])
                {
                    case "DistToSelf":
                        analysisManager.Frames.ForEach(x =>
                        {
                            var temp = x.AverageDistanceToSameType();
                            if (temp.ContainsKey(type))
                            {
                                (double average, double standardDiviation) = temp[type];
                                this.dataGridView1.Rows.Add(average, standardDiviation);
                            }
                            else
                            {
                                this.dataGridView1.Rows.Add(0, 0);
                            }
                        });
                        break;
                    case "DistToOther":
                        analysisManager.Frames.ForEach(x =>
                        {
                            var temp = x.AverageDistanceToOtherTypes();
                            if (temp.ContainsKey(type))
                            {
                                (double average, double standardDiviation) = temp[type];
                                this.dataGridView1.Rows.Add(average, standardDiviation);
                            }
                            else
                            {
                                this.dataGridView1.Rows.Add(0, 0);
                            }
                        });
                        break;
                }
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            analysisManager = LarvaeAnalysisManager.CreateMonteCarloAnalysisManager(new List<string>() { "A", "C" }, countOfNodesInGroup: 30);
            this.treeView1.Nodes.Clear();
            analysisManager.AvailableTypes.ForEach(x =>
            {
                var typeNode = new TreeNode(x);
                typeNode.Nodes.Add($"{x} DistToSelf", "Distance To Self");
                typeNode.Nodes.Add($"{x} DistToOther", "Distance To Other");
                treeView1.Nodes.Add(typeNode);
            });
            this.Text = "Monte Carlo Simulation";
        }
    }
}