using DinkToPdf;
using DinkToPdf.Contracts;
using Microsoft.AspNetCore.Html;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookSale.Management.Infrastructure.Services
{
    public class PDFService : IPDFService
    {
        private readonly IConverter _conventer;

        public PDFService(IConverter conventer)
        {
            _conventer = conventer;
        }

        public byte[] GeneratePDF(string contentHTML, 
                        Orientation orientation = Orientation.Portrait,
                        PaperKind paperKind = PaperKind.A4)
        {
            var globalSetting = new GlobalSettings
            {
                ColorMode = ColorMode.Color,
                Orientation = orientation,
                PaperSize = paperKind,
                Margins = new MarginSettings() { Top = 10, Bottom = 10 },
                Out = string.Empty,
            };

            var objectSetting = new ObjectSettings()
            {
                PagesCount = true,
                HtmlContent = contentHTML,
                WebSettings = { DefaultEncoding = "utf-8" },
            };

            var pdf = new HtmlToPdfDocument()
            {
                GlobalSettings = globalSetting,
                Objects = { objectSetting },
            };


            return _conventer.Convert(pdf);


        }

    }
}
