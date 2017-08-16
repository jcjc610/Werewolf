using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Updater
{
    class Program
    {
        internal static string RootDirectory
        {
            get
            {
                string codeBase = Assembly.GetExecutingAssembly().CodeBase;
                UriBuilder uri = new UriBuilder(codeBase);
                string path = Uri.UnescapeDataString(uri.Path);
                return Path.GetDirectoryName(path);
            }
        }
        static void Main(string[] args)
        {
            try
            {
                var id = "";
                if (args.Length > 0)
                {
                    id = args[0];
                    Console.WriteLine($"Update called from {id}", id);
                }
                //get the root directory for the control
                var rootPath = Path.Combine(RootDirectory, @"..\..\");
                var updatePath = Path.Combine(rootPath, "Update");
                if (File.Exists(Path.Combine(updatePath, "Control\\Werewolf Control.exe"))) //DO WE NEED IT?
                {
                    Console.WriteLine("Start Control Update...");
                    Console.WriteLine("Waiting on bot to exit...");
                    //first, wait for the bot to close out
#if RELEASE2
                while (Process.GetProcessesByName("Werewolf Control 2").Any())
#else
                while (Process.GetProcessesByName("Werewolf Control").Any())
#endif
                    {
                        Thread.Sleep(100);
                    }
                    Console.WriteLine("Patching...");
                    Thread.Sleep(500);
                    //ok, it's off, patch it
                    foreach (var file in Directory.GetFiles(Path.Combine(updatePath, "Control")))
                    {
                        Console.WriteLine(file);
                        File.Copy(file, file.Replace("\\Update", ""), true);
                        File.Delete(file);
                    }
                    Console.WriteLine("Starting bot...");
                    //now start it back up
                    //if (!Process.GetProcessesByName("Werewolf Control").Any())
                    var path = Path.Combine(rootPath, "Control\\" +
#if RELEASE2
                    "werewolf control 2.exe"
#else
                    "werewolf control.exe"
#endif
                    );
                    Process.Start(path, id);
                    Console.WriteLine("Control Update complete");
                    Thread.Sleep(2000);
                }

                if (File.Exists(Path.Combine(updatePath, "Node\\Werewolf Node.exe")))
                { //DO WE NEED IT?
                    Console.WriteLine("Start Node Update...");
                    int i = 1; bool deleted = false;
                    while (File.Exists(Path.Combine(rootPath, "Node " + i.ToString(), "Werewolf Node.exe")))
                    {
                        //old .exe exists, now i have to delete it
                        try
                        {
                            File.Delete(Path.Combine(rootPath, "Node " + i.ToString(), "Werewolf Node.exe"));
                            deleted = true;
                        }
                        catch (UnauthorizedAccessException e)
                        {
                            //oh no, it is being used!
                            Console.WriteLine("Node " + i.ToString() + " inaccessible");
                            deleted = false;
                            i++; //try next one
                        }
                    }
                    if (deleted)
                    {
                        Console.WriteLine("Node " + i.ToString() + " deleted. Deleting other files...");
                        //need to delete other files
                        foreach (var file in Directory.GetFiles(Path.Combine(rootPath, "Node " + i.ToString())))
                        {
                            File.Delete(file);
                            Console.WriteLine(file);
                        }
                        Console.WriteLine("Node directory deleted.");
                    }
                    else
                    {
                        Console.WriteLine("Creating new Node folder #" + i.ToString());
                        Directory.CreateDirectory(Path.Combine(rootPath, "Node " + i.ToString()));
                    }
                    Console.WriteLine("Node " + i.ToString() + " free. Moving updated files there...");
                    foreach (var file in Directory.GetFiles(Path.Combine(updatePath, "Node")))
                    {
                        File.Copy(file, file.Replace("Update\\Node", "Node " + i.ToString()), true);
                        File.Delete(file);
                        Console.WriteLine(file);
                    }
                    Console.WriteLine("Node Update complete. Remember to /replacenodes in order to start them.");
                }
                var path = Path.Combine(rootPath, "Control\\werewolf control.exe");
                Process.Start(path, id);
                Console.WriteLine("Update complete.");
                Thread.Sleep(5000);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                Thread.Sleep(-1);
            }
        }
    }
}
