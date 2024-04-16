using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Railway_Reservation
{
    class Program
    {
        // craete an object of the context class and instantiate it
        static MiniProjectTrainEntities db = new MiniProjectTrainEntities();
        static Train train = new Train();
        static TicketClassDetail TCD = new TicketClassDetail();

        static void Main(string[] args) { 
        Console.WriteLine("=======================================");
       Console.WriteLine("|                                     |");
       Console.WriteLine("|        Railway Ticket Forum         |");
       Console.WriteLine("|                                     |");
       Console.WriteLine("=======================================");
       Console.WriteLine("|                                     |");
       Console.WriteLine("|    Welcome to Railway Forum        |");
       Console.WriteLine("|   Your One-Stop Destination for    |");
       Console.WriteLine("|        All Your Rail Travel        |");
       Console.WriteLine("|             Queries                 |");
       Console.WriteLine("|                                     |");
       Console.WriteLine("=======================================");

            Console.WriteLine("1 for  login as Admin");
            Console.WriteLine("2 for  login as User");
            Console.WriteLine("Enter your Choice");
            string Choice = Console.ReadLine();
            switch (Choice)
            {
                case "1":
                    Console.WriteLine("You have now admin");
                    Admincontrol();
                    break;
                case "2":
                    Console.WriteLine("you have now user you have access to user function");
                    UserControl();
                    break;
                default:
                    Console.WriteLine("Enter valid");
                    break;
            }
            Console.Read();
        }
        static void Admincontrol()
        {
            Console.WriteLine("Enter Admin ID:");
            int AdminId = int.Parse(Console.ReadLine());
            Console.WriteLine("Enter Password:");
            string Password = Console.ReadLine();
            var admin = db.Admins.FirstOrDefault(a => a.AdminId == AdminId && a.Password == Password);

            if (admin != null)
            {
                Console.WriteLine("Admin login successful!");
                Console.WriteLine("Now you can access all admin authorization ");
                Console.WriteLine("Press 1 for Add Trains");
                Console.WriteLine("Press 2 for Modify Trains");
                Console.WriteLine("Press 3 for SoftDeleting");
                string res = Console.ReadLine();
                switch (res)
                {
                    case "1":
                        Console.WriteLine("Please Add The Trains");
                        AddTrain();
                        Console.WriteLine("Trains has been successfully added");
                        displaytrain();
                        break;
                    case "2":
                        Console.WriteLine("Please modify the train");
                        displaytrain();
                        ModifyTrain();
                        break;
                    case "3":
                        Console.WriteLine(" Please soft delete the train");
                        DeleteTrain();
                        displaytrain();
                        break;
                    default:
                        Console.WriteLine("Enter valid number");
                        break;
                }
            }
            else
            {
                Console.WriteLine("Invalid admin credentials, you can't access the admincontrol.");
                Console.ReadLine();
                Environment.Exit(0);
            }
            } 
        static void UserControl()
        {
            Console.WriteLine("Now you can access all user authorization ");
            Console.WriteLine("Press 1 for Booking Trains");
            Console.WriteLine("Press 2 for Cancel Trains");
            string res = Console.ReadLine();
            switch (res)
            {
                case "1":
                    Console.WriteLine("Please Book the tickets");
                   // displaytrain();
                    //BookingTicket();
                    Console.WriteLine("Here is your printing ticket");
                    DisplayBooking_Details();
                    Console.WriteLine("Booking has been successfully completed");
                    break;
                case "2":
                    Console.WriteLine("Please Cancel the tickets");
                    Cancel_train();
                    break;
                default:
                    Console.WriteLine("Enter valid number");
                    break;
            }
        }
        static void AddTrain()
        {

            Console.WriteLine("Enter Train Number:");
            train.TrainNo = int.Parse(Console.ReadLine());
            Console.WriteLine("Enter Train Name:");
            train.TrainName = Console.ReadLine();
            Console.WriteLine("Enter Source:");
            train.Source = Console.ReadLine();
            Console.WriteLine("Enter Destination:");
            train.Destination = Console.ReadLine();
            train.Status = "Active";
            db.Trains.Add(train);
            db.SaveChanges();
            var classname = new string[] { "First Class", "Second Class", "Sleeper" };
            foreach (var ClassName in classname)
            {
                TCD.TrainNo = train.TrainNo;
                TCD.ClassName = ClassName;
                Console.WriteLine($"Enter TOTAL SEATS{ClassName}:");
                TCD.TotalSeats = int.Parse(Console.ReadLine());
                Console.WriteLine("availble seats:");
                TCD.AvailableSeats = int.Parse(Console.ReadLine());
                Console.WriteLine("Enter Fare Amount");
                TCD.Fare = int.Parse(Console.ReadLine());

                db.TicketClassDetails.Add(TCD);
                db.SaveChanges();
            }
        }
        static void displaytrain()
        {
            var trains = db.Trains.ToList();

            // Display the result in the console
            foreach (var train in trains)
            {
                Console.WriteLine($"TrainNo: {train.TrainNo}, TrainName: {train.TrainName}, Source: {train.Source}, Destination: {train.Destination}, Status: {train.Status}");

            }
            displaytickeDisplayTicketClass_Detail();
        }

        static void displaytickeDisplayTicketClass_Detail()
        {

            Console.WriteLine("Enter Train Number:");
            train.TrainNo = int.Parse(Console.ReadLine());
            var trainVal = db.TicketClassDetails.Where(t => t.TrainNo == train.TrainNo).ToList();


            // Display the result in the console
            foreach (var ticketClassDetail in trainVal)
            {
                Console.WriteLine($"TrainNo: {ticketClassDetail.TrainNo}, ClassName: {ticketClassDetail.ClassName}, TotalSeats: {ticketClassDetail.TotalSeats}, AvailableSeats: {ticketClassDetail.AvailableSeats}, Fare: {ticketClassDetail.Fare}");
            }
        }


        public static void ModifyTrain()
        {
            Console.WriteLine("Enter Train Number:");
            int trainNo = int.Parse(Console.ReadLine());

            // Retrieve the train details from the database
            var trainToUpdate = db.Trains.FirstOrDefault(t => t.TrainNo == trainNo);
            if (trainToUpdate != null)
            {
                Console.WriteLine("Enter Train Name:");
                trainToUpdate.TrainName = Console.ReadLine();
                Console.WriteLine("Enter Train source:");
                trainToUpdate.Source = Console.ReadLine();
                Console.WriteLine("Enter Train Destination:");
                trainToUpdate.Destination = Console.ReadLine();
                Console.WriteLine("Enter Status:");
                trainToUpdate.Status = Console.ReadLine();
                // Update the train in the database
                db.SaveChanges();
                Console.WriteLine("Train details updated successfully.");
                Console.WriteLine("Here is the after modification table");
                displaytrain();
                Console.ReadLine();
                Environment.Exit(0);
            }

            else
            {
                Console.WriteLine("Train with the provided number does not exist.");
                Admincontrol();
            }
        }
        public static void DeleteTrain()
        {
            Console.WriteLine("Enter Train Number:");
            int trainNo = int.Parse(Console.ReadLine());

            // Retrieve the train details from the database
            var trainToDelete = db.Trains.FirstOrDefault(t => t.TrainNo == trainNo);
            if (trainToDelete != null)
            {
                // Soft delete by updating the status to "Inactive"
                trainToDelete.Status = "InActive";
                db.SaveChanges();
                Console.WriteLine("Train  Soft deleted successfully.");
                Console.ReadLine();
                Environment.Exit(0);
            }
            else
            {
                Console.WriteLine("Train with the provided number does not exist.");
                Admincontrol();
            }
        }
        public static void BookingTicket()
        {
            try
            {

                Console.Write("Enter Passenger Name: ");
                string PassengerName = Console.ReadLine();

                Console.Write("Enter Train Number: ");
                int TrainNo = Convert.ToInt32(Console.ReadLine());

                Console.Write("Enter Class Name: ");
                string ClassName = Console.ReadLine();

                Console.Write("Enter Date of Travel (YYYY-MM-DD): ");
                DateTime DateOfTravel = Convert.ToDateTime(Console.ReadLine());

                Console.Write("Enter Number of Tickets: ");
                int NumberOfTickets = Convert.ToInt32(Console.ReadLine());


                db.BookTrainTicket(PassengerName, TrainNo, ClassName, DateOfTravel, NumberOfTickets);
                db.SaveChanges();

                Console.WriteLine("Ticket booked successfully!");
            }
            catch (Exception ex)
            {
                Console.WriteLine("An error occurred: " + ex.Message);
            }
        }
            static void Cancel_train()
            {
                
                    Console.WriteLine("Enter Booking ID:");
                    int bookingId = int.Parse(Console.ReadLine());

                    Console.WriteLine("Enter Passenger Name:");
                    string passengerName = Console.ReadLine();

                    Console.WriteLine("Enter Train Number:");
                    int trainNo = int.Parse(Console.ReadLine());

                    Console.WriteLine("Enter Class Name:");
                    string className = Console.ReadLine();

                    Console.WriteLine("Enter Number of Tickets to Cancel:");
                    int numberOfTickets = int.Parse(Console.ReadLine());

            try {   
                    // Call the stored procedure to cancel the ticket
                    db.CancelTicket(bookingId, passengerName, trainNo, className, numberOfTickets);
                    db.SaveChanges();
                    Console.WriteLine("Cancellation successful!");

                }
                catch (Exception ex)
                {
                    Console.WriteLine($"An error occurred: {ex.Message}");
                }
            }
        static void DisplayBooking_Details()
        {
             Console.WriteLine("Enter Booking ID:");
                int bookingId = int.Parse(Console.ReadLine());

                var booking = db.Bookings.FirstOrDefault(b => b.BookingId == bookingId);
            if (booking != null)
            {
                Console.WriteLine("╔═════════════════════════════════════════╗");
                Console.WriteLine("║             BOOKING DETAILS              ║");
                Console.WriteLine("╠═════════════════════════════════════════╣");
                Console.WriteLine($"║ Booking ID:       {booking.BookingId}║");
                Console.WriteLine($"║ Passenger Name:   {booking.PassengerName}║");
                Console.WriteLine($"║ Train Number:     {booking.TrainNo}║");
                Console.WriteLine($"║ Class Name:       {booking.ClassName}║");
                Console.WriteLine($"║ Date of Travel:   {booking.DateOfTravel}║");
                Console.WriteLine($"║ Number of Tickets:{booking.NumberOfTickets}║");
                Console.WriteLine($"║ Total Amount:     {booking.TotalAmount}║");
                Console.WriteLine($"║ Status:           {booking.Status}║");
                Console.WriteLine("╚═════════════════════════════════════════╝");


            }
            else
            {
                Console.WriteLine("Booking not found.");
            }
        }

    }
}

