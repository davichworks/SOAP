using System.Xml;

public class XmlConverter
{
    public static string ProcesarXmlConvertRequest(string xmlData, out decimal resultado)
    {
        XmlDocument doc = new XmlDocument();
        doc.LoadXml(xmlData);
        var nodeUnits = doc.SelectSingleNode("//units");
        var fromUnits = doc.SelectSingleNode("//from");
        var toUnits = doc.SelectSingleNode("//to");
        resultado = Convert.ToDecimal(nodeUnits.InnerText);
        Console.WriteLine(String.Concat(fromUnits.InnerText.ToUpper().Trim(), "-",
         toUnits.InnerText.ToUpper().Trim()));
        return String.Concat(fromUnits.InnerText.ToUpper().Trim(), "-",
         toUnits.InnerText.ToUpper().Trim());
    }
    public static string GenerarPaqueteXmlConvertResponse(string finalCurrency, string units)
    {
        XmlDocument doc = new XmlDocument();
        XmlNode docNode = doc.CreateXmlDeclaration("1.0", "UTF-8", null);
        doc.AppendChild(docNode);
        XmlNode responseNode = doc.CreateElement("ConvertResponse");
        doc.AppendChild(responseNode);
        XmlNode unitsNode = doc.CreateElement("units");
        unitsNode.InnerText = units;
        responseNode.AppendChild(unitsNode);
        XmlNode toNode = doc.CreateElement("to");
        toNode.InnerText = finalCurrency;
        responseNode.AppendChild(toNode);
        XmlNode result = doc.CreateElement("result");
        result.InnerText = convertirDivisa(finalCurrency,units); ;
        responseNode.AppendChild(result);   
        Console.WriteLine(doc.InnerXml);
        return doc.InnerXml;
        
    }

    public static string convertirDivisa(string finalcurrency, string units)
    {
        string resultado;
        double resultado1;
        if (finalcurrency == "EUR")
        {
             resultado1 = double.Parse(units)*0.94;
            return resultado1.ToString();
        }else if(finalcurrency == "USDT")
        {
            resultado1 = double.Parse(units) / 0.94;
            return resultado1.ToString() ;
        }
        else
        {
            return units;
        }
        
    }

    public static string GenerarPaqueteXmlConvertResponseError(string message)
    {
        XmlDocument doc = new XmlDocument();
        XmlNode docNode = doc.CreateXmlDeclaration("1.0", "UTF-8", null);
        doc.AppendChild(docNode);
        XmlNode responseNode = doc.CreateElement("ConvertResponse");
        responseNode.InnerText = message;
        doc.AppendChild(responseNode);
        return doc.InnerXml;
    }
}
