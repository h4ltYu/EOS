using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace NAudio.Sfz
{
    internal class SfzFileReader
    {
        public SfzFileReader(string fileName)
        {
            new StringBuilder();
            using (StreamReader streamReader = new StreamReader(fileName))
            {
                List<Region> list = new List<Region>();
                Region region = null;
                StringBuilder stringBuilder = new StringBuilder();
                StringBuilder stringBuilder2 = new StringBuilder();
                int num = 0;
                string text;
                while ((text = streamReader.ReadLine()) != null)
                {
                    num++;
                    int num2 = text.IndexOf('/');
                    if (num2 != -1)
                    {
                        text = text.Substring(num2);
                    }
                    for (int i = 0; i < text.Length; i++)
                    {
                        char c = text[i];
                        if (char.IsWhiteSpace(c))
                        {
                            if (stringBuilder.Length != 0)
                            {
                                if (stringBuilder2.Length == 0)
                                {
                                    throw new FormatException(string.Format("Invalid Whitespace Line {0}, Char {1}", num, i));
                                }
                                stringBuilder2.Append(c);
                            }
                        }
                        else if (c != '=' && c == '<')
                        {
                            if (text.Substring(i).StartsWith("<region>"))
                            {
                                if (region != null)
                                {
                                    list.Add(region);
                                }
                                region = new Region();
                                stringBuilder.Length = 0;
                                stringBuilder2.Length = 0;
                                i += 7;
                            }
                            else
                            {
                                if (!text.Substring(i).StartsWith("<group>"))
                                {
                                    throw new FormatException(string.Format("Unrecognised section Line {0}, Char {1}", num, i));
                                }
                                if (region != null)
                                {
                                    list.Add(region);
                                }
                                stringBuilder.Length = 0;
                                stringBuilder2.Length = 0;
                                region = null;
                                new Group();
                                i += 6;
                            }
                        }
                    }
                }
            }
        }
    }
}
