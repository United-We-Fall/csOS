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
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("Welcome back User!");
            Console.Write("Password:");
            string input = Console.ReadLine();
            if (input == "csOS_user")
            {
                Console.Clear();
                Run();
            }
            else
            {
                BeforeRun();
            }
        }
        
        public Sys.FileSystem.CosmosVFS fs;

        ///<summary>
        ///The operating system utilities
        ///</summary>
        private void Utilities()
        {
            Console.CursorVisible = true;
            Sys.Global.NumLock = true;
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
                    Console.WriteLine("Found 13 in the directory: \n HELP: Shows a list of commands \n RESTART: Restarts client \n SHUTDOWN: Shuts down client \n CLEAR: Clears the console \n ECHO: Echoes a line of text into the console \n COLOR: Changes the console color based on the user prefrences \n SYSINFO: Gives the system infomation \n DIR: Lists the volume directories \n CSPAD: csOS Text Editor. Can write to files \n DEL: Deletes the specified file \n LSDIR: Lists the files in a directory \n PRINT: Displays the text of the file \n MKDIR: Makes the directory of the users choice \n CD: Goes to the user-specified directory");
                    break;
                case "clear":
                    Console.Clear();
                    break;
                case "echo":
                    Console.Write("Line to echo:");
                    string input1 = Console.ReadLine();
                    Console.WriteLine(input1);
                    break;
                case "color":
                    Console.WriteLine("Valid colors: ROYGBIV, Black, and White");
                    Console.Write("Color:");
                    string input2 = Console.ReadLine();
                    switch (input2)
                    {
                        case "red":
                            Console.ForegroundColor = ConsoleColor.Red;
                            Console.WriteLine("Color changed");
                            break;
                        case "orange":
                            Console.ForegroundColor = ConsoleColor.DarkYellow;
                            break;
                        case "yellow":
                            Console.ForegroundColor = ConsoleColor.Yellow;
                            break;
                        case "green":
                            Console.ForegroundColor = ConsoleColor.Green;
                            break;
                        case "blue":
                            Console.ForegroundColor = ConsoleColor.DarkBlue;
                            break;
                        case "indigo":
                            Console.ForegroundColor = ConsoleColor.Blue;
                            break;
                        case "violet":
                            Console.ForegroundColor = ConsoleColor.Magenta;
                            break;
                        case "black":
                            Console.ForegroundColor = ConsoleColor.Black;
                            break;
                        case "white":
                            Console.ForegroundColor = ConsoleColor.White;
                            break;
                        case "reset":
                            Console.ForegroundColor = ConsoleColor.Green;
                            Console.WriteLine("Color reset.");
                            break;
                        default:
                            Console.WriteLine("The color you have inputed is not a valid color. Please use ROYGBIV and Black and White.");
                            break;
                    }
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
        
        private void CSPadGUI()
        {
            Console.WriteLine();
            Console.WriteLine();
            Console.WriteLine();
            Console.WriteLine();
            Console.WriteLine();
            Console.ForegroundColor = ConsoleColor.White;
            Console.BackgroundColor = ConsoleColor.DarkBlue;
            Console.Write("CSPad                                                                          ");
            Console.BackgroundColor = ConsoleColor.Red;
            Console.Write("X");
            Console.BackgroundColor = ConsoleColor.Green;
            Console.Write("This application uses a test text-based GUI that is still in the works. If you like it, I will including this as the main csOS GUI in a later version.          ");
            Console.Write("                                                                        Enter:OK");
            Console.ReadLine();
            Console.BackgroundColor = ConsoleColor.Black;
            Console.WriteLine();
            Console.WriteLine();
            Console.WriteLine();
            Console.WriteLine();
            Console.Clear();
            Console.BackgroundColor = ConsoleColor.DarkBlue;
            Console.Write("CTRL+F:File  CTRL+E:Edit                                                 ");
            Console.BackgroundColor = ConsoleColor.Red;
            Console.Write("Enter:X");
        }
    }
}
