/*using Microsoft.VisualBasic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace practi2
{
    public class HtmlElement
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public List<string> Attributes { get; set; } = new List<string>();
        public List<string> Classes { get; set; } = new List<string>();
        public string InnerHtml { get; set; }
        public HtmlElement Parent { get; set; }
        public List<HtmlElement> Children { get; set; } = new List<HtmlElement>();

        public IEnumerable<HtmlElement> Descendants()
        {
            var q = new Queue<HtmlElement>();
            q.Enqueue(this);
            while (q != null)
            {
                var currentElement = q.Dequeue();
                foreach (var child in currentElement.Children)
                {
                    q.Enqueue(child);
                    yield return child;
                }
            }
        }

        internal IEnumerable<HtmlElement> FindElements(Selector selector)
        {
            return FindElements(selector);
        }

        IEnumerable<HtmlElement> Ancestors()
        {
            HtmlElement hE = this;
            while (hE.Parent != null)
            {
                yield return hE.Parent;
                hE = hE.Parent;
            }
        }


    }



}
*/

 using System;
using System.Collections.Generic;
using System.Linq;

namespace practi2
{
    public class HtmlElement
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public List<string> Attributes { get; set; } = new List<string>();
        public List<string> Classes { get; set; } = new List<string>();
        public string InnerHtml { get; set; }
        public HtmlElement Parent { get; set; }
        public List<HtmlElement> Children { get; set; } = new List<HtmlElement>();

        public IEnumerable<HtmlElement> Descendants()
        {
            var stack = new Stack<HtmlElement>();
            stack.Push(this);

            while (stack.Count > 0)
            {
                var currentElement = stack.Pop();
                yield return currentElement;

                foreach (var child in currentElement.Children)
                {
                    stack.Push(child);
                }
            }
        }

        public IEnumerable<HtmlElement> FindElements(Selector selector)
        {
            foreach (var element in Descendants())
            {
                if (Class1.Matches(element, selector))
                {
                    yield return element;
                }
            }
        }
    }
}

