// Creado por Luis Guijarro Pérez.
using System;
using System.Net;
using System.ComponentModel;

namespace VulkanParser;

/// <summary>
/// Class with program entry point.
/// </summary>
internal sealed class Program
{
    static bool verbose;
    static bool showErrors;
    static bool download;
    static bool downloaded;
    static bool gitRefPages;
    static bool withgles;
    static bool ayuda;
    static string output = "./output/";
    static string s_namespace = "Vulkan";
    static int cursortop;
    static bool textoprocesado;

    /// <summary>
    /// Program entry point.
    /// </summary>
    [STAThread]
    private static void Main(string[] args)
    {
        Console.Clear();
        // output = "./output/";
        // s_namespace = "Vulkan";
        for (int i = 0; i < args.Length; i++)
        {
            string arg = args[i];
            switch (arg)
            {
                case "-v":
                case "-V":
                    verbose = true;
                    break;
                case "-e":
                case "-E":
                    showErrors = true;
                    break;
                case "-d":
                case "-D":
                    download = true;
                    break;
                case "-g":
                case "-G":
                    gitRefPages = true;
                    break;
                case "-es":
                case "-Es":
                case "-eS":
                case "-ES":
                    withgles = true;
                    break;
                case "-o":
                case "-O":
                    output = args[i + 1];
                    i++;
                    break;
                case "-n":
                case "-N":
                    s_namespace = args[i + 1];
                    i++;
                    break;
                case "-h":
                case "-H":
                case "--help":
                    ayuda = true;
                    break;
            }
            if (ayuda)
            {
                ShowHelp(); //Muestra la ayuda
                return; //Finaliza la aplicación
            }
        }
        Console.Clear();
        Console.ForegroundColor = ConsoleColor.Red;
        Console.WriteLine("Vulkan Parser");
        Console.ForegroundColor = ConsoleColor.Cyan;
        Console.WriteLine("==========================================================");
        Console.ResetColor();

        if (download)
        {
            if (System.IO.File.Exists("./downloads/vk.xml"))
            {
                Console.WriteLine("Ya tiene descargada una versión del archivo vk.xml");
                Console.Write("¿Desea realmente descargarlo nuevamente?(s/N):");

                if (Console.ReadKey().KeyChar == 's')
                {
                    Console.WriteLine();
                    Download_vkxml();
                }
                else
                {
                    downloaded = true;
                }
                Console.WriteLine();
            }
            else
            {
                Download_vkxml();
            }
            while (!downloaded)
            {
                //Esperar a que termine la descarga.
                System.Threading.Thread.Sleep(100); //Aligerar consumo de recursos.
            }
        }
        else
        {
            if (!System.IO.File.Exists("./downloads/vk.xml"))
            {
                Download_vkxml();
            }
            else
            {
                downloaded = true;
            }
            Console.WriteLine();
        }
        while (!downloaded)
        {
            //Esperar a que termine la descarga.
            System.Threading.Thread.Sleep(100); //Aligerar consumo de recursos.
        }

        // TODO: Parsear xml.
        Parser.Parse("./downloads/vk.xml", verbose, showErrors);
        CodeWriter.WriteCode(s_namespace, output, verbose);

        Console.ResetColor();
        Console.WriteLine();
        Console.WriteLine("vk.xml Parsing Finnished!");
        Console.ForegroundColor = ConsoleColor.Cyan;
        Console.WriteLine("==========================================================");
        Console.ResetColor();
    }

    private static void ShowHelp()
    {
        Console.ForegroundColor = ConsoleColor.Red;
        Console.WriteLine("Vulkan Parser 3 Help Output:");
        Console.ForegroundColor = ConsoleColor.Cyan;
        Console.WriteLine("==========================================================");
        Console.ResetColor();
        Console.WriteLine("vkp2 [OPTION] <option value>");
        Console.WriteLine("Options:");
        Console.WriteLine("  -v  -> Verbose Mode.");
        Console.WriteLine("  -d  -> Download new vk.xml file.");
        Console.WriteLine("         If vk.xml file dont exist -d is by default.");
        Console.WriteLine("  -o  -> Output Path of .cs Files. (./output/ by default)");
        Console.WriteLine("  -n  -> NameSpace of .cs Files. (Vulkan by default)");
        Console.WriteLine("  -e  -> Show Only Errors.");
        //Console.WriteLine("  -g  -> Complete Enums with Vulkan-RefPages. requires git.");
        //Console.WriteLine("  -es -> Parse with OpenGL|ES");
        Console.WriteLine("  -h  -> Show this Help. Ignore another options.");
    }

    #region Download VK.xml
    private static void Download_vkxml()
    {
        using (System.Net.WebClient wc = new System.Net.WebClient())
        {
            wc.DownloadProgressChanged += new DownloadProgressChangedEventHandler(Wc_DownloadProgressChanged);
            wc.DownloadFileCompleted += new AsyncCompletedEventHandler(Wc_DownloadFileCompleted);
            wc.BaseAddress = "";
            wc.Headers.Add("User-Agent", "Mozilla/4.0 (compatible; MSIE 8.0)");
            wc.Proxy = System.Net.WebRequest.GetSystemWebProxy();
            wc.Proxy.Credentials = System.Net.CredentialCache.DefaultCredentials;

            Uri uri = new Uri("https://raw.githubusercontent.com/KhronosGroup/Vulkan-Headers/main/registry/vk.xml");

            Console.WriteLine();
            Console.WriteLine("Descargando vk.xml desde repositorio oficial.");
            cursortop = Console.CursorTop;
            textoprocesado = true;
            if (!System.IO.Directory.Exists("./downloads"))
            {
                System.IO.Directory.CreateDirectory("./downloads");
            }
            try
            {
                wc.DownloadFileAsync(uri, "./downloads/vk.xml.temp");
            }
            catch (Exception e)
            {
                Console.WriteLine("EXCEPTION: " + e.Message);
            }
        }
    }


    static void Wc_DownloadProgressChanged(object sender, DownloadProgressChangedEventArgs e)
    {
        if (textoprocesado) //Solo se procesa la escritura de texto si la anterior se ha terminado.
        {
            textoprocesado = false;
            string s_line = "Downloading: ▕";
            Console.SetCursorPosition(0, cursortop);
            Console.Write(s_line);
            Console.ForegroundColor = ConsoleColor.Green;
            int con_width = Console.WindowWidth - (s_line.Length + 7);
            float i_variant = 100f / (float)(con_width);
            int value = e.ProgressPercentage;
            s_line = "";
            for (int i = 0; i < con_width; i++)
            {
                string progreschar = " ";
                if ((i_variant * i) <= value)
                {
                    progreschar = "▆";
                }
                else
                {
                    Console.ResetColor();
                    progreschar = "_";
                }
                Console.Write(progreschar);
            }
            Console.Write(s_line);
            Console.ResetColor();
            s_line = "▏ ";
            Console.Write(s_line);
            s_line = value.ToString("D3") + "%";
            //Console.SetCursorPosition(0, cursortop);
            Console.Write(s_line);
            textoprocesado = true;
        }
    }

    static void Wc_DownloadFileCompleted(object sender, AsyncCompletedEventArgs e)
    {
        if (e.Cancelled)
        {
            Console.WriteLine();
            Console.WriteLine("Descarga de vk.xml Cancelada");
        }
        if (e.Error != null)
        {
            Console.WriteLine();
            Console.WriteLine("Error en descarga de vk.xml");
            return;
        }
        if ((e.Error == null) && (!e.Cancelled))
        {
            System.IO.FileStream fs = System.IO.File.Open("./downloads/vk.xml.temp", System.IO.FileMode.Open);
            if (fs.Length > 0)
            {
                if (System.IO.File.Exists("./downloads/vk.xml"))
                {
                    System.IO.File.Replace("./downloads/vk.xml.temp", "./downloads/vk.xml", "./downloads/vk.xml.old");
                }
                else
                {
                    System.IO.File.Move("./downloads/vk.xml.temp", "./downloads/vk.xml");
                }

                if (System.IO.File.Exists("./downloads/vk.xml.temp"))
                {
                    System.IO.File.Delete("./downloads/vk.xml.temp");
                }
                Console.WriteLine();
                Console.WriteLine();
                Console.WriteLine("Descarga de vk.xml Realizada con Exito");
                fs.Close();
                downloaded = true;
            }
            else
            {
                System.IO.File.Delete("./downloads/vk.xml.temp");
                Console.WriteLine();
                Console.WriteLine(e.Error.Message, "Error en descarga de vk.xml");
                fs.Close();
            }
        }
    }
    #endregion
}
