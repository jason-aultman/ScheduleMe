using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;


namespace ScheduleMe
{
    public static class Serialize_Me
    {
        public static void Make(Clipboard[] a, string filename)
        {
            BinaryFormatter bf = new BinaryFormatter();
            FileStream fileStream = new FileStream(filename, FileMode.Create, FileAccess.Write, FileShare.None);
            try
            {
                using (fileStream)
                {
                    bf.Serialize(fileStream, a);
                    Console.WriteLine("objects serialized");
                }
            }
            catch
            {
                Console.WriteLine("Nothing was done, sorry");
            }
        }

        public static Clipboard[] GetClipboards(string filename)
        {
            Clipboard[] cb;
            BinaryFormatter bf = new BinaryFormatter();
            FileStream fileStream = new FileStream(filename, FileMode.Open, FileAccess.Read, FileShare.None);
            try
            {
                using (fileStream)
                {
                    cb = (Clipboard[])bf.Deserialize(fileStream);
                    return cb;
                }
            }
            catch
            {
                Console.WriteLine("An error has occured");


            }
            return cb = new Clipboard[0];
        }
    }
}
