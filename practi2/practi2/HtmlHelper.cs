using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Text.Json;

namespace practi2
{
    public class HtmlHelper
    {
        private readonly static HtmlHelper _instance = new HtmlHelper();
        
        public static HtmlHelper Instance => _instance;

        public string[] AllHtmlTags { get; set; }
        public string[] SelfClosingTags { get; set; }

        private HtmlHelper()
        {
            AllHtmlTags = JsonSerializer.Deserialize<string[]>(File.ReadAllText("HtmlTags.json"));
            SelfClosingTags = JsonSerializer.Deserialize<string[]>(File.ReadAllText("HtmlVoidTags.json"));
        }
    }

}
