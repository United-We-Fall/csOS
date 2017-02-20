// Here is the latest csOS Kernel!

using System;
using System.IO;
using Sys = Cosmos.System;

namespace csOS1_1
{
    public class Kernel : Sys.Kernel
    {
        string current_directory = "0:\\" + Directory.GetCurrentDirectory();

        ///<summary>
        ///Before the operating system boots
        ///</summary>
        protected override void BeforeRun()
        {
            fs = new Sys.FileSystem.CosmosVFS();
            Sys.FileSystem.VFS.VFSManager.RegisterVFS(fs);
            Console.Clear();
            Console.WriteLine("csOS booted successfully.");
        }

        public Sys.Network.EthernetPacket ethernet;
        public Sys.FileSystem.CosmosVFS fs;

        ///<summary>
        ///The operating system utilities
        ///</summary>
        private void Utilities()
        {
            
            Console.CursorVisible = true;
        }

        ///<summary>
        ///Operating System
        ///</summary>
        protected override void Run()
        {
            Utilities();
               
            Console.Write(current_directory + "> ");
            string userinput = Console.ReadLine();

            string[] args = userinput.Split(' ');

            switch (args[0].ToLower())
            {
                case "restart":
                    Sys.Power.Reboot();
                    break;
                case "shutdown":
                    throw new Exception("Shuting Down...");
#pragma warning disable CS0162 // Unreachable code detected
                    break;
#pragma warning restore CS0162 // Unreachable code detected
                case "help":
                    Console.WriteLine("Found 13 in the directory: \n HELP: Shows a list of commands \n RESTART: Restarts client \n SHUTDOWN: Shuts down client \n CLEAR: Clears the console \n ECHO: Echoes a line of text into the console \n SYSINFO: Gives the system infomation \n DIR: Lists the volume directories \n CSPAD: csOS Text Editor. Can write to files \n DEL: Deletes the specified file \n LSDIR: Lists the files in a directory \n PRINT: Displays the text of the file \n MKDIR: Makes the directory of the users choice \n CD: Goes to the user-specified directory");
                    break;
                case "clear":
                    Console.Clear();
                    break;
                case "echo":
                    Console.Write("Line to echo:");
                    string input1 = Console.ReadLine();
                    Console.WriteLine(input1);
                    break;
                case "cspad":
                    Console.WriteLine("Entering CSPad...");
                    Console.WriteLine("Press enter to enter CSPad");
                    Console.ReadLine();
                    Console.Clear();
                    Console.BackgroundColor = ConsoleColor.White;
                    Console.ForegroundColor = ConsoleColor.Black;
                    Console.Write("File to save as:");
                    string fileName = Console.ReadLine();
                    fs.CreateFile(current_directory + fileName);
                    Console.Write("Text to save:");
                    string input = Console.ReadLine();
                    File.WriteAllText(current_directory + fileName, input);
                    Console.WriteLine("File Saved!");
                    break;
                case "del":
                    Console.WriteLine("***WARNING*** \\ This cannot be undone!");
                    Console.Write("File to Delete:");
                    string del = Console.ReadLine();
                    File.Delete(del);
                    break;
                case "print":
                    Console.WriteLine("Entering print mode...");
                    Console.Write("File to read:");
                    string file = Console.ReadLine();
                    if (File.Exists(file)) {
                        Console.WriteLine(File.ReadAllText(file));
                    } else
                    {
                        if (File.Exists(file) == false)
                        {
                            Console.WriteLine("The file you have inputed does not exist. Please try again");
                        }
                    }
                    break;
                case "sysinfo":
                    Console.WriteLine("csOS (C-Sharp Operating System) \n CREATOR: Lskywalker48 \n VERSION: 1.0.1");
                    break;
                case "dir":
                    var vols = fs.GetVolumes();
                    Console.WriteLine("NAME\tSIZE");
                    foreach (var vol in vols)
                    {
                        Console.WriteLine(vol.mName + "\t" + vol.mSize.ToString());
                    }
                    break;
                case "mkdir":
                    Console.Write("Name of Directory:");
                    string dirAdd = Console.ReadLine();
                    fs.CreateDirectory(current_directory + dirAdd);
                    Console.WriteLine("Directory Created!");
                    break;
                case "cd":
                    Console.Write("DIR to go to:");
                    string dirtogo = Console.ReadLine();

                    if (fs.GetDirectory(current_directory + dirtogo) != null)
                    {
                        current_directory = current_directory + dirtogo + "\\";
                    }
                    else
                    {
                        if (dirtogo == "goback")
                        {
                            if (current_directory.Length > 3)
                            {
                                var dirGet = fs.GetDirectory(Directory.GetCurrentDirectory());
                            }
                        }
                    }
                    break;
                case "lsdir":
                    Console.WriteLine("TYPE\tNAME");
                    foreach (var dir in Directory.GetDirectories(Directory.GetCurrentDirectory()))
                    {
                        Console.WriteLine("<DIR>\t" + dir);
                    }
                    foreach (var dir in Directory.GetFiles(Directory.GetCurrentDirectory()))
                    {
                        Console.WriteLine(Directory.GetFiles(Directory.GetCurrentDirectory()).ToString() + "\t" + dir);
                    }
                    break;
                
                case "":
                    break;
                default:
                    Console.WriteLine("The entered command: '" + userinput + "' does not exist. Please try again.");
                    break;
            }
        }
    }
}
