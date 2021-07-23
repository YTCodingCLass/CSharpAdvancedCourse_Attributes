using System;
using System.Collections.Generic;
using System.Linq;

namespace Attributes
{
    class Program
    {
        static void Main(string[] args)
        {
            Type testType = typeof(Test);

            var documetationAtt = (DocumentationAttribute) testType.GetCustomAttributes(typeof(DocumentationAttribute), false).FirstOrDefault();
            if(documetationAtt != null)
            {
                Console.WriteLine(documetationAtt.Text);
            }

            var attributes = testType.GetProperties().Where(prop => Attribute.IsDefined(prop, typeof(DocumentationAttribute)))
                .Select(prop => (DocumentationAttribute)prop.GetCustomAttributes(typeof(DocumentationAttribute), false).FirstOrDefault());

            foreach (var att in attributes)
            {
                Console.WriteLine(att.Text);
            }
        }
    }

    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Property)]
    class DocumentationAttribute : Attribute
    {
        public string Text { get; set; }
        public DocumentationAttribute(string text)
        {
            Text = text;
        }
    }

    [Documentation("Test: is a Test Class")]
    class Test
    {
        [Documentation("TestProperty: is a Test Property")]
        public int TestProperty { get; set; }

        [Documentation("TestProperty: is a 2 Test Property")]
        public string TestProperty2 { get; set; }

    }
}
