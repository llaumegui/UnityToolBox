using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Text.RegularExpressions;
using System.Linq;

public class CSVLoader
{
    TextAsset _csvFile;
    char _lineSeparator = '\n';
    char _surround = '"';
    string[] _fieldSeparator = { "\",\"" };

    public void LoadCSV()
    {
        _csvFile = Resources.Load<TextAsset>("localization");
    }

    public Dictionary<string,string> GetDictionnaryValues(string id)
    {
        Dictionary<string, string> dictionary = new Dictionary<string, string>();
        string[] lines = _csvFile.text.Split(_lineSeparator);
        int index = -1;
        string[] headers = lines[0].Split(_fieldSeparator, System.StringSplitOptions.None);

        for(int i =0;i<headers.Length;i++)
        {
            if(headers[i].Contains(id))
            {
                index = i;
                break;
            }
        }

        Regex CVSParser = new Regex(",(?=(?:[^\"]*\"[^\"]*\")*(?![^\"]*\"))");

        for(int i =1;i<lines.Length;i++)
        {
            string line = lines[i];
            string[] fields = CVSParser.Split(line);

            for(int f=0;f<fields.Length;f++)
            {
                fields[f] = fields[f].TrimStart(' ', _surround);
                fields[f] = fields[f].TrimEnd(_surround);
            }

            if(fields.Length>index)
            {
                var key = fields[0];
                if (dictionary.ContainsKey(key)) { continue; }
                var value = fields[index];
                dictionary.Add(key, value);
            }
        }

        return dictionary;
    }

#if UNITY_EDITOR
    public void Add(string key,string value)
    {
        string appended = string.Format("\n\"{0}\",\"{1}\",\"\"", key, value);
        File.AppendAllText("Assets/Resources/localization.csv", appended);

        UnityEditor.AssetDatabase.Refresh();
    }

    public void Remove(string key)
    {
        string[] lines = _csvFile.text.Split(_lineSeparator);
        string[] keys = new string[lines.Length];

        for(int i =0;i<lines.Length;i++)
        {
            string line = lines[i];
            keys[i] = line.Split(_fieldSeparator, System.StringSplitOptions.None)[0];
        }

        int id = -1;
        for(int i =0;i<keys.Length;i++)
        {
            if(keys[i].Contains(key))
            {
                id = i;
                break;
            }
        }
        if(id>-1)
        {
            string[] newLines;
            newLines = lines.Where(w => w != lines[id]).ToArray();
            string replaced = string.Join(_lineSeparator.ToString(), newLines);
            File.WriteAllText("Assets/Resources/localization.csv", replaced);
        }
    }
#endif
}
