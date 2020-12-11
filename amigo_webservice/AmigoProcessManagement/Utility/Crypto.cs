using System;
using System.Text;
using System.IO;
using System.Security.Cryptography;

namespace AmigoPaperworkProcess.Core
{
   public class Crypto
    {
        /// <summary>
        /// Key to use for the Encrypting and the Decrypting
        /// </summary>
        private byte[] _strPrivateKey = { 8, 21, 11, 41, 4, 17, 34, 47, 51, 32, 5, 45, 67, 61, 3, 38 };

        /// <summary>
        /// Salt/Vector to be used
        /// </summary>
        private byte[] _strSalt = { 14, 21, 33, 47, 11, 67, 61, 5, 3, 87, 3, 24, 54, 71, 62, 78 };

        /// <summary>
        /// Property for PrivateKey Value
        /// </summary>
        private Byte[] PrivateKey
        {
            get { return _strPrivateKey; }
        }

        /// <summary>
        /// Property for the Salt Value
        /// </summary>
        private Byte[] Salt
        {
            get { return _strSalt; }
        }

        #region Constructor

        /// <summary>
        /// Constructor (Currently has no Code)
        /// </summary>          

        #endregion Constructor

        #region EncryptString

        /// <summary>
        /// Encrypt the passed string
        /// </summary>
        /// <PARAM name="plainTextString"></PARAM>
        /// String to Encrypt
        public string EncryptString(string plainTextString)
        {
            string strValue = String.Empty;

            try
            {
                System.Security.Cryptography.RijndaelManaged cryptObj = new RijndaelManaged();
                if (plainTextString != string.Empty)
                {
                    MemoryStream ms = new MemoryStream();
                    CryptoStream cs = new CryptoStream(ms, cryptObj.CreateEncryptor(PrivateKey, Salt), CryptoStreamMode.Write);
                    StreamWriter sw = new StreamWriter(cs);
                    sw.Write(plainTextString);
                    sw.Flush();
                    cs.FlushFinalBlock();
                    ms.Flush();
                    strValue = Convert.ToBase64String(ms.GetBuffer(), 0, (int)ms.Length);
                }
                else
                    strValue = string.Empty;
            }
            catch (Exception)
            {
                // Make sure that the application that is consuming this
                // .DLL gets the message returned so it can be displayed.
                //MessageBox.Show("Unable to Encrypt the passed string: \r\n\r\n " + ex.Message.ToString(), "Error");
            }

            // Return the Encrypted String
            return strValue;

        }

        #endregion EncryptString

        #region EncodeString

        /// <summary>
        /// Encode the passed string
        /// </summary>
        /// <PARAM name="encryptedTextString"></PARAM>
        /// Encrypted String to Encode
        public string EncodeString(string encryptedTextString)
        {
            // Local Declarations
            byte[] baValue = null;
            string strValue = String.Empty;

            try
            {
                // Encode the Passed String
                baValue = Encoding.Unicode.GetBytes(encryptedTextString);
                strValue = Convert.ToBase64String(baValue);
            }
            catch (Exception)
            {
                // Make sure that the application that is consuming this
                // .DLL gets the message returned so it can be displayed.
                //MessageBox.Show("Unable to Encode the passed string: \r\n\r\n " + ex.Message.ToString(), "Error");
            }

            // Return the Encoded Value
            return strValue;
        }

        #endregion EncodeString

        #region DecryptString

        /// <summary>
        /// Decrypt the passed string
        /// </summary>
        /// <PARAM name="encryptTextString"></PARAM>
        /// String to Decrypt
        public string DecryptString(string encodedTextString)
        {
            string strValue = String.Empty;

            try
            {
                System.Security.Cryptography.RijndaelManaged cryptObj = new RijndaelManaged();
                //cryptObj.Padding = PaddingMode.None;
                //cryptObj.Mode = CipherMode.CBC;
                if (encodedTextString != string.Empty)
                {
                    byte[] buf = Convert.FromBase64String(encodedTextString.Trim());
                    MemoryStream ms = new MemoryStream(buf);
                    ms.Position = 0;
                    CryptoStream cs = new CryptoStream(ms, cryptObj.CreateDecryptor(PrivateKey, Salt), CryptoStreamMode.Read);
                    StreamReader sr = new StreamReader(cs);
                    strValue = sr.ReadToEnd();
                }
                else
                    strValue = String.Empty;
            }
            catch (Exception)
            {
                // Make sure that the application that is consuming this
                // .DLL gets the message returned so it can be displayed.
                //MessageBox.Show("Unable to Decrypt the passed string: \r\n\r\n " + ex.Message.ToString(), "Error");
            }

            // Return the decrypted text
            return strValue;
        }

        #endregion DecryptString

        #region DecodeString

        /// <summary>
        /// Decode the passed string
        /// </summary>
        /// <PARAM name="encodedTextString"></PARAM>
        /// Encoded String to Decode
        public string DecodeString(string encodedTextString)
        {
            // Local Declarations
            byte[] baValue = null;
            string strValue = String.Empty;

            try
            {
                // Decode the Passed String
                baValue = Convert.FromBase64String(encodedTextString);
                strValue = Encoding.Unicode.GetString(baValue);
            }
            catch (Exception)
            {
                // Make sure that the application that is consuming this
                // .DLL gets the message returned so it can be displayed.

                //MessageBox.Show("Unable to Decode the passed string: \r\n\r\n " + ex.Message.ToString(), "Error");
            }

            // Return the Decoded String
            return strValue;
        }

        #endregion DecodeString
    }
}
