create Database EmployeeManagementSystem;

---creating table
create Table Admins
(
	AdminId int Identity(1,1) primary key not null,
	FullName varchar(255) not null,
	Email varchar(255) not null,
	Password varchar(255) not null,
	MobileNumber varchar(50) not null,
	Address varchar(50) not null,
);

select * from Admins


---Store procedure of Admin
INSERT INTO Admins VALUES ('Admin Sanjay','sanjay@admin.com', 'Admin@123', '+91 7209637857','pune');

Create Proc SPLoginAdmin
(
	@Email varchar(max),
	@Password varchar(max)
)
as
BEGIN
	If(Exists(select * from Admins where Email= @Email and Password = @Password))
		Begin
			select * from Admins where Email= @Email and Password = @Password;
		end
	Else
		Begin
			select 2;
		End
END;


