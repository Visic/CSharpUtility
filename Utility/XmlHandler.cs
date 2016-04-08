using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Xml.Linq;

namespace Utility {
    public class XmlHandler {
        XDocument _doc;

        public XmlHandler(Stream xmlContent) {
            _doc = XDocument.Load(xmlContent);
        }

        public XmlHandler(string xmlContent) {
            _doc = XDocument.Parse(xmlContent);
        }

        public List<Dictionary<string, string>> GetFromUri(string root, params string[] uri) {
            var result = new List<Dictionary<string, string>>();
            VisitAtUris(
                root, 
                uri.Select(
                    x => {
                        var id = Guid.NewGuid().ToString();
                        return Tuple.Create<string, Visitor>(
                            x,
                            (fc, a, b) => {
                                if (fc) result.Add(new Dictionary<string, string>());
                                result.Last()[a] = b.Match<string>(z => (string)z, z => z?.Value);
                            }
                        );
                    }
                ).ToArray()
            );
            return result;
        }

        public delegate void Visitor(bool firstVisitToNode, string uri, Union<XElement, XAttribute> value);
        public void VisitAtUris(string root, params Tuple<string, Visitor>[] work) {
            foreach(var ele in string.IsNullOrEmpty(root) ? _doc.Descendants() : _doc.Descendants(root)) {
                var firstCall = true;
                foreach(var thingToDo in work) {
                    Parse_Rec(ele, thingToDo.Item1, thingToDo.Item1.Contains("?=")).Apply(
                        x => {
                            thingToDo.Item2(firstCall, thingToDo.Item1, x);
                            firstCall = false;
                        }
                    );
                }
            }
        }

        private Option<Union<XElement, XAttribute>> Parse_Rec(XElement ele, string uri, bool isQuery) {
            if(ele == null) return new None();
            string[] parts = uri.Split(new string[] { "/", "?=" }, 2, StringSplitOptions.RemoveEmptyEntries);

            if(parts.Length == 2) return Parse_Rec(ele.Element(parts[0]), parts[1], isQuery);

            if (isQuery) return new Union<XElement, XAttribute>(ele.Attribute(parts[0]));
            else return new Union<XElement, XAttribute>(ele.Element(parts[0]));
        }
    }
}