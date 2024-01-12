create database Library

create table users(
id int not null identity(1,1) primary key,
username varchar(30) not null,
password varchar(30) not null,
userType varchar(30) not null,
)

insert into users(username,password,userType) values('admin','123','Admin');

insert into users(username,password,userType) values('ali','1234','client');


create table NewBook(
bName varchar(40) unique not null,
bAuthor varchar(40) not null,
bPubl varchar(40) not null,
bPDate varchar(100) not null,
bQuantity bigint not null,
)




create table Student(
sID int not null identity(1,1) primary key,
sName varchar(40) not null,
entroll varchar(40) unique not null,
depart varchar(40) not null,
sem varchar(40) not null,
contact bigint not null,
email varchar(40) not null,
)


create table IRBook(
sID int not null identity(1,1) primary key,
std_entroll varchar(40) not null,
std_name varchar(40) not null,
std_depart varchar(40) not null,
std_sem varchar(40) not null,
std_contact bigint not null,
std_email varchar(40) not null,
book_name varchar(40) not null,
book_issue_date varchar(100) not null,
book_return_date varchar(100),
)


