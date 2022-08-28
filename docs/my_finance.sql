create database my_finance;
use my_finance;

create table account_plans(
	id int identity(1, 1) not null,
	description varchar(50) not null,
	type char(1) not null,
	primary key(id)
);

create table transactions(
	id bigint identity(1, 1) not null,
	date date not null,
	value decimal(18, 2) not null,
	type char(1) not null,
	history text,
	account_plan_id int not null,
	primary key(id),
	foreign key(account_plan_id) references account_plans
);

insert into account_plans(description, type) values('Aluguel', 'D');
insert into account_plans(description, type) values('Alimentação', 'D');
insert into account_plans(description, type) values('Combustível', 'D');
insert into account_plans(description, type) values('Viagens', 'D');
insert into account_plans(description, type) values('Salário', 'C');