using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Xml.Linq;

public class XmlReader : MonoBehaviour {
    public List<string> ReadXml(string fileName, string headerName, string elementName) {
        List<string> valueList = new List<string>();
        XDocument xml = XDocument.Load(Application.dataPath + @"/StreamingAssets/XML/" + fileName);

        foreach (XElement x in xml.Descendants(headerName)) {
                valueList.Add(x.Element(elementName).Value);
        }

        return valueList;
    }
}
