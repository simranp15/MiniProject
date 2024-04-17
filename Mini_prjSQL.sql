create database MiniProjectTrain
use MiniProjectTrain

------------------- Creating Tables----------------------------------------------
CREATE TABLE Trains (
    TrainNo INT PRIMARY KEY,
    TrainName VARCHAR(100) NOT NULL,
    Source VARCHAR(100) NOT NULL,
    Destination VARCHAR(100) NOT NULL, 
    Status VARCHAR(20) CHECK (Status IN ('Active', 'Inactive')) DEFAULT 'Active'
);
------------------------------------------------------------
create table TicketClassDetail(
ClassId int identity(1,1) primary key,
TrainNo INT,
ClassName Varchar(50),
TotalSeats INT NOT NULL,
AvailableSeats INT NOT NULL,
Fare DECIMAL(10,2)
FOREIGN KEY (TrainNo) REFERENCES Trains(TrainNo),
);
select*from TicketClassDetail

----------------------------------------------------------------------------
CREATE TABLE Admins (
    AdminId INT PRIMARY KEY,
    Password VARCHAR(50) NOT NULL
);

INSERT INTO Admins(AdminId, Password)
VALUES (1505, 'Shrija');

CREATE TABLE Bookings (
    BookingId int identity(1,1) primary key,
    PassengerName varchar(50),
    TrainNo INT NOT NULl,
    ClassName VARCHAR(50) NOT NULL,
    DateOfTravel DATETIME NOT NULL,
    NumberOfTickets INT NOT NULL,
    TotalAmount DECIMAL(10, 2) ,
	Status VARCHAR(20) CHECK (Status IN ('Active', 'Inactive')) DEFAULT 'Active'
    FOREIGN KEY (TrainNo) REFERENCES Trains(TrainNo)
);

CREATE TABLE Cancellation (
    CancellationId  int  IDENTITY(1,1) PRIMARY KEY,
    BookingId INT ,
	PassengerName varchar(30),
    TrainNo INT ,
    ClassName varchar(30) ,
    NumberOfTickets INT,
    DateOfCancel DATETIME DEFAULT GETDATE(),
    FOREIGN KEY (BookingId) REFERENCES bookings(BookingId),
    FOREIGN KEY (TrainNo) REFERENCES Trains(TrainNo)
	);

----------------------------------------------------------------------------------------------------------------------------------
---------------------------Creating  Procedure-------------------------------------------------------------

CREATE OR ALTER PROCEDURE AddTrain
    @TrainNo INT,
    @TrainName VARCHAR(100),
    @Source VARCHAR(100),
	@Destination varchar(100)
   
AS
BEGIN
    -- Insert the train details
    INSERT INTO Trains(TrainNo, TrainName, Source, Destination)
    VALUES (@TrainNo, @TrainName, @Source, @Destination );

END;
Exec AddTrain @TrainNo=12332, @TrainName=Vande_Bharat, @Source=Delhi, @Destination=Ludhiyana
select* from Trains
---------------------------------------------------------------------------

CREATE OR ALTER PROCEDURE AddTicketClassDetail 
@TrainNo int,
@ClassName Varchar(50),
@TotalSeats int ,
@AvailableSeats int,
@Fare DECIMAL(10,2)  
As
BEGIN
    -- Insert the train details
    INSERT INTO TicketClassDetail(TrainNo,ClassName, TotalSeats,AvailableSeats,Fare)
    VALUES (@TrainNo,@ClassName, @TotalSeats,@AvailableSeats,@Fare);
END;

-------------------------------------------

create or alter proc DisplayTrain
as
begin
select*from Trains
end 
exec DisplayTrain

--------------------------------------------
select* from Bookings
drop table Cancellation
select*from Cancellation
drop proc CancelTicket
------------------------------------------
CREATE OR ALTER PROCEDURE BookTrainTicket
    @PassengerName VARCHAR(50),
    @TrainNo INT,
    @ClassName VARCHAR(50),
    @DateOfTravel DATETIME,
    @NumberOfTickets INT
AS
BEGIN
    -- Calculate booking amount
    DECLARE @TicketFare DECIMAL(10, 2);
    DECLARE @TotalAmount DECIMAL(10, 2);

    SELECT @TicketFare = Fare
    FROM TicketClassDetail
    WHERE TrainNo = @TrainNo AND ClassName = @ClassName;

    SET @TotalAmount = @TicketFare * @NumberOfTickets;

    -- Check if the train and berth class seats are available
    DECLARE @AvailableSeats INT;

    SELECT @AvailableSeats = AvailableSeats
    FROM TicketClassDetail
    WHERE TrainNo = @TrainNo AND ClassName = @ClassName;

    IF @AvailableSeats >= @NumberOfTickets
    BEGIN
        -- Update available seats
        UPDATE TicketClassDetail
        SET AvailableSeats = AvailableSeats - @NumberOfTickets
        WHERE TrainNo = @TrainNo AND ClassName = @ClassName;

        -- Insert booking details
        INSERT INTO Bookings(PassengerName, TrainNo, ClassName, DateOfTravel, NumberOfTickets, TotalAmount, Status)
        VALUES (@PassengerName, @TrainNo, @ClassName, @DateOfTravel, @NumberOfTickets, @TotalAmount, 'Active');
    END
END
select*from Trains
Select*from Bookings
EXEC BookTrainTicket 
    @PassengerName = 'Shreya',
    @TrainNo = 12332,
    @ClassName = 'Second class',
    @DateOfTravel = '2024-04-20',
    @NumberOfTickets = 2

	---------------------------------------------------------------------------

	CREATE OR ALTER PROCEDURE CancelTicket
    @BookingId INT,
    @PassengerName VARCHAR(30),
    @TrainNo INT,
    @ClassName VARCHAR(30),
    @NumberOfTickets INT
    

AS
BEGIN
    -- Update available seats in CLASS_DETAILS
    UPDATE TicketClassDetail
    SET AVAILABLESEATS = AVAILABLESEATS + @NumberOfTickets
    WHERE TRAINNO = @TrainNo;
	
	 UPDATE Bookings
    SET Status = 'Inactive'
    WHERE BookingId = @BookingId;
    -- Insert cancellation details into Cancellation table
    INSERT INTO Cancellation (BookingId, PassengerName, TrainNo, ClassName, NumberOfTickets)
    VALUES (@BookingId, @PassengerName, @TrainNo, @ClassName, @NumberOfTickets);
END;

EXEC CancelTicket 
    @BookingId = 1,
    @PassengerName = Sangeeta,
    @TrainNo = 43212,
    @ClassName = FirstClass,
    @NumberOfTickets = 2;
	select*from Bookings
	select*from TicketClassDetail
-----------------------------------
-------------------------------------------------------------------------------


