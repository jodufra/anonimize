namespace Anonimize.Services
{
    public interface IDecryptService
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
        T Decrypt<T>(string input);
    }
}