using System;
using System.IO;
using System.Net;
using System.Text;

namespace lID3
{
    public class Id3V2Parser
    {
        #region Constants

        private readonly Id3Info _id3Info;

        #endregion

        #region Properties
        /// <summary>
        /// Url to file
        /// </summary>
        public string Url { get; set; }

        #endregion

        #region Constructors
        /// <summary>
        /// Default contrutor
        /// </summary>
        public Id3V2Parser()
        {
            _id3Info=new Id3Info
            {
                Album = "unknown",
                Comments = "unknown",
                Composer = "unknown",
                EncodedBy="unknown",
                Performer = "unknown",
                SongName = "unknown",
                TypeContent = "unknown",
                Version = "0.0",
                Year = "unknown",
                Image = null
            };
        }

        /// <summary>
        /// Special constructor
        /// </summary>
        /// <param name="url">url to file</param>
        public Id3V2Parser(string url)
        {
            Url = url;
            _id3Info=new Id3Info
            {
                Album = "unknown",
                Comments = "unknown",
                Composer = "unknown",
                EncodedBy="unknown",
                Performer = "unknown",
                SongName = "unknown",
                TypeContent = "unknown",
                Version = "0.0",
                Year = "unknown",
                Image = null
            };
        }

        #endregion

        #region Methods
        /// <summary>
        /// Main method
        /// </summary>
        /// <returns>Object of class ID3Info</returns>
        public Id3Info GetInfo()
        {
            try
            {
                var buffer = GetDecodeBytes(Url);
                if (StringAt(0,3,buffer).Equals("ID3"))
                {
                    _id3Info.Version = "ID3v2." + buffer[3];
                    var tagSize=UnSynchsafeInt(Slice(buffer,6,4));
                    buffer = GetDecodeBytes(Url, tagSize);
                    string id="";
                    var cursor = 10;
                    do
                    {
                        id = StringAt(cursor, 4, buffer);
                        tagSize = UnSynchsafeInt(Slice(buffer, cursor + 4, 4));
                        cursor += 10;
                        switch (id)
                        {
                            case "TIT2":
                                _id3Info.SongName = StringAt(cursor + 1, tagSize - 1, buffer);
                                break;
                            case "TYER":
                                _id3Info.Year = StringAt(cursor + 1, tagSize- 1, buffer);
                                break;
                            case "TCOM":
                                _id3Info.Composer = StringAt(cursor + 1, tagSize - 1, buffer);
                                break;
                            case "TALB":
                                _id3Info.Album = StringAt(cursor + 1, tagSize - 1, buffer);
                                break;
                            case "TPE1":
                                _id3Info.Performer = StringAt(cursor + 1, tagSize - 1, buffer);
                                break;
                            case "COMM":
                                _id3Info.Comments = StringAt(cursor + 1, tagSize - 1, buffer);
                                break;
                            case "TCON":
                                _id3Info.TypeContent = StringAt(cursor + 1, tagSize - 1, buffer);
                                break;
                            case "TENC":
                                _id3Info.EncodedBy = StringAt(cursor + 1, tagSize - 1, buffer);
                                break;
                            case "APIC":
                                _id3Info.Image = Slice(buffer, cursor + 1, tagSize - 1);
                                break;
                        }
                        cursor += tagSize;
                    } while (id.Length==4);
                }
            }
            catch
            {
                throw new Exception();
            }
            return _id3Info;
        }

        /// <summary>
        /// Slice the byte array
        /// </summary>
        /// <param name="buffer">Array to slice</param>
        /// <param name="start">First byte</param>
        /// <param name="len">Length of slice-array</param>
        /// <returns></returns>
        byte[] Slice(byte[] buffer, int start, int len)
        {
            try
            {
                var arr = new byte[len];
                for (var i = 0; i < len; i++)
                {
                    arr[i] = buffer[i + start];
                }
                return arr;
            }
            catch
            {
                return new byte[0];
            }
        }

        /// <summary>
        /// Download from web slice of file
        /// </summary>
        /// <param name="linkToMp3">Link to file</param>
        /// <param name="bytesToGet">Count bytes for get from file</param>
        /// <param name="index">Start read from</param>
        /// <returns>Byte array</returns>
        byte[] GetDecodeBytes(string linkToMp3, int bytesToGet = 10,int index=0)
        {
            try
            {
                var request = WebRequest.Create(linkToMp3) as HttpWebRequest;
                var buffer = new char[bytesToGet];
                if (request != null)
                    using (var response = request.GetResponse())
                    {
                        using (var sr = new StreamReader(response.GetResponseStream()))
                        {
                            sr.Read(buffer, index, bytesToGet);
                        }
                    }
                return Encoding.ASCII.GetBytes(buffer);
            }
            catch
            {
                return new byte[0];
            }
        }

        /// <summary>
        /// More information in http://en.wikipedia.org/wiki/Synchsafe
        /// </summary>
        /// <param name="buffer"></param>
        /// <returns>Size of frame</returns>
        int UnSynchsafeInt(byte[] buffer)
        {
            try
            {
                int value = 0;
                for (int i = 0, length = buffer.Length; i < length; i++)
                {
                    value += Convert.ToInt32((buffer[i] & 0x7F)*Math.Pow(Math.Pow(2, 7), length - i - 1));
                }
                return value;
            }
            catch
            {
                return 0;
            }
        }

        /// <summary>
        /// Extract string from byte array
        /// </summary>
        /// <param name="offset"></param>
        /// <param name="length"></param>
        /// <param name="buffer"></param>
        /// <returns></returns>
        string StringAt(int offset, int length, byte[] buffer)
        {
            try
            {
                var str = "";
                for (var i = 0; i < length; i++)
                {
                    str += Convert.ToChar(buffer[i + offset]);
                }
                return str;
            }
            catch
            {
                return "";
            }
        }
        #endregion
    }
}