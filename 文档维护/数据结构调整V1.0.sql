IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[data_dict]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[data_dict](
	[table_name] [VARCHAR](50) NULL,
	[table_name_c] [VARCHAR](150) NULL,
	[field_name] [VARCHAR](50) NULL,
	[field_name_c] [VARCHAR](50) NULL,
	[field_sequence] [SMALLINT] NULL,
	[data_type] [VARCHAR](16) NULL,
	[width] [SMALLINT] NULL,
	[id] [INT] IDENTITY(1,1) NOT NULL,
	[GUID] [UNIQUEIDENTIFIER] NULL,
	[TableGUID] [UNIQUEIDENTIFIER] NULL,
	[B_PK] [TINYINT] NULL,
	[XmlAbreviation] [VARCHAR](16) NULL,
	[ValidForReadAPI] [TINYINT] NULL,
	[ValidForCreateAPI] [TINYINT] NULL,
	[ValidForUpdateAPI] [TINYINT] NULL,
	[DisplayMask] [INT] NULL,
	[ReferencedEntityObjectTypeCode] [INT] NULL,
	[Description] [VARCHAR](1000) NULL,
	[b_sys] [TINYINT] NULL,
	[defaultvalue] [VARCHAR](50) NULL,
	[attributetype] [VARCHAR](20) NULL,
	[LookupType] [VARCHAR](30) NULL,
	[DisableFilter] [TINYINT] NULL,
	[b_null] [TINYINT] NULL,
	[b_identity] [INT] NULL,
	[b_identify] [INT] NULL,
 CONSTRAINT [PK_data_dict] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
END
GO


-------------------------------------------------【BEGIN】新增ds_User表------------------------------------------------- 
IF OBJECT_ID('ds_User') IS NULL 
BEGIN 
     CREATE TABLE ds_User([UserGUID] [UNIQUEIDENTIFIER] PRIMARY KEY
          ,[UserName] [VARCHAR](200) 
          ,[UserCode] [VARCHAR](200) 
          ,[PassWord] [VARCHAR](200) 
          ,[MobilePhoto] [VARCHAR](200) 
     )
END
GO 
delete from data_dict where table_name='ds_User' ; insert into data_dict(guid , tableguid , field_name_c , field_name , width ,data_type , field_sequence, b_null , b_pk , b_sys , table_name ,	table_name_c , Description , defaultvalue , b_identity) values  ('d35c5064-a94c-4e78-bb3d-b6987a1854c4' , '100a613b-3503-48f8-b9e5-8f6ad775a793','用户GUID','UserGUID',16,'uniqueidentifier',1,0,0,0,'ds_User','用户表','','',0) ; insert into data_dict(guid , tableguid , field_name_c , field_name , width ,data_type , field_sequence, b_null , b_pk , b_sys , table_name ,	table_name_c , Description , defaultvalue , b_identity) values  ('4d724f1c-c901-4e48-8d97-915908969a46' , '100a613b-3503-48f8-b9e5-8f6ad775a793','用户名称','UserName',200,'varchar',2,0,0,0,'ds_User','用户表','','',0) ; insert into data_dict(guid , tableguid , field_name_c , field_name , width ,data_type , field_sequence, b_null , b_pk , b_sys , table_name ,	table_name_c , Description , defaultvalue , b_identity) values  ('d9d1e0f5-c9b0-4bb8-8ad9-f03c5fa42275' , '100a613b-3503-48f8-b9e5-8f6ad775a793','用户编码','UserCode',200,'varchar',3,0,0,0,'ds_User','用户表','','',0) ; insert into data_dict(guid , tableguid , field_name_c , field_name , width ,data_type , field_sequence, b_null , b_pk , b_sys , table_name ,	table_name_c , Description , defaultvalue , b_identity) values  ('f5c1cf4f-a0fb-4b5b-9a55-9b8d37e7fdab' , '100a613b-3503-48f8-b9e5-8f6ad775a793','密码','PassWord',200,'varchar',4,0,0,0,'ds_User','用户表','','',0) ; insert into data_dict(guid , tableguid , field_name_c , field_name , width ,data_type , field_sequence, b_null , b_pk , b_sys , table_name ,	table_name_c , Description , defaultvalue , b_identity) values  ('5e321737-006a-4f9e-9292-56a6599c07a4' , '100a613b-3503-48f8-b9e5-8f6ad775a793','手机号码','MobilePhoto',200,'varchar',5,0,0,0,'ds_User','用户表','','',0) ; 
GO 
-------------------------------------------------【END】新增ds_User表------------------------------------------------- 
 
 
-------------------------------------------------【BEGIN】新增ds_Renter表------------------------------------------------- 
IF OBJECT_ID('ds_Renter') IS NULL 
BEGIN 
     CREATE TABLE ds_Renter([RenterGUID] [UNIQUEIDENTIFIER] PRIMARY KEY
          ,[RenterCode] [VARCHAR](200) 
          ,[RenterName] [VARCHAR](200) 
     )
END
GO 
 
delete from data_dict where table_name='ds_Renter' ; insert into data_dict(guid , tableguid , field_name_c , field_name , width ,data_type , field_sequence, b_null , b_pk , b_sys , table_name ,	table_name_c , Description , defaultvalue , b_identity) values  ('1a730db9-a7e1-486e-90bb-594426d1ed8c' , '100a613b-3503-48f8-b9e5-8f6ad775a793','租户GUID','RenterGUID',16,'uniqueidentifier',1,0,0,0,'ds_Renter','租户表','','',0) ; insert into data_dict(guid , tableguid , field_name_c , field_name , width ,data_type , field_sequence, b_null , b_pk , b_sys , table_name ,	table_name_c , Description , defaultvalue , b_identity) values  ('8723f0a2-6999-44ae-83d6-ce5c5b34606a' , '100a613b-3503-48f8-b9e5-8f6ad775a793','租户编码','RenterCode',200,'varchar',2,0,0,0,'ds_Renter','租户表','','',0) ; insert into data_dict(guid , tableguid , field_name_c , field_name , width ,data_type , field_sequence, b_null , b_pk , b_sys , table_name ,	table_name_c , Description , defaultvalue , b_identity) values  ('e2483442-98c6-43bd-a86b-a74e91ee1f63' , '100a613b-3503-48f8-b9e5-8f6ad775a793','租户名称','RenterName',200,'varchar',3,0,0,0,'ds_Renter','租户表','','',0) ; 
GO 
-------------------------------------------------【END】新增ds_Renter表------------------------------------------------- 
 
 
-------------------------------------------------【BEGIN】新增ds_Template表------------------------------------------------- 
IF OBJECT_ID('ds_Template') IS NULL 
BEGIN 
     CREATE TABLE ds_Template([TemplateGUID] [UNIQUEIDENTIFIER] PRIMARY KEY
          ,[TemplateName] [VARCHAR](200) 
          ,[TemplateMemo] [VARCHAR](1000) 
          ,[TemplateIcon] [VARCHAR](1000) 
          ,[TemplatePicSrc] [VARCHAR](1000) 
     )
END
GO 
 
 
delete from data_dict where table_name='ds_Template' ; insert into data_dict(guid , tableguid , field_name_c , field_name , width ,data_type , field_sequence, b_null , b_pk , b_sys , table_name ,	table_name_c , Description , defaultvalue , b_identity) values  ('d10ecb15-8ed4-4f94-9489-e55ac07d2e05' , '100a613b-3503-48f8-b9e5-8f6ad775a793','模板GUID','TemplateGUID',16,'uniqueidentifier',1,0,0,0,'ds_Template','模板表','','',0) ; insert into data_dict(guid , tableguid , field_name_c , field_name , width ,data_type , field_sequence, b_null , b_pk , b_sys , table_name ,	table_name_c , Description , defaultvalue , b_identity) values  ('a9425438-f688-418f-88d2-36525a5281c8' , '100a613b-3503-48f8-b9e5-8f6ad775a793','模板名称','TemplateName',200,'varchar',2,0,0,0,'ds_Template','模板表','','',0) ; insert into data_dict(guid , tableguid , field_name_c , field_name , width ,data_type , field_sequence, b_null , b_pk , b_sys , table_name ,	table_name_c , Description , defaultvalue , b_identity) values  ('6f82d16d-32ee-4a77-a7f7-2921d5330c6f' , '100a613b-3503-48f8-b9e5-8f6ad775a793','模板描述','TemplateMemo',1000,'varchar',3,0,0,0,'ds_Template','模板表','','',0) ; insert into data_dict(guid , tableguid , field_name_c , field_name , width ,data_type , field_sequence, b_null , b_pk , b_sys , table_name ,	table_name_c , Description , defaultvalue , b_identity) values  ('abc50d01-9f28-4592-bf11-30ca356e1ffc' , '100a613b-3503-48f8-b9e5-8f6ad775a793','模板小图标','TemplateIcon',1000,'varchar',4,0,0,0,'ds_Template','模板表','','',0) ; insert into data_dict(guid , tableguid , field_name_c , field_name , width ,data_type , field_sequence, b_null , b_pk , b_sys , table_name ,	table_name_c , Description , defaultvalue , b_identity) values  ('cd66d99a-ec33-43bf-8ebf-19e56da6cba4' , '100a613b-3503-48f8-b9e5-8f6ad775a793','模板样例图地址','TemplatePicSrc',1000,'varchar',5,0,0,0,'ds_Template','模板表','','',0) ; 
GO 

-------------------------------------------------【END】新增ds_Template表------------------------------------------------- 
 
 
-------------------------------------------------【BEGIN】新增ds_TemplatePage表------------------------------------------------- 
IF OBJECT_ID('ds_TemplatePage') IS NULL 
BEGIN 
     CREATE TABLE ds_TemplatePage([TemplatePageGUID] [UNIQUEIDENTIFIER] PRIMARY KEY
          ,[TemplateGUID] [UNIQUEIDENTIFIER] 
          ,[PageNum] [INT] 
          ,[JsonExample] [VARCHAR](MAX) 
          ,[PageMemo] [VARCHAR](1000) 
          ,[PagePicSrc] [VARCHAR](1000) 
     )
END
GO 
 

delete from data_dict where table_name='ds_TemplatePage' ; insert into data_dict(guid , tableguid , field_name_c , field_name , width ,data_type , field_sequence, b_null , b_pk , b_sys , table_name ,	table_name_c , Description , defaultvalue , b_identity) values  ('d7050a91-c880-4829-b1fc-3f14983e9fa4' , '100a613b-3503-48f8-b9e5-8f6ad775a793','页面GUID','TemplatePageGUID',16,'uniqueidentifier',1,0,0,0,'ds_TemplatePage','页面模板表','','',0) ; insert into data_dict(guid , tableguid , field_name_c , field_name , width ,data_type , field_sequence, b_null , b_pk , b_sys , table_name ,	table_name_c , Description , defaultvalue , b_identity) values  ('402fb4f2-ca25-4d9a-a1b9-c30a562a881c' , '100a613b-3503-48f8-b9e5-8f6ad775a793','模板GUID','TemplateGUID',16,'uniqueidentifier',2,0,0,0,'ds_TemplatePage','页面模板表','','',0) ; insert into data_dict(guid , tableguid , field_name_c , field_name , width ,data_type , field_sequence, b_null , b_pk , b_sys , table_name ,	table_name_c , Description , defaultvalue , b_identity) values  ('d03bfed9-23e8-4336-b293-7c72ac8e1e46' , '100a613b-3503-48f8-b9e5-8f6ad775a793','页面顺序','PageNum',4,'int',3,0,0,0,'ds_TemplatePage','页面模板表','','',0) ; insert into data_dict(guid , tableguid , field_name_c , field_name , width ,data_type , field_sequence, b_null , b_pk , b_sys , table_name ,	table_name_c , Description , defaultvalue , b_identity) values  ('31f81bd7-2590-4eb0-85cf-8160a6d4a3d2' , '100a613b-3503-48f8-b9e5-8f6ad775a793','Json样例','JsonExample',8000,'varchar',4,0,0,0,'ds_TemplatePage','页面模板表','','',0) ; insert into data_dict(guid , tableguid , field_name_c , field_name , width ,data_type , field_sequence, b_null , b_pk , b_sys , table_name ,	table_name_c , Description , defaultvalue , b_identity) values  ('68b9e923-b209-4a92-8298-08eb8512bc54' , '100a613b-3503-48f8-b9e5-8f6ad775a793','页面描述','PageMemo',1000,'varchar',5,0,0,0,'ds_TemplatePage','页面模板表','','',0) ; insert into data_dict(guid , tableguid , field_name_c , field_name , width ,data_type , field_sequence, b_null , b_pk , b_sys , table_name ,	table_name_c , Description , defaultvalue , b_identity) values  ('0ff6e39e-da07-4be8-b9da-9cd08eca1230' , '100a613b-3503-48f8-b9e5-8f6ad775a793','页面样例图地址','PagePicSrc',1000,'varchar',6,0,0,0,'ds_TemplatePage','页面模板表','','',0) ; 
GO 

-------------------------------------------------【END】新增ds_TemplatePage表------------------------------------------------- 
 
 
-------------------------------------------------【BEGIN】新增ds_TemplatePageNodeData表------------------------------------------------- 
IF OBJECT_ID('ds_TemplatePageNodeData') IS NULL 
BEGIN 
     CREATE TABLE ds_TemplatePageNodeData([TemplatePageNodeDataGUID] [UNIQUEIDENTIFIER] PRIMARY KEY
          ,[Name] [VARCHAR](500) 
          ,[TemplatePageGUID] [UNIQUEIDENTIFIER] 
          ,[Code] [VARCHAR](500) 
          ,[ParentCode] [VARCHAR](500) 
          ,[DataType] [INT] 
          ,[StaticData] [VARCHAR](MAX) 
          ,[Sql] [VARCHAR](MAX) 
          ,[IfSqlExcutedLooped] [INT] 
          ,[SqlExcutedLoopedNum] [INT] 
          ,[IfEnd] [INT] 
          ,[Level] [INT] 
          ,[IfDataHandled] [INT] 
          ,[DataHandleAssemble] [VARCHAR](200) 
          ,[DataHandleFunction] [VARCHAR](200) 
     )
END
GO 

delete from data_dict where table_name='ds_TemplatePageNodeData' ; insert into data_dict(guid , tableguid , field_name_c , field_name , width ,data_type , field_sequence, b_null , b_pk , b_sys , table_name ,	table_name_c , Description , defaultvalue , b_identity) values  ('51b5fd7f-5156-460f-9d00-b4cf92b7e346' , '100a613b-3503-48f8-b9e5-8f6ad775a793','页面数据结构GUID','TemplatePageNodeDataGUID',16,'uniqueidentifier',1,0,0,0,'ds_TemplatePageNodeData','页面数据结构模板表','','',0) ; insert into data_dict(guid , tableguid , field_name_c , field_name , width ,data_type , field_sequence, b_null , b_pk , b_sys , table_name ,	table_name_c , Description , defaultvalue , b_identity) values  ('01bf9ece-bcd8-4c28-9954-cc0a09f4eace' , '100a613b-3503-48f8-b9e5-8f6ad775a793','节点名称','Name',500,'varchar',2,0,0,0,'ds_TemplatePageNodeData','页面数据结构模板表','','',0) ; insert into data_dict(guid , tableguid , field_name_c , field_name , width ,data_type , field_sequence, b_null , b_pk , b_sys , table_name ,	table_name_c , Description , defaultvalue , b_identity) values  ('1c36d571-f66c-43a7-90c8-2af24cc766fe' , '100a613b-3503-48f8-b9e5-8f6ad775a793','页面GUID','TemplatePageGUID',16,'uniqueidentifier',3,0,0,0,'ds_TemplatePageNodeData','页面数据结构模板表','','',0) ; insert into data_dict(guid , tableguid , field_name_c , field_name , width ,data_type , field_sequence, b_null , b_pk , b_sys , table_name ,	table_name_c , Description , defaultvalue , b_identity) values  ('fed06411-b027-4c25-8098-81971ae16fb7' , '100a613b-3503-48f8-b9e5-8f6ad775a793','当前层级Code','Code',500,'varchar',4,0,0,0,'ds_TemplatePageNodeData','页面数据结构模板表','','',0) ; insert into data_dict(guid , tableguid , field_name_c , field_name , width ,data_type , field_sequence, b_null , b_pk , b_sys , table_name ,	table_name_c , Description , defaultvalue , b_identity) values  ('68248877-3de3-490c-aaab-b5f09dc1968e' , '100a613b-3503-48f8-b9e5-8f6ad775a793','父级层级Code','ParentCode',500,'varchar',5,0,0,0,'ds_TemplatePageNodeData','页面数据结构模板表','','',0) ; insert into data_dict(guid , tableguid , field_name_c , field_name , width ,data_type , field_sequence, b_null , b_pk , b_sys , table_name ,	table_name_c , Description , defaultvalue , b_identity) values  ('9e8d88e7-5dbd-43c0-9097-5e63ef5018f2' , '100a613b-3503-48f8-b9e5-8f6ad775a793','数据形式','DataType',4,'int',6,0,0,0,'ds_TemplatePageNodeData','页面数据结构模板表','','',0) ; insert into data_dict(guid , tableguid , field_name_c , field_name , width ,data_type , field_sequence, b_null , b_pk , b_sys , table_name ,	table_name_c , Description , defaultvalue , b_identity) values  ('fb205291-cbf7-4bdc-8651-76324f5f97a6' , '100a613b-3503-48f8-b9e5-8f6ad775a793','静态数据','StaticData',8000,'varchar',7,0,0,0,'ds_TemplatePageNodeData','页面数据结构模板表','','',0) ; insert into data_dict(guid , tableguid , field_name_c , field_name , width ,data_type , field_sequence, b_null , b_pk , b_sys , table_name ,	table_name_c , Description , defaultvalue , b_identity) values  ('1242d9e7-2661-4bc3-9264-d65866b8cc56' , '100a613b-3503-48f8-b9e5-8f6ad775a793','节点Sql','Sql',8000,'varchar',8,0,0,0,'ds_TemplatePageNodeData','页面数据结构模板表','','',0) ; insert into data_dict(guid , tableguid , field_name_c , field_name , width ,data_type , field_sequence, b_null , b_pk , b_sys , table_name ,	table_name_c , Description , defaultvalue , b_identity) values  ('d9d8c09d-93c8-425d-8df5-78c7ad1ee565' , '100a613b-3503-48f8-b9e5-8f6ad775a793','是否循环调用Sql','IfSqlExcutedLooped',4,'int',9,0,0,0,'ds_TemplatePageNodeData','页面数据结构模板表','','',0) ; insert into data_dict(guid , tableguid , field_name_c , field_name , width ,data_type , field_sequence, b_null , b_pk , b_sys , table_name ,	table_name_c , Description , defaultvalue , b_identity) values  ('03a9b329-9d2a-4324-bd69-e7b72f318bf0' , '100a613b-3503-48f8-b9e5-8f6ad775a793','循环调用单次返回数量','SqlExcutedLoopedNum',4,'int',10,0,0,0,'ds_TemplatePageNodeData','页面数据结构模板表','','',0) ; insert into data_dict(guid , tableguid , field_name_c , field_name , width ,data_type , field_sequence, b_null , b_pk , b_sys , table_name ,	table_name_c , Description , defaultvalue , b_identity) values  ('ecaeaaeb-db14-4e58-a47c-f2b84fd55fdb' , '100a613b-3503-48f8-b9e5-8f6ad775a793','是否末级','IfEnd',4,'int',11,0,0,0,'ds_TemplatePageNodeData','页面数据结构模板表','','',0) ; insert into data_dict(guid , tableguid , field_name_c , field_name , width ,data_type , field_sequence, b_null , b_pk , b_sys , table_name ,	table_name_c , Description , defaultvalue , b_identity) values  ('8b1dca69-a6a7-4435-9a9f-42e4f20b3a9f' , '100a613b-3503-48f8-b9e5-8f6ad775a793','是否加工数据','IfDataHandled',4,'int',12,0,0,0,'ds_TemplatePageNodeData','页面数据结构模板表','','',0) ; insert into data_dict(guid , tableguid , field_name_c , field_name , width ,data_type , field_sequence, b_null , b_pk , b_sys , table_name ,	table_name_c , Description , defaultvalue , b_identity) values  ('4f65d2d6-96ed-402e-a390-4365a8188754' , '100a613b-3503-48f8-b9e5-8f6ad775a793','加工映射类','DataHandleAssemble',200,'varchar',13,0,0,0,'ds_TemplatePageNodeData','页面数据结构模板表','','',0) ; insert into data_dict(guid , tableguid , field_name_c , field_name , width ,data_type , field_sequence, b_null , b_pk , b_sys , table_name ,	table_name_c , Description , defaultvalue , b_identity) values  ('9ad494d0-35cb-46a9-a97c-fb9e674ec64e' , '100a613b-3503-48f8-b9e5-8f6ad775a793','加工映射方法','DataHandleFunction',200,'varchar',14,0,0,0,'ds_TemplatePageNodeData','页面数据结构模板表','','',0) ; 
GO 

-------------------------------------------------【END】新增ds_TemplatePageNodeData表------------------------------------------------- 
 
 
-------------------------------------------------【BEGIN】新增ds_Renter2Template表------------------------------------------------- 
IF OBJECT_ID('ds_Renter2Template') IS NULL 
BEGIN 
     CREATE TABLE ds_Renter2Template([Renter2TemplateGUID] [UNIQUEIDENTIFIER] PRIMARY KEY
          ,[RenterGUID] [UNIQUEIDENTIFIER] 
          ,[TemplateGUID] [UNIQUEIDENTIFIER] 
          ,[IsDisabled] [INT] 
     )
END
GO 
 

delete from data_dict where table_name='ds_Renter2Template' ; insert into data_dict(guid , tableguid , field_name_c , field_name , width ,data_type , field_sequence, b_null , b_pk , b_sys , table_name ,	table_name_c , Description , defaultvalue , b_identity) values  ('fac21aef-636d-4edc-aed9-27e0b8934662' , '100a613b-3503-48f8-b9e5-8f6ad775a793','租户对应模板GUID','Renter2TemplateGUID',16,'uniqueidentifier',1,0,0,0,'ds_Renter2Template','租户对应模板表','','',0) ; insert into data_dict(guid , tableguid , field_name_c , field_name , width ,data_type , field_sequence, b_null , b_pk , b_sys , table_name ,	table_name_c , Description , defaultvalue , b_identity) values  ('1f06344f-4215-40f8-b132-638e1dcbdd3a' , '100a613b-3503-48f8-b9e5-8f6ad775a793','租户GUID','RenterGUID',16,'uniqueidentifier',2,0,0,0,'ds_Renter2Template','租户对应模板表','','',0) ; insert into data_dict(guid , tableguid , field_name_c , field_name , width ,data_type , field_sequence, b_null , b_pk , b_sys , table_name ,	table_name_c , Description , defaultvalue , b_identity) values  ('8ebe2b9f-36e4-4b72-92dc-c1f41fa77b53' , '100a613b-3503-48f8-b9e5-8f6ad775a793','模板GUID','TemplateGUID',16,'uniqueidentifier',3,0,0,0,'ds_Renter2Template','租户对应模板表','','',0) ; insert into data_dict(guid , tableguid , field_name_c , field_name , width ,data_type , field_sequence, b_null , b_pk , b_sys , table_name ,	table_name_c , Description , defaultvalue , b_identity) values  ('5d49780d-2991-41c5-b3c2-e5f420ed4de4' , '100a613b-3503-48f8-b9e5-8f6ad775a793','租户模板是否禁用','IsDisabled',4,'int',4,0,0,0,'ds_Renter2Template','租户对应模板表','','',0) ; 
GO 

-------------------------------------------------【END】新增ds_Renter2Template表------------------------------------------------- 
 
 
-------------------------------------------------【BEGIN】新增ds_EntityRenter2Template表------------------------------------------------- 
IF OBJECT_ID('ds_EntityRenter2Template') IS NULL 
BEGIN 
     CREATE TABLE ds_EntityRenter2Template([EntityRenter2TemplateGUID] [UNIQUEIDENTIFIER] PRIMARY KEY
          ,[Renter2TemplateGUID] [UNIQUEIDENTIFIER] 
          ,[RenterTemplateEntityName] [VARCHAR](500) 
          ,[RenterGUID] [UNIQUEIDENTIFIER] 
          ,[TemplateGUID] [UNIQUEIDENTIFIER] 
          ,[JsonDataFilePath] [VARCHAR](1000) 
          ,[IsDisabled] [INT] 
     )
END
GO 
 

delete from data_dict where table_name='ds_EntityRenter2Template' ; insert into data_dict(guid , tableguid , field_name_c , field_name , width ,data_type , field_sequence, b_null , b_pk , b_sys , table_name ,	table_name_c , Description , defaultvalue , b_identity) values  ('5c56af63-d636-4c91-b14a-2587afc1bed9' , '100a613b-3503-48f8-b9e5-8f6ad775a793','租户模板实例GUID','EntityRenter2TemplateGUID',16,'uniqueidentifier',1,0,0,0,'ds_EntityRenter2Template','租户模板实例表','','',0) ; insert into data_dict(guid , tableguid , field_name_c , field_name , width ,data_type , field_sequence, b_null , b_pk , b_sys , table_name ,	table_name_c , Description , defaultvalue , b_identity) values  ('4e8ae8cc-8388-4739-bd30-a1a470968eca' , '100a613b-3503-48f8-b9e5-8f6ad775a793','租户对应模板GUID','Renter2TemplateGUID',16,'uniqueidentifier',2,0,0,0,'ds_EntityRenter2Template','租户模板实例表','','',0) ; insert into data_dict(guid , tableguid , field_name_c , field_name , width ,data_type , field_sequence, b_null , b_pk , b_sys , table_name ,	table_name_c , Description , defaultvalue , b_identity) values  ('9fa6524b-d2cc-4f1d-bbd8-83cf925c1ad9' , '100a613b-3503-48f8-b9e5-8f6ad775a793','租户模板实例名称','RenterTemplateEntityName',500,'varchar',3,0,0,0,'ds_EntityRenter2Template','租户模板实例表','','',0) ; insert into data_dict(guid , tableguid , field_name_c , field_name , width ,data_type , field_sequence, b_null , b_pk , b_sys , table_name ,	table_name_c , Description , defaultvalue , b_identity) values  ('c0ed286a-eb58-40e9-8fb1-80f9c3389273' , '100a613b-3503-48f8-b9e5-8f6ad775a793','租户GUID','RenterGUID',16,'uniqueidentifier',4,0,0,0,'ds_EntityRenter2Template','租户模板实例表','','',0) ; insert into data_dict(guid , tableguid , field_name_c , field_name , width ,data_type , field_sequence, b_null , b_pk , b_sys , table_name ,	table_name_c , Description , defaultvalue , b_identity) values  ('f669b687-6ff7-45cf-9065-b9a36c3654f1' , '100a613b-3503-48f8-b9e5-8f6ad775a793','模板GUID','TemplateGUID',16,'uniqueidentifier',5,0,0,0,'ds_EntityRenter2Template','租户模板实例表','','',0) ; insert into data_dict(guid , tableguid , field_name_c , field_name , width ,data_type , field_sequence, b_null , b_pk , b_sys , table_name ,	table_name_c , Description , defaultvalue , b_identity) values  ('2e8d802f-8323-4fff-9dcc-0b8c11d67ed3' , '100a613b-3503-48f8-b9e5-8f6ad775a793','实例发布相对地址','JsonDataFilePath',1000,'varchar',6,0,0,0,'ds_EntityRenter2Template','租户模板实例表','','',0) ; insert into data_dict(guid , tableguid , field_name_c , field_name , width ,data_type , field_sequence, b_null , b_pk , b_sys , table_name ,	table_name_c , Description , defaultvalue , b_identity) values  ('c30a96bb-3442-46e2-96e0-11e779e6a4fc' , '100a613b-3503-48f8-b9e5-8f6ad775a793','实例是否禁用','IsDisabled',4,'int',7,0,0,0,'ds_EntityRenter2Template','租户模板实例表','','',0) ; 
GO 

-------------------------------------------------【END】新增ds_EntityRenter2Template表------------------------------------------------- 
 
 
-------------------------------------------------【BEGIN】新增ds_EntityPage表------------------------------------------------- 
IF OBJECT_ID('ds_EntityPage') IS NULL 
BEGIN 
     CREATE TABLE ds_EntityPage([EntityPageGUID] [UNIQUEIDENTIFIER] PRIMARY KEY
          ,[EntityRenter2TemplateGUID] [UNIQUEIDENTIFIER] 
          ,[TemplateGUID] [UNIQUEIDENTIFIER] 
          ,[TemplatePageGUID] [UNIQUEIDENTIFIER] 
          ,[OriginalPageNum] [INT] 
          ,[PageNum] [INT] 
          ,[JsonExample] [VARCHAR](MAX) 
          ,[PageMemo] [VARCHAR](1000) 
     )
END
GO 
 

delete from data_dict where table_name='ds_EntityPage' ; insert into data_dict(guid , tableguid , field_name_c , field_name , width ,data_type , field_sequence, b_null , b_pk , b_sys , table_name ,	table_name_c , Description , defaultvalue , b_identity) values  ('1cf79045-3c99-4a53-8d48-91039e95349c' , '100a613b-3503-48f8-b9e5-8f6ad775a793','页面GUID','EntityPageGUID',16,'uniqueidentifier',1,0,0,0,'ds_EntityPage','页面实例表','','',0) ; insert into data_dict(guid , tableguid , field_name_c , field_name , width ,data_type , field_sequence, b_null , b_pk , b_sys , table_name ,	table_name_c , Description , defaultvalue , b_identity) values  ('7bf03a23-678d-4d08-beb2-e4ea63b2fe24' , '100a613b-3503-48f8-b9e5-8f6ad775a793','租户模板实例GUID','EntityRenter2TemplateGUID',16,'uniqueidentifier',2,0,0,0,'ds_EntityPage','页面实例表','','',0) ; insert into data_dict(guid , tableguid , field_name_c , field_name , width ,data_type , field_sequence, b_null , b_pk , b_sys , table_name ,	table_name_c , Description , defaultvalue , b_identity) values  ('9ad21f08-ef2d-48bb-ae91-2fbb0084a54f' , '100a613b-3503-48f8-b9e5-8f6ad775a793','模板GUID','TemplateGUID',16,'uniqueidentifier',3,0,0,0,'ds_EntityPage','页面实例表','','',0) ; insert into data_dict(guid , tableguid , field_name_c , field_name , width ,data_type , field_sequence, b_null , b_pk , b_sys , table_name ,	table_name_c , Description , defaultvalue , b_identity) values  ('4ac8f41c-dde0-4ada-bab3-94f035098550' , '100a613b-3503-48f8-b9e5-8f6ad775a793','模板页面GUID','TemplatePageGUID',16,'uniqueidentifier',4,0,0,0,'ds_EntityPage','页面实例表','','',0) ; insert into data_dict(guid , tableguid , field_name_c , field_name , width ,data_type , field_sequence, b_null , b_pk , b_sys , table_name ,	table_name_c , Description , defaultvalue , b_identity) values  ('89ab91cf-cbbc-4a6e-9233-a07aa37d5c89' , '100a613b-3503-48f8-b9e5-8f6ad775a793','页面原始顺序','OriginalPageNum',4,'int',5,0,0,0,'ds_EntityPage','页面实例表','','',0) ; insert into data_dict(guid , tableguid , field_name_c , field_name , width ,data_type , field_sequence, b_null , b_pk , b_sys , table_name ,	table_name_c , Description , defaultvalue , b_identity) values  ('c37de791-a0db-4283-b498-0961ba63c45a' , '100a613b-3503-48f8-b9e5-8f6ad775a793','页面最终顺序','PageNum',4,'int',6,0,0,0,'ds_EntityPage','页面实例表','','',0) ; insert into data_dict(guid , tableguid , field_name_c , field_name , width ,data_type , field_sequence, b_null , b_pk , b_sys , table_name ,	table_name_c , Description , defaultvalue , b_identity) values  ('ae55b0a6-2fd1-47d2-adbe-9a838e193ab9' , '100a613b-3503-48f8-b9e5-8f6ad775a793','Json样例','JsonExample',8000,'varchar',7,0,0,0,'ds_EntityPage','页面实例表','','',0) ; insert into data_dict(guid , tableguid , field_name_c , field_name , width ,data_type , field_sequence, b_null , b_pk , b_sys , table_name ,	table_name_c , Description , defaultvalue , b_identity) values  ('ca85cb25-f488-4189-a363-8d7fe135ab2f' , '100a613b-3503-48f8-b9e5-8f6ad775a793','页面描述','PageMemo',1000,'varchar',8,0,0,0,'ds_EntityPage','页面实例表','','',0) ; 
GO 

-------------------------------------------------【END】新增ds_EntityPage表------------------------------------------------- 
 
 
-------------------------------------------------【BEGIN】新增ds_EntityPageNodeData表------------------------------------------------- 
IF OBJECT_ID('ds_EntityPageNodeData') IS NULL 
BEGIN 
     CREATE TABLE ds_EntityPageNodeData([EntityPageNodeDataGUID] [UNIQUEIDENTIFIER] PRIMARY KEY
          ,[Name] [VARCHAR](500) 
          ,[EntityPageGUID] [UNIQUEIDENTIFIER] 
          ,[Code] [VARCHAR](500) 
          ,[ParentCode] [VARCHAR](500) 
          ,[DataType] [INT] 
          ,[StaticData] [VARCHAR](MAX) 
          ,[Sql] [VARCHAR](MAX) 
          ,[IfSqlExcutedLooped] [INT] 
          ,[SqlExcutedLoopedNum] [INT] 
          ,[IfEnd] [INT] 
          ,[Level] [INT] 
          ,[IfDataHandled] [INT] 
          ,[DataHandleAssemble] [VARCHAR](200) 
          ,[DataHandleFunction] [VARCHAR](200) 
     )
END
GO 
 

delete from data_dict where table_name='ds_EntityPageNodeData' ; insert into data_dict(guid , tableguid , field_name_c , field_name , width ,data_type , field_sequence, b_null , b_pk , b_sys , table_name ,	table_name_c , Description , defaultvalue , b_identity) values  ('b380d3ea-2de7-489c-a46b-7648480092e2' , '100a613b-3503-48f8-b9e5-8f6ad775a793','页面数据结构GUID','EntityPageNodeDataGUID',16,'uniqueidentifier',1,0,0,0,'ds_EntityPageNodeData','页面数据结构实例表','','',0) ; insert into data_dict(guid , tableguid , field_name_c , field_name , width ,data_type , field_sequence, b_null , b_pk , b_sys , table_name ,	table_name_c , Description , defaultvalue , b_identity) values  ('ea404c8b-8209-495a-a428-08cbea90bed4' , '100a613b-3503-48f8-b9e5-8f6ad775a793','节点名称','aName',500,'varchar',2,0,0,0,'ds_EntityPageNodeData','页面数据结构实例表','','',0) ; insert into data_dict(guid , tableguid , field_name_c , field_name , width ,data_type , field_sequence, b_null , b_pk , b_sys , table_name ,	table_name_c , Description , defaultvalue , b_identity) values  ('bd695f8b-2f93-4ec2-8994-9045b2e51524' , '100a613b-3503-48f8-b9e5-8f6ad775a793','页面GUID','EntityPageGUID',16,'uniqueidentifier',3,0,0,0,'ds_EntityPageNodeData','页面数据结构实例表','','',0) ; insert into data_dict(guid , tableguid , field_name_c , field_name , width ,data_type , field_sequence, b_null , b_pk , b_sys , table_name ,	table_name_c , Description , defaultvalue , b_identity) values  ('0c550838-f2e5-4f6c-bd5e-9f350bfe8f47' , '100a613b-3503-48f8-b9e5-8f6ad775a793','当前层级Code','Code',500,'varchar',4,0,0,0,'ds_EntityPageNodeData','页面数据结构实例表','','',0) ; insert into data_dict(guid , tableguid , field_name_c , field_name , width ,data_type , field_sequence, b_null , b_pk , b_sys , table_name ,	table_name_c , Description , defaultvalue , b_identity) values  ('60d59d0e-d3d1-430f-8fe2-8299acbda550' , '100a613b-3503-48f8-b9e5-8f6ad775a793','父级层级Code','ParentCode',500,'varchar',5,0,0,0,'ds_EntityPageNodeData','页面数据结构实例表','','',0) ; insert into data_dict(guid , tableguid , field_name_c , field_name , width ,data_type , field_sequence, b_null , b_pk , b_sys , table_name ,	table_name_c , Description , defaultvalue , b_identity) values  ('6388116f-c167-44a8-97b9-01564e36b0f9' , '100a613b-3503-48f8-b9e5-8f6ad775a793','数据形式','DataType',4,'int',6,0,0,0,'ds_EntityPageNodeData','页面数据结构实例表','','',0) ; insert into data_dict(guid , tableguid , field_name_c , field_name , width ,data_type , field_sequence, b_null , b_pk , b_sys , table_name ,	table_name_c , Description , defaultvalue , b_identity) values  ('ccd16cab-9137-485e-b8de-6c25343ff976' , '100a613b-3503-48f8-b9e5-8f6ad775a793','静态数据','StaticData',8000,'varchar',7,0,0,0,'ds_EntityPageNodeData','页面数据结构实例表','','',0) ; insert into data_dict(guid , tableguid , field_name_c , field_name , width ,data_type , field_sequence, b_null , b_pk , b_sys , table_name ,	table_name_c , Description , defaultvalue , b_identity) values  ('2b4d73be-de4a-45c2-9cf9-ac4262de843e' , '100a613b-3503-48f8-b9e5-8f6ad775a793','节点Sql','Sql',8000,'varchar',8,0,0,0,'ds_EntityPageNodeData','页面数据结构实例表','','',0) ; insert into data_dict(guid , tableguid , field_name_c , field_name , width ,data_type , field_sequence, b_null , b_pk , b_sys , table_name ,	table_name_c , Description , defaultvalue , b_identity) values  ('f1141eb7-5a67-4738-9caf-75eddb135284' , '100a613b-3503-48f8-b9e5-8f6ad775a793','是否循环调用Sql','IfSqlExcutedLooped',4,'int',9,0,0,0,'ds_EntityPageNodeData','页面数据结构实例表','','',0) ; insert into data_dict(guid , tableguid , field_name_c , field_name , width ,data_type , field_sequence, b_null , b_pk , b_sys , table_name ,	table_name_c , Description , defaultvalue , b_identity) values  ('e5ab3d58-edb6-4c68-b65e-d18908d6193e' , '100a613b-3503-48f8-b9e5-8f6ad775a793','循环调用单次返回数量','SqlExcutedLoopedNum',4,'int',10,0,0,0,'ds_EntityPageNodeData','页面数据结构实例表','','',0) ; insert into data_dict(guid , tableguid , field_name_c , field_name , width ,data_type , field_sequence, b_null , b_pk , b_sys , table_name ,	table_name_c , Description , defaultvalue , b_identity) values  ('dbe78390-3aa0-468a-a369-ebf3939073d3' , '100a613b-3503-48f8-b9e5-8f6ad775a793','是否末级','IfEnd',4,'int',11,0,0,0,'ds_EntityPageNodeData','页面数据结构实例表','','',0) ; insert into data_dict(guid , tableguid , field_name_c , field_name , width ,data_type , field_sequence, b_null , b_pk , b_sys , table_name ,	table_name_c , Description , defaultvalue , b_identity) values  ('bb290889-bb52-4861-a08f-0bc3683e9f97' , '100a613b-3503-48f8-b9e5-8f6ad775a793','是否加工数据','IfDataHandled',4,'int',12,0,0,0,'ds_EntityPageNodeData','页面数据结构实例表','','',0) ; insert into data_dict(guid , tableguid , field_name_c , field_name , width ,data_type , field_sequence, b_null , b_pk , b_sys , table_name ,	table_name_c , Description , defaultvalue , b_identity) values  ('debabf1a-c515-41c3-b897-a34617475ffc' , '100a613b-3503-48f8-b9e5-8f6ad775a793','加工映射类','DataHandleAssemble',200,'varchar',13,0,0,0,'ds_EntityPageNodeData','页面数据结构实例表','','',0) ; insert into data_dict(guid , tableguid , field_name_c , field_name , width ,data_type , field_sequence, b_null , b_pk , b_sys , table_name ,	table_name_c , Description , defaultvalue , b_identity) values  ('e8f7efce-f000-4600-a973-572aa6aa0367' , '100a613b-3503-48f8-b9e5-8f6ad775a793','加工映射方法','DataHandleFunction',200,'varchar',14,0,0,0,'ds_EntityPageNodeData','页面数据结构实例表','','',0) ; 
GO 

-------------------------------------------------【END】新增ds_EntityPageNodeData表------------------------------------------------- 
 
 
