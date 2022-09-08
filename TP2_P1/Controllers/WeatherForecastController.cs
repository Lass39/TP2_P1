using Microsoft.AspNetCore.Mvc;
using System.Text;
using System.Xml;
using System.Text;
using System.Xml.Linq;

namespace TP2_P1.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {
        private static readonly string[] Summaries = new[]
        {
        "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
    };

        private readonly ILogger<WeatherForecastController> _logger;

        public WeatherForecastController(ILogger<WeatherForecastController> logger)
        {
            _logger = logger;
        }

        [HttpGet("XML")]
        public void XmlTp()
        {
            //Caminho onde o arquivo será salvo, no meu caso será:
            //C:\inetpub\wwwroot\exemplos\exemplo3.xml
            string strFilePath = "c://Taa//teste.xml";
            //Esta linha indica que o o arquivo xml sera salvo, diferente dos outros exemplos
            XmlTextWriter xtw = new XmlTextWriter(strFilePath, Encoding.UTF8);
            //A linha abaixo vai identar o código, se não usar isso tudo ficará em uma linha.
            xtw.Formatting = Formatting.Indented;
            //Escreve a declaração do documento <?xml version="1.0" encoding="utf-8"?>
            xtw.WriteStartDocument();

            xtw.WriteStartElement("blog");
            xtw.WriteStartElement("artigos");
            xtw.WriteAttributeString("linguagem", "asp.net");
            xtw.WriteStartElement("artigo");
            xtw.WriteElementString("titulo", "DataSet para XML em ASP.NET / C#");
            xtw.WriteElementString("url", "http://cbsa.com.br/post/dataset-para-xmlem-aspnet-c.aspx");
            xtw.WriteEndElement();
            xtw.WriteStartElement("artigo");
            xtw.WriteElementString("titulo", "XML para DataSet em ASP.NET / C#");
            xtw.WriteElementString("url", "http://cbsa.com.br/post/xml-para-datasetem-aspnet-c.aspx");
            xtw.WriteEndElement();
            xtw.WriteStartElement("artigo");
            xtw.WriteElementString("titulo", "Ler arquivo XML usando XmlTextReader e XmlDocument em C# - ASP.NET");

            xtw.WriteElementString("url", "http://cbsa.com.br/post/ler-arquivo-xmlusando-xmltextreader-e-xmldocument-em-c---aspnet.aspx");
            xtw.WriteEndElement();
            xtw.WriteEndElement();
            xtw.WriteStartElement("artigos");
            xtw.WriteAttributeString("linguagem", "C#");
            xtw.WriteStartElement("artigo");
            xtw.WriteElementString("titulo", "Calcular idade em C#");
            xtw.WriteElementString("url", "http://cbsa.com.br/post/calcular-idadeem-c.aspx");
            xtw.WriteEndElement();
            xtw.WriteEndElement();
            xtw.WriteEndElement();
            xtw.WriteEndDocument();
            //libera o XmlTextWriter
            xtw.Flush();
            //fechar o XmlTextWriter
            xtw.Close();
            //Termina aqui

            return;
        }
        [HttpGet("XMLRead")]
        public string XmlTpRead()
        {
            XmlDocument documento = new XmlDocument();
            documento.Load("c://Taa//teste.xml");
            XmlNodeReader nos = new XmlNodeReader(documento);
            int endentar = -1;

            string te = "";

            while (nos.Read())
            {
                switch (nos.NodeType)
                {
                    case XmlNodeType.Element:
                        endentar++;
                        Tabular(endentar, ref te);
                        if (nos.Name != "operando")
                            te += "<" + nos.Name + ">" + "\r\n";
                        else
                        {
                            nos.Read();
                            if (nos.Value == "T")
                                te += "Vôo operando\r\n";
                            else
                                te += "Vôo NÃO operando\r\n";

                            endentar--;
                            // nos.Read();
                            nos.Read();
                        }
                        if (nos.IsEmptyElement)
                            endentar--;
                        break;
                    case XmlNodeType.Comment:
                        Tabular(endentar, ref te);
                        te += "Comentário: " + nos.Value + "\r\n";
                        break;
                    case XmlNodeType.Text:
                        endentar++;
                        Tabular(endentar, ref te);
                        te += (nos.Value + "\r\n");
                        endentar--;
                        break;
                    case XmlNodeType.XmlDeclaration:
                        te += "<?" + nos.Name + " " + nos.Value + " ?>" + "\r\n";
                        break;

                    case XmlNodeType.EndElement:
                        Tabular(endentar,ref te);
                        te += "</" + nos.Name + ">" + "\r\n";
                        endentar--;
                        break;

                      
                }
                
            }
            return te;
        }
        private void Tabular(int valor,ref string te)
        {
            for (int i = 0; i < valor; i++)
                te += '\t';
        }
    }
}
           