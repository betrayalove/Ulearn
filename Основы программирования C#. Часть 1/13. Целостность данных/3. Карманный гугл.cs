// Вставьте сюда финальное содержимое файла Indexer.cs

// Эту не делал, но нашел для вас...

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
 
namespace PocketGoogle
{
    public class Indexer : IIndexer
    {
        public Dictionary<int, Dictionary<string, List<int>>> Library = new Dictionary<int, Dictionary<string, List<int>>>();
 
        public void Add(int id, string documentText)
        {
            string[] list = documentText.Split(' ', '.', ',', '!', '?', ':', '-', '\r', '\n');
            var diction = new Dictionary<string, List<int>>();
            if (list != null)
            {
                int k = 0;
                foreach(var item in list)
                {
                    if (!diction.ContainsKey(item))
                    {
                        diction.Add(item, new List<int>());
                        diction[item].Add(k);
                    }
                    else
                    {
                        diction[item].Add(k);
                    }
                    k += item.Length + 1;
                }
                
            }
            Library.Add(id, diction);
        }
 
        public List<int> GetIds(string word)
        {
            var list = new List<int>();
            foreach (var document in Library)
                if (document.Value.ContainsKey(word))
                    list.Add(document.Key);
            return list;
        }
 
        public List<int> GetPositions(int id, string word)
        {
            var list = new List<int>();
            foreach (var document in Library[id].Keys)
                if (document == word)
                    list = Library[id][document];
            return list;
        }
 
        public void Remove(int id)
        {
            Library.Remove(id);
        }
    }
}