CREATE TABLE Cafe (
    Id UNIQUEIDENTIFIER PRIMARY KEY DEFAULT NEWID(),
    Name NVARCHAR(100) NOT NULL,
    Description NVARCHAR(255) NOT NULL,
    Logo NVARCHAR(255) NULL,
    Location NVARCHAR(100) NOT NULL
);


CREATE TABLE Employee (
    Id CHAR(9) PRIMARY KEY, -- Format 'UIXXXXXXX'
    Name NVARCHAR(100) NOT NULL,
    Email_Address NVARCHAR(255) NOT NULL UNIQUE,
    Phone_Number CHAR(8) NOT NULL CHECK (Phone_Number LIKE '[89]_______'), -- Starts with 8 or 9 and 8 digits long
    Gender NVARCHAR(10) NOT NULL CHECK (Gender IN ('Male', 'Female'))
);


CREATE TABLE EmployeeCafe (
    CafeId UNIQUEIDENTIFIER NOT NULL,
    EmployeeId CHAR(9) NOT NULL,
    StartDate DATETIME NOT NULL,
    PRIMARY KEY (CafeId, EmployeeId),
    FOREIGN KEY (CafeId) REFERENCES Cafe(Id),
    FOREIGN KEY (EmployeeId) REFERENCES Employee(Id),
    CONSTRAINT UC_EmployeeCafe UNIQUE (EmployeeId) -- Prevents the same employee from working in multiple cafés
);


INSERT INTO Cafe (Name, Description, Logo, Location) VALUES
( 'Arabica Cafe', 'The most popular coffee type in the world', 'Arabica.png', 'Location1'),
('Tea Emporium', 'Your destination for exotic teas', NULL, 'Location2'),
('Caffe latte', 'Coffee drink of Italian origin made with espresso and steamed milk', 'latte.png', 'Location3');


INSERT INTO Employee (Id, Name, Email_Address, Phone_Number, Gender) VALUES
('UI1234567', 'Saroj Jha', 'srojkrjha@gmail.com', '91234567', 'Male'),
('UI2345678', 'Reena Jha', 'reenajha@gmail.com', '82345678', 'Female'),
('UI3456789', 'Kritika Jha', 'kreetikajha@gmail.com', '83456789', 'Female'),
('UI4567890', 'Rishav Jha', 'rishavjha@gmail.com', '94345678', 'Male');


INSERT INTO EmployeeCafe (CafeId, EmployeeId, StartDate) VALUES
((SELECT Id FROM Cafe WHERE Name = 'Arabica Cafe'), 'UI1234567', '2024-01-15'),
((SELECT Id FROM Cafe WHERE Name = 'Tea Emporium'), 'UI2345678', '2024-02-01'),
((SELECT Id FROM Cafe WHERE Name = 'Caffe latte'), 'UI3456789', '2024-03-10'),
((SELECT Id FROM Cafe WHERE Name = 'Caffe latte'), 'UI4567890', '2024-03-22');



SELECT * FROM Employee WHERE Phone_Number NOT LIKE '[89]_______';

SELECT EmployeeId, COUNT(CafeId)
FROM EmployeeCafe
GROUP BY EmployeeId
HAVING COUNT(CafeId) > 1;