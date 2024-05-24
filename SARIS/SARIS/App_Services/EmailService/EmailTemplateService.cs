using NReco.PdfGenerator;
using OrionCoreCableColor.App_Helper;
using OrionCoreCableColor.App_Services.ContratoService;
using OrionCoreCableColor.App_Services.ReportesService;

using OrionCoreCableColor.Models.EmailTemplateService;

using Spire.Doc;
using Spire.Doc.Documents;
using Spire.Doc.Fields;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.Entity.Infrastructure;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net.Mail;
using System.Net.Mime;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Web;

namespace OrionCoreCableColor.App_Services.EmailService
{
    public class EmailTemplateService
    {
       
        private SendEmailService _emailService;

        public EmailTemplateService()
        {
            var ConnectionString = ConfigurationManager.ConnectionStrings["OrionConnections"].ConnectionString; // DataCrypt.Desencriptar(ConfigurationManager.ConnectionStrings["ConexionEncriptada"].ConnectionString);
           

            _emailService = new SendEmailService();

        }

        public string pathToOpen { get; set; }
        public string pathToSave { get; set; }

        public Dictionary<string, string> DictionaryList = new Dictionary<string, string>();

        private Attachment GeneratePDFAttachment()
        {
            Document doc = new Document();
            doc.LoadFromFile(pathToOpen);

            foreach (var item in DictionaryList)
            {
                doc.Replace("{" + item.Key + "}", item.Value, true, true);
            }

            doc.SaveToFile(pathToSave, FileFormat.PDF);

            var attachmentFile = new Attachment(pathToSave, MediaTypeNames.Application.Octet);
            doc.Close();
            return attachmentFile;
        }

        private Attachment GeneratePDFAttachmentFrimadoRevisado()
        {
            Document doc = new Document();
            doc.LoadFromFile(pathToOpen);
            string data = "";
            foreach (var item in DictionaryList)
            {
                if (item.Key == "fcFirma")
                {
                    data = item.Value;
                }
                else
                {
                    doc.Replace("{" + item.Key + "}", item.Value ?? "", true, true);
                }
            }

            var base64Data = Regex.Match(data, @"data:image/(?<type>.+?),(?<data>.+)").Groups["data"].Value;
            var binData = Convert.FromBase64String(base64Data);

            using (var stream = new MemoryStream(binData))
            {
                var Images = new Bitmap(stream);

                Image image = Image.FromHbitmap(Images.GetHbitmap());

                foreach (Section section in doc.Sections)
                {
                    foreach (Paragraph paragraph in section.Paragraphs)
                    {
                        foreach (DocumentObject docObj in paragraph.ChildObjects)
                        {
                            if (docObj.DocumentObjectType == DocumentObjectType.Picture)
                            {
                                DocPicture picture = docObj as DocPicture;
                                if (picture.Title == "Firma")
                                {
                                    //Replace the image
                                    picture.LoadImage(image);
                                }
                            }
                        }
                    }
                    //Loop through the child elements of paragraph

                }


            }

            doc.SaveToFile(pathToSave, FileFormat.PDF);

            var attachmentFile = new Attachment(pathToSave, MediaTypeNames.Application.Octet);
            doc.Close();
            return attachmentFile;
        }

        private Attachment GeneratePDFAttachmentFrimado()
        {
            Document doc = new Document();
            doc.LoadFromFile(pathToOpen);
            string data = "";
            foreach (var item in DictionaryList)
            {
                if (item.Key == "fcFirma")
                {
                    data = item.Value;
                }
                else
                {
                    doc.Replace("{" + item.Key + "}", item.Value ?? "", true, true);
                }
            }

            var base64Data = Regex.Match(data, @"data:image/(?<type>.+?),(?<data>.+)").Groups["data"].Value;
            var binData = Convert.FromBase64String(base64Data);

            using (var stream = new MemoryStream(binData))
            {
                var Images = new Bitmap(stream);

                Image image = Image.FromHbitmap(Images.GetHbitmap());

                foreach (Section section in doc.Sections)
                {
                    foreach (Paragraph paragraph in section.Paragraphs)
                    {
                        foreach (DocumentObject docObj in paragraph.ChildObjects)
                        {
                            if (docObj.DocumentObjectType == DocumentObjectType.Picture)
                            {
                                DocPicture picture = docObj as DocPicture;
                                if (picture.Title == "Firma")
                                {
                                    //Replace the image
                                    picture.LoadImage(image);
                                }
                            }
                        }
                    }
                    //Loop through the child elements of paragraph

                }


            }

            doc.SaveToFile(pathToSave, FileFormat.PDF);

            var attachmentFile = new Attachment(pathToSave, MediaTypeNames.Application.Octet);
            doc.Close();
            return attachmentFile;
        }

        

        public string ParseVariablesInEmailTemplate(string emailTemplate, Dictionary<string, string> variableValues)
        {

            var codeStartDelimiter = "{";
            var codeEndDelimiter = "}";

            var escapeCharacters = new[] { ".", "$", "{", "}", "[", "]", "^", "(", ")", "|", "*", "+", "?", @"\" };

            var delStartReg = $"{codeStartDelimiter}";
            var delEndReg = $"{codeEndDelimiter}";

            if (escapeCharacters.Contains(delStartReg)) delStartReg = $"\\{delStartReg}";
            if (escapeCharacters.Contains(delEndReg)) delEndReg = $"\\{delEndReg}";

            var regexp = new Regex(delStartReg + "[^" + delStartReg + delEndReg + "]*" + delEndReg);

            var matches = regexp.Matches(emailTemplate);

            foreach (Match match in matches)
            {
                var code = match.Value.Replace(codeStartDelimiter, "");
                code = code.Replace(codeEndDelimiter, "");

                var codeResult = variableValues[code];

                //try
                //{
                //    codeResult = _evaluateExpression.EvaluateCodeSnippet<string>(code).Result;
                //}
                //catch (Exception e)
                //{
                //    throw _exceptionFactory.GetException(25084, code);
                //}

                emailTemplate = emailTemplate.Replace(match.Value, codeResult);

            }

            return emailTemplate;

        }


        


        

        public async Task<bool> SendEmailToSolicitud(EmailTemplateTicketModel model)
        {
            try
            {

                var contenidoCorreo = "<table style=\"width: 500px; border-collapse: collapse; border-width: 0; border-style: none; border-spacing: 0; padding: 0;\">" +
                                       "<tr><th style='text-align:left;'>Numero de Ticket:</th> <td>" + model.fiIDRequerimiento + "</td></tr>" +
                                       "<tr><th style='text-align:left;'>Titulo Ticket:</th> <td>" + model.fcTituloRequerimiento + "</td></tr>" +
                                       "<tr><th style='text-align:left;'>Fecha de Creacion:</th> <td>" + model.fdFechaCreacion.ToString("MM/dd/yyyy hh:mm tt") + "</td></tr>" +
                                       "<tr><th style='text-align:left;'>Area Solicitante:</th> <td>" + model.fcAreaSolicitante + "</td></tr>" +
                                       "<tr><th style='text-align:left;'>Usuario Solicitante:</th> <td>" + model.fcNombreCorto + "</td></tr>" +
                                       "<tr><th style='text-align:left;'>Descripcion:</th> <td>" + model.fcDescripcionRequerimiento + "</td></tr>" +
                                       "<tr><th style='text-align:left;'>Estado: </th> <td>" + model.fcDescripcionEstado + "</td></tr>" +

                                  "</table>";

                string htmlString = @"<!DOCTYPE html> " +
                                    "<html>" +
                                    "<body>" +
                                    " <div style=\"width: 500px;\">" +
                                    " <table style=\"width: 500px; border-collapse: collapse; border-width: 0; border-style: none; border-spacing: 0; padding: 0;\">" +
                                    " <tr style=\"height: 30px; background-color:#56396b; font-family: 'Microsoft Tai Le'; font-size: 14px; font-weight: bold; color: white;\">" +
                                    " <td style=\"vertical-align: central; text-align:center;\">Ticket</td>" +
                                    " </tr>" +
                                    " <tr style=\"height: 24px; font-family: 'Microsoft Tai Le'; font-size: 12px; font-weight: bold;\">" +
                                    " <td>&nbsp;</td>" +
                                    " </tr>" +
                                    " <tr style=\"height: 24px; font-family: 'Microsoft Tai Le'; font-size: 12px; font-weight: bold;\">" +
                                    " <td style=\"background-color:whitesmoke; text-align:center;\">Datos</td>" +
                                    " </tr>" +
                                    " <tr style=\"height: 24px; font-family: 'Microsoft Tai Le'; font-size: 12px; font-weight: bold;\">" +
                                    " <td>&nbsp;</td>" +
                                    " </tr>" +
                                    " <tr style=\"height: 24px; font-family: 'Microsoft Tai Le'; font-size: 12px; font-weight: bold;\">" +
                                    " <td style=\"vertical-align: central;\">" + contenidoCorreo + "</td>" +
                                    " </tr>" +
                                    " <tr style=\"height: 24px; font-family: 'Microsoft Tai Le'; font-size: 12px; font-weight: bold;\">" +
                                    " <td>&nbsp;</td>" +
                                    " </tr>" +
                                    " <tr style=\"height: 20px; font-family: 'Microsoft Tai Le'; font-size: 12px; text-align:center;\">" +
                                    " <td>System Bot Prestadito</td>" +
                                    " </tr>" +
                                    " </table>" +
                                    " </div>" +
                                    "</body> " +
                                    "</html> ";

                var emailGeneratedToSend = new SendEmailViewModel
                {
                    EmailName = "Saris",
                    Subject = "Ticket",
                    Body = htmlString,
                    DestinationEmail = model.fcCorreoElectronico,
                };

                var SendEmailResult = await _emailService.SendEmailAsync(emailGeneratedToSend);



                return SendEmailResult;
            }
            catch (Exception e)
            {
                await _emailService.SendEmailException(e, "Send Email");

                return false;
            }


        }

        public async Task<bool> SendEmailPresonalizado(EmailTemplateServiceModel model)
        {
            try
            {

                var emailGeneratedToSend = new SendEmailViewModel
                {
                    EmailName = "NovaNet",
                    Subject = model.Comment,
                    Body = model.HtmlBody,
                    DestinationEmail = model.CustomerEmail,
                };

                var SendEmailResult = await _emailService.SendEmailAsync(emailGeneratedToSend);
                return SendEmailResult;
            }
            catch (Exception e)
            {
                await _emailService.SendEmailException(e, "Send Email");

                return false;
            }


        }
        


        public SendEmailViewModel GenerarCorreoGerencia(EmailTemplateServiceModel model)
        {

                Attachment fileAttachment = null;

                var CustomerAttachmentName = $"Reporte Gerencia Novanet del  {DateTime.Now:dd-mm-yyyy}.pdf";
                var fileStreamInventario = new StreamReader(model.Archivo.InputStream,  System.Text.Encoding.Default, false);
                fileAttachment = new Attachment(fileStreamInventario.BaseStream, CustomerAttachmentName);

                var modelCorreo = new SendEmailViewModel
                {
                    EmailName = "Reporte Gerencial",
                    Subject = "Reporte Dia General Novanet",
                    Body = $"Reporte de Gerencia ",
                    DestinationEmail = model.CustomerEmail,

                    List_CC = new List<string>(),
                    firma = "",
                    Attachment = fileAttachment
                };
                
                return modelCorreo;
           


        }


        public async Task<bool> SendEmailToGerencia(EmailTemplateServiceModel model)
        {
            try
            {
                var emailGeneratedToSend = GenerarCorreoGerencia(model);
                var SendEmailResult = await _emailService.SendEmailAsync(emailGeneratedToSend);
                return SendEmailResult;
            }
            catch (Exception e)
            {
                await _emailService.SendEmailException(e, "Send Email");

                return false;
            }


        }



    }
}
