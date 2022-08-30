create database my_finance;
use my_finance;

create table account_plan_types(
	id int identity(1, 1) not null,
	code char(1) not null,
	description varchar(30) not null
	primary key(id)
);

create table account_plans(
	id int identity(1, 1) not null,
	description varchar(50) not null,
	account_plan_type_id int not null,
	primary key(id),
	foreign key (account_plan_type_id) references account_plan_types
);

create table transactions(
	id bigint identity(1, 1) not null,
	date date not null,
	value decimal(18, 2) not null,
	description text,
	account_plan_id int not null,
	primary key(id),
	foreign key(account_plan_id) references account_plans
);

insert into account_plan_types(code, description) values ('C', 'Crédito');
insert into account_plan_types(code, description) values ('D', 'Débito');

insert into account_plans(description, account_plan_type_id) values('Aluguel', (select id from account_plan_types where code = 'C'));
insert into account_plans(description, account_plan_type_id) values('Alimentação', (select id from account_plan_types where code = 'D'));
insert into account_plans(description, account_plan_type_id) values('Combustível', (select id from account_plan_types where code = 'D'));
insert into account_plans(description, account_plan_type_id) values('Viagens', (select id from account_plan_types where code = 'D'));
insert into account_plans(description, account_plan_type_id) values('Salário', (select id from account_plan_types where code = 'C'));