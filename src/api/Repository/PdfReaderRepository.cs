using System;
using api.Interfaces.Repository;
using System.Text;
using UglyToad.PdfPig;
using UglyToad.PdfPig.Content;


public class PdfReaderRepository : IPdfReaderRepository
{
    public string ReadText(Stream stream)
    {
        var sb = new StringBuilder();
        using (var pdf = PdfDocument.Open(stream))
        {
            foreach (Page page in pdf.GetPages())
            {
                sb.AppendLine(page.Text);
            }
        }
        return sb.ToString();
    }
}