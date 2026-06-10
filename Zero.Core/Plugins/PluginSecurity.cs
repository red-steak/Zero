namespace Zero.Core.Plugins
{
    public static class PluginSecurity
    {
        public static bool VerifySignature(string dllPath)
        {
            // tady bys použil System.Security.Cryptography.X509Certificates
            // a ověřil Authenticode podpis
            return true; // placeholder
        }
    }

}
