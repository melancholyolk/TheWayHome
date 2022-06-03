using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PropLoad : MonoBehaviour
{
    private List<PropInfo> prop_info = new List<PropInfo>();

    public TextAsset[] m_propText = new TextAsset[0];

    public List<PropInfo> FindTextByName(string[] text_names)
    {
        List<TextAsset> text_list = new List<TextAsset>();

        foreach (string text_name in text_names)
        {
            var text = System.Array.Find(m_propText, x => x.name == text_name);

            if (text_list != null)
            {
                text_list.Add(text);
            }
            else
            {
                print("找不到此文件");
            }
        }

        return ReadCSV_StagingDirection(text_list.ToArray());
    }
    public List<PropInfo> ReadCSV_StagingDirection(TextAsset[] texts)
    {
        List<string> linesOfAllFiles = new List<string>();

        for (int i = 0; i < texts.Length; i++)
        {
            //去除空项
            string[] lines = texts[i].text.Split(new char[] { '\r', '\n' }, System.StringSplitOptions.RemoveEmptyEntries);

            linesOfAllFiles.AddRange(lines);
        }
        return ReadPropText(linesOfAllFiles.ToArray());
    }

    private List<PropInfo> ReadPropText(string[] lines)
    {
        prop_info.Clear();
        foreach (string line in lines)
        {
            //数据处理  去除空格和注释
            int index = line.IndexOf(";;");
            string str = index < 0 ? line : line.Substring(0, index);
            str = str.Trim();
            char[] separator = {','};
            string[] division = str.Split(separator);
            PropInfo info = new PropInfo();
            info.prop_number = division[0];
            info.prop_name = division[1];
            info.prop_describe = division[2];
            info.prop_belong = division[3];
            info.prop_type = division[4];
            prop_info.Add(info);
        }
        return prop_info;
    }
}
