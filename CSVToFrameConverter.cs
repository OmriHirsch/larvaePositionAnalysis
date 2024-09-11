using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace larvaePositionAnalysis
{
    internal class CSVToFrameConverter
    {
        public Dictionary<int,Frame> Frames { get; private set; } = new Dictionary<int, Frame>();
        private List<Larva> Larvae = new List<Larva>();
        private static string defualtLarvaType = "";
        public CSVToFrameConverter(List<string[]> rows, string defualtLarvaType = "")
        {
            foreach (var row in rows)
            {
                if (row[0] != "larvae")
                {
                    if (row[1].Length <= 1)
                        Larvae.Add(_convertRowToLarva(row));
                    if(row[1].Length > 1)
                    {
                        foreach(char type in row[1])
                        {
                            Larvae.Add(_convertRowToLarva(row, type));
                        }
                    }
                }
                    
            }

            foreach(Larva larva in Larvae)
            {
                if (Frames.ContainsKey(larva.FrameNumber))
                {
                    Frames[larva.FrameNumber].Larvae.Add(larva);
                }
                else
                {
                    Frames.Add(larva.FrameNumber, new Frame() { Larvae = new List<Larva>() { larva } });
                }
            }
        }
        private static Larva _convertRowToLarva(string[] row)
        {
            if(defualtLarvaType == "" && row[1] == "")
            {
                defualtLarvaType = InputDialog.ShowDialog("Empty Row type", "Please enter the type of Larvae that is mmising its type decleration on each row");
            }
            var newLarva = new Larva()
            {
                Position = new PointF() { X = float.Parse(row[3]), Y = float.Parse(row[4]) },
                LarvaType = row[1] == "" ? defualtLarvaType : row[1],
                FrameNumber = Int32.Parse(row[6])
            };
            return newLarva;
        }
        private static Larva _convertRowToLarva(string[] row, char larvaType)
        {
            var newLarva = new Larva()
            {
                Position = new PointF() { X = float.Parse(row[3]), Y = float.Parse(row[4]) },
                LarvaType = larvaType.ToString(),
                FrameNumber = Int32.Parse(row[6])
            };
            return newLarva;
        }
    }
}
