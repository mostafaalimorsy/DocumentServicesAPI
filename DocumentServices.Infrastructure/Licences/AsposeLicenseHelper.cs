namespace DocumentService.Licensing
{
    public static class AsposeLicenseHelper
    {
        public static void ApplyLicenses()
        {
            var licensePath = Path.Combine(AppContext.BaseDirectory, "Licences/Intalio.Core.Aspose.Total.Product.Family 1.lic");

            var pdfLicense = new Aspose.Pdf.License();
            pdfLicense.SetLicense(licensePath);

            var wordsLicense = new Aspose.Words.License();
            wordsLicense.SetLicense(licensePath);

            var cellsLicense = new Aspose.Cells.License();
            cellsLicense.SetLicense(licensePath);
        }
    }
}
