namespace Anonimize.Services
{
    public interface IEncryptService
    {
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
        string Encrypt<T>(T input);
    }
}