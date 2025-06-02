namespace DocumentService.Domain.Helpers;

public static class FileValidator
{
    public static bool IsPdf(string base64String)
    {
        try
        {
            var bytes = Convert.FromBase64String(base64String);

            // Check if first 4 bytes match %PDF
            return bytes.Length >= 4 &&
                   bytes[0] == 0x25 && // %
                   bytes[1] == 0x50 && // P
                   bytes[2] == 0x44 && // D
                   bytes[3] == 0x46;   // F
        }
        catch
        {
            return false;
        }
    }
}
