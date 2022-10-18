// See https://aka.ms/new-console-template for more information

List<string> allTheFiles = new List<string>();

{
    string[] files = System.IO.Directory.GetFiles(Directory.GetCurrentDirectory());
    foreach (string file in files)
    {
        if (file.Contains("txt"))
        {
            allTheFiles.Add(file);
        }
    }
}

foreach (string file in allTheFiles)
{
    string[] path = file.Split("\\");
    string fileName = path[path.Length - 1];
    fileName = fileName.Substring(0, fileName.IndexOf('.'));
    string csvfilePath = fileName+".csv";
    string[] lines = System.IO.File.ReadAllLines(file);

    Dictionary<string, string> map = new Dictionary<string, string>();

    foreach (string line in lines)
    {
        var parts = line.Split(": ");

        if (parts.Length == 2)
        {
            bool success = true;

            int i = 1;
            string baseKey = parts[0];
            do
            {
                success = map.TryAdd(parts[0], parts[1]);
                if (!success)
                {
                    parts[0] = baseKey + i;
                    i++;
                }
            }
            while (!success);
        }
    }

    File.Delete(csvfilePath);

    foreach (var item in map)
    {
        File.AppendAllText(csvfilePath, item.Key + ';');
    }
    File.AppendAllText(csvfilePath, Environment.NewLine);
    foreach (var item in map)
    {
        File.AppendAllText(csvfilePath, item.Value + ';');
    }
}
