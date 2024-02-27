using System;
using System.Collections.Generic;
using System.Linq;

namespace practi2
{
    public static class Class1
    {
        public static IEnumerable<HtmlElement> FindElements(this HtmlElement element, Selector selector)
        {
            if (Matches(element, selector))
                yield return element;
            
            if (selector.Child != null)
            {
                foreach (var child in element.Descendants())
                {
                    foreach (var foundElement in child.FindElements(selector.Child))
                    {
                        yield return foundElement;
                    }
                }
            }
        }

        private static bool Matches(HtmlElement element, Selector selector)
        {
            if (selector.TagName != null && element.Name != selector.TagName)
                return false;
            if (selector.Id != null && element.Id != selector.Id)
                return false;
            if (selector.Classes.Any() && !selector.Classes.All(c => element.Classes.Contains(c)))
                return false;
            return true;
        }
    }
}
