using System;
using System.Collections.Generic;

namespace Visitor.Pattern
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.OutputEncoding = System.Text.Encoding.UTF8;
            Console.WriteLine("Visitor Pattern Example in .NET 9\n");

            // Create documents
            List<Document> documents = new()
            {
                new HtmlDocument("<html><body>Hello World</body></html>"),
                new XMLDocument("<message>Hello XML</message>")
            };

            // Create visitors
            IDocumentVisitor saveVisitor = new SaveVisitor();
            IDocumentVisitor printVisitor = new PrintVisitor();

            // Apply visitors
            foreach (var document in documents)
            {
                document.Accept(saveVisitor);
                document.Accept(printVisitor);
                Console.WriteLine(new string('-', 40));
            }
        }
    }

    // Visitor Interface
    public interface IDocumentVisitor
    {
        void Visit(HtmlDocument htmlDocument);
        void Visit(XMLDocument xmlDocument);
    }

    // Concrete Visitor: Save
    public class SaveVisitor : IDocumentVisitor
    {
        public void Visit(HtmlDocument htmlDocument)
        {
            Console.WriteLine($"Saving HTML Document: {htmlDocument.Content}");
        }

        public void Visit(XMLDocument xmlDocument)
        {
            Console.WriteLine($"Saving XML Document: {xmlDocument.Content}");
        }
    }

    // Concrete Visitor: Print
    public class PrintVisitor : IDocumentVisitor
    {
        public void Visit(HtmlDocument htmlDocument)
        {
            Console.WriteLine($"Printing HTML Document: {htmlDocument.Content}");
        }

        public void Visit(XMLDocument xmlDocument)
        {
            Console.WriteLine($"Printing XML Document: {xmlDocument.Content}");
        }
    }

    // Abstract Element
    public abstract class Document
    {
        public string Content { get; protected set; }
        public abstract void Accept(IDocumentVisitor visitor);
    }

    // Concrete Element: HTML
    public class HtmlDocument : Document
    {
        public HtmlDocument(string content)
        {
            Content = content;
        }

        public override void Accept(IDocumentVisitor visitor)
        {
            visitor.Visit(this);
        }
    }

    // Concrete Element: XML
    public class XMLDocument : Document
    {
        public XMLDocument(string content)
        {
            Content = content;
        }

        public override void Accept(IDocumentVisitor visitor)
        {
            visitor.Visit(this);
        }
    }
}
