namespace Anonimize.Services
{
    public interface ICryptoService
    {
        /// <summary>
        /// Decrypts the specified input buffer.
        /// </summary>
        /// <param name="inputBuffer">The input buffer.</param>
        /// <returns></returns>
        byte[] Decrypt(byte[] inputBuffer);

        /// <summary>
        /// Decrypts the specified input.
        /// </summary>
        /// <param name="input">The input.</param>
        /// <returns>The decrypted input</returns>
        string Decrypt(string input);

        /// <summary>
        /// Encrypts the specified input buffer.
        /// </summary>
        /// <param name="inputBuffer">The input buffer.</param>
        /// <returns></returns>
        byte[] Encrypt(byte[] inputBuffer);

        /// <summary>
        /// Encrypts the specified input.
        /// </summary>
        /// <param name="input">The input.</param>
        /// <returns>The encrypted input</returns>
        string Encrypt(string input);
    }
}