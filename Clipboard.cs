using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using System.IO;

namespace ScheduleMe
{
    [Serializable()]
    public class Clipboard
    {
        public List<WorkOrder> workOrders;
        public static int numb = 1;
        public int lineNumber = 0;
        public const double HOURS_IN_A_WEEK = 168.0;
        public const double HOURS_IN_A_DAY = 24.0;
        public DateTime dayStart;
        public Clipboard()
        {
            workOrders = new List<WorkOrder>();
            lineNumber = numb;
            numb++;
            
        }
        public Clipboard(List<WorkOrder> ts)
        {
            workOrders = ts;
        }
        public void AddNewWorkOrder()
        {
            workOrders.Add(new WorkOrder());
        }

        public List<WorkOrder> filteredByDate()
        {
            List<WorkOrder> filtered = new List<WorkOrder>();
            workOrders.Sort(delegate (WorkOrder a, WorkOrder b)
                {
                    return a.dueDate.CompareTo(b.dueDate);
                });
            foreach(WorkOrder w in workOrders)
            {
                if (w.dueDate <= DateTime.Now||w.mustStartDate<DateTime.Now)
                {
                    w.onTime = false;
                }
            }
            return workOrders;
        }
        public List<WorkOrder> GetWorkOrdersByNeedToStartDate()
        {
            List<WorkOrder> filtered = new List<WorkOrder>();
            workOrders.Sort(delegate (WorkOrder a, WorkOrder b)
                {
                    return a.mustStartDate.CompareTo(b.mustStartDate);
                });
            return workOrders;
        }
       
        public List<WorkOrder> GetWorkOrdersDueBy(DateTime date)
        {
            IEnumerable<WorkOrder> orderingQuery =
                from ord in workOrders
                where ord.dueDate < date
                select ord;
            List<WorkOrder> tempOrderList = new List<WorkOrder>();
            foreach (WorkOrder ord in orderingQuery)
                {
                tempOrderList.Add(ord);
            }
            return tempOrderList;
        }
        public void MoveOrder(int a, int b)
        {
            WorkOrder temp = workOrders[a];
            workOrders.RemoveAt(a);
            workOrders.Insert(b, temp);
        }
        public void  DeleteOrder(int del)
        {
            workOrders.RemoveAt(del);
            Console.WriteLine("Work order deleted");
        }

        public void runReport(List<WorkOrder> orders)
        {
            double cumulativeTime = 0;
            int i = 1;
            Console.WriteLine();
            Console.WriteLine("Line number {0}", lineNumber);
            Console.WriteLine("{0,-12}{1,-14}{2,-12}{3,-12}{4,-24}","Customer","Work Order","Due date","Total time", "Colored?");
            Console.WriteLine("----------------------------------------------------------------------------------------------");
            foreach (WorkOrder w in orders)
            {
                cumulativeTime += w.totalTime;
                Console.WriteLine("["+i+"]"+"{0,-12}{1,-14}{2,-12}{3,-12}{4,-24}", w.customer, w.ID, String.Format("{0:M/d/yyyy}",w.dueDate), w.totalTime, w.color);
                i++;
            }
            Console.WriteLine("----------------------------------------------------------------------------------------------");
            Console.WriteLine("Total hours = {0} ",cumulativeTime);
            Console.WriteLine();
            ErrorLog();
        }
        public void RunSchedule(DateTime start)
        {
            DateTime Start = start;
            DateTime EndDateTime;
            Console.WriteLine("Work order  Run TIme   Start and end times                              On Time?");
            Console.WriteLine("---------------------------------------------------------------------------------");
            foreach(WorkOrder w in workOrders)
            {
                Console.WriteLine("{0, -12} {1,-8} {2} - {3}     {4}", w.ID, w.totalTime, Start, EndDateTime = Start.AddHours(w.totalTime), (w.dueDate>EndDateTime));
                Start = EndDateTime;
                
            }
            ErrorLog();
        }
        public void ErrorLog()
        {
            foreach (WorkOrder w in workOrders)
            {
                if (!w.onTime)
                {
                    Console.WriteLine("Warning! The following work order is past due:  {0}", w.ID);
                }
            }
        }
    }
}
