

using System.Net;
using System.Security.Cryptography.X509Certificates;

namespace RockUtility
{
    public class CustomCertificatePolicy : ICertificatePolicy
    {
        public bool CheckValidationResult(ServicePoint sp, X509Certificate cert, WebRequest req, int problem)
        {
            //* Return "true" to force the certificate to be accepted.
            return true;
        }
    }
}
