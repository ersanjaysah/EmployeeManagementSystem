
---create table of Employee


Create table Employee
( 
EmpId int identity(1,1) Primary key,
FullName Varchar(225) not null,
Email Varchar(225) not null unique,
Password varchar(225) not null,
MobileNumber bigint not null,
Address Varchar(225) not null,
Gender Varchar(225) not null,
Position Varchar(225) not null,
Salary bigint not null

)

select *from Employee

Create procedure SPEmpRegistration
(
@FullName varchar(255),
@Email varchar(255),
@Password Varchar(255),
@MobileNumber bigint,
@Address Varchar(225),
@Gender Varchar(225),
@Position Varchar(225),
@Salary bigint
)
as
Begin
		insert Employee
		values (@FullName, @Email, @Password, @MobileNumber, @Address, @Gender, @Position, @Salary) 
end;

--procedure to updateEmployee
create procedure SPUpdateEmployee
(
@EmpId int,
@FullName varchar(255),
@Email varchar(255),
@Password Varchar(255),
@MobileNumber Bigint,
@Address Varchar(225),
@Gender Varchar(225),
@Position Varchar(225),
@Salary Bigint
)
as
BEGIN
Update Employee set FullName = @FullName, 
Email = @Email,
Password = @Password,
MobileNumber = @MobileNumber,
Address= @Address,
Gender = @Gender,
Position = @Position,
Salary = @Salary
where EmpId = @EmpId;
End;

---Procedure to deleteEmployee
create procedure SPDeleteEmployee
(
@EmpId int
)
as
BEGIN
Delete Employee 
where EmpId = @EmpId;
End;
