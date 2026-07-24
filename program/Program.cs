using System.Numerics;

namespace Static_OAT;

class Program
{
    const string cfgpath = "externalcamera.cfg";

    static Comms mmf;

    static float DtoR(float degrees)
    {
        return (float)(degrees * Math.PI / 180d);
    }

    static void DoFile(string path)
    {
        if (!File.Exists(path)) {
            Console.WriteLine("Config file does not exist.");
            return;
        }
        Dictionary<string,string> keyvalues = new Dictionary<string, string>();

        foreach (string line in File.ReadAllLines(path)) {
            if (line == ""){continue;}
            var t = line.Split('=');
            if (t.Length == 2) {keyvalues[t[0]] = t[1];}
        }

        ExternalCameraCfg e = new ExternalCameraCfg(keyvalues);
        Console.WriteLine("Read Values: "+e.ToString());

        Vector3 vec = new Vector3(e.x ,e.y, e.z);

        Quaternion quat = Quaternion.CreateFromYawPitchRoll(DtoR(e.ry), DtoR(e.rx), DtoR(e.rz));
        Console.WriteLine("Made Quaternion: "+quat.ToString());

        Console.WriteLine("Writing camera position.");
        mmf.Write(vec);
        Console.WriteLine("Writing camera rotation.");
        mmf.Write(quat);
        Console.WriteLine("Writing camera fov.");
        mmf.Write(e.fov);
        
    }

    static void Main(string[] args)
    {
        Console.WriteLine("Static Camera Tool for OnAirTap");
        Console.WriteLine("by milkydelta");
        Console.WriteLine("Press ESC to exit.");
        Console.WriteLine("Press R to (Re)load the config.");
        Console.WriteLine("Press E to toggle the camera.");
        Console.WriteLine("Press L to toggle mod logging.");
        Console.WriteLine("Press P to toggle mod log spam.");

        mmf = Comms.New();

        Console.WriteLine("Opening shared memory");
        mmf.Open("uk.lum.livnyan.cameradata.v1.1");


        ConsoleKeyInfo ck;
        do
        {
            ck = Console.ReadKey(true);

            switch (ck.Key)
            {
                case ConsoleKey.R:
                    DoFile(cfgpath);
                    break;
                case ConsoleKey.E:
                    Console.Write("Toggled camera ");
                    mmf.Toggle(LIVnyan_cfg.CAM_ON);
                    break;
                case ConsoleKey.L:
                    Console.Write("Toggled logs ");
                    mmf.Toggle(LIVnyan_cfg.LOG_ON);
                    break;
                case ConsoleKey.P:
                    Console.Write("Toggled log spam ");
                    mmf.Toggle(LIVnyan_cfg.LOGSPM);

                    break;
                default:
                    break;
            }
            
        } while (ck.Key != ConsoleKey.Escape);


        // For some reason, on Linux, calling close here deletes the SHM, even if BS still has it open.
        // That doesn't cause segfaults, but it does mean OAT silently stops working.

        //Console.WriteLine("Closing shared memory");
        //mmf.Close();

    }
}
