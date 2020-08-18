ALTER TABLE dbo.s_type_org ADD isActive bit not null default 1 
ALTER TABLE dbo.s_type_org ADD id_Editor INT NULL
ALTER TABLE dbo.s_type_org ADD DateEdit DATETIME NULL


ALTER TABLE dbo.[s_Subjects] ADD isActive bit not null default 1 
ALTER TABLE dbo.[s_Subjects] ADD id_Editor INT NULL
ALTER TABLE dbo.[s_Subjects] ADD DateEdit DATETIME NULL

INSERT INTO dbo.prog_config (id_prog,id_value,type_value,value_name,value,comment)  VALUES (118,'ciru','C','Код для России',460,'')



ALTER TABLE dbo.[s_Proizvoditel] ADD isActive bit not null default 1 
ALTER TABLE dbo.[s_Proizvoditel] ADD id_Editor INT NULL
ALTER TABLE dbo.[s_Proizvoditel] ADD DateEdit DATETIME NULL


ALTER TABLE dbo.[s_adress_proizvod] ADD isActive bit not null default 1 
ALTER TABLE dbo.[s_adress_proizvod] ADD id_Editor INT NULL
ALTER TABLE dbo.[s_adress_proizvod] ADD DateEdit DATETIME NULL