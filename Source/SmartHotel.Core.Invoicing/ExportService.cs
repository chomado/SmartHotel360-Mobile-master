using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Globalization;
using System.Linq;

namespace SmartHotel.Core.Invoicing
{
    public class ExportService
    {
        public void ExportInvoice(Invoice invoice, string path)
        {
            var fileName = DateTime.Now.ToString("yyyyMMdd_HHmmss", CultureInfo.InvariantCulture);

            // Path.Combine は、Winなら\区切り、Linuxなら/区切りで結合してくれる
            var finalPath = Path.Combine(path, "invoices");

            if (!Directory.Exists(finalPath))
            {
                Directory.CreateDirectory(finalPath);
            }

            finalPath = Path.Combine(finalPath, fileName); 

            var info =
                "################################################\n\n" +
                $"{invoice.HotelName}" +
                $"Invoice: {invoice.InvoiceNumber} \n\n" +
                "################################################\n\n" +
                $"{invoice.Name}\n" +
                $"{string.Concat(invoice.Items.Select(i => i + "\n").ToArray())}\n" +
                $"Total: {invoice.Total.ToString("C")}\n\n" +
                "################################################\n\n";

            // 指定したpathに infoの内容を出力
            File.WriteAllText(finalPath, info);
        }
    }
}
