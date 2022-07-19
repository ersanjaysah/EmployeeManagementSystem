
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
@MobileNumber Bigint,
@Address Varchar(225),
@Gender Varchar(225),
@Position Varchar(225),
@Salary Bigint
)
as
Begin
		insert Employee
		values (@FullName, @Email, @Password, @MobileNumber, @Address, @Gender, @Position, @Salary) 
end;

---this SP is for login
create procedure SPEmpLogin
(
@Email varchar(255),
@Password varchar(255)
)
as
begin
select * from Employee
where Email = @Email and Password = @Password
End;
