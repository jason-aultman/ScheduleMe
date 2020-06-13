using System;
using System.Collections.Generic;
using System.Text;
using System.IO;

namespace ScheduleMe
{
    [Serializable()]
    public class WorkOrder
    {
        
        public int ID;
        public string description;
        public bool color;
        public double rate;
        public double quantity = 0;
        public string customer;
        public double productSize;
        public DateTime dueDate;
        public double totalTime;
        public bool onTime = true;
        public DateTime mustStartDate;

        public WorkOrder()
        {
            bool more = true;
            //Ask and validate user input for  ID
            Console.WriteLine("What is the work order number");
            do
            {
                try
                {
                    ID = Convert.ToInt32(Console.ReadLine());
                    more = false;
                }
                catch
                {
                    Console.WriteLine("There was an error with your input, must be in the form of a whole number, please try again");
                }

            } while (more);
            //Ask and  validate user input for Description
            more = true;
            Console.WriteLine("What is the product description?");
            do
            {
                try
                {
                    description=Console.ReadLine().ToString();
                    more = false;
                }
                catch
                {
                    Console.WriteLine("There was an error with your input, must be in the form of a string, please try again");
                }

            } while (more);
            more = true;
            //Ask and validate user input for colored
            Console.WriteLine("Is the film colored? True or false");
            do
            {
                try
                {
                    color = Convert.ToBoolean(Console.ReadLine());
                    more = false;
                }
                catch
                {
                    Console.WriteLine("There was an error with your input, must be in the form of a True or False, please try again");
                }

            } while (more);
            //Ask and validate user input for quantity
            more = true;
            Console.WriteLine("What is the quantity of product in lbs");
            do
            {
                try
                {
                    quantity = Convert.ToDouble(Console.ReadLine());
                    more = false;
                }
                catch
                {
                    Console.WriteLine("There was an error with your input, must be in the form of a number, please try again");
                }

            } while (more);
            //Ask and validate user input for rate
            more = true;
            Console.WriteLine("What is the rate?");
            do
            {
                try
                {
                    rate = Convert.ToDouble(Console.ReadLine());
                    more = false;
                }
                catch
                {
                    Console.WriteLine("There was an error with your input, must be in the form of a number, please try again");
                }

            } while (more);
            //Ask and validate user input for bubble size
            more = true;
            Console.WriteLine("What is the bubble size for this product?");
            do
            {
                try
                {
                    productSize = Convert.ToDouble(Console.ReadLine());
                    more = false;
                }
                catch
                {
                    Console.WriteLine("There was an error with your input, must be in the form of a whole number, please try again");
                }

            } while (more);
            //Ask and validate user input for due date
            more = true;
            Console.WriteLine("What is the due date for this order?");
            do
            {
                try
                {
                    dueDate = Convert.ToDateTime(Console.ReadLine());
                    more = false;
                }
                catch
                {
                    Console.WriteLine("There was an error with your input, must be in the form of a date mm/dd/yyyy, please try again");
                }

            } while (more);
            //calculate total time to  run the order in minutes
            totalTime = Runtime(quantity, rate);
            Console.WriteLine("What is the customer name or id");
            do
            {
                try
                {
                    customer = Console.ReadLine();
                    more = false;
                }
                catch
                {
                    Console.WriteLine("There was an error with your input, must be in the form of a word or phrase, please try again");
                }
            } while (more);
            mustStartDate = MustStartBy(dueDate, totalTime);
            System.Console.Clear();
        }


         public WorkOrder(int workOrderNumber, string Description, bool Color, double CycleTime, double Units, string custCode, DateTime due)
        {
            ID = workOrderNumber;
            description = Description;
            color = Color;
            rate = CycleTime;
            quantity = Units;
            customer = custCode;
            dueDate = due;
            totalTime = Runtime(quantity, rate);
            mustStartDate = MustStartBy(dueDate, totalTime);
        }
        public DateTime MustStartBy(DateTime due, double runtime)
        {
            DateTime need;
            TimeSpan time = new TimeSpan(0, (int)runtime*60, 0);
            need = due-time;
            return need;

        }
        public void Details()
        {
            Console.WriteLine("Work order number: {0}", ID);
            Console.WriteLine("Customer id: {0}", customer);
            Console.WriteLine("Description : {0}", this.description);
            Console.WriteLine("Quantity : {0}", this.quantity);
            Console.WriteLine("Due : {0}", dueDate);
            Console.WriteLine("Is Colored: {0}", color);

        }
       
       
        
        public double Runtime(double unit, double cycles)
        {
            return (Math.Floor((unit / cycles)*100)/100);
        }
    }
}
