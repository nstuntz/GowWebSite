using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.IO;
using System.Text;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace GowWebSite
{
    public class Helpers
    {
        public static string Encrypt(string password)
        {
            byte[] toEncrypt = Encoding.UTF8.GetBytes(password);
            PasswordDeriveBytes cdk = new PasswordDeriveBytes("April May Blue Red Key for th3 encypt1on", null);
            byte[] iv = new byte[] { 0, 0, 0, 0, 0, 0, 0, 0 };
            byte[] key = cdk.CryptDeriveKey("RC2", "MD5", 0, iv);
            RC2CryptoServiceProvider rc2 = new RC2CryptoServiceProvider();
            rc2.Key = key;
            rc2.IV = iv;    //IV MUST be specified with Zeroes, or it will be defaulted to a random value
            MemoryStream ms = new MemoryStream();
            CryptoStream cs = new CryptoStream(ms, rc2.CreateEncryptor(), CryptoStreamMode.Write);
            cs.Write(toEncrypt, 0, toEncrypt.Length);
            cs.Close();
            string str = BitConverter.ToString(ms.ToArray());
            str = str.Replace("-", "");  //formats output of BitConvertor to AutoIT binary formatted as string
            //System.Windows.Forms.MessageBox.Show("0x" + str);
            return str;
        }

        public static string Decrypt(string password)
        {
            //byte[] toDecrypt = Encoding.UTF8.GetBytes(password);
            byte[] encrypted = Enumerable.Range(0, password.Length)
                .Where(x => x % 2 == 0)
                .Select(x => Convert.ToByte(password.Substring(x, 2), 16))
                .ToArray();

            PasswordDeriveBytes cdk = new PasswordDeriveBytes("April May Blue Red Key for th3 encypt1on", null);
            byte[] iv = new byte[] { 0, 0, 0, 0, 0, 0, 0, 0 };
            byte[] key = cdk.CryptDeriveKey("RC2", "MD5", 0, iv);
            RC2CryptoServiceProvider rc2 = new RC2CryptoServiceProvider();
            rc2.Key = key;
            rc2.IV = iv;

            // Now decrypt the previously encrypted message using the decryptor 
            // obtained in the above step.
            MemoryStream msDecrypt = new MemoryStream(encrypted);
            CryptoStream cs = new CryptoStream(msDecrypt, rc2.CreateDecryptor(), CryptoStreamMode.Read);
            StreamReader srDecrypt = new StreamReader(cs);
            string roundtrip = srDecrypt.ReadToEnd();

            return roundtrip.ToString();
        }
    }



    public class PDTHolder
    {
        public double GrossTotal { get; set; }
        public int InvoiceNumber { get; set; }
        public string PaymentStatus { get; set; }
        public string PayerFirstName { get; set; }
        public double PaymentFee { get; set; }
        public string BusinessEmail { get; set; }
        public string PayerEmail { get; set; }
        public string TxToken { get; set; }
        public string PayerLastName { get; set; }
        public string ReceiverEmail { get; set; }
        public string ItemName { get; set; }
        public string Currency { get; set; }
        public string TransactionId { get; set; }
        public string SubscriberId { get; set; }
        public string Custom { get; set; }
        public string RawData { get; set; }
        public string PayerID { get; set; }

        public static PDTHolder Parse(string postData)
        {
            String sKey, sValue;
            PDTHolder ph = new PDTHolder();

            try
            {
                ph.RawData = postData;
                //split response into string array using whitespace delimeter
                String[] StringArray = postData.Split('\n');

                // NOTE:
                /*
                * loop is set to start at 1 rather than 0 because first
                string in array will be single word SUCCESS or FAIL
                Only used to verify post data
                */

                // use split to split array we already have using "=" as delimiter
                int i;
                for (i = 1; i < StringArray.Length - 1; i++)
                {
                    String[] StringArray1 = StringArray[i].Split('=');

                    sKey = StringArray1[0];
                    sValue = HttpUtility.UrlDecode(StringArray1[1]);

                    // set string vars to hold variable names using a switch
                    switch (sKey)
                    {
                        case "mc_gross":
                            ph.GrossTotal = Convert.ToDouble(sValue);
                            break;

                        case "invoice":
                            ph.InvoiceNumber = Convert.ToInt32(sValue);
                            break;

                        case "payment_status":
                            ph.PaymentStatus = Convert.ToString(sValue);
                            break;

                        case "first_name":
                            ph.PayerFirstName = Convert.ToString(sValue);
                            break;

                        case "mc_fee":
                            ph.PaymentFee = Convert.ToDouble(sValue);
                            break;

                        case "business":
                            ph.BusinessEmail = Convert.ToString(sValue);
                            break;

                        case "payer_email":
                            ph.PayerEmail = Convert.ToString(sValue);
                            break;

                        case "Tx Token":
                            ph.TxToken = Convert.ToString(sValue);
                            break;

                        case "last_name":
                            ph.PayerLastName = Convert.ToString(sValue);
                            break;

                        case "receiver_email":
                            ph.ReceiverEmail = Convert.ToString(sValue);
                            break;

                        case "item_name":
                            ph.ItemName = Convert.ToString(sValue);
                            break;

                        case "mc_currency":
                            ph.Currency = Convert.ToString(sValue);
                            break;

                        case "txn_id":
                            ph.TransactionId = Convert.ToString(sValue);
                            break;

                        case "custom":
                            ph.Custom = Convert.ToString(sValue);
                            break;

                        case "subscr_id":
                            ph.SubscriberId = Convert.ToString(sValue);
                            break;

                        case "payer_id":
                            ph.PayerID = Convert.ToString(sValue);
                            break;
                    }
                }

                return ph;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }

    public static class HtmlExtensions
    {
        public static MvcHtmlString Image(this HtmlHelper html, byte[] image)
        {
            if (image==null)
            {
                return new MvcHtmlString("");
            }

            string img = String.Format("data:image/jpg;base64,{0}", Convert.ToBase64String(image));
            return new MvcHtmlString("<img src='" + img + "' />");
        }
    }
}