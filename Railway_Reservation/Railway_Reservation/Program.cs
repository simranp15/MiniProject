using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Railway_Reservation
{
    class Program
    {
        // Create an object of the context class and instantiate it
        static MiniProjectTrainEntities db = new MiniProjectTrainEntities();
        static Train train = new Train();
        static TicketClassDetail TCD = new TicketClassDetail();

        static void Main(string[] args)
        {
            Console.WriteLine("                                     =======================================               ");
            Console.WriteLine("                                     |                                     |               ");
            Console.WriteLine("                             |         Welcome to the Indian Railway Forum     |               ");
            Console.WriteLine("                                     |                                     |               ");
            Console.WriteLine("                                     =======================================                ");
            while (true)
            {

                Console.WriteLine("\n Press 1 for login as Admin");
                Console.WriteLine(" Press 2 for login as User");
                Console.WriteLine(" Press 3 for exit");
                Console.WriteLine("\n Enter your Choice");
                string Choice = Console.ReadLine();
                switch (Choice)
                {
                    case "1":
                        Console.WriteLine("\n You have now admin");
                        Admincontrol();
                        break;
                    case "2":
                        Console.WriteLine("\nYou have now user, you have access to user functions");
                        UserControl();
                        break;
                    case "3":
                        Console.WriteLine("\nThank You For Your Time....");
                        break;
                    default:
                        Console.WriteLine("\nEnter valid input");
                        break;
                }
                Console.Read();
            }
        }   //--------------------------------------Admin Control--------------------------------------------------------------------------------------------------------------------
        static void Admincontrol()
        {
            Console.WriteLine("Enter Admin ID:");
            int AdminId = int.Parse(Console.ReadLine());
            Console.WriteLine("Enter Password:");
            string Password = Console.ReadLine();
            var admin = db.Admins.FirstOrDefault(a => a.AdminId == AdminId && a.Password == Password);
            while (true)
            {
                if (admin != null)
                {
                    Console.WriteLine("\nAdmin login successful!");
                    Console.WriteLine("Now you can access all admin authorization ");
                    Console.WriteLine("\nPress 1 for Add Trains");
                    Console.WriteLine("Press 2 for Modify Trains");
                    Console.WriteLine("Press 3 for SoftDeleting");
                    Console.WriteLine("Press 4 for logout");
                    Console.WriteLine("\n Enter your Choice");

                    string res = Console.ReadLine();
                    switch (res)
                    {
                        case "1":
                            Console.WriteLine("Please Add The Trains");
                            AddTrain();
                            Console.WriteLine("Trains have been successfully added");
                            Console.WriteLine("\n********************************************************************************************************************");
                            displaytrainForAdmin();
                            DisplayAllTicketClassDetailsForAdmin();
                            break;
                        case "2":
                            Console.WriteLine("Please modify the train");
                            Console.WriteLine("\n********************************************************************************************************************");
                            displaytrainForAdmin();
                            Console.WriteLine("\n********************************************************************************************************************");
                            Console.WriteLine("Which Train do you want to modify");
                            ModifyTrain();
                            break;
                        case "3":
                            Console.WriteLine(" Please soft delete the train");
                            DeleteTrain();
                            Console.WriteLine("\n********************************************************************************************************************");
                            displaytrainForAdmin();
                            Console.WriteLine("\n********************************************************************************************************************");
                            break;
                        case "4":
                            Console.WriteLine("\nThanks for visiting this page");
                            break;
                        default:
                            Console.WriteLine("\nEnter valid number");
                            break;
                    }
                }
                else
                {
                    Console.WriteLine("\nInvalid admin credentials, you can't access the admincontrol.");
                    Console.ReadLine();
                    Environment.Exit(0);
                }
            }
        }
        //--------------------------------------User Control--------------------------------------------------------------------------------------------------------------------
        static void UserControl()
        {
            while (true)
            {
                Console.WriteLine("Now you can access all user authorization ");
                Console.WriteLine("Press 1 for Booking Trains");
                Console.WriteLine("Press 2 for Printing Tickets");
                Console.WriteLine("Press 3 for Cancel the tickets");
                Console.WriteLine("Press 4 for Printing Cancel Ticket");
                Console.WriteLine("Press 5 for Exit");
                Console.WriteLine("\n Enter your Choice");


                string res = Console.ReadLine();
                switch (res)
                {
                    case "1":
                        Console.WriteLine("Please Book the tickets");
                        displaytrainForUser();
                        DisplayAllTicketClassDetailsForUser();
                        BookingTicket();
                        DisplayAllBooking_Details();

                        break;
                    case "2":
                        Console.WriteLine("\nFor Printing ticket you have to give your booking id");
                        DisplayBooking_Details();
                        Console.WriteLine("\nHere is your printing tickets");
                        break;
                    case "3":
                        Console.WriteLine("\nPlease Cancel the tickets");
                        Cancel_Ticket();
                        DisplayAllCanceltDetails();
                        break;
                    case "4":
                        DisplayCancellationDetails();
                        Console.WriteLine(" Here is your print of cancel ticket");
                        break;
                    case "5":
                        Console.WriteLine("\nThanks for visting this page");
                        break;
                    default:
                        Console.WriteLine("\nEnter valid number");
                        break;
                }
            }
        }
        //--------------------------------------Add Train--------------------------------------------------------------------------------------------------------------------
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
                Console.WriteLine("Available seats:");
                TCD.AvailableSeats = int.Parse(Console.ReadLine());
                Console.WriteLine("Enter Fare Amount");
                TCD.Fare = int.Parse(Console.ReadLine());

                db.TicketClassDetails.Add(TCD);
                db.SaveChanges();
            }
        }

        static void displaytrainForAdmin()
        {
            var allTrains = db.Trains.ToList();

            // Display all trains including inactive ones for the admin
            Console.WriteLine("******************************************All Trains**********************************************************");
            foreach (var train in allTrains)
            {
                Console.WriteLine($"TrainNo: {train.TrainNo}, TrainName: {train.TrainName}, Source: {train.Source}, Destination: {train.Destination}, Status: {train.Status}");
                Console.WriteLine("***********************************************************************************************************************");

            }
        }

        static void DisplayAllTicketClassDetailsForAdmin()
        {
            try
            {
                var allTicketClassDetails = db.TicketClassDetails.ToList();

                Console.WriteLine("\n********************************************************************************************************************");
                Console.WriteLine("                                      ALL TICKET CLASS DETAILS                                                     ");
                Console.WriteLine("********************************************************************************************************************");

                Console.WriteLine("| Train No | Class Name | Total Seats | Available Seats | Fare Amount |");

                foreach (var ticketClassDetail in allTicketClassDetails)
                {
                    Console.WriteLine("| {0,-9} | {1,-10} | {2,-11} | {3,-16} | {4,-11} |",
                        ticketClassDetail.TrainNo, ticketClassDetail.ClassName, ticketClassDetail.TotalSeats,
                        ticketClassDetail.AvailableSeats, ticketClassDetail.Fare);
                }

                if (!allTicketClassDetails.Any())
                {
                    Console.WriteLine("\nNo ticket class details found.");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"\nAn error occurred: {ex.Message}");
            }
        }

        static void displaytrainForUser()
        {
            var activeTrains = db.Trains.Where(t => t.Status == "Active").ToList();

            // Display only active trains for the user
            Console.WriteLine("\n********************************************************************************************************************");
            Console.WriteLine("                                           Active Trains                                                             ");
            Console.WriteLine("********************************************************************************************************************");

            foreach (var train in activeTrains)
            {
                Console.WriteLine($"TrainNo: {train.TrainNo}, TrainName: {train.TrainName}, Source: {train.Source}, Destination: {train.Destination}, Status: {train.Status}");
                Console.WriteLine("***********************************************************************************************************************");

            }

        }
        static void DisplayAllTicketClassDetailsForUser()
        {
            try
            {
                // Fetch ticket class details for active trains using an inner join
                var allTicketClassDetails = from train in db.Trains
                                            join ticketClassDetail in db.TicketClassDetails
                                            on train.TrainNo equals ticketClassDetail.TrainNo
                                            where train.Status == "Active"
                                            select ticketClassDetail;

                if (allTicketClassDetails.Any())
                {
                    Console.WriteLine("\n********************************************************************************************************************");
                    Console.WriteLine("                                      AVAILABLE TICKET CLASS DETAILS                                               ");
                    Console.WriteLine("********************************************************************************************************************");

                    Console.WriteLine("| Train No | Class Name | Total Seats | Available Seats | Fare Amount |");

                    foreach (var ticketClassDetail in allTicketClassDetails)
                    {
                        Console.WriteLine("| {0,-9} | {1,-10} | {2,-11} | {3,-16} | {4,-11} |",
                            ticketClassDetail.TrainNo, ticketClassDetail.ClassName, ticketClassDetail.TotalSeats,
                            ticketClassDetail.AvailableSeats, ticketClassDetail.Fare);
                    }
                }
                else
                {
                    Console.WriteLine("\nNo ticket class details found.");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"\nAn error occurred: {ex.Message}");
            }
        }


        //--------------------------------------Modify Train--------------------------------------------------------------------------------------------------------------------

        public static void ModifyTrain()
        {
            Console.WriteLine("\nEnter Train Number:");
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
                Console.WriteLine("\nTrain details updated successfully.");
                Console.WriteLine("\nHere is the after modification table");
                displaytrainForAdmin();
                Console.ReadLine();
                Environment.Exit(0);
            }
            else
            {
                Console.WriteLine("Train with the provided number does not exist.");
                Admincontrol();
            }
        }

        //--------------------------------------Delete Train--------------------------------------------------------------------------------------------------------------------
        public static void DeleteTrain()
        {
            Console.WriteLine("\nEnter Train Number:");
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

            }
        }

        //--------------------------------------Booking Ticket--------------------------------------------------------------------------------------------------------------------
        public static void BookingTicket()
        {
            try
            {
                Console.Write("\nEnter Passenger Name: ");
                string passengerName = Console.ReadLine();

                Console.Write("Enter Train Number: ");
                int trainNo = Convert.ToInt32(Console.ReadLine());

                // Check if the train status is active
                var train = db.Trains.FirstOrDefault(t => t.TrainNo == trainNo && t.Status == "Active");
                if (train == null)
                {
                    Console.WriteLine("\nCannot book tickets for an inactive train.");
                    Console.WriteLine("You can only book ticket for active train \n So please enter valid train number");
                    return; // Exit the method if train is inactive
                }

                Console.Write("Enter Class Name: ");
                string className = Console.ReadLine();

                Console.Write("Enter Date of Travel (YYYY-MM-DD): ");
                DateTime dateOfTravel = Convert.ToDateTime(Console.ReadLine());

                Console.Write("Enter Number of Tickets: ");
                int numberOfTickets = Convert.ToInt32(Console.ReadLine());

                db.BookTrainTicket(passengerName, trainNo, className, dateOfTravel, numberOfTickets);
                db.SaveChanges();

                Console.WriteLine("Ticket booked successfully!");

            }
            catch (Exception ex)
            {
                Console.WriteLine("An error occurred: " + ex.Message);
            }
        }

        //--------------------------------------Cancel Ticket--------------------------------------------------------------------------------------------------------------------
        static void Cancel_Ticket()
        {
            Console.WriteLine("\nEnter Booking ID:");
            int bookingId = int.Parse(Console.ReadLine());

            Console.WriteLine("Enter Passenger Name:");
            string passengerName = Console.ReadLine();

            Console.WriteLine("Enter Train Number:");
            int trainNo = int.Parse(Console.ReadLine());

            Console.WriteLine("Enter Class Name:");
            string className = Console.ReadLine();

            Console.WriteLine("Enter Number of Tickets to Cancel:");
            int numberOfTickets = int.Parse(Console.ReadLine());

            try
            {
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
            Console.WriteLine("\nEnter Booking ID:");
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
        static void DisplayAllBooking_Details()
        {
            try
            {
                Console.WriteLine("****************************************************************************************************************");
                Console.WriteLine("                                    BOOKING DETAILS                              ");
                Console.WriteLine(" ***************************************************************************************************************** ");

                Console.WriteLine("| Booking ID | Train ID | Passenger Name | Class Name | Total Tickets | Booking Status | Book Amount |");

                foreach (var book in db.Bookings)
                {
                    Console.WriteLine("\n| {0,-10} | {1,-8} | {2,-10} | {3,-12} | {4,-14} | {5,-15} | {6,-14} |",
                        book.BookingId, book.TrainNo, book.PassengerName, book.ClassName,
                        book.NumberOfTickets, book.Status, book.TotalAmount);
                }

                if (!db.Bookings.Any())
                {
                    Console.WriteLine("\nNo bookings found.");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred: {ex.Message}");
            }
        }
        static void DisplayCancellationDetails()
        {
            try
            {
                Console.WriteLine("\nEnter Booking ID:");
                int bookingId = int.Parse(Console.ReadLine());

                var cancellation = db.Cancellations.FirstOrDefault(c => c.BookingId == bookingId);
                if (cancellation != null)
                {
                    Console.WriteLine("╔═════════════════════════════════════════╗");
                    Console.WriteLine("║           CANCELLATION DETAILS           ║");
                    Console.WriteLine("╠═════════════════════════════════════════╣");
                    Console.WriteLine($"║ Booking ID:       {cancellation.BookingId}║");
                    Console.WriteLine($"║ Passenger Name:   {cancellation.PassengerName}║");
                    Console.WriteLine($"║ Train Number:     {cancellation.TrainNo}║");
                    Console.WriteLine($"║ Class Name:       {cancellation.ClassName}║");
                    Console.WriteLine($"║ Number of Tickets:{cancellation.NumberOfTickets}║");
                    Console.WriteLine("╚═════════════════════════════════════════╝");
                }
                else
                {
                    Console.WriteLine("Cancellation details not found.");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred: {ex.Message}");
            }
        }
        static void DisplayAllCanceltDetails()
        {
            try
            {
                Console.WriteLine("\n******************************************************************************");
                Console.WriteLine("                          ALL CANCELED TICKET DETAILS                         ");
                Console.WriteLine("\n*****************************************************************************");

                Console.WriteLine("\n| Cancel ID |     Date of Cancel    | Train ID | No. of Tickets |  Booking ID |");

                foreach (var cancel in db.Cancellations)
                {
                    Console.WriteLine("| {0,-9} | {1,-15} | {2,-8} | {3,-14} | {4,-11} |",
                        cancel.CancellationId, cancel.DateOfCancel, cancel.TrainNo,
                        cancel.NumberOfTickets, cancel.BookingId);
                }

                if (!db.Cancellations.Any())
                {
                    Console.WriteLine("\nNo canceled tickets found.");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"\nAn error occurred: {ex.Message}");
            }
        }

    }


}