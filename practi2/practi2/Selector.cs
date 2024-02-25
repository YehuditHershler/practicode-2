using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace practi2
{
    public class Selector
    {
        public string TagName { get; set; }
        public string Id { get; set; }
        public List<string> Classes { get; set; }
        public Selector Parent { get; set; }
        public Selector Child { get; set; }
        public Selector(string asking)
        {
            Classes = new List<string>();
            StringToSelector(asking);
        }
        public Selector(){ }

        public static Selector StringToSelector(string asking)
        {
            var root = new Selector();
            var currentSelector = root;

            // פיצול המחרוזת למילים לפי רווחים
            string[] parts = asking.Split(' ');

            foreach (string part in parts)
            {
                // פיצול החלק לבוררים לפי # ו- .
                string[] selectors = part.Split(new char[] { '#', '.' }, StringSplitOptions.RemoveEmptyEntries);

                foreach (string selectorString in selectors)
                {
                    var selector = new Selector();

                    if (selectorString.StartsWith("#"))
                    {
                        selector.Id = selectorString.Substring(1);  // הסרת # מוביל
                    }
                    else if (selectorString.StartsWith("."))
                    {
                        selector.Classes.Add(selectorString.Substring(1));  // הסרת . מוביל
                    }
                    else
                    {
                        // אימות שם תג אם אפשרי
                        if (HtmlHelper.Instance.AllHtmlTags.Contains(selectorString))
                        {
                            selector.TagName = selectorString;
                        }
                        else
                        {
                            // טיפול בשם תג לא חוקי
                            Console.WriteLine($"Error!! the TagName {selectorString} is not valid!!!");
                        }
                    }

                    currentSelector.Child = selector;
                    currentSelector = selector;
                }
            }

            return root;
        }

    }
}
