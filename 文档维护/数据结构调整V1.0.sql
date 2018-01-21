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


-------------------------------------------------【BEGIN】新增ds_Renter表------------------------------------------------- 
IF OBJECT_ID('ds_Renter') IS NULL 
BEGIN 
     CREATE TABLE ds_Renter([RenterGUID] [UNIQUEIDENTIFIER] PRIMARY KEY CLUSTERED NOT NULL  
          ,[RenterName] [VARCHAR](200) 
     )
END
GO 
 
delete from data_dict where table_name='ds_Renter' ; insert into data_dict(guid , tableguid , field_name_c , field_name , width ,data_type , field_sequence, b_null , b_pk , b_sys , table_name ,	table_name_c , Description , defaultvalue , b_identity) values  ('e334cf85-e1c4-4db9-91fb-20c68b03e45f' , 'b60007dd-3e6e-4e83-bafc-0f01ebfc9597','租户GUID','RenterGUID',16,'uniqueidentifier',1,0,1,0,'ds_Renter','租户表','','',0) ; insert into data_dict(guid , tableguid , field_name_c , field_name , width ,data_type , field_sequence, b_null , b_pk , b_sys , table_name ,	table_name_c , Description , defaultvalue , b_identity) values  ('1fafe1d5-3ad9-4def-8f66-9a1570ab40e1' , 'b60007dd-3e6e-4e83-bafc-0f01ebfc9597','租户名称','RenterName',200,'varchar',2,0,0,0,'ds_Renter','租户表','','',0) ; 
GO 
-------------------------------------------------【END】新增ds_Renter表------------------------------------------------- 
 
 
-------------------------------------------------【BEGIN】新增ds_Template表------------------------------------------------- 
IF OBJECT_ID('ds_Template') IS NULL 
BEGIN 
     CREATE TABLE ds_Template([TemplateGUID] [UNIQUEIDENTIFIER] PRIMARY KEY CLUSTERED NOT NULL  
          ,[TemplateName] [VARCHAR](200) 
          ,[TemplateMemo] [VARCHAR](1000) 
     )
END
GO 
 
delete from data_dict where table_name='ds_Template' ; insert into data_dict(guid , tableguid , field_name_c , field_name , width ,data_type , field_sequence, b_null , b_pk , b_sys , table_name ,	table_name_c , Description , defaultvalue , b_identity) values  ('82c709da-7ab7-4105-aa56-1cd4990436c4' , 'b60007dd-3e6e-4e83-bafc-0f01ebfc9597','模板GUID','TemplateGUID',16,'uniqueidentifier',1,0,1,0,'ds_Template','模板表','','',0) ; insert into data_dict(guid , tableguid , field_name_c , field_name , width ,data_type , field_sequence, b_null , b_pk , b_sys , table_name ,	table_name_c , Description , defaultvalue , b_identity) values  ('6d4573f1-5e00-4191-8908-5d2b394144f1' , 'b60007dd-3e6e-4e83-bafc-0f01ebfc9597','模板名称','TemplateName',200,'varchar',2,0,0,0,'ds_Template','模板表','','',0) ; insert into data_dict(guid , tableguid , field_name_c , field_name , width ,data_type , field_sequence, b_null , b_pk , b_sys , table_name ,	table_name_c , Description , defaultvalue , b_identity) values  ('fab12d67-e953-4853-a029-c853b5c2813c' , 'b60007dd-3e6e-4e83-bafc-0f01ebfc9597','模板描述','TemplateMemo',1000,'varchar',3,0,0,0,'ds_Template','模板表','','',0) ; 
GO 
-------------------------------------------------【END】新增ds_Template表------------------------------------------------- 
 
 
-------------------------------------------------【BEGIN】新增ds_TemplatePage表------------------------------------------------- 
IF OBJECT_ID('ds_TemplatePage') IS NULL 
BEGIN 
     CREATE TABLE ds_TemplatePage([TemplatePageGUID] [UNIQUEIDENTIFIER] PRIMARY KEY CLUSTERED NOT NULL  
          ,[TemplateGUID] [UNIQUEIDENTIFIER] 
          ,[PageNum] [INT] 
          ,[JsonExample] [VARCHAR](MAX)
          ,[PageMemo] [VARCHAR](1000) 
     )
END
GO 
 
delete from data_dict where table_name='ds_TemplatePage' ; insert into data_dict(guid , tableguid , field_name_c , field_name , width ,data_type , field_sequence, b_null , b_pk , b_sys , table_name ,	table_name_c , Description , defaultvalue , b_identity) values  ('052c7348-db5f-4043-92cb-716e078ee8c5' , 'b60007dd-3e6e-4e83-bafc-0f01ebfc9597','页面GUID','TemplatePageGUID',16,'uniqueidentifier',1,0,1,0,'ds_TemplatePage','页面模板表','','',0) ; insert into data_dict(guid , tableguid , field_name_c , field_name , width ,data_type , field_sequence, b_null , b_pk , b_sys , table_name ,	table_name_c , Description , defaultvalue , b_identity) values  ('9f649ad4-98de-464d-b2d3-02f658026f40' , 'b60007dd-3e6e-4e83-bafc-0f01ebfc9597','模板GUID','TemplateGUID',16,'uniqueidentifier',2,0,0,0,'ds_TemplatePage','页面模板表','','',0) ; insert into data_dict(guid , tableguid , field_name_c , field_name , width ,data_type , field_sequence, b_null , b_pk , b_sys , table_name ,	table_name_c , Description , defaultvalue , b_identity) values  ('52700de5-3024-4c33-8ca1-a14790dc23a0' , 'b60007dd-3e6e-4e83-bafc-0f01ebfc9597','页面顺序','PageNum',4,'int',3,0,0,0,'ds_TemplatePage','页面模板表','','',0) ; insert into data_dict(guid , tableguid , field_name_c , field_name , width ,data_type , field_sequence, b_null , b_pk , b_sys , table_name ,	table_name_c , Description , defaultvalue , b_identity) values  ('4a55141a-d72d-42c6-baee-103a2c7fe396' , 'b60007dd-3e6e-4e83-bafc-0f01ebfc9597','Json样例','JsonExample',8000,'varchar',4,0,0,0,'ds_TemplatePage','页面模板表','','',0) ; insert into data_dict(guid , tableguid , field_name_c , field_name , width ,data_type , field_sequence, b_null , b_pk , b_sys , table_name ,	table_name_c , Description , defaultvalue , b_identity) values  ('538fd1dd-b94d-4419-947e-bfa8b3058fa8' , 'b60007dd-3e6e-4e83-bafc-0f01ebfc9597','页面描述','PageMemo',1000,'varchar',5,0,0,0,'ds_TemplatePage','页面模板表','','',0) ; 
GO 
-------------------------------------------------【END】新增ds_TemplatePage表------------------------------------------------- 
 
 
-------------------------------------------------【BEGIN】新增ds_TemplatePageNodeData表------------------------------------------------- 
IF OBJECT_ID('ds_TemplatePageNodeData') IS NULL 
BEGIN 
     CREATE TABLE ds_TemplatePageNodeData([TemplatePageNodeDataGUID] [UNIQUEIDENTIFIER] PRIMARY KEY CLUSTERED NOT NULL  
          ,[TemplatePageNodeDataName] [VARCHAR](500) 
          ,[TemplatePageGUID] [UNIQUEIDENTIFIER] 
          ,[CurNodeCode] [VARCHAR](500) 
          ,[ParentNodeCode] [VARCHAR](500) 
          ,[NodeSql] [VARCHAR](MAX) 
          ,[IfEnd] [INT] 
     )
END
GO 
 
delete from data_dict where table_name='ds_TemplatePageNodeData' ; insert into data_dict(guid , tableguid , field_name_c , field_name , width ,data_type , field_sequence, b_null , b_pk , b_sys , table_name ,	table_name_c , Description , defaultvalue , b_identity) values  ('129da212-4a31-4391-8d07-0848515159d3' , 'b60007dd-3e6e-4e83-bafc-0f01ebfc9597','页面数据结构GUID','TemplatePageNodeDataGUID',16,'uniqueidentifier',1,0,1,0,'ds_TemplatePageNodeData','页面数据结构模板表','','',0) ; insert into data_dict(guid , tableguid , field_name_c , field_name , width ,data_type , field_sequence, b_null , b_pk , b_sys , table_name ,	table_name_c , Description , defaultvalue , b_identity) values  ('408f55c6-45c1-471d-8385-5731c35120df' , 'b60007dd-3e6e-4e83-bafc-0f01ebfc9597','节点名称','TemplatePageNodeDataName',500,'varchar',2,0,0,0,'ds_TemplatePageNodeData','页面数据结构模板表','','',0) ; insert into data_dict(guid , tableguid , field_name_c , field_name , width ,data_type , field_sequence, b_null , b_pk , b_sys , table_name ,	table_name_c , Description , defaultvalue , b_identity) values  ('c20c1779-24f5-45d7-a044-a057ed73c751' , 'b60007dd-3e6e-4e83-bafc-0f01ebfc9597','页面GUID','TemplatePageGUID',16,'uniqueidentifier',3,0,0,0,'ds_TemplatePageNodeData','页面数据结构模板表','','',0) ; insert into data_dict(guid , tableguid , field_name_c , field_name , width ,data_type , field_sequence, b_null , b_pk , b_sys , table_name ,	table_name_c , Description , defaultvalue , b_identity) values  ('e126a99d-4b4e-4773-825c-c27aa7b169ba' , 'b60007dd-3e6e-4e83-bafc-0f01ebfc9597','当前层级Code','CurNodeCode',500,'varchar',4,0,0,0,'ds_TemplatePageNodeData','页面数据结构模板表','','',0) ; insert into data_dict(guid , tableguid , field_name_c , field_name , width ,data_type , field_sequence, b_null , b_pk , b_sys , table_name ,	table_name_c , Description , defaultvalue , b_identity) values  ('07dc7249-d66b-4047-9525-d84206a39a32' , 'b60007dd-3e6e-4e83-bafc-0f01ebfc9597','父级层级Code','ParentNodeCode',500,'varchar',5,0,0,0,'ds_TemplatePageNodeData','页面数据结构模板表','','',0) ; insert into data_dict(guid , tableguid , field_name_c , field_name , width ,data_type , field_sequence, b_null , b_pk , b_sys , table_name ,	table_name_c , Description , defaultvalue , b_identity) values  ('9ccd555c-0422-4961-a78a-bd6abed76588' , 'b60007dd-3e6e-4e83-bafc-0f01ebfc9597','节点Sql','NodeSql',8000,'varchar',6,0,0,0,'ds_TemplatePageNodeData','页面数据结构模板表','','',0) ; insert into data_dict(guid , tableguid , field_name_c , field_name , width ,data_type , field_sequence, b_null , b_pk , b_sys , table_name ,	table_name_c , Description , defaultvalue , b_identity) values  ('7ea764ec-3a24-4ac1-9703-9501efd1efb9' , 'b60007dd-3e6e-4e83-bafc-0f01ebfc9597','是否末级','IfEnd',4,'int',7,0,0,0,'ds_TemplatePageNodeData','页面数据结构模板表','','',0) ; 
GO 
-------------------------------------------------【END】新增ds_TemplatePageNodeData表------------------------------------------------- 
 
 
-------------------------------------------------【BEGIN】新增ds_Renter2Template表------------------------------------------------- 
IF OBJECT_ID('ds_Renter2Template') IS NULL 
BEGIN 
     CREATE TABLE ds_Renter2Template([Renter2TemplateGUID] [UNIQUEIDENTIFIER] PRIMARY KEY CLUSTERED NOT NULL  
          ,[RenterGUID] [UNIQUEIDENTIFIER] 
          ,[TemplateGUID] [UNIQUEIDENTIFIER] 
          ,[IsDisabled] [INT] 
     )
END
GO 
 
delete from data_dict where table_name='ds_Renter2Template' ; insert into data_dict(guid , tableguid , field_name_c , field_name , width ,data_type , field_sequence, b_null , b_pk , b_sys , table_name ,	table_name_c , Description , defaultvalue , b_identity) values  ('614457f9-8fdd-44b2-82cb-77a6da47bba9' , 'b60007dd-3e6e-4e83-bafc-0f01ebfc9597','租户对应模板GUID','Renter2TemplateGUID',16,'uniqueidentifier',1,0,1,0,'ds_Renter2Template','租户对应模板表','','',0) ; insert into data_dict(guid , tableguid , field_name_c , field_name , width ,data_type , field_sequence, b_null , b_pk , b_sys , table_name ,	table_name_c , Description , defaultvalue , b_identity) values  ('e08825af-5a66-4e5d-9735-0422673f0354' , 'b60007dd-3e6e-4e83-bafc-0f01ebfc9597','租户GUID','RenterGUID',16,'uniqueidentifier',2,0,0,0,'ds_Renter2Template','租户对应模板表','','',0) ; insert into data_dict(guid , tableguid , field_name_c , field_name , width ,data_type , field_sequence, b_null , b_pk , b_sys , table_name ,	table_name_c , Description , defaultvalue , b_identity) values  ('e79cdbb3-8ece-465a-99a3-33926e8806ae' , 'b60007dd-3e6e-4e83-bafc-0f01ebfc9597','模板GUID','TemplateGUID',16,'uniqueidentifier',3,0,0,0,'ds_Renter2Template','租户对应模板表','','',0) ; insert into data_dict(guid , tableguid , field_name_c , field_name , width ,data_type , field_sequence, b_null , b_pk , b_sys , table_name ,	table_name_c , Description , defaultvalue , b_identity) values  ('cbc27142-28c6-4a17-85a3-27909c88d983' , 'b60007dd-3e6e-4e83-bafc-0f01ebfc9597','租户模板是否禁用','IsDisabled',4,'int',4,0,0,0,'ds_Renter2Template','租户对应模板表','','',0) ; 
GO 
-------------------------------------------------【END】新增ds_Renter2Template表------------------------------------------------- 
 
 
-------------------------------------------------【BEGIN】新增ds_EntityRenter2Template表------------------------------------------------- 
IF OBJECT_ID('ds_EntityRenter2Template') IS NULL 
BEGIN 
     CREATE TABLE ds_EntityRenter2Template([EntityRenter2TemplateGUID] [UNIQUEIDENTIFIER] PRIMARY KEY CLUSTERED NOT NULL  
          ,[Renter2TemplateGUID] [UNIQUEIDENTIFIER] 
          ,[RenterTemplateEntityName] [VARCHAR](500) 
          ,[RenterGUID] [UNIQUEIDENTIFIER] 
          ,[TemplateGUID] [UNIQUEIDENTIFIER] 
          ,[JsonDataFilePath] [VARCHAR](1000) 
          ,[IsDisabled] [INT] 
     )
END
GO 
 
delete from data_dict where table_name='ds_EntityRenter2Template' ; insert into data_dict(guid , tableguid , field_name_c , field_name , width ,data_type , field_sequence, b_null , b_pk , b_sys , table_name ,	table_name_c , Description , defaultvalue , b_identity) values  ('9e412a93-b7de-49b5-90f8-1a3fab065b6d' , 'b60007dd-3e6e-4e83-bafc-0f01ebfc9597','租户模板实例GUID','EntityRenter2TemplateGUID',16,'uniqueidentifier',1,0,1,0,'ds_EntityRenter2Template','租户模板实例表','','',0) ; insert into data_dict(guid , tableguid , field_name_c , field_name , width ,data_type , field_sequence, b_null , b_pk , b_sys , table_name ,	table_name_c , Description , defaultvalue , b_identity) values  ('cd148ad4-0504-463a-9df9-3ae38989f9ef' , 'b60007dd-3e6e-4e83-bafc-0f01ebfc9597','租户对应模板GUID','Renter2TemplateGUID',16,'uniqueidentifier',2,0,0,0,'ds_EntityRenter2Template','租户模板实例表','','',0) ; insert into data_dict(guid , tableguid , field_name_c , field_name , width ,data_type , field_sequence, b_null , b_pk , b_sys , table_name ,	table_name_c , Description , defaultvalue , b_identity) values  ('71940947-7ec4-4804-8f41-7fc04133af60' , 'b60007dd-3e6e-4e83-bafc-0f01ebfc9597','租户模板实例名称','RenterTemplateEntityName',500,'varchar',3,0,0,0,'ds_EntityRenter2Template','租户模板实例表','','',0) ; insert into data_dict(guid , tableguid , field_name_c , field_name , width ,data_type , field_sequence, b_null , b_pk , b_sys , table_name ,	table_name_c , Description , defaultvalue , b_identity) values  ('b219ada0-dba9-4653-8aa8-e84211d5d9f5' , 'b60007dd-3e6e-4e83-bafc-0f01ebfc9597','租户GUID','RenterGUID',16,'uniqueidentifier',4,0,0,0,'ds_EntityRenter2Template','租户模板实例表','','',0) ; insert into data_dict(guid , tableguid , field_name_c , field_name , width ,data_type , field_sequence, b_null , b_pk , b_sys , table_name ,	table_name_c , Description , defaultvalue , b_identity) values  ('48625489-472e-47d8-9a8d-ba0004a9e01e' , 'b60007dd-3e6e-4e83-bafc-0f01ebfc9597','模板GUID','TemplateGUID',16,'uniqueidentifier',5,0,0,0,'ds_EntityRenter2Template','租户模板实例表','','',0) ; insert into data_dict(guid , tableguid , field_name_c , field_name , width ,data_type , field_sequence, b_null , b_pk , b_sys , table_name ,	table_name_c , Description , defaultvalue , b_identity) values  ('04aeac57-2f14-4859-a188-ebad96719130' , 'b60007dd-3e6e-4e83-bafc-0f01ebfc9597','实例发布相对地址','JsonDataFilePath',1000,'varchar',6,0,0,0,'ds_EntityRenter2Template','租户模板实例表','','',0) ; insert into data_dict(guid , tableguid , field_name_c , field_name , width ,data_type , field_sequence, b_null , b_pk , b_sys , table_name ,	table_name_c , Description , defaultvalue , b_identity) values  ('c358bbc8-e6c7-412e-a784-6e24d178672b' , 'b60007dd-3e6e-4e83-bafc-0f01ebfc9597','实例是否禁用','IsDisabled',4,'int',7,0,0,0,'ds_EntityRenter2Template','租户模板实例表','','',0) ; 
GO 
-------------------------------------------------【END】新增ds_EntityRenter2Template表------------------------------------------------- 
 
 
-------------------------------------------------【BEGIN】新增ds_EntityPage表------------------------------------------------- 
IF OBJECT_ID('ds_EntityPage') IS NULL 
BEGIN 
     CREATE TABLE ds_EntityPage([EntityPageGUID] [UNIQUEIDENTIFIER] PRIMARY KEY CLUSTERED NOT NULL  
          ,[EntityRenter2TemplateGUID] [UNIQUEIDENTIFIER] 
          ,[PageNum] [INT] 
          ,[JsonExample] [VARCHAR](MAX)
          ,[PageMemo] [VARCHAR](1000) 
     )
END
GO 
 
delete from data_dict where table_name='ds_EntityPage' ; insert into data_dict(guid , tableguid , field_name_c , field_name , width ,data_type , field_sequence, b_null , b_pk , b_sys , table_name ,	table_name_c , Description , defaultvalue , b_identity) values  ('01f7fba5-6616-42df-aef8-5aaa5bf5f936' , 'b60007dd-3e6e-4e83-bafc-0f01ebfc9597','页面GUID','EntityPageGUID',16,'uniqueidentifier',1,0,1,0,'ds_EntityPage','页面实例表','','',0) ; insert into data_dict(guid , tableguid , field_name_c , field_name , width ,data_type , field_sequence, b_null , b_pk , b_sys , table_name ,	table_name_c , Description , defaultvalue , b_identity) values  ('257b76c4-bca8-413c-9f6b-bf6be12c883e' , 'b60007dd-3e6e-4e83-bafc-0f01ebfc9597','租户模板实例GUID','EntityRenter2TemplateGUID',16,'uniqueidentifier',2,0,0,0,'ds_EntityPage','页面实例表','','',0) ; insert into data_dict(guid , tableguid , field_name_c , field_name , width ,data_type , field_sequence, b_null , b_pk , b_sys , table_name ,	table_name_c , Description , defaultvalue , b_identity) values  ('0f2e4194-0cf4-4e21-90b2-e461dd6b038a' , 'b60007dd-3e6e-4e83-bafc-0f01ebfc9597','页面顺序','PageNum',4,'int',3,0,0,0,'ds_EntityPage','页面实例表','','',0) ; insert into data_dict(guid , tableguid , field_name_c , field_name , width ,data_type , field_sequence, b_null , b_pk , b_sys , table_name ,	table_name_c , Description , defaultvalue , b_identity) values  ('8fa58434-3e3e-4820-bed0-ea76c6762415' , 'b60007dd-3e6e-4e83-bafc-0f01ebfc9597','Json样例','JsonExample',8000,'varchar',4,0,0,0,'ds_EntityPage','页面实例表','','',0) ; insert into data_dict(guid , tableguid , field_name_c , field_name , width ,data_type , field_sequence, b_null , b_pk , b_sys , table_name ,	table_name_c , Description , defaultvalue , b_identity) values  ('7af9fc49-47a7-4cb7-b22f-364981949671' , 'b60007dd-3e6e-4e83-bafc-0f01ebfc9597','页面描述','PageMemo',1000,'varchar',5,0,0,0,'ds_EntityPage','页面实例表','','',0) ; 
GO 
-------------------------------------------------【END】新增ds_EntityPage表------------------------------------------------- 
 
 
-------------------------------------------------【BEGIN】新增ds_EntityPageNodeData表------------------------------------------------- 
IF OBJECT_ID('ds_EntityPageNodeData') IS NULL 
BEGIN 
     CREATE TABLE ds_EntityPageNodeData([EntityPageNodeDataGUID] [UNIQUEIDENTIFIER] PRIMARY KEY CLUSTERED NOT NULL  
          ,[EntityPageNodeDataName] [VARCHAR](500) 
          ,[EntityPageGUID] [UNIQUEIDENTIFIER] 
          ,[CurNodeCode] [VARCHAR](500) 
          ,[ParentNodeCode] [VARCHAR](500) 
          ,[NodeSql] [VARCHAR](MAX)
          ,[IfSqlExcutedLooped] [INT] 
          ,[SqlExcutedLoopedNum] [INT] 
          ,[IfEnd] [INT] 
          ,[IfDataHandled] [INT] 
          ,[DataHandleAssemble] [VARCHAR](200) 
          ,[DataHandleFunction] [VARCHAR](200) 
     )
END
GO 
 
delete from data_dict where table_name='ds_EntityPageNodeData' ; insert into data_dict(guid , tableguid , field_name_c , field_name , width ,data_type , field_sequence, b_null , b_pk , b_sys , table_name ,	table_name_c , Description , defaultvalue , b_identity) values  ('dfd06f86-5a4d-4edf-953e-13fd8ecb7042' , 'b60007dd-3e6e-4e83-bafc-0f01ebfc9597','页面数据结构GUID','EntityPageNodeDataGUID',16,'uniqueidentifier',1,0,1,0,'ds_EntityPageNodeData','页面数据结构实例表','','',0) ; insert into data_dict(guid , tableguid , field_name_c , field_name , width ,data_type , field_sequence, b_null , b_pk , b_sys , table_name ,	table_name_c , Description , defaultvalue , b_identity) values  ('89049dd2-1a90-40fd-a02c-e5d73b7ec290' , 'b60007dd-3e6e-4e83-bafc-0f01ebfc9597','节点名称','EntityPageNodeDataName',500,'varchar',2,0,0,0,'ds_EntityPageNodeData','页面数据结构实例表','','',0) ; insert into data_dict(guid , tableguid , field_name_c , field_name , width ,data_type , field_sequence, b_null , b_pk , b_sys , table_name ,	table_name_c , Description , defaultvalue , b_identity) values  ('3cf42f17-f007-4b54-8e5b-acde77f5cff7' , 'b60007dd-3e6e-4e83-bafc-0f01ebfc9597','页面GUID','EntityPageGUID',16,'uniqueidentifier',3,0,0,0,'ds_EntityPageNodeData','页面数据结构实例表','','',0) ; insert into data_dict(guid , tableguid , field_name_c , field_name , width ,data_type , field_sequence, b_null , b_pk , b_sys , table_name ,	table_name_c , Description , defaultvalue , b_identity) values  ('776a1369-66f7-4427-8ab0-4e05b6a27bf3' , 'b60007dd-3e6e-4e83-bafc-0f01ebfc9597','当前层级Code','CurNodeCode',500,'varchar',4,0,0,0,'ds_EntityPageNodeData','页面数据结构实例表','','',0) ; insert into data_dict(guid , tableguid , field_name_c , field_name , width ,data_type , field_sequence, b_null , b_pk , b_sys , table_name ,	table_name_c , Description , defaultvalue , b_identity) values  ('35c254d9-f138-41a0-a7f3-a9d6453d1951' , 'b60007dd-3e6e-4e83-bafc-0f01ebfc9597','父级层级Code','ParentNodeCode',500,'varchar',5,0,0,0,'ds_EntityPageNodeData','页面数据结构实例表','','',0) ; insert into data_dict(guid , tableguid , field_name_c , field_name , width ,data_type , field_sequence, b_null , b_pk , b_sys , table_name ,	table_name_c , Description , defaultvalue , b_identity) values  ('7f8202f1-558f-4b7a-96ce-02375645bd5c' , 'b60007dd-3e6e-4e83-bafc-0f01ebfc9597','节点Sql','NodeSql',8000,'varchar',6,0,0,0,'ds_EntityPageNodeData','页面数据结构实例表','','',0) ; insert into data_dict(guid , tableguid , field_name_c , field_name , width ,data_type , field_sequence, b_null , b_pk , b_sys , table_name ,	table_name_c , Description , defaultvalue , b_identity) values  ('4d0038f4-69a4-4362-a37b-255b6f2e8f59' , 'b60007dd-3e6e-4e83-bafc-0f01ebfc9597','是否循环调用Sql','IfSqlExcutedLooped',4,'int',7,0,0,0,'ds_EntityPageNodeData','页面数据结构实例表','','',0) ; insert into data_dict(guid , tableguid , field_name_c , field_name , width ,data_type , field_sequence, b_null , b_pk , b_sys , table_name ,	table_name_c , Description , defaultvalue , b_identity) values  ('44aee2ba-09b0-43e2-a377-ba6fe99f1ca0' , 'b60007dd-3e6e-4e83-bafc-0f01ebfc9597','循环调用单次返回数量','SqlExcutedLoopedNum',4,'int',8,0,0,0,'ds_EntityPageNodeData','页面数据结构实例表','','',0) ; insert into data_dict(guid , tableguid , field_name_c , field_name , width ,data_type , field_sequence, b_null , b_pk , b_sys , table_name ,	table_name_c , Description , defaultvalue , b_identity) values  ('4d830052-1845-4e55-8337-b6c659579d2c' , 'b60007dd-3e6e-4e83-bafc-0f01ebfc9597','是否末级','IfEnd',4,'int',9,0,0,0,'ds_EntityPageNodeData','页面数据结构实例表','','',0) ; insert into data_dict(guid , tableguid , field_name_c , field_name , width ,data_type , field_sequence, b_null , b_pk , b_sys , table_name ,	table_name_c , Description , defaultvalue , b_identity) values  ('d5e479cd-956a-464f-af5c-376e5dc410ec' , 'b60007dd-3e6e-4e83-bafc-0f01ebfc9597','是否加工数据','IfDataHandled',4,'int',10,0,0,0,'ds_EntityPageNodeData','页面数据结构实例表','','',0) ; insert into data_dict(guid , tableguid , field_name_c , field_name , width ,data_type , field_sequence, b_null , b_pk , b_sys , table_name ,	table_name_c , Description , defaultvalue , b_identity) values  ('489ea518-d956-4769-b4c3-e11ea80b60d5' , 'b60007dd-3e6e-4e83-bafc-0f01ebfc9597','加工映射类','DataHandleAssemble',200,'varchar',11,0,0,0,'ds_EntityPageNodeData','页面数据结构实例表','','',0) ; insert into data_dict(guid , tableguid , field_name_c , field_name , width ,data_type , field_sequence, b_null , b_pk , b_sys , table_name ,	table_name_c , Description , defaultvalue , b_identity) values  ('bd329436-9025-4c42-a01e-99adfecc804b' , 'b60007dd-3e6e-4e83-bafc-0f01ebfc9597','加工映射方法','DataHandleFunction',200,'varchar',12,0,0,0,'ds_EntityPageNodeData','页面数据结构实例表','','',0) ; 
GO 
-------------------------------------------------【END】新增ds_EntityPageNodeData表------------------------------------------------- 

IF COL_LENGTH('ds_EntityPage', 'OriginalPageNum') IS NULL 
    BEGIN 
        ALTER TABLE ds_EntityPage ADD OriginalPageNum INT
        INSERT  INTO data_dict
                ( guid ,
                  tableguid ,
                  field_name_c ,
                  field_name ,
                  width ,
                  data_type ,
                  field_sequence ,
                  b_null ,
                  b_pk ,
                  b_sys ,
                  table_name ,
                  table_name_c ,
                  DESCRIPTION ,
                  defaultvalue ,
                  b_identity
                )
                SELECT TOP 1
                        NEWID() ,
                        TableGUID ,
                        '原始页面顺序' ,
                        'OriginalPageNum' ,
                        '' ,
                        'int' ,
                        field_sequence + 1 ,
                        1 ,
                        0 ,
                        0 ,
                        table_name ,
                        table_name_c ,
                        '' ,
                        '' ,
                        0
                FROM    data_dict dd
                WHERE   dd.table_name = 'ds_EntityPage'
                ORDER BY dd.field_sequence DESC 
    END 
GO
 
 
