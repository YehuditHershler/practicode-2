using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using practi2;
using System.Text.RegularExpressions;

//IEnumerable<HtmlElement> results = rootElement.FindElements(selector);


// טעינת HTML מאתר אינטרנט
string url = "https://learn.malkabruk.co.il/practicode/projects/pract-2/#_2";
string html = Load(url).Result;

// יצירת סלקטור
string query = "div .md-top";
Selector selector = new Selector(query);

// בניית עץ של אובייקטי HtmlElement
HtmlElement rootElement = CreateHtmlTree(html);

// חיפוש אלמנטים לפי סלקטור
IEnumerable<HtmlElement> results = (IEnumerable<HtmlElement>)rootElement.FindElements(selector);

// הדפסת תוצאות החיפוש
foreach (var element in results)
{
    Console.WriteLine(element.Name);
}

// פונקציה לטעינת HTML מאתר אינטרנט
async static Task<string> Load(string url)
{
    HttpClient client = new HttpClient();
    var response = await client.GetAsync(url);
    return await response.Content.ReadAsStringAsync();
}

// פונקציה לבניית עץ של אובייקטי HtmlElement
static HtmlElement CreateHtmlTree(string html)
{
    // ניקוי HTML
    html = new Regex("[\\r\\n\\t]").Replace(new Regex("\\s{2,}").Replace(html, ""), "");
    var htmlLines = new Regex("<(.*?)>").Split(html).Where(x => x.Length > 0).ToArray();

    // יצירת אלמנט שורש
    HtmlElement rootElement = new HtmlElement { Name = htmlLines[1].Split(' ')[0] };

    // פירוק HTML
    ParseHtml(rootElement, htmlLines.Skip(2).ToList());

    return rootElement;
}

// פונקציה רקורסיבית לפירוק HTML
static void ParseHtml(HtmlElement rootElement, List<string> htmlLines)
{
    HtmlElement currentParent = rootElement;
    foreach (var line in htmlLines)
    {
        if (line.StartsWith("/html"))
            break;

        // סגירת תגית
        if (line.StartsWith("/"))
        {
            currentParent = currentParent.Parent;
            continue;
        }

        string tagName = line.Split(' ')[0];
        // טקסט פנימי
        if (!HtmlHelper.Instance.AllHtmlTags.Contains(tagName))
        {
            currentParent.InnerHtml += line;
            continue;
        }

        // יצירת אלמנט ילד
        HtmlElement child = new HtmlElement { Name = tagName, Parent = currentParent };
        var attributes = new Regex("([^\\s]?)=\"(.?)\"").Matches(line);
        foreach (var attr in attributes)
        {
            string attributeName = attr.ToString().Split('=')[0];
            string attributeValue = attr.ToString().Split('=')[1].Replace("\"", "");
            if (attributeName.ToLower() == "class")
                child.Classes.AddRange(attributeValue.Split(' '));
            else if (attributeName.ToLower() == "id")
                child.Id = attributeValue;
            else
            {
                child.Attributes.Add(attributeName);
                child.Attributes.Add(attributeValue);
            }
        }

        currentParent.Children.Add(child);

        // המשך פירוק אם לא תגית סוגרת
        if (!HtmlHelper.Instance.SelfClosingTags.Contains(tagName) && !line.EndsWith("/"))
            currentParent = child;
    }
}
