using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace PDF_Bulk_Downloader
{
    class Program
    {


      


        static void Main(string[] args)
        {


            int counter = 1;
            int number_of_urls = File.ReadLines(System.Environment.CurrentDirectory + "/urls.txt").Count();


            string line;
            System.IO.StreamReader file =
            new System.IO.StreamReader(System.Environment.CurrentDirectory + "/urls.txt");
            while ((line = file.ReadLine()) != null)
            {

                //Thread.Sleep(1000);
                Console.WriteLine("Downloading...");
                if (download (line, counter + ".pdf"))
                {


                    Console.WriteLine(counter + " has been download  LEFT = " + counter+"/"+number_of_urls);

                }
                else
                {
                    Console.WriteLine(counter + " failed");
                }

                

                counter++;
            }



            Console.WriteLine("Done!");
            Console.ReadLine();

        }

        static bool download(string remoteFilename, string localFilename)
        {


            



                long length = 0;

            if (File.Exists(System.Environment.CurrentDirectory + "/PDFs/" + localFilename))
                length = new System.IO.FileInfo(System.Environment.CurrentDirectory + "/PDFs/" + localFilename).Length;

            if (length <= 0)
            {

                while (length <= 0)
                {

                    using (WebClient client = new WebClient())
                    {
                        // grab documentid
                        //localFilename = remoteFilename.Substring(remoteFilename.Length - 11, 5) + ".pdf";
                        client.DownloadFile(new Uri(remoteFilename, UriKind.Absolute), System.Environment.CurrentDirectory + "/PDFs/" + localFilename);
                    }

                    length = new System.IO.FileInfo(System.Environment.CurrentDirectory + "/PDFs/" + localFilename).Length;
                    if (length <= 0)
                        Console.Write(".");




                }
            }
            else
            {
                Console.WriteLine("Redownloading file...");
            }

            Console.WriteLine();

                //client.DownloadFile(new Uri(remoteFilename, UriKind.Absolute), System.Environment.CurrentDirectory + "/PDFs/" + localFilename);
            



            return true;
        }
    }
}
