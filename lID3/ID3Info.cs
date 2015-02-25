using System;

namespace lID3
{
    public class Id3Info
    {
        /// <summary>
        /// Set a version of id3 tags
        /// </summary>
        public string Version { get; set; }
        
        /// <summary>
        /// TIT2 Name of composition
        /// </summary>
        public string SongName { get; set; }

        /// <summary>
        /// APIC Icon of album
        /// </summary>
        public byte[] Image { get; set; }

        /// <summary>
        /// TYER Year of sound
        /// </summary>
        public string Year { get; set; }

        /// <summary>
        /// TCOM Composer of sound
        /// </summary>
        public string Composer { get; set; }

        /// <summary>
        /// TALB Album
        /// </summary>
        public string Album { get; set; }

        /// <summary>
        /// TPE1 Lead performer(s)/Soloist(s) 
        /// </summary>
        public string Performer { get; set; }

        /// <summary>
        /// COMM Comments
        /// </summary>
        public string Comments { get; set; }

        /// <summary>
        /// TCON Content type
        /// </summary>
        public string TypeContent { get; set; }

        /// <summary>
        /// TENC Encoded by
        /// </summary>
        public string EncodedBy { get; set; }
    }
}
