CREATE TABLE angajat (
  idangajat int NOT NULL IDENTITY(1,1),
  nume varchar(45) NOT NULL,
  prenume varchar(45) NOT NULL,
  parola int,
  PRIMARY KEY (idangajat)
) 

Select a.nume,a.parola from angajat a 
Join masa b ON a.idangajat = b.idangajat
where b.nrmasa = 1;
 

insert into masa(idangajat,nrmasa,Liber) 
SELECT idangajat,2,0 from angajat where nume = 'Irimia';

ALTER TABLE angajat
ALTER COLUMN parola int;

INSERT INTO angajat VALUES ('Irimia','Nicolae',0,'1234'),('Marinescu','Andrei',0,'1345'),('Matoi','Adrian',0,'1046');

Select* from angajat;

INSERT INTO masa VALUES (1,0);

Select* from masa
Delete from masa;

INSERT INTO masa (idangajat) select idangajat from angajat where nume='Irimia' ;

DELETE FROM masa
DBCC CHECKIDENT ('[masa]', RESEED, 0)
GO

Select Count(nrmasa) from masa where nrmasa = 1

Insert into plata Values (1,'29-JUN-04',25);

select* from plata;

drop table produse

select * from masa

CREATE TABLE produse (
  idproduse int NOT NULL IDENTITY(1,1),
  nume varchar(45) NOT NULL,
  pret int NOT NULL,
  tip varchar(45) NOT NULL,
  instock int NOT NULL,
  PRIMARY KEY (idproduse)
)

INSERT INTO produse VALUES('fanta',5,'racoritoare',100),('cola',5,'racoritoare',100),('sprite',5,'racoritoare',100),('stella',6,'bere',100),('holsten',6,'bere',100);
Insert into produse values('peroni',5,'bere',100),('timisoreana',5,'bere',100);
insert into produse values('pizza 4formagi',15,'pizza',100),('pizza peperoni',25,'pizza',100),('pizza margerita',14,'pizza',100),('pizza bacon',15,'pizza',100);
insert into produse values('ciorba de burta',6,'ciorba',100),('ciorba de fasole',6,'ciorba',100),('ciorba de vacuta',6,'ciorba',100),('ciorba de perisoare',6,'ciorba',100);

select COUNT(*) from produse group by tip
select * from produse
update produse
set pret = 16
where nume = 'pizza 4formagi'

CREATE TABLE masa (
  idmasa int NOT NULL IDENTITY(1,1),
  idangajat int NOT NULL ,
  Liber int NOT NULL,
  PRIMARY KEY (idmasa),
  CONSTRAINT fk_masa_angajat FOREIGN KEY (idangajat) REFERENCES angajat (idangajat) ON DELETE CASCADE ON UPDATE CASCADE
) 



CREATE TABLE comanda (
  idcomanda int NOT NULL IDENTITY(1,1),
  idmasa int NOT NULL ,
  PRIMARY KEY (idcomanda),
  CONSTRAINT fk_comanda_masa FOREIGN KEY (idmasa) REFERENCES masa (idmasa) ON DELETE CASCADE ON UPDATE CASCADE
) 

CREATE TABLE plata (
  idplata int NOT NULL IDENTITY(1,1),
  idmasa int NOT NULL ,
  Datat date NOT NULL,
  Ora time NOT NULL,
  PRIMARY KEY (idplata),
  CONSTRAINT fk_plata_masa FOREIGN KEY (idmasa) REFERENCES masa (idmasa) ON DELETE CASCADE ON UPDATE CASCADE
) 

CREATE TABLE detalii_plata (
  iddetalii int NOT NULL IDENTITY(1,1),
  idcomanda int NOT NULL ,
  idproduse int NOT NULL ,
  cantitate int NOT NULL,
  pret int NOT NULL,
  PRIMARY KEY (idDetalii),
  CONSTRAINT fk_detalii_plata_comanda FOREIGN KEY (idcomanda) REFERENCES comanda (idcomanda) ON DELETE NO ACTION,
  CONSTRAINT fk_detalii_plata_product FOREIGN KEY (idproduse) REFERENCES produse (idproduse) ON DELETE CASCADE ON UPDATE CASCADE
 )

 
 drop table produse;
 drop table angajat;
 drop table masa;
 drop table comanda;
 drop table plata;
 drop table detalii_plata;
 
 
 
 alter table masa drop constraint fk_masa_angajat
 alter table comanda drop constraint fk_comanda_masa
 alter table plata drop constraint fk_plata_masa
 alter table detalii_plata drop constraint fk_detalii_plata_comanda
 alter table detalii_plata drop constraint fk_detalii_plata_product

 alter table detalii_plata add CONSTRAINT fk_detalii_plata_product FOREIGN KEY (idproduse) REFERENCES produse (idproduse) ON DELETE CASCADE ON UPDATE CASCADE

 ALTER TABLE masa
DROP COLUMN Liber;

Alter table masa
ADD COLUMN Liber int NOT NULL;
select nume,pret,tip,instock from produse;
select* from masa;
drop table produse;

select* from plata

UPDATE  masa
SET     Liber = 1
WHERE nrmasa = 9
GO

select * from masa






insert into detalii_plata(idcomanda,idproduse) 
Select
	(Select max(idcomanda) as idcomada from comanda) , 
	(select idproduse as idproduse from produse where nume = 'fanta') ;
	

select * from detalii_plata






Select * from masa



insert into comanda select idmasa from masa where nrmasa=4



Select max(idcomanda) from comanda

DELETE FROM angajat
DBCC CHECKIDENT ('[angajat]', RESEED, 0)
GO

DELETE FROM produse
DBCC CHECKIDENT ('[produse]', RESEED, 0)
GO


DELETE FROM masa
DBCC CHECKIDENT ('[masa]', RESEED, 0)
GO


DELETE FROM comanda
DBCC CHECKIDENT ('[comanda]', RESEED, 0)
GO

DELETE FROM detalii_plata
DBCC CHECKIDENT ('[detalii_plata]', RESEED, 0)
GO

DELETE FROM plata
DBCC CHECKIDENT ('[plata]', RESEED, 0)
GO




Select a.idplata,c.idangajat,c.nume
from plata a Join masa b ON (a.idmasa = b.idmasa)
Join angajat c ON(b.idangajat = c.idangajat) where b.idmasa = 1;


Select SUM(pret) from produse p
JOIN detalii_plata d ON(p.idproduse = d.idproduse)
JOIN comanda c ON(d.idcomanda = c.idcomanda) 
JOIN masa m ON(c.idmasa = m.idmasa) where m.nrmasa = 8;

Select COUNT(p.idproduse) from produse p
JOIN detalii_plata d ON(p.idproduse = d.idproduse)
JOIN comanda c ON(d.idcomanda = c.idcomanda) 
JOIN masa m ON(c.idmasa = m.idmasa) where m.nrmasa = 9;


select* from masa;

select * from detalii_plata;

select * from comanda

select * from produse;

select * from plata

insert into detalii_plata(idcomanda,idproduse) 
Select
	(Select max(idcomanda) as idcomada from comanda) , 
	(select idproduse as idproduse from produse where nume = 'fanta') ;



INSERT INTO plata(idmasa,Datat,Ora,Suma)
Select
	(Select idmasa from masa where nrmasa = 9),
	(Select GETDATE()),
	(SELECT CONVERT(VARCHAR(8),GETDATE(),108)),
	(Select SUM(pret) from produse p
		JOIN detalii_plata d ON(p.idproduse = d.idproduse)
		JOIN comanda c ON(d.idcomanda = c.idcomanda) 
		JOIN masa m ON(c.idmasa = m.idmasa) where m.nrmasa = 9);
		 

select * from plata;

select * from masa;

select * from comanda;

select * from angajat

select * from produse

select Top 1 
	Count(Liber),Liber from masa where nrmasa = 4 Group By Liber

	select Count(Liber),Liber from masa where nrmasa = 1 and Liber = 0  Group By Liber 

Update angajat
set parola='1000'
where nume = 'Maldini'

DELETE FROM angajat where idangajat = 1
DBCC CHECKIDENT ('[angajat]', RESEED, 0)
GO


Select c.idcomanda as idcomada from comanda c JOIN masa m ON(m.idmasa = c.idmasa) Where m.nrmasa = 1 and Liber = 0

Select a.nume,m.nrmasa,p.suma from angajat a
Join masa m ON(m.idangajat = a.idangajat)
Join plata p ON(p.idmasa = m.idmasa)
where a.nume = 'Irimia'

Select a.nume,m.nrmasa,p.suma from angajat a
Join masa m ON(m.idangajat = a.idangajat)
Join plata p ON(p.idmasa = m.idmasa)
where a.nume = 'Marinescu'

Select a.nume,p.suma,p.datat,p.ora from angajat a
Join masa m ON(m.idangajat = a.idangajat)
Join plata p ON(p.idmasa = m.idmasa)
Order by p.suma DESC
where a.nume = 'Matoi'

select * from plata;

UPDATE  masa
SET     Liber = 1
WHERE nrmasa = 1 and Liber = 0


SELECT nume FROM produse where tip = 'racoritoare' and instock > 1;

select* from produse

Update produse
Set instock = ( select instock from produse where nume = 'holsten') + 2
where nume = 'holsten';

Update produse
Set instock = 100, pret = 10
where nume = 'red bull'


select * from angajat

update angajat
SET parola = CAST(NULL AS INT)
where nume = 'Irimia' and prenume = '';

Select Count(nrmasa) from masa where nrmasa = 1 and Liber = 0

SELECT nume,parola FROM angajat where parola IS NOT NULL;


select * from angajat

select count(idangajat) from angajat
where nume = 'Irimia' and prenume = 'Nicolae' and parola IS NULL

update angajat
set nume = 'Irimia', prenume = 'Nicolae', parola = '1234'
where nume = 'Irimia'and prenume = 'Nicolae' and parola IS NULL

select nume,prenume from angajat
where parola is not null

INSERT INTO angajat VALUES ('Gatuzo','Genaro',1945)

select pr.nume,pr.pret from produse pr
JOIN detalii_plata d ON(pr.idproduse = d.idproduse)
JOIN comanda c ON(d.idcomanda = c.idcomanda) 
JOIN masa m ON(c.idmasa = m.idmasa) 
join plata p ON(m.idmasa = p.idmasa)
where p.ora = '1:55:09'


Select Concat(a.nume, ' ',a.prenume) as nume,p.suma,p.datat,p.ora from angajat a
Join masa m ON(m.idangajat = a.idangajat)
Join plata p ON(p.idmasa = m.idmasa)
Order by datat,ora DESC

select count(idangajat) from angajat
where nume = 'Marcu' and prenume = 'Denis' and parola IS NULL;