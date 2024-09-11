using System;
using System.Collections.Generic;
using System.IO;
using System.Windows.Forms;

namespace larvaePositionAnalysis
{
    internal class CSVParser
    {
        public string CsvPath = "";
        private string[] _allFileLines;
        public List<string[]> _columns { get; private set; } = new();
        public CSVParser()
        {
            CsvPath = OpenCsvFile();

            if (string.IsNullOrEmpty(CsvPath))
            {
                Console.WriteLine("No CSV file selected.");
                return;
            }

            _allFileLines = File.ReadAllLines(CsvPath);

            _columns = ParseCsv();


            Console.WriteLine("CSV Parsing complete.");
        }

        public string OpenCsvFile()
        {
            using (OpenFileDialog openFileDialog = new OpenFileDialog())
            {
                openFileDialog.InitialDirectory = "c:\\";
                openFileDialog.Filter = "CSV files (*.csv)|*.csv|All files (*.*)|*.*";
                openFileDialog.FilterIndex = 1;
                openFileDialog.RestoreDirectory = true;

                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    return openFileDialog.FileName;
                }
            }

            return "";
        }

        public List<string[]> ParseCsv()
        {
            var parsedData = new List<string[]>();

            foreach (var line in _allFileLines)
            {

                var columns = line.Split(',');


                parsedData.Add(columns);
            }

            return parsedData;
        }
    }
}

