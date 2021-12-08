using System.IO;
using System.Linq;
using System.Text;

namespace AAC_Graph.Сервисы.FileIO
{
    public static class FileReaderWriter
    {
        private static readonly string _baseApplicationPath = Directory.GetCurrentDirectory();
        
        public static readonly string InputFilePath = Path.Combine(_baseApplicationPath,@"ФайлыВвода_Вывода\Input.txt");
        public static readonly string OutputFilePath = Path.Combine(_baseApplicationPath,@"ФайлыВвода_Вывода\Output.txt");

        
        /// <param name="filePath">путь к файлу</param>
        /// <param name="result"> сolorsAmount - кол-во цветов, coloredNodes - массив узлов,где индекс - номер узла, а значение это номер цвета в который он раскрашен</param>
        public static void WriteGraphColorResult(string filePath, (int colorsAmount, int[] coloredNodes) result)
        {

            using var fileStream = new FileStream(filePath, FileMode.Create);
            var (colorsAmount, coloredNodes) = result;
            var header = $"Кол-во узлов:{coloredNodes.Length} \n" +
            $"Кол-во цветов:{colorsAmount}\n";
            var headerData = Encoding.Default.GetBytes(header);
            fileStream.Write(headerData, 0, headerData.Length);

            for (var i = 0; i < coloredNodes.Length; i++)
            {
                var color = coloredNodes[i];
                var line = $"{i} —- {color}\n";
                var data = Encoding.Default.GetBytes(line);
                fileStream.Write(data, 0, data.Length);
            }
        }
        public static byte[,] ReadAdjacencyMatrix(string filePath)
        {
            if (!File.Exists(filePath)) return null;

            using var fileReader = new StreamReader(filePath);
            var dimension = int.Parse(fileReader.ReadLine());
            var adjMatrix = new byte[dimension, dimension];
            var index = 0;
            string line;
            while ((line = fileReader.ReadLine()) != null)
            {
                var values = line.Split(',').Select(byte.Parse).ToArray();
                for (var i = 0; i < dimension; i++)
                {
                    adjMatrix[index, i] = values[i];
                }

                index++;
            }

            return adjMatrix;
        }       
    }
}