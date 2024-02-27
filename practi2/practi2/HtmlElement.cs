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

