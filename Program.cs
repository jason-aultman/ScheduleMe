using System;
using System.Collections.Generic;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;


namespace ScheduleMe
{
    class Program
    {
        static void Main(string[] args)
        {
            const int NUMBER_OF_LINES = 5;
            const string COMPANY = "Apac Manufacturing";
            const string DEPARTMENT = "Extrusion";

            Clipboard[] lines = new Clipboard[NUMBER_OF_LINES];
            for (int a=0; a<NUMBER_OF_LINES; a++) //creating Clipboards x number  of lines and adding them to array
            {
                Console.WriteLine("Creating clipboard for line number {0}", a+1);
                lines[a] = new Clipboard();
            }

        //Assigning alias for each line
            Clipboard Line_1 = lines[0];
            Clipboard Line_2 = lines[1];
            Clipboard Line_3 = lines[2];
            Clipboard Line_4 = lines[3];
            Clipboard Line_2SM = lines[4];
 /*           Line_1.workOrders.Add(new WorkOrder(50902, "27.0018 blue", true, 332, 2496, "sho490", new DateTime(2020, 03, 17)));
            Line_1.workOrders.Add(new WorkOrder(50945, "33x33.75 ,001125", false, 300, 848, "but700", new DateTime(2020, 02, 28)));
            Line_1.workOrders.Add(new WorkOrder(50941, "20x10 .0018", false, 300, 6, "sample 1", new DateTime(2020, 03, 05)));
            Line_1.workOrders.Add(new WorkOrder(50901, "18x16 .00135", false, 350, 139, "adv100", new DateTime(2020, 03, 03)));
            Line_1.workOrders.Add(new WorkOrder(50895, "25x17 .00135", false, 300, 200, "adv100", new DateTime(2020, 03, 03)));
            Line_1.workOrders.Add(new WorkOrder(50913, "11.5x13.5 .0009", false, 282, 85, "adv385", new DateTime(2020, 03, 06)));
            Line_1.workOrders.Add(new WorkOrder(50947, "30x60.0036", false, 330, 476, "con800", new DateTime(2020, 03, 09)));
            Line_1.workOrders.Add(new WorkOrder(50943, "24 .0018", false, 251, 571, "dir500", new DateTime(2020, 03, 10)));
            Line_1.workOrders.Add(new WorkOrder(50933, "20.5x89 .0027", false, 275, 362, "man550", new DateTime(2020, 03, 10)));
            Line_1.workOrders.Add(new WorkOrder(50934, "20.5 x 89 .0027", false, 275, 362, "man550", new DateTime(2020, 03, 10)));
            Line_1.workOrders.Add(new WorkOrder(50936, "30 .0027", false, 350, 177, "pro270", new DateTime(2020, 03, 10)));
            Line_1.workOrders.Add(new WorkOrder(50867, "34x37.5 .0018", false, 300, 6077, "xpe500", new DateTime(2020, 03, 10)));

            Line_2.workOrders.Add(new WorkOrder(23111, "verit1", false, 250, 2000, "gol100", new DateTime(2020, 02, 28)));
            Line_3.workOrders.Add(new WorkOrder(32115, "shore1", false, 600, 7500, "sho500", new DateTime(2020, 02, 29)));
            */
         //   Serialize_Clipboards(lines);
        //    Console.WriteLine("Clearing all clipboards...");
         //   Array.Clear(lines, 0, 5);
            Console.WriteLine("Loading {0} {1} schedules.....please wait", COMPANY, DEPARTMENT);
            //    lines = Deserialize_Clipboards();
            lines = Serialize_Me.GetClipboards("workorders.binary");
            Console.WriteLine("Done");
            bool exit = false;
            do
            {
                exit = menuSelect(menu(), lines);
            } while (!exit);

          static void Report(Clipboard[] lines)
            {
                Console.WriteLine("\n\n");
                Console.WriteLine("These are the work orders as they were entered on each clipboard".ToUpper());
                foreach (Clipboard c in lines)
                {
                    c.runReport(c.workOrders);
                }
                Console.WriteLine("\n\n\n");
          
            }
          static int menu()
            {
                Console.WriteLine("Press any key to continue....");
                string alpha = Console.ReadLine();
                System.Console.Clear();
                Console.WriteLine("Main Menu");
                Console.WriteLine("1. Enter new work order");
                Console.WriteLine("2. Run report for all lines");
                Console.WriteLine("3. Move item");
                Console.WriteLine("4. Delete item");
                Console.WriteLine("5. Show Schedule");
                Console.WriteLine("6. Get Order Item details");
                Console.WriteLine("7. Setup clipboards");
                Console.WriteLine("Enter your choice 1-7, or 9 to exit");
                try
                {
                    return Convert.ToInt32(Console.ReadLine());
                }catch
                {
                    Console.WriteLine("The input must be in the form of a number");
                    
                }
                return 0;
            }
          static bool menuSelect(int c, Clipboard[] lines)
            {
                switch(c)
                {
                    case 1:
                        Console.WriteLine("What line #:");
                        try
                        {
                            int lineNumber = Convert.ToInt32(Console.ReadLine());
                            if (lineNumber == 1) { lines[lineNumber - 1].AddNewWorkOrder(); }
                            else if (lineNumber == 2) { lines[lineNumber - 1].AddNewWorkOrder(); }
                            else if (lineNumber == 3) { lines[lineNumber - 1].AddNewWorkOrder(); }
                            else if (lineNumber == 4) { lines[lineNumber - 1].AddNewWorkOrder(); }
                            else if (lineNumber == 5) { lines[lineNumber - 1].AddNewWorkOrder(); }
                            else
                            {
                                Console.WriteLine("Invalid line number must be between 1 and 5, returning to menu");
                                menuSelect(menu(), lines);
                            }
                        }
                        catch { Console.WriteLine("invalid selection, nothing will be done"); }
                        //  Serialize_Clipboards(lines);
                        Serialize_Me.Make(lines,"workorders.binary");
                        menuSelect(menu(), lines);
                        break;

                    case 2:
                        Console.WriteLine("Running report....");
                        Report(lines);
                        break;

                    case 3:
                        Console.WriteLine("Which clipboard?:");
                        int input = 0;
                        try
                        {
                            input = Convert.ToInt32(Console.ReadLine());
                        }
                        catch
                        {
                            Console.WriteLine("That is not a valid input, doing nothing");
                            
                        }
                        if (input != 0)
                        {
                            //need to add Try Catch statements to this for input error handling
                            lines[input - 1].runReport(lines[input - 1].workOrders);
                            Console.WriteLine("Which number to move");
                            int a = Convert.ToInt32(Console.ReadLine());
                            Console.WriteLine("Move to which space");
                            int b = Convert.ToInt32(Console.ReadLine());
                            lines[input - 1].MoveOrder(--a, --b); //subtracting 1 to each parameter for correct index values 
                            lines[input - 1].runReport(lines[input - 1].workOrders);

                        }
                        // Serialize_Clipboards(lines);
                        Serialize_Me.Make(lines, "workorders.binary");
                        break;
                    case 4:
                        {
                            Console.WriteLine("Which clipboard?:");
                            input = 0;
                            try
                            {
                                input = Convert.ToInt32(Console.ReadLine());
                            }catch
                            {
                                Console.WriteLine("Must be a number, nothing will be done");
                            }
                            if (input!=0)
                            {
                                lines[input - 1].runReport(lines[input - 1].workOrders);
                                Console.WriteLine("Remove which work order?");
                                int del = Convert.ToInt32(Console.ReadLine()); //add Int validation and error checking here
                                //call delete function on clipboard with a int parameter del
                                lines[input - 1].DeleteOrder(del - 1); //del-1 to get correct index for list<>
                                lines[input - 1].runReport(lines[input - 1].workOrders);
                            }
                            //Serialize_Clipboards(lines);
                            Serialize_Me.Make(lines,"workorders.binary");
                            break;
                            
                        }
                    case 5:
                        {
                            Console.WriteLine("Enter date and time to start schedule 'mm/dd/yyyy hh:mm:ss' or type 'now' for starting right now");
                            DateTime start = new DateTime();
                            try
                            {
                                start = Convert.ToDateTime(Console.ReadLine());
                            }catch
                            {
                                //start = DateTime.Now;
                                foreach (Clipboard c2 in lines)
                                {
                                    Console.WriteLine("Line {0}", c2.lineNumber);
                                    c2.RunSchedule(c2.dayStart);
                                }
                             break;


                            }
                            foreach (Clipboard c2 in lines)
                            {
                                Console.WriteLine("Line {0}",c2.lineNumber);
                                c2.RunSchedule(start);
                            }
                            break;

                        }
                    case 6:
                        {
                            Console.WriteLine("Which clipboard?:");
                            input = 0;
                            try
                            {
                                input = Convert.ToInt32(Console.ReadLine());
                                input--;
                            }
                            catch
                            {
                                Console.WriteLine("Must be a number, nothing will be done");
                            }
                            if (input >= 0 && input < NUMBER_OF_LINES)
                            {
                                lines[input].runReport(lines[input].workOrders);
                                Console.WriteLine("Details on which work order?");
                                int deets = Convert.ToInt32(Console.ReadLine()); //add Int validation and error checking here
                                //call delete function on clipboard with a int parameter del
                                lines[input].workOrders[deets - 1].Details();
                                
                            }
                            break;
                        }
                    case 7:
                        {
                           
                            foreach (Clipboard clips in lines)
                            {
                                Console.WriteLine("Starting time for schedule on line {0}", clips.lineNumber);
                                clips.dayStart = Convert.ToDateTime(Console.ReadLine());
                            }
                            Serialize_Me.Make(lines, "workorders.binary");
                           // Serialize_Clipboards(lines);

                            break;
                        }
                    case 9:
                        {
                            return true;
                        }

                    default:
                        Console.WriteLine("No valid selections were made, returning to main menu");
                        menuSelect(menu(), lines);
                        break;
                }





                return false;
            }
 /*         static void Serialize_Clipboards(Clipboard[] clipboards)
            {
                BinaryFormatter bf = new BinaryFormatter();
                FileStream fileStream = new FileStream("workorders.binary", FileMode.Create, FileAccess.Write, FileShare.None);
                try
                {
                    using (fileStream)
                    {
                        bf.Serialize(fileStream, clipboards);
                        Console.WriteLine("objects serialized");
                    }
                }
                catch
                {
                    Console.WriteLine("Nothing was done, sorry");
                }
            }
          static Clipboard[] Deserialize_Clipboards()
            {
                Clipboard[] cb;
                BinaryFormatter bf = new BinaryFormatter();
                FileStream fileStream = new FileStream("workorders.binary", FileMode.Open, FileAccess.Read, FileShare.None);
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
            */
        }


    }
}
